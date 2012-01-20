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

namespace Perspective.Wpf3DX
{
    /// <summary>
    /// Represents a perspective projection camera. 
    /// </summary>
    public class PerspectiveCamera
    {
        Vector3 _position;

        /// <summary>
        /// Gets or sets the position of the camera in world coordinates.
        /// </summary>
        [TypeConverter(typeof(Vector3Converter))]
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        Vector3 _lookTarget;

        /// <summary>
        /// Gets a Vector3 which defines the the target point at which the camera is looking in world coordinates.
        /// </summary>
        [TypeConverter(typeof(Vector3Converter))]
        public Vector3 LookTarget
        {
            get { return _lookTarget; }
            set { _lookTarget = value; }
        }

        Vector3 _rightDirection = Vector3.UnitX;

        Vector3 _upDirection = Vector3.Up;

        /// <summary>
        /// Gets or sets a Vector3 which defines the upward direction of the camera. 
        /// </summary>
        [TypeConverter(typeof(Vector3Converter))]
        public Vector3 UpDirection
        {
            get { return _upDirection; }
            set { _upDirection = value; }
        }

        /// <summary>
        /// Initializes a new instance of the PerspectiveCamera class.
        /// </summary>
        public PerspectiveCamera()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes default camera values.
        /// </summary>
        private void Initialize()
        {
            // _position = new Vector3(0.0f, 0.5f, 10.0f);
            _position = new Vector3(10.0f, 3.0f, 10.0f);
            // _lookDirection = new Vector3(0.0f, 0.0f, 0.0f);
            _lookTarget = new Vector3(0.0f, 0.0f, 0.0f);
            _rightDirection = Vector3.UnitX;
            _fieldOfView = 50.0f;
            _upDirection = Vector3.Up;
            InvalidateView();
            InvalidateProjection();
        }

        /// <summary>
        /// Orbits the target point.
        /// </summary>
        /// <remarks>When orbit kind is Vertical, the view may change in the drawing surface because of the Camera.UpDirection value.</remarks>
        /// <param name="angleOffset">Rotation angle (in degrees).</param>
        /// <param name="ad">An AxisDirection enum value.</param>
        public void OrbitTarget(float angleOffset, AxisDirection ad)
        {
            float radAngleOffset = MathHelper.TwoPi - MathHelper.ToRadians(angleOffset);
            Matrix m1 = Matrix.Identity;
            switch (ad)
            {
                case AxisDirection.X:
                    m1 = Matrix.CreateRotationX(radAngleOffset);
                    break;
                case AxisDirection.Y:
                    m1 = Matrix.CreateRotationY(radAngleOffset);
                    break;
                case AxisDirection.Z:
                    m1 = Matrix.CreateRotationZ(radAngleOffset); ;
                    break;
            }

            _position = Vector3.Transform(_position, m1);
            InvalidateView();
        }

        /// <summary>
        /// Moves the camera.
        /// </summary>
        /// <param name="offset">Vector3 that represents the offset.</param>
        public void Move(Vector3 offset)
        {
            _position += _rightDirection * offset.X;
            _position += _upDirection * offset.Y;
            _position.Z += offset.Z;
            InvalidateView();
        }

        /// <summary>
        /// Orients the camera.
        /// <remarks>This changes the LookDirection property.</remarks>
        /// </summary>
        /// <param name="angleOffset">Rotation angle (in degrees).</param>
        /// <param name="ad">An AxisDirection enum value.</param>
        public void Orient(float angleOffset, AxisDirection ad)
        {
            float radAngleOffset = MathHelper.TwoPi - MathHelper.ToRadians(angleOffset);
            Matrix m = Matrix.Identity;
            switch (ad)
            {
                case AxisDirection.X:
                    m = Matrix.CreateFromAxisAngle(Vector3.UnitX, radAngleOffset);
                    break;
                case AxisDirection.Y:
                    m = Matrix.CreateFromAxisAngle(Vector3.UnitY, radAngleOffset);
                    break;
                case AxisDirection.Z:
                    m = Matrix.CreateFromAxisAngle(Vector3.UnitZ, radAngleOffset);
                    break;
            }
            Vector3 orient = _position - _lookTarget;
            orient = Vector3.Transform(orient, m);
            _lookTarget = _position - orient;
            InvalidateView();
        }

        /// <summary>
        /// Increments the zoom factor.
        /// </summary>
        /// <remarks>This changes the FieldOfView property.</remarks>
        /// <param name="zoomIncrement">The increment step.</param>
        public void Zoom(float zoomIncrement)
        {
            FieldOfView -= zoomIncrement;
            InvalidateProjection();
        }

        /// <summary>
        /// Refreshes the view matrix of the camera.
        /// </summary>
        public void InvalidateView()
        {
            _view = Matrix.CreateLookAt(_position, _lookTarget, _upDirection);
            _viewProjection = _view * _projection;
        }

        Matrix _view;

        /// <summary>
        /// Gets the view matrix.
        /// </summary>
        public Matrix View
        {
            get
            {
                return _view;
            }
        }


        float _fieldOfView;

        /// <summary>
        /// Gets or sets a value that represents the camera's field of view in the y direction, in degrees. 
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float FieldOfView
        {
            get { return _fieldOfView; }
            set 
            {
                if (value <= 0.0f)
                {
                    _fieldOfView = 0.01f;
                }
                else if (value >= 180.0f)
                {
                    _fieldOfView = 179.09f;
                }
                else
                {
                    _fieldOfView = value;
                }
            }
        }

        float _aspectRatio = 1.6f;

        /// <summary>
        /// Gets or sets the aspect ratio, defined as view space width divided by height.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float AspectRatio
        {
            get { return _aspectRatio; }
            set { _aspectRatio = value; }
        }

        float _nearPlaneDistance = 1.0f;

        /// <summary>
        /// Gets or sets a value that specifies the distance from the camera of the camera's near clip plane.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float NearPlaneDistance
        {
            get { return _nearPlaneDistance; }
            set 
            {
                if (value <= 0.0f)
                {
                    _nearPlaneDistance = 0.01f;
                }
                else
                {
                    _nearPlaneDistance = value;
                }
            }
        }

        float _farPlaneDistance = 100.0f;

        /// <summary>
        /// Gets or sets a value that specifies the distance from the camera of the camera's far clip plane.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float FarPlaneDistance
        {
            get { return _farPlaneDistance; }
            set 
            {
                if (value <= 0.0f)
                {
                    _farPlaneDistance = 0.01f;
                }
                else
                {
                    _farPlaneDistance = value;
                } 
            }
        }

        /// <summary>
        /// Refreshes the perspective projection.
        /// </summary>
        public void InvalidateProjection()
        {
            float radFieldOfView = MathHelper.ToRadians(_fieldOfView);
            _projection = Matrix.CreatePerspectiveFieldOfView(radFieldOfView, _aspectRatio, _nearPlaneDistance, _farPlaneDistance);
            _viewProjection = _view * _projection;
        }

        Matrix _projection;

        /// <summary>
        /// Gets the projection matrix.
        /// </summary>
        public Matrix Projection
        {
            get { return _projection; }
        }

        Matrix _viewProjection;

        /// <summary>
        /// Gets the matrix resulting of the combination of the view and the projection.
        /// </summary>
        public Matrix ViewProjection
        {
            get { return _viewProjection; }
        }
    }
}
