// SampSharp
// Copyright 2020 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Reflection;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents the entity manager.
    /// </summary>
    /// <seealso cref="IEntityManager" />
    public class EntityManager : IEntityManager
    {
        private readonly RecyclePool<ComponentEntry> _componentPool = new RecyclePool<ComponentEntry>(512);
        private readonly ComponentStore _components = new ComponentStore();
        private readonly Dictionary<EntityId, EntityEntry> _entities = new Dictionary<EntityId, EntityEntry>(new EntityIdEqualityComparer());
        private readonly RecyclePool<EntityEntry> _entityPool = new RecyclePool<EntityEntry>(512);
        private readonly RecyclePool<ComponentStore> _storePool = new RecyclePool<ComponentStore>(512);
        private readonly HashSet<EntityId> _entityIds = new HashSet<EntityId>(new EntityIdEqualityComparer());
        private EntityEntry _firstRoot;
        private int _rootCount;

        private class EntityIdEqualityComparer : IEqualityComparer<EntityId>
        {
            public bool Equals(EntityId x, EntityId y) => x.Handle == y.Handle && x.Type == y.Type;
            public int GetHashCode(EntityId obj) => obj.GetHashCode();
        }

        /// <inheritdoc />
        public EntityManager()
        {
            _components.ComponentPool = _componentPool;
        }

        /// <inheritdoc />
        public void Create(EntityId entity, EntityId parent = default)
        {
            EntityEntry parentEntry = null;

            if (!entity)
                throw new ArgumentException("The specified entity is empty.", nameof(entity));

            if (parent && !_entities.TryGetValue(parent, out parentEntry))
                throw new ArgumentException("The specified parent entity does not exist.", nameof(parent));

            // Create entry for the entity in storage
            var entry = _entityPool.New();

            entry.Id = entity;
            entry.Components = _storePool.New();
            entry.Components.ComponentPool = _componentPool;
            entry.Parent = parentEntry;

            // Attach to parent if parent is present
            if (parentEntry != null)
            {
                if (parentEntry.Child?.Previous != null)
                    throw new Exception("Invalid entity store state");

                // Attach to siblings if present
                entry.Next = parentEntry.Child;
                if (parentEntry.Child != null)
                    parentEntry.Child.Previous = entry;

                parentEntry.Child = entry;
                parentEntry.ChildCount++;
            }
            else
            {
                if (_firstRoot != null)
                {
                    _firstRoot.Previous = entry;
                    entry.Next = _firstRoot;
                }

                _rootCount++;
                _firstRoot = entry;
            }

            // Update id association table
            _entityIds.Add(entity);
            _entities[entity] = entry;
        }

        /// <inheritdoc />
        public void Destroy(EntityId entity)
        {
            if (!_entities.TryGetValue(entity, out var entry))
                return;

            Destroy(entry);
        }
        
        /// <inheritdoc />
        public void Destroy<T>(EntityId entity) where T : Component
        {
            foreach (var c in GetComponents<T>(entity))
                Destroy(c);
        }

        /// <inheritdoc />
        public T AddComponent<T>(EntityId entity, params object[] args) where T : Component
        {
            if (!_entities.TryGetValue(entity, out var entityEntry))
                throw new EntityNotFoundException(nameof(entity));

            var component = typeof(NativeComponent).IsAssignableFrom(typeof(T))
                ? NativeObjectProxyFactory.CreateInstance<T>(args)
                : (T)Activator.CreateInstance(typeof(T),
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, args, null);

            component.Entity = entity;
            component.Manager = this;
            
            entityEntry.Components.Add(component);
            _components.Add(component);
            
            component.InitializeComponent();

            return component;
        }
        
        /// <inheritdoc />
        public T AddComponent<T>(EntityId entity) where T : Component
        {
            if (!_entities.TryGetValue(entity, out var entityEntry))
                throw new EntityNotFoundException(nameof(entity));

            var component = typeof(NativeComponent).IsAssignableFrom(typeof(T))
                ? NativeObjectProxyFactory.CreateInstance<T>()
                : (T)Activator.CreateInstance(typeof(T),
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, null, null);
            
            component.Entity = entity;
            component.Manager = this;
            
            entityEntry.Components.Add(component);
            _components.Add(component);
            
            component.InitializeComponent();

            return component;
        }

        /// <inheritdoc />
        public EntityId GetParent(EntityId entity)
        {
            if (!_entities.TryGetValue(entity, out var entityEntry))
                throw new EntityNotFoundException(nameof(entity));

            return entityEntry.Parent.Id;
        }
        
        /// <inheritdoc />
        public bool Exists(EntityId entity)
        {
            return _entityIds.Contains(entity);
        }

        /// <inheritdoc />
        public void Destroy(Component component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));

            if (!_entities.TryGetValue(component.Entity, out var entityEntry))
                throw new EntityNotFoundException(nameof(component));

            component.DestroyComponent();
            entityEntry.Components.Remove(component);
            _components.Remove(component);
        }
        
        /// <inheritdoc />
        public T[] GetComponents<T>() where T : Component
        {
            return _components.GetAll<T>();
        }

        /// <inheritdoc />
        public EntityId[] GetRootEntities()
        {
            if (_rootCount == 0)
                return Array.Empty<EntityId>();

            var result = new EntityId[_rootCount];

            var current = _firstRoot;
            var index = 0;
            while (current != null)
            {
                result[index++] = current.Id;
                current = current.Next;
            }

            return result;
        }

        /// <inheritdoc />
        public T GetComponent<T>() where T : Component
        {
            return _components.Get<T>();
        }
        
        /// <inheritdoc />
        public T[] GetComponents<T>(EntityId entity) where T : Component
        {
            if (!_entities.TryGetValue(entity, out var entityEntry))
                throw new EntityNotFoundException(nameof(entity));

            return entityEntry.Components.GetAll<T>();
        }
        
        /// <inheritdoc />
        public T GetComponent<T>(EntityId entity) where T : Component
        {
            if (!_entities.TryGetValue(entity, out var entityEntry))
                throw new EntityNotFoundException(nameof(entity));

            return entityEntry.Components.Get<T>();
        }
        
        /// <inheritdoc />
        public T[] GetComponentsInParent<T>(EntityId entity) where T : Component
        {
            if (!_entities.TryGetValue(entity, out var entityEntry))
                throw new EntityNotFoundException(nameof(entity));

            var current = entityEntry.Parent;

            var length = 0;
            while (current != null)
            {
                length += current.Components.GetCount<T>();
                current = current.Parent;
            }

            if (length == 0)
                return Array.Empty<T>();

            var result = new T[length];
            var index = 0;

            current = entityEntry.Parent;

            while (current != null)
            {
                index += current.Components.GetAll(result, index);
                current = current.Parent;
            }

            return result;
        }
        
        /// <inheritdoc />
        public T GetComponentInParent<T>(EntityId entity) where T : Component
        {
            if (!_entities.TryGetValue(entity, out var entityEntry))
                throw new EntityNotFoundException(nameof(entity));

            var current = entityEntry.Parent;

            while (current != null)
            {
                var result = current.Components.Get<T>();

                if (result != null)
                    return result;

                current = current.Parent;
            }

            return null;
        }
        
        /// <inheritdoc />
        public T GetComponentInChildren<T>(EntityId entity) where T : Component
        {
            if (!_entities.TryGetValue(entity, out var entityEntry))
                throw new EntityNotFoundException(nameof(entity));

            return GetComponentInChildren<T>(entityEntry.Child);
        }
        
        /// <inheritdoc />
        public T[] GetComponentsInChildren<T>(EntityId entity) where T : Component
        {
            if (!_entities.TryGetValue(entity, out var entityEntry))
                throw new EntityNotFoundException(nameof(entity));

            var length = GetComponentCountInChildren<T>(entityEntry.Child);

            if (length == 0)
                return Array.Empty<T>();

            var result = new T[length];
            GetComponentsInChildren(result, 0, entityEntry.Child);

            return result;
        }
        
        /// <inheritdoc />
        public EntityId[] GetChildren(EntityId entity)
        {
            if (!_entities.TryGetValue(entity, out var entityEntry))
                throw new EntityNotFoundException(nameof(entity));

            var result = new EntityId[entityEntry.ChildCount];

            var current = entityEntry.Child;
            var index = 0;
            while (current != null)
            {
                result[index++] = current.Id;
                current = current.Next;
            }

            return result;
        }

        private static T GetComponentInChildren<T>(EntityEntry current) where T : Component
        {
            while (current != null)
            {
                var result = GetComponentInChildren<T>(current.Child) ?? current.Components.Get<T>();

                if (result != null)
                    return result;

                current = current.Next;
            }

            return null;
        }

        private static int GetComponentCountInChildren<T>(EntityEntry current) where T : Component
        {
            var result = 0;

            while (current != null)
            {
                result += current.Components.GetCount<T>() + GetComponentCountInChildren<T>(current.Child);
                current = current.Next;
            }

            return result;
        }

        private static int GetComponentsInChildren<T>(T[] array, int startIndex, EntityEntry current) where T : Component
        {
            var index = startIndex;
            while (current != null)
            {
                index += current.Components.GetAll(array, index);
                index += GetComponentsInChildren(array, index, current.Child);

                current = current.Next;
            }

            return index - startIndex;
        }
        
        private void Destroy(EntityEntry entry)
        {
            var child = entry.Child;

            // Destroy each child
            while (child != null)
            {
                var current = child;
                child = current.Next;

                Destroy(current);
            }

            // Destroy components
            var component = entry.Components.First;
            while (component != null)
            {
                component.Component.DestroyComponent();
                _components.Remove(component.Component);

                component = component.Next;
            }

            _storePool.Recycle(entry.Components);

            // Remove from relations
            if (entry.Previous != null) entry.Previous.Next = entry.Next;
            if (entry.Next != null) entry.Next.Previous = entry.Previous;
            if (entry.Parent != null) entry.Parent.ChildCount--;
            else _rootCount--;
            if (_firstRoot == entry) _firstRoot = entry.Next;
            if (entry.Parent?.Child == entry) entry.Parent.Child = entry.Next;

            _entities.Remove(entry.Id);
            _entityIds.Remove(entry.Id);
            _entityPool.Recycle(entry);
        }

        private class ComponentStore : IRecyclable
        {
            private readonly Dictionary<Type, Data> _components = new Dictionary<Type, Data>();

            public RecyclePool<ComponentEntry> ComponentPool;
            public ComponentEntry First => _components.TryGetValue(typeof(Component), out var data) ? data.Entry : null;

            public void Reset()
            {
                foreach (var kv in _components)
                {
                    var entry = kv.Value.Entry;
                    while (entry != null)
                    {
                        var current = entry;
                        entry = entry.Next;

                        ComponentPool.Recycle(current);
                    }
                }

                _components.Clear();
                ComponentPool = null;
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
                    return Array.Empty<T>();

                var result = new T[data.Count];

                var index = 0;
                var current = data.Entry;

                while (current != null)
                {
                    result[index++] = (T)current.Component;
                    current = current.Next;
                }

                return result;
            }

            public int GetAll<T>(T[] array, int startIndex) where T : Component
            {
                if (!_components.TryGetValue(typeof(T), out var data))
                    return 0;

                var index = startIndex;
                var current = data.Entry;

                if (array.Length < index + data.Count)
                    throw new ArgumentException(
                        "The length of the array is less than the number of elements to be copied.", nameof(array));
                while (current != null)
                {
                    array[index++] = (T)current.Component;
                    current = current.Next;
                }

                return index - startIndex;
            }

            public T Get<T>() where T : Component
            {
                return !_components.TryGetValue(typeof(T), out var data)
                    ? null
                    : data.Entry?.Component as T;
            }

            private void Add(Type type, Component component)
            {
                var newEntry = ComponentPool.New();
                newEntry.Component = component;

                if (_components.TryGetValue(type, out var data))
                {
                    newEntry.Next = data.Entry;
                    if (data.Entry != null)
                        data.Entry.Previous = newEntry;
                    data.Entry = newEntry;
                    data.Count++;
                    _components[type] = data;
                }
                else
                {
                    _components[type] = new Data
                    {
                        Entry = newEntry,
                        Count = 1
                    };
                }
            }

            private void Remove(Type type, Component component)
            {
                if (!_components.TryGetValue(type, out var data))
                    return;

                var entry = data.Entry;
                while (entry != null)
                {
                    if (entry.Component != component)
                    {
                        entry = entry.Next;
                        continue;
                    }

                    if (entry.Previous != null)
                        entry.Previous.Next = entry.Next;
                    if (entry.Next != null)
                        entry.Next.Previous = entry.Previous;
                    if (data.Entry == entry)
                        data.Entry = entry.Next;

                    ComponentPool.Recycle(entry);
                    data.Count--;
                    _components[type] = data;
                    return;
                }
            }

            public void Add<T>(T component) where T : Component
            {
                var current = component.GetType();
                
                Add(current, component);
                do
                {
                    // ReSharper disable once PossibleNullReferenceException
                    current = current.BaseType;

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
                    // ReSharper disable once PossibleNullReferenceException
                    current = current.BaseType;

                    Remove(current, component);
                } while (current != typeof(Component));
            }

            private struct Data
            {
                public ComponentEntry Entry;
                public int Count;
            }
        }

        private class ComponentEntry : IRecyclable
        {
            public Component Component;
            public ComponentEntry Next;
            public ComponentEntry Previous;

            public void Reset()
            {
                Component = null;
                Previous = null;
                Next = null;
            }
        }

        private class EntityEntry : IRecyclable
        {
            public EntityEntry Child;
            public int ChildCount;
            public ComponentStore Components;
            public EntityId Id;
            public EntityEntry Next;
            public EntityEntry Parent;
            public EntityEntry Previous;

            public void Reset()
            {
                Id = default;
                Next = null;
                Previous = null;
                Parent = null;
                Child = null;
                ChildCount = 0;
            }
        }
    }
}