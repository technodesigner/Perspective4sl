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

using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework.Graphics;

namespace Perspective.Wpf3DX.Textures
{
    /// <summary>
    /// Creates a texture from a bitmap.
    /// </summary>
    public class BitmapTexture : ModelTexture
    {
        /// <summary>
        /// Initializes a new instance of the BitmapTexture class.
        /// </summary>
        public BitmapTexture()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BitmapTexture class.
        /// </summary>
        /// <param name="bitmapSource">A BitmapSource object.</param>
        public BitmapTexture(BitmapSource bitmapSource)
        {
            // BitmapSource = bitmapSource;
            _bitmapSource = bitmapSource;
        }

        private BitmapSource _bitmapSource;

        //No exposed property because no XAML loading compatibility today
        ///// <summary>
        ///// Gets or sets the bitmap source.
        ///// </summary>
        //public BitmapSource BitmapSource { get; set; }

        /// <summary>
        /// Gets the Texture2D object.
        /// </summary>
        /// <returns>A Texture2D object.</returns>
        public override Texture2D GetTexture2D()
        {
            // return Helper3D.CreateTexture2D(BitmapSource);
            return Helper3D.CreateTexture2D(_bitmapSource);
        }
    }
}
