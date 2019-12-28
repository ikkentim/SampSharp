// SampSharp
// Copyright 2019 Tim Potze
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
using System.Linq;
using SampSharp.EntityComponentSystem.Components;

namespace SampSharp.EntityComponentSystem.Entities
{
    /// <summary>
    /// Represents an entity.
    /// </summary>
    public sealed class Entity
    {
        private readonly List<Entity> _children = new List<Entity>();
        private readonly List<Component> _components = new List<Component>();

        internal Entity(Entity parent, EntityId id)
        {
            Parent = parent;
            Id = id;

            parent?._children.Add(this);
        }

        /// <summary>
        /// Gets the identifier of this entity.
        /// </summary>
        public EntityId Id { get; }

        /// <summary>
        /// Gets the parent entity of this entity.
        /// </summary>
        public Entity Parent { get; private set; }

        /// <summary>
        /// Gets a collection of child entities of this entity.
        /// </summary>
        public IEnumerable<Entity> Children => _children.AsReadOnly();

        /// <summary>
        /// Adds a new component of the specified type <typeparamref name="T" /> to this entity.
        /// </summary>
        /// <typeparam name="T">The type of the entity to add.</typeparam>
        public void AddComponent<T>() where T : Component, new()
        {
            AddComponent(typeof(T));
        }

        /// <summary>
        /// Adds a new component of the specified <paramref name="type" /> to this entity.
        /// </summary>
        /// <param name="type">The type of the component to add.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="type" /> is null.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the specified <paramref name="type" /> is not instantiable or is not a
        /// subclass of <see cref="Component" />.
        /// </exception>
        public void AddComponent(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (!typeof(Component).IsAssignableFrom(type) || !type.IsClass || type.IsAbstract)
                throw new ArgumentException("The type must be a subtype of Component", nameof(type));

            var component = (Component) Activator.CreateInstance(type);
            component.Entity = this;

            _components.Add(component);

            component.InitializeComponent();
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
            return Parent?.GetComponent<T>() ?? Parent?.GetComponentInParent<T>();
        }

        /// <summary>
        /// Gets all components of the specified type <typeparamref name="T" /> attached to a parent entity of this entity.
        /// </summary>
        /// <typeparam name="T">The type of the components to find.</typeparam>
        /// <returns>A collection of the found components.</returns>
        public IEnumerable<T> GetComponentsInParent<T>() where T : Component
        {
            if (Parent == null)
                return EmptyArray<T>.Instance;

            var result = new List<T>();
            Parent?.GetComponentsInCurrentAndParent(result);
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

            Parent?.GetComponentsInCurrentAndParent(result);
        }

        internal void Destroy()
        {
            Parent?._children.Remove(this);
            Parent = null;

            foreach (var component in _components) component.DestroyComponent();

            _components.Clear();
        }

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