using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    public interface IMessageSerializer<in T>
    {
        /// <summary>
        /// Serializes a <paramref name="value"/> into a <see cref="CloudQueueMessage"/>.
        /// This serialized message can be deserialized by a <see cref="IMessageDeserializer{T}"/>
        /// </summary>
        CloudQueueMessage SerializeMessage(T value);
    }
}