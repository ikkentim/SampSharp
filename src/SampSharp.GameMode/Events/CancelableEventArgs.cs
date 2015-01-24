using System;
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for events in <see cref="KeyHandlerSet" />.
    /// </summary>
    public class CancelableEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a value indicating whether this event should not be trigger events with lower priorities.
        /// </summary>
        public bool IsCanceled { get; set; }
    }
}