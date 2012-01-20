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

using Microsoft.Xna.Framework.Graphics;

namespace Perspective.Wpf3DX.Textures
{
    /// <summary>
    /// Creates a texture.
    /// </summary>
    public abstract class ModelTexture
    {
        /// <summary>
        /// Gets the Texture2D object.
        /// </summary>
        /// <returns>A Texture2D object.</returns>
        public abstract Texture2D GetTexture2D();
    }
}
