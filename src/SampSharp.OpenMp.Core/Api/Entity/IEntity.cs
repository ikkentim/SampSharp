using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IEntity" /> interface.
/// </summary>
[OpenMpApi(typeof(IIDProvider))]
public readonly partial struct IEntity
{
    /// <summary>
    /// Gets the position of this entity.
    /// </summary>
    /// <returns>The position of this entity.</returns>
    public partial Vector3 GetPosition();

    /// <summary>
    /// Sets the position of this entity.
    /// </summary>
    /// <param name="position">The position to set.</param>
    public partial void SetPosition(Vector3 position);

    /// <summary>
    /// Gets the rotation of this entity.
    /// </summary>
    /// <returns>The rotation of this entity.</returns>
    public partial GTAQuat GetRotation();

    /// <summary>
    /// Sets the rotation of this entity.
    /// </summary>
    /// <param name="rotation">The rotation to set.</param>
    public partial void SetRotation(GTAQuat rotation);

    /// <summary>
    /// Gets the virtual world of this entity.
    /// </summary>
    /// <returns>The virtual world of this entity.</returns>
    public partial int GetVirtualWorld();

    /// <summary>
    /// Sets the virtual world of this entity.
    /// </summary>
    /// <param name="vw">The virtual world to set.</param>
    public partial void SetVirtualWorld(int vw);
}
