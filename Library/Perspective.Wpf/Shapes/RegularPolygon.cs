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
using Perspective.Wpf.Drawers;

namespace Perspective.Wpf.Shapes
{
    /// <summary>
    /// Draws a regular polygon.
    /// </summary>
    public class RegularPolygon : CustomPath
    {
        /// <summary>
        /// Identifies the InitialAngle dependency property.
        /// </summary>
        private static readonly DependencyProperty InitialAngleProperty =
            DependencyProperty.Register(
                "InitialAngle",
                typeof(double),
                typeof(RegularPolygon),
                new PropertyMetadata(
                    0.0, PropertyChanged));

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
                typeof(RegularPolygon),
                new PropertyMetadata(
                    6, (d, e) =>
                    {
                        RegularPolygon path = (d as RegularPolygon);
                        if (path.SideCount < 3)
                        {
                            throw new ArgumentException("SideCount < 3");
                        }
                        // shape.BuildGeometryIfInitialized();
                        if (path.IsInitialized)
                        {
                            path.BuildGeometry();
                        }
                    }));

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
            }
        }

        private RegularPolygonDrawer _drawer;

        /// <summary>
        /// Creates a RegularPolygonDrawer object.
        /// </summary>
        /// <returns>A RegularPolygonDrawer object.</returns>
        protected override Drawer CreateDrawer()
        {
            _drawer = new RegularPolygonDrawer();
            return _drawer;
        }

        /// <summary>
        /// Initializes the RegularPolygonDrawer object.
        /// </summary>
        protected override void InitializeDrawer()
        {
            _drawer.Initialize(
                InitialAngle,
                SideCount,
                ActualWidth,
                ActualHeight,
                StrokeThickness);
        }
    }
}
