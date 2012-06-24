using System;

namespace ReactiveAzure
{
    public class MessageReceivedArgs<T> : EventArgs
    {
        public MessageReceivedArgs(T message)
        {
            Message = message;
        }

        public T Message { get; private set; }
    }
}