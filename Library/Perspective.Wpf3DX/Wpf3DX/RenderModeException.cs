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
using System.Windows.Graphics;

namespace Perspective.Wpf3DX
{
    /// <summary>
    /// The exception that is thrown when there the 3D rendering is not available.
    /// </summary>
    public class RenderModeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the RenderModeException class with a specified reason.
        /// </summary>
        /// <param name="reason">The reason why the 3D rendering is not available.</param>
        public RenderModeException(RenderModeReason reason) :
            base(String.Format("RenderModeException : {0}", reason.ToString()))
        {
            this.Reason = reason;
        }

        /// <summary>
        /// Gets the reason of the exception.
        /// </summary>
        public RenderModeReason Reason { get; private set; }
    }
}
