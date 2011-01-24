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
using System.Windows.Markup;
using System.IO;
using Perspective.Core;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;

namespace Perspective.Hosting
{
    /// <summary>
    /// A class for extension management.
    /// </summary>
    public sealed class ExtensionManager
    {
        private static ExtensionManager _current = null;

        private static readonly Object _locker = new Object();

        private ExtensionManager()
        {
        }

        public static ExtensionManager Current
        {
            get
            {
                lock (_locker)
                {
                    if (_current == null)
                    {
                        _current = new ExtensionManager();
                    }
                    return _current;
                }
            }
        }

        private Dictionary<string, Assembly> _assemblies = new Dictionary<string, Assembly>();

        /// <summary>
        /// Gets a dictionary of the loaded extension assemblies.
        /// Indeed, Silverlight 4 and MEF don't manage the loaded extension assemblies.
        /// </summary>
        public Dictionary<string, Assembly> Assemblies
        {
            get
            {
                return _assemblies;
            }
        }

        /// <summary>
        /// Registers an extension assembly.
        /// This method must be called directly by each extension.
        /// Indeed, Silverlight 4 and MEF don't manage the loaded extension assemblies.
        /// </summary>
        /// <param name="assemblyName"></param>
        public void RegisterAssembly(string assemblyName)
        {
            Assemblies.Add(assemblyName, Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Gets the collection of the Extensionlink objects.
        /// </summary>
        public ExtensionLinkCollection ExtensionLinks { get; private set; }

        private const string _hostingConfigFileName = "Perspective.Hosting.xaml";
        private Uri _hostingConfigUri = new Uri(_hostingConfigFileName, UriKind.Relative);

        /// <summary>
        /// Loads the extension links from the Perspective.Hosting.xaml file.
        /// </summary>
        /// <param name="timeout">sets the timeout value (in seconds)</param>
        public void LoadExtensionLinks(int timeout = 10)
        {
            if (Application.Current.IsRunningOutOfBrowser)
            {
                // loads file from the isolated storage
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    // Wait until the _hostingConfigFileName exists
                    bool hostingConfigFileNameFound = false;
                    for (int check = 0; check < timeout; check++)
                    {
                        if (store.FileExists(_hostingConfigFileName))
                        {
                            hostingConfigFileNameFound = true;
                            break;
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                    if (!hostingConfigFileNameFound)
                    {
                        throw new Exception(
                            String.Format("File {0} not found in isolated storage", 
                            _hostingConfigFileName));
                    }

                    IsolatedStorageFileStream stream = null;
                    try
                    {
                        stream = new IsolatedStorageFileStream(
                            _hostingConfigFileName, FileMode.Open, store);
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            stream = null;      // prevents CA2202
                            LoadXaml(reader.ReadToEnd());
                        }
                    }
                    finally
                    {
                        if (stream != null)
                        {
                            stream.Dispose();
                        }
                    }
                }
            }
            else
            {
                // loads file from the server
                WebClient hostingConfigWebClient = new WebClient();
                hostingConfigWebClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(hostingConfigWebClient_DownloadStringCompleted);
                hostingConfigWebClient.DownloadStringAsync(_hostingConfigUri);
            }
        }

        private void hostingConfigWebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            LoadXaml(e.Result);
        }

        private void LoadXaml(string xamlString)
        {
            ExtensionLinks = (ExtensionLinkCollection)XamlReader.Load(xamlString);
            if (_extensionLinksLoaded != null)
            {
                _extensionLinksLoaded(this, new EventArgs());
            }
        }

        /// <summary>
        /// Loads an extension if not already loaded.
        /// </summary>
        /// <param name="link">The ExtensionLink object.</param>
        public void CheckExtension(ExtensionLink link, Action actionWhenExtensionLoaded = null)
        {
            if (link.Extension == null)
            {
                string xapFileName = String.Format("{0}.xap", link.Package);
                if (Application.Current.IsRunningOutOfBrowser)
                {
                    // loads package from the isolated storage
                    using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        // TODO : check for update on the server
                        using (IsolatedStorageFileStream xapStream =
                            new IsolatedStorageFileStream(
                                xapFileName, FileMode.Open, store))
                        {
                            LoadPackage(xapStream, link);
                        }
                    }
                }
                else
                {
                    // loads package from the server
                    Uri xapUri = new Uri(xapFileName, UriKind.RelativeOrAbsolute);
                    WebClient webClient = new WebClient();
                    webClient.OpenReadCompleted +=
                        (sender, e) =>
                        {
                            Stream xapStream = e.Result;
                            try
                            {
                                LoadPackage(xapStream, link, actionWhenExtensionLoaded);
                            }
                            finally
                            {
                                xapStream.Close();
                            }
                        };
                    webClient.OpenReadAsync(xapUri);
                }
            }
        }

