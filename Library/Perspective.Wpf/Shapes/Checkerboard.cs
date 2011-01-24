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
using System.ComponentModel;

namespace Perspective.Wpf.Shapes
{
    /// <summary>
    /// Draws a checkerboard.
    /// </summary>
    public class Checkerboard : CustomPath
    {
        /// <summary>
        /// Gets or sets the checkboard row count.
        /// The default value is 8.
        /// </summary>
        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        /// <summary>
        /// Identifies the RowCount dependency property.
        /// </summary>
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register(
                "RowCount",
                typeof(int),
                typeof(Checkerboard),
                new PropertyMetadata(8, PropertyChanged));

        /// <summary>
        /// Gets or sets the checkboard column count.
        /// The default value is 8.
        /// </summary>
        public int ColumnCount
        {
            get { return (int)GetValue(ColumnCountProperty); }
            set { SetValue(ColumnCountProperty, value); }
        }

        /// <summary>
        /// Identifies the ColumnCount dependency property.
        /// </summary>
        public static readonly DependencyProperty ColumnCountProperty =
            DependencyProperty.Register(
                "ColumnCount",
                typeof(int),
                typeof(Checkerboard),
                new PropertyMetadata(8, PropertyChanged));


        /// <summary>
        /// Gets or sets the checkboard cell side length, in pixels.
        /// The default value is 25.
        /// </summary>
        public int CellLength
        {
            get { return (int)GetValue(CellLengthProperty); }
            set { SetValue(CellLengthProperty, value); }
        }

        /// <summary>
        /// Identifies the CellLength dependency property.
        /// </summary>
        public static readonly DependencyProperty CellLengthProperty =
            DependencyProperty.Register(
                "CellLength",
                typeof(int),
                typeof(Checkerboard),
                new PropertyMetadata(25, PropertyChanged));

        private CheckerboardDrawer _drawer;

        /// <summary>
        /// Creates a CheckerboardDrawer object.
        /// </summary>
        /// <returns>A CheckerboardDrawer object.</returns>
        protected override Drawer CreateDrawer()
        {
            _drawer = new CheckerboardDrawer();
            return _drawer;
        }

        /// <summary>
        /// Initializes the CheckerboardDrawer object.
        /// <remarks>Mathod called at each geometry redefinition.</remarks>
        /// </summary>
        protected override void InitializeDrawer()
        {
            _drawer.Initialize(
                RowCount,
                ColumnCount,
                CellLength,
                Width,
                Height,
                StrokeThickness);
        }

    }
}
