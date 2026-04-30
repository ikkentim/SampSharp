namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the style of a map icon in the game.
/// </summary>
public enum MapIconStyle
{
    /// <summary>
    /// The map icon is visible only if it is within the range of the minimap.
    /// </summary>
    Local,

    /// <summary>
    /// The map icon is visible globally and will stick to the edge of the minimap if it is outside the minimap range.
    /// </summary>
    Global,

    /// <summary>
    /// The map icon represents a local checkpoint and is visible only if it is within the range of the minimap.
    /// </summary>
    LocalCheckpoint,

    /// <summary>
    /// The map icon represents a global checkpoint and will stick to the edge of the minimap if it is outside the minimap range.
    /// </summary>
    GlobalCheckpoint
}