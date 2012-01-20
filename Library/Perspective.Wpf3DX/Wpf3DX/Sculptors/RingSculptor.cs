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
using System;

namespace Perspective.Wpf3DX.Sculptors
{
    /// <summary>
    /// A class to handle points and triangles of a 3D bar model.
    /// Default radius is 10.0.
    /// </summary>
    public class RingSculptor : Sculptor
    {
        private int _circumferenceSideCount;
        private float _initialAngle;
        private float _roundingRate;
        private float _radius;
        private int _segmentCount;

        /// <summary>
        /// Initializes a new instance of RingSculptor.
        /// </summary>
        public RingSculptor()
        {
        }

        /// <summary>
        /// Initializes a new instance of RingSculptor.
        /// </summary>
        /// <param name="radius">Ring radius.</param>
        /// <param name="segmentCount">Ring bar segment count</param>
        /// <param name="circumferenceSideCount">Ring slice circumference side count.</param>
        /// <param name="initialAngle">Angle between the axis [origin - first point] and the X-axis, in degrees. Applies to the ring slice.</param>
        /// <param name="roundingRate">Angle rounding rate. The value must be comprized between 0.0 and 0.5. Applies to the ring slice.</param>
        public RingSculptor(
            float radius,
            int segmentCount,
            int circumferenceSideCount,
            float initialAngle,
            float roundingRate)
        {
            Initialize(
                radius,
                segmentCount,
                circumferenceSideCount,
                initialAngle,
                roundingRate);
        }

        /// <summary>
        /// Initializes an existing instance of RingSculptor.
        /// </summary>
        /// <param name="radius">Ring radius.</param>
        /// <param name="segmentCount">Ring bar segment count</param>
        /// <param name="circumferenceSideCount">Ring slice circumference side count.</param>
        /// <param name="initialAngle">Angle between the axis [origin - first point] and the X-axis, in degrees. Applies to the ring slice.</param>
        /// <param name="roundingRate">Angle rounding rate. The value must be comprized between 0.0 and 0.5. Applies to the ring slice.</param>
        public void Initialize(
            float radius,
            int segmentCount,
            int circumferenceSideCount,
            float initialAngle,
            float roundingRate)
        {
            _radius = radius;
            _segmentCount = segmentCount;
            _circumferenceSideCount = circumferenceSideCount;
            _initialAngle = initialAngle;
            _roundingRate = roundingRate;
        }

        /// <summary>
        /// Initializes the Points and Triangles collections.
        /// Called By Sculptor.BuildMesh()
        /// </summary>
        /// <remarks>The cull mode is supposed to be clockwise (front faces are counter-clockwise).</remarks>
        protected override void CreateTriangles()
        {
            Vector3 radiusVector = new Vector3(_radius, 0, 0);

            PolygonSculptor pv1 = new PolygonSculptor();
            pv1.Initialize(_circumferenceSideCount, _initialAngle);
            pv1.RoundingRate = _roundingRate;
            pv1.RoundCorner();
            int circumferencePointCount = pv1.Points.Count;
            foreach (Vector3 p in pv1.Points)
            {
                Vector3 p1 = new Vector3(p.X, p.Y, p.Z) + radiusVector;
                this.Points.Add(p1);
            }

            float thetaStep = 2 * MathHelper.Pi / _segmentCount;
            float theta = 0.0f;

            for (int i = 1; i <= _segmentCount; i++)
            {
                theta = (thetaStep * i);
                Vector3 thetaVector = new Vector3(
                    (float)Math.Cos(theta),
                    0,
                    (float)Math.Sin(theta));

                for (int i1 = 0; i1 < circumferencePointCount; i1++)
                {
                    Vector3 p2 = Helper3D.RotateVector(Points[i1], theta, AxisDirection.Y);
                    this.Points.Add(p2);
                }

                for (int j = 0; j < circumferencePointCount; j++)
                {
                    int index1Min = (i - 1) * circumferencePointCount;
                    int index1 = index1Min + j;
                    int index2Min = i * circumferencePointCount;
                    int index2 = index2Min + j;
                    int indexLimit = index2Min + circumferencePointCount - 1;

                    if (index2 < indexLimit)
                    {
                        this.Triangles.Add(new Vector3Triplet(Points[index2], Points[index2 + 1], Points[index1]));
                        this.Triangles.Add(new Vector3Triplet(Points[index2 + 1], Points[index1 + 1], Points[index1]));
                    }
                    else
                    {
                        this.Triangles.Add(new Vector3Triplet(Points[index2], Points[index2Min], Points[index1]));
                        this.Triangles.Add(new Vector3Triplet(Points[index2Min], Points[index1Min], Points[index1]));
                    }
                }
            }
        }
    }
}
