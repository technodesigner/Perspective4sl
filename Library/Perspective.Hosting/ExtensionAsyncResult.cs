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
using System.Threading;

namespace Perspective.Hosting
{
    /// <summary>
    /// Thanks to Corrado Cavalli : http://silverlightfeeds.com/post/1531/Silverlight_4.0_INavigationContentLoader.aspx
    /// </summary>
    public class ExtensionAsyncResult : IAsyncResult
    {
        public ExtensionAsyncResult(object asyncState)
        {
            AsyncState = asyncState;
            AsyncWaitHandle = new ManualResetEvent(true);
        }

        public object Result { get; set; }

        #region IAsyncResult Members

        public object AsyncState { get; private set; }

        public System.Threading.WaitHandle AsyncWaitHandle { get; private set; }

        public bool CompletedSynchronously
        {
            get { return true; }
        }

        public bool IsCompleted
        {
            get { return true; }
        }

        #endregion
    }
}
