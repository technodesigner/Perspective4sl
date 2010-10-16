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
using Perspective.Wpf.Shapes;

namespace Perspective.Demo.View
{
    public partial class ShapeDemo : Page
    {
        public ShapeDemo()
        {
            InitializeComponent();
        }

        // S'exécute lorsque l'utilisateur navigue vers cette page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            const string polygonSideCountKey = "Polygon";
            if (this.NavigationContext.QueryString.ContainsKey(polygonSideCountKey))
            {
                int sideCount = Convert.ToInt32(this.NavigationContext.QueryString[polygonSideCountKey]);
                polygon.SideCount = sideCount;
                deepLinkingTabItem.IsSelected = true;
            }
        }

        private void bCreateShape_Click(object sender, RoutedEventArgs e)
        {
            shapeStackPanel.Children.Clear();

            TextBlock text1 = new TextBlock();
            text1.Text = "The bad way :";
            text1.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            shapeStackPanel.Children.Add(text1);

            // The bad way...
            Checkerboard checkerboard1 = new Checkerboard();
            checkerboard1.Margin = new Thickness(5.0);
            checkerboard1.Width = 400;
            checkerboard1.Height = 160;
            checkerboard1.Fill = new SolidColorBrush(Colors.Red);
            checkerboard1.Stroke = new SolidColorBrush(Colors.Blue);
            checkerboard1.StrokeThickness = 1.0;
            checkerboard1.RowCount = 4;
            checkerboard1.ColumnCount = 10;
            checkerboard1.CellLength = 40;
            shapeStackPanel.Children.Add(checkerboard1);

            TextBlock text2 = new TextBlock();
            text2.Text = "The good way :";
            text2.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            shapeStackPanel.Children.Add(text2);

            // The good way !
            Checkerboard checkerboard2 = new Checkerboard();
            checkerboard2.Margin = new Thickness(5.0);
            checkerboard2.Width = 400;
            checkerboard2.Height = 160;
            checkerboard2.Fill = new SolidColorBrush(Colors.Orange);
            checkerboard2.Stroke = new SolidColorBrush(Colors.Black);
            checkerboard2.StrokeThickness = 1.0;
            checkerboard2.BeginInit();
            try
            {
                checkerboard2.RowCount = 4;
                checkerboard2.ColumnCount = 10;
                checkerboard2.CellLength = 40;
            }
            finally
            {
                checkerboard2.EndInitAndBuildContent();
            }
            shapeStackPanel.Children.Add(checkerboard2);
        }
    }
}
