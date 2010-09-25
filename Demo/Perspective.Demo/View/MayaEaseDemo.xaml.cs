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
using Perspective.Core.Wpf.Animation;

namespace Perspective.Demo.View
{
    public partial class MayaEaseDemo : Page
    {
        public MayaEaseDemo()
        {
            InitializeComponent();
        }

        // S'exécute lorsque l'utilisateur navigue vers cette page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        Path _curvePath;

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            _curvePath = new Path();
            _curvePath.Stroke = new SolidColorBrush(Colors.Black);
            _curvePath.StrokeThickness = 0.8;
            canvas.Children.Add(_curvePath);
        }
        private void DrawEaseCurvePath(EasingFunctionBase ease)
        {
            PathGeometry geometry = new PathGeometry();
            _curvePath.Data = geometry;
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(0.0, 100.0);
            geometry.Figures.Add(figure);
            double resolution = 2000;
            for (int i = 0; i <= resolution; i++)
            {
                double d = ease.Ease(i / resolution);
                LineSegment segment = new LineSegment();
                segment.Point = new Point(i * 100 / resolution, 100.0 - (d * 100.0));
                figure.Segments.Add(segment);
            }
        }

        private void StartAnimation(EasingFunctionBase ease)
        {
            storyboard.Stop();
            animation1.EasingFunction = ease;
            animation2.EasingFunction = ease;
            animation3.EasingFunction = ease;
            animation4.EasingFunction = ease;
            storyboard.Begin();
        }
        private void bMaya_Click(object sender, RoutedEventArgs e)
        {
            MayaEase ease = new MayaEase();
            ease.EasingMode = EasingMode.EaseIn;
            DrawEaseCurvePath(ease);
            StartAnimation(ease);
        }
        private void bMaya2_Click(object sender, RoutedEventArgs e)
        {
            MayaEase ease = new MayaEase();
            ease.EasingMode = EasingMode.EaseIn;
            ease.Threshold = 0.5;
            DrawEaseCurvePath(ease);
            StartAnimation(ease);
        }
        private void bMaya3_Click(object sender, RoutedEventArgs e)
        {
            MayaEase ease = new MayaEase();
            ease.EasingMode = EasingMode.EaseOut;
            DrawEaseCurvePath(ease);
            StartAnimation(ease);
        }
        private void bMaya4_Click(object sender, RoutedEventArgs e)
        {
            MayaEase ease = new MayaEase();
            ease.EasingMode = EasingMode.EaseInOut;
            DrawEaseCurvePath(ease);
            StartAnimation(ease);
        }
    }
}
