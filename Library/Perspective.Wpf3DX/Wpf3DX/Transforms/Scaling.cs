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
using System.ComponentModel;

namespace Perspective.Wpf3DX.Transforms
{
    /// <summary>
    /// Scales an object in the three-dimensional x-y-z plane.
    /// </summary>
    public class Scaling : ModelTransform
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public Scaling()
        {
            ScaleX = 1.0f;
            ScaleY = 1.0f;
            ScaleZ = 1.0f;
        }

        /// <summary>
        /// Initializes a new instance using the specified scale factors.
        /// </summary>
        /// <param name="scaleX">Factor by which to scale the X value.</param>
        /// <param name="scaleY">Factor by which to scale the Y value.</param>
        /// <param name="scaleZ">Factor by which to scale the Z value.</param>
        public Scaling(float scaleX, float scaleY, float scaleZ)
        {
            ScaleX = scaleX;
            ScaleY = scaleY;
            ScaleZ = scaleZ;
        }

        /// <summary>
        /// Gets or sets the scale factor in the x-direction.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float ScaleX { get; set; }

        /// <summary>
        /// Gets or sets the scale factor in the y-direction.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float ScaleY { get; set; }

        /// <summary>
        /// Gets or sets the scale factor in the z-direction.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float ScaleZ { get; set; }

        /// <summary>
        /// Gets the transformation matrix.
        /// </summary>
        /// <returns>A matrix object.</returns>
        protected override Matrix GetMatrix()
        {
            return Matrix.CreateScale(new Vector3(ScaleX, ScaleY, ScaleZ));
        }
    }
}
