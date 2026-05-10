namespace SampSharp.Entities;

/// <summary>
/// Provides a handler for logging unhandled exceptions that occur during runtime.
/// </summary>
public interface IUnhandledExceptionHandler
{
    /// <summary>
    /// Handles the specified exception within the given context.
    /// </summary>
    /// <param name="context">A string that describes the context in which the exception occurred. This information can be used to provide
    /// additional details for error handling or logging.</param>
    /// <param name="exception">The exception to be handled. Cannot be null.</param>
    void Handle(string context, Exception exception);
}