using System.Reflection;
using Microsoft.Extensions.ObjectPool;

namespace SampSharp.Entities;

internal class EntityManager : IEntityManager
{
    private readonly ComponentStore _components;
    private readonly Dictionary<EntityId, EntityNode> _entities = new(100);
    private readonly ObjectPool<EntityNode> _entityPool;
    private readonly HashSet<EntityId> _rootEntities = new(100);

    public EntityManager()
    {
        var componentPool = new DefaultObjectPool<ComponentNode>(new ComponentNodeObjectPolicy(), 100);
        _entityPool = new DefaultObjectPool<EntityNode>(new EntityNodeObjectPolicy(componentPool), 100);
        _components = new ComponentStore(componentPool);
    }

    private EntityNode AddEntityNode(EntityId entity, EntityId parent)
    {
        if (!_entities.TryGetValue(entity, out var entityNode))
        {
            // New entity
            EntityNode? parentNode = null;

            if (parent != default && !_entities.TryGetValue(parent, out parentNode))
            {
                // Parent is new too
                parentNode = AddEntityNode(parent, default);
            }

            // Create the entity
            _entities[entity] = entityNode = _entityPool.Get();
            entityNode.Id = entity;

            if (parentNode != null)
            {
                // Assign the parent in the node
                entityNode.Parent = parentNode;

                // Append the entity to the parent
                parentNode.AppendChild(entityNode);
            }

            // Keep track of roots
            if (parentNode == null)
            {
                _rootEntities.Add(entity);
            }
        }
        else
        {
            // Existing entity
            if (parent != default)
            {
                // Verify parent matches
                if (entityNode.Parent == null || entityNode.Parent.Id != parent)
                {
                    throw new ArgumentException("The specified parent does not match the current parent entity.",
                        nameof(parent));
                }
            }
        }

        return entityNode;
    }

    private void RemoveOne<T>(EntityNode node, T component) where T : Component
    {
        node.Components.Remove(component);
        _components.Remove(component);

        component.InvokedDestroy();

        if (node.IsEmpty)
        {
            RemoveOne(node);
        }
    }

    private void RemoveOne(EntityNode node)
    {
        if (node.Next != null)
        {
            node.Next.Previous = node.Previous;
        }

        if (node.Previous != null)
        {
            node.Previous.Next = node.Next;
        }

        if (node.Parent == null)
        {
            _rootEntities.Remove(node.Id);
        }
        else
        {
            node.Parent.ChildCount--;
            if (node.Parent.FirstChild == node)
            {
                node.Parent.FirstChild = node.Next;
            }

            if (node.Parent.IsEmpty)
            {
                RemoveOne(node.Parent);
            }
        }
    }

    public T AddComponent<T>(EntityId entity, params object[] args) where T : Component
    {
        return AddComponent<T>(entity, default, args);
    }

    public T AddComponent<T>(EntityId entity) where T : Component
    {
        return AddComponent<T>(entity, default, []);
    }

    public T AddComponent<T>(EntityId entity, EntityId parent) where T : Component
    {
        return AddComponent<T>(entity, parent, []);
    }

    public void AddComponent<T>(EntityId entity, T component) where T : Component
    {
        AddComponent(entity, default, component);
    }

    public T AddComponent<T>(EntityId entity, EntityId parent, params object[] args) where T : Component
    {
        var component = (T)Activator.CreateInstance(typeof(T),
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, args, null)!;

        AddComponent(entity, parent, component);

        return component;
    }

    public void AddComponent<T>(EntityId entity, EntityId parent, T component) where T : Component
    {
        var entityNode = AddEntityNode(entity, parent);

        // Connect component to entity
        component.Entity = entity;
        component.Manager = this;

        // Add component to components store of entity and global components store
        entityNode.Components.Add(component);
        _components.Add(component);

        component.InvokeInitialize();
    }

    public void Destroy(Component component)
    {
        ArgumentNullException.ThrowIfNull(component);

        if (!component.IsComponentAlive)
        {
            return;
        }

        if (!_entities.TryGetValue(component.Entity, out var node))
        {
            throw new InvalidOperationException("Invalid entity manager state.");
        }

        RemoveOne(node, component);
    }

    public void Destroy(EntityId entity)
    {
        if (!_entities.TryGetValue(entity, out var node))
        {
            return;
        }

        while (node.FirstChild != null)
        {
            Destroy(node.FirstChild.Id);
        }

        foreach (var component in node.Components.GetAll<Component>())
        {
            if (!component.IsComponentAlive)
            {
                continue;
            }

            node.Components.Remove(component);
            _components.Remove(component);
            component.InvokedDestroy();
        }

        RemoveOne(node);
    }

