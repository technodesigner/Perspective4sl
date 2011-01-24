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
using System.Windows.Controls.Primitives;
using Perspective.Core.Wpf.Primitives;

namespace Perspective.Wpf.Controls
{
    /// <summary>
    /// A knob (rotative button) class, compatible with range element multiselection.
    /// <see cref="RangeElementSelectionManager"/>
    /// </summary>
    [TemplatePart(Name = RotateTransformPartName, Type = typeof(RotateTransform))]
    [TemplateVisualState(Name = NormalState, GroupName = CommonStates)]
    [TemplateVisualState(Name = MouseOverState, GroupName = CommonStates)]
    [TemplateVisualState(Name = DraggingState, GroupName = CommonStates)]
    [TemplateVisualState(Name = UnselectedState, GroupName = SelectionStates)]
    [TemplateVisualState(Name = SelectedState, GroupName = SelectionStates)]
    [TemplateVisualState(Name = FocusedState, GroupName = FocusStates)]
    [TemplateVisualState(Name = UnfocusedState, GroupName = FocusStates)]
    public class Knob : RangeBase, IRangeElement
    {
        internal const string CommonStates = "CommonStates";
        internal const string SelectionStates = "SelectionStates";
        internal const string FocusStates = "FocusStates";

        internal const string NormalState = "Normal";
        internal const string MouseOverState = "MouseOver";
        internal const string DraggingState = "Dragging";
        internal const string UnselectedState = "Unselected";
        internal const string SelectedState = "Selected";
        internal const string FocusedState = "Focused";
        internal const string UnfocusedState = "Unfocused";

        /// <summary>
        /// Initializes a new instance of the Knob class.
        /// </summary>
        public Knob()
        {
            this.DefaultStyleKey = typeof(Knob);
        }

        /// <summary>
        /// Template name of the PART_RotateTransform template part.
        /// </summary>
        public const string RotateTransformPartName = "PART_RotateTransform";

        private RotateTransform _rotateTransform;

        /// <summary>
        /// Invoked when ApplyTemplate() is called.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ApplyControlTemplate();
        }

        private void ApplyControlTemplate()
        {
            _rotateTransform = base.GetTemplateChild(RotateTransformPartName) as RotateTransform;
            ApplyRotation();
            ChangeVisualState(false);
        }

        private bool _isMouseOver;

        private void ChangeVisualState(bool useTransitions)
        {
            if (IsDragging)
            {
                VisualStateManager.GoToState(this, DraggingState, useTransitions);
            }
            else if (_isMouseOver)
            {
                VisualStateManager.GoToState(this, MouseOverState, useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, NormalState, useTransitions);
            }
            VisualStateManager.GoToState(
                this,
                IsFocused ? FocusedState : UnfocusedState,
                useTransitions);
            VisualStateManager.GoToState(
                this,
                IsSelected ? SelectedState : UnselectedState,
                useTransitions);
        }

