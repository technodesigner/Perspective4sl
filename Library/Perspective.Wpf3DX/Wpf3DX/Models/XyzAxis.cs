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
using Perspective.Wpf3DX.Textures;

namespace Perspective.Wpf3DX.Models
{
    /// <summary>
    /// A class that represents a 3D XYZ axis
    /// Default length of each axis is 1.0.
    /// Default radius of the arrows body is 0.025.
    /// Default radius of the arrows head is 0.05.
    /// </summary> 
    public class XyzAxis : Sculpture
    {
        /// <summary>
        /// Initializes a new instance of XyzAxis class.
        /// </summary>
        public XyzAxis()
        {
            Length = 1.0f;
            Radius = 0.025f;
            Signed = false;
            CoordinateSystemKind = CoordinateSystemKind.RightHanded;
            Texture = new ColorTexture(0.75f, 0.75f, 0.75f, 1.0f);
        }

        /// <summary>
        /// Gets or sets the axis length.
        /// Default value is 1.0.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float Length { get; set; }

        /// <summary>
        /// Gets or sets the axis body's radius.
        /// Default radius of the arrows body is 0.025.
        /// Default radius of the arrows head is 0.05.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float Radius { get; set; }

        /// <summary>
        /// Gets or sets the axis signed characteristic.
        /// Default value is false.
        /// </summary>
        public bool Signed { get; set; }

        /// <summary>
        /// Gets or sets the kind of coordinate system.
        /// The default value is CoordinateSystemKind.RightHanded.
        /// </summary>
        public CoordinateSystemKind CoordinateSystemKind { get; set; }
    
        /// <summary>
        /// Initializes a new specific Sculptor instance.
        /// </summary>
        /// <returns>A XyzAxisSculptor object.</returns>
        protected override Sculptor CreateSculptor()
        {
            return new XyzAxisSculptor(Length, Radius, Signed, CoordinateSystemKind);
        }
    }
}