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
using System;
using System.ComponentModel;

namespace Perspective.Wpf3DX.Models
{
    /// <summary>
    /// Basic abstract class for models with an inner geometry with polygonal dimensions.  
    /// </summary>
    public abstract class PolygonalModel : Sculpture
    {
        private int _sideCount = 4;

        /// <summary>
        /// Gets or sets the model side count.
        /// Default value is 4. Minimum value is 3.
        /// </summary>
        public int SideCount 
        {
            get
            {
                return _sideCount;
            }
            set
            {
                if (value < 3)
                {
                    throw new ArgumentOutOfRangeException("SideCount");
                }
                else
                {
                    _sideCount = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the model's initial angle value (in degrees).
        /// Default value is 0.0.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float InitialAngle { get; set; }

        private float _roundingRate;

        /// <summary>
        /// Gets or sets the model's angle rounding rate.
        /// The value must be comprized between 0.0 and 0.5.
        /// Default value is 0.0.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float RoundingRate 
        {
            get
            {
                return _roundingRate;
            }
            set
            {
                if ((value < 0.0) || (value > 0.5))
                {
                    throw new ArgumentOutOfRangeException("RoundingRate");
                }
                else
                {
                    _roundingRate = value;
                }
            }
        }
    }
}
