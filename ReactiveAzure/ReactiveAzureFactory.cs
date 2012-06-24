using System;
using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    public class ReactiveAzureFactory<T> : IReactiveAzureFactory<T> where T : class 
    {
        public ReactiveAzure<T> Create(TimeSpan pollInterval, CloudQueue azureQueue)
        {
            var notifier = new ManualResetTimer(pollInterval);
            var messageDeserializer = new XmlMessageDeserializer<T>();
            var queueReader = new CloudQueueMessageReader<T>(azureQueue, messageDeserializer);
            var result = new ReactiveAzure<T>(notifier, queueReader);
            return result;
        }

        public IObservable<ITypedQueueMessage<T>> CreateObservable(TimeSpan pollInterval, CloudQueue azureQueue)
        {
            var reactiveAzure = Create(pollInterval, azureQueue);
            var reactiveAzureObservable = reactiveAzure.AsObservable();
            var result = new SubscriberControlledObservable<ITypedQueueMessage<T>>(reactiveAzureObservable,
                                                                                   reactiveAzure.BeginNotifications,
                                                                                   reactiveAzure.EndNotifications);

            return result;
        }
    }
}