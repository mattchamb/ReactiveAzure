using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    public class CloudQueueMessageReader<T> : IQueueMessageReader<T>
    {
        private readonly CloudQueue _cloudQueue;
        private readonly IMessageDeserializer<T> _messageDeserializer;

        public CloudQueueMessageReader(CloudQueue cloudQueue, IMessageDeserializer<T> messageDeserializer)
        {
            _cloudQueue = cloudQueue;
            _messageDeserializer = messageDeserializer;
        }

        public ITypedQueueMessage<T> GetMessage()
        {
            var message = _cloudQueue.GetMessage();
            if (message == null)
                return null;
            return new TypedQueueMessage<T>(message, _messageDeserializer);
        }
    }
}