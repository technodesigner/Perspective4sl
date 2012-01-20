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
using System.Collections.ObjectModel;
using System;

namespace Perspective.Wpf3DX.Sculptors
{
    /// <summary>
    /// A class to handle points and triangles of a polygonal model.
    /// Default radius is 1.0.
    /// </summary>
    public class PolygonSculptor : Sculptor
    {
        private Collection<Vector3> _notRoundedPoints = new Collection<Vector3>();

        /// <summary>
        /// Initializes a new instance of PolygonSculptor.
        /// </summary>
        public PolygonSculptor()
            : base()
        {
        }

        /// <summary>
        /// Initializes an existing instance of PolygonSculptor.
        /// Vertices are automatically generated.
        /// </summary>
        /// <param name="circumferenceSideCount">Side count</param>
        /// <param name="initialAngle">Angle between the axis [origin - first point] and the X-axis, in degrees</param>
        public void Initialize(int circumferenceSideCount, float initialAngle)
        {
            _initialAngle = initialAngle;
            _circumferenceSideCount = circumferenceSideCount;
            CreatePoints();
        }

        /// <summary>
        /// Initializes an existing instance of PolygonSculptor.
        /// Vertices are automatically generated.
        /// </summary>
        /// <param name="circumferenceSideCount">Side count</param>
        /// <param name="initialAngle">Angle between the axis [origin - first point] and the X-axis, in degrees</param>
        /// <param name="roundingRate">Angle rounding rate. The value must be comprized between 0.0 and 0.5.</param>
        public void Initialize(int circumferenceSideCount, float initialAngle, float roundingRate)
        {
            _initialAngle = initialAngle;
            _roundingRate = roundingRate;
            _circumferenceSideCount = circumferenceSideCount;
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
                CreatePoints();
            }
            if ((Triangles.Count == 0) &&
                (Points.Count > 0))
            {
                BuildTriangles();
            }
        }

        private Vector3 _center;

        /// <summary>
        /// The polygon's center point.
        /// </summary>
        public Vector3 Center
        {
            get { return _center; }
            set { _center = value; }
        }

        private float _initialAngle;

        /// <summary>
        /// Initial angle (first point), in degrees.
        /// </summary>
        public float InitialAngle
        {
            get { return _initialAngle; }
            set { _initialAngle = value; }
        }

        private bool _centered;

        /// <summary>
        /// Indicates if the sculptor has a center point.
        /// </summary>
        protected bool Centered
        {
            get { return _centered; }
            set { _centered = value; }
        }

        private float _roundingRate;

        /// <summary>
        /// Gets or sets the angle rounding rate.
        /// The value must be comprized between 0.0 and 0.5.
        /// </summary>
        public float RoundingRate
        {
            get { return _roundingRate; }
            set
            {
                if ((value < 0.0) || (value > 0.5))
                {
                    throw new ArgumentOutOfRangeException("RoundingRate");
                }
                else
                {
                    _roundingRate = value;
                }
            }
        }

        private int _circumferenceSideCount;

        /// <summary>
        /// Gets or sets the side count of the circumference.
        /// </summary>
        protected int CircumferenceSideCount
        {
            get { return _circumferenceSideCount; }
            set { _circumferenceSideCount = value; }
        }

        /// <summary>
        /// Create the points of the polygon.
        /// </summary>
        protected virtual void CreatePoints()
        {
            if (_circumferenceSideCount < 3)
            {
                throw new ArgumentException("_circumferenceSideCount < 3");
            }
            this.Points.Clear();
            _center = new Vector3(0, 0, 0);
            _centered = true;
            double angle1 = 2 * Math.PI / _circumferenceSideCount;
            double angle = 0.0;
            for (int i = 1; i <= _circumferenceSideCount; i++)
            {
                // angle = (angle1 * i);
                angle = (angle1 * i) + MathHelper.ToRadians(_initialAngle);
                Vector3 p = new Vector3();
                p.X = (float)Math.Cos(angle);
                p.Y = (float)Math.Sin(angle);
                p.Z = 0;
                this.Points.Add(p);
            }
        }

