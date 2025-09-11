namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a message queue for messages sent to a <see cref="SampSharpSynchronizationContext" />.
    /// </summary>
    internal interface IMessageQueue
    {
        /// <summary>
        ///     Pushes the specified item onto this instance.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        void PushMessage(SendOrPostCallbackItem item);
    }
}