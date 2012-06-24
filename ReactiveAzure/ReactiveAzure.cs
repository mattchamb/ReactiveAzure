using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveAzure
{
    public delegate void MessageReceivedHandler<in TMessage>(ITypedQueueMessage<TMessage> message);

    public class ReactiveAzure<TMessage> where TMessage : class
    {
        private static readonly IReactiveAzureFactory<TMessage> s_Factory = new ReactiveAzureFactory<TMessage>();
        public static IReactiveAzureFactory<TMessage> Factory
        {
            get { return s_Factory; }
        }

        private readonly IManualResetNotifier _resetNotifier;
        private readonly IQueueMessageReader<TMessage> _messageReader;
        private volatile bool _sendNotifications;
        internal event MessageReceivedHandler<TMessage> MessageReceived;

        internal ReactiveAzure(IManualResetNotifier resetNotifier, IQueueMessageReader<TMessage> messageReader)
        {
            if (resetNotifier == null) 
                throw new ArgumentNullException("resetNotifier");
            if (messageReader == null) 
                throw new ArgumentNullException("messageReader");

            _resetNotifier = resetNotifier;
            _messageReader = messageReader;
            _sendNotifications = false;

            _resetNotifier.OnElapsed += OnNotifierFired;
        }

        /// <summary>
        /// Signals that notifications should be raised.
        /// </summary>
        internal void BeginNotifications()
        {
            _sendNotifications = true;
        }

        /// <summary>
        /// Signals that notifications should not be raised.
        /// </summary>
        internal void EndNotifications()
        {
            _sendNotifications = false;
        }

        private void OnNotifierFired(object sender, ResetEventArgs e)
        {
            if (e == null) 
                throw new ArgumentNullException("e");
            if (_sendNotifications)
            {
                ITypedQueueMessage<TMessage> message = _messageReader.GetMessage();
                if (message != null)
                {
                    OnMessageReceived(message);
                }
            }
            e.Reset();
        }

        private void OnMessageReceived(ITypedQueueMessage<TMessage> message)
        {
            if (message == null) 
                throw new ArgumentNullException("message");

            var handler = MessageReceived;
            if (handler != null)
            {
                handler(message);
            }
        }

        internal IObservable<ITypedQueueMessage<TMessage>> AsObservable()
        {
            var observable =
                Observable.FromEvent<MessageReceivedHandler<TMessage>,
                                     ITypedQueueMessage<TMessage>>(ev => MessageReceived += ev,
                                                                   ev => MessageReceived -= ev);
            return observable;
        }

    }
}
