using System;
using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    /// <summary>
    /// A factory that creates <see cref="IObservable{T}"/> objects from a <see cref="CloudQueue"/>
    /// </summary>
    /// <typeparam name="T">The type that the <see cref="CloudQueueMessage"/> will be deserialiszed into.</typeparam>
    public interface IQueueObserverFactory<T> where T : class
    {
        /// <summary>
        /// Creates an <see cref="IObservable{T}"/> that polls <paramref name="azureQueue"/> after each <paramref name="pollInterval"/>
        /// for a new message from queue. 
        /// </summary>
        /// <param name="pollInterval">The interval between each poll.</param>
        /// <param name="azureQueue">The queue from which to read messages</param>
        IObservable<TypedMessage<T>> CreateObservable(TimeSpan pollInterval, CloudQueue azureQueue);
    }
}
