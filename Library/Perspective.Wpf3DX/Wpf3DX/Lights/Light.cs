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
using System.ComponentModel;

namespace Perspective.Wpf3DX.Lights
{
    /// <summary>
    /// Represents lighting applied to a 3-D scene.
    /// </summary>
    public abstract class Light
    {
        /// <summary>
        /// Gets or sets the light intensity factor.
        /// <remarks>Default value depends on the concrte light type.</remarks>
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float Intensity { get; set; }
    }
}
