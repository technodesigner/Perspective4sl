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

using Perspective.Wpf3DX.Transforms;
using System;
namespace Perspective.Wpf3DX.Sculptors
{
    /// <summary>
    /// A class to handle points and triangles of an XyzAxis model.
    /// </summary>
    public class XyzAxisSculptor : Sculptor
    {
        internal const float DefaultLength = 1.0f;
        internal const float DefaultRadius = 0.025f;
        private enum Sign { Positive, Negative };
        private float _length = DefaultLength;
        private float _radius = DefaultRadius;
        private bool _signed;
        private CoordinateSystemKind _coordinateSystemKind;

        /// <summary>
        /// Initializes a new instance of XyzAxisSculptor.
        /// </summary>
        /// <param name="length">Length of each axis.</param>
        /// <param name="radius">Radius of each axis.</param>
        /// <param name="signed">Axis signed characteristic.</param>
        /// <param name="coordinateSystemKind">The kind of coordinate system.</param>
        public XyzAxisSculptor(
            float length,
            float radius,
            bool signed,
            CoordinateSystemKind coordinateSystemKind)
        {
            Initialize(length, radius, signed, coordinateSystemKind);
        }

        /// <summary>
        /// Initializes an existing instance of XyzAxisSculptor.
        /// </summary>
        /// <param name="length">Length of each axis.</param>
        /// <param name="radius">Radius of each axis.</param>
        /// <param name="signed">Axis signed characteristic.</param>
        /// <param name="coordinateSystemKind">The kind of coordinate system.</param>
        public void Initialize(
            float length,
            float radius,
            bool signed,
            CoordinateSystemKind coordinateSystemKind)
        {
            _length = length;
            _radius = radius;
            _signed = signed;
            _coordinateSystemKind = coordinateSystemKind;
        }

        /// <summary>
        /// Initializes the Points and Triangles collections.
        /// Called By Sculptor.BuildMesh()
        /// </summary>
        protected override void CreateTriangles()
        {
            SphericalSculptor ssOrigin = new SphericalSculptor();
            ssOrigin.BuildMesh();
            float sphereScaleFactor = _radius;
            ssOrigin.Transform(new Scaling(sphereScaleFactor, sphereScaleFactor, sphereScaleFactor));
            CopyFrom(ssOrigin);

            CreateArrows(Sign.Positive);
            if (_signed)
            {
                CreateArrows(Sign.Negative);
            }
        }

        /// <summary>
        /// Creates the 3 arrows objects
        /// </summary>
        /// <param name="s">Sign enum value.</param>
        private void CreateArrows(Sign s)
        {
            // float arrowScaleFactor = _radius * 10.0 / 2.0;
            float arrowScaleFactor = _radius * 10.0f;
            Scaling arrowScaling = new Scaling(arrowScaleFactor, arrowScaleFactor, 1.0f);

            ArrowSculptor asZ = new ArrowSculptor();
            asZ.Initialize(_length);
            asZ.BuildMesh();
            ModelTransformGroup tgZ = new ModelTransformGroup();
            tgZ.Children.Add(arrowScaling);
            if (((s == Sign.Negative) && (_coordinateSystemKind == CoordinateSystemKind.RightHanded))
                || ((s == Sign.Positive) && (_coordinateSystemKind == CoordinateSystemKind.LeftHanded)))
            {
                tgZ.Children.Add(new Rotation(180.0f, AxisDirection.Y));
            }
            asZ.Transform(tgZ);
            CopyFrom(asZ);

            ArrowSculptor asX = new ArrowSculptor();
            asX.Initialize(_length);
            asX.BuildMesh();
            ModelTransformGroup tgX = new ModelTransformGroup();
            tgX.Children.Add(arrowScaling);
            tgX.Children.Add(new Rotation(s == Sign.Positive ? 90.0f : -90.0f, AxisDirection.Y));
            asX.Transform(tgX);
            CopyFrom(asX);

            ArrowSculptor asY = new ArrowSculptor();
            asY.Initialize(_length);
            asY.BuildMesh();
            ModelTransformGroup tgY = new ModelTransformGroup();
            tgY.Children.Add(arrowScaling);
            tgY.Children.Add(new Rotation(s == Sign.Positive ? -90.0f : 90.0f, AxisDirection.X));
            asY.Transform(tgY);
            CopyFrom(asY);

            if (_length > 1.0)
            {
                float sepRadius = _radius * 2.0f;
                int sep = Convert.ToInt32(_length) - 1;
                BarSculptor[][] bars = new BarSculptor[sep][];
                Rotation[] r = new Rotation[2];
                r[0] = new Rotation(s == Sign.Positive ? 90.0f : -90.0f, AxisDirection.Y);
                r[1] = new Rotation(s == Sign.Positive ? -90.0f : 90.0f, AxisDirection.X);
                for (int i = 0; i < sep; i++)
                {
                    int offset = (s == Sign.Positive ? i + 1 : -i - 1);
                    int offsetZ = (
                        (((s == Sign.Positive) && (_coordinateSystemKind == CoordinateSystemKind.RightHanded))
                            || ((s == Sign.Negative) && (_coordinateSystemKind == CoordinateSystemKind.LeftHanded))) ?
                            i + 1 : -i - 1);
                    bars[i] = new BarSculptor[3];
                    for (int j = 0; j < 3; j++)
                    {
                        bars[i][j] = new BarSculptor();
                        bars[i][j].Initialize(40, 0.0f, 0.0f);
                        bars[i][j].BuildMesh();
                        ModelTransformGroup tg = new ModelTransformGroup();
                        tg.Children.Add(new Scaling(sepRadius, sepRadius, 0.01f));
                        if (j != 2)
                        {
                            tg.Children.Add(r[j]);
                        }
                        tg.Children.Add(new Translation(
                            j == 0 ? offset : 0.0f,
                            j == 1 ? offset : 0.0f,
                            j == 2 ? offsetZ : 0.0f));
                        bars[i][j].Transform(tg);
                        CopyFrom(bars[i][j]);
                    }
                }
            }
        }
    }
}
