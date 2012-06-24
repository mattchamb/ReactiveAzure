using System;
using System.Threading;

namespace ReactiveAzure
{
    /// <summary>
    /// Tracks the number of subscribers, and performs actions when the number
    /// of subscribers changes when its above zero, and when all subscribers unsubscribe.
    /// </summary>
    /// <remarks>
    /// This class is required because we want to be able to stop reading messages from the <see cref="Microsoft.WindowsAzure.StorageClient.CloudQueue"/>
    /// when there are no subscribers. The reason for this, is that a message read when there are no subscribers will be lost.
    /// </remarks>
    internal class SubscriberControlledObservable<T> : IObservable<T>
    {
        private readonly IObservable<T> _innerObservable;
        private readonly Action _whenObservers;
        private readonly Action _whenNoObservers;
        private int _subscriberCount;

        public SubscriberControlledObservable(IObservable<T> innerObservable, Action whenObservers, Action whenNoObservers)
        {
            if (innerObservable == null) 
                throw new ArgumentNullException("innerObservable");
            if (whenObservers == null) 
                throw new ArgumentNullException("whenObservers");
            if (whenNoObservers == null) 
                throw new ArgumentNullException("whenNoObservers");
            _subscriberCount = 0;
            _innerObservable = innerObservable;
            _whenObservers = whenObservers;
            _whenNoObservers = whenNoObservers;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            var innerSubscribeResult = _innerObservable.Subscribe(observer);
            Interlocked.Increment(ref _subscriberCount);
            if(_subscriberCount > 0)
                _whenObservers();
            var result = new SubscriberTracker(innerSubscribeResult, () =>
                                                                         {
                                                                             Interlocked.Decrement(ref _subscriberCount);
                                                                             if (_subscriberCount == 0)
                                                                             {
                                                                                 _whenNoObservers();
                                                                             }
                                                                         });
            return result;
        }

        private class SubscriberTracker : IDisposable
        {
            private readonly IDisposable _innerDisposable;
            private readonly Action _onDispose;
            private bool _isDisposed;

            public SubscriberTracker(IDisposable innerDisposable, Action onDispose)
            {
                _innerDisposable = innerDisposable;
                _onDispose = onDispose;
                _isDisposed = false;
            }

            public void Dispose()
            {
                if(!_isDisposed)
                {
                    _isDisposed = true;
                    _onDispose();
                }
                _innerDisposable.Dispose();
            }
        }
    }
}