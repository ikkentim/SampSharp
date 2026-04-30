namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents a reference to a <see cref="IEventHandler{TEventHandler}" />.
/// </summary>
/// <typeparam name="TEventHandler">The interface type of the event handler.</typeparam>
public readonly struct EventHandlerReference<TEventHandler> where TEventHandler : class
{
    private readonly EventHandlerMarshaller<TEventHandler> _marshaller;
    private readonly TEventHandler _handler;

    internal EventHandlerReference(EventHandlerMarshaller<TEventHandler> marshaller, TEventHandler handler)
    {
        _marshaller = marshaller;
        _handler = handler;
    }

    /// <summary>
    /// Gets the unmanaged handle of the event handler. Returns <see langword="null" /> if this event handler reference has
    /// not been created or has been freed.
    /// </summary>
    public nint? Handle => _marshaller?.GetReference(_handler);

    /// <summary>
    /// Creates a new reference to the native event handler or increases its reference count.
    /// </summary>
    /// <returns>The created reference to the native event handler.</returns>
    public nint Create()
    {
        return _marshaller?.IncreaseReferenceCount(_handler) ?? throw new InvalidOperationException("Invalid state");
    }

    /// <summary>
    /// Decreases the reference count of the native event handler or frees its resources.
    /// </summary>
    public void Free()
    {
        if (_marshaller == null)
        {
            throw new InvalidOperationException("Invalid state");
        }

        _marshaller.DecreaseReferenceCount(_handler);
    }
}