using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace SampSharp.Entities;

/// <summary>Provides functionality for handling events.</summary>
public interface IEventDispatcher
{
    /// <summary>Adds a middleware to the handler of the event with the specified <paramref name="name" />.</summary>
    /// <param name="name">The name of the event.</param>
    /// <param name="middleware">The middleware to add to the event.</param>
    void UseMiddleware(string name, Func<EventDelegate, EventDelegate> middleware);

    /// <summary>Invokes the event with the specified <paramref name="name" /> and <paramref name="arguments" />.</summary>
    /// <param name="name">The name of the event.</param>
    /// <param name="arguments">The arguments of the event.</param>
    /// <returns>The result of the event.</returns>
    object? Invoke(string name, params ReadOnlySpan<object> arguments);

    /// <summary>Invokes the event with the specified <paramref name="name" /> and <paramref name="arguments" />.</summary>
    /// <param name="name">The name of the event.</param>
    /// <param name="defaultValue">The default value to be returned in case no event handler returned a result.</param>
    /// <param name="arguments">The arguments of the event.</param>
    /// <returns>The result as returned by an event handler or <paramref name="defaultValue" /> if no non-null value was returned.</returns>
    [return: NotNullIfNotNull(nameof(defaultValue))]
    public T? InvokeAs<T>(string name, T defaultValue, params ReadOnlySpan<object> arguments);
}
