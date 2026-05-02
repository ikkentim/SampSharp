namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all types of things bullets can hit.
/// </summary>
/// <remarks>See <see href="https://www.open.mp/docs/scripting/resources/bullethittypes" />.</remarks>
public enum BulletHitType
{
    /// <summary>
    /// Hit nothing.
    /// </summary>
    None = 0,

    /// <summary>
    /// Hit a player.
    /// </summary>
    Player = 1,

    /// <summary>
    /// Hit a vehicle.
    /// </summary>
    Vehicle = 2,

    /// <summary>
    /// Hit an GlobalObject.
    /// </summary>
    Object = 3,

    /// <summary>
    /// Hit a PlayerObject.
    /// </summary>
    PlayerObject = 4
}