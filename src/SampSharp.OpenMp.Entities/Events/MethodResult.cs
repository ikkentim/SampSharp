namespace SampSharp.Entities;

/// <summary>
/// Boxed boolean event-handler return value. <see cref="EventDispatcher"/>'s
/// invokers convert <see langword="bool"/>-returning handlers to
/// <see cref="MethodResult"/> so the dispatcher can distinguish "explicit
/// false" from "no handler ran" (which is <see langword="null"/>). Public so
/// middlewares can interpret the truthiness of an upstream result.
/// </summary>
public sealed record MethodResult(bool Value)
{
    /// <summary>Singleton "true" instance.</summary>
    public static MethodResult True { get; } = new(true);
    /// <summary>Singleton "false" instance.</summary>
    public static MethodResult False { get; } = new(false);

    /// <summary>Returns the singleton <see cref="MethodResult"/> for the given <paramref name="value"/>.</summary>
    public static object From(bool value) => value ? True : False;
}