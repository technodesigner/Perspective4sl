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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Perspective.Wpf.Primitives;
using Perspective.Core.Wpf;

namespace Perspective.Wpf.Drawers
{
    /// <summary>
    /// A class to generate the points and segments of a regular polygon Pathfigure object.
    /// </summary>
    public class RegularPolygonDrawer : Drawer
    {
        private double _initialAngle;
        private int _sideCount;
        private PointCollection _points = new PointCollection();
        private PathFigure _figure = new PathFigure();

        /// <summary>
        /// Initializes a new instance of RegularPolygonDrawer.
        /// </summary>
        public RegularPolygonDrawer()
            : base()
        {
            _figure.IsClosed = true;
        }

        /// <summary>
        /// Initializes a new instance of RegularPolygonDrawer.
        /// </summary>
        /// <param name="initialAngle">The initial angle (first point), in degrees.</param>
        /// <param name="sideCount">The side count of the circumference.</param>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        public RegularPolygonDrawer(
            double initialAngle,
            int sideCount,
            double width,
            double height,
            double strokeThickness) : this()
        {
            Initialize(initialAngle, sideCount, width, height, strokeThickness);
        }

        /// <summary>
        /// Initializes a RegularPolygonDrawer object.
        /// </summary>
        /// <param name="initialAngle">The initial angle (first point), in degrees.</param>
        /// <param name="sideCount">The side count of the circumference.</param>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        public void Initialize(
            double initialAngle,
            int sideCount,
            double width,
            double height,
            double strokeThickness)
        {
            if (sideCount < 3)
            {
                throw new ArgumentOutOfRangeException("sideCount < 3");
            }
            _initialAngle = initialAngle;
            _sideCount = sideCount;
            base.Initialize(width, height, strokeThickness);
        }

        /// <summary>
        /// Generates the points and segments of the polygonal Figure object.
        /// </summary>
        protected override void BuildFiguresOverride()
        {
            _points.Clear();
            double initialAngleRad = GeometryHelper.DegreeToRadian(_initialAngle);
            double angle1Rad = 2 * Math.PI / _sideCount;
            double angleRad = 0.0;
            double areaWidth = Double.IsNaN(Width) ? 0.0 : Width / 2;
            double areaHeight = Double.IsNaN(Height) ? 0.0 : Height / 2;
            for (int i = 1; i <= _sideCount; i++)
            {
                angleRad = (angle1Rad * i) + initialAngleRad;
                Point p = new Point();
                p.X = areaWidth == 0.0 ?
                    Math.Cos(angleRad) :
                    areaWidth + Math.Cos(angleRad) * (areaWidth - StrokeThickness / 2);
                p.Y = areaHeight == 0.0 ?
                    Math.Sin(angleRad) :
                    areaHeight + Math.Sin(angleRad) * (areaHeight - StrokeThickness / 2);
                _points.Add(p);
            }
            _figure.Segments.Clear();
            _figure.StartPoint = _points[0];
            for (int i = 0; i < _points.Count; i++)
            {
                if (i < _points.Count - 1)
                {
                    LineSegment segment = new LineSegment();
                    segment.Point = _points[i + 1];
                    _figure.Segments.Add(segment);
                }
            }
            Figures.Add(_figure);
        }
    }
}
