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
using System.ComponentModel;

namespace Perspective.Wpf.Primitives
{
    /// <summary>
    /// Base class for custom shapes.
    /// </summary>
    /// <remarks>The Silverlight 4 XAML processor supports ISupportInitialize behavior.</remarks>
    public abstract class CustomPath : Path, ISupportInitialize
    {
        private PathGeometry _geometry = new PathGeometry();
        private Drawer _drawer;
        private bool _isInitialized = true;

        /// <summary>
        /// Initializes a new instance of CustomShape.
        /// </summary>
        public CustomPath()
        {
            Stretch = Stretch.Uniform;
            _drawer = CreateDrawer();
            Loaded += (sender, e) =>
                {
                    _isInitialized = true; // assigned here because Style application occurs after EndInit call.
                    BuildContent();
                };
        }

        /// <summary>
        /// Gets the shape's geometry object.
        /// </summary>
        protected PathGeometry Geometry
        {
            get { return _geometry; }
        }

        /// <summary>
        /// A method to override to create the Drawer object.
        /// </summary>
        /// <returns>A Drawer object.</returns>
        protected abstract Drawer CreateDrawer();

        /// <summary>
        /// A method to override to initialize the existing Drawer object.
        /// </summary>
        protected abstract void InitializeDrawer();

        protected static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CustomPath).BuildContent();
        }

        protected void BuildContent()
        {
            if (_isInitialized)
            {
                Data = DefiningGeometry;
            }
        }

        /// <summary>
        /// Gets a value that represents the Geometry of the Shape.
        /// </summary>
        private System.Windows.Media.Geometry DefiningGeometry
        {
            get
            {
                InitializeDrawer();
                if (_drawer != null)
                {
                    _drawer.BuildFigure();
                    _geometry.Figures = _drawer.Figures;
                }
                return _geometry;
            }
        }

        /// <summary>
        /// Maintains performance while properties initialization by preventing the element from rendering until the EndInit method is called.
        /// </summary>
        /// <remarks>This method is automatically called by the Silverlight 4 XAML processor before parsing the element.</remarks>
        public void BeginInit()
        {
            _isInitialized = false;
        }

        /// <summary>
        /// Resumes rendering the shape control after rendering is suspended by the BeginInit method.
        /// </summary>
        /// <remarks>This method is automatically called by the Silverlight 4 XAML processor after parsing the element, but before a style is applied... So it does nothing. From code, call EndInitAndBuildContent instead.</remarks>
        public void EndInit()
        {
        }

        /// <summary>
        /// Resumes rendering the shape control after rendering is suspended by the BeginInit method.
        /// </summary>
        /// <remarks>This method has to be called instead of EndInit when using BeginInit from code.</remarks>
        public void EndInitAndBuildContent()
        {
            _isInitialized = true;
            BuildContent();
        }

        /// <summary>
        /// Provides the behavior for the Arrange pass of Silverlight layout.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this object should use to arrange itself and its children.</param>
        /// <returns>The actual size that is used after the element is arranged in layout.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            // BuildContent call required to prevent disappearance of Fill brush i.e. in a TabItem.
            // Size comparison required to prevent layout loop.
            if ((finalSize.Width != this.ActualWidth)
                && (finalSize.Height != this.ActualHeight))
            {
                BuildContent();
            }
            return finalSize;
        }
    }
}
