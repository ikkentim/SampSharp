namespace SampSharp.Entities.SAMP;

/// <summary>
/// An exception thrown when a player name is invalid.
/// </summary>
public class InvalidPlayerNameException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidPlayerNameException" /> class.
    /// </summary>
    public InvalidPlayerNameException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidPlayerNameException" /> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidPlayerNameException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidPlayerNameException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public InvalidPlayerNameException(string message, Exception? inner) : base(message, inner) { }
}