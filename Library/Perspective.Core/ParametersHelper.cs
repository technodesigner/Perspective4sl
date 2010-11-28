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
using System.Windows.Browser;

namespace Perspective.Core
{
    /// <summary>
    /// An helper class to get the Silverlight object parameters.
    /// </summary>
    public class ParametersHelper
    {
        private static IDictionary<String, String> _queryStrings = HtmlPage.Document.QueryString;

        /// <summary>
        /// Gets the dictionary filled with URI parameters.
        /// </summary>
        //private static IDictionary<String, String> QueryStrings
        //{
        //    get { return _queryStrings; }
        //}

        private static Dictionary<String, String> _initParameters = new Dictionary<String, String>();

        /// <summary>
        /// Gets or sets the dictionary filled with initialization parameters 
        /// (initParameters attribute of the HTML object element, or InitParameters property of the ASP.NET Silverlight control).
        /// </summary>
        public static IDictionary<String, String> InitParameters
        {
            get { return _initParameters; }
        }

        /// <summary>
        /// Fills the InitParameters dictionary with the initialization parameters.
        /// Should be called in the Application.Startup event.
        /// </summary>
        /// <param name="initParameters">A dictionary containing the initialization parameters.</param>
        public static void LoadInitParametersFrom(IDictionary<String, String> initParameters)
        {
            _initParameters.Clear();
            foreach (String key in initParameters.Keys)
            {
                _initParameters.Add(key, initParameters[key]);
            }
        }

        /// <summary>
        /// Reads a parameter from the initParameters attribute
        /// of the HTML object element.
        /// </summary>
        /// <param name="key">Parameter key.</param>
        /// <returns>Parameter value.</returns>
        public static String ReadParameterValue(String key)
        {
            String s = "";
            if ((_initParameters != null) && (_initParameters.ContainsKey(key)))
            {
                s = _initParameters[key];
            }
            return s;
        }

        /// <summary>
        /// Reads a parameter first from the URI parameters,
        /// and then (if not found) from the initParameters attribute 
        /// of the HTML object element.
        /// Thus, HTML hard-coded parameters may be overriden by URI parameters.
        /// </summary>
        /// <param name="key">Parameter key.</param>
        /// <returns>Parameter value.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        public static String ReadParameterValueFromUrlFirst(String key)
        {
            String s = "";
            if ((_queryStrings != null) && (_queryStrings.ContainsKey(key)))
            {
                s = _queryStrings[key];
            }
            else if ((_initParameters != null) && (_initParameters.ContainsKey(key)))
            {
                s = _initParameters[key];
            }
            return s;
        }
    }
}
