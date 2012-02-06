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
using Perspective.Wpf3DX.Sculptors;
using System.ComponentModel;

namespace Perspective.Wpf3DX.Models
{
    /// <summary>
    /// A 3D arrow element.
    /// By default, the direction of the arrow is the Z axis, and the length is 1.0.
    /// Default radius of the body is 0.1.
    /// Default radius of the head is 0.2.
    /// Default ratio of the head to the first unit is 0.8 (instead of 0.2 for the WPF equivalent Arrow model).
    /// </summary>
    /// <remarks>The Arrow model has been renamed as Dart (because there is already a 2D Arrow class in Perspective).</remarks>
    public class Dart : Sculpture
    {
        public Dart()
        {
            Length = DartSculptor.DefaultLength;
        }

        /// <summary>
        /// Gets or sets the axis length.
        /// </summary>        
        [TypeConverter(typeof(FloatConverter))]
        public float Length { get; set; }
        
        /// <summary>
        /// Initializes a new specific Sculptor instance.
        /// </summary>
        /// <returns>An ArrowSculptor object.</returns>
        protected override Sculptor CreateSculptor()
        {
            return new DartSculptor(Length, 0.8f);
        }
    }
}
