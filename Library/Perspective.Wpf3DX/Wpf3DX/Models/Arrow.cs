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
    /// </summary>
    public class Arrow : Sculpture
    {
        public Arrow()
        {
            Length = ArrowSculptor.DefaultLength;
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
            return new ArrowSculptor(Length);
        }
    }
}
