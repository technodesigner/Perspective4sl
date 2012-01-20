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
    /// A class to handle points and triangles of a spherical model.
    /// Default radius is 1.0.
    /// </summary>
    public class SphericalSculptor : Sculptor
    {
        /// <summary>
        /// Initializes a new instance of SphericalSculptor.
        /// </summary>
        public SphericalSculptor()
        {
        }

        /// <summary>
        /// Initializes a new instance of SphericalSculptor.
        /// </summary>
        /// <param name="parallelCount">Parallel Count.</param>
        public SphericalSculptor(int parallelCount)
        {
            Initialize(parallelCount);
        }

        /// <summary>
        /// Initializes an existing instance of SphericalSculptor.
        /// </summary>
        /// <param name="parallelCount">Parallel Count.</param>
        public void Initialize(int parallelCount)
        {
            if (parallelCount < 2)
            {
                throw new ArgumentException("parallelCount < 2");
            }
            _parallelCount = parallelCount;
            CreatePoints();
        }
        /// <summary>
        /// Initializes the Points and Triangles collections.
        /// Called By Sculptor.BuildMesh()
        /// </summary>
        protected override void CreateTriangles()
        {
            if (Points.Count == 0)
            {
                CreatePoints(this.ParallelCount);
            }
            if ((Triangles.Count == 0) && (Points.Count > 0))
            {
                BuildTriangles();
            }
        }

        internal const int DefaultParallelCount = 20;
        private int _parallelCount = DefaultParallelCount;

        /// <summary>
        /// Gets or sets the side count for half of the circumference.
        /// </summary>
        public int ParallelCount
        {
            get { return _parallelCount; }
            set { _parallelCount = value; }
        }

        /// <summary>
        /// Creates the structure points.
        /// </summary>
        /// <param name="parallelCount">Side count for half of the circumference.</param>
        public void CreatePoints(int parallelCount)
        {
            _parallelCount = parallelCount;
            CreatePoints();
        }

        /// <summary>
        /// Creates the points.
        /// Points are added by vertical rank from top to bottom (on Z-Y first)
        /// and then counter-clockwise.
        /// </summary>
        public void CreatePoints()
        {
            this.Points.Clear();

            // theta : vertical slices
            // phi : horizontal slices

            double thetaStep = MathHelper.Pi / (_parallelCount + 1);
            double phiStep = MathHelper.Pi / (_parallelCount + 1);
            double theta = 0.0;
            double phi = 0.0;

            for (int i = 0; i <= ((_parallelCount + 1) * 2); i++)
            {
                theta = (thetaStep * i);
                for (int j = 0; j <= _parallelCount + 1; j++)
                {
                    phi = phiStep * j;
                    Vector3 p = new Vector3();
                    p.X = (float)(Math.Sin(theta) * Math.Sin(phi));
                    p.Y = (float)Math.Cos(phi);
                    p.Z = (float)(Math.Cos(theta) * Math.Sin(phi));
                    this.Points.Add(p);
                }
            }
        }

        /// <summary>
        /// Builds the structure triangles.
        /// 2 triangles are built on each side, except at the top and at the bottom (only 1 trinagle by side).
        /// Triangles are added by vertical rank from top to bottom (on Z-Y first)
        /// and then counter-clockwise.
        /// On each side, the upperleft triangle is built before the bottomright one.
        /// </summary>
        /// <remarks>The cull mode is supposed to be clockwise (front faces are counter-clockwise).</remarks>
        public void BuildTriangles()
        {
            if (Points.Count < 2)
            {
                throw new ArgumentException("Points.Count < 2");
            }
            this.Triangles.Clear();
            for (int i = 0; i <= Points.Count - 1; i++)
            {
                // vRank : vertical point rank (counter-clockwise)
                int vRank = i / (_parallelCount + 2);

                // index : index of current point in the vertical rank
                int index = i % (_parallelCount + 2);
                if (vRank >= 1)
                {
                    // Upperleft triangle
                    // (the only one at the bottom of the rank)
                    if ((index > 0) && (index < _parallelCount + 1))
                    {
                        this.Triangles.Add(new Vector3Triplet(
                            Points[i],
                            Points[i - _parallelCount - 2],
                            Points[i - _parallelCount - 1]));
                    }
                    // Bottomright triangle
                    // (the only one at the top of the rank)
                    if (index < _parallelCount)
                    {
                        this.Triangles.Add(new Vector3Triplet(
                            Points[i],
                            Points[i - _parallelCount - 1],
                            Points[i + 1]));
                    }
                }
            }
        }

        /// <summary>
        /// A method for building the texture coordinates of the mesh.
        /// </summary>
        protected override void MapTexture()
        {
            int triangleIndex = 0;
            float vSideCount = _parallelCount + 1;
            float hSideCount = (_parallelCount + 1) * 2;
            for (int i = 0; i <= Points.Count - 1; i++)
            {
                // vRank : vertical point rank (counter-clockwise)
                int vRank = i / (_parallelCount + 2);

                // index : index of current point in the vertical rank
                int index = i % (_parallelCount + 2);
                if (vRank >= 1)
                {
                    float minX = vRank / hSideCount;
                    float maxX = (vRank + 1) / hSideCount;
                    float minY = index / vSideCount;
                    float maxY = (index + 1) / vSideCount;

                    // Upperleft triangle
                    // (the only one at the bottom of the rank)
                    if ((index > 0) && (index < _parallelCount + 1))
                    {
                        int firstVertexIndex = triangleIndex * 3;
                        VertexPositionTextureNormals[firstVertexIndex++].TextureCoordinates = new Vector2(maxX, minY);
                        VertexPositionTextureNormals[firstVertexIndex++].TextureCoordinates = new Vector2(minX, minY);
                        VertexPositionTextureNormals[firstVertexIndex].TextureCoordinates = new Vector2(minX, maxY);
                        triangleIndex++;
                    }
                    // Bottomright triangle
                    // (the only one at the top of the rank)
                    if (index < _parallelCount)
                    {
                        int firstVertexIndex = triangleIndex * 3;
                        VertexPositionTextureNormals[firstVertexIndex++].TextureCoordinates = new Vector2(maxX, minY);
                        VertexPositionTextureNormals[firstVertexIndex++].TextureCoordinates = new Vector2(minX, maxY);
                        VertexPositionTextureNormals[firstVertexIndex].TextureCoordinates = new Vector2(maxX, maxY);
                        triangleIndex++;
                    }
                }
            }
        }
    }
}
