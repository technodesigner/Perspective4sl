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
using System.Windows.Threading;
using Perspective.Core.Wpf.Data;

namespace Perspective.Core.Wpf.Data
{
    /// <summary>
    /// A chronometer data source.
    /// </summary>
    public class ChronometerSource : DependencyObject
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        private DateTime _dtStart;
        private bool _timerRunning = false;

        /// <summary>
        /// A command to start the chronometer.
        /// </summary>
        public SignalCommand StartCommand { get; private set; }

        /// <summary>
        /// A command to stop the chronometer.
        /// </summary>
        public SignalCommand StopCommand { get; private set; }

        /// <summary>
        /// A command to resume the chronometer.
        /// </summary>
        public SignalCommand ResumeCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of ChronometerSource.
        /// </summary>
        public ChronometerSource()
        {
            Interval = 1000;
            _timer.Tick += new EventHandler(_timer_Tick);
            
            StartCommand = new SignalCommand();
            StartCommand.Executed += (sender, e) =>
            {
                _startTimeMilliseconds = 0.0;
                if (e.Parameter != null)
                {
                    _startTimeMilliseconds = Convert.ToDouble(e.Parameter);
                }
                Start();
                EnsureCommandsCanExecuteCheck();
                
            };
            StartCommand.CanExecuteChecking += (sender, e) =>
            {
                e.Cancel = _timerRunning;
            };

            StopCommand = new SignalCommand();
            StopCommand.Executed += (sender, e) =>
            {
                Stop();
                EnsureCommandsCanExecuteCheck();
            };
            StopCommand.CanExecuteChecking += (sender, e) =>
            {
                e.Cancel = !_timerRunning;
            };

            ResumeCommand = new SignalCommand();
            ResumeCommand.Executed += (sender, e) =>
            {
                Resume();
                EnsureCommandsCanExecuteCheck();
            };
            ResumeCommand.CanExecuteChecking += (sender, e) =>
            {
                e.Cancel = _timerRunning;
            };
        }

        private void EnsureCommandsCanExecuteCheck()
        {
            StartCommand.EnsureCanExecuteCheck();
            StopCommand.EnsureCanExecuteCheck();
            ResumeCommand.EnsureCanExecuteCheck();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_startTimeMilliseconds > 0.0)
            {
                ElapsedTime = (DateTime.Now.Subtract(_dtStart)).Add(TimeSpan.FromMilliseconds(_startTimeMilliseconds));
            }
            else
            {
                ElapsedTime = DateTime.Now.Subtract(_dtStart);
            }
        }

        private double _interval;

        /// <summary>
        /// Gets or sets the interval of the timer.
        /// </summary>
        public double Interval 
        { 
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;
                _timer.Interval = TimeSpan.FromMilliseconds(_interval);
            }
        }

        private double _startTimeMilliseconds = 0.0;

        /// <summary>
        /// Gets or sets a start time (in milliseconds).
        /// </summary>
        public double StartTimeMilliseconds
        {
            get { return _startTimeMilliseconds; }
            set { _startTimeMilliseconds = value; }
        }

        /// <summary>
        /// Gets the elapsed time since the start.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get { return (TimeSpan)GetValue(ElapsedTimeProperty); }
            private set { SetValue(ElapsedTimeProperty, value); }
        }

        /// <summary>
        /// Identifies the ElapsedTime dependency property.
        /// </summary>
        public static readonly DependencyProperty ElapsedTimeProperty =
            DependencyProperty.Register(
                "ElapsedTime",
                typeof(TimeSpan),
                typeof(ChronometerSource),
                new PropertyMetadata(new TimeSpan(0)));

        /// <summary>
        /// Starts the chronometer.
        /// </summary>
        public void Start()
        {
            _dtStart = DateTime.Now;
            _timer.Start();
            _timerRunning = true;
        }

        /// <summary>
        /// Resumes the chronometer.
        /// </summary>
        public void Resume()
        {
            _timer.Start();
            _timerRunning = true;
        }

        /// <summary>
        /// Stops the chronometer.
        /// </summary>
        public void Stop()
        {
            _timer.Stop();
            _timerRunning = false;
        }
    }
}
