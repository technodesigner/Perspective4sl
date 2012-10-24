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
using Perspective.Wpf3DX.Transforms;
using Perspective.Wpf3DX.Textures;

namespace Perspective.Demo3D.View
{
    public partial class DynamicSceneDemo : Page
    {
        public DynamicSceneDemo()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            redSlider.Value = Helper3D.DefaultColor.R;
            greenSlider.Value = Helper3D.DefaultColor.G;
            blueSlider.Value = Helper3D.DefaultColor.B;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void resetSceneButton_Click(object sender, RoutedEventArgs e)
        {
            CreateScene(workshop3DX);
        }

        private void axisButton_Click(object sender, RoutedEventArgs e)
        {
            AddAxisToScene(workshop3DX);
        }

        private void boxButton_Click(object sender, RoutedEventArgs e)
        {
            AddBoxToScene(
                workshop3DX, 
                (float)scaleSlider.Value,
                (float)redSlider.Value / 255,
                (float)greenSlider.Value / 255,
                (float)blueSlider.Value / 255);
        }

        private void sphericalButton_Click(object sender, RoutedEventArgs e)
        {
            AddSphericalToScene(
                workshop3DX,
                (float)scaleSlider.Value,
                (float)redSlider.Value / 255,
                (float)greenSlider.Value / 255,
                (float)blueSlider.Value / 255);
        }

        private void conicalButton_Click(object sender, RoutedEventArgs e)
        {
            AddConicalToScene(
                workshop3DX, 
                (float)scaleSlider.Value,
                (float)redSlider.Value / 255,
                (float)greenSlider.Value / 255,
                (float)blueSlider.Value / 255);
        }

        private void CreateScene(Wpf.Controls.Workshop3DX workshop3DX)
        {
            var camera = new PerspectiveCamera();
            camera.Position = new Vector3(10.0f, 2.0f, 10.0f);
            camera.LookTarget = new Vector3(0.0f, 1.0f, 0.0f);
            camera.FieldOfView = 25.0f;

            var scene = new Scene();
            scene.Camera = camera;

            workshop3DX.Scene = scene;
            workshop3DX.InvalidateProjection();
        }

        private void AddAxisToScene(Wpf.Controls.Workshop3DX workshop3DX)
        {
            var axis = new XyzAxis();
            axis.Length = 3.0f;
            workshop3DX.Scene.AddModel(axis);
            workshop3DX.Scene.Initialize();
        }

        private void AddBoxToScene(
            Wpf.Controls.Workshop3DX workshop3DX, 
            float scaleFactor,
            float r, float g, float b)
        {
            var box = new Box();
            box.Texture = new ColorTexture(r, g, b, 1.0f);
            box.Transform = GetScalingAndRandomTranslation(scaleFactor);
            workshop3DX.Scene.AddModel(box);
            workshop3DX.Scene.Initialize();
        }

        private void AddSphericalToScene(
            Wpf.Controls.Workshop3DX workshop3DX, 
            float scaleFactor,
            float r, float g, float b)
        {
            var spherical = new Spherical();
            spherical.ParallelCount = 80;
            spherical.Texture = new ColorTexture(r, g, b, 1.0f);
            spherical.Material = GetMaterial();
            spherical.Transform = GetScalingAndRandomTranslation(scaleFactor);
            workshop3DX.Scene.AddModel(spherical);
            workshop3DX.Scene.Initialize();
        }

        private void AddConicalToScene(
            Wpf.Controls.Workshop3DX workshop3DX, 
            float scaleFactor,
            float r, float g, float b)
        {
            var conical = new Conical();
            conical.SideCount = 100;
            conical.Texture = new ColorTexture(r, g, b, 1.0f);
            conical.Material = GetMaterial(); 
            conical.Transform = GetScalingAndRandomTranslation(scaleFactor);
            workshop3DX.Scene.AddModel(conical);
            workshop3DX.Scene.Initialize();
        }

        private ModelMaterial GetMaterial()
        {
            return new ModelMaterial()
            {
                Diffuseness = 1.0f,
                Specularness = 1.0f,
                Shininess = 0.8f
            };
        }

        private Random _random = new Random();
        private float GetRandomOffset()
        {
            return ((float)_random.NextDouble() * 6) - 3.0f;
        }

        private ModelTransformGroup GetScalingAndRandomTranslation(float scaleFactor)
        {
            var transformGroup = new ModelTransformGroup();
            var scaling = new Scaling(scaleFactor, scaleFactor, scaleFactor);
            transformGroup.Children.Add(scaling);
            var translation = new Translation();
            translation.OffsetX = GetRandomOffset();
            translation.OffsetY = GetRandomOffset();
            translation.OffsetZ = GetRandomOffset();
            transformGroup.Children.Add(translation);
            return transformGroup;
        }
    }
}
