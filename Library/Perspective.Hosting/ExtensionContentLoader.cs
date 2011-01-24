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
using System.Threading;

namespace Perspective.Hosting
{
    /// <summary>
    ///  Provides extension page loading for the Silverlight navigation system
    ///  (the default PageResourceContentLoader working only for the main application XAP).
    ///  Thanks to these authors for their article about INavigationContentLoader :
    ///  Corrado Cavalli : http://silverlightfeeds.com/post/1531/Silverlight_4.0_INavigationContentLoader.aspx
    ///  Pencho Popadiyn : http://www.silverlightshow.net/items/Navigating-between-Pages-in-Different-Xaps-by-using-MEF.aspx
    ///  Mike Taultys : http://mtaulty.com/CommunityServer/blogs/mike_taultys_blog/archive/2009/11/18/silverlight-4-rough-notes-taking-control-of-navigation.aspx
    ///  David Hill : http://blogs.msdn.com/b/dphill/archive/2009/12/24/custom-content-loaders-in-silverlight-4-0.aspx
    /// </summary>
    public class ExtensionContentLoader : INavigationContentLoader
    {
        private PageResourceContentLoader _defaultContentLoader = new PageResourceContentLoader();
        private bool _defaultLoaderMode;

        #region INavigationContentLoader Members

        public IAsyncResult BeginLoad(Uri targetUri, Uri currentUri, AsyncCallback userCallback, object asyncState)
        {
            _defaultLoaderMode = false;
            ExtensionAsyncResult ear = new ExtensionAsyncResult(asyncState);

            int indexOfComponent = targetUri.OriginalString.IndexOf(";component/");
            if (indexOfComponent == -1)
            {
                _defaultLoaderMode = true;
                return _defaultContentLoader.BeginLoad(targetUri, currentUri, userCallback, asyncState);
            }

            var assemblyName = targetUri.OriginalString.Substring(1, indexOfComponent - 1);
            if (!ExtensionManager.Current.Assemblies.ContainsKey(assemblyName))
            {
                foreach (var link in ExtensionManager.Current.ExtensionLinks)
                {
                    if (link.Package.Equals(assemblyName))
                    {
                        Action actionWhenExtensionLoaded =
                            () =>
                            {
                                CreatePageInstance(targetUri, indexOfComponent, assemblyName, userCallback, ear);
                            };
                        ExtensionManager.Current.CheckExtension(link, actionWhenExtensionLoaded);
                        break;
                    }
                }
            }
            else
            {
                CreatePageInstance(targetUri, indexOfComponent, assemblyName, userCallback, ear);
            }
            return ear;
        }

        private void CreatePageInstance(Uri targetUri, int indexOfComponent, string assemblyName, AsyncCallback userCallback, ExtensionAsyncResult ear)
        {
            int i2 = indexOfComponent + 11;
            int i3 = targetUri.OriginalString.IndexOf(".xaml");
            StringBuilder sb = new StringBuilder(assemblyName);
            var assembly = ExtensionManager.Current.Assemblies[sb.ToString()];
            sb.Append('.');
            sb.Append(targetUri.OriginalString.Substring(i2, i3 - i2));
            sb.Replace('/', '.');
            sb.Append(", ");
            sb.Append(assembly.FullName);
            Type type = Type.GetType(sb.ToString(), true);
            ear.Result = Activator.CreateInstance(type);
            userCallback(ear);
            // ear.IsCompleted = true;
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

        private class ExtensionAsyncResult : IAsyncResult
        {
            public ExtensionAsyncResult(object asyncState)
            {
                AsyncState = asyncState;
            }

            public object Result { get; set; }

            #region IAsyncResult Members

            public object AsyncState { get; private set; }

            public WaitHandle AsyncWaitHandle
            {
                get
                {
                    return null;
                }
            }

            public bool CompletedSynchronously
            {
                get
                {
                    return true;
                }
            }

            public bool IsCompleted
            {
                get
                {
                    return true;
                }
            }

            #endregion
        }
    }
}