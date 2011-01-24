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
using System.Windows;
using System.Net.NetworkInformation;
using Perspective.Config.Strings;

namespace Perspective.Config
{
    /// <summary>
    /// Represents the Config extension for the Perspective application.
    /// </summary>
    public class Extension : Perspective.Hosting.Extension
    {
        private const string _iconFile = "Perspective.Config.Icon.png";

        private static string _assemblyName = "Perspective.Config";

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        public static string AssemblyNameConst
        {
            get { return Extension._assemblyName; }
        }

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        public override string AssemblyName
        {
            get
            {
                return _assemblyName;
            }
        }

        /// <summary>
        /// Initializes a new instance of Extension.
        /// Localization of PageLinks occurs only at startup.
        /// </summary>
        public Extension()
            : base()
        {
            PageLinks = new List<PageLink>
            {
                new PageLink(this)
                {
                    // Title = "About",
                    Title = StringProvider.Extension_About,
                    IconFile = _iconFile,
                    PageName = "About"
                },
                new PageLink(this)
                {
                    Title = StringProvider.Extension_OutOfBrowser,
                    IconFile = _iconFile,
                    PageName = "OutOfBrowserConfig"
                },
                new PageLink(this)
                {
                    Title = StringProvider.Extension_Localization,
                    IconFile = _iconFile,
                    PageName = "LocalizationConfig"
                },
                new PageLink(this)
                {
                    Title = StringProvider.Extension_IsolatedStorage,
                    IconFile = _iconFile,
                    PageName = "IsolatedStorageConfig"
                },
                new PageLink(this)
                {
                    Title = StringProvider.Extension_Graphics,
                    IconFile = _iconFile,
                    PageName = "GraphicsConfig"
                },
            };
            ExtensionManager.Current.RegisterAssembly(_assemblyName);
            NetworkChange.NetworkAddressChanged += (sender1, e1) =>
            {
                if (Application.Current.IsRunningOutOfBrowser)
                {
                    var toast = new NotificationWindow();
                    var text = NetworkInterface.GetIsNetworkAvailable() ?
                        "Online" : "Offline";
                    var toastControl = new View.NotificationControl();
                    toastControl.DataContext = text;
                    toast.Content = toastControl;
                    toast.Width = 200;
                    toast.Height = 100;
                    toast.Show(8000);
                    Application.Current.MainWindow.Activate();
                }
            };

        }
    }
}