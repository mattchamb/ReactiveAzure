using System;

namespace ReactiveAzure.Tests
{
    class TestNotifier : IManualResetNotifier
    {
        public event EventHandler<ResetEventArgs> OnElapsed;


        public void ForceElapse()
        {
            EventHandler<ResetEventArgs> handler = OnElapsed;
            if (handler != null) 
                handler(this, new ResetEventArgs(() => { }));
        }
    }
}