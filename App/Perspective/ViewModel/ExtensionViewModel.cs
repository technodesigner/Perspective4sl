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
using Perspective.Model;
using System.Collections.Generic;
using Perspective.Hosting;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Perspective.Core.Wpf.Data;

namespace Perspective.ViewModel
{
    public class ExtensionViewModel : INotifyPropertyChanged
    {
        private ExtensionModel _extensionModel;

        /// <summary>
        /// Updates the CurrentPageInfo property.
        /// </summary>
        public SignalCommand SetCurrentPageInfoCommand { get; private set; }

        public ExtensionViewModel()
        {
            _extensionModel = new ExtensionModel();
            SetCurrentPageInfoCommand = new SignalCommand();
            SetCurrentPageInfoCommand.Executed += (sender, e) =>
            {
                if ((e.Parameter != null) && (e.Parameter is PageInfo))
                {
                    CurrentPageInfo = e.Parameter as PageInfo;
                }
            };
        }

        public ObservableCollection<Extension> Extensions 
        {
            get
            {
                return _extensionModel.Extensions;
            }
        }

        private PageInfo _currentPageInfo;

        /// <summary>
        /// Gets the current PageInfo object.
        /// </summary>
        public PageInfo CurrentPageInfo
        {
            get
            {
                return _currentPageInfo;
            }
            private set
            {
                _currentPageInfo = value;
                NotifyPropertyChanged("CurrentPageInfo");
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method called when a property value changes.
        /// </summary>
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
