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

using System.Collections.Generic;

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents a component which can be attached to an <see cref="Entity" />.
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// Gets the <see cref="Entities.Entity" /> to which this component has been attached.
        /// </summary>
        public Entity Entity { get; internal set; }

        /// <summary>
        /// Gets a component of the specified type <typeparamref name="T" /> attached to the <see cref="Entity" />.
        /// </summary>
        /// <typeparam name="T">The type of the component to find.</typeparam>
        /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
        public T GetComponent<T>() where T : Component
        {
            return Entity.GetComponent<T>();
        }

        /// <summary>
        /// Gets all components of the specified type <typeparamref name="T" /> attached to the <see cref="Entity" />.
        /// </summary>
        /// <typeparam name="T">The type of the components to find.</typeparam>
        /// <returns>A collection of the found components.</returns>
        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            return Entity.GetComponents<T>();
        }

        /// <summary>
        /// Gets a component of the specified type <typeparamref name="T" /> attached to a child entity of the
        /// <see cref="Entity" /> using a depth first search.
        /// </summary>
        /// <typeparam name="T">The type of the component to find.</typeparam>
        /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
        public T GetComponentInChildren<T>() where T : Component
        {
            return Entity.GetComponentInChildren<T>();
        }

        /// <summary>
        /// Gets all components of the specified type <typeparamref name="T" /> attached to a child entity of the
        /// <see cref="Entity" />.
        /// </summary>
        /// <typeparam name="T">The type of the components to find.</typeparam>
        /// <returns>A collection of the found components.</returns>
        public IEnumerable<T> GetComponentsInChildren<T>() where T : Component
        {
            return Entity.GetComponentsInChildren<T>();
        }

        /// <summary>
        /// Gets a component of the specified type <typeparamref name="T" /> attached to a parent entity of the
        /// <see cref="Entity" />.
        /// </summary>
        /// <typeparam name="T">The type of the component to find.</typeparam>
        /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
        public T GetComponentInParent<T>() where T : Component
        {
            return Entity.GetComponentInParent<T>();
        }

        /// <summary>
        /// Gets all components of the specified type <typeparamref name="T" /> attached to a parent entity of the
        /// <see cref="Entity" />.
        /// </summary>
        /// <typeparam name="T">The type of the components to find.</typeparam>
        /// <returns>A collection of the found components.</returns>
        public IEnumerable<T> GetComponentsInParent<T>() where T : Component
        {
            return Entity.GetComponentsInParent<T>();
        }

        /// <summary>
        /// This method is invoked after this component has been attached the an entity.
        /// </summary>
        protected virtual void OnInitializeComponent()
        {
        }

        /// <summary>
        /// This method is invoked before this component is destroyed and removed from its entity.
        /// </summary>
        protected virtual void OnDestroyComponent()
        {
        }

        internal void InitializeComponent()
        {
            OnInitializeComponent();
        }

        internal void DestroyComponent()
        {
            OnDestroyComponent();
        }
    }
}