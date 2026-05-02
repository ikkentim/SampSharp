using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities;

internal class SafeEventHandlerRegistration<TComponent, TEventHandler>(SampSharpEnvironment environment, TEventHandler handler, Func<TComponent, IEventDispatcher<TEventHandler>> dispatcherProvider) : IDisposable
    where TComponent : unmanaged, IComponent.IManagedInterface
    where TEventHandler : class, IEventHandler<TEventHandler>
{
    private bool _disposed;

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        var component = environment.Components.QueryComponent<TComponent>();

        if (!component.HasValue)
        {
            TEventHandler.Marshaller.Marshal(handler).Free();
            return;
        }

        var dispatcher = dispatcherProvider(component);

        if (!dispatcher.HasValue)
        {
            TEventHandler.Marshaller.Marshal(handler).Free();
            return;
        }

        dispatcher.RemoveEventHandler(handler);
    }
}

internal class SafeEventHandlerRegistration<TEventHandler>(SampSharpEnvironment environment, TEventHandler handler, Func<ICore, IEventDispatcher<TEventHandler>> dispatcherProvider) : IDisposable
    where TEventHandler : class, IEventHandler<TEventHandler>
{
    private bool _disposed;

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;

        if (!environment.Core.HasValue)
        {
            TEventHandler.Marshaller.Marshal(handler).Free();
            return;
        }

        var dispatcher = dispatcherProvider(environment.Core);

        if (!dispatcher.HasValue)
        {
            TEventHandler.Marshaller.Marshal(handler).Free();
            return;
        }

        dispatcher.RemoveEventHandler(handler);
    }
}