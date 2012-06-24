using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    public interface IMessageSerializer<in T>
    {
        CloudQueueMessage SerializeMessage(T value);
    }
}