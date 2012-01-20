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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Perspective.Wpf3DX
{
    /// <summary>
    /// A Flags enumeration to identify the sides od a cube 
    /// (seen from an X, Y, Z positive position).
    /// </summary>
    [Flags()]
    public enum BoxSides
    {
        /// <summary>
        /// The front side of the cube.
        /// </summary>
        Front = 1,

        /// <summary>
        /// The back side of the cube.
        /// </summary>
        Back = 2,

        /// <summary>
        /// The left side of the cube.
        /// </summary>
        Left = 4,

        /// <summary>
        /// The right side of the cube.
        /// </summary>
        Right = 8,

        /// <summary>
        /// The upper side of the cube.
        /// </summary>
        Up = 16,

        /// <summary>
        /// The down side of the cube.
        /// </summary>
        Down = 32,

        /// <summary>
        /// All the sides of the cube.
        /// </summary>
        All = Front | Back | Left | Right | Up | Down
    }
}
