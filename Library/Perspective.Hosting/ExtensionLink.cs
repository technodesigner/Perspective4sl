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
using System.Windows.Media.Imaging;
using Perspective.Core;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.IO;
using System.Collections.ObjectModel;

namespace Perspective.Hosting
{
    /// <summary>
    /// A collection of ExtensionLink objects.
    /// </summary>
    public class ExtensionLinkCollection : Collection<ExtensionLink>
    {
    }

    /// <summary>
    /// A shortcut for extension lazy loading
    /// </summary>
    public class ExtensionLink : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the extension's package name.
        /// </summary>
        public string Package { get; set; }

        /// <summary>
        /// Gets or sets the extension's title.
        /// </summary>
        public string Title { get; set; }

        private string _iconFile;

        /// <summary>
        /// Gets or sets the extension's icon file.
        /// </summary>
        public string IconFile
        {
            get
            {
                return _iconFile;
            }

            set
            {
                if (_iconFile != value)
                {
                    _iconFile = value;
                    _image = ExtensionManager.Current.LoadImageFile(_iconFile);
                }
            }
        }

        BitmapImage _image;

        /// <summary>
        /// Gets the extension's icon.
        /// </summary>
        public BitmapImage Icon
        {
            get
            {
                return _image;
            }
        }

        private Extension _extension;

        /// <summary>
        /// Gets the name of the Extension property.
        /// </summary>
        public const string ExtensionKey = "Extension";


        /// <summary>
        /// The associated Extension object. null if not loaded.
        /// </summary>
        public Extension Extension
        {
            get
            {
                return _extension;
            }
            set
            {
                _extension = value;
                NotifyPropertyChanged(ExtensionKey);
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