    public void Destroy<T>(EntityId entity) where T : Component
    {
        if (!_entities.TryGetValue(entity, out var node))
        {
            return;
        }

        var count = node.Components.GetCount<T>();

        switch (count)
        {
            case 0:
                return;
            case 1:
                {
                    var component = node.Components.Get<T>()!;
                    RemoveOne(node, component);
                    break;
                }
            default:
                {
                    var components = node.Components.GetAll<T>();

                    foreach (var component in components)
                    {
                        RemoveOne(node, component);
                    }

                    break;
                }
        }
    }

    public EntityId[] GetChildren(EntityId entity)
    {
        if (!_entities.TryGetValue(entity, out var node))
        {
            return [];
        }

        var result = new EntityId[node.ChildCount];

        var current = node.FirstChild;
        var index = 0;
        while (current != null)
        {
            result[index++] = current.Id;
            current = current.Next;
        }

        return result;
    }

    public EntityId[] GetRootEntities()
    {
        if (_rootEntities.Count == 0)
        {
            return [];
        }

        var result = new EntityId[_rootEntities.Count];
        _rootEntities.CopyTo(result);
        return result;
    }

    public T? GetComponent<T>() where T : Component
    {
        return _components.Get<T>();
    }

    public T? GetComponent<T>(EntityId entity) where T : Component
    {
        return _entities.TryGetValue(entity, out var node)
            ? node.Components.Get<T>()
            : null;
    }

    public T? GetComponentInChildren<T>(EntityId entity) where T : Component
    {
        if (!_entities.TryGetValue(entity, out var entityNode))
        {
            return null;
        }

        return Search(entityNode);

        static T? Search(EntityNode node)
        {
            // shallow search
            var current = node.FirstChild;
            while (current != null)
            {
                var result = current.Components.Get<T>();
                if (result != null)
                {
                    return result;
                }

                current = current.Next;
            }

            // deep search
            current = node.FirstChild;

            while (current != null)
            {
                var result = Search(current);
                if (result != null)
                {
                    return result;
                }

                current = current.Next;
            }

            return null;
        }
    }

    public T? GetComponentInParent<T>(EntityId entity) where T : Component
    {
        _entities.TryGetValue(entity, out var entry);

        var current = entry?.Parent;

        while (current != null)
        {
            var result = current.Components.Get<T>();

            if (result != null)
            {
                return result;
            }

            current = current.Parent;
        }

        return null;
    }

    public T[] GetComponents<T>() where T : Component
    {
        return _components.GetAll<T>();
    }

    public T[] GetComponents<T>(EntityId entity) where T : Component
    {
        return _entities.TryGetValue(entity, out var node)
            ? node.Components.GetAll<T>()
            : [];
    }

    public T[] GetComponentsInChildren<T>(EntityId entity) where T : Component
    {
        if (!_entities.TryGetValue(entity, out var entityNode))
        {
            return [];
        }

        var count = Count(entityNode);

        if (count == 0)
        {
            return [];
        }

        var result = new T[count];

        Search(result, 0, entityNode);
        return result;

        static int Count(EntityNode node)
        {
            var result = 0;
            var current = node.FirstChild;

            while (current != null)
            {
                var components = current.Components.GetCount<T>();
                result += components;

                Count(current);

                current = current.Next;
            }

            return result;
        }

        static int Search(T[] array, int startIndex, EntityNode node)
        {
            var index = startIndex;
            var current = node.FirstChild;
            while (current != null)
            {
                index += current.Components.GetAll(array, index);
                current = current.Next;
            }

            current = node.FirstChild;

            while (current != null)
            {
                index += Search(array, index, current);
                current = current.Next;
            }

            return index - startIndex;
        }
    }

    public T[] GetComponentsInParent<T>(EntityId entity) where T : Component
    {
        if (!_entities.TryGetValue(entity, out var entityNode))
        {
            return [];
        }

        var count = Count(entityNode);

        if (count == 0)
        {
            return [];
        }

        var result = new T[count];
        Search(result, 0, entityNode);

        return result;

        static int Count(EntityNode node)
        {
            var result = 0;
            var current = node.Parent;

            while (current != null)
            {
                result += current.Components.GetCount<T>();
                current = current.Parent;
            }


            return result;
        }

        static void Search(T[] array, int startIndex, EntityNode node)
        {
            var index = startIndex;
            var current = node.Parent;
            while (current != null)
            {
                index += current.Components.GetAll(array, index);
                current = current.Parent;
            }
        }
    }

    public EntityId GetParent(EntityId entity)
    {
        if (_entities.TryGetValue(entity, out var node))
        {
            return node.Parent?.Id ?? default;
        }

        return default;
    }

    public bool Exists(EntityId entity)
    {
        return _entities.ContainsKey(entity);
    }
}