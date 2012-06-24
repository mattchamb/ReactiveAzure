using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    public interface IMessageDeserializer<out T>
    {
        T DeserializeMessage(CloudQueueMessage message);
    }
}