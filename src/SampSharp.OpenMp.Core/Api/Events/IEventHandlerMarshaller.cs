namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides methods for marshalling a managed event handler to a <see cref="EventHandlerReference{TEventHandler}" />
/// </summary>
/// <typeparam name="TEventHandler">The type of the event handler interface.</typeparam>
public interface IEventHandlerMarshaller<TEventHandler> where TEventHandler : class
{
    /// <summary>
    /// Marshals the specified managed event handler to a <see cref="EventHandlerReference{TEventHandler}" />.
    /// </summary>
    /// <param name="handler">The managed event handler to marshal.</param>
    /// <returns>The unmanaged event handler.</returns>
    EventHandlerReference<TEventHandler> Marshal(TEventHandler handler);
}