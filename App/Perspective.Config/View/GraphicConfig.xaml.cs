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

namespace Perspective.Config.View
{
    public partial class GraphicConfig : Page
    {
        private System.Windows.Interop.Content _pluginVisual;
        public GraphicConfig()
        {
            InitializeComponent();
            _pluginVisual = Application.Current.Host.Content;
            _pluginVisual.FullScreenOptions = FullScreenOptions.StaysFullScreenWhenUnfocused;
            _pluginVisual.FullScreenChanged += (sender, e) =>
            {
                if (_pluginVisual.IsFullScreen)
                {
                    fullScreenButton.Content = "Windowed";
                }
                else
                {
                    fullScreenButton.Content = "Full screen";
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
