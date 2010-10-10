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
using Perspective.Hosting;

namespace Perspective.Demo
{
    /// <summary>
    /// Represents the Config extension for the Perspective application.
    /// </summary>
    public class Extension : Perspective.Hosting.Extension
    {
        private const string _iconFile = "Perspective.Demo.Icon.png";

        private static string _assemblyName = "Perspective.Demo";

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        public static string AssemblyNameConst
        {
            get { return Extension._assemblyName; }
        }

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        public override string AssemblyName
        {
            get
            {
                return _assemblyName;
            }
        }

        /// <summary>
        /// Initializes a new instance of Extension.
        /// </summary>
        public Extension()
            : base()
        {
            PageLinks = new List<PageLink>
            {
                new PageLink(this)
                {
                    Title = "About",
                    IconFile = _iconFile,
                    PageName = "About"
                },
                new PageLink(this)
                {
                    Title = "PolygonElement",
                    IconFile = _iconFile,
                    PageName = "PolygonElementDemo"
                },
                new PageLink(this)
                {
                    Title = "Deep Linking",
                    IconFile = _iconFile,
                    PageName = "PolygonElementDemo/SideCount/4"
                },
                new PageLink(this)
                {
                    Title = "BeePanel - BeeGrid",
                    IconFile = _iconFile,
                    PageName = "BeePanelDemo"
                },
                new PageLink(this)
                {
                    Title = "Knob",
                    IconFile = _iconFile,
                    PageName = "KnobDemo"
                },
                new PageLink(this)
                {
                    Title = "MayaEase",
                    IconFile = _iconFile,
                    PageName = "MayaEaseDemo"
                },
                new PageLink(this)
                {
                    Title = "Matrix",
                    IconFile = _iconFile,
                    PageName = "MatrixDemo"
                },
                new PageLink(this)
                {
                    Title = "Matrix3D",
                    IconFile = _iconFile,
                    PageName = "Matrix3DDemo"
                },
            };
            ExtensionManager.Current.RegisterAssembly(_assemblyName);
        }
    }
}