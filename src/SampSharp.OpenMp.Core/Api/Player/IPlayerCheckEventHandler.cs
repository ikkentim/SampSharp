namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IPlayerPool.GetPlayerCheckDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerCheckEventHandler
{
    /// <summary>
    /// Called when a client check response is received.
    /// </summary>
    /// <param name="player">The player who sent the response.</param>
    /// <param name="actionType">The type of action being checked.</param>
    /// <param name="address">The memory address being checked.</param>
    /// <param name="results">The results of the check.</param>
    void OnClientCheckResponse(IPlayer player, int actionType, int address, int results);
}
