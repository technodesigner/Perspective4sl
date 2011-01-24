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

namespace Perspective.Demo.Strings
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
        public static string About_PageSubTitle
        {
            get
            {
                return About.PageSubTitle;
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
        public static string ShapeDemo_Dynamic
        {
            get
            {
                return ShapeDemo.Dynamic;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string ShapeDemo_Create
        {
            get
            {
                return ShapeDemo.Create;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string ShapeDemo_TheBadWay
        {
            get
            {
                return ShapeDemo.TheBadWay;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string ShapeDemo_TheGoodWay
        {
            get
            {
                return ShapeDemo.TheGoodWay;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string MayaEaseDemo_Time
        {
            get
            {
                return MayaEaseDemo.Time;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string MayaEaseDemo_Value
        {
            get
            {
                return MayaEaseDemo.Value;
            }
        }

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        public static string MatrixDemo_MatrixOperations
        {
            get
            {
                return MatrixDemo.MatrixOperations;
            }
        }
    }
}
