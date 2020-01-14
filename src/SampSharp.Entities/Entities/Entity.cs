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
using System.Globalization;
using System.Linq;
using System.Reflection;
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents an entity.
    /// </summary>
    public sealed class Entity
    {
        private readonly List<Entity> _children = new List<Entity>();
        private readonly List<Component> _components = new List<Component>();
        private readonly EntityManager _manager;
        private Entity _parent;

        internal Entity(EntityManager manager, Entity parent, EntityId id)
        {
            _manager = manager;
            Id = id;
            Parent = parent;
        }

        /// <summary>
        /// Gets the identifier of this entity.
        /// </summary>
        public EntityId Id { get; }

        /// <summary>
        /// Gets or sets the parent entity of this entity.
        /// </summary>
        public Entity Parent
        {
            get => _parent;
            set
            {
                if (value == _parent)
                    return;

                _parent?._children.Remove(this);
                value?._children.Add(this);

                _parent = value;
            }
        }

        /// <summary>
        /// Gets the manager of this entity.
        /// </summary>
        public IEntityManager Manager => _manager;

        /// <summary>
        /// Gets a collection of child entities of this entity.
        /// </summary>
        public IEnumerable<Entity> Children => _children.AsReadOnly();

        /// <summary>
        /// Destroys this entity, its components and its children.
        /// </summary>
        public void Destroy()
        {
            foreach (var child in _children) child.Destroy();
            _children.Clear();


            if (_components != null)
            {
                foreach (var component in _components) component.DestroyComponent();
                _components.Clear();
            }

            Parent = null;

            _manager.Remove(this);
        }

        /// <summary>
        /// Destroys the specified component in this entity.
        /// </summary>
        /// <param name="component">The component to destroy.</param>
        /// <returns><c>true</c> if the component was successfully destroyed.</returns>
        public bool Destroy(Component component)
        {
            if (component == null || !_components.Contains(component))
                return false;

            component.DestroyComponent();
            component.Entity = null;

            _components.Remove(component);
            return true;
        }

        /// <summary>
        /// Destroys the components with the specified type <typeparamref name="T" /> in this entity.
        /// </summary>
        /// <typeparam name="T">The type of the components to remove.</typeparam>
        /// <returns><c>true</c> if a component was successfully destroyed.</returns>
        public bool Destroy<T>() where T : Component
        {
            var components = GetComponents<T>().ToArray();

            return components.Aggregate(false, (current, component) => current | Destroy(component));
        }

        /// <summary>
        /// Adds a new component of the specified type <typeparamref name="T" /> to this entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity to add.</typeparam>
        /// <param name="args">The arguments for the constructor of the component.</param>
        /// <returns>The added component.</returns>
        public T AddComponent<T>(params object[] args) where T : Component
        {
            return (T) AddComponent(typeof(T), args);
        }

        /// <summary>
        /// Adds a new component of the specified <paramref name="type" /> to this entity.
        /// </summary>
        /// <param name="type">The type of the component to add.</param>
        /// <param name="args">The arguments for the constructor of the component.</param>
        /// <returns>The added component.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="type" /> is null.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the specified <paramref name="type" /> is not instantiable or is not a
        /// subclass of <see cref="Component" />.
        /// </exception>
        public object AddComponent(Type type, params object[] args)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (!typeof(Component).IsAssignableFrom(type) || !type.IsClass || type.IsAbstract)
                throw new ArgumentException("The type must be a subtype of Component", nameof(type));

            var component = typeof(NativeComponent).IsAssignableFrom(type)
                ? (Component) NativeObjectProxyFactory.CreateInstance(type, args)
                : (Component) Activator.CreateInstance(type, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, args, null);
            component.Entity = this;

            _components.Add(component);

            component.InitializeComponent();

            return component;
        }

        /// <summary>
        /// Gets a component of the specified type <typeparamref name="T" /> attached to this entity.
        /// </summary>
        /// <typeparam name="T">The type of the component to find.</typeparam>
        /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
        public T GetComponent<T>() where T : Component
        {
            return _components.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Gets all components of the specified type <typeparamref name="T" /> attached to this entity.
        /// </summary>
        /// <typeparam name="T">The type of the components to find.</typeparam>
        /// <returns>A collection of the found components.</returns>
        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            return _components.OfType<T>();
        }

        /// <summary>
        /// Gets a component of the specified type <typeparamref name="T" /> attached to a child entity of this entity using
        /// a depth first search.
        /// </summary>
        /// <typeparam name="T">The type of the component to find.</typeparam>
        /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
        public T GetComponentInChildren<T>() where T : Component
        {
            foreach (var c in _children)
            {
                var component = c.GetComponent<T>() ?? c.GetComponentInChildren<T>();

                if (component != null)
                    return component;
            }

            return null;
        }

        /// <summary>
        /// Gets all components of the specified type <typeparamref name="T" /> attached to a child entity of this entity.
        /// </summary>
        /// <typeparam name="T">The type of the components to find.</typeparam>
        /// <returns>A collection of the found components.</returns>
        public IEnumerable<T> GetComponentsInChildren<T>() where T : Component
        {
            if (_children.Count == 0)
                return EmptyArray<T>.Instance;

            var result = new List<T>();
            foreach (var c in _children)
                c.GetComponentsInCurrentAndChildren(result);
            return result;
        }

        /// <summary>
        /// Gets a component of the specified type <typeparamref name="T" /> attached to a parent entity of this entity.
        /// </summary>
        /// <typeparam name="T">The type of the component to find.</typeparam>
        /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
        public T GetComponentInParent<T>() where T : Component
        {
            return _parent?.GetComponent<T>() ?? _parent?.GetComponentInParent<T>();
        }

        /// <summary>
        /// Gets all components of the specified type <typeparamref name="T" /> attached to a parent entity of this entity.
        /// </summary>
        /// <typeparam name="T">The type of the components to find.</typeparam>
        /// <returns>A collection of the found components.</returns>
        public IEnumerable<T> GetComponentsInParent<T>() where T : Component
        {
            if (_parent == null)
                return EmptyArray<T>.Instance;

            var result = new List<T>();
            _parent?.GetComponentsInCurrentAndParent(result);
            return result;
        }

        private void GetComponentsInCurrentAndChildren<T>(List<T> result) where T : Component
        {
            result.AddRange(_components.OfType<T>());

            foreach (var c in _children)
                c.GetComponentsInCurrentAndChildren(result);
        }

        private void GetComponentsInCurrentAndParent<T>(List<T> result) where T : Component
        {
            result.AddRange(_components.OfType<T>());

            _parent?.GetComponentsInCurrentAndParent(result);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"(Id: {Id})";
        }

        private static class EmptyArray<T>
        {
            public static readonly T[] Instance = new T[0];
        }
    }
}