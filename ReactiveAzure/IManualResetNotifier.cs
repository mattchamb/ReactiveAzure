using System;

namespace ReactiveAzure
{
    internal interface IManualResetNotifier
    {
        event EventHandler<ResetEventArgs> OnElapsed;
    }
}