using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IObject" /> interface.
/// </summary>
[OpenMpApi(typeof(IBaseObject))]
public readonly partial struct IObject
{
    /// <summary>
    /// Attaches this object to a player with the specified offset and rotation.
    /// </summary>
    /// <param name="player">The player to attach to.</param>
    /// <param name="offset">The offset of this object relative to the player.</param>
    /// <param name="rotation">The rotation of this object relative to the player.</param>
    public partial void AttachToPlayer(IPlayer player, Vector3 offset, Vector3 rotation);

    /// <summary>
    /// Attaches this object to another object with the specified offset, rotation, and synchronization option.
    /// </summary>
    /// <param name="objekt">The object to attach to.</param>
    /// <param name="offset">The offset of this object relative to the target object.</param>
    /// <param name="rotation">The rotation of this object relative to the target object.</param>
    /// <param name="syncRotation">A value indicating whether the rotation should be synchronized with the target object.</param>
    public partial void AttachToObject(IObject objekt, Vector3 offset, Vector3 rotation, bool syncRotation);
}