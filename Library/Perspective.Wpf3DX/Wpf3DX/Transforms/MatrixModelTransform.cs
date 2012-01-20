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
    /// Creates a transformation specified by a Matrix, used to manipulate objects or coordinate systems in 3-D world space.
    /// </summary>
    public class MatrixModelTransform : ModelTransform
    {
        /// <summary>
        /// Gets or sets the value the value at position (1, 1) of the transformation Matrix. 
        /// </summary>
        public float M11 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (1, 2) of the transformation Matrix. 
        /// </summary>
        public float M12 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (1, 3) of the transformation Matrix. 
        /// </summary>
        public float M13 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (1, 4) of the transformation Matrix. 
        /// </summary>
        public float M14 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (2, 1) of the transformation Matrix. 
        /// </summary>
        public float M21 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (2, 2) of the transformation Matrix. 
        /// </summary>
        public float M22 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (2, 3) of the transformation Matrix. 
        /// </summary>
        public float M23 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (2, 4) of the transformation Matrix. 
        /// </summary>
        public float M24 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (3, 1) of the transformation Matrix. 
        /// </summary>
        public float M31 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (3, 2) of the transformation Matrix. 
        /// </summary>
        public float M32 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (3, 3) of the transformation Matrix. 
        /// </summary>
        public float M33 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (3, 4) of the transformation Matrix. 
        /// </summary>
        public float M34 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (4, 1) of the transformation Matrix. 
        /// </summary>
        public float M41 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (4, 2) of the transformation Matrix. 
        /// </summary>
        public float M42 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (4, 3) of the transformation Matrix. 
        /// </summary>
        public float M43 { get; set; }
        
        /// <summary>
        /// Gets or sets the value the value at position (4, 4) of the transformation Matrix. 
        /// </summary>
        public float M44 { get; set; }

        /// <summary>
        /// Gets the transformation matrix.
        /// </summary>
        /// <returns>A matrix object.</returns>
        protected override Matrix GetMatrix()
        {
            return new Matrix(M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44);
        }
    }
}
