namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Specifies the type of race checkpoint.
/// </summary>
public enum RaceCheckpointType
{
    /// <summary>
    /// Normal checkpoint. Must have a next position, otherwise it displays as a finish checkpoint.
    /// </summary>
    NORMAL = 0,

    /// <summary>
    /// Finish checkpoint. Must not have a next position, otherwise it displays as a normal checkpoint.
    /// </summary>
    FINISH,

    /// <summary>
    /// Invisible checkpoint with no visual representation.
    /// </summary>
    NOTHING,

    /// <summary>
    /// Floating/aerial normal checkpoint.
    /// </summary>
    AIR_NORMAL,

    /// <summary>
    /// Floating/aerial finish checkpoint.
    /// </summary>
    AIR_FINISH,

    /// <summary>
    /// Floating/aerial checkpoint variant 1.
    /// </summary>
    AIR_ONE,

    /// <summary>
    /// Floating/aerial checkpoint variant 2.
    /// </summary>
    AIR_TWO,

    /// <summary>
    /// Floating/aerial checkpoint variant 3.
    /// </summary>
    AIR_THREE,

    /// <summary>
    /// Floating/aerial checkpoint variant 4.
    /// </summary>
    AIR_FOUR,

    /// <summary>
    /// No checkpoint type.
    /// </summary>
    NONE
}
