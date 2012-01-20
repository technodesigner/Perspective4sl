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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Perspective.Wpf3DX.Transforms;

namespace Perspective.Wpf3DX.Models
{
    /// <summary>
    /// Provides services and properties that are common to 3-D models.
    /// </summary>
    public abstract class Model
    {
        /// <summary>
        /// Gets or sets the scene object.
        /// </summary>
        public Scene Scene { get; set; }

        /// <summary>
        /// Initializes the model.
        /// </summary>
        /// <param name="scene">The Scene object.</param>
        /// <param name="containerMatrix">The container matrix.</param>
        public virtual void Initialize(Scene scene, Matrix containerMatrix)
        {
            Scene = scene;
            OnInitialized(new EventArgs());
        }

        /// <summary>
        /// Occurs after initialization.
        /// </summary>
        public event EventHandler<EventArgs> Initialized;

        /// <summary>
        /// Throws the Initialized event.
        /// </summary>
        /// <param name="e">The event data.</param>
        public virtual void OnInitialized(EventArgs e)
        {
            if (Initialized != null)
            {
                Initialized(this, e);
            }
        }

        /// <summary>
        /// Occurs when rendering.
        /// </summary>
        public event EventHandler<RenderEventArgs> Render;

        /// <summary>
        /// Throws the Render event.
        /// </summary>
        /// <param name="e">The event data.</param>
        public virtual void OnRender(RenderEventArgs e)
        {
            if (Render != null)
            {
                Render(this, e);
            }
        }

        private ModelTransform _transform;

        /// <summary>
        /// Gets or sets the transform object.
        /// </summary>
        public ModelTransform Transform
        {
            get { return _transform; }
            set { _transform = value; }
        }

        private Matrix _matrix;

        /// <summary>
        /// Gets the visual's Matrix.
        /// </summary>
        protected Matrix Matrix
        {
            get { return _matrix; }
        }

        private Matrix _containerMatrix = Matrix.Identity;

        /// <summary>
        /// Gets or sets the container's matrix.
        /// </summary>
        public Matrix ContainerMatrix
        {
            get { return _containerMatrix; }
            set { _containerMatrix = value; }
        }

        /// <summary>
        /// Invalidates the transform of the model.
        /// </summary>
        /// <remarks>This initializes the Matrix property.</remarks>
        public virtual void InvalidateTransform()
        {
            if (this.Transform != null)
            {
                _matrix = _transform.Matrix * _containerMatrix;
            }
            else
            {
                _matrix = _containerMatrix;
            }
        }
    }
}
