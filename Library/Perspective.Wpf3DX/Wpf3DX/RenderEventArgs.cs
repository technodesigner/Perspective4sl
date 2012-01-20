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
using Microsoft.Xna.Framework;
using System;

namespace Perspective.Wpf3DX
{
    /// <summary>
    /// Provides data for Render events.
    /// </summary>
    public class RenderEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the RenderEventArgs class.
        /// </summary>
        /// <param name="graphicsDevice">The graphic device.</param>
        /// <param name="viewProjection">The view projection matrix.</param>
        public RenderEventArgs(GraphicsDevice graphicsDevice, Matrix viewProjection)
        {
            GraphicsDevice = graphicsDevice;
            ViewProjection = viewProjection;
        }

        /// <summary>
        /// Gets or sets the graphic device.
        /// </summary>
        /// <remarks>No getter to avoid getter access in the Model.OnRender method.</remarks>
        public GraphicsDevice GraphicsDevice;
        
        /// <summary>
        /// Gets or sets the view projection matrix.
        /// </summary>
        /// <remarks>No getter to avoid getter access in the Model.OnRender method.</remarks>
        public Matrix ViewProjection;
    }
}
