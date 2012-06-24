using System;

namespace ReactiveAzure
{
    public interface ITypedQueueMessage<T>
    {
        string Id { get; }

        string PopReceipt { get; }

        DateTime? InsertionTime { get; }

        DateTime? ExpirationTime { get; }

        DateTime? NextVisibleTime { get; }

        int DequeueCount { get; }
    }
}