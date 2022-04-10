using System.Collections.Concurrent;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a message queue for messages sent to a <see cref="SampSharpSynchronizationContext" /> without a waiting mechanism.
    /// </summary>
    internal class NoWaitMessageQueue : IMessageQueue
    { 
        private readonly ConcurrentQueue<SendOrPostCallbackItem> _queue = new ConcurrentQueue<SendOrPostCallbackItem>();
        
        /// <summary>
        ///     Pushes the specified item onto this instance.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public void PushMessage(SendOrPostCallbackItem item)
        {
            _queue.Enqueue(item);
        }
        
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