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
using System.IO;
using System.Windows.Resources;
using System.Xml;

namespace Perspective.Hosting
{
    /// <summary>
    /// An helper class for package loading.
    /// Inspired by PackageManager (Perspective 1.0) and System.ComponentModel.Composition.Hosting.Package.
    /// </summary>
    public class XapHelper
    {
        /// <summary>
        /// Loads the assemblies of a package.
        /// </summary>
        /// <param name="packageStream">The stream of the package file (stays open at the end of the call).</param>
        /// <returns>A list of the assemblies of the package.</returns>
        public static List<Assembly> LoadPackageAssemblies(Stream packageStream)
        {
            List<Assembly> list = new List<Assembly>();
            StreamResourceInfo sriXap = new StreamResourceInfo(packageStream, null);
            List<AssemblyPart> assemblyParts = GetAssemblyParts(sriXap);
            foreach (AssemblyPart part in assemblyParts)
            {
                StreamResourceInfo sriAssembly = Application.GetResourceStream(
                    sriXap, 
                    new Uri(part.Source, UriKind.Relative));
                list.Add(part.Load(sriAssembly.Stream));
            }
            return list;
        }

        private static List<AssemblyPart> GetAssemblyParts(StreamResourceInfo sriXap)
        {
            Uri appManifestUri = new Uri("AppManifest.xaml", UriKind.Relative);
            StreamResourceInfo sriAppManifest = Application.GetResourceStream(sriXap, appManifestUri);
            List<AssemblyPart> list = new List<AssemblyPart>();
            if (sriAppManifest != null)
            {
                using (XmlReader reader = XmlReader.Create(sriAppManifest.Stream))
                {
                    while (reader.Read())
                    {
                        if ((reader.NodeType == XmlNodeType.Element)
                            && (reader.Name.Equals("AssemblyPart")))
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.Name.Equals("Source"))
                                {
                                    AssemblyPart part = new AssemblyPart();
                                    part.Source = reader.Value;
                                    list.Add(part);
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }
    }
}