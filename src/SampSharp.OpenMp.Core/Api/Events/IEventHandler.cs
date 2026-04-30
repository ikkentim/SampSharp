namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Base interface for event handlers. This interface is automatically implemented by the code generator for event
/// handlers which are marked with the <see cref="OpenMpEventHandlerAttribute" />.
/// </summary>
/// <typeparam name="TEventHandler">The type of the event handler interface.</typeparam>
public interface IEventHandler<TEventHandler> where TEventHandler : class
{
    /// <summary>
    /// Gets the marshaller which can marshal the event handler to its unmanaged representation.
    /// </summary>
    static abstract IEventHandlerMarshaller<TEventHandler> Marshaller { get; }
}