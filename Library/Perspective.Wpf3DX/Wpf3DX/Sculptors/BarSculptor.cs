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
using Microsoft.Xna.Framework;

namespace Perspective.Wpf3DX.Sculptors
{
    /// <summary>
    /// A class to handle points and triangles of a 3D bar model.
    /// Default radius is 1.0.
    /// By default, the direction of the bar is the Z axis, and the length is 1.0.
    /// </summary>
    public class BarSculptor : Sculptor
    {
        private int _circumferenceSideCount;
        private float _initialAngle;
        private float _roundingRate;

        /// <summary>
        /// Initializes a new instance of BarSculptor.
        /// </summary>
        public BarSculptor()
        {
        }

        /// <summary>
        /// Initializes a new instance of BarSculptor.
        /// </summary>
        /// <param name="circumferenceSideCount">Model circumference side count.</param>
        /// <param name="initialAngle">Angle between the axis [origin - first point] and the X-axis, in degrees.</param>
        /// <param name="roundingRate">Angle rounding rate. The value must be comprized between 0.0 and 0.5.</param>
        public BarSculptor(int circumferenceSideCount, float initialAngle, float roundingRate)
        {
            Initialize(circumferenceSideCount, initialAngle, roundingRate);
        }

        /// <summary>
        /// Initializes an existing instance of BarSculptor.
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
        /// <remarks>The cull mode is supposed to be clockwise (front faces are counter-clockwise).</remarks>
        protected override void CreateTriangles()
        {
            _ps1 = new PolygonSculptor();
            _ps1.Initialize(_circumferenceSideCount, _initialAngle);
            _ps1.RoundingRate = _roundingRate;
            _ps1.BuildTriangles(TriangleSideKind.Back);
            foreach (Vector3 p in _ps1.Points)
            {
                this.Points.Add(p);
            }
            foreach (Vector3Triplet tpl in _ps1.Triangles)
            {
                this.Triangles.Add(tpl);
            }

            _ps2 = new PolygonSculptor();
            _ps2.Initialize(_circumferenceSideCount, _initialAngle);
            _ps2.Center = new Vector3(
                _ps2.Center.X,
                _ps2.Center.Y,
                _ps2.Center.Z + 1.0f);
            for (int i = 0; i < _ps2.Points.Count; i++)
            {
                _ps2.Points[i] = new Vector3(
                    _ps2.Points[i].X,
                    _ps2.Points[i].Y,
                    _ps2.Points[i].Z + 1.0f);
                this.Points.Add(_ps2.Points[i]);
            }
            _ps2.RoundingRate = _roundingRate;
            _ps2.BuildTriangles(TriangleSideKind.Front);

            foreach (Vector3Triplet tpl in _ps2.Triangles)
            {
                this.Triangles.Add(tpl);
            }

            for (int i = 0; i < _ps1.Points.Count; i++)
            {
                if (i < _ps1.Points.Count - 1)
                {
                    this.Triangles.Add(new Vector3Triplet(_ps1.Points[i], _ps2.Points[i + 1], _ps2.Points[i]));
                    this.Triangles.Add(new Vector3Triplet(_ps1.Points[i], _ps1.Points[i + 1], _ps2.Points[i + 1]));

                }
                else
                {
                    this.Triangles.Add(new Vector3Triplet(_ps1.Points[i], _ps2.Points[0], _ps2.Points[i]));
                    this.Triangles.Add(new Vector3Triplet(_ps1.Points[i], _ps1.Points[0], _ps2.Points[0]));
                }
            }
        }

        /// <summary>
        /// A method for building the texture coordinates of the mesh.
        /// </summary>
        /// <remarks>The code differs from Perspective for WPF in that values are not automatically normalized.</remarks>
        protected override void MapTexture()
        {
            float minY = 0.0f;
            float maxY = 1.0f;
            float minX = 0.0f;
            float maxX;
            float xPos = 0.0f;
            // float segmentLength = 1.0f;
            float segmentLength = 1.0f / _circumferenceSideCount;

            int vertexIndex = 0;
            foreach (Vector3Triplet pt in _ps1.Triangles)
            {
                foreach (Vector3 p in pt.Points)
                {
                    VertexPositionTextureNormals[vertexIndex++].TextureCoordinates = new Vector2(0.0f, 0.0f);
                }
            }

            foreach (Vector3Triplet pt in _ps2.Triangles)
            {
                foreach (Vector3 p in pt.Points)
                {
                    VertexPositionTextureNormals[vertexIndex++].TextureCoordinates = new Vector2(0.0f, 0.0f);
                }
            }

            float totalLength = 0.0f;
            if (_roundingRate != 0.0)
            {
                for (int i = 0; i <= _ps1.Points.Count - 1; i++)
                {
                    Vector3 p2;
                    if (i == _ps1.Points.Count - 1)
                    {
                        p2 = _ps1.Points[0];
                    }
                    else
                    {
                        p2 = _ps1.Points[i + 1];
                    }
                    Vector3 v = p2 - _ps1.Points[i];
                    totalLength += v.Length();
                }
            }

            for (int i = 0; i <= _ps1.Points.Count - 1; i++)
            {
                if (_roundingRate != 0.0)
                {
                    Vector3 p2;
                    if (i == _ps1.Points.Count - 1)
                    {
                        p2 = _ps1.Points[0];
                    }
                    else
                    {
                        p2 = _ps1.Points[i + 1];
                    }
                    Vector3 v = p2 - _ps1.Points[i];
                    segmentLength = v.Length() / totalLength;
                }
                minX = xPos;
                xPos += segmentLength;
                maxX = xPos;

                VertexPositionTextureNormals[vertexIndex++].TextureCoordinates = new Vector2(minX, maxY);
                VertexPositionTextureNormals[vertexIndex++].TextureCoordinates = new Vector2(maxX, minY);
                VertexPositionTextureNormals[vertexIndex++].TextureCoordinates = new Vector2(minX, minY);
                
                VertexPositionTextureNormals[vertexIndex++].TextureCoordinates = new Vector2(minX, maxY);
                VertexPositionTextureNormals[vertexIndex++].TextureCoordinates = new Vector2(maxX, maxY);
                VertexPositionTextureNormals[vertexIndex++].TextureCoordinates = new Vector2(maxX, minY);
            }
        }

    }
}
