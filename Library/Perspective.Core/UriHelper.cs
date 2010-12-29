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

namespace Perspective.Core
{
    /// <summary>
    /// An helper class to manage URIs.
    /// </summary>
    public static class UriHelper
    {
        /// <summary>
        /// Gets the URI of a file on the server.
        /// </summary>
        /// <param name="uriString">The URI of the file relative to the ClientBin directory.</param>
        /// <returns></returns>
        public static Uri GetHostFileUri(string uriString)
        {
            var hostSourceUriString = Application.Current.Host.Source.ToString();
            hostSourceUriString = hostSourceUriString.Substring(0, hostSourceUriString.LastIndexOf('/') + 1);
            var hostSourceUri = new Uri(hostSourceUriString);

            return new Uri(hostSourceUri, uriString);
        }
    }
}