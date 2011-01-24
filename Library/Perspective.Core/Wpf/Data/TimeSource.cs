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
using System.Windows.Threading;
using System.ComponentModel;

namespace Perspective.Core.Wpf.Data
{
    /// <summary>
    /// A time data source.
    /// </summary>
    public class TimeSource : INotifyPropertyChanged
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        private DateTime _currentTime;

        /// <summary>
        /// Initializes a new instance of TimeSource.
        /// </summary>
        public TimeSource()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            _currentTime = DateTime.Now;
            NotifyPropertyChanged("CurrentTime");
            NotifyPropertyChanged("CurrentTimeString");
        }

        /// <summary>
        /// Gets the current time.
        /// </summary>
        public DateTime CurrentTime 
        {
            get
            {
                return _currentTime;
            }
        }


        /// <summary>
        /// Gets the current time as a string.
        /// </summary>
        public String CurrentTimeString
        {
            get
            {
                return _currentTime.ToLongTimeString();
            }
        }

        #region INotifyPropertyChanged Membres

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
