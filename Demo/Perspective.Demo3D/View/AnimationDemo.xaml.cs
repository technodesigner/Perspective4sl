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
using Perspective.Wpf3DX.Models;
using Perspective.Wpf3DX.Transforms;
using Perspective.Wpf3DX;

namespace Perspective.Demo3D.View
{
    public partial class AnimationDemo : Page
    {
        private Spherical _spherical;
        private Rotation _sphericalRotation = new Rotation();

        public AnimationDemo()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Spherical_Initialized(object sender, EventArgs e)
        {
            _spherical = (Spherical)sender;
            _sphericalRotation.Axis = AxisDirection.Y;
            (_spherical.Transform as ModelTransformGroup).Children.Add(_sphericalRotation);
        }

        private void Spherical_Render(object sender, Wpf3DX.RenderEventArgs e)
        {
            if (_spherical != null)
            {
                _sphericalRotation.Angle++;
                _spherical.InvalidateTransform();
            }
        }
    }
}
