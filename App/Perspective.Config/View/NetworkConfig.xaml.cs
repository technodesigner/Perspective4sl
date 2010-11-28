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

namespace Perspective.Config.View
{
    public partial class NetworkConfig : Page
    {
        public NetworkConfig()
        {
            InitializeComponent();
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
