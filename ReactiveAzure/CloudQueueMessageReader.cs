using System;
using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    /// <summary>
    /// Responsible for reading messages out of a <see cref="CloudQueue" /> and deserializing the resulting raw message.
    /// </summary>
    /// <typeparam name="T">The type of the message to be deserialzed from the <see cref="CloudQueueMessage"/></typeparam>
    public class CloudQueueMessageReader<T> : IQueueMessageReader<T>
    {
        private readonly CloudQueue _cloudQueue;
        private readonly IMessageDeserializer<T> _messageDeserializer;

        public CloudQueueMessageReader(CloudQueue cloudQueue, IMessageDeserializer<T> messageDeserializer)
        {
            if (cloudQueue == null) 
                throw new ArgumentNullException("cloudQueue");
            if (messageDeserializer == null) 
                throw new ArgumentNullException("messageDeserializer");
            _cloudQueue = cloudQueue;
            _messageDeserializer = messageDeserializer;
        }

        /// <summary>
        /// Reads a <see cref="TypedMessage{TMessage}"/> from a <see cref="CloudQueue"/>.
        /// </summary>
        /// <returns>A <see cref="TypedMessage{TMessage}"/> that contains the deserialized object that the message represents.</returns>
        public TypedMessage<T> GetMessage()
        {
            var message = _cloudQueue.GetMessage();
            if (message == null)
                return null;
            var messageContents = _messageDeserializer.DeserializeMessage(message);
            return new TypedMessage<T>(messageContents, message);
        }
    }
}