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
    /// A class to generate the segments of a pie slice Pathfigure object.
    /// </summary>
    public class PieSliceDrawer : Drawer
    {
        private PathFigure  _figure = new PathFigure();
        private double _angle;
        private double _initialAngle;

        /// <summary>
        /// Initializes a new instance of PieSliceDrawer.
        /// </summary>
        public PieSliceDrawer()
            : base()
        {
            _figure.IsClosed = true;
        }

        /// <summary>
        /// Initializes a new instance of PieSliceDrawer.
        /// </summary>
        /// <param name="angle">The pie slice angle value (in degrees).</param>
        /// <param name="initialAngle">The initial angle (first point), in degrees.</param>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        public PieSliceDrawer(
            double angle,
            double initialAngle,
            double width,
            double height,
            double strokeThickness) : this()
        {
            Initialize(angle, initialAngle, width, height, strokeThickness);
        }

        /// <summary>
        /// Initializes a PieSliceDrawer object.
        /// </summary>
        /// <param name="angle">The pie slice angle value (in degrees).</param>
        /// <param name="initialAngle">The initial angle (first point), in degrees.</param>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        public void Initialize(
            double angle,
            double initialAngle,
            double width,
            double height,
            double strokeThickness)
        {
            _angle = angle;
            _initialAngle = initialAngle;
            base.Initialize(width, height, strokeThickness);
        }

        /// <summary>
        /// Generates the segments of the pie slice object.
        /// </summary>
        protected override void BuildFiguresOverride()
        {
            double initialAngleRad = GeometryHelper.DegreeToRadian(_initialAngle);
            double effectiveAngleRad = GeometryHelper.DegreeToRadian(_initialAngle + _angle);
            double radiusX = Double.IsNaN(Width) ? 1.0 : (Width - StrokeThickness) / 2;
            double radiusY = Double.IsNaN(Height) ? 1.0 : (Height - StrokeThickness) / 2;

            _figure.Segments.Clear();
            _figure.StartPoint = new Point(radiusX, radiusY);

            LineSegment lineSegment = new LineSegment();
            lineSegment.Point = new Point(
                radiusX + Math.Cos(initialAngleRad) * radiusX,
                radiusY + Math.Sin(initialAngleRad) * radiusY);
            _figure.Segments.Add(lineSegment);

            ArcSegment arcSegment = new ArcSegment();
            arcSegment.Point = new Point(
                radiusX + Math.Cos(effectiveAngleRad) * radiusX,
                radiusY + Math.Sin(effectiveAngleRad) * radiusY);
            arcSegment.Size = new Size(radiusX, radiusY);
            arcSegment.SweepDirection = SweepDirection.Clockwise;
            arcSegment.IsLargeArc = (_angle % 360) >= 180;
            _figure.Segments.Add(arcSegment);

            Figures.Add(_figure);
        }
    }
}
