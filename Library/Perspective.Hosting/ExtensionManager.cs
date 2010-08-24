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
using System.Collections.Generic;
using System.Reflection;

namespace Perspective.Hosting
{
    /// <summary>
    /// A class for extension assembly management.
    /// Indeed, Silverlight 4 and MEF don't manage the loaded extension assemblies.
    /// </summary>
    public class ExtensionManager
    {
        private static Dictionary<string, Assembly> _assemblies = new Dictionary<string, Assembly>();

        /// <summary>
        /// Gets a static dictionary of the loaded extension assemblies.
        /// </summary>
        public static Dictionary<string, Assembly> Assemblies
        {
            get
            {
                return _assemblies;
            }
        }

        /// <summary>
        /// Registers an extension assembly.
        /// This method must be called directly by each extension.
        /// </summary>
        /// <param name="assemblyName"></param>
        public static void RegisterAssembly(string assemblyName)
        {
            Assemblies.Add(assemblyName, Assembly.GetCallingAssembly());
        }
    }
}
