using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IIndexedEventDispatcher{T}" /> interface.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct IIndexedEventDispatcher<T> : IUnmanagedInterface where T : class, IEventHandler<T>
{
    private readonly nint _handle;

    /// <inheritdoc />
    public nint Handle => _handle;

    /// <inheritdoc />
    public bool HasValue => Handle != 0;

    /// <summary>
    /// Gets the number of event handlers registered.
    /// </summary>
    /// <returns>The number of registered event handlers.</returns>
    public int Count()
    {
        return (int)IndexedEventDispatcherInterop.Count(_handle);
    }

    /// <summary>
    /// Gets the number of event handlers registered for the specified index.
    /// </summary>
    /// <param name="index">The index to get the number of event handlers for.</param>
    /// <returns>The number of registered event handlers.</returns>
    public int Count(int index)
    {
        return (int)IndexedEventDispatcherInterop.Count(_handle, index);
    }

    /// <summary>
    /// Adds the specified <paramref name="handler" /> to this indexed event dispatcher.
    /// </summary>
    /// <param name="handler">The event handler to add.</param>
    /// <param name="index">The index for which to add the handler.</param>
    /// <param name="priority">The priority at which the handler should receive the events.</param>
    /// <returns><see langword="true" /> if the event handler was added; otherwise, <see langword="false" />.</returns>
    public bool AddEventHandler(T handler, int index, EventPriority priority = EventPriority.Default)
    {
        var handlerHandle = T.Marshaller.Marshal(handler).Create();

        return IndexedEventDispatcherInterop.AddEventHandler(_handle, handlerHandle, index, priority);
    }

    /// <summary>
    /// Removes the specified <paramref name="handler" /> from this indexed event dispatcher.
    /// </summary>
    /// <param name="handler">The event handler to remove.</param>
    /// <param name="index">The index from which to remove the handler.</param>
    /// <returns><see langword="true" /> if the event handler was removed; otherwise, <see langword="false" />.</returns>
    public bool RemoveEventHandler(T handler, int index)
    {
        var reference = T.Marshaller.Marshal(handler);
        var handlerHandle = reference.Handle;

        if (!handlerHandle.HasValue)
        {
            return false;
        }

        if (IndexedEventDispatcherInterop.RemoveEventHandler(_handle, handlerHandle.Value, index))
        {
            reference.Free();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns a value indicating whether the specified <paramref name="handler" /> is registered at the specified
    /// <paramref name="index" />.
    /// </summary>
    /// <param name="handler">The handler to check.</param>
    /// <param name="index">The index to check</param>
    /// <param name="priority">The priority at which the handler should receive the events.</param>
    /// <returns><see langword="true" /> if handler is registered with this event dispatcher at the given index;
    /// otherwise <see langword="false" />.</returns>
    public bool HasEventHandler(T handler, int index, out EventPriority priority)
    {
        var handlerHandle = T.Marshaller.Marshal(handler).Handle;
        priority = default;

        return handlerHandle.HasValue && IndexedEventDispatcherInterop.HasEventHandler(_handle, handlerHandle.Value, index, out priority);
    }
}