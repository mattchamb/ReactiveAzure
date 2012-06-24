using System;

namespace ReactiveAzure
{
    public class ResetEventArgs: EventArgs
    {
        private readonly Action _reset;

        public ResetEventArgs(Action reset)
        {
            if (reset == null) 
                throw new ArgumentNullException("reset");
            _reset = reset;
        }

        public void Reset()
        {
            _reset();
        }
    }
}