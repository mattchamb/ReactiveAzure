using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    public interface IReactiveAzureFactory<out T> where T : class
    {
        IObservable<ITypedQueueMessage<T>> CreateObservable(TimeSpan pollInterval, CloudQueue azureQueue);
    }
}
