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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;
using System.Windows.Markup;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Perspective.Wpf3DX")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(Perspective.Wpf3DX.LibraryInfo.Company)]
[assembly: AssemblyProduct("Perspective.Wpf3DX")]
[assembly: AssemblyCopyright(Perspective.Wpf3DX.LibraryInfo.Copyright)]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("02aa02a8-4f53-4037-af4c-1130ac5d3b6e")]

[assembly: AssemblyVersion(Perspective.Wpf3DX.LibraryInfo.GlobalVersion)]
[assembly: AssemblyFileVersion(Perspective.Wpf3DX.LibraryInfo.GlobalVersion)]

[assembly: CLSCompliant(true)]

[assembly: XmlnsDefinition(Perspective.Wpf3DX.LibraryInfo.XmlNamespace, "Perspective.Wpf.Controls")]
[assembly: XmlnsDefinition(Perspective.Wpf3DX.LibraryInfo.XmlNamespace, "Perspective.Wpf3DX")]
[assembly: XmlnsDefinition(Perspective.Wpf3DX.LibraryInfo.XmlNamespace, "Perspective.Wpf3DX.Models")]
[assembly: XmlnsDefinition(Perspective.Wpf3DX.LibraryInfo.XmlNamespace, "Perspective.Wpf3DX.Transforms")]
[assembly: XmlnsDefinition(Perspective.Wpf3DX.LibraryInfo.XmlNamespace, "Perspective.Wpf3DX.Textures")]
[assembly: XmlnsDefinition(Perspective.Wpf3DX.LibraryInfo.XmlNamespace, "Perspective.Wpf3DX.Lights")]

namespace Perspective.Wpf3DX
{
    /// <summary>
    /// A class to handle the default assembly attribute values for Perspective.
    /// </summary>
    public sealed class LibraryInfo
    {
        private LibraryInfo() { }

        /// <summary>
        /// The default XML namespace for Perspective.
        /// </summary>
        public const string XmlNamespace = "http://www.codeplex.com/perspective";

        /// <summary>
        /// Perspective global version number.
        /// 2.0.0.1 : 2.0 alpha
        /// 2.0.0.2 : 2.0 beta
        /// 2.0.0.5 : 2.0 final
        /// 3.0.0.0 : 3.0 beta (3D only, Silverlight 5 beta)
        /// 3.0.0.1 : 3.0 final (3D only, Silverlight 5 RTM)
        /// </summary>
        public const string GlobalVersion = "3.0.0.1";

        /// <summary>
        /// Global company name.
        /// </summary>
        public const string Company = "www.odewit.net";

        /// <summary>
        /// Global copyright information.
        /// </summary>
        public const string Copyright = "Copyright © http://perspective4sl.codeplex.com 2011";
    }
}
