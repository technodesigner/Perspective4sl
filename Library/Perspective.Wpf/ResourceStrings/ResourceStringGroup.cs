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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Resources;
using System.Reflection;
using System.Windows.Markup;

namespace Perspective.Wpf.ResourceStrings
{
    /// <summary>
    /// A class to handle a collection of ResourceStringLink objects.
    /// </summary>
    [ContentProperty("Links")]
    public class ResourceStringGroup : DependencyObject
    {

        /// <summary>
        /// Initializes a new instance of the ResourceStringGroup class.
        /// </summary>
        public ResourceStringGroup()
        {
            // A collection DP default value must be set in the constructor
            SetValue(LinksProperty, new Collection<ResourceStringLink>()); 
        }

        /// <summary>
        /// Gets the collection of the ResourceStringLink objects.
        /// </summary>
        public Collection<ResourceStringLink> Links
        {
            get 
            { 
                return (Collection<ResourceStringLink>)GetValue(LinksProperty); 
            }
        }

        /// <summary>
        /// Identifies the Links dependency property.
        /// </summary>
        public static readonly DependencyProperty LinksProperty =
            DependencyProperty.Register(
                "Links",
                typeof(Collection<ResourceStringLink>),
                typeof(ResourceStringGroup),
                null);


        /// <summary>
        /// Gets or sets the name of the assembly containing the BaseName resx file for localization.
        /// </summary>
        public string AssemblyName
        {
            get 
            { 
                return (string)GetValue(AssemblyNameProperty); 
            }
            set 
            { 
                SetValue(AssemblyNameProperty, value); 
            }
        }

        /// <summary>
        /// Identifies the AssemblyName dependency property.
        /// </summary>
        public static readonly DependencyProperty AssemblyNameProperty =
            DependencyProperty.Register(
                "AssemblyName",
                typeof(string),
                typeof(ResourceStringGroup),
                new PropertyMetadata(""));

        /// <summary>
        /// Gets or sets the resx file base name for localization.
        /// </summary>
        public string BaseName
        {
            get 
            { 
                return (string)GetValue(BaseNameProperty); 
            }
            set 
            { 
                SetValue(BaseNameProperty, value); 
            }
        }

        /// <summary>
        /// Identifies the BaseName dependency property.
        /// </summary>
        public static readonly DependencyProperty BaseNameProperty =
            DependencyProperty.Register(
                "BaseName",
                typeof(string),
                typeof(ResourceStringGroup),
                new PropertyMetadata(""));

        internal void ApplyResources(FrameworkElement rootElement)
        {
            string fullBaseName = AssemblyName + "." + BaseName;
            Assembly assembly = ResourceStringManager.Assemblies[AssemblyName];
            ResourceManager rm = new ResourceManager(
                fullBaseName,
                assembly);
            foreach (ResourceStringLink rsl in Links)
            {
                rsl.ApplyResource(rm, rootElement);
            }
        }
    }
}
