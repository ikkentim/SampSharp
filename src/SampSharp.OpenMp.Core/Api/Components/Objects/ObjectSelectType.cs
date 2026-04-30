namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Specifies the type of object selection.
/// </summary>
public enum ObjectSelectType
{
    /// <summary>
    /// Indicates that no object selection is active.
    /// </summary>
    None,

    /// <summary>
    /// Indicates that a global object is being selected.
    /// </summary>
    Global,

    /// <summary>
    /// Indicates that a player-specific object is being selected.
    /// </summary>
    Player
}