using System;

namespace SampSharp.Core
{
    /// <summary>
    ///     Thrown when an error occurs while building a game mode client in <see cref="GameModeBuilder"/>.
    /// </summary>
    public class GameModeBuilderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameModeBuilderException"/> class.
        /// </summary>
        public GameModeBuilderException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModeBuilderException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GameModeBuilderException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModeBuilderException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public GameModeBuilderException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}