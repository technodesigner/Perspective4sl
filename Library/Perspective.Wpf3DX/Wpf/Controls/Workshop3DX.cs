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
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using Perspective.Wpf3DX;
using System.Windows.Markup;
using System.Windows.Graphics;

namespace Perspective.Wpf.Controls
{
    /// <summary>
    /// A ready to use viewport for 3D models, with light and moveable camera.
    /// The camera can be controlled by mouse (with joysticks) or by keyboard (when the control is focused) :
    /// - Plus and Minus keys act on the zoom factor.
    /// - Numeric keypad arrow key act on the camera position (on a XZ plane)
    /// - When the Ctrl key is pressed, they move the orientation of the camera according to a vertical plane (Up and Down arrows) or horizontal plane (Left and Right arrows). 
    /// - When the Shift key is pressed, they turn the camera around the origin according to a vertical plane (Up and Down arrows or position joystick's buttons) or horizontal plane (Left and Right arrows or position joystick's buttons). 
    /// - The key 5 or Ctrl-Plus of the numeric keypad elevate the position of the camera. Keys Ctrl-5 or Ctrl-Minus reduce the height of the camera position.
    /// </summary>
    [ContentProperty("Scene")]
    public class Workshop3DX : ContentControl
    {
        private readonly Grid _grid = new Grid();
        private readonly DrawingSurface _drawingSurface = new DrawingSurface();
        private Scene _scene = new Scene();

        /// <summary>
        /// Gets the 3D scene
        /// </summary>
        public Scene Scene
        {
            get { return _scene; }
            set { _scene = value;  }
        }

        /// <summary>
        /// Initializes a new Workshop3DX instance.
        /// </summary>
        public Workshop3DX()
        {
            _drawingSurface.Draw += new EventHandler<DrawEventArgs>(_drawingSurface_Draw);
            _drawingSurface.SizeChanged += new SizeChangedEventHandler(_drawingSurface_SizeChanged);
            this.Content = _grid;
            _grid.Children.Add(_drawingSurface);
            this.Loaded += new RoutedEventHandler(Workshop3DX_Loaded);
        }

        private void Workshop3DX_Loaded(object sender, RoutedEventArgs e)
        {
            _scene.Initialize();
        }

        private void _drawingSurface_Draw(object sender, DrawEventArgs e)
        {
            _scene.Draw(Helper3D.GraphicsDevice);

            // invalidates to get a callback next frame
            e.InvalidateSurface();
        }

        private void _drawingSurface_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //DrawingSurface surface = sender as DrawingSurface;
            //_scene.InvalidateProjection((float)surface.ActualWidth / (float)surface.ActualHeight);
            InvalidateProjection();
        }

        /// <summary>
        /// Refreshes the perspective projection.
        /// </summary>
        public void InvalidateProjection()
        {
            _scene.InvalidateProjection((float)_drawingSurface.ActualWidth / (float)_drawingSurface.ActualHeight);
        }


        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect rViewport = new Rect(0, 0, finalSize.Width, finalSize.Height);
            _grid.Arrange(rViewport);
            return finalSize;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            float orbitStepAngle = 15.0f;
            float orientationStepAngle = 1.0f;
            float zoomStep = 1.0f;
            // this prevents focus changing
            if ((e.Key == Key.Up)
                || (e.Key == Key.Down)
                || (e.Key == Key.Left)
                || (e.Key == Key.Right)
                )
            {
                e.Handled = true;
            }
            switch (e.Key)
            {
                case Key.NumPad2:
                case Key.Down:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        _scene.Camera.Orient(-orientationStepAngle, AxisDirection.X);
                    }
                    else if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        _scene.Camera.Move(Vector3.Backward);
                    }
                    else if (Keyboard.Modifiers == (ModifierKeys.Shift))
                    {
                        _scene.Camera.OrbitTarget(-orbitStepAngle, AxisDirection.X);
                    }
                    break;

                case Key.NumPad4:
                case Key.Left:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        _scene.Camera.Orient(orientationStepAngle, AxisDirection.Y);
                    }
                    else if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        _scene.Camera.Move(Vector3.Left);
                    }
                    else if (Keyboard.Modifiers == (ModifierKeys.Shift))
                    {
                        _scene.Camera.OrbitTarget(orbitStepAngle, AxisDirection.Y);
                    }
                    break;

                case Key.NumPad6:
                case Key.Right:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        _scene.Camera.Orient(-orientationStepAngle, AxisDirection.Y);
                    }
                    else if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        _scene.Camera.Move(Vector3.Right);
                    }
                    else if (Keyboard.Modifiers == (ModifierKeys.Shift))
                    {
                        _scene.Camera.OrbitTarget(-orbitStepAngle, AxisDirection.Y);
                    }
                    break;

                case Key.Up:
                case Key.NumPad8:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        _scene.Camera.Orient(orientationStepAngle, AxisDirection.X);
                    }
                    else if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        _scene.Camera.Move(Vector3.Forward);
                    }
                    else if (Keyboard.Modifiers == (ModifierKeys.Shift))
                    {
                        _scene.Camera.OrbitTarget(orbitStepAngle, AxisDirection.X);
                    }
                    break;

                case Key.Add:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        _scene.Camera.Move(Vector3.Up);
                    }
                    else if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        _scene.Camera.Zoom(zoomStep);
                    }
                    break;

                case Key.Subtract:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        _scene.Camera.Move(Vector3.Down);
                    }
                    else if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        _scene.Camera.Zoom(-zoomStep);
                    }
                    break;

                case Key.NumPad5:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        _scene.Camera.Move(Vector3.Down);
                    }
                    else
                    {
                        _scene.Camera.Move(Vector3.Up);
                    }
                    break;

                case Key.Unknown :  // Mac keyboard
                    switch (e.PlatformKeyCode)
                    {
                        case 187 :  // -
                            if (Keyboard.Modifiers == ModifierKeys.Control)
                            {
                                _scene.Camera.Move(Vector3.Down);
                            }
                            else if (Keyboard.Modifiers == ModifierKeys.None)
                            {
                                _scene.Camera.Zoom(-zoomStep);
                            }
                            break;
                        case 223:  // +
                            if (Keyboard.Modifiers == ModifierKeys.Control)
                            {
                                _scene.Camera.Move(Vector3.Up);
                            }
                            else if (Keyboard.Modifiers == ModifierKeys.None)
                            {
                                _scene.Camera.Zoom(zoomStep);
                            }
                            break;
                    }
                    break;
            }
            base.OnKeyDown(e);
        }
    }
}
