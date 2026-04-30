using Microsoft.Extensions.ObjectPool;

namespace SampSharp.Entities;

internal sealed class ComponentStore(ObjectPool<ComponentNode> componentPool)
{
    /// A list of components attached to the entity by component type. Components are stored in a linked list
    /// and are stored in the list associated with their type and every base type of their type.
    private readonly Dictionary<Type, Data> _components = new();

    public bool IsEmpty => _components.All(x => x.Value.Count == 0);

    public void ReturnComponents()
    {
        foreach (var kv in _components)
        {
            var entry = kv.Value.First;
            while (entry != null)
            {
                var current = entry;
                entry = entry.Next;

                componentPool.Return(current);
            }
        }

        _components.Clear();
    }

    public int GetCount<T>() where T : Component
    {
        return !_components.TryGetValue(typeof(T), out var data)
            ? 0
            : data.Count;
    }

    public T[] GetAll<T>() where T : Component
    {
        if (!_components.TryGetValue(typeof(T), out var data))
        {
            return [];
        }

        var result = new T[data.Count];

        var index = 0;
        var current = data.First;

        while (current != null)
        {
            result[index++] = (T)current.Component!;
            current = current.Next;
        }

        return result;
    }

    public int GetAll<T>(T[] array, int startIndex) where T : Component
    {
        if (!_components.TryGetValue(typeof(T), out var data))
        {
            return 0;
        }

        var index = startIndex;
        var current = data.First;

        if (array.Length < index + data.Count)
        {
            throw new ArgumentException("The length of the array is less than the number of elements to be copied.", nameof(array));
        }

        while (current != null)
        {
            array[index++] = (T)current.Component!;
            current = current.Next;
        }

        return index - startIndex;
    }

    public T? Get<T>() where T : Component
    {
        return !_components.TryGetValue(typeof(T), out var data)
            ? null
            : data.First?.Component as T;
    }

    public void Add<T>(T component) where T : Component
    {
        var current = component.GetType();

        Add(current, component);
        do
        {
            current = current.BaseType!;

            Add(current, component);
        } while (current != typeof(Component));
    }

    public void Remove(Component component)
    {
        var current = component.GetType();

        // Remove the component in every type list
        Remove(current, component);
        do
        {
            current = current.BaseType!;

            Remove(current, component);
        } while (current != typeof(Component));
    }

    private void Add(Type type, Component component)
    {
        var newEntry = componentPool.Get();
        newEntry.Component = component;

        if (_components.TryGetValue(type, out var data))
        {
            newEntry.Next = data.First;
            if (data.First != null)
            {
                data.First.Previous = newEntry;
            }

            data.First = newEntry;
            data.Count++;
            _components[type] = data;
        }
        else
        {
            _components[type] = new Data
            {
                First = newEntry,
                Count = 1
            };
        }
    }

    private void Remove(Type type, Component component)
    {
        if (!_components.TryGetValue(type, out var data))
        {
            return;
        }

        var entry = data.First;
        while (entry != null)
        {
            if (entry.Component != component)
            {
                entry = entry.Next;
                continue;
            }

            if (entry.Previous != null)
            {
                entry.Previous.Next = entry.Next;
            }

            if (entry.Next != null)
            {
                entry.Next.Previous = entry.Previous;
            }

            if (data.First == entry)
            {
                data.First = entry.Next;
            }

            componentPool.Return(entry);
            data.Count--;
            _components[type] = data;
            return;
        }
    }

    private struct Data
    {
        /// <summary>
        /// First linked list node.
        /// </summary>
        public ComponentNode? First;

        /// <summary>
        /// Number of components in the list.
        /// </summary>
        public int Count;
    }
}