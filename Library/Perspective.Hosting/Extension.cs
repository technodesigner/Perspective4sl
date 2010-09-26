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
using System.ComponentModel;

namespace Perspective.Hosting
{
    /// <summary>
    /// Represents an extension for the Perspective application.
    /// </summary>
    public abstract class Extension : INotifyPropertyChanged
    {
        /// <summary>
        /// Property to override to specify the assembly name of the inherited class (i.e. "Perspective.Config")
        /// </summary>
        /// <returns></returns>
        public abstract string AssemblyName { get; }

        private List<PageLink> _pageLinks;

        /// <summary>
        /// Gets the name of the PageLinks property.
        /// </summary>
        public const string PageLinksKey = "PageLinks";

        /// <summary>
        /// Gets the PageLink collection.
        /// </summary>
        public List<PageLink> PageLinks
        {
            get
            {
                return _pageLinks;
            }
            set
            {
                _pageLinks = value;
                NotifyPropertyChanged(PageLinksKey);
            }
        }
        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method called when a property value changes.
        /// </summary>
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}