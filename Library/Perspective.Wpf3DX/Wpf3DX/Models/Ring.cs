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
    public class Ring : PolygonalModel
    {
        /// <summary>
        /// Initializes a new instance of Ring.
        /// </summary>
        public Ring()
        {
            Radius = 10.0f;
            SegmentCount = 40;
        }

        /// <summary>
        /// Gets or sets the ring radius.
        /// Default value is 10.0.
        /// </summary>
        [TypeConverter(typeof(FloatConverter))]
        public float Radius { get; set; }

        private int _segmentCount;

        /// <summary>
        /// Gets or sets the ring bar segment count.
        /// Default value is 40. Minimum value is 3.
        /// </summary>
        public int SegmentCount 
        {
            get
            {
                return _segmentCount;
            }
            set
            {
                if (value < 3)
                {
                    throw new ArgumentOutOfRangeException("SegmentCount");
                }
                else
                {
                    _segmentCount = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new specific Sculptor instance.
        /// </summary>
        /// <returns>A RingSculptor object.</returns>
        protected override Sculptor CreateSculptor()
        {
            return new RingSculptor(Radius, SegmentCount, SideCount, InitialAngle, RoundingRate);
        }
    }
}
