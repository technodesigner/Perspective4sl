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
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows;
using Microsoft.Xna.Framework;
using System.Windows.Markup;
using System.Collections.Generic;

namespace Perspective.Wpf3DX.Transforms
{
    /// <summary>
    /// Represents a transformation that is a composite of the transform children in its List collection. 
    /// </summary>
    [ContentProperty("Children")]
    public class ModelTransformGroup : ModelTransform
    {
        List<ModelTransform> _children = new List<ModelTransform>();

        /// <summary>
        /// Gets a collection of transform objects. 
        /// </summary>
        public List<ModelTransform> Children
        {
            get { return _children; }
        }

        /// <summary>
        /// Gets the transformation matrix.
        /// </summary>
        /// <returns>A matrix object.</returns>
        protected override Matrix GetMatrix()
        {
            Matrix matrix = Matrix.Identity;
            foreach (ModelTransform transform in _children)
            {
                matrix *= transform.Matrix;
            }
            return matrix;
        }
    }
}
