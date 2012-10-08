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
using Perspective.Demo3D.Strings;

namespace Perspective.Demo3D
{
    /// <summary>
    /// Represents the Config extension for the Perspective application.
    /// </summary>
    public class Extension : Perspective.Hosting.Extension
    {
        private const string _iconFile = "Perspective.Demo3D.Icon.png";

        private static string _assemblyName = "Perspective.Demo3D";

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
        /// Not localized.
        /// </summary>
        public Extension()
            : base()
        {
            PageLinks = new List<PageLink>
            {
                new PageLink(this)
                {
                    Title = StringProvider.Extension_About,
                    IconFile = _iconFile,
                    PageName = "About"
                },
                new PageLink(this)
                {
                    Title = "XyzAxis",
                    IconFile = _iconFile,
                    PageName = "XyzAxisDemo"
                },
                new PageLink(this)
                {
                    Title = "Box",
                    IconFile = _iconFile,
                    PageName = "BoxDemo"
                },
                new PageLink(this)
                {
                    Title = "Box+Transform",
                    IconFile = _iconFile,
                    PageName = "BoxTransformDemo"
                },
                new PageLink(this)
                {
                    Title = "Box+Texture",
                    IconFile = _iconFile,
                    PageName = "BoxTextureDemo"
                },
                new PageLink(this)
                {
                    Title = "Box+Image",
                    IconFile = _iconFile,
                    PageName = "BoxImageDemo"
                },
                new PageLink(this)
                {
                    Title = "Spherical+Material",
                    IconFile = _iconFile,
                    PageName = "MaterialDemo"
                },
                new PageLink(this)
                {
                    Title = "Spherical+Animation",
                    IconFile = _iconFile,
                    PageName = "AnimationDemo"
                },
                new PageLink(this)
                {
                    Title = "Square",
                    IconFile = _iconFile,
                    PageName = "SquareDemo"
                },
                new PageLink(this)
                {
                    Title = "Polygon",
                    IconFile = _iconFile,
                    PageName = "PolygonDemo"
                },
                new PageLink(this)
                {
                    Title = "Bar",
                    IconFile = _iconFile,
                    PageName = "BarDemo"
                },
                new PageLink(this)
                {
                    Title = "Cylinder",
                    IconFile = _iconFile,
                    PageName = "CylinderDemo"
                },
                new PageLink(this)
                {
                    Title = "Conical",
                    IconFile = _iconFile,
                    PageName = "ConicalDemo"
                },
                new PageLink(this)
                {
                    Title = "Ring",
                    IconFile = _iconFile,
                    PageName = "RingDemo"
                },
                new PageLink(this)
                {
                    Title = "Dart",
                    IconFile = _iconFile,
                    PageName = "DartDemo"
                },
                new PageLink(this)
                {
                    Title = "CompositeModel",
                    IconFile = _iconFile,
                    PageName = "CompositeDemo"
                },
                new PageLink(this)
                {
                    Title = "Dynamic scene",
                    IconFile = _iconFile,
                    PageName = "DynamicDemo"
                },
            };
            ExtensionManager.Current.RegisterAssembly(_assemblyName);
        }
    }
}