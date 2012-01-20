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

namespace Perspective.Wpf3DX.Lights
{
    /// <summary>
    /// Light object that projects its effect along a direction specified by a Vector3.
    /// </summary>
    public class DirectionalLight : Light
    {
        Vector3 _direction = Vector3.Zero;

        public DirectionalLight()
        {
            Intensity = 1.0f;
        }

        /// <summary>
        /// Gets or sets a Vector3 along which the light's effect will be seen on models in a 3-D scene.
        /// </summary>
        [TypeConverter(typeof(Vector3Converter))]
        public Vector3 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
    }
}
