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

namespace Perspective.Config.View
{
    public partial class IsolatedStorageConfig : Page
    {
        public IsolatedStorageConfig()
        {
            InitializeComponent();
        }

        // S'exécute lorsque l'utilisateur navigue vers cette page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
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
