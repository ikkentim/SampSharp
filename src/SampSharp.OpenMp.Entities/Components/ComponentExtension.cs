using SampSharp.OpenMp.Core;

namespace SampSharp.Entities;

/// <summary>
/// Represents an extension to an open.mp component which binds its lifetime to the lifetime of an <see cref="Entities.Component" />.
/// </summary>
/// <param name="component">The component to which this extension is bound.</param>
[Extension(0x53d3b0b0bbb28a7f)]
public sealed class ComponentExtension(Component component) : Extension
{

    /// <summary>
    /// Gets the component to which this extension is bound.
    /// </summary>
    public Component Component { get; } = component;

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    public bool IsOmpEntityDestroyed { get; private set; }

    /// <inheritdoc />
    protected override void Cleanup()
    {
        IsOmpEntityDestroyed = true;

        if (!Component.IsDestroying)
        {
            Component.DestroyEntity();
        }
    }
}