using System;
using System.Runtime.Serialization;

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents an error which occurs while registering a system.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class SystemRegistryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRegistryException"/> class.
        /// </summary>
        public SystemRegistryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRegistryException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SystemRegistryException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRegistryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public SystemRegistryException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRegistryException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected SystemRegistryException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}