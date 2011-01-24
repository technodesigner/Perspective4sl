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
using System.IO.IsolatedStorage;
using Perspective.Config.Strings;

namespace Perspective.Config.View
{
    /// <summary>
    /// A configuration page for isolated storage.
    /// </summary>
    public partial class IsolatedStorageConfig : Page
    {
        /// <summary>
        /// Initializes a new instance of IsolatedStorageConfig.
        /// </summary>
        public IsolatedStorageConfig()
        {
            InitializeComponent();
            this.Title = StringProvider.IsolatedStorageConfig_PageTitle;
        }

        private void increaseQuotaBy1MoButton_Click(object sender, RoutedEventArgs e)
        {
            IncreaseQuotaBy(1048576);
        }

        private void increaseQuotaBy5MoButton_Click(object sender, RoutedEventArgs e)
        {
            IncreaseQuotaBy(4194304);
        }

        private void IncreaseQuotaBy(long increment)
        {
            try
            {
                IsolatedStorageFile isf =
                    IsolatedStorageFile.GetUserStoreForApplication();
                if (isf.IncreaseQuotaTo(isf.Quota + increment))
                {
                    MessageBox.Show(
                        String.Format("Nouveau quota : {0} Mo", isf.Quota / 1048576));
                }
            }
            catch (IsolatedStorageException)
            {
                MessageBox.Show("Erreur stockage d’application.");
            }
        }
    }
}
