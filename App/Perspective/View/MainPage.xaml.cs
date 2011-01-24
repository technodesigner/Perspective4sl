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
using Perspective.ViewModel;

namespace Perspective.View
{
    /// <summary>
    /// The main UI.
    /// </summary>
    public partial class MainPage : UserControl
    {
        private bool _frameVisible = false;

        /// <summary>
        /// Initializes a new instance of MainPage.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ExtensionViewModel evm = this.Resources["ExtensionViewModel"] as ExtensionViewModel;
            evm.PropertyChanged +=
                (sender1, e1) =>
                {
                    if (e1.PropertyName == ExtensionViewModel.CurrentPageLinkKey)
                    {
                        if (frame.Source == evm.CurrentPageLink.Uri)
                        {
                            frame.Refresh(); // To force the Navigated event
                        }
                        else
                        {
                            // Without UriMapper
                            // frame.Source = evm.CurrentPageLink.OriginalUri;

                            // With UriMapper
                            frame.Source = evm.CurrentPageLink.Uri;
                        }
                    }
                };
        }

        private void frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ShowFrameWithAnimation();
        }

        private void extensionListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            HideFrameWithAnimation();
        }

        private void ShowFrameWithAnimation()
        {
            if (!_frameVisible)
            {
                _frameVisible = true;
                frame.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void HideFrameWithAnimation()
        {
            if (_frameVisible)
            {
                // frame is collapsed so the current page can't get the focus
                frame.Visibility = System.Windows.Visibility.Collapsed;
                _frameVisible = false;
            }
        }
    }
}
