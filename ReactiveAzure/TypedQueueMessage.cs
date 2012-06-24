using System;
using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    public class TypedQueueMessage<T> : ITypedQueueMessage<T> 
    {
        private readonly CloudQueueMessage _message;
        private readonly IMessageDeserializer<T> _messageDeserializer;

        public TypedQueueMessage(CloudQueueMessage message, IMessageDeserializer<T> messageDeserializer)
        {
            _message = message;
            _messageDeserializer = messageDeserializer;
        }

        public T GetValue()
        {
            return _messageDeserializer.DeserializeMessage(_message);
        }

        public string Id
        {
            get { return _message.Id; }
        }

        public string PopReceipt
        {
            get { return _message.PopReceipt; }
        }

        public DateTime? InsertionTime
        {
            get { return _message.InsertionTime; }
        }

        public DateTime? ExpirationTime
        {
            get { return _message.ExpirationTime; }
        }

        public DateTime? NextVisibleTime
        {
            get { return _message.NextVisibleTime; }
        }

        public int DequeueCount
        {
            get { return _message.DequeueCount; }
        }
    }
}