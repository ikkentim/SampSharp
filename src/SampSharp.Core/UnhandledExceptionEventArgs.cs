using System;

namespace SampSharp.Core
{
    /// <summary>
    ///     Provides data for the <see cref="IGameModeClient.UnhandledException" /> event.
    /// </summary>
    public class UnhandledExceptionEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnhandledExceptionEventArgs" /> class.
        /// </summary>
        /// <param name="callbackName">The name of the callback during which the exception was thrown.</param>
        /// <param name="exception">The exception.</param>
        public UnhandledExceptionEventArgs(string callbackName, Exception exception)
        {
            CallbackName = callbackName;
            Exception = exception;
        }

        /// <summary>
        /// Gets or sets the name of the callback during which the exception was thrown.
        /// </summary>
        public string CallbackName { get; }

        /// <summary>
        ///     Gets the exception.
        /// </summary>
        public Exception Exception { get; }
    }
}