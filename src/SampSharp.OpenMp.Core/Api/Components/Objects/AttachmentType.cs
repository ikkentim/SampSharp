namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Specifies the type of attachment point for an object.
/// </summary>
public enum AttachmentType : byte
{
    /// <summary>
    /// The object is not attached to anything.
    /// </summary>
    None,

    /// <summary>
    /// The object is attached to a vehicle.
    /// </summary>
    Vehicle,

    /// <summary>
    /// The object is attached to another object.
    /// </summary>
    Object,

    /// <summary>
    /// The object is attached to a player.
    /// </summary>
    Player
}