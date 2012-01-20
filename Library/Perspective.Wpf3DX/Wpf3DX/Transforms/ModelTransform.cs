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

namespace Perspective.Wpf3DX.Transforms
{
    /// <summary>
    /// Creates a transformation.
    /// </summary>
    public abstract class ModelTransform
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public Matrix Matrix
        {
            get { return GetMatrix(); }
        }

        /// <summary>
        /// Gets the transformation matrix.
        /// </summary>
        /// <returns>A matrix object.</returns>
        protected abstract Matrix GetMatrix();

        /// <summary>
        /// Transforms the specified Vector3.
        /// </summary>
        /// <param name="vector">Vector3 to transform.</param>
        /// <returns>Transformed Vector3.</returns>
        public Vector3 Transform(Vector3 vector)
        {
            return Vector3.Transform(vector, GetMatrix());
        }
    }
}
