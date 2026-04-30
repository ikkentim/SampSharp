using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.Entities;

internal sealed class SystemRegistry(IServiceProvider serviceProvider) : ISystemRegistry
{
    private Type[]? _systemTypes;
    private Dictionary<Type, ISystem[]>? _data;

    private List<Action>? _handlers = [];

    public void LoadSystems()
    {
        if (_data != null)
        {
            return;
        }

        _systemTypes = serviceProvider.GetServices<SystemEntry>()
            .Select(w => w.Type)
            .ToArray();

        _data = ExtractSystemTypeLookupTable(_systemTypes)
            .ToDictionary(x => x.Key, x => x.Value.ToArray());

        InvokeHandlers();
    }

    private Dictionary<Type, HashSet<ISystem>> ExtractSystemTypeLookupTable(Type[] systemTypes)
    {
        var data = new Dictionary<Type, HashSet<ISystem>>();
        foreach (var type in systemTypes)
        {
            if (serviceProvider.GetService(type) is not ISystem instance)
            {
                continue;
            }

            // Add the system to all types and interfaces it implements.
            var currentType = type;
            while (currentType != null && currentType != typeof(object))
            {
                if (!data.TryGetValue(currentType, out var set))
                {
                    data[currentType] = set = [];
                }

                set.Add(instance);

                currentType = currentType.BaseType;
            }

            foreach (var interfaceType in type.GetInterfaces()
                         .Where(t => typeof(ISystem).IsAssignableFrom(t)))
            {
                if (!data.TryGetValue(interfaceType, out var set))
                {
                    data[interfaceType] = set = [];
                }

                set.Add(instance);
            }
        }

        return data;
    }

    private void InvokeHandlers()
    {
        if (_handlers != null)
        {
            foreach (var handler in _handlers)
            {
                handler();
            }

            _handlers = null;
        }
    }

    public ReadOnlyMemory<ISystem> Get(Type type)
    {
        return _data?.TryGetValue(type, out var value) ?? false ? value : default(ReadOnlyMemory<ISystem>);
    }

    public ReadOnlyMemory<ISystem> Get<TSystem>() where TSystem : ISystem
    {
        return Get(typeof(TSystem));
    }

    public ReadOnlyMemory<Type> GetSystemTypes()
    {
        return _systemTypes?.AsMemory() ?? default;
    }

    public void Register(Action handler)
    {
        if (_handlers != null)
        {
            _handlers.Add(handler);
        }
        else
        {
            handler();
        }
    }
}