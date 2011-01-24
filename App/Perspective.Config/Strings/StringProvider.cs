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

namespace Perspective.Config.Strings
{
    /// <summary>
    /// A class to bind strings from resx files and propagate a culture change through binding.
    /// </summary>
    public class StringProvider : Perspective.Core.Wpf.Data.StringProviderBase
    {
        /// <summary>
        /// Gets the concrete class type.
        /// </summary>
        /// <returns>A Type object.</returns>
        protected override Type GetConcreteType()
        {
            return typeof(StringProvider);
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string LocalizationConfig_PageTitle
        {
            get
            {
                return LocalizationConfig.PageTitle;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string LocalizationConfig_ChooseLanguage
        {
            get
            {
                return LocalizationConfig.ChooseLanguage;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string LocalizationConfig_English
        {
            get
            {
                return LocalizationConfig.English;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string LocalizationConfig_French
        {
            get
            {
                return LocalizationConfig.French;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string Extension_About
        {
            get
            {
                return Extension.About;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string Extension_OutOfBrowser
        {
            get
            {
                return Extension.OutOfBrowser;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string Extension_Localization
        {
            get
            {
                return Extension.Localization;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string Extension_IsolatedStorage
        {
            get
            {
                return Extension.IsolatedStorage;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string Extension_Graphics
        {
            get
            {
                return Extension.Graphics;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string GraphicsConfig_PageTitle
        {
            get
            {
                return GraphicsConfig.PageTitle;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string GraphicsConfig_FullScreen
        {
            get
            {
                return GraphicsConfig.FullScreen;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string GraphicsConfig_Windowed
        {
            get
            {
                return GraphicsConfig.Windowed;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string About_PageTitle
        {
            get
            {
                return About.PageTitle;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string About_PageSubTitle2
        {
            get
            {
                return About.PageSubTitle2;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string About_perspective4sl
        {
            get
            {
                return About.perspective4sl;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string IsolatedStorageConfig_PageTitle
        {
            get
            {
                return IsolatedStorageConfig.PageTitle;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string NetworkConfig_PageTitle
        {
            get
            {
                return NetworkConfig.PageTitle;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string OutOfBrowserConfig_PageTitle
        {
            get
            {
                return OutOfBrowserConfig.PageTitle;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string OutOfBrowserConfig_Install
        {
            get
            {
                return OutOfBrowserConfig.Install;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string OutOfBrowserConfig_Update
        {
            get
            {
                return OutOfBrowserConfig.Update;
            }
        }
    }
}
