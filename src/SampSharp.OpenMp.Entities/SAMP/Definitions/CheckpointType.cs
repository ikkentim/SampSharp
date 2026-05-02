namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all race checkpoint types.
/// </summary>
/// <remarks>
/// See
/// <see href="https://www.open.mp/docs/scripting/functions/SetPlayerRaceCheckpoint">https://www.open.mp/docs/scripting/functions/SetPlayerRaceCheckpoint</see> .
/// </remarks>
public enum CheckpointType
{
    /// <summary>
    /// Normal race checkpoint. (Normal red cylinder)
    /// </summary>
    Normal = 0,

    /// <summary>
    /// Finish race checkpoint. (Finish flag in red cylinder)
    /// </summary>
    Finish = 1,

    /// <summary>
    /// No checkpoint.
    /// </summary>
    Nothing = 2,

    /// <summary>
    /// Air race checkpoint. (normal red circle in the air)
    /// </summary>
    Air = 3,

    /// <summary>
    /// Finish air race checkpoint. (Finish flag in red circle in the air)
    /// </summary>
    AirFinish = 4,

    /// <summary>
    /// Checkpoint one.
    /// </summary>
    One = 5,
    /// <summary>
    /// Checkpoint two.
    /// </summary>
    Two = 6,
    /// <summary>
    /// Checkpoint three.
    /// </summary>
    Three = 7,
    /// <summary>
    /// Checkpoint four.
    /// </summary>
    Four = 8,
    /// <summary>
    /// No checkpoint.
    /// </summary>
    None = 9
}