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
using Perspective.Core.Wpf;

namespace Perspective.Wpf.Shapes
{
    /// <summary>
    /// Draws a star.
    /// </summary>
    public class Star : CustomPath
    {
        /// <summary>
        /// Identifies the BranchCount dependency property.
        /// </summary>
        public static readonly DependencyProperty BranchCountProperty =
            DependencyProperty.Register(
                "BranchCount",
                typeof(int),
                typeof(Star),
                new PropertyMetadata(
                    6, (d, e) =>
                    {
                        Star path = (d as Star);
                        if (path.BranchCount < 2)
                        {
                            throw new ArgumentException("BranchCount < 2");
                        }
                        if (path.IsInitialized)
                        {
                            path.BuildGeometry();
                        }
                    }));

        /// <summary>
        /// Gets or sets the branch count of the star.
        /// </summary>
        public int BranchCount
        {
            get { return (int)GetValue(BranchCountProperty); }
            set { SetValue(BranchCountProperty, value); }
        }

        /// <summary>
        /// Identifies the BranchWidth dependency property.
        /// </summary>
        public static readonly DependencyProperty BranchWidthProperty =
            DependencyProperty.Register(
                "BranchWidth",
                typeof(double),
                typeof(Star),
                new PropertyMetadata(
                    0.2,
                    PropertyChanged));

        /// <summary>
        /// Gets or sets the width of a star branch.
        /// </summary>
        public double BranchWidth
        {
            get { return (double)GetValue(BranchWidthProperty); }
            set { SetValue(BranchWidthProperty, value); }
        }

        /// <summary>
        /// Identifies the InitialAngle dependency property.
        /// </summary>
        private static readonly DependencyProperty InitialAngleProperty =
            DependencyProperty.Register(
                "InitialAngle",
                typeof(double),
                typeof(Star),
                new PropertyMetadata(
                    0.0,
                    PropertyChanged));

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

        private StarDrawer _drawer;

        /// <summary>
        /// Creates a StarDrawer object.
        /// </summary>
        /// <returns>A StarDrawer object.</returns>
        protected override Drawer CreateDrawer()
        {
            _drawer = new StarDrawer();
            return _drawer;
        }

        /// <summary>
        /// Initializes the StarDrawer object.
        /// <remarks>Mathod called at each geometry redefinition.</remarks>
        /// </summary>
        protected override void InitializeDrawer()
        {
            _drawer.Initialize(
                BranchCount,
                BranchWidth,
                InitialAngle,
                Width,
                Height,
                StrokeThickness);
        }
    }
}
