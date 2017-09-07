using System;

namespace SampSharp.Core.Communication.Clients
{
    /// <summary>
    /// Represents errors that occur when the connection between the <see cref="StreamCommunicationClient"/> and the server is closed while waiting for server data.
    /// </summary>
    public class StreamCommunicationClientClosedException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StreamCommunicationClientClosedException" /> class.
        /// </summary>
        public StreamCommunicationClientClosedException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StreamCommunicationClientClosedException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public StreamCommunicationClientClosedException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StreamCommunicationClientClosedException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The inner exception.</param>
        public StreamCommunicationClientClosedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}