using System;
using Microsoft.WindowsAzure.StorageClient;

namespace ReactiveAzure
{
    //TODO - MattChamb 3-July 2012: Evaluate if this class should also be used to serialize messages.
    public class TypedMessage<TMessage> : CloudQueueMessage
    {
        public TMessage Value { get; private set; }

        public TypedMessage(TMessage value, string contents) 
            : base(contents)
        {
            if (contents == null) 
                throw new ArgumentNullException("contents");

            Value = value;
        }

        public TypedMessage(TMessage value, CloudQueueMessage originalMessage)
            : this(value, originalMessage.AsString)
        {
            //Perform a memberwise copy of the original message properties.
            DequeueCount = originalMessage.DequeueCount;
            ExpirationTime = originalMessage.ExpirationTime;
            Id = originalMessage.Id;
            InsertionTime = originalMessage.InsertionTime;
            NextVisibleTime = originalMessage.NextVisibleTime;
            PopReceipt = originalMessage.PopReceipt;
        }
    }
}