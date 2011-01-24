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
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Perspective.Demo.Strings;

namespace Perspective.Demo.View
{
    /// <summary>
    /// A page about this module.
    /// </summary>
    public partial class About : Page
    {
        /// <summary>
        /// Initializes a new instance of About.
        /// </summary>
        public About()
        {
            InitializeComponent();
            this.DataContext = new StringProvider();
            this.Title = StringProvider.About_PageTitle;
        }
    }
}
