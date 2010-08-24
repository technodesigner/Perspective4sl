//------------------------------------------------------------------
//
//  For licensing information and to get the latest version go to:
//  http://www.codeplex.com/perspective
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY
//  OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//  LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//  FITNESS FOR A PARTICULAR PURPOSE.
//
//------------------------------------------------------------------
using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Perspective.Hosting
{
    /// <summary>
    /// Represents an extension for the Perspective application.
    /// </summary>
    [InheritedExport(typeof(Extension))]
    public abstract class Extension : ItemBase
    {
        // public int SortOrder { get; set; }

        /// <summary>
        /// Property to override to get the children PageInfos collection.
        /// </summary>
        public abstract List<PageInfo> PageInfos
        {
            get;
        }
    }
}
