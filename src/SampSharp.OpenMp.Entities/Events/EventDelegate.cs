namespace SampSharp.Entities;

/// <summary>Invokes the next event handler with the specified event context.</summary>
/// <param name="context">The event context.</param>
/// <returns>The return value of the event.</returns>
public delegate object? EventDelegate(EventContext context);