        /// <summary>
        /// Reinitialize not rounded points list, if necessary
        /// (i.e. after a Points initialization).
        /// </summary>
        /// <seealso cref="RoundCorner"/>
        public void InitializeNotRoundedPointList()
        {
            Helper3D.CloneVectors(Points, _notRoundedPoints);
        }

        private bool _rounded;

        /// <summary>
        /// Round the polygon's corners.
        /// </summary>
        internal void RoundCorner()
        {
            if (_roundingRate > 0.0)
            {

                if (!_rounded)
                {
                    // Must be executed only once
                    InitializeNotRoundedPointList();
                }

                Collection<Vector3> points = new Collection<Vector3>();
                for (int i = 0; i <= _notRoundedPoints.Count - 1; i++)
                {
                    Vector3 pA = new Vector3();
                    Vector3 pB = new Vector3();
                    Vector3 pC = new Vector3();

                    pB = _notRoundedPoints[i];
                    if (i >= 1)
                    {
                        pA = _notRoundedPoints[i - 1];
                    }
                    else if (i == 0)
                    {
                        pA = _notRoundedPoints[_notRoundedPoints.Count - 1];
                    }
                    if (i < _notRoundedPoints.Count - 1)
                    {
                        pC = _notRoundedPoints[i + 1];
                    }
                    else if (i == _notRoundedPoints.Count - 1)
                    {
                        pC = _notRoundedPoints[0];
                    }

                    Collection<Vector3> pl = Helper3D.RoundCorner(pA, pB, pC, _roundingRate);
                    Helper3D.CopyVectors(pl, points);
                }
                Helper3D.CloneVectors(points, Points);
                _rounded = true;
            }
        }

        /// <summary>
        /// Build the triangles.
        /// </summary>
        public void BuildTriangles()
        {
            BuildTriangles(TriangleSideKind.Front);
        }

        /// <summary>
        /// Build the triangles, with a specific side orientation.
        /// </summary>
        /// <remarks>The cull mode is supposed to be clockwise (front faces are counter-clockwise).</remarks>
        /// <param name="tsk">TriangleSideKind value (triangle orientation).</param>
        public virtual void BuildTriangles(TriangleSideKind tsk)
        {
            RoundCorner();
            this.Triangles.Clear();
            if (Points.Count < 3)
            {
                throw new ArgumentException("Points.Count < 3");
            }
            if (!_centered)
            {
                CreateSideTriangles(Points, tsk);
            }
            else
            {
                // center based triangles
                for (int i = 0; i <= Points.Count - 1; i++)
                {
                    if (tsk == TriangleSideKind.Front)
                    {
                        if (i < Points.Count - 1)
                        {
                            this.Triangles.Add(new Vector3Triplet(_center, Points[i], Points[i + 1]));
                        }
                        else
                        {
                            this.Triangles.Add(new Vector3Triplet(_center, Points[i], Points[0]));
                        }
                    }
                    else
                    {
                        if (i < Points.Count - 1)
                        {
                            this.Triangles.Add(new Vector3Triplet(_center, Points[i + 1], Points[i]));
                        }
                        else
                        {
                            this.Triangles.Add(new Vector3Triplet(_center, Points[0], Points[i]));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// A method for building the texture coordinates of the mesh.
        /// </summary>
        /// <remarks>The code differs from Perspective for WPF in that values are not automatically normalized.</remarks>
        protected override void MapTexture()
        {
            int vertexIndex = 0;
            foreach (Vector3Triplet pt in this.Triangles)
            {
                foreach (Vector3 p in pt.Points)
                {
                    VertexPositionTextureNormals[vertexIndex++].TextureCoordinates = new Vector2((p.X + 1) / 2, -(p.Y + 1) / 2);
                }
            }
        }
    }
}
