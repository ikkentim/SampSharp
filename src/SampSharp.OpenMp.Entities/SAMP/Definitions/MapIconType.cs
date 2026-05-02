namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all map icon styles.
/// </summary>
/// <remarks>See <see href="https://www.open.mp/docs/scripting/resources/mapiconstyles">https://www.open.mp/docs/scripting/resources/mapiconstyles</see>.</remarks>
public enum MapIconType
{
    /// <summary>
    /// Displays in the player's local area.
    /// </summary>
    Local = 0,

    /// <summary>
    /// Displays always.
    /// </summary>
    Global = 1,

    /// <summary>
    /// Displays in the player's local area and has a checkpoint marker.
    /// </summary>
    LocalCheckPoint = 2,

    /// <summary>
    /// Displays always and has a checkpoint marker.
    /// </summary>
    GlobalCheckPoint = 3
}