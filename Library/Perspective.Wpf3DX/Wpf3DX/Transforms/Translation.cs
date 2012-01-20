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
    /// Translates an object in the three-dimensional x-y-z plane. 
    /// </summary>
    public class Translation : ModelTransform
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public Translation()
        {
        }

        /// <summary>
        /// Initializes a new instance using the specified offset.
        /// </summary>
        /// <param name="offsetX">The X value of the Vector3 that specifies the translation offset.</param>
        /// <param name="offsetY">The Y value of the Vector3 that specifies the translation offset.</param>
        /// <param name="offsetZ">The Z value of the Vector3 that specifies the translation offset.</param>
        public Translation(float offsetX, float offsetY, float offsetZ)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
            OffsetZ = offsetZ;
        }

        /// <summary>
        /// Gets or sets the X value of the Vector3 that specifies the translation offset.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float OffsetX { get; set; }

        /// <summary>
        /// Gets or sets the Y value of the Vector3 that specifies the translation offset.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float OffsetY { get; set; }

        /// <summary>
        /// Gets or sets the Z value of the Vector3 that specifies the translation offset.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float OffsetZ { get; set; }

        /// <summary>
        /// Gets the transformation matrix.
        /// </summary>
        /// <returns>A matrix object.</returns>
        [TypeConverter(typeof(FloatConverter))]
        protected override Matrix GetMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(OffsetX, OffsetY, OffsetZ));
        }
    }
}
