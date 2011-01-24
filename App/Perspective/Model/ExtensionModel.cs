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
using System.Collections.Generic;
using Perspective.Hosting;
using System.Windows.Media.Imaging;
using System.IO;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.Net;
using System.Reflection;
using Perspective.Core;
using System.Windows.Markup;

namespace Perspective.Model
{
    /// <summary>
    /// Manages the data that represent the extensions.
    /// </summary>
    public class ExtensionModel
    {
        /// <summary>
        /// Gets the collection of the Extensionlink objects.
        /// </summary>
        public ExtensionLinkCollection ExtensionLinks
        {
            get
            {
                return ExtensionManager.Current.ExtensionLinks;
            }
        }

        /// <summary>
        /// Initializes a new instance of MainModel.
        /// </summary>
        public ExtensionModel()
        {
            ExtensionManager.Current.ExtensionLinksLoaded +=
                (sender, e) =>
                {
                    if (_extensionLinksLoaded != null)
                    {
                        _extensionLinksLoaded(this, new EventArgs());
                    }
                };

            ExtensionManager.Current.ExtensionLoaded +=
                (sender, e) =>
                {
                    if (_extensionLoaded != null)
                    {
                        _extensionLoaded(this, new ExtensionEventArgs(e.Extension));
                    }
                };
            //#if DEBUG
            //            if (Application.Current.IsRunningOutOfBrowser)
            //            {
            //                while (!System.Diagnostics.Debugger.IsAttached)
            //                {
            //                    System.Threading.Thread.Sleep(100);
            //                }
            //            }
            //#endif
        }

        public void LoadExtensionLinks(int timeout = 10)
        {
            ExtensionManager.Current.LoadExtensionLinks(timeout);
        }

        public void CheckExtension(ExtensionLink link)
        {
            ExtensionManager.Current.CheckExtension(link);
        }

        /// <summary>
        /// Deploys all the extensions in the isolated storage.
        /// </summary>
        public void InstallExtensionFiles()
        {
            ExtensionManager.Current.Install();
        }

        /// <summary>
        /// Unistall extension files from the isolated storage.
        /// </summary>
        public void UninstallExtensionFiles()
        {
            ExtensionManager.Current.Uninstall();
        }

        private event EventHandler _extensionLinksLoaded;

        /// <summary>
        /// Event fired when the extension links are loaded.
        /// </summary>
        public event EventHandler ExtensionLinksLoaded
        {
            add
            {
                _extensionLinksLoaded += value;
            }
            remove
            {
                _extensionLinksLoaded -= value;
            }
        }

        private event EventHandler<ExtensionEventArgs> _extensionLoaded;

        /// <summary>
        /// Event fired when an extension is loaded.
        /// </summary>
        public event EventHandler<ExtensionEventArgs> ExtensionLoaded
        {
            add
            {
                _extensionLoaded += value;
            }
            remove
            {
                _extensionLoaded -= value;
            }
        }
    }
}
