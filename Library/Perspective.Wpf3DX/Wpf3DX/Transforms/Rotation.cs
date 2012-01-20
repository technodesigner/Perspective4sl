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
using Microsoft.Xna.Framework;
using System.ComponentModel;

namespace Perspective.Wpf3DX.Transforms
{
    /// <summary>
    /// Specifies a rotation transformation. 
    /// </summary>
    public class Rotation : ModelTransform
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public Rotation()
        {
            // The default direction is Counterclockwise by compatibility with WPF RotateTransform3D and general trigonometry.
            Direction = RotationDirection.CounterClockwise;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="angle">The rotation angle, in degrees.</param>
        /// <param name="axis">The axis of the rotation.</param>
        public Rotation(float angle, AxisDirection axis) : this()
        {
            Angle = angle;
            Axis = axis;
        }

        /// <summary>
        /// Gets or sets the axis of the rotation.
        /// </summary>
        public AxisDirection Axis { get; set; }

        /// <summary>
        /// Gets or sets the rotation angle, in degrees.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float Angle { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate of the point about which to rotate.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float CenterX { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate of the point about which to rotate.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float CenterY { get; set; }

        /// <summary>
        /// Gets or sets the Z coordinate of the point about which to rotate.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float CenterZ { get; set; }

        /// <summary>
        /// Gets or sets the rotation direction.
        /// The default value is Counterclockwise.
        /// </summary>
        public RotationDirection Direction { get; set; }

        /// <summary>
        /// Gets the transformation matrix.
        /// </summary>
        /// <returns>A matrix object.</returns>
        protected override Matrix GetMatrix()
        {
            Matrix matrix = Matrix.CreateTranslation(-CenterX, -CenterY, -CenterZ);
            Vector3 axis;
            switch (Axis)
            {
                case AxisDirection.X :
                    axis = Vector3.UnitX; 
                    break;
                case AxisDirection.Y :
                    axis = Vector3.UnitY;
                    break;
                default :
                    axis = Vector3.UnitZ;
                    break;           
            }
            matrix *= Matrix.CreateFromAxisAngle(axis, MathHelper.ToRadians(Angle) * (int)Direction);
            matrix *= Matrix.CreateTranslation(CenterX, CenterY, CenterZ);
            return matrix;
        }
    }
}
