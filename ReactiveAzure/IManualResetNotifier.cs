using System;

namespace ReactiveAzure
{
    public interface IManualResetNotifier
    {
        event EventHandler<ResetEventArgs> OnElapsed;
    }
}