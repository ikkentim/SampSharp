using System;
namespace GameMode.Exceptions
{
    /// <summary>
    /// Represents errors that occur when the given player isn't connected.
    /// </summary>
    public class VehicleDoesNotExistException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleDoesNotExistException"/> class.
        /// </summary>
        public VehicleDoesNotExistException()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleDoesNotExistException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public VehicleDoesNotExistException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleDoesNotExistException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public VehicleDoesNotExistException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
