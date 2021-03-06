﻿//------------------------------------------------------------------
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
using Perspective.Model;
using System.Collections.Generic;
using Perspective.Hosting;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Perspective.Core.Wpf.Data;

namespace Perspective.ViewModel
{
    /// <summary>
    /// Manages at the View level the data that represent the extensions.
    /// </summary>
    public class ExtensionViewModel : INotifyPropertyChanged
    {
        private ExtensionModel _extensionModel;

        /// <summary>
        /// Updates the CurrentPageLink property.
        /// </summary>
        public SignalCommand SetCurrentPageLinkCommand { get; private set; }

        /// <summary>
        /// Updates the CurrentExtension property.
        /// </summary>
        public SignalCommand SetCurrentExtensionCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of ExtensionViewModel.
        /// </summary>
        public ExtensionViewModel()
        {
            _extensionModel = (App.Current as App).ExtensionModel;
            _extensionModel.ExtensionLoaded +=
                (sender, e) =>
                {
                    CurrentExtension = e.Extension;
                };
            SetCurrentPageLinkCommand = new SignalCommand();
            SetCurrentPageLinkCommand.Executed += (sender, e) =>
            {
                if ((e.Parameter != null) && (e.Parameter is PageLink))
                {
                    CurrentPageLink = e.Parameter as PageLink;
                }
            };

            SetCurrentExtensionCommand = new SignalCommand();
            SetCurrentExtensionCommand.Executed += (sender, e) =>
            {
                if ((e.Parameter != null) && (e.Parameter is ExtensionLink))
                {
                    var link = e.Parameter as ExtensionLink;
                    CheckExtension(link);
                    if (link.Extension != null)     // may be null because of async loading (in this case, see _extensionModel.ExtensionLoaded event)
                    {
                        CurrentExtension = link.Extension;
                    }
                }
            };
        }

        /// <summary>
        /// Gets a collection of ExtensionLink objects.
        /// </summary>
        public ExtensionLinkCollection ExtensionLinks
        {
            get
            {
                return _extensionModel.ExtensionLinks;
            }
        }

        private void CheckExtension(ExtensionLink link)
        {
            _extensionModel.CheckExtension(link);
        }

        private Extension _currentExtension;

        /// <summary>
        /// Gets the name of the CurrentExtension property.
        /// </summary>
        public const string CurrentExtensionKey = "CurrentExtension";

        /// <summary>
        /// Gets the current Extension object.
        /// </summary>
        public Extension CurrentExtension
        {
            get
            {
                return _currentExtension;
            }
            private set
            {
                _currentExtension = value;
                NotifyPropertyChanged(CurrentExtensionKey);
            }
        }

        private PageLink _currentPageLink;

        /// <summary>
        /// Gets the name of the CurrentPageLink property.
        /// </summary>
        public const string CurrentPageLinkKey = "CurrentPageLink";

        /// <summary>
        /// Gets the current PageLink object.
        /// </summary>
        public PageLink CurrentPageLink
        {
            get
            {
                return _currentPageLink;
            }
            private set
            {
                _currentPageLink = value;
                NotifyPropertyChanged(CurrentPageLinkKey);
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
