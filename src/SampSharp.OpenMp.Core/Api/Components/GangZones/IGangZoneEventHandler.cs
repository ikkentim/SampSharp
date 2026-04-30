namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IGangZonesComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface IGangZoneEventHandler
{
    /// <summary>
    /// Called when a player enters a gang zone.
    /// </summary>
    /// <param name="player">The player who entered the zone.</param>
    /// <param name="zone">The gang zone that was entered.</param>
    void OnPlayerEnterGangZone(IPlayer player, IGangZone zone);

    /// <summary>
    /// Called when a player leaves a gang zone.
    /// </summary>
    /// <param name="player">The player who left the zone.</param>
    /// <param name="zone">The gang zone that was left.</param>
    void OnPlayerLeaveGangZone(IPlayer player, IGangZone zone);

    /// <summary>
    /// Called when a player clicks a gang zone.
    /// </summary>
    /// <param name="player">The player who clicked the zone.</param>
    /// <param name="zone">The gang zone that was clicked.</param>
    void OnPlayerClickGangZone(IPlayer player, IGangZone zone);
}