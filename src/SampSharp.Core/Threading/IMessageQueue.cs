namespace SampSharp.Core.Threading
{
    /// <summary>
    ///     Represents a message queue for messages sent to a <see cref="SampSharpSynchronizationContext" />.
    /// </summary>
    public interface IMessageQueue
    {
        /// <summary>
        ///     Pushes the specified item onto this instance.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        void PushMessage(SendOrPostCallbackItem item);
    }
}