using System;
using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    public class TypedQueueMessage<T> : CloudQueueMessage, ITypedQueueMessage<T>
    {
        private readonly IMessageDeserializer<T> _messageDeserializer;

        public TypedQueueMessage(IMessageDeserializer<T> messageDeserializer, string contents) 
            : base(contents)
        {
            _messageDeserializer = messageDeserializer;
        }

        public TypedQueueMessage(IMessageDeserializer<T> messageDeserializer, CloudQueueMessage originalMessage)
            : this(messageDeserializer, originalMessage.AsString)
        {
            //Perform a memberwise copy of the original message properties.
            DequeueCount = originalMessage.DequeueCount;
            ExpirationTime = originalMessage.ExpirationTime;
            Id = originalMessage.Id;
            InsertionTime = originalMessage.InsertionTime;
            NextVisibleTime = originalMessage.NextVisibleTime;
            PopReceipt = originalMessage.PopReceipt;
        }

        public T GetValue()
        {
            return _messageDeserializer.DeserializeMessage(this);
        }
    }
}