namespace SampSharp.Entities;

/// <summary>
/// Represents a method that handles an unhandled exception occurred within SampSharp.OpenMp.Entities.
/// </summary>
/// <param name="serviceProvider">The service provider.</param>
/// <param name="context">Context in which the exception occurred.</param>
/// <param name="exception">The exception.</param>
public delegate void UnhandledExceptionHandler(IServiceProvider serviceProvider, string context, Exception exception);