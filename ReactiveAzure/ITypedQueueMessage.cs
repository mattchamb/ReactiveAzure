using System;

namespace ReactiveAzure
{
    public interface ITypedQueueMessage<out T>
    {
        T GetValue();
    }
}