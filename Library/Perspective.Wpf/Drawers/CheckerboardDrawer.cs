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

namespace Perspective.Wpf.Drawers
{
    /// <summary>
    /// A class to generate the segments of a checkerboard Pathfigure collection object.
    /// </summary>
    public class CheckerboardDrawer : Drawer
    {
        private int _rowCount = 8;
        private int _columnCount = 8;
        private int _cellLength = 25;

        /// <summary>
        /// Initializes a new instance of CheckerboardDrawer.
        /// </summary>
        public CheckerboardDrawer()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of CheckerboardDrawer.
        /// </summary>
        /// <param name="rowCount">The checkerboard row count.</param>
        /// <param name="columnCount">The checkerboard column count.</param>
        /// <param name="cellLength">The checkerboard cell side length, in pixels.</param>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        public CheckerboardDrawer(
            int rowCount,
            int columnCount,
            int cellLength,
            double width,
            double height,
            double strokeThickness)
            : this()
        {
            Initialize(rowCount, columnCount, cellLength, width, height, strokeThickness);
        }

        /// <summary>
        /// Initializes a CheckerboardDrawer object.
        /// </summary>
        /// <param name="rowCount">The checkerboard row count.</param>
        /// <param name="columnCount">The checkerboard column count.</param>
        /// <param name="cellLength">The checkerboard cell side length, in pixels.</param>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        public void Initialize(
            int rowCount,
            int columnCount,
            int cellLength,
            double width,
            double height,
            double strokeThickness)
        {
            _rowCount = rowCount;
            _columnCount = columnCount;
            _cellLength = cellLength;
            base.Initialize(width, height, strokeThickness);
        }

        /// <summary>
        /// Generates the segments of the checkerboard Figure object.
        /// </summary>
        protected override void BuildFiguresOverride()
        {
            for (int i = 0; i < _columnCount; i++)
            {
                for (int j = 0; j < _rowCount; j++)
                {
                    if (((i % 2 == 0) && (j % 2 == 0))
                        || ((i % 2 == 1) && (j % 2 == 1)))
                    {
                        CreateFigure(i * _cellLength, j * _cellLength);
                    }
                }
            }
        }

        private void CreateFigure(int x, int y)
        {
            var figure = new PathFigure();
            figure.IsClosed = true;
            figure.StartPoint = new Point(x, y);

            LineSegment segment1 = new LineSegment();
            segment1.Point = new Point(x + _cellLength, y);
            figure.Segments.Add(segment1);

            LineSegment segment2 = new LineSegment();
            segment2.Point = new Point(x + _cellLength, y + _cellLength);
            figure.Segments.Add(segment2);

            LineSegment segment3 = new LineSegment();
            segment3.Point = new Point(x, y + _cellLength);
            figure.Segments.Add(segment3);

            Figures.Add(figure);
        }
    }
}
