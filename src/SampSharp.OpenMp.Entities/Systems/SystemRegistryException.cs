namespace SampSharp.Entities;

/// <summary>Represents an error which occurs while registering a system.</summary>
/// <seealso cref="Exception" />
public class SystemRegistryException : Exception
{
    /// <summary>Initializes a new instance of the <see cref="SystemRegistryException" /> class.</summary>
    public SystemRegistryException()
    {
    }

    /// <summary>Initializes a new instance of the <see cref="SystemRegistryException" /> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    public SystemRegistryException(string message) : base(message)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="SystemRegistryException" /> class.</summary>
    /// <param name="message">The message.</param>
    /// <param name="inner">The inner.</param>
    public SystemRegistryException(string message, Exception inner) : base(message, inner)
    {
    }
}