﻿//------------------------------------------------------------------
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

namespace Perspective.Wpf.Primitives
{
    /// <summary>
    /// An abstract class to generate the Pathfigure objects of a custom shape.
    /// </summary>
    public abstract class Drawer
    {
        private PathFigureCollection _figures = new PathFigureCollection();

        /// <summary>
        /// Gets the drawer's collection of Pathfigure objects.
        /// </summary>
        public PathFigureCollection Figures
        {
            get { return _figures; }
        }

        private double _width = double.NaN;

        /// <summary>
        /// Gets the width of the drawing area (if specified in the constructor).
        /// </summary>
        public double Width
        {
            get { return _width; }
        }

        private double _height = double.NaN;

        /// <summary>
        /// Gets the height of the drawing area (if specified in the constructor).
        /// </summary>
        public double Height
        {
            get { return _height; }
        }

        private double _strokeThickness;

        /// <summary>
        /// Gets the stroke thickness of the drawing (if specified in the constructor).
        /// </summary>
        public double StrokeThickness
        {
            get { return _strokeThickness; }
        }

        /// <summary>
        /// Initializes a Drawer object.
        /// </summary>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        /// <param name="strokeThickness">The stroke thickness of the drawing.</param>
        protected void Initialize(
            double width,
            double height,
            double strokeThickness)
        {
            _width = width;
            _height = height;
            _strokeThickness = strokeThickness;
        }

        /// <summary>
        /// A method to call to generate the figure(s).
        /// </summary>
        public void BuildFigures()
        {
            Figures.Clear();
            BuildFiguresOverride();
        }

        /// <summary>
        /// A method to override to generate the figure(s).
        /// </summary>
        protected abstract void BuildFiguresOverride();
    }
}
