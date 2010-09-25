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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Perspective.Hosting
{
    /// <summary>
    /// Base class for extension elements with name and icon
    /// </summary>
    public abstract class ItemBase
    {
        /// <summary>
        /// Property to override to specify the assembly name of the inherited class (i.e. "Perspective.Config")
        /// </summary>
        /// <returns></returns>
        public abstract string AssemblyName { get; }
    }
}
