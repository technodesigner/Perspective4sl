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
using System.ComponentModel.Composition;

namespace Perspective.Config
{
    /// <summary>
    /// Represents the Config extension for the Perspective application.
    /// </summary>
    // [Export(typeof(Perspective.Hosting.Extension))]
    public class Extension : Perspective.Hosting.Extension
    {
        private const string _title = "Config";
        private const string _iconKey = "Perspective.Config.Icon.png";

        private static string _assemblyName = "Perspective.Config";

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

        private List<PageInfo> _pageInfos;

        /// <summary>
        /// Gets the children PageInfos collection.
        /// </summary>
        public override List<PageInfo> PageInfos
        {
            get
            {
                return _pageInfos;
            }
        }

        /// <summary>
        /// Initializes a new instance of Extension.
        /// </summary>
        public Extension()
            : base()
        {
            Title = _title;
            IconKey = _iconKey;
            // SortOrder = 90;

            _pageInfos = new List<PageInfo>
            {
                new PageInfo(this)
                {
                    Title = "About",
                    IconKey = _iconKey,
                    PartialClassName = "View/About.xaml"
                },
            };
            ExtensionManager.RegisterAssembly(_assemblyName);
        }
    }
}