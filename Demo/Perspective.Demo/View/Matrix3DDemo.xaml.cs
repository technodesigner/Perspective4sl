using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Perspective.Core.Wpf;
using System.Windows.Media.Media3D;

namespace Perspective.Demo.View
{
    public partial class Matrix3DDemo : Page
    {
        public Matrix3DDemo()
        {
            InitializeComponent();
        }

        // S'exécute lorsque l'utilisateur navigue vers cette page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyPerspectiveProjection(tb10, rotationY:45.0);
        }

        private void image10_ImageOpened(object sender, RoutedEventArgs e)
        {
            ApplyPerspectiveProjection(image10, centerXOfRotation:1.0, rotationY:30.0);
        }

        private void Grid_LayoutUpdated(object sender, EventArgs e)
        {
            ApplyPerspectiveProjection(r10, rotationZ: 30.0);
        }

        private void ApplyPerspectiveProjection(
            FrameworkElement element,
            double centerXOfRotation = 0.5,
            double centerYOfRotation = 0.5,
            double centerZOfRotation = 0.0,
            double rotationX = 0.0,
            double rotationY = 0.0,
            double rotationZ = 0.0,
            double localOffsetX = 0.0,
            double localOffsetY = 0.0,
            double localOffsetZ = 0.0,
            double globalOffsetX = 0.0,
            double globalOffsetY = 0.0,
            double globalOffsetZ = 0.0,
            double fovY = 57.0)         // The field of view of a Planeprojection is 57.0 - thanks to Jaime Rodriguez http://blogs.msdn.com/b/jaimer/archive/2009/06/03/silverlight3-planeprojection-primer.aspx
        {
            var m = new Matrix3D();

            double width = element.ActualWidth;
            double height = element.ActualHeight;

            // World definition
            //-----------------

            // Defines the central point of rotations for the element
            // (that is the origin of the coordinate system)
            m *= Helper3D.GetTranslationMatrix(
                    -width * centerXOfRotation,
                    -height * centerYOfRotation,
                    -centerZOfRotation);

            // Local translation
            m *= Helper3D.GetTranslationMatrix(
                localOffsetX,
                localOffsetY,
                localOffsetZ);

            // Inverts the Y axis (in 2D, the direction is down, in 3D, it's up)
            // The resulting coordinate system is a left-handed one
            m *= Helper3D.GetScaleMatrix(1.0, -1.0, 1.0);
            
            //// Rotates the element
            if (rotationX != 0.0)
            {
                m *= Helper3D.GetXRotationMatrix(rotationX);
            }
            if (rotationY != 0.0)
            {
                m *= Helper3D.GetYRotationMatrix(rotationY);
            }
            if (rotationZ != 0.0)
            {
                m *= Helper3D.GetZRotationMatrix(rotationZ);
            }

            // Global translation
            m *= Helper3D.GetTranslationMatrix(
                globalOffsetX,
                globalOffsetY,
                globalOffsetZ);

            // View defintion
            //---------------
            // Camera's position
            // Moves the camera on the Z-axis so that the element fits
            // (the sign is negative because a camera inverts the Z axis in its coordinate system)
            m *= Helper3D.GetTranslationMatrix(0.0, 0.0,
                -height / Math.Tan(GeometryHelper.DegreeToRadian(fovY) / 2.0));

            // Projection definition
            //----------------------
            // Represents the 3D scene in perspective
            // The clip planes distance of a Planeprojection is 999.0 - thanks to Jaime Rodriguez http://blogs.msdn.com/b/jaimer/archive/2009/06/03/silverlight3-planeprojection-primer.aspx
            m *= Helper3D.GetPerspectiveMatrix(
                fovY,
                width / height,   // image format
                1.0,              // near plane
                1000.0);          // far plane
            //0.0,              // near plane
            //999.0);          // far plane
            
            // Viewport definition
            //--------------------
            // Re-inverts the Y axis, to fit in the 2D coordinate system
            // Cancels the initial rotation point translation
            m *= new Matrix3D
            {
                M11 = width,
                M22 = -height,
                OffsetX = width * centerXOfRotation,
                OffsetY = height * centerYOfRotation
            };

            // Applies the projection
            //-----------------------
            Matrix3DProjection m3dProjection = new Matrix3DProjection();
            m3dProjection.ProjectionMatrix = m;
            element.Projection = m3dProjection;
        }
    }
}
