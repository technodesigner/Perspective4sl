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
    /// Draws a pie slice.
    /// </summary>
    public class PieSlice : CustomPath
    {
        /// <summary>
        /// Initializes a new instance of PieSlice.
        /// </summary>
        public PieSlice() : base()
        {
            Stretch = Stretch.None;
            StrokeLineJoin = PenLineJoin.Round;
        }

        /// <summary>
        /// Identifies the InitialAngle dependency property.
        /// Default value is -90.0 (90 degrees counterclockwise).
        /// </summary>
        private static readonly DependencyProperty InitialAngleProperty =
            DependencyProperty.Register(
                "InitialAngle",
                typeof(double),
                typeof(PieSlice),
                new PropertyMetadata(
                    -90.0, PropertyChanged));

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
        /// Gets or sets the pie slice angle value (in degrees).
        /// Default value is 60.0.
        /// </summary>
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        /// <summary>
        /// Identifies the Angle dependency property.
        /// </summary>
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(
                "Angle",
                typeof(double),
                typeof(PieSlice),
                new PropertyMetadata(
                    60.0,
                    PropertyChanged));

        private PieSliceDrawer _drawer;

        /// <summary>
        /// Creates a PieSliceDrawer object.
        /// </summary>
        /// <returns>A PieSliceDrawer object.</returns>
        protected override Drawer CreateDrawer()
        {
            _drawer = new PieSliceDrawer();
            return _drawer;
        }

        /// <summary>
        /// Initializes the PieSliceDrawer object.
        /// </summary>
        protected override void InitializeDrawer()
        {
            _drawer.Initialize(
                Angle,
                InitialAngle,
                ActualWidth,
                ActualHeight,
                StrokeThickness);
        }
    }
}
