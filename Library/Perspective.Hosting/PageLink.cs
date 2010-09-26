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
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using Perspective.Core;

namespace Perspective.Hosting
{
    /// <summary>
    /// Represents the metadata associated with a page of an extension assembly.
    /// </summary>
    public class PageLink
    {
        /// <summary>
        /// Initializes a new PageLink instance.
        /// </summary>
        /// <param name="extension">The Extension container.</param>
        public PageLink(Extension extension)
        {
            _extension = extension;
        }

        private string _title;

        /// <summary>
        /// Gets or sets the page info title.
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        private Extension _extension;

        /// <summary>
        /// Gets the extension container.
        /// </summary>
        public Extension Extension
        {
            get { return _extension; }
        }

        private Uri _uri = null;

        /// <summary>
        /// Gets the mapped relative URI of the page.
        /// </summary>
        public Uri Uri
        {
            get
            {
                if (_uri == null)
                {
                    //_uri = new System.Uri(
                    //    String.Format(@"/{0};component/{1}", _extension.AssemblyName, _partialUriString),
                    //    System.UriKind.Relative);

                    // Returns a mapped URI
                    _uri = new System.Uri(
                        String.Format(@"/{0}/{1}", _extension.AssemblyName, _pageName),
                        System.UriKind.Relative);
                }
                return _uri;
            }
        }

        private string _pageName;

        /// <summary>
        /// Gets or sets the class name of the page to load, without the .xaml extension and the complementary namespace.
        /// </summary>
        public string PageName
        {
            get
            {
                return _pageName;
            }
            set
            {
                _pageName = value;
                _uri = null;
            }
        }

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        public string AssemblyName
        {
            get
            {
                return _extension.AssemblyName;
            }
        }

        /// <summary>
        /// Gets or sets the page's icon file.
        /// </summary>
        public string IconFile { get; set; }

        public ImageSource Icon
        {
            get
            {
                //Uri uri = new Uri(String.Format(@"/{0};component/Icons/{1}", AssemblyName, IconFile), UriKind.RelativeOrAbsolute);
                //return new BitmapImage(uri);

                return ExtensionManager.Current.LoadImageFile(IconFile);
            }
        }
    }
}
