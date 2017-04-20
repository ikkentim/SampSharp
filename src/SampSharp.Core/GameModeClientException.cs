using System;

namespace SampSharp.Core
{
    public class GameModeClientException : Exception
    {
        public GameModeClientException()
        {
        }

        public GameModeClientException(string message) : base(message)
        {
        }

        public GameModeClientException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}