using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IRaceCheckpointData" /> interface.
/// </summary>
[OpenMpApi(typeof(ICheckpointDataBase))]
public readonly partial struct IRaceCheckpointData
{
    /// <summary>
    /// Gets the type of this race checkpoint.
    /// </summary>
    /// <returns>The checkpoint type.</returns>
    [OpenMpApiFunction("getType")]
    public partial RaceCheckpointType GetCheckpointType();

    /// <summary>
    /// Sets the type of this race checkpoint.
    /// </summary>
    /// <param name="type">The checkpoint type to set.</param>
    public partial void SetType(RaceCheckpointType type);

    /// <summary>
    /// Gets the next checkpoint position in the race sequence.
    /// </summary>
    /// <returns>The position of the next checkpoint.</returns>
    public partial Vector3 GetNextPosition();

    /// <summary>
    /// Sets the next checkpoint position in the race sequence.
    /// </summary>
    /// <param name="nextPosition">The next checkpoint position to set.</param>
    public partial void SetNextPosition(ref Vector3 nextPosition);
}