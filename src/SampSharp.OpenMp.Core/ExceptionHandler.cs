namespace SampSharp.OpenMp.Core;

/// <summary>
/// Represents a method that handles exceptions.
/// </summary>
/// <param name="context">The context in which the exception has occurred.</param>
/// <param name="exception">The exception which has occurred.</param>
public delegate void ExceptionHandler(string context, Exception exception);