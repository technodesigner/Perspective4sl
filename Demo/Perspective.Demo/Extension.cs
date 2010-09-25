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
            PageInfos = new List<PageInfo>
            {
                new PageInfo(this)
                {
                    Title = "About",
                    IconFile = _iconFile,
                    PageName = "About"
                },
                new PageInfo(this)
                {
                    Title = "PolygonElement",
                    IconFile = _iconFile,
                    PageName = "PolygonElementDemo"
                },
                new PageInfo(this)
                {
                    Title = "Deep Linking",
                    IconFile = _iconFile,
                    PageName = "PolygonElementDemo/SideCount/4"
                },
                new PageInfo(this)
                {
                    Title = "BeePanel - BeeGrid",
                    IconFile = _iconFile,
                    PageName = "BeePanelDemo"
                },
                new PageInfo(this)
                {
                    Title = "Knob",
                    IconFile = _iconFile,
                    PageName = "KnobDemo"
                },
                new PageInfo(this)
                {
                    Title = "MayaEaseDemo",
                    IconFile = _iconFile,
                    PageName = "MayaEaseDemo"
                },
                new PageInfo(this)
                {
                    Title = "Matrix3DDemo",
                    IconFile = _iconFile,
                    PageName = "Matrix3DDemo"
                },
            };
            ExtensionManager.Current.RegisterAssembly(_assemblyName);
        }
    }
}