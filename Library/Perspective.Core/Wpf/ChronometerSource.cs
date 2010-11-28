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

namespace Perspective.Core.Wpf
{
    /// <summary>
    /// A chronometer data source
    /// </summary>
    public class ChronometerSource : DependencyObject
    {
        private DispatcherTimer _timer = new DispatcherTimer();
        private double _ms;
        private bool _timerRunning = false;

        /// <summary>
        /// A command to start the chronometer.
        /// </summary>
        public SignalCommand StartCommand { get; private set; }

        public SignalCommand StopCommand { get; private set; }

        public SignalCommand ResumeCommand { get; private set; }

        public ChronometerSource()
        {
            Interval = 1000;

            _timer.Tick += new EventHandler(_timer_Tick);
            
            StartCommand = new SignalCommand();
            StartCommand.Executed += (sender, e) =>
            {
                if (e.Parameter != null)
                {
                    Start(Convert.ToDouble(e.Parameter));
                }
                else
                {
                    Start();
                }
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
            _ms += _interval;
            ElapsedTime = TimeSpan.FromMilliseconds(_ms);
        }

        private double _interval;
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

        public TimeSpan ElapsedTime
        {
            get { return (TimeSpan)GetValue(ElapsedTimeProperty); }
            private set { SetValue(ElapsedTimeProperty, value); }
        }

        public static readonly DependencyProperty ElapsedTimeProperty =
            DependencyProperty.Register(
                "ElapsedTime",
                typeof(TimeSpan),
                typeof(ChronometerSource),
                new PropertyMetadata(new TimeSpan(0)));

        public void Start()
        {
            _ms = 0.0;
            _timer.Start();
            _timerRunning = true;
        }

        public void Start(double startMilliseconds)
        {
            _ms = startMilliseconds;
            _timer.Start();
            _timerRunning = true;
        }

        public void Resume()
        {
            _timer.Start();
            _timerRunning = true;
        }

        public void Stop()
        {
            _timer.Stop();
            _timerRunning = false;
        }
    }
}
