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
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Perspective.Config.Strings;

namespace Perspective.Config.View
{
    /// <summary>
    /// A configuration page for OOB mode.
    /// </summary>
    public partial class OutOfBrowserConfig : Page
    {
        /// <summary>
        /// Initializes a new instance of OutOfBrowserConfig.
        /// </summary>
        public OutOfBrowserConfig()
        {
            InitializeComponent();
            this.DataContext = new StringProvider();
            this.Title = StringProvider.OutOfBrowserConfig_PageTitle;
            Application.Current.InstallStateChanged += (sender, e) =>
            {
                CheckButtonState();

            };

            // not yet compatible with the Perspective extension system
            //Application.Current.CheckAndDownloadUpdateCompleted += (sender, e) =>
            //{
            //    if (e.UpdateAvailable)
            //    {
            //        MessageBox.Show("Update available. Please restart the application.");
            //    }
            //};
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CheckButtonState();
        }

        private void CheckButtonState()
        {
            installButton.IsEnabled =
                (!Application.Current.IsRunningOutOfBrowser
                && (Application.Current.InstallState == InstallState.NotInstalled));
            //updateButton.IsEnabled = Application.Current.IsRunningOutOfBrowser;
            updateButton.IsEnabled = false;
        }
        
        private void installButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Install();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            // not yet compatible with the Perspective extension system
            // Application.Current.CheckAndDownloadUpdateAsync();
        }
    }
}
