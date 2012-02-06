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
using Perspective.Demo3D.Strings;

namespace Perspective.Demo3D.View
{
    public partial class About : Page
    {
        public About()
        {
            InitializeComponent();
            this.DataContext = new StringProvider();
            this.Title = StringProvider.About_PageTitle;
        }
    }
}
