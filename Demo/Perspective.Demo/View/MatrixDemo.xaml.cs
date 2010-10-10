﻿using System;
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
using Perspective.Core.Wpf;

namespace Perspective.Demo.View
{
    public partial class MatrixDemo : Page
    {
        public MatrixDemo()
        {
            InitializeComponent();
        }

        // S'exécute lorsque l'utilisateur navigue vers cette page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private double _radius = 50.0;
        private double _initialAngle = 30.0;
        private double _angle = 15.0;
        private Point _currentPoint;
        private Ellipse _ellipse1 = new Ellipse();
        private Ellipse _ellipse2 = new Ellipse();
        private Line _line1 = new Line();
        private Line _line2 = new Line();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _ellipse1.Fill = new SolidColorBrush(Colors.Gray);
            _ellipse2.Fill = new SolidColorBrush(Colors.Orange);
            _ellipse2.Stroke = new SolidColorBrush(Colors.Orange);
            canvas.Children.Add(_line1);
            canvas.Children.Add(_line2);
            _line2.Stroke = new SolidColorBrush(Colors.Orange);
        }

        private void CheckCurrentPoint()
        {
            if ((_currentPoint.X == 0.0) && (_currentPoint.Y == 0.0))
            {
                _currentPoint = InitializePoint(_initialAngle, _radius);
            }
        }

        private void initializePointButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPoint = InitializePoint(_initialAngle, _radius);
            DisplayPoints();
        }

        private void rotatePointButton_Click(object sender, RoutedEventArgs e)
        {
            CheckCurrentPoint();
            _currentPoint = RotatePoint(_currentPoint, _angle);
            DisplayPoints();
        }

        private void rotatePointByTransformButton_Click(object sender, RoutedEventArgs e)
        {
            CheckCurrentPoint();
            _currentPoint = RotatePointByTransformation(_currentPoint, _angle);
            DisplayPoints();
        }

        private void rotatePointByMatrixButton_Click(object sender, RoutedEventArgs e)
        {
            CheckCurrentPoint();
            _currentPoint = GeometryHelper.GetRotationMatrix(_angle).Transform(_currentPoint);
            DisplayPoints();
        }

        private void matrixOperationButton_Click(object sender, RoutedEventArgs e)
        {
            CheckCurrentPoint();
            Matrix m1 = GeometryHelper.GetRotationMatrix(45.0);
            Matrix m2 = GeometryHelper.GetTranslationMatrix(50.0, 0.0);
            Matrix m3 = GeometryHelper.MultiplyMatrices(m1, m2);
            _currentPoint = m3.Transform(_currentPoint);
            DisplayPoints();
        }

        private void DisplayPoints()
        {
            double x = Canvas.GetLeft(_ellipse2);
            // if (!double.IsNaN(x))
            if (x != 0.0)
            {
                _line1.X2 = Canvas.GetLeft(_ellipse2) + _ellipse2.ActualWidth / 2.0;
                _line1.Y2 = Canvas.GetTop(_ellipse2) + _ellipse2.ActualHeight / 2.0;
                Canvas.SetLeft(_ellipse1, Canvas.GetLeft(_ellipse2));
                Canvas.SetTop(_ellipse1, Canvas.GetTop(_ellipse2));
            }
            else
            {
                canvas.Children.Add(_ellipse1);
                Canvas.SetLeft(_ellipse1, _currentPoint.X - _ellipse1.ActualWidth / 2.0);
                Canvas.SetTop(_ellipse1, _currentPoint.Y - _ellipse1.ActualHeight / 2.0);
                canvas.Children.Add(_ellipse2);
            }
            Canvas.SetLeft(_ellipse2, _currentPoint.X - _ellipse2.ActualWidth / 2.0);
            Canvas.SetTop(_ellipse2, _currentPoint.Y - _ellipse2.ActualHeight / 2.0);
            _line2.X2 = Canvas.GetLeft(_ellipse2) + _ellipse2.ActualWidth / 2.0;
            _line2.Y2 = Canvas.GetTop(_ellipse2) + _ellipse2.ActualHeight / 2.0;
        }

        private Point InitializePoint(double angle, double radius)
        {
            var radianAngle = GeometryHelper.DegreeToRadian(angle);
            return new Point(radius * Math.Cos(radianAngle), radius * Math.Sin(radianAngle));
        }

        private Point RotatePoint(Point p, double angle)
        {
            var radianAngle = GeometryHelper.DegreeToRadian(angle);
            return new Point(
                p.X * Math.Cos(radianAngle) - p.Y * Math.Sin(radianAngle),
                p.X * Math.Sin(radianAngle) + p.Y * Math.Cos(radianAngle));
        }

        private Point RotatePointByTransformation(Point p, double angle)
        {
            var rotation = new RotateTransform();
            rotation.Angle = angle;
            return rotation.Transform(p);
        }
    }
}
