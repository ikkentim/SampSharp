using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IBasePickup" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(IEntity))]
public readonly partial struct IBasePickup
{
    /// <summary>
    /// Sets the type of the pickup.
    /// </summary>
    /// <param name="type">The type ID of the pickup.</param>
    /// <param name="update">Whether to update the pickup visually for all players; defaults to <c>true</c>.</param>
    public partial void SetType(byte type, bool update = true);

    /// <summary>
    /// Gets the type of the pickup.
    /// </summary>
    /// <returns>The type ID of the pickup.</returns>
    [OpenMpApiFunction("getType")]
    public partial byte GetPickupType();

    /// <summary>
    /// Sets the position of the pickup without updating the visual representation.
    /// </summary>
    /// <param name="position">The new position for the pickup.</param>
    public partial void SetPositionNoUpdate(Vector3 position);

    /// <summary>
    /// Sets the model ID of the pickup.
    /// </summary>
    /// <param name="id">The model ID to set.</param>
    /// <param name="update">Whether to update the pickup visually for all players; defaults to <c>true</c>.</param>
    public partial void SetModel(int id, bool update = true);

    /// <summary>
    /// Gets the model ID of the pickup.
    /// </summary>
    /// <returns>The model ID of the pickup.</returns>
    public partial int GetModel();

    /// <summary>
    /// Checks if the pickup is streamed in for the specified player.
    /// </summary>
    /// <param name="player">The player to check for.</param>
    /// <returns><c>true</c> if the pickup is streamed in for the player; otherwise, <c>false</c>.</returns>
    public partial bool IsStreamedInForPlayer(IPlayer player);

    /// <summary>
    /// Streams the pickup in for the specified player.
    /// </summary>
    /// <param name="player">The player to stream the pickup in for.</param>
    public partial void StreamInForPlayer(IPlayer player);

    /// <summary>
    /// Streams the pickup out for the specified player.
    /// </summary>
    /// <param name="player">The player to stream the pickup out for.</param>
    public partial void StreamOutForPlayer(IPlayer player);

    /// <summary>
    /// Sets whether the pickup is hidden for the specified player.
    /// </summary>
    /// <param name="player">The player to hide or show the pickup for.</param>
    /// <param name="hidden"><c>true</c> to hide the pickup; <c>false</c> to show it.</param>
    public partial void SetPickupHiddenForPlayer(IPlayer player, bool hidden);

    /// <summary>
    /// Checks if the pickup is hidden for the specified player.
    /// </summary>
    /// <param name="player">The player to check for.</param>
    /// <returns><c>true</c> if the pickup is hidden for the player; otherwise, <c>false</c>.</returns>
    public partial bool IsPickupHiddenForPlayer(IPlayer player);

    /// <summary>
    /// Sets the legacy player for this pickup, used for ID mapping in per-player pickups.
    /// </summary>
    /// <param name="player">The player to associate with this pickup, or <c>null</c>.</param>
    public partial void SetLegacyPlayer(IPlayer player);

    /// <summary>
    /// Gets the legacy player associated with this pickup, used for ID mapping in per-player pickups.
    /// </summary>
    /// <returns>The associated legacy player, or <c>null</c> if not set.</returns>
    public partial IPlayer GetLegacyPlayer();
}