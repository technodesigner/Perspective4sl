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
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using Perspective.Core.Wpf;

namespace Perspective.Core.Wpf
{
    /// <summary>
    /// A helper class for 3D operations.
    /// </summary>
    public static class Helper3D
    {
        /// <summary>
        /// Creates a 3D scaling matrix.
        /// </summary>
        /// <param name="scaleX">The x-axis scale factor.</param>
        /// <param name="scaleY">The y-axis scale factor.</param>
        /// <param name="scaleZ">The z-axis scale factor.</param>
        /// <returns>A Matrix3D object.</returns>
        public static Matrix3D GetScaleMatrix(double scaleX, double scaleY, double scaleZ)
        {
            //var m = new Matrix3D();
            //m.Scale(new Vector3D(scaleX, scaleY, scaleZ));
            //return m;
            return new Matrix3D
            {
                M11 = scaleX,
                M22 = scaleY,
                M33 = scaleZ
            };
        }

        /// <summary>
        /// Creates a 3D scaling matrix.
        /// </summary>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <returns>A Matrix3D object.</returns>
        public static Matrix3D GetScaleMatrix(double scaleFactor)
        {
            return new Matrix3D
            {
                M11 = scaleFactor,
                M22 = scaleFactor,
                M33 = scaleFactor
            };
        }

        /// <summary>
        /// Creates a 3D translation matrix.
        /// </summary>
        /// <param name="offsetX">The distance to translate along the x-axis.</param>
        /// <param name="offsetY">The distance to translate along the y-axis.</param>
        /// <param name="offsetZ">The distance to translate along the z-axis.</param>
        /// <returns>A Matrix3D object.</returns>
        public static Matrix3D GetTranslationMatrix(double offsetX, double offsetY, double offsetZ)
        {
            //var m = new Matrix3D();
            //m.Translate(new Vector3D(offsetX, offsetY, offsetZ));
            //return m;
            return new Matrix3D
            {
                OffsetX = offsetX,
                OffsetY = offsetY,
                OffsetZ = offsetZ
            };
        }

        /// <summary>
        /// Creates a 3D X-rotation matrix.
        /// </summary>
        /// <remarks>For a left-handed 3D system (the default in Silverlight projections), a positive angle value results in a clockwise rotation around the axis. For a right-handed 3D system, a positive angle value results in a counter-clockwise rotation around the axis.</remarks>
        /// <param name="angle">Angle value in degree.</param>
        /// <param name="handedness">The handedness of the coordinate system (optional). Under Silverlight, the defaut value is Left-handed.</param>
        /// <returns>A Matrix3D object.</returns>
        public static Matrix3D GetXRotationMatrix(double angle, Handedness3D handedness = Handedness3D.LeftHanded)
        {
            //var m = new Matrix3D();
            //m.Rotate(new Quaternion(new Vector3D(1, 0, 0), angle));
            //return m;

            // var radianAngle = GeometryHelper.DegreeToRadian(angle);
            double radianAngle = 0.0;
            if (handedness == Handedness3D.LeftHanded)
            {
                radianAngle = (2 * Math.PI) - GeometryHelper.DegreeToRadian(angle);
            }
            else
            {
                radianAngle = GeometryHelper.DegreeToRadian(angle);
            }
            return new Matrix3D
            {
                M22 = Math.Cos(radianAngle), M32 = -Math.Sin(radianAngle),
                M23 = Math.Sin(radianAngle), M33 = Math.Cos(radianAngle)
            };
        }

        /// <summary>
        /// Creates a 3D Y-rotation matrix.
        /// </summary>
        /// <remarks>For a left-handed 3D system (the default in Silverlight projections), a positive angle value results in a clockwise rotation around the axis. For a right-handed 3D system, a positive angle value results in a counter-clockwise rotation around the axis.</remarks>
        /// <param name="angle">Angle value in degree.</param>
        /// <param name="handedness">The handedness of the coordinate system (optional). Under Silverlight, the defaut value is Left-handed.</param>
        /// <returns>A Matrix3D object.</returns>
        public static Matrix3D GetYRotationMatrix(double angle, Handedness3D handedness = Handedness3D.LeftHanded)
        {
            //var m = new Matrix3D();
            //m.Rotate(new Quaternion(new Vector3D(0, 1, 0), angle));
            //return m;

            double radianAngle = 0.0;
            // angle inverted in respect to the relative orientation of Z to X
            if (handedness == Handedness3D.LeftHanded)
            {
                radianAngle = GeometryHelper.DegreeToRadian(angle);
            }
            else
            {
                radianAngle = (2 * Math.PI) - GeometryHelper.DegreeToRadian(angle);
            }
            return new Matrix3D
            {
                M11 = Math.Cos(radianAngle), M31 = -Math.Sin(radianAngle),
                M13 = Math.Sin(radianAngle), M33 = Math.Cos(radianAngle)
            };
        }

        /// <summary>
        /// Creates a 3D Z-rotation matrix.
        /// </summary>
        /// <remarks>For a left-handed 3D system (the default in Silverlight projections), a positive angle value results in a clockwise rotation around the axis. For a right-handed 3D system, a positive angle value results in a counter-clockwise rotation around the axis.</remarks>
        /// <param name="angle">Angle value in degree.</param>
        /// <param name="handedness">The handedness of the coordinate system (optional). Under Silverlight, the defaut value is Left-handed.</param>
        /// <returns>A Matrix3D object.</returns>
        public static Matrix3D GetZRotationMatrix(double angle, Handedness3D handedness = Handedness3D.LeftHanded)
        {
            //var m = new Matrix3D();
            //m.Rotate(new Quaternion(new Vector3D(0, 0, 1), angle));
            //return m;

            // var radianAngle = GeometryHelper.DegreeToRadian(angle);
            double radianAngle = 0.0;
            if (handedness == Handedness3D.LeftHanded)
            {
                radianAngle = (2 * Math.PI) - GeometryHelper.DegreeToRadian(angle);
            }
            else
            {
                radianAngle = GeometryHelper.DegreeToRadian(angle);
            }
            return new Matrix3D
            {
                M11 = Math.Cos(radianAngle), M21 = -Math.Sin(radianAngle),
                M12 = Math.Sin(radianAngle), M22 = Math.Cos(radianAngle)
            };
        }

        /// <summary>
        /// Creates a 3D perspective view matrix.
        /// </summary>
        /// <param name="fieldOfView">The vertical field of view.</param>
        /// <param name="imageFormat">The format of the image (ratio width / length).</param>
        /// <param name="nearPlaneDistance">Distance of the near clip plane.</param>
        /// <param name="farPlaneDistance">Distance of the far clip plane.</param>
        /// <returns></returns>
        public static Matrix3D GetPerspectiveMatrix(
            double fieldOfView,
            double imageFormat,
            double nearPlaneDistance,
            double farPlaneDistance)
        {
            var radianField = GeometryHelper.DegreeToRadian(fieldOfView);
            double scaleY = 1.0 / Math.Tan(radianField / 2.0);
            double scaleX = scaleY / imageFormat;
            double distance = nearPlaneDistance - farPlaneDistance;
            return new Matrix3D
            {
                M11 = scaleX,
                M22 = scaleY,
                M33 = farPlaneDistance / distance,
                M34 = -1,
                OffsetZ = nearPlaneDistance * farPlaneDistance / distance,
                M44 = 0
            };
        }
    }
}