        /// <summary>
        /// Provides class handling for the MouseEnter event. 
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            _isMouseOver = true;
            ChangeVisualState(true);
        }

        /// <summary>
        /// Provides class handling for the MouseLeave event. 
        /// </summary>
        /// <param name="e">A MouseEventArgs that contains the event data.</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            _isMouseOver = false;
            ChangeVisualState(true);
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Identifies the IsFocused dependency property.
        /// </summary>
        private static readonly DependencyProperty IsFocusedProperty = 
            DependencyProperty.Register(
                "IsFocused", 
                typeof(bool), 
                typeof(Knob),
                new PropertyMetadata(IsFocusedPropertyChanged));

        /// <summary>
        /// Gets a value indicating whether the slider control has focus. 
        /// </summary>
        public bool IsFocused
        {
            get
            {
                return (bool)base.GetValue(IsFocusedProperty);
            }
            private set
            {
                base.SetValue(IsFocusedProperty, value);
            }
        }

        private static void IsFocusedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Knob).ChangeVisualState(true);
        }

        /// <summary>
        /// Provides class handling for the GotFocus event. 
        /// </summary>
        /// <param name="e">A RoutedEventArgs that contains the event data.</param>
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            IsFocused = true;
        }

        /// <summary>
        /// Provides class handling for the LostFocus event. 
        /// </summary>
        /// <param name="e">A RoutedEventArgs that contains the event data.</param>
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            IsFocused = false;
        }

        private double CurrentRate
        {
            get
            {
                return (Value - Minimum) / (Maximum - Minimum);
            }
        }

        private void ApplyRotation()
        {
            if (_rotateTransform != null)
            {
                _rotateTransform.Angle = CurrentRate * WalkAngle + ((360.0 - WalkAngle) / 2.0);
            }
        }

        /// <summary>
        /// Gets or sets the walkangle of the knob.
        /// (in french : Angle de débattement)
        /// </summary>
        public double WalkAngle
        {
            get { return (double)GetValue(WalkAngleProperty); }
            set { SetValue(WalkAngleProperty, value); }
        }

        /// <summary>
        /// Identifies the WalkAngle dependency property.
        /// </summary>
        public static readonly DependencyProperty WalkAngleProperty =
            DependencyProperty.Register(
                "WalkAngle",
                typeof(double),
                typeof(Knob),
                new PropertyMetadata(
                    300.0,
                    WalkAnglePropertyChanged));

        private static void WalkAnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Knob).ApplyRotation();
        }

        /// <summary>
        /// Called when the Maximum property changes. 
        /// </summary>
        /// <param name="oldMaximum">Old value of the Maximum property.</param>
        /// <param name="newMaximum">New value of the Maximum property.</param>
        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            this.ApplyRotation();
        }

        /// <summary>
        /// Called when the Minimum property changes. 
        /// </summary>
        /// <param name="oldMinimum">Old value of the Minimum property.</param>
        /// <param name="newMinimum">New value of the Minimum property.</param>
        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            this.ApplyRotation();
        }

        /// <summary>
        /// Called when the Value property changes. 
        /// </summary>
        /// <param name="oldValue">Old value of the Value property.</param>
        /// <param name="newValue">New value of the Value property.</param>
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            this.ApplyRotation();
        }

        /// <summary>
        /// Indicates if the knob is selected.
        /// This is a read-only dependency property.
        /// </summary>
        public bool IsSelected
        {
            get 
            { 
                return (bool)GetValue(IsSelectedProperty); 
            }
            private set 
            { 
                SetValue(IsSelectedProperty, value); 
            }
        }

        /// <summary>
        /// Identifies the IsSelected dependency property.
        /// </summary>
        private static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                "IsSelected",
                typeof(bool),
                typeof(Knob),
                new PropertyMetadata(
                    IsSelectedPropertyChanged));

        private static void IsSelectedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Knob).ChangeVisualState(true);
            RangeElementSelectionManager.IsSelectedPropertyChanged(d, e);
        }

        /// <summary>
        /// Indicates if a drag and drop operation occurs on the knob.
        /// This is a read-only dependency property.
        /// </summary>
        public bool IsDragging
        {
            get { return (bool)GetValue(IsDraggingProperty); }
            private set { SetValue(IsDraggingProperty, value); }
        }

        /// <summary>
        /// Identifies the IsDragging dependency property.
        /// </summary>
        private static readonly DependencyProperty IsDraggingProperty =
            DependencyProperty.Register(
                "IsDragging",
                typeof(bool),
                typeof(Knob),
                new PropertyMetadata(false));

        private Point _dragOriginPoint;
        private double _yOrigin;

        /// <summary>
        /// Gets or sets the mouse drag height (in physical pixels) used in calculations.
        /// Default value is 100.0.
        /// </summary>
        public double DragHeight
        {
            get 
            { 
                return (double)GetValue(DragHeightProperty); 
            }
            set 
            { 
                SetValue(DragHeightProperty, value); 
            }
        }

        /// <summary>
        /// Identifies the DragHeight dependency property.
        /// </summary>
        public static readonly DependencyProperty DragHeightProperty =
            DependencyProperty.Register(
                "DragHeight",
                typeof(double),
                typeof(Knob),
                new PropertyMetadata(100.0));


        /// <summary>
        /// Invoked when an unhandled MouseLeftButtonDown routed event is raised on this element.
        /// </summary>
        /// <param name="e">The MouseButtonEventArgs that contains the event data.</param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.Focus();
            IsFocused = true;
            if (((Keyboard.Modifiers & ModifierKeys.Control) > 0)
                ||((Keyboard.Modifiers & ModifierKeys.Apple) > 0))
            {
                IsSelected = !IsSelected;
            }
            if (!(((Keyboard.Modifiers & ModifierKeys.Control) > 0)
                    || ((Keyboard.Modifiers & ModifierKeys.Apple) > 0))
                && (!this.IsDragging))
            {
                // Memorizations must occur before CaptureAndHideMouse() call
                _dragOriginPoint = e.GetPosition(this);

                // CaptureAndHideMouse Moves the mouse
                CaptureAndHideMouse();

                IsDragging = true;
            }
            base.OnMouseLeftButtonDown(e);
        }

        /// <summary>
        /// Invoked when an unhandled MouseMove attached event reaches an element in its route that is derived from this class.
        /// </summary>
        /// <param name="e">The MouseEventArgs that contains the event data. </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.IsDragging)
            {
                UpdateValue(e.GetPosition(this));
            }
        }

        /// <summary>
        /// Provides class handling for the MouseLeftButtonUp event.
        /// </summary>
        /// <param name="e">A MouseButtonEventArgs that contains the event data.</param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (this.IsDragging)
            {
                ReleaseAndShowMouse();
                // base.ClearValue(IsDraggingProperty);
                IsDragging = false;
            }
            base.OnMouseLeftButtonUp(e);
        }

        private void CaptureAndHideMouse()
        {
            base.CaptureMouse();
            Cursor = Cursors.None;
            _yOrigin = _dragOriginPoint.Y + (DragHeight * CurrentRate);
        }

        private void ReleaseAndShowMouse()
        {
            Cursor = null;
            base.ReleaseMouseCapture();
        }

        private void UpdateValue(Point position)
        {
            double rate = (_yOrigin - position.Y) / DragHeight;
            ApplyRate(rate);
        }

        private void ApplyRate(double rate)
        {
            double previousRate = CurrentRate;
            if (IsFocused)
            {
                Value = Minimum + ((Maximum - Minimum) * rate);
            }
            if (IsSelected)
            {
                RangeElementSelectionManager.Current.ApplyRate(rate - previousRate);
            }
        }
    }
}
