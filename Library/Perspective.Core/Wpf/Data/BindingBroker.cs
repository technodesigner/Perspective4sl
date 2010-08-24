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
using System.ComponentModel;

namespace Perspective.Core.Wpf.Data
{
    /// <summary>
    /// A class to bind 2 visual elements in Silverlight 2
    /// (in Silverlight 3, you can use Binding.ElementName instead)
    /// </summary>
    public class BindingBroker : INotifyPropertyChanged
    {
        private int _intValue;

        /// <summary>
        /// Intermediate int value used to bind 2 visual elements.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "int")]
        public int IntValue
        {
            get { return _intValue; }
            set
            {
                _intValue = value;
                NotifyPropertyChanged("IntValue");
            }
        }

        private double _doubleValue;

        /// <summary>
        /// Intermediate double value used to bind 2 visual elements.
        /// </summary>
        public double DoubleValue
        {
            get { return _doubleValue; }
            set
            {
                _doubleValue = value;
                NotifyPropertyChanged("DoubleValue");
            }
        }

        private string _stringValue;

        /// <summary>
        /// Intermediate string value used to bind 2 visual elements.
        /// </summary>
        public string StringValue
        {
            get { return _stringValue; }
            set
            {
                _stringValue = value;
                NotifyPropertyChanged("StringValue");
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
