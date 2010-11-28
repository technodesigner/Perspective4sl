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
    public partial class MainPage : UserControl
    {
        private bool _frameVisible = false;
        //private Storyboard _frameShowStoryboard = new Storyboard();
        //private Storyboard _frameHideStoryboard = new Storyboard();
        //private Duration _duration = new Duration(TimeSpan.FromMilliseconds(600));
        //private IEasingFunction _easeIn = new ExponentialEase();
        //private IEasingFunction _easeOut = new ExponentialEase();

        public MainPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // (_easeOut as ExponentialEase).EasingMode = EasingMode.EaseOut;

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
                            // Sans UriMapper
                            // frame.Source = evm.CurrentPageLink.OriginalUri;

                            // Avec UriMapper
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
                // _frameShowStoryboard.Begin();
            }
        }

        private void HideFrameWithAnimation()
        {
            if (_frameVisible)
            {
                // _frameHideStoryboard.Begin();
                // frame is collapsed so the current page can't get the focus
                frame.Visibility = System.Windows.Visibility.Collapsed;
                _frameVisible = false;
            }
        }
    }
}
