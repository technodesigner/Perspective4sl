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

namespace Perspective.Wpf3DX
{
    /// <summary>
    /// A class to handle the points of a triangle.
    /// </summary>
    public class Vector3Triplet
    {
        private Vector3[] _points = new Vector3[3];

        /// <summary>
        /// Gets the triangle's points collection.
        /// </summary>
        public Vector3[] Points
        {
            get { return _points; }
        }

        /// <summary>
        /// Initializes a new instance of Vector3Triplet.
        /// </summary>
        /// <param name="p1">First point.</param>
        /// <param name="p2">Second point.</param>
        /// <param name="p3">Thirst point.</param>
        public Vector3Triplet(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            _points[0] = p1;
            _points[1] = p2;
            _points[2] = p3;
        }
    }
}
