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
using Perspective.Wpf3DX.Sculptors;

namespace Perspective.Wpf3DX.Models
{
    /// <summary>
    /// A 3D spherical element.
    /// Default radius is 1.0.
    /// </summary>
    public class Spherical : Sculpture
    {
        /// <summary>
        /// Gets or sets the side count for half of the circumference.
        /// Default value is 20.
        /// Meridian count is twice this value.
        /// </summary>
        public int ParallelCount { get; set; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public Spherical()
        {
            ParallelCount = 20;
        }

        /// <summary>
        /// Initializes a new specific Sculptor instance.
        /// </summary>
        /// <returns>A SphericalSculptor object.</returns>
        protected override Sculptor CreateSculptor()
        {
            return new SphericalSculptor(ParallelCount);
        }
    }
}
