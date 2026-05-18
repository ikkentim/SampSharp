using SampSharp.OpenMp.Core;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities;

internal class SafeComponentHandleProvider : ISafeComponentHandleProvider
{
    private readonly IStartupContext _startupContext;
    private readonly Dictionary<UID, ISafeComponentHandle> _safeHandles = [];

    public SafeComponentHandleProvider(IStartupContext startupContext)
    {
        _startupContext = startupContext;

        startupContext.ComponentFreed += OnComponentFreed;
        startupContext.Cleanup += OnCleanup;
    }

    public SafeComponentHandle<T> Get<T>() where T : unmanaged, IComponent.IManagedInterface
    {
        var uid = T.ComponentId;

        if (_safeHandles.TryGetValue(uid, out var existing))
        {
            return (SafeComponentHandle<T>)existing;
        }

        unsafe
        {
            var component = _startupContext.ComponentList.QueryComponent(uid);

            var typedHandle = T.FromComponentHandle(component.Handle);
            var typedComponent = *(T*)&typedHandle;

            var safeHandle = new SafeComponentHandle<T>(typedComponent, component.Handle);
            _safeHandles[uid] = safeHandle;
            return safeHandle;
        }
    }

    private void OnCleanup(object? sender, EventArgs e)
    {
        _startupContext.ComponentFreed -= OnComponentFreed;
        _startupContext.Cleanup -= OnCleanup;
    }

    private void OnComponentFreed(object? sender, IComponent e)
    {
        var handle = e.Handle;
        _safeHandles.Values.FirstOrDefault(x => x.Handle == handle)?.Free();
    }
}