        private void LoadPackage(Stream xapStream, ExtensionLink link, Action actionWhenExtensionLoaded = null)
        {
            string xapFileName = String.Format("{0}.xap", link.Package);
            string assemblyKey = System.IO.Path.ChangeExtension(xapFileName, null);
            List<Assembly> assemblies = XapHelper.LoadPackageAssemblies(xapStream);
            foreach (var assembly in assemblies)
            {
                var assemblyName = assembly.FullName.Substring(0, assembly.FullName.IndexOf(','));
                if (assemblyName.Equals(assemblyKey))
                {
                    var obj = assembly.CreateInstance(String.Format("{0}.Extension", assemblyKey));
                    link.Extension = obj as Extension;
                    if (_extensionLoaded != null)
                    {
                        _extensionLoaded(this, new ExtensionEventArgs(obj as Extension));
                    }
                    if (actionWhenExtensionLoaded != null)
                    {
                        actionWhenExtensionLoaded();
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Loads an icon file from the web site if online,
        /// or from isolated storage if out of browser.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public BitmapImage LoadImageFile(string fileName)
        {
            return Application.Current.IsRunningOutOfBrowser ?
                IsolatedStorageHelper.LoadImageFromUserStoreForApplication(fileName) :
                new BitmapImage(UriHelper.GetHostFileUri(fileName));
        }

        private const string _flagFileName = "ExtensionInstalled";

        /// <summary>
        /// Deploys all the extension files in the isolated storage.
        /// </summary>
        public void Install()
        {
            foreach (var link in ExtensionLinks)
            {
                var xapFileName = String.Format("{0}.xap", link.Package);
                var xapUri = new Uri(xapFileName, UriKind.RelativeOrAbsolute);
                IsolatedStorageHelper.CopyFileToUserStoreForApplicationAsync(xapUri, xapFileName);
                if (link.IconFile != null)
                {
                    IsolatedStorageHelper.CopyFileToUserStoreForApplicationAsync(
                        UriHelper.GetHostFileUri(link.IconFile),
                        link.IconFile);
                }
            }
            // Must be copied in last position (cf. LoadExtensionLinks)
            IsolatedStorageHelper.CopyFileToUserStoreForApplicationAsync(_hostingConfigUri, _hostingConfigFileName);
        }

        /// <summary>
        /// Unistall extension files from the isolated storage.
        /// </summary>
        public void Uninstall()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                // Must be deleted in first position
                if (store.FileExists(_hostingConfigFileName))
                {
                    store.DeleteFile(_hostingConfigFileName);
                }
                foreach (var link in ExtensionLinks)
                {
                    var xapFileName = String.Format("{0}.xap", link.Package);
                    if (store.FileExists(xapFileName))
                    {
                        store.DeleteFile(xapFileName);
                    }
                    if ((link.IconFile != null) && store.FileExists(link.IconFile))
                    {
                        store.DeleteFile(link.IconFile);
                    }
                }
            }
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