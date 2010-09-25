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

namespace Perspective.Demo.View
{
    public partial class KnobDemo : Page
    {
        public KnobDemo()
        {
            InitializeComponent();
        }

        // S'exécute lorsque l'utilisateur navigue vers cette page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Knob_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbKnob.Text = String.Format("{0:0.00}", e.NewValue);
        }
    }
}
