using System;
using System.Reactive.Linq;

namespace ReactiveAzure
{
    /// <summary>
    /// This is the main class of this library.
    /// <para>
    /// 
    /// </para>
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public class QueueObserver<TMessage> where TMessage : class
    {
        private static readonly IQueueObserverFactory<TMessage> s_Factory = new QueueObserverFactory<TMessage>();
        /// <summary>
        /// A factory to handle the initialization of the QueueObserver instances.
        /// </summary>
        /// <remarks>Stole this way of having the factory from Task.Factory</remarks>
        public static IQueueObserverFactory<TMessage> Factory
        {
            get { return s_Factory; }
        }

        private readonly IManualResetNotifier _resetNotifier;
        private readonly IQueueMessageReader<TMessage> _messageReader;
        private volatile bool _sendNotifications;
        internal event MessageReceivedHandler<TMessage> MessageReceived;

        internal QueueObserver(IManualResetNotifier resetNotifier, IQueueMessageReader<TMessage> messageReader)
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
        /// <remarks>Does affect the notifications being raised, just starts this class acting on them.</remarks>
        internal void BeginNotifications()
        {
            _sendNotifications = true;
        }

        /// <summary>
        /// Signals that notifications should not be raised.
        /// </summary>
        /// <remarks>Does affect the notifications being raised, just stops this class acting on them.</remarks>
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
                var message = _messageReader.GetMessage();
                if (message != null)
                {
                    OnMessageReceived(message);
                }
            }
            e.Reset();
        }

        private void OnMessageReceived(TypedMessage<TMessage> message)
        {
            if (message == null) 
                throw new ArgumentNullException("message");

            var handler = MessageReceived;
            if (handler != null)
            {
                handler(message);
            }
        }

        /// <summary>
        /// Converts the <see cref="QueueObserver{TMessage}"/> into an <see cref="IObservable{T}"/>.
        /// </summary>
        /// <returns></returns>
        internal IObservable<TypedMessage<TMessage>> AsObservable()
        {
            //Use the Observable.FromEvent helper so that I don't need to implement the IObservable interface for myself.
            var observable =
                Observable.FromEvent<MessageReceivedHandler<TMessage>,
                                     TypedMessage<TMessage>>(ev => MessageReceived += ev,
                                                             ev => MessageReceived -= ev);
            return observable;
        }

    }
}
