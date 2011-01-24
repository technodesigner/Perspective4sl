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
using System.Net.NetworkInformation;
using Perspective.Config.Strings;

namespace Perspective.Config.View
{
    /// <summary>
    /// A configuration page for network.
    /// </summary>
    public partial class NetworkConfig : Page
    {
        /// <summary>
        /// Initializes a new instance of NetworkConfig.
        /// </summary>
        public NetworkConfig()
        {
            InitializeComponent();
            this.Title = StringProvider.NetworkConfig_PageTitle;
        }

        // S'exécute lorsque l'utilisateur navigue vers cette page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayNetworkInfo();
            NetworkChange.NetworkAddressChanged += (sender, args) =>
                {
                    DisplayNetworkInfo();
                };
        }

        private void DisplayNetworkInfo()
        {
            bool networkAvailable = NetworkInterface.GetIsNetworkAvailable();
            networkInfoTextBlock.Text = networkAvailable ?
              "Online" :
              "Offline";
        }

    }
}
