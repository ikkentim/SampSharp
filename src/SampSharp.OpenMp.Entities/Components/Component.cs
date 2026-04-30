using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace SampSharp.Entities;

/// <summary>Represents a component which can be attached to an entity.</summary>
public abstract class Component
{
    private IEntityManager? _manager;

    /// <summary>Gets the manager of the entity of this component.</summary>
    protected internal virtual IEntityManager Manager
    {
        get => _manager ?? throw new InvalidOperationException("Component not yet added to entity manager.");
        internal set => _manager = value;
    }

    /// <summary>Gets the parent entity of the entity to which this component has been attached.</summary>
    public EntityId Parent => Manager.GetParent(Entity);

    /// <summary>Gets the entity to which this component has been attached.</summary>
    public virtual EntityId Entity { get; internal set; }

    /// <summary>Gets a value indicating whether this component is alive (has not been destroyed).</summary>
    public bool IsComponentAlive { get; private set; } = true;

    /// <summary>
    /// Gets a value indicating whether this component is being destroyed. This property is set to <see langword="true" /> when the
    /// destruction of this component has been initiated.
    /// </summary>
    public bool IsDestroying { get; private set; }

    /// <summary>Gets a component of the specified type <typeparamref name="T" /> attached to the entity.</summary>
    /// <typeparam name="T">The type of the component to find.</typeparam>
    /// <returns>The found component or <see langword="null" /> if no component of the specified type could be found.</returns>
    [Pure]
    public T? GetComponent<T>() where T : Component
    {
        return Manager.GetComponent<T>(Entity);
    }

    /// <summary>Adds a component of the specified type <typeparamref name="T" /> to the entity with the specified
    /// constructor <paramref name="args" />.</summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <param name="args">The arguments of the constructor of the component.</param>
    /// <returns>The created component.</returns>
    public T AddComponent<T>(params object[] args) where T : Component
    {
        return Manager.AddComponent<T>(Entity, args);
    }

    /// <summary>Adds a component of the specified type <typeparamref name="T" /> to the entity.</summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
    /// <returns>The created component.</returns>
    public T AddComponent<T>() where T : Component
    {
        return Manager.AddComponent<T>(Entity);
    }

    /// <summary>Adds a component of the specified type <typeparamref name="T" /> to the entity.</summary>
    /// <typeparam name="T">The type of the component to add.</typeparam>
	/// <param name="component">The instance of component to be added.</param>
    public void AddComponent<T>(T component) where T : Component
    {
        Manager.AddComponent(Entity, component);
    }

    /// <summary>Destroys the components of the specified type <typeparamref name="T" /> attached to the
    /// entity.</summary>
    /// <typeparam name="T">The type of the components to destroy.</typeparam>
    public void DestroyComponents<T>() where T : Component
    {
        Manager.Destroy<T>(Entity);
    }

    /// <summary>Destroys the entity.</summary>
    public void DestroyEntity()
    {
        Manager.Destroy(Entity);
    }

    /// <summary>Destroys this component.</summary>
    public void Destroy()
    {
        Manager.Destroy(this);
    }

    /// <summary>Gets all components of the specified type <typeparamref name="T" /> attached to the entity.</summary>
    /// <typeparam name="T">The type of the components to find.</typeparam>
    /// <returns>A collection of the found components.</returns>
    [Pure]
    public T[] GetComponents<T>() where T : Component
    {
        return Manager.GetComponents<T>(Entity);
    }

    /// <summary>Gets a component of the specified type <typeparamref name="T" /> attached to a child entity of the
    /// entity using a depth first search.</summary>
    /// <typeparam name="T">The type of the component to find.</typeparam>
    /// <returns>The found component or <see langword="null" /> if no component of the specified type could be found.</returns>
    [Pure]
    public T? GetComponentInChildren<T>() where T : Component
    {
        return Manager.GetComponentInChildren<T>(Entity);
    }

    /// <summary>Gets all components of the specified type <typeparamref name="T" /> attached to a child entity of the
    /// entity.</summary>
    /// <typeparam name="T">The type of the components to find.</typeparam>
    /// <returns>A collection of the found components.</returns>
    [Pure]
    public T[] GetComponentsInChildren<T>() where T : Component
    {
        return Manager.GetComponentsInChildren<T>(Entity);
    }

    /// <summary>Gets a component of the specified type <typeparamref name="T" /> attached to a parent entity of the
    /// entity.</summary>
    /// <typeparam name="T">The type of the component to find.</typeparam>
    /// <returns>The found component or <see langword="null" /> if no component of the specified type could be found.</returns>
    [Pure]
    public T? GetComponentInParent<T>() where T : Component
    {
        return Manager.GetComponentInParent<T>(Entity);
    }

    /// <summary>Gets all components of the specified type <typeparamref name="T" /> attached to a parent entity of the
    /// entity.</summary>
    /// <typeparam name="T">The type of the components to find.</typeparam>
    /// <returns>A collection of the found components.</returns>
    [Pure]
    public T[] GetComponentsInParent<T>() where T : Component
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

    internal void InvokeInitialize()
    {
        OnInitializeComponent();
    }

    internal void InvokedDestroy()
    {
        IsDestroying = true;
        OnDestroyComponent();
        IsComponentAlive = false;
    }

    /// <summary>Implements the operator true. Returns <see langword="true" /> if the specified <paramref name="component" /> is
    /// alive.</summary>
    /// <param name="component">The component.</param>
    /// <returns><see langword="true" /> if the specified <paramref name="component" /> is alive; <see langword="false" /> otherwise.</returns>
    public static bool operator true([NotNullWhen(true)]Component? component)
    {
        return component is { IsComponentAlive: true };
    }

    /// <summary>Implements the operator false. Returns <see langword="true" /> if the specified <paramref name="component" /> is
    /// not alive.</summary>
    /// <param name="component">The component.</param>
    /// <returns><see langword="true" /> if the specified <paramref name="component" /> is not alive; <see langword="false" />
    /// otherwise.</returns>
    public static bool operator false([NotNullWhen(false)]Component? component)
    {
        return component is not { IsComponentAlive: true };
    }

    /// <summary>Implements the operator !. Returns <see langword="true" /> if the specified <paramref name="component" /> is not
    /// alive.</summary>
    /// <param name="component">The component.</param>
    /// <returns><see langword="true" /> if the specified <paramref name="component" /> is not alive; otherwise
    /// <see langword="false" />.</returns>
    public static bool operator !([NotNullWhen(false)]Component? component)
    {
        return component is not { IsComponentAlive: true };
    }
}