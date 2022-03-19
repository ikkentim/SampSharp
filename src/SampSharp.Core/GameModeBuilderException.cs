using System;
using System.Runtime.Serialization;

namespace SampSharp.Core
{
    /// <summary>
    ///     Thrown when an error occurs while building a game mode client in <see cref="GameModeBuilder" />.
    /// </summary>
    [Serializable]
    public class GameModeBuilderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameModeBuilderException" /> class.
        /// </summary>
        public GameModeBuilderException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModeBuilderException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GameModeBuilderException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModeBuilderException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public GameModeBuilderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModeBuilderException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected GameModeBuilderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}