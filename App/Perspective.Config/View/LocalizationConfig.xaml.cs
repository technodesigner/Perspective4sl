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
using System.Globalization;
using System.Threading;
using Perspective.Config.Strings;
using System.IO.IsolatedStorage;
using Perspective.Core;

namespace Perspective.Config.View
{
    /// <summary>
    /// A configuration page for localization.
    /// </summary>
    public partial class LocalizationConfig : Page
    {
        private StringProvider _stringProvider = new StringProvider();
        private bool _loaded = false;

        /// <summary>
        /// Initializes a new instance of LocalizationConfig.
        /// </summary>
        public LocalizationConfig()
        {
            InitializeComponent();
            this.DataContext = _stringProvider;
            this.Title = StringProvider.LocalizationConfig_PageTitle;
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
                IsolatedStorageHelper.SaveCultureSetting(cultureString);
            }
        }
    }
}
