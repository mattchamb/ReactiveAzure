using System;
using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    /// <summary>
    /// A factory that creates <see cref="IObservable{T}"/> objects from a <see cref="CloudQueue"/>
    /// </summary>
    /// <typeparam name="T">The type that the <see cref="CloudQueueMessage"/> will be deserialiszed into.</typeparam>
    public class QueueObserverFactory<T> : IQueueObserverFactory<T> where T : class 
    {
        public QueueObserver<T> Create(TimeSpan pollInterval, CloudQueue azureQueue)
        {
            //TODO - MattChamb 3-July 2012: How do I make dependency injection work nicely with Disposable dependencies? Do I let the GC handle it?
            var notifier = new ManualResetTimer(pollInterval);
            var messageDeserializer = new XmlMessageDeserializer<T>();
            var queueReader = new CloudQueueMessageReader<T>(azureQueue, messageDeserializer);
            var result = new QueueObserver<T>(notifier, queueReader);
            return result;
        }

        /// <summary>
        /// Creates an <see cref="IObservable{T}"/> that polls <paramref name="azureQueue"/> after each <paramref name="pollInterval"/>
        /// for a new message from queue. 
        /// </summary>
        /// <param name="pollInterval">The interval between each poll.</param>
        /// <param name="azureQueue">The queue from which to read messages</param>
        public IObservable<TypedMessage<T>> CreateObservable(TimeSpan pollInterval, CloudQueue azureQueue)
        {
            var reactiveAzure = Create(pollInterval, azureQueue);
            var reactiveAzureObservable = reactiveAzure.AsObservable();
            var result = new SubscriberControlledObservable<TypedMessage<T>>(reactiveAzureObservable,
                                                                             reactiveAzure.BeginNotifications,
                                                                             reactiveAzure.EndNotifications);
            return result;
        }
    }
}