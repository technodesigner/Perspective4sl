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
using System.Windows.Data;

namespace Perspective.Core.Wpf.Data
{
    public class TimeSpanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string formattedTimeSpan = "{0:00}:{1:00}:{2:00}:{3:00}:{4:000}";
            string format = (String)parameter;
            TimeSpan ts = (TimeSpan)value;
            if ((ts != null) && !String.IsNullOrEmpty(format))
            {
                formattedTimeSpan = String.Format(format,
                    ts.Days, ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            }
            return formattedTimeSpan;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
