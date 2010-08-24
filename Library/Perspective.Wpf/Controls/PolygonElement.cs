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
using Perspective.Core.Wpf;

namespace Perspective.Wpf.Controls
{
    /// <summary>
    /// Draws a regular polygon.
    /// </summary>
    public class PolygonElement : Panel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PolygonElement()
        {
            _points = new PointCollection();
            this.Loaded += new RoutedEventHandler(Polygon_Loaded);
        }

        void Polygon_Loaded(object sender, RoutedEventArgs e)
        {
            BuildContent();
        }

        private PointCollection _points;

        /// <summary>
        /// Gets the shape points collection.
        /// </summary>
        public PointCollection Points
        {
            get { return _points; }
        }

        /// <summary>
        /// Clear the point collections and the content.
        /// </summary>
        private void Clear()
        {
            Children.Clear();
            _points.Clear();
        }

        /// <summary>
        /// Identifies the Stroke dependency property.
        /// </summary>
        private static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register(
                "Stroke",
                typeof(Brush),
                typeof(PolygonElement),
                new PropertyMetadata(StrokePropertyChanged));

        private static void StrokePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PolygonElement p = (d as PolygonElement);
            if (p._path != null)
            {
                p._path.Stroke = p.Stroke;
            }
        }

        /// <summary>
        /// Gets or sets the Brush that specifies how the polygon outline is painted. 
        /// </summary>
        public Brush Stroke
        {
            get
            {
                return (Brush)base.GetValue(StrokeProperty);
            }
            set
            {
                base.SetValue(StrokeProperty, value);
            }
        }

        /// <summary>
        /// Identifies the Fill dependency property.
        /// </summary>
        private static readonly DependencyProperty FillProperty =
            DependencyProperty.Register(
                "Fill",
                typeof(Brush),
                typeof(PolygonElement),
                new PropertyMetadata(FillPropertyChanged));

        private static void FillPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PolygonElement p = (d as PolygonElement);
            if (p._path != null)
            {
                p._path.Fill = p.Fill;
            }
        }

        /// <summary>
        /// Gets or sets the Brush that specifies how to paint the interior of the polygon. 
        /// </summary>
        public Brush Fill
        {
            get
            {
                return (Brush)base.GetValue(FillProperty);
            }
            set
            {
                base.SetValue(FillProperty, value);
            }
        }

        /// <summary>
        /// Identifies the StrokeThickness dependency property.
        /// </summary>
        private static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register(
                "StrokeThickness",
                typeof(double),
                typeof(PolygonElement),
                new PropertyMetadata(0.0, StrokeThicknessPropertyChanged));

        private static void StrokeThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PolygonElement).BuildContent();
        }

        /// <summary>
        /// Gets or sets the width of the polygon stroke outline.
        /// </summary>
        public double StrokeThickness
        {
            get
            {
                return (double)base.GetValue(StrokeThicknessProperty);
            }
            set
            {
                base.SetValue(StrokeThicknessProperty, value);
            }
        }


        /// <summary>
        /// Identifies the InitialAngle dependency property.
        /// </summary>
        private static readonly DependencyProperty InitialAngleProperty =
            DependencyProperty.Register(
                "InitialAngle",
                typeof(double),
                typeof(PolygonElement),
                new PropertyMetadata(0.0, InitialAnglePropertyChanged));

        private static void InitialAnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PolygonElement).BuildContent();
        }

        /// <summary>
        /// Gets or sets the initial angle (first point), in degrees.
        /// </summary>
        public double InitialAngle
        {
            get
            {
                return (double)base.GetValue(InitialAngleProperty);
            }
            set
            {
                base.SetValue(InitialAngleProperty, value);
            }
        }

        /// <summary>
        /// Identifies the SideCount dependency property.
        /// </summary>
        private static readonly DependencyProperty SideCountProperty =
            DependencyProperty.Register(
                "SideCount",
                typeof(int),
                typeof(PolygonElement),
                new PropertyMetadata(6, SideCountPropertyChanged));

        private static void SideCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PolygonElement element = (d as PolygonElement);
            if (element.SideCount < 3)
            {
                throw new ArgumentException("SideCount < 3");
            }
            element.BuildContent();
        }

        /// <summary>
        /// Gets or sets the side count of the circumference.
        /// </summary>
        public int SideCount
        {
            get 
            {
                return (int)base.GetValue(SideCountProperty);
            }
            set
            {
                base.SetValue(SideCountProperty, value);
                // BuildContent();
            }
        }

        /// <summary>
        /// Create the points of a regular polygon.
        /// </summary>
        private void CreatePoints()
        {
            this.Points.Clear();
            double initialAngle = GeometryHelper.DegreeToRadian(InitialAngle);
            double angle1 = 2 * Math.PI / SideCount;
            double angle = 0.0;
            double areaWidth = Double.IsNaN(Width) ? DesiredSize.Width / 2 : Width / 2;
            double areaHeight = Double.IsNaN(Height) ? DesiredSize.Height / 2 : Height / 2;
            for (int i = 1; i <= SideCount; i++)
            {
                angle = (angle1 * i) + initialAngle;
                Point p = new Point();
                p.X = areaWidth + Math.Cos(angle) * (areaWidth - _path.StrokeThickness / 2);
                p.Y = areaHeight + Math.Sin(angle) * (areaHeight - _path.StrokeThickness / 2);
                this.Points.Add(p);
            }
        }

        private Path _path;

        private void BuildContent()
        {
            if ((!Double.IsNaN(this.Width)) || (DesiredSize.Width != 0))
            {
                this.Clear();
                _path = new Path();

                _path.Stroke = Stroke;
                _path.StrokeThickness = StrokeThickness;
                _path.Fill = Fill;

                this.CreatePoints();

                PathFigure figure = new PathFigure();
                figure.IsClosed = true;
                figure.StartPoint = _points[0];
                for (int i = 0; i < _points.Count; i++)
                {
                    if (i < _points.Count - 1)
                    {
                        LineSegment segment = new LineSegment();
                        segment.Point = _points[i + 1];
                        figure.Segments.Add(segment);
                    }
                }
                PathGeometry geometry = new PathGeometry();
                geometry.Figures.Add(figure);
                _path.Data = geometry;

                Children.Add(_path);
            }
        }

        /// <summary>
        /// Provides the behavior for the "measure" pass of Silverlight layout.
        /// </summary>
        /// <param name="availableSize">The available size that this object can give to child objects.</param>
        /// <returns>The size that this object determines it needs during layout, based on its calculations of child object allotted sizes.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (_path != null)
            {
                _path.Measure(availableSize);
            }
            return availableSize;
            // return _path.DesiredSize;
        }

        /// <summary>
        /// Provides the behavior for the "Arrange" pass of Silverlight layout.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this object should use to arrange itself and its children.</param>
        /// <returns>The actual size used.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_path == null)
            {
                BuildContent();
            }
            Rect finalRect = new Rect(
                0.0, 
                0.0, 
                _path.DesiredSize.Width, 
                _path.DesiredSize.Height);
            _path.Arrange(finalRect);
            return finalSize;
        }
    }
}
