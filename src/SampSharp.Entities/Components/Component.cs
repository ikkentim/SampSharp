// SampSharp
// Copyright 2022 Tim Potze
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

using System.Diagnostics.Contracts;

namespace SampSharp.Entities;

/// <summary>Represents a component which can be attached to an entity.</summary>
public abstract class Component
{
    /// <summary>Gets the manager of the entity of this component.</summary>
    protected internal IEntityManager Manager { get; internal set; }

    /// <summary>Gets the parent entity of the entity to which this component has been attached.</summary>
    public virtual EntityId Parent => Manager.GetParent(Entity);

    /// <summary>Gets the entity to which this component has been attached.</summary>
    public virtual EntityId Entity { get; internal set; }

    /// <summary>Gets a value indicating whether this component is alive (has not been destroyed).</summary>
    public virtual bool IsComponentAlive { get; private set; } = true;

    /// <summary>Gets a component of the specified type <typeparamref name="T" /> attached to the entity.</summary>
    /// <typeparam name="T">The type of the component to find.</typeparam>
    /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
    [Pure]
    public virtual T GetComponent<T>() where T : Component
    {
        return Manager.GetComponent<T>(Entity);
    }

    /// <summary>Adds a component of the specified type <typeparamref name="T" /> to the entity with the specified constructor <paramref name="args" />.</summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <param name="args">The arguments of the constructor of the component.</param>
    /// <returns>The created component.</returns>
    public virtual T AddComponent<T>(params object[] args) where T : Component
    {
        return Manager.AddComponent<T>(Entity, args);
    }

    /// <summary>Adds a component of the specified type <typeparamref name="T" /> to the entity.</summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>The created component.</returns>
    public virtual T AddComponent<T>() where T : Component
    {
        return Manager.AddComponent<T>(Entity);
    }

    /// <summary>Destroys the components of the specified type <typeparamref name="T" /> attached to the entity.</summary>
    /// <typeparam name="T">The type of the components to destroy.</typeparam>
    public virtual void DestroyComponents<T>() where T : Component
    {
        Manager.Destroy<T>(Entity);
    }

    /// <summary>Destroys the entity.</summary>
    public virtual void DestroyEntity()
    {
        Manager.Destroy(Entity);
    }

    /// <summary>Destroys this component.</summary>
    public virtual void Destroy()
    {
        Manager.Destroy(this);
    }

    /// <summary>Gets all components of the specified type <typeparamref name="T" /> attached to the entity.</summary>
    /// <typeparam name="T">The type of the components to find.</typeparam>
    /// <returns>A collection of the found components.</returns>
    [Pure]
    public virtual T[] GetComponents<T>() where T : Component
    {
        return Manager.GetComponents<T>(Entity);
    }

    /// <summary>Gets a component of the specified type <typeparamref name="T" /> attached to a child entity of the entity using a depth first search.</summary>
    /// <typeparam name="T">The type of the component to find.</typeparam>
    /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
    [Pure]
    public virtual T GetComponentInChildren<T>() where T : Component
    {
        return Manager.GetComponentInChildren<T>(Entity);
    }

    /// <summary>Gets all components of the specified type <typeparamref name="T" /> attached to a child entity of the entity.</summary>
    /// <typeparam name="T">The type of the components to find.</typeparam>
    /// <returns>A collection of the found components.</returns>
    [Pure]
    public virtual T[] GetComponentsInChildren<T>() where T : Component
    {
        return Manager.GetComponentsInChildren<T>(Entity);
    }

    /// <summary>Gets a component of the specified type <typeparamref name="T" /> attached to a parent entity of the entity.</summary>
    /// <typeparam name="T">The type of the component to find.</typeparam>
    /// <returns>The found component or <c>null</c> if no component of the specified type could be found.</returns>
    [Pure]
    public virtual T GetComponentInParent<T>() where T : Component
    {
        return Manager.GetComponentInParent<T>(Entity);
    }

    /// <summary>Gets all components of the specified type <typeparamref name="T" /> attached to a parent entity of the entity.</summary>
    /// <typeparam name="T">The type of the components to find.</typeparam>
    /// <returns>A collection of the found components.</returns>
    [Pure]
    public virtual T[] GetComponentsInParent<T>() where T : Component
    {
        return Manager.GetComponentsInParent<T>(Entity);
    }

    /// <summary>This method is invoked after this component has been attached the an entity.</summary>
    protected virtual void OnInitializeComponent()
    {
    }

    /// <summary>This method is invoked before this component is destroyed and removed from its entity.</summary>
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
        IsComponentAlive = false;
    }

    /// <summary>Implements the operator true. Returns <c>true</c> if the specified <paramref name="component" /> is alive.</summary>
    /// <param name="component">The component.</param>
    /// <returns><c>true</c> if the specified <paramref name="component" /> is alive; <c>false</c> otherwise.</returns>
    public static bool operator true(Component component)
    {
        return component is { IsComponentAlive: true };
    }

    /// <summary>Implements the operator false. Returns <c>true</c> if the specified <paramref name="component" /> is not alive.</summary>
    /// <param name="component">The component.</param>
    /// <returns><c>true</c> if the specified <paramref name="component" /> is not alive; <c>false</c> otherwise.</returns>
    public static bool operator false(Component component)
    {
        return component is not { IsComponentAlive: true };
    }

    /// <summary>Implements the operator !. Returns <c>true</c> if the specified <paramref name="component" /> is not alive.</summary>
    /// <param name="component">The component.</param>
    /// <returns><c>true</c> if the specified <paramref name="component" /> is not alive; otherwise <c>false</c>.</returns>
    public static bool operator !(Component component)
    {
        return component is not { IsComponentAlive: true };
    }
}
