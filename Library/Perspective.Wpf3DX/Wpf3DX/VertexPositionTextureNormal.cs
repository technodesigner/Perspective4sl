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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perspective.Wpf3DX
{
    /// <summary>
    /// A structure th handle data associated with a vertex.
    /// </summary>
    public struct VertexPositionTextureNormal
    {
        /// <summary>
        /// Gets or sets the position of the vertex.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// Gets or sets the texture coordinates of the vertex.
        /// </summary>
        public Vector2 TextureCoordinates;
        
        /// <summary>
        /// Gets or sets the normal vector of a vertex.
        /// </summary>
        public Vector3 Normal;

        /// <summary>
        /// Initializes a new instance of the VertexPositionTextureNormal structure.
        /// </summary>
        /// <param name="position">The position of the vertex.</param>
        /// <param name="normal">The normal vector of the vertex.</param>
        public VertexPositionTextureNormal(Vector3 position, Vector3 normal)
        {
            Position = position;
            Normal = normal;
            TextureCoordinates = Vector2.Zero;
        }

        /// <summary>
        /// Gets the vertex declaration.
        /// </summary>
        public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
            new VertexElement(sizeof(float) * 5, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
            );
    }
}
