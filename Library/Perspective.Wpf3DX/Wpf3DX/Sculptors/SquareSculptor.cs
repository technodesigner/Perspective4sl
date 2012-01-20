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

namespace Perspective.Wpf3DX.Sculptors
{
    /// <summary>
    /// A class to handle points and triangles of a square model.
    /// Default size of a side is 1.0.
    /// </summary>
    public class SquareSculptor : Sculptor
    {
        Vector3 _p000 = new Vector3(0, 0, 0);
        Vector3 _p100 = new Vector3(1, 0, 0);
        Vector3 _p110 = new Vector3(1, 1, 0);
        Vector3 _p010 = new Vector3(0, 1, 0);

        /// <summary>
        /// Initializes a new instance of SquareSculptor.
        /// </summary>
        public SquareSculptor()
            : base()
        {
            this.Points.Add(_p000);
            this.Points.Add(_p100);
            this.Points.Add(_p110);
            this.Points.Add(_p010);
        }

        /// <summary>
        /// Initializes the Points and Triangles collections.
        /// Called By Sculptor.BuildMesh()
        /// </summary>
        protected override void CreateTriangles()
        {
            this.Triangles.Clear();
            CreateSideTriangles(Points, TriangleSideKind.Front);
        }

        /// <summary>
        /// A method for building the texture coordinates of the mesh.
        /// </summary>
        protected override void MapTexture()
        {
            Helper3D.MapSquareTexture(VertexPositionTextureNormals, 0, 0.0f, 0.0f, 1.0f, 1.0f);
        }
    }
}
