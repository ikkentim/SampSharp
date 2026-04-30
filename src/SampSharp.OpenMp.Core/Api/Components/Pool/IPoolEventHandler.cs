namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IPool{T}.GetPoolEventDispatcher" />.
/// </summary>
public interface IPoolEventHandler<T> : IEventHandler<IPoolEventHandler<T>> where T : unmanaged, IUnmanagedInterface
{
    /// <summary>
    /// Called when a pool entry is created.
    /// </summary>
    /// <param name="entry">The created pool entry.</param>
    void OnPoolEntryCreated(T entry);

    /// <summary>
    /// Called when a pool entry is destroyed.
    /// </summary>
    /// <param name="entry">The destroyed pool entry.</param>
    void OnPoolEntryDestroyed(T entry);

    // [OpenMpEventHandler] does not support generic types - implement it manually

    /// <summary>
    /// Gets the marshaller for the event handler.
    /// </summary>
    static IEventHandlerMarshaller<IPoolEventHandler<T>> IEventHandler<IPoolEventHandler<T>>.Marshaller => NativeEventHandlerManager.Instance;

    /// <summary>
    /// Manages the marshalling of native event handlers for <see cref="IPoolEventHandler{T}" />.
    /// </summary>
    public class NativeEventHandlerManager : EventHandlerMarshaller<IPoolEventHandler<T>>
    {
        /// <summary>
        /// Gets the singleton instance of the <see cref="NativeEventHandlerManager" />.
        /// </summary>
        public static NativeEventHandlerManager Instance { get; } = new();

        /// <inheritdoc />
        protected override (nint, object) Create(IPoolEventHandler<T> handler)
        {
            Delegate onPoolEntryCreatedDelegate = (PoolDelegate)(h => handler.OnPoolEntryCreated(StructPointer.AsStruct<T>(h))),
                onPoolEntryDestroyedDelegate = (PoolDelegate)(h => handler.OnPoolEntryDestroyed(StructPointer.AsStruct<T>(h)));

            nint onPoolEntryCreatedPtr = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(onPoolEntryCreatedDelegate),
                onPoolEntryDestroyedPtr = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(onPoolEntryDestroyedDelegate);

            object[] data = [onPoolEntryCreatedDelegate, onPoolEntryDestroyedDelegate];

            var handle = PoolEventHandlerInterop.PoolEventHandlerImpl_create(onPoolEntryCreatedPtr, onPoolEntryDestroyedPtr);
            return (handle, data);
        }

        /// <inheritdoc />
        protected override void Free(nint handle)
        {
            PoolEventHandlerInterop.PoolEventHandlerImpl_delete(handle);
        }
    }
}
