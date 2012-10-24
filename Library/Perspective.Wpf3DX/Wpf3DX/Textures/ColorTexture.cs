using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel;

namespace Perspective.Wpf3DX.Textures
{
    /// <summary>
    /// Creates a texture from a color.
    /// </summary>
    public class ColorTexture : ModelTexture
    {
        /// <summary>
        /// Initializes a new instance of the ColorTexture class.
        /// </summary>
        public ColorTexture()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ColorTexture class.
        /// </summary>
        /// <param name="color">A color structure.</param>
        public ColorTexture(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        /// <summary>
        /// Initializes a new instance of the ColorTexture class.
        /// </summary>
        /// <param name="r">The red component of the color, between 0 and 1.0.</param>
        /// <param name="g">The green component of the color, between 0 and 1.0.</param>
        /// <param name="b">The blue component of the color, between 0 and 1.0.</param>
        /// <param name="a">The alpha component of the color, between 0 and 1.0.</param>
        public ColorTexture(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Gets or sets the red component of the color, between 0 and 1.0.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float R { get; set; }

        /// <summary>
        /// Gets or sets the green component of the color, between 0 and 1.0.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float G { get; set; }

        /// <summary>
        /// Gets or sets the blue component of the color, between 0 and 1.0.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float B { get; set; }

        /// <summary>
        /// Gets or sets the alpha component of the color, between 0 and 1.0.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float A { get; set; }

        /// <summary>
        /// Gets the Texture2D object.
        /// </summary>
        /// <returns>A Texture2D object.</returns>
        public override Texture2D GetTexture2D()
        {
            return Helper3D.CreateTexture2D(new Color(R, G, B, A));
        }
    }
}
