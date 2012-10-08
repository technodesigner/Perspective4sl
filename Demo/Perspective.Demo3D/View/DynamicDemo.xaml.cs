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
using Perspective.Wpf3DX;
using Perspective.Wpf3DX.Models;
using Microsoft.Xna.Framework;

namespace Perspective.Demo3D.View
{
    public partial class DynamicDemo : Page
    {
        public DynamicDemo()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var camera = new PerspectiveCamera();
            camera.Position = new Vector3(10.0f, 2.0f, 10.0f);
            camera.LookTarget = new Vector3(0.0f, 1.0f, 0.0f);
            camera.FieldOfView = 25.0f;

            var axis = new XyzAxis();
            axis.Length = 3.0f;

            var box = new Box();

            var scene = new Scene();
            scene.Camera = camera;
            scene.Models.Add(axis);
            scene.Models.Add(box);
            scene.Initialize();
            // scene.InvalidateProjection((float)workshop3DX.ActualWidth / (float)workshop3DX.ActualHeight);
            
            workshop3DX.Scene = scene;
            workshop3DX.InvalidateProjection();
        }

    }
}
