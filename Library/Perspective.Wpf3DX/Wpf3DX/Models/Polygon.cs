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
    /// A 3D flat polygon element.
    /// Default radius is 1.0.
    /// </summary>
    public class Polygon : PolygonalModel
    {
        /// <summary>
        /// Initializes a new specific Sculptor instance.
        /// </summary>
        /// <returns>A PolygonSculptor object.</returns>
        protected override Sculptor CreateSculptor()
        {
            var sculptor = new PolygonSculptor();
            sculptor.Initialize(SideCount, InitialAngle, RoundingRate);
            return sculptor;
        }
    }
}
