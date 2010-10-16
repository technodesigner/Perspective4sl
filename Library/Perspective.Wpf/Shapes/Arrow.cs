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
    /// Draws an arrow.
    /// </summary>
    public class Arrow : CustomPath
    {
        /// <summary>
        /// Identifies the FormatRatio dependency property.
        /// </summary>
        public static readonly DependencyProperty FormatRatioProperty =
            DependencyProperty.Register(
                "FormatRatio",
                typeof(double),
                typeof(Arrow),
                new PropertyMetadata(
                    2.5, PropertyChanged));

        /// <summary>
        /// Gets or sets a value indicating the width/height ratio (the figure's format).
        /// Default value is 2.5.
        /// </summary>
        public double FormatRatio
        {
            get { return (double)GetValue(FormatRatioProperty); }
            set { SetValue(FormatRatioProperty, value); }
        }

        /// <summary>
        /// Identifies the HeadLengthRatio dependency property.
        /// </summary>
        public static readonly DependencyProperty HeadLengthRatioProperty =
            DependencyProperty.Register(
                "HeadLengthRatio",
                typeof(double),
                typeof(Arrow),
                new PropertyMetadata(
                    0.2, PropertyChanged));

        /// <summary>
        /// Gets or sets a value indicating the "head length/total length" ratio.
        /// Default value is 0.2.
        /// </summary>
        public double HeadLengthRatio
        {
            get { return (double)GetValue(HeadLengthRatioProperty); }
            set { SetValue(HeadLengthRatioProperty, value); }
        }

        /// <summary>
        /// Identifies the HeadWidthRatio dependency property.
        /// </summary>
        public static readonly DependencyProperty HeadWidthRatioProperty =
            DependencyProperty.Register(
                "HeadWidthRatio",
                typeof(double),
                typeof(Arrow),
                new PropertyMetadata(
                    2.0, PropertyChanged));

        /// <summary>
        /// Gets or sets a value indicating the "head width/body width" ratio.
        /// </summary>
        public double HeadWidthRatio
        {
            get { return (double)GetValue(HeadWidthRatioProperty); }
            set { SetValue(HeadWidthRatioProperty, value); }
        }

        private ArrowDrawer _drawer;

        /// <summary>
        /// Creates a ArrowDrawer object.
        /// </summary>
        /// <returns>A ArrowDrawer object.</returns>
        protected override Drawer CreateDrawer()
        {
            _drawer = new ArrowDrawer();
            return _drawer;
        }

        /// <summary>
        /// Initializes the ArrowDrawer object.
        /// </summary>
        protected override void InitializeDrawer()
        {
            _drawer.Initialize(
                FormatRatio,
                HeadLengthRatio,
                HeadWidthRatio,
                Width,
                Height,
                StrokeThickness);
        }
    }
}
