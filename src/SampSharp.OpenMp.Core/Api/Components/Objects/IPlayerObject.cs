using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerObject" /> interface.
/// </summary>
[OpenMpApi(typeof(IBaseObject))]
public readonly partial struct IPlayerObject
{
    /// <summary>
    /// Attaches this player object to another player object with the specified offset and rotation.
    /// </summary>
    /// <param name="objekt">The player object to attach to.</param>
    /// <param name="offset">The offset of this object relative to the target object.</param>
    /// <param name="rotation">The rotation of this object relative to the target object.</param>
    public partial void AttachToObject(IPlayerObject objekt, Vector3 offset, Vector3 rotation);

    /// <summary>
    /// Attaches this player object to a player with the specified offset and rotation.
    /// </summary>
    /// <param name="player">The player to attach to.</param>
    /// <param name="offset">The offset of this object relative to the player.</param>
    /// <param name="rotation">The rotation of this object relative to the player.</param>
    public partial void AttachToPlayer(IPlayer player, Vector3 offset, Vector3 rotation);
}