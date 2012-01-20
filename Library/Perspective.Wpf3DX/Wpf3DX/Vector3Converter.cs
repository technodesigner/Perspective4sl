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
using System.ComponentModel;
using System;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace Perspective.Wpf3DX
{
    public class Vector3Converter : TypeConverter
    {
        /// <summary>
        /// Returns whether this converter can convert the object to the specified type, using the specified context.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="sourceType">A Type that represents the type that you want to convert to.</param>
        /// <returns>A Boolean value that is true if destinationType is of type String; otherwise, false.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            return (sourceType == typeof(string));
        }

        /// <summary>
        /// Converts the given object to the type of this converter, using the specified context and culture information. 
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="culture">The CultureInfo to use as the current culture.</param>
        /// <param name="value">The Object to convert to an instance of Uri.</param>
        /// <returns>A Vector3 value that represents the converted Object.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            String sValue = (string)value;
            String[] values = sValue.Split(new Char[] { ' ', ',' });
            Vector3 v = new Vector3();
            v.X = float.Parse(values[0], CultureInfo.InvariantCulture.NumberFormat);
            v.Y = float.Parse(values[1], CultureInfo.InvariantCulture.NumberFormat);
            v.Z = float.Parse(values[2], CultureInfo.InvariantCulture.NumberFormat);
            return v;
        }
    }
}
