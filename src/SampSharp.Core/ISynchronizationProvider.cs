using System;

namespace SampSharp.Core
{
    /// <summary>
    /// Contains the methods of a provider of synchronization to the main thread.
    /// </summary>
    public interface ISynchronizationProvider
    {
        /// <summary>
        /// Gets a value indicating whether an invoke is required to synchronize to the main tread.
        /// </summary>
        public bool InvokeRequired { get; }

        /// <summary>
        /// Invokes the specified action on the main thread.
        /// </summary>
        /// <param name="action">The action to invoke on the main thread.</param>
        public void Invoke(Action action);

    }
}