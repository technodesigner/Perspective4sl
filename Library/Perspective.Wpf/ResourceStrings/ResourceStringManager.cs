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
using System.Collections.Generic;
using System.Reflection;

namespace Perspective.Wpf.ResourceStrings
{
    /// <summary>
    /// Manages string resources for control localization. 
    /// </summary>
    public class ResourceStringManager : DependencyObject
    {
        /// <summary>
        /// Identifies the Groups attached property.
        /// </summary>
        private static readonly DependencyProperty GroupsProperty = 
            DependencyProperty.RegisterAttached(
                "Groups",
                typeof(Collection<ResourceStringGroup>), 
                typeof(ResourceStringManager), 
                null);

        /// <summary>
        /// Gets the ResourceStringManager.Groups attached property.
        /// </summary>
        /// <param name="target">The element from which to get the groups.</param>
        /// <returns>The collection of ResourceStringGroup objects that is associated with the specified element.</returns>
        public static Collection<ResourceStringGroup> GetGroups(FrameworkElement target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            Collection<ResourceStringGroup> collection = target.GetValue(GroupsProperty) as Collection<ResourceStringGroup>;
            if (collection == null)
            {
                collection = new Collection<ResourceStringGroup>();
                SetGroups(target, collection);
            }
            return collection;
        }

        private static void SetGroups(FrameworkElement target, Collection<ResourceStringGroup> value)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }
            target.SetValue(GroupsProperty, value);
            target.Loaded += new RoutedEventHandler(target_Loaded);
        }

        private static void target_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement target = (sender as FrameworkElement);
            ApplyResources(target);
            target.Loaded -= new RoutedEventHandler(target_Loaded);
        }

        /// <summary>
        /// Applies the string resources to the children of a root element.
        /// </summary>
        /// <param name="rootElement">A root element.</param>
        public static void ApplyResources(FrameworkElement rootElement)
        {
            foreach (ResourceStringGroup rsg in GetGroups(rootElement))
            {
                rsg.ApplyResources(rootElement);
            }
        }

        private static Dictionary<string, Assembly> _assemblies = new Dictionary<string, Assembly>();

        /// <summary>
        /// Gets the resource assemblies collection.
        /// </summary>
        internal static Dictionary<string, Assembly> Assemblies
        {
            get { return _assemblies; }
        }

        /// <summary>
        /// Registers a resource assembly
        /// </summary>
        /// <param name="assembly"></param>
        public static void RegisterAssembly(Assembly assembly)
        {
            string key = assembly.FullName.Split(new char[] { ',' })[0];
            if (!Assemblies.ContainsKey(key))
            {
                Assemblies.Add(
                    key,
                    assembly);
            }
        }
    }
}
