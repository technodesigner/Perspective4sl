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

namespace Perspective.Demo3D.View
{
    public partial class BoxTransformDemo : Page
    {
        public BoxTransformDemo()
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                workshop3DX.Focus();
            };
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
