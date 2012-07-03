using System;
using System.Timers;

namespace ReactiveAzure
{
    /// <summary>
    /// Uses a <see cref="Timer"/> to trigger an event at a given interval.
    /// The timer must be manually reset 
    /// </summary>
    internal class ManualResetTimer : IManualResetNotifier, IDisposable
    {
        private readonly Timer _timer;
        private bool _disposed;

        public ManualResetTimer(TimeSpan interval)
        {
            _disposed = false;
            _timer = new Timer(interval.TotalMilliseconds)
                         {
                             AutoReset = false
                         };
            _timer.Elapsed += NotifyListeners;
        }

        public event EventHandler<ResetEventArgs> OnElapsed;

        private void NotifyListeners(object sender, ElapsedEventArgs e)
        {
            var handler = OnElapsed;
            if(handler != null)
            {
                //TODO - MattChamb 3-July 2012: Evaluate having multiple listeners all resetting this event - multiple calls to Start()
                var eventArgs = new ResetEventArgs(_timer.Start);
                handler(this, eventArgs);
            }
            else
            {
                //There are no event handlers.
                //So we are the only ones that know to reset the timer.
                _timer.Start();
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _timer.Dispose();
            }
        }
    }
}