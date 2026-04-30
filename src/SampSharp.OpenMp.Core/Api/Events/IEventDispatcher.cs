using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IEventDispatcher{T}" /> interface.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct IEventDispatcher<T> : IUnmanagedInterface where T : class, IEventHandler<T>
{
    private readonly nint _handle;

    /// <summary>
    /// Initializes a new instance of the <see cref="IEventDispatcher{T}" /> struct.
    /// </summary>
    /// <param name="handle">The pointer handle.</param>
    public IEventDispatcher(nint handle)
    {
        _handle = handle;
    }
    
    /// <inheritdoc />
    public nint Handle => _handle;

    /// <inheritdoc />
    public bool HasValue => Handle != 0;

    /// <summary>
    /// Adds the specified <paramref name="handler" /> to this event dispatcher.
    /// </summary>
    /// <param name="handler">The event handler to add.</param>
    /// <param name="priority">The priority at which the handler should receive the events.</param>
    /// <returns><see langword="true" /> if the handler was added; otherwise, <see langword="false" />.</returns>
    public bool AddEventHandler(T handler, EventPriority priority = EventPriority.Default)
    {
        var handlerHandle = T.Marshaller.Marshal(handler).Create();

        return EventDispatcherInterop.AddEventHandler(_handle, handlerHandle, priority);
    }

    /// <summary>
    /// Removes the specified <paramref name="handler" /> from this event dispatcher.
    /// </summary>
    /// <param name="handler">The event handler to remove.</param>
    /// <returns><see langword="true" /> if the event handler was removed; otherwise, <see langword="false" />.</returns>
    public bool RemoveEventHandler(T handler)
    {
        var reference = T.Marshaller.Marshal(handler);
        var handlerHandle = reference.Handle;

        if (!handlerHandle.HasValue)
        {
            return false;
        }

        if (EventDispatcherInterop.RemoveEventHandler(_handle, handlerHandle.Value))
        {
            reference.Free();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns a value indicating whether the specified <paramref name="handler" /> is registered with this event dispatcher.
    /// </summary>
    /// <param name="handler">The event handler to check</param>
    /// <param name="priority">The priority at which the handler receives the events.</param>
    /// <returns><see langword="true" /> if the event handler was added; otherwise, <see langword="false" />.</returns>
    public bool HasEventHandler(T handler, out EventPriority priority)
    {
        var handlerHandle = T.Marshaller.Marshal(handler).Handle;
        priority = default;

        return handlerHandle.HasValue && EventDispatcherInterop.HasEventHandler(_handle, handlerHandle.Value, out priority);
    }

    /// <summary>
    /// Returns the number of event handlers registered with this event dispatcher.
    /// </summary>
    /// <returns>The number of event handlers registered.</returns>
    public int Count()
    {
        return (int)EventDispatcherInterop.Count(_handle);
    }
}