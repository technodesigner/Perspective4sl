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

namespace Perspective.Core.Wpf
{
    /// <summary>
    /// A helper class for geometry operations.
    /// </summary>
    public static class GeometryHelper
    {
        /// <summary>
        /// Convert a degree angle value in radian.
        /// </summary>
        /// <param name="degree">Angle value in degree.</param>
        /// <returns>Angle value in radian.</returns>
        public static double DegreeToRadian(double degree)
        {
            return (degree / 180.0) * Math.PI;
        }

        /// <summary>
        /// Convert a radian angle value in degree.
        /// </summary>
        /// <param name="radian">Angle value in radian.</param>
        /// <returns>Angle value in degree.</returns>
        public static double RadianToDegree(double radian)
        {
            return radian * 180.0 / Math.PI;
        }

        /// <summary>
        /// Creates a 2D rotation matrix.
        /// </summary>
        /// <param name="angle">Angle value in degree.</param>
        /// <returns>A Matrix object.</returns>
        public static Matrix GetRotationMatrix(double angle)
        {
            var radianAngle = GeometryHelper.DegreeToRadian(angle);
            return new Matrix
            {
                M11 = Math.Cos(radianAngle),
                M21 = -Math.Sin(radianAngle),
                M12 = Math.Sin(radianAngle),
                M22 = Math.Cos(radianAngle)
            };
        }


        /// <summary>
        /// Creates a 2D rotation matrix.
        /// </summary>
        /// <param name="angle">Angle value in degree.</param>
        /// <param name="centerX">The x-coordinate of the point about which to rotate this matrix.</param>
        /// <param name="centerY">The y-coordinate of the point about which to rotate this matrix.</param>
        /// <returns>A Matrix object.</returns>
        //public static Matrix GetRotationMatrix(double angle, double centerX, double centerY)
        //{
        //    Matrix m = new Matrix();
        //    m.RotateAt(angle, centerX, centerY);
        //    return m;
        //}

        /// <summary>
        /// Creates a 2D translation matrix.
        /// </summary>
        /// <param name="offsetX">The distance to translate along the x-axis.</param>
        /// <param name="offsetY">The distance to translate along the y-axis.</param>
        /// <returns>A Matrix object.</returns>
        public static Matrix GetTranslationMatrix(double offsetX, double offsetY)
        {
            return new Matrix
            {
                OffsetX = offsetX,
                OffsetY = offsetY
            };
        }

        /// <summary>
        /// Creates a 2D scaling matrix.
        /// </summary>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <returns>A Matrix object.</returns>
        public static Matrix GetScaleMatrix(double scaleFactor)
        {
            return new Matrix
            {
                M11 = scaleFactor,
                M22 = scaleFactor
            };
        }

        /// <summary>
        /// Creates a 2D scaling matrix.
        /// </summary>
        /// <param name="scaleX">The x-axis scale factor.</param>
        /// <param name="scaleY">The y-axis scale factor.</param>
        /// <returns>A Matrix object.</returns>
        public static Matrix GetScaleMatrix(double scaleX, double scaleY)
        {
            return new Matrix
            {
                M11 = scaleX,
                M22 = scaleY
            };
        }

        /// <summary>
        /// Creates a 2D scaling matrix.
        /// </summary>
        /// <param name="scaleX">The x-axis scale factor.</param>
        /// <param name="scaleY">The y-axis scale factor.</param>
        /// <param name="centerX">The x-coordinate of the scale operation's center point.</param>
        /// <param name="centerY">The y-coordinate of the scale operation's center point.</param>
        /// <returns>A Matrix object.</returns>
        //public static Matrix GetScaleMatrix(double scaleX, double scaleY, double centerX, double centerY)
        //{
        //    Matrix m = new Matrix();
        //    m.ScaleAt(scaleX, scaleY, centerX, centerY);
        //    return m;
        //}

        /// <summary>
        /// Creates a 2D skew matrix.
        /// </summary>
        /// <param name="skewX">The angle in the x dimension by which to skew this Matrix.</param>
        /// <param name="skewY">The angle in the y dimension by which to skew this Matrix.</param>
        /// <returns></returns>
        //public static Matrix GetSkewMatrix(double skewX, double skewY)
        //{
        //    Matrix m = new Matrix();
        //    m.Skew(skewX, skewY);
        //    return m;
        //}

        /// <summary>
        /// Rotates a 2D point according to a given angle.
        /// </summary>
        /// <param name="source">Source point.</param>
        /// <param name="angle">Angle value in degree.</param>
        /// <returns>A new Point3D object corresponding to the rotation.</returns>
        public static Point RotatePoint(Point source, double angle)
        {
            Matrix m = GetRotationMatrix(angle);
            return m.Transform(source);
        }

        /// <summary>
        /// Multiply 2 matrices.
        /// </summary>
        /// <param name="m1">The first matrice.</param>
        /// <param name="m2">The second matrice.</param>
        /// <returns></returns>
        public static Matrix MultiplyMatrices(Matrix m1, Matrix m2)
        {
            return new Matrix(
                (m1.M11 * m2.M11) + (m1.M12 * m2.M21),
                (m1.M11 * m2.M12) + (m1.M12 * m2.M22), 
                (m1.M21 * m2.M11) + (m1.M22 * m2.M21),
                (m1.M21 * m2.M12) + (m1.M22 * m2.M22), 
                (m1.OffsetX * m2.M11) + (m1.OffsetY * m2.M21) + m2.OffsetX, 
                (m1.OffsetX * m2.M12) + (m1.OffsetY * m2.M22) + m2.OffsetY);
        }
    }
}
