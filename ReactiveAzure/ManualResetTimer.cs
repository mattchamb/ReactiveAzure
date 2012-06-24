using System;
using System.Timers;

namespace ReactiveAzure
{
    public class ManualResetTimer : IManualResetNotifier
    {
        private readonly Timer _timer;
        public ManualResetTimer(TimeSpan interval)
        {
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
    }
}