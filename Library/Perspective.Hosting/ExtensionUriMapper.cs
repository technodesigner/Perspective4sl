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
using System.Windows.Navigation;
using System.Collections.ObjectModel;

namespace Perspective.Hosting
{
    /// <summary>
    /// A custom URI mapper for extension's page loading.
    /// Enables simplified URIs to load pages from a View folder.
    /// </summary>
    public class ExtensionUriMapper : UriMapperBase
    {
        private Collection<UriMapping> _uriMappings;

        /// <summary>
        /// Initializes a new instance of ExtensionUriMapper.
        /// </summary>
        public ExtensionUriMapper()
        {
            _uriMappings = new Collection<UriMapping>();

            // 1) Neutral mapping to preserve full base URI.
            // i.e. /Perspective.Demo;component/View/ShapeDemo.xaml
            UriMapping m1 = CreateMapping(
                "/{assembly};component/View/{pageName}.xaml", 
                "/{assembly};component/View/{pageName}.xaml");
            _uriMappings.Add(m1);

            // 2) Simple mapping mapping for arguments
            // i.e. /Perspective.Demo/ShapeDemo/Polygon/8
            UriMapping m2 = CreateMapping(
                "/{assembly}/{pageName}/{argKey}/{argValue}",
                "/{assembly};component/View/{pageName}.xaml?{argKey}={argValue}");
            _uriMappings.Add(m2);

            // 3) Simple mapping to load a page
            // i.e. /Perspective.Demo/ShapeDemo
            UriMapping m3 = CreateMapping(
                "/{assembly}/{pageName}",
                "/{assembly};component/View/{pageName}.xaml");
            _uriMappings.Add(m3);
        }

        private UriMapping CreateMapping(string uriString, string uriMappedString)
        {
            UriMapping mapping = new UriMapping();
            mapping.Uri = new Uri(uriString, UriKind.Relative);
            mapping.MappedUri = new Uri(uriMappedString, UriKind.Relative);
            return mapping;
        }

        /// <summary>
        /// Converts a uniform resource identifier (URI) into a new URI based on the rules of a matching object specified in a collection of mapping objects.
        /// </summary>
        /// <param name="uri">Original URI value to be converted to a new URI.</param>
        /// <returns>A URI to use for handling the request instead of the value of the uri parameter. If no object in the UriMappings collection matches uri, the original value for uri is returned.</returns>
        public override Uri MapUri(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }
            Uri uri2 = null;
            foreach (var mapping in _uriMappings)
            {
                uri2 = mapping.MapUri(uri);
                if (uri2 != null)
                {
                    return uri2;
                }
            }
            return uri;
        }
    }
}
