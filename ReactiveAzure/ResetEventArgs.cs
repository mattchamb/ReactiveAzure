using System;

namespace ReactiveAzure
{
    /// <summary>
    /// Provides a method of allowing an event listener to control whether the event will be raised again.
    /// </summary>
    internal class ResetEventArgs: EventArgs
    {
        private readonly Action _reset;
        private bool _isReset;

        /// <remarks>
        /// We use an <see cref="Action"/> because we want this to be independent of 
        /// the underlying mechanism that it is resetting.
        /// </remarks>
        public ResetEventArgs(Action reset)
        {
            if (reset == null) 
                throw new ArgumentNullException("reset");
            _reset = reset;
            _isReset = false;
        }

        public void Reset()
        {
            if (!_isReset)
            {
                _isReset = true;
                _reset();
            }
        }
    }
}