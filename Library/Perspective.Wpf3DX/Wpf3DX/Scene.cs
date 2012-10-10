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
using System.IO;
using System.Windows;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Perspective.Wpf3DX.Models;
using System.Windows.Markup;
using Perspective.Wpf3DX.Lights;

namespace Perspective.Wpf3DX
{
    /// <summary>
    /// Represents the 3D scene. Manages the camera (view / projection) transforms and draws 3D visuals.
    /// </summary>
    [ContentProperty("Models")]
    public class Scene
    {
        private List<Model> _models = new List<Model>();

        /// <summary>
        /// Gets the model collection.
        /// </summary>
        public List<Model> Models
        {
            get { return _models; }
            set { _models = value; }
        }

        PerspectiveCamera _camera = new PerspectiveCamera();

        /// <summary>
        /// Gets or sets the camera.
        /// </summary>
        public PerspectiveCamera Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }

        internal VertexShader _vertexShader;
        internal PixelShader _pixelShader;

        internal Vector4 _ambientLightVector = Vector4.Zero;
        internal Vector4 _directionalLightVector1 = Vector4.Zero;
        internal Vector4 _directionalLightVector2 = Vector4.Zero;
        internal Vector4 _directionalLightVector3 = Vector4.Zero;
        internal Vector4 _directionalLightVector4 = Vector4.Zero;

        /// <summary>
        /// Indicates if the default lights should be used.
        /// The default value is true.
        /// </summary>
        public bool DefaultLighting { get; set; }

        /// <summary>
        /// Initializes a new instance of the Scene class.
        /// </summary>
        public Scene()
        {
            DefaultLighting = true;
        }

        /// <summary>
        /// Invalidates the camera projection matrix.
        /// </summary>
        /// <param name="aspectRatio">The new aspect ratio.</param>
        public void InvalidateProjection(float aspectRatio)
        {
            _camera.AspectRatio = aspectRatio;
            _camera.InvalidateProjection();
        }

        /// <summary>
        /// Initializes the 3D scene : loads the shaders and initializes the visuals.
        /// </summary>
        public void Initialize()
        {
            //try
            //{
                Helper3D.Check3DAvailability();
                GraphicsDevice graphicsDevice = Helper3D.GraphicsDevice;
                Stream shaderStream = Application.GetResourceStream(new Uri(@"Perspective.Wpf3DX;component/Wpf3DX/Shaders/Perspective.VertexShader", UriKind.Relative)).Stream;
                _vertexShader = VertexShader.FromStream(graphicsDevice, shaderStream);
                shaderStream = Application.GetResourceStream(new Uri(@"Perspective.Wpf3DX;component/Wpf3DX/Shaders/Perspective.PixelShader", UriKind.Relative)).Stream;
                _pixelShader = PixelShader.FromStream(graphicsDevice, shaderStream);
                InvalidateLights();
                _camera.InvalidateView();
                foreach (var visual in _models)
                {
                    visual.Initialize(this, Matrix.Identity);
                }
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message, e.GetType().ToString(), MessageBoxButton.OK);
            //}
        }

        /// <summary>
        /// Gets or sets the ambient light of the scene.
        /// </summary>
        public AmbientLight AmbientLight { get; set; }

        /// <summary>
        /// Gets or sets the first directional light of the scene.
        /// </summary>
        public DirectionalLight DirectionalLight1 { get; set; }

        /// <summary>
        /// Gets or sets the second directional light of the scene.
        /// </summary>
        public DirectionalLight DirectionalLight2 { get; set; }

        /// <summary>
        /// Gets or sets the third directional light of the scene.
        /// </summary>
        public DirectionalLight DirectionalLight3 { get; set; }

        /// <summary>
        /// Gets or sets the fourth directional light of the scene.
        /// </summary>
        public DirectionalLight DirectionalLight4 { get; set; }

        /// <summary>
        /// Invalidates the lights.
        /// </summary>
        public void InvalidateLights()
        {
            if (DefaultLighting && (AmbientLight == null))
            {
                AmbientLight = new AmbientLight();
            }
            if (AmbientLight != null)
            {
                _ambientLightVector = new Vector4(0.0f, 0.0f, 0.0f, AmbientLight.Intensity); // X (unused), Y (unused), Z (unused), W (intensity) 
            }
            if (DefaultLighting && (DirectionalLight1 == null))
            {
                DirectionalLight1 = new DirectionalLight();
                DirectionalLight1.Direction = new Vector3(0.0f, -0.25f, 1.0f);
            }
            if (DirectionalLight1 != null)
            {
                Vector3 v = DirectionalLight1.Direction;
                v.Normalize();
                _directionalLightVector1 = new Vector4(v, DirectionalLight1.Intensity);
            }
            if (DefaultLighting && (DirectionalLight2 == null))
            {
                DirectionalLight2 = new DirectionalLight();
                DirectionalLight2.Direction = new Vector3(-0.75f, -0.5f, -1.0f);
            }
            if (DirectionalLight2 != null)
            {
                Vector3 v = DirectionalLight2.Direction;
                v.Normalize();
                _directionalLightVector2 = new Vector4(v, DirectionalLight2.Intensity);
            }
            if (DefaultLighting && (DirectionalLight3 == null))
            {
                DirectionalLight3 = new DirectionalLight();
                DirectionalLight3.Direction = new Vector3(1.0f, 0.0f, -1.0f);
                DirectionalLight3.Intensity = 0.5f;
            }
            if (DirectionalLight3 != null)
            {
                Vector3 v = DirectionalLight3.Direction;
                v.Normalize();
                _directionalLightVector3 = new Vector4(v, DirectionalLight3.Intensity);
            }
            if (DefaultLighting && (DirectionalLight4 == null))
            {
                DirectionalLight4 = new DirectionalLight();
                DirectionalLight4.Direction = new Vector3(0.0f, 1.0f, 0.0f);
                DirectionalLight4.Intensity = 0.5f;
            }
            if (DirectionalLight4 != null)
            {
                Vector3 v = DirectionalLight4.Direction;
                v.Normalize();
                _directionalLightVector4 = new Vector4(v, DirectionalLight4.Intensity);
            }

        }

        /// <summary>
        /// Adds dynamically a model to the scene, using a thread-safe way
        /// </summary>
        /// <param name="model"></param>
        public void AddModel(Model model)
        {
            lock (this)
            {
                Models.Add(model);
            }
        }

        /// <summary>
        /// Draws the scene.
        /// </summary>
        /// <param name="graphicsDevice">The graphic device.</param>
        public void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Transparent, 1.0f, 0);
            graphicsDevice.RasterizerState = Helper3D.DefaultRasterizerState;
            lock (this) // to prevent InvalidOperationException during a collection modification
            {
                foreach (var shape in _models)
                {
                    shape.OnRender(new RenderEventArgs(graphicsDevice, _camera.ViewProjection));
                }
            }
            //foreach (var shape in _models)
            //{
            //    shape.OnRender(new RenderEventArgs(graphicsDevice, _camera.ViewProjection));
            //}
        }
    }
}
