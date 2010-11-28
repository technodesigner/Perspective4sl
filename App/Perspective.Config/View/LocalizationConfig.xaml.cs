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
using System.Globalization;
using System.Threading;
using Perspective.Config.Strings;

namespace Perspective.Config.View
{
    public partial class LocalizationConfig : Page
    {
        private StringProvider _stringProvider = new StringProvider();
        private bool _loaded = false;

        public LocalizationConfig()
        {
            InitializeComponent();
            this.DataContext = _stringProvider;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Thread.CurrentThread.CurrentUICulture.Name == "en")
            {
                englishRadioButton.IsChecked = true;
            }
            _loaded = true;
        }

        private void englishRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SetUICulture("en");
        }

        private void frenchRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SetUICulture("fr");
        }

        private void SetUICulture(string cultureString)
        {
            if (_loaded)
            {
                var cultureInfo = new CultureInfo(cultureString);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                _stringProvider.NotifyCultureChanged();
            }
        }
    }
}
