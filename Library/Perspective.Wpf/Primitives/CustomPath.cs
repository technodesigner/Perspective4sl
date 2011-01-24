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
        private Drawer _drawer = null;
        private bool _isInitialized = true;

        /// <summary>
        /// Initializes a new instance of CustomShape.
        /// </summary>
        public CustomPath()
        {
            Stretch = Stretch.Uniform;
            Loaded += (sender, e) =>
                {
                    _isInitialized = true; // assigned here because Style application occurs after EndInit call.
                    BuildGeometry();
                };
        }

        /// <summary>
        /// Gets the initialization state.
        /// </summary>
        protected bool IsInitialized
        {
            get { return _isInitialized; }
        }

        /// <summary>
        /// A method to override to create the Drawer object.
        /// </summary>
        /// <returns>A Drawer object.</returns>
        protected abstract Drawer CreateDrawer();

        /// <summary>
        /// A method to override to initialize the existing Drawer object.
        /// <remarks>Mathod called at each geometry redefinition.</remarks>
        /// </summary>
        protected abstract void InitializeDrawer();

        protected static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var path = (d as CustomPath);
            if (path._isInitialized)
            {
                path.BuildGeometry();
            }
        }

        protected void BuildGeometry()
        {
            Data = DefiningGeometry;
        }

        /// <summary>
        /// Gets a value that represents the Geometry of the Shape.
        /// </summary>
        private System.Windows.Media.Geometry DefiningGeometry
        {
            get
            {
                if (_drawer == null)
                {
                    _drawer = CreateDrawer();
                }
                InitializeDrawer();
                _drawer.BuildFigures();
                _geometry.Figures = _drawer.Figures;
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
        public void EndInit() {}

        /// <summary>
        /// Resumes rendering the shape control after rendering is suspended by the BeginInit method.
        /// </summary>
        /// <remarks>This method has to be called instead of EndInit when using BeginInit from code.</remarks>
        public void EndInitAndBuildContent()
        {
            _isInitialized = true;
            BuildGeometry();
        }

        /// <summary>
        /// Provides the behavior for the Arrange pass of Silverlight layout.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this object should use to arrange itself and its children.</param>
        /// <returns>The actual size that is used after the element is arranged in layout.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if ((this.ActualWidth != 0.0)
                && (this.ActualHeight != 0.0)
                && (finalSize.Width != this.ActualWidth)
                && (finalSize.Height != this.ActualHeight))
            {
                BuildGeometry();
            }
            return finalSize;
        }
    }
}
