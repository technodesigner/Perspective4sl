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

namespace Perspective.Config.View
{
    public partial class OutOfBrowserConfig : Page
    {
        public OutOfBrowserConfig()
        {
            InitializeComponent();
            Application.Current.InstallStateChanged += (sender, e) =>
            {
                CheckButtonState();

            };
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
            // Application.Current.CheckAndDownloadUpdateAsync();
        }
    }
}
