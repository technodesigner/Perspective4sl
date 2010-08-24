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

namespace Perspective.Core.Wpf.Animation
{
    /// <summary>
    /// Represents an easing function that creates an animation that accelerates and/or decelerates using a maya pyramid-like function.
    /// </summary>
    public class MayaEase : EasingFunctionBase
    {
        /// <summary>
        /// Provides the logic portion of the easing function to produce the EaseIn mode.
        /// </summary>
        /// <param name="normalizedTime">Normalized time (progress) of the animation.</param>
        /// <returns>A double that represents the transformed progress.</returns>
        protected override double EaseInCore(double normalizedTime)
        {
            double normalizedTargetValue = 1.0;
            for (int i = 0; i < StepCount; i++)
            {
                double stepStart = (double)i / StepCount;
                double stepLength = 1.0 / StepCount;
                double thresholdStart = stepStart + Threshold * stepLength;
                double stepEnd = stepStart + stepLength;
                if ((normalizedTime >= stepStart) && (normalizedTime < thresholdStart))
                {
                    normalizedTargetValue = stepStart + (normalizedTime - stepStart) / Threshold;
                    break;
                }
                if ((normalizedTime >= thresholdStart) && (normalizedTime < stepEnd))
                {
                    normalizedTargetValue = stepEnd;
                    break;
                }
            }
            return normalizedTargetValue;
        }

        /// <summary>
        /// Identifies the StepCount dependency property.
        /// </summary>
        private static readonly DependencyProperty StepCountProperty =
            DependencyProperty.Register(
                "StepCount",
                typeof(int),
                typeof(MayaEase),
                new PropertyMetadata(4));

        /// <summary>
        /// Gets or sets the step count. 
        /// </summary>
        public int StepCount
        {
            get
            {
                return (int)base.GetValue(StepCountProperty);
            }
            set
            {
                base.SetValue(StepCountProperty, value);
            }
        }

        /// <summary>
        /// Identifies the Threshold dependency property.
        /// </summary>
        private static readonly DependencyProperty ThresholdProperty =
            DependencyProperty.Register(
                "Threshold",
                typeof(double),
                typeof(MayaEase),
                new PropertyMetadata(0.25));

        /// <summary>
        /// Gets or sets the threshold (for a step, the normalized value from which begins a flat curve).
        /// </summary>
        public double Threshold
        {
            get
            {
                return (double)base.GetValue(ThresholdProperty);
            }
            set
            {
                base.SetValue(ThresholdProperty, value);
            }
        }
    }
}
