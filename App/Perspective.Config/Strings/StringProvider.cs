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
using System.ComponentModel;
using System.Reflection;
using System.Globalization;

namespace Perspective.Config.Strings
{
    public class StringProvider : INotifyPropertyChanged
    {
        private static LocalizationConfig _localizationConfig;

        static StringProvider()
        {
            _localizationConfig = new LocalizationConfig();
        }

        /// <summary>
        ///   Recherche une chaîne localisée semblable à Please choose the current language.
        /// </summary>
        public static string LocalizationConfig_ChooseLanguage
        {
            get
            {
                return LocalizationConfig.ChooseLanguage;
            }
        }

        /// <summary>
        ///   Recherche une chaîne localisée semblable à English.
        /// </summary>
        public static string LocalizationConfig_English
        {
            get
            {
                return LocalizationConfig.English;
            }
        }

        /// <summary>
        ///   Recherche une chaîne localisée semblable à French.
        /// </summary>
        public static string LocalizationConfig_French
        {
            get
            {
                return LocalizationConfig.French;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Thanks to xiaoyumu
        // http://forums.silverlight.net/forums/p/160527/360282.aspx
        public void NotifyCultureChanged()
        {
            foreach (var property in (typeof(StringProvider)).GetProperties(
                BindingFlags.Static | BindingFlags.Public))
            {
                OnPropertyChanged(property.Name);
            }
        }
    }
}
