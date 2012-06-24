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
                var eventArgs = new ResetEventArgs(_timer);
                handler(this, eventArgs);
            }
        }
    }
}