using SampSharp.OpenMp.Core.RobinHood;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IBaseGangZone" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(IIDProvider))]
public readonly partial struct IBaseGangZone
{
    /// <summary>
    /// Checks if the gangzone is shown for the specified player.
    /// </summary>
    /// <param name="player">The player to check for.</param>
    /// <returns><c>true</c> if the gangzone is shown for the player; otherwise, <c>false</c>.</returns>
    public partial bool IsShownForPlayer(IPlayer player);

    /// <summary>
    /// Checks if the gangzone is flashing for the specified player.
    /// </summary>
    /// <param name="player">The player to check for.</param>
    /// <returns><c>true</c> if the gangzone is flashing for the player; otherwise, <c>false</c>.</returns>
    public partial bool IsFlashingForPlayer(IPlayer player);

    /// <summary>
    /// Shows the gangzone to the specified player with the given colour.
    /// </summary>
    /// <param name="player">The player to show the gangzone to.</param>
    /// <param name="colour">The colour to display the gangzone with.</param>
    public partial void ShowForPlayer(IPlayer player, ref Colour colour);

    /// <summary>
    /// Hides the gangzone for the specified player.
    /// </summary>
    /// <param name="player">The player to hide the gangzone for.</param>
    public partial void HideForPlayer(IPlayer player);

    /// <summary>
    /// Starts flashing the gangzone for the specified player with the given colour.
    /// </summary>
    /// <param name="player">The player to flash the gangzone for.</param>
    /// <param name="colour">The colour to flash the gangzone with.</param>
    public partial void FlashForPlayer(IPlayer player, ref Colour colour);

    /// <summary>
    /// Stops flashing the gangzone for the specified player.
    /// </summary>
    /// <param name="player">The player to stop flashing the gangzone for.</param>
    public partial void StopFlashForPlayer(IPlayer player);

    /// <summary>
    /// Gets the position of the gangzone.
    /// </summary>
    /// <returns>A <see cref="GangZonePos"/> structure containing the minimum and maximum coordinates.</returns>
    public partial GangZonePos GetPosition();

    /// <summary>
    /// Sets the position of the gangzone.
    /// </summary>
    /// <param name="position">A <see cref="GangZonePos"/> structure containing the minimum and maximum coordinates.</param>
    public partial void SetPosition(ref GangZonePos position);

    /// <summary>
    /// Checks if the specified player is within the gangzone bounds.
    /// </summary>
    /// <remarks>This only works if the gangzone has been added to the checking list via <c>IGangZonesComponent.UseGangZoneCheck</c>.</remarks>
    /// <param name="player">The player to check.</param>
    /// <returns><c>true</c> if the player is inside the gangzone; otherwise, <c>false</c>.</returns>
    public partial bool IsPlayerInside(IPlayer player);

    /// <summary>
    /// Gets a list of players the gangzone is shown for.
    /// </summary>
    /// <returns>A collection of players the gangzone is visible to.</returns>
    public partial FlatPtrHashSet<IPlayer> GetShownFor();

    /// <summary>
    /// Gets the flashing colour of the gangzone for the specified player.
    /// </summary>
    /// <param name="player">The player to get the flashing colour for.</param>
    /// <returns>The flashing colour of the gangzone for the player.</returns>
    public partial Colour GetFlashingColourForPlayer(IPlayer player);

    /// <summary>
    /// Gets the colour of the gangzone for the specified player.
    /// </summary>
    /// <param name="player">The player to get the colour for.</param>
    /// <returns>The colour of the gangzone for the player.</returns>
    public partial Colour GetColourForPlayer(IPlayer player);

    /// <summary>
    /// Sets the legacy player for this gangzone, used for ID mapping in per-player gangzones.
    /// </summary>
    /// <param name="player">The player to associate with this gangzone, or <c>null</c>.</param>
    public partial void SetLegacyPlayer(IPlayer player);

    /// <summary>
    /// Gets the legacy player associated with this gangzone, used for ID mapping in per-player gangzones.
    /// </summary>
    /// <returns>The associated legacy player, or <c>null</c> if not set.</returns>
    public partial IPlayer GetLegacyPlayer();
}