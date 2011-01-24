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
using System.Windows.Interop;
using Perspective.Config.Strings;

namespace Perspective.Config.View
{
    /// <summary>
    /// A configuration page for graphics.
    /// </summary>
    public partial class GraphicsConfig : Page
    {
        private System.Windows.Interop.Content _pluginVisual;

        /// <summary>
        /// Initializes a new instance of GraphicsConfig.
        /// </summary>
        public GraphicsConfig()
        {
            InitializeComponent();
            this.DataContext = new StringProvider();
            this.Title = StringProvider.GraphicsConfig_PageTitle;
            _pluginVisual = Application.Current.Host.Content;
            _pluginVisual.FullScreenOptions = FullScreenOptions.StaysFullScreenWhenUnfocused;
            _pluginVisual.FullScreenChanged += (sender, e) =>
            {
                if (_pluginVisual.IsFullScreen)
                {
                    fullScreenButton.Content = StringProvider.GraphicsConfig_Windowed;
                }
                else
                {
                    fullScreenButton.Content = StringProvider.GraphicsConfig_FullScreen;
                }
            };
        }

        private void fullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            _pluginVisual.IsFullScreen = !_pluginVisual.IsFullScreen;
        }

        private void cacheVisualizationCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Application.Current.Host.Settings.EnableCacheVisualization = cacheVisualizationCheckBox.IsChecked == true;
        }

    }
}
