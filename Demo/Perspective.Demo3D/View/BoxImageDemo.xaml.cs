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
using System.Windows.Resources;
using System.Windows.Media.Imaging;
using Perspective.Wpf3DX.Models;
using Perspective.Wpf3DX.Textures;
using Perspective.Core;

namespace Perspective.Demo3D.View
{
    public partial class BoxImageDemo : Page
    {
        public BoxImageDemo()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Box_Initialized(object sender, EventArgs e)
        {
            // Uri uri = new Uri("/ClientBin/Perspective.Config.Icon.png", UriKind.Relative);
            //BitmapImage bitmapImage = new BitmapImage();
            //bitmapImage.UriSource = UriHelper.GetHostFileUri("Perspective.Config.Icon.png");
            //var box = (Box)sender;
            //box.Texture = new BitmapTexture(bitmapImage);
            //box.InvalidateTexture();
            Uri uri = new Uri("/Perspective.Demo3D;component/BoxTexture.png", UriKind.Relative);
            StreamResourceInfo sri = Application.GetResourceStream(uri);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.SetSource(sri.Stream);
            var box = (Box)sender;
            box.Texture = new BitmapTexture(bitmapImage);
            box.InvalidateTexture();
        }

    }
}
