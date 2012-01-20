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
using System.Collections.Generic;
using System.Windows.Markup;
using Microsoft.Xna.Framework;

namespace Perspective.Wpf3DX.Models
{
    /// <summary>
    /// Represents a composite model.
    /// </summary>
    [ContentProperty("Children")]
    public class CompositeModel : Model
    {
        private List<Model> _children = new List<Model>();

        /// <summary>
        /// Gets a list of child elements of this object.
        /// </summary>
        public List<Model> Children
        {
            get { return _children; }
        }

        /// <summary>
        /// Initializes the visual.
        /// </summary>
        /// <param name="scene">The Scene object.</param>
        /// <param name="ContainerMatrix">The container matrix.</param>
        public override void Initialize(Scene scene, Matrix containerMatrix)
        {
            ContainerMatrix = containerMatrix;
            InvalidateTransform();
            foreach (var visual in _children)
            {
                visual.Initialize(scene, Matrix);
            }
            base.Initialize(scene, Matrix);
        }

        /// <summary>
        /// Invalidates the transform of the visual.
        /// </summary>
        /// <remarks>This initializes the Matrix property.</remarks>
        public override void InvalidateTransform()
        {
            base.InvalidateTransform();
            foreach (var visual in _children)
            {
                visual.ContainerMatrix = Matrix;
                visual.InvalidateTransform();
            }
        }

        /// <summary>
        /// Throws the Render event.
        /// </summary>
        /// <param name="e">The event data.</param>
        public override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);
            foreach (var visual in _children)
            {
                visual.OnRender(e);
            }
        }
    }
}
