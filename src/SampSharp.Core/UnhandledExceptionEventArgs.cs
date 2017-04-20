using System;

namespace SampSharp.Core
{
    /// <summary>
    ///     Provides data for the <see cref="IGameModeClient.UnhandledException"/> event.
    /// </summary>
    public class UnhandledExceptionEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnhandledExceptionEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public UnhandledExceptionEventArgs(Exception exception)
        {
            Exception = exception;
        }

        /// <summary>
        ///     Gets the exception.
        /// </summary>
        public Exception Exception { get; }
    }
}