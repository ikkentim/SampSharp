using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IPlayerPool.GetPlayerUpdateDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerUpdateEventHandler
{
    /// <summary>
    /// Called when a player has sent an update.
    /// </summary>
    /// <param name="player">The player being updated.</param>
    /// <param name="now">The current time point of the update.</param>
    /// <returns><see langword="true" /> if the event should be processed; otherwise, <see langword="false" /> to ignore it.</returns>
    bool OnPlayerUpdate(IPlayer player, TimePoint now);
}
