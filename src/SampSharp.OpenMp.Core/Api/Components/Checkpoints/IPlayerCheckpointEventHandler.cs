namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="ICheckpointsComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerCheckpointEventHandler
{
    /// <summary>Fired when the player enters their currently-set checkpoint.</summary>
    void OnPlayerEnterCheckpoint(IPlayer player);

    /// <summary>Fired when the player leaves their currently-set checkpoint.</summary>
    void OnPlayerLeaveCheckpoint(IPlayer player);

    /// <summary>Fired when the player enters their currently-set race checkpoint.</summary>
    void OnPlayerEnterRaceCheckpoint(IPlayer player);

    /// <summary>Fired when the player leaves their currently-set race checkpoint.</summary>
    void OnPlayerLeaveRaceCheckpoint(IPlayer player);
}