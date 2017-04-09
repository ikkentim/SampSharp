using System;
using System.Threading;

namespace SampSharp.Core.Threading
{
    public class SendOrPostCallbackItem
    {
        private readonly ManualResetEvent _asyncWaitHandle = new ManualResetEvent(false);
        private readonly ExecutionType _executionType;
        private readonly SendOrPostCallback _method;
        private readonly object _state;

        internal SendOrPostCallbackItem(SendOrPostCallback callback,
            object state, ExecutionType type)
        {
            _method = callback;
            _state = state;
            _executionType = type;
        }

        public Exception Exception { get; private set; }

        public bool ExecutedWithException => Exception != null;

        public WaitHandle ExecutionCompleteWaitHandle => _asyncWaitHandle;

        public void Execute()
        {
            if (_executionType == ExecutionType.Send)
            {
                try
                {
                    _method(_state);
                }
                catch (Exception e)
                {
                    Exception = e;
                }
                finally
                {
                    _asyncWaitHandle.Set();
                }
            }
            else
            {
                _method(_state);
            }
        }

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"SendOrPostCallbackItem {{ ExecutionType: {_executionType} }}";
        }

        #endregion
    }
}