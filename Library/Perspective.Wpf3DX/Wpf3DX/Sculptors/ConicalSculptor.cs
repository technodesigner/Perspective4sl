﻿//------------------------------------------------------------------
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
using Microsoft.Xna.Framework;

namespace Perspective.Wpf3DX.Sculptors
{
    /// <summary>
    /// A class to handle points and triangles of a 3D conical model.
    /// Default radius is 1.0.
    /// By default, the direction of the cone is the Z axis, and the length is 1.0.
    /// </summary>
    public class ConicalSculptor : Sculptor
    {
        private int _circumferenceSideCount;
        private float _initialAngle;
        private float _roundingRate;

        /// <summary>
        /// Initializes a new instance of ConicalSculptor.
        /// </summary>
        public ConicalSculptor()
        {
        }

        /// <summary>
        /// Initializes a new instance of ConicalSculptor.
        /// </summary>
        /// <param name="circumferenceSideCount">Model circumference side count.</param>
        /// <param name="initialAngle">Angle between the axis [origin - first point] and the X-axis, in degrees.</param>
        /// <param name="roundingRate">Angle rounding rate. The value must be comprized between 0.0 and 0.5.</param>
        public ConicalSculptor(int circumferenceSideCount, float initialAngle, float roundingRate)
        {
            Initialize(circumferenceSideCount, initialAngle, roundingRate);
        }

        /// <summary>
        /// Initializes an existing instance of ConicalSculptor.
        /// </summary>
        /// <param name="circumferenceSideCount">Model circumference side count.</param>
        /// <param name="initialAngle">Angle between the axis [origin - first point] and the X-axis, in degrees.</param>
        /// <param name="roundingRate">Angle rounding rate. The value must be comprized between 0.0 and 0.5.</param>
        public void Initialize(int circumferenceSideCount, float initialAngle, float roundingRate)
        {
            _circumferenceSideCount = circumferenceSideCount;
            _initialAngle = initialAngle;
            _roundingRate = roundingRate;
        }

        private PolygonSculptor _ps1;
        private PolygonSculptor _ps2;

        /// <summary>
        /// Initializes the Points and Triangles collections.
        /// Called By Sculptor.BuildMesh()
        /// </summary>
        protected override void CreateTriangles()
        {
            _ps1 = new PolygonSculptor();
            _ps1.Initialize(_circumferenceSideCount, _initialAngle);
            _ps1.RoundingRate = _roundingRate;
            _ps1.BuildTriangles(TriangleSideKind.Back);
            foreach (Vector3Triplet tpl in _ps1.Triangles)
            {
                this.Triangles.Add(tpl);
            }
            foreach (Vector3 p in _ps1.Points)
            {
                this.Points.Add(p);
            }

            _ps2 = new PolygonSculptor();
            _ps2.Initialize(_circumferenceSideCount, _initialAngle);
            _ps2.RoundingRate = _roundingRate;
            _ps2.Center = new Vector3(
                _ps2.Center.X,
                _ps2.Center.Y,
                _ps2.Center.Z + 1.0f);
            this.Points.Add(_ps2.Center);
            _ps2.BuildTriangles(TriangleSideKind.Front);
            foreach (Vector3Triplet tpl in _ps2.Triangles)
            {
                this.Triangles.Add(tpl);
            }
        }

        /// <summary>
        /// A method for building the texture coordinates of the mesh.
        /// </summary>
        /// <remarks>The code differs from Perspective for WPF in that values are not automatically normalized.</remarks>
        protected override void MapTexture()

        {
            int vertexIndex = 0;
            foreach (Vector3Triplet pt in _ps1.Triangles)
            {
                foreach (Vector3 p in pt.Points)
                {
                    //Mesh.TextureCoordinates.Add(
                    //    new Point(p.X, -p.Y));
                    VertexPositionTextureNormals[vertexIndex++].TextureCoordinates = new Vector2((p.X + 1) / 2, -(p.Y + 1) / 2);
                }
            }

            foreach (Vector3Triplet pt in _ps2.Triangles)
            {
                foreach (Vector3 p in pt.Points)
                {
                    //Mesh.TextureCoordinates.Add(
                    //    new Point(p.X, -p.Y));
                    VertexPositionTextureNormals[vertexIndex++].TextureCoordinates = new Vector2((p.X + 1) / 2, -(p.Y + 1) / 2);
                }
            }
        }
    }
}
