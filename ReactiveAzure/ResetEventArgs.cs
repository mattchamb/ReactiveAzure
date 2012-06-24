using System;
using System.Timers;

namespace ReactiveAzure
{
    public class ResetEventArgs: EventArgs
    {
        private readonly Timer _timer;

        public ResetEventArgs(Timer timer)
        {
            _timer = timer;
        }

        public void Reset()
        {
            _timer.Start();
        }
    }
}