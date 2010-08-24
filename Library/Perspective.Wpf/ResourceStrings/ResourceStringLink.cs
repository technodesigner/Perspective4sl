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
using System.ComponentModel;
using System.Windows.Data;

namespace Perspective.Wpf.ResourceStrings
{
    /// <summary>
    /// Represents the association between a framework element property and a string resource.
    /// </summary>
    public class ResourceStringLink : FrameworkElement
    {
        /// <summary>
        /// Identifies the ResourceName dependency property.
        /// </summary>
        public static readonly DependencyProperty ResourceNameProperty =
            DependencyProperty.Register(
                "ResourceName",
                typeof(string),
                typeof(ResourceStringLink),
                new PropertyMetadata(
                    ""));

        /// <summary>
        /// Gets or sets the name of string resource.
        /// </summary>
        public string ResourceName
        {
            get
            {
                return (string)GetValue(ResourceNameProperty);
            }
            set
            {
                SetValue(ResourceNameProperty, value);
            }
        }

        /// <summary>
        /// Identifies the TargetName dependency property.
        /// </summary>
        public static readonly DependencyProperty TargetNameProperty =
            DependencyProperty.Register(
                "TargetName",
                typeof(string),
                typeof(ResourceStringLink),
                new PropertyMetadata(
                    ""));

        /// <summary>
        /// Gets or sets the name of the target element.
        /// </summary>
        public string TargetName
        {
            get
            {
                return (string)GetValue(TargetNameProperty);
            }
            set
            {
                SetValue(TargetNameProperty, value);
            }
        }

        /// <summary>
        /// Identifies the TargetProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty TargetPropertyProperty =
            DependencyProperty.Register(
                "TargetProperty",
                typeof(string),
                typeof(ResourceStringLink),
                new PropertyMetadata(null));

        /// <summary>
        /// Sets or gets the name of the property of the target element.
        /// (It can't be an advanced property path syntax).
        /// </summary>
        public string TargetProperty
        {
            get
            {
                return (string)GetValue(TargetPropertyProperty);
            }
            set
            {
                SetValue(TargetPropertyProperty, value);
            }
        }

        internal void ApplyResource(System.Resources.ResourceManager rm, FrameworkElement rootElement)
        {
            // We use a binding on a private DP to set the target value
            Binding b = new Binding(TargetProperty);
            // b.Source = TargetName;
            b.Mode = BindingMode.TwoWay;
            FrameworkElement target = rootElement.FindName(TargetName) as FrameworkElement;
            b.Source = target;

            this.SetBinding(ResourceStringProperty, b);
            string value = rm.GetString(ResourceName);
            this.SetValue(ResourceStringProperty, value);
        }

        private static readonly DependencyProperty ResourceStringProperty =
            DependencyProperty.Register(
                "ResourceString",
                typeof(string),
                typeof(ResourceStringLink),
                new PropertyMetadata(null));
    }
}
