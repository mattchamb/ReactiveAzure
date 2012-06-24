using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReactiveAzure
{
    public class ReactiveAzure<T>
    {
        private readonly IManualResetNotifier _resetNotifier;
        private readonly IQueueMessageReader<T> _messageReader;
        public event EventHandler<MessageReceivedArgs<ITypedQueueMessage<T>>> MessageReceived;

        public ReactiveAzure(IManualResetNotifier resetNotifier, IQueueMessageReader<T> messageReader)
        {
            _resetNotifier = resetNotifier;
            _messageReader = messageReader;

            _resetNotifier.OnElapsed += OnNotifierFired;
        }

        protected virtual void OnNotifierFired(object sender, ResetEventArgs e)
        {
            ITypedQueueMessage<T> message = _messageReader.GetMessage();
            if(message != null)
            {
                OnMessageReceived(message);
            }
            e.Reset();
        }

        private void OnMessageReceived(ITypedQueueMessage<T> message)
        {
            var handler = MessageReceived;
            if (handler != null)
            {
                var eventArgs = new MessageReceivedArgs<ITypedQueueMessage<T>>(message);
                handler(this, eventArgs);
            }
        }
    }
}
