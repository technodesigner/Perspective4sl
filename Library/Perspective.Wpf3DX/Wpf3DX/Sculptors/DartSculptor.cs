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
using Perspective.Wpf3DX.Transforms;

namespace Perspective.Wpf3DX.Sculptors
{
    /// <summary>
    /// A class to handle points and triangles of a 3D arrow model.
    /// Default radius of the head is 0.2.
    /// Default radius of the body is 0.1.
    /// By default, the direction of the arrow is the Z axis, and the length is 1.0.
    /// Default ratio of the head to the first unit is 0.8 (instead of 0.2 for the WPF equivalent ArrowSculptor).
    /// </summary>
    public class DartSculptor : Sculptor
    {
        internal const float DefaultLength = 1.0f;
        private float _length = DefaultLength;
        private float _headUnitRatio = 0.8f;

        /// <summary>
        /// Initializes a new instance of ArrowSculptor.
        /// </summary>
        public DartSculptor()
        {
        }

        /// <summary>
        /// Initializes a new instance of ArrowSculptor.
        /// </summary>
        /// <param name="length">Length of each axis.</param>
        /// <param name="headUnitRatio">Ratio of the head to the unit.</param>
        public DartSculptor(float length, float headUnitRatio)
        {
            Initialize(length, headUnitRatio);
        }

        /// <summary>
        /// Initializes an existing instance of XyzAxisSculptor.
        /// </summary>
        /// <param name="length">Length of each axis.</param>
        /// <param name="headUnitRatio">Ratio of the head to the unit.</param>
        public void Initialize(float length, float headUnitRatio)
        {
            _length = length;
            _headUnitRatio = headUnitRatio;
        }

        /// <summary>
        /// Initializes the Points and Triangles collections.
        /// Called By Sculptor.BuildMesh()
        /// </summary>
        protected override void CreateTriangles()
        {
            ConicalSculptor _conicalSculptor = new ConicalSculptor(40, 0.0f, 0.0f);
            _conicalSculptor.BuildMesh();
            ModelTransformGroup tgHead = new ModelTransformGroup();
            tgHead.Children.Add(new Scaling(0.2f, 0.2f, _headUnitRatio));
            // tgHead.Children.Add(new Translation(0.0f, 0.0f, _length - 1.0f + 0.8f));
            tgHead.Children.Add(new Translation(0.0f, 0.0f, _length - _headUnitRatio));
            _conicalSculptor.Transform(tgHead);
            CopyFrom(_conicalSculptor);

            BarSculptor _barSculptor = new BarSculptor();
            _barSculptor.Initialize(40, 0.0f, 0.0f);
            _barSculptor.BuildMesh();
            // _barSculptor.Transform(new Scaling(0.1f, 0.1f, _length - 1.0f + 0.8f));
            _barSculptor.Transform(new Scaling(0.1f, 0.1f, _length - _headUnitRatio));
            CopyFrom(_barSculptor);
        }
    }
}
