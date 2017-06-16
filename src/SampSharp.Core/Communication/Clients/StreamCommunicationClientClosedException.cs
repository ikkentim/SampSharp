using System;

namespace SampSharp.Core.Communication.Clients
{
    public class StreamCommunicationClientClosedException : Exception
    {
        public StreamCommunicationClientClosedException()
        {
        }

        public StreamCommunicationClientClosedException(string message) : base(message)
        {
        }

        public StreamCommunicationClientClosedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}