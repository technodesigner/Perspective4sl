﻿//------------------------------------------------------------------
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
    /// <summary>
    /// A demo page for Knob.
    /// </summary>
    public partial class KnobDemo : Page
    {
        /// <summary>
        /// Initializes a new instance of KnobDemo.
        /// </summary>
        public KnobDemo()
        {
            InitializeComponent();
        }

        private void Knob_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbKnob.Text = String.Format("{0:0.00}", e.NewValue);
        }
    }
}
