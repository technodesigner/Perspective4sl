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
using System.Reflection;

namespace Perspective.Core.Wpf.Data
{
    /// <summary>
    /// An abstract class to bind strings from resx files and propagate a culture change through binding.
    /// </summary>
    public abstract class StringProviderBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires the PropertyChanged event for a property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Fires the PropertyChanged event for each static public property.
        /// </summary>
        /// <remarks>Call this method when the current culture changes. Thanks to xiaoyumu - http://forums.silverlight.net/forums/p/160527/360282.aspx </remarks>
        public void NotifyCultureChanged()
        {
            foreach (var property in GetConcreteType().GetProperties(
                BindingFlags.Static | BindingFlags.Public))
            {
                OnPropertyChanged(property.Name);
            }
        }

        /// <summary>
        /// Gets the concrete class type.
        /// </summary>
        /// <remarks>Override this method and return the type of the concrete class.</remarks>
        /// <returns>A Type object.</returns>
        protected abstract Type GetConcreteType();
    }
}
