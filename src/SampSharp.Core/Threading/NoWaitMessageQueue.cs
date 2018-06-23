using System.Collections.Concurrent;

namespace SampSharp.Core.Threading
{
    /// <summary>
    ///     Represents a message queue for messages sent to a <see cref="SampSharpSyncronizationContext" /> without a waiting mechanism.
    /// </summary>
    public class NoWaitMessageQueue : IMessageQueue
    { 
        private readonly ConcurrentQueue<SendOrPostCallbackItem> _queue = new ConcurrentQueue<SendOrPostCallbackItem>();

        #region Implementation of IMessageQueue

        /// <summary>
        ///     Pushes the specified item onto this instance.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public void PushMessage(SendOrPostCallbackItem item)
        {
            _queue.Enqueue(item);
        }

        #endregion

        /// <summary>
        /// Pops a message from the queue.
        /// </summary>
        /// <returns>The popped message.</returns>
        public SendOrPostCallbackItem GetMessage()
        {
            // Dequeue from the internal queue.
            _queue.TryDequeue(out var result);
            return result;
        }
    }
}