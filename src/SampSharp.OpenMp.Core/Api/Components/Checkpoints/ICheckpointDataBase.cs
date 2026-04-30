using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ICheckpointDataBase" /> interface.
/// </summary>
[OpenMpApi]
public readonly partial struct ICheckpointDataBase
{
    /// <summary>
    /// Gets the position of this checkpoint.
    /// </summary>
    /// <returns>The checkpoint position.</returns>
    public partial Vector3 GetPosition();

    /// <summary>
    /// Sets the position of this checkpoint.
    /// </summary>
    /// <param name="position">The checkpoint position to set.</param>
    public partial void SetPosition(ref Vector3 position);

    /// <summary>
    /// Gets the radius of this checkpoint.
    /// </summary>
    /// <returns>The checkpoint radius.</returns>
    public partial float EtRadius();

    /// <summary>
    /// Sets the radius of this checkpoint.
    /// </summary>
    /// <param name="radius">The checkpoint radius to set.</param>
    public partial void SetRadius(float radius);

    /// <summary>
    /// Checks whether the player is inside this checkpoint.
    /// </summary>
    /// <returns><c>true</c> if the player is inside the checkpoint; otherwise, <c>false</c>.</returns>
    public partial bool IsPlayerInside();

    /// <summary>
    /// Sets whether the player is inside this checkpoint.
    /// </summary>
    /// <param name="inside"><c>true</c> to set the player as inside; <c>false</c> to set them as outside.</param>
    public partial void SetPlayerInside(bool inside);

    /// <summary>
    /// Enables this checkpoint.
    /// </summary>
    public partial void Enable();

    /// <summary>
    /// Disables this checkpoint.
    /// </summary>
    public partial void Disable();

    /// <summary>
    /// Checks whether this checkpoint is enabled.
    /// </summary>
    /// <returns><c>true</c> if the checkpoint is enabled; otherwise, <c>false</c>.</returns>
    public partial bool IsEnabled();
}