namespace SampSharp.Entities;

/// <summary>
/// Represents an identifier of an entity.
/// </summary>
public readonly record struct EntityId
{
    /// <summary>
    /// An empty entity identifier.
    /// </summary>
    public static readonly EntityId Empty = new();

    private readonly Guid _id;

    private EntityId(Guid id)
    {
        _id = id;
    }

    /// <summary>
    /// Gets a value indicating whether this handle is empty.
    /// </summary>
    public bool IsEmpty => _id == Guid.Empty;

    /// <summary>
    /// Creates a fresh entity handle backed by a new <see cref="Guid" />.
    /// </summary>
    public static EntityId NewEntityId()
    {
        return new EntityId(Guid.NewGuid());
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return IsEmpty ? "(Empty)" : $"(Id = {_id})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Component" /> to <see cref="EntityId" />.
    /// Returns the entity of the component.
    /// </summary>
    public static implicit operator EntityId(Component component)
    {
        return component?.Entity ?? default;
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="EntityId" /> to <see cref="bool" />.
    /// Returns <see langword="true" /> if the specified <paramref name="value" /> is not empty.
    /// </summary>
    public static implicit operator bool(EntityId value)
    {
        return !value.IsEmpty;
    }

    /// <summary>
    /// Implements the operator <see langword="true" />.
    /// </summary>
    public static bool operator true(EntityId value)
    {
        return !value.IsEmpty;
    }

    /// <summary>
    /// Implements the operator <see langword="false" />.
    /// </summary>
    public static bool operator false(EntityId value)
    {
        return value.IsEmpty;
    }

    /// <summary>
    /// Implements the operator <c>!</c>.
    /// </summary>
    public static bool operator !(EntityId value)
    {
        return value.IsEmpty;
    }
}