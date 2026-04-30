namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IPlayerPool.GetPlayerConnectDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface IPlayerConnectEventHandler
{
    /// <summary>
    /// Called when a player is attempting to connect to the server.
    /// </summary>
    /// <param name="player">The player attempting to connect.</param>
    /// <param name="ipAddress">The IP address of the player.</param>
    /// <param name="port">The port used by the player.</param>
    void OnIncomingConnection(IPlayer player, string ipAddress, ushort port);

    /// <summary>
    /// Called when a player successfully connects to the server.
    /// </summary>
    /// <param name="player">The player who connected.</param>
    void OnPlayerConnect(IPlayer player);

    /// <summary>
    /// Called when a player disconnects from the server.
    /// </summary>
    /// <param name="player">The player who disconnected.</param>
    /// <param name="reason">The reason for the disconnection.</param>
    void OnPlayerDisconnect(IPlayer player, PeerDisconnectReason reason);

    /// <summary>  
    /// Called when a player's client starts initializing.  
    /// </summary>  
    /// <param name="player">The player whose client has started initializing.</param>  
    void OnPlayerClientInit(IPlayer player);
}