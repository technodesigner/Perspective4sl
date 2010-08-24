//------------------------------------------------------------------
//
//  For licensing information and to get the latest version go to:
//  http://www.codeplex.com/perspective4sl
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY
//  OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//  LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//  FITNESS FOR A PARTICULAR PURPOSE.
//
//------------------------------------------------------------------
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Text;

namespace Perspective.Hosting
{
    /// <summary>
    ///  Provides extension page loading for the Silverlight navigation system
    ///  (the default PageResourceContentLoader working only for the main application XAP).
    ///  Thanks to Corrado Cavalli for his article : http://silverlightfeeds.com/post/1531/Silverlight_4.0_INavigationContentLoader.aspx
    ///  Thanks to Pencho Popadiyn for his article : http://www.silverlightshow.net/items/Navigating-between-Pages-in-Different-Xaps-by-using-MEF.aspx
    /// </summary>
    public class ExtensionContentLoader : INavigationContentLoader
    {
        private PageResourceContentLoader _defaultContentLoader = new PageResourceContentLoader();
        private bool _defaultLoaderMode;

        #region INavigationContentLoader Members

        public IAsyncResult BeginLoad(Uri targetUri, Uri currentUri, AsyncCallback userCallback, object asyncState)
        {
            _defaultLoaderMode = false;
            var ear = new ExtensionAsyncResult(asyncState);

            int i = targetUri.OriginalString.IndexOf(";component/");
            if (i == -1)
            {
                _defaultLoaderMode = true;
                return _defaultContentLoader.BeginLoad(targetUri, currentUri, userCallback, asyncState);
            }

            int i1 = i - 1;
            int i2 = i + 11;
            int i3 = targetUri.OriginalString.IndexOf(".xaml");

            StringBuilder sb = new StringBuilder(targetUri.OriginalString.Substring(1, i1));
            var assembly = ExtensionManager.Assemblies[sb.ToString()];
            sb.Append('.');
            sb.Append(targetUri.OriginalString.Substring(i2, i3 - i2));
            sb.Replace('/', '.');
            sb.Append(", ");
            sb.Append(assembly.FullName);
            Type type = Type.GetType(sb.ToString(), true);
            ear.Result = Activator.CreateInstance(type);

            userCallback(ear);
            return ear;
        }

        public bool CanLoad(Uri targetUri, Uri currentUri)
        {
            return true;
        }

        public void CancelLoad(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }

        public LoadResult EndLoad(IAsyncResult asyncResult)
        {
            if (_defaultLoaderMode)
            {
                return _defaultContentLoader.EndLoad(asyncResult);
            }
            else
            {
                return new LoadResult((asyncResult as ExtensionAsyncResult).Result);
            }
        }

        #endregion
    }
}
