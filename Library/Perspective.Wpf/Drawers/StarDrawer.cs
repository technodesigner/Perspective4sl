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
    /// A class to generate the points and segments of a star Pathfigure object.
    /// </summary>
    public class StarDrawer : Drawer
    {
        private int _branchCount;
        private double _branchWidth;
        private double _initialAngle;
        private PointCollection _points = new PointCollection();
        private PathFigure _figure = new PathFigure();

        /// <summary>
        /// Initializes a new instance of StarDrawer.
        /// </summary>
        public StarDrawer()
            : base()
        {
            _figure.IsClosed = true;
        }

        /// <summary>
        /// Initializes a new instance of StarDrawer.
        /// </summary>
        /// <param name="branchCount">The branch count of the star.</param>
        /// <param name="branchWidth">The width of a star branch.</param>
        /// <param name="initialAngle">Angle between the axis [origin - first point] and the X-axis, in degrees.</param>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        public StarDrawer(
            int branchCount,
            double branchWidth,
            double initialAngle,
            double width,
            double height,
            double strokeThickness)
            : this()
        {
            Initialize(branchCount, branchWidth, initialAngle, width, height, strokeThickness);
        }

        /// <summary>
        /// Initializes a StarDrawer object.
        /// </summary>
        /// <param name="branchCount">The branch count of the star.</param>
        /// <param name="branchWidth">The width of a star branch.</param>
        /// <param name="initialAngle">Angle between the axis [origin - first point] and the X-axis, in degrees.</param>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        public void Initialize(
            int branchCount,
            double branchWidth,
            double initialAngle,
            double width,
            double height,
            double strokeThickness)
        {
            if (branchCount < 2)
            {
                throw new ArgumentOutOfRangeException("sideCount < 2");
            }
            _branchCount = branchCount;
            _branchWidth = branchWidth;
            _initialAngle = initialAngle;
            base.Initialize(width, height, strokeThickness);
        }

        /// <summary>
        /// Generates the points and segments of the star Figure object.
        /// </summary>
        protected override void BuildFiguresOverride()
        {
            double initialAngleRad = GeometryHelper.DegreeToRadian(_initialAngle);
            double angle1Rad = 2 * Math.PI / _branchCount;
            double angleBranchHeadOffset = angle1Rad / 2;
            double angleRad = 0.0;
            double areaWidth = Double.IsNaN(Width) ? 0.0 : Width / 2;
            double areaHeight = Double.IsNaN(Height) ? 0.0 : Height / 2;
            _points.Clear();
            for (int i = 0; i < _branchCount; i++)
            {
                // base point
                angleRad = (angle1Rad * i) + initialAngleRad;
                Point p = new Point();
                p.X = areaWidth == 0.0 ?
                    Math.Cos(angleRad) * _branchWidth :
                    areaWidth + Math.Cos(angleRad) * _branchWidth * (areaWidth - StrokeThickness / 2);
                p.Y = areaHeight == 0.0 ?
                    Math.Sin(angleRad) * _branchWidth :
                    areaHeight + Math.Sin(angleRad) * _branchWidth * (areaHeight - StrokeThickness / 2);
                _points.Add(p);

                // head point
                double angleBranchHead = angleRad + angleBranchHeadOffset;
                Point pBranch = new Point();
                pBranch.X = areaWidth == 0.0 ?
                    Math.Cos(angleBranchHead) :
                    areaWidth + Math.Cos(angleBranchHead) * (areaWidth - StrokeThickness / 2);
                pBranch.Y = areaHeight == 0.0 ?
                    Math.Sin(angleBranchHead) :
                    areaHeight + Math.Sin(angleBranchHead) * (areaHeight - StrokeThickness / 2);
                _points.Add(pBranch);
            }

            _figure.Segments.Clear();
            _figure.StartPoint = _points[0];
            for (int i = 0; i < _points.Count; i++)
            {
                if (i < _points.Count - 1)
                {
                    // LineSegment segment = new LineSegment(Points[i + 1], true);
                    LineSegment segment = new LineSegment();
                    segment.Point = _points[i + 1];
                    _figure.Segments.Add(segment);
                }
            }
            Figures.Add(_figure);
        }
    }
}
