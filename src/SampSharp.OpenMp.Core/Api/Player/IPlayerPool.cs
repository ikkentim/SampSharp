using System.Numerics;
using System.Runtime.InteropServices.Marshalling;
using SampSharp.OpenMp.Core.RobinHood;
using SampSharp.OpenMp.Core.Std;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerPool" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(IReadOnlyPool<IPlayer>))]
public readonly partial struct IPlayerPool
{
    /// <summary>
    /// Gets a set of all available players and bots (anything in the pool).
    /// </summary>
    public partial FlatPtrHashSet<IPlayer> Entries();

    /// <summary>
    /// Gets a set of all available players only.
    /// </summary>
    public partial FlatPtrHashSet<IPlayer> Players();

    /// <summary>
    /// Gets a set of all available bots only.
    /// </summary>
    public partial FlatPtrHashSet<IPlayer> Bots();

    /// <summary>
    /// Gets a dispatcher for player spawn events.
    /// </summary>
    public partial IEventDispatcher<IPlayerSpawnEventHandler> GetPlayerSpawnDispatcher();

    /// <summary>
    /// Gets a dispatcher for player connection events.
    /// </summary>
    public partial IEventDispatcher<IPlayerConnectEventHandler> GetPlayerConnectDispatcher();

    /// <summary>
    /// Gets a dispatcher for player streaming events.
    /// </summary>
    public partial IEventDispatcher<IPlayerStreamEventHandler> GetPlayerStreamDispatcher();

    /// <summary>
    /// Gets a dispatcher for player text and command events.
    /// </summary>
    public partial IEventDispatcher<IPlayerTextEventHandler> GetPlayerTextDispatcher();

    /// <summary>
    /// Gets a dispatcher for player shooting events.
    /// </summary>
    public partial IEventDispatcher<IPlayerShotEventHandler> GetPlayerShotDispatcher();

    /// <summary>
    /// Gets a dispatcher for player data change events.
    /// </summary>
    public partial IEventDispatcher<IPlayerChangeEventHandler> GetPlayerChangeDispatcher();

    /// <summary>
    /// Gets a dispatcher for player damage and death events.
    /// </summary>
    public partial IEventDispatcher<IPlayerDamageEventHandler> GetPlayerDamageDispatcher();

    /// <summary>
    /// Gets a dispatcher for player clicking events.
    /// </summary>
    public partial IEventDispatcher<IPlayerClickEventHandler> GetPlayerClickDispatcher();

    /// <summary>
    /// Gets a dispatcher for player client check response events.
    /// </summary>
    public partial IEventDispatcher<IPlayerCheckEventHandler> GetPlayerCheckDispatcher();

    /// <summary>
    /// Gets a dispatcher for player update events.
    /// </summary>
    public partial IEventDispatcher<IPlayerUpdateEventHandler> GetPlayerUpdateDispatcher();

    /// <summary>
    /// Gets a dispatcher for player pool events.
    /// </summary>
    public partial IEventDispatcher<IPoolEventHandler<IPlayer>> GetPoolEventDispatcher();

    /// <summary>
    /// Checks if a name is taken by any player, excluding a specific player.
    /// </summary>
    /// <param name="name">The name to check.</param>
    /// <param name="skip">The player to exclude from the check.</param>
    /// <returns><c>true</c> if the name is taken; otherwise, <c>false</c>.</returns>
    public partial bool IsNameTaken(string name, IPlayer skip);

    /// <summary>
    /// Sends a client message to all players.
    /// </summary>
    /// <param name="colour">The colour of the message.</param>
    /// <param name="message">The message to send.</param>
    public partial void SendClientMessageToAll(ref Colour colour, string message);

    /// <summary>
    /// Sends a chat message to all players.
    /// </summary>
    /// <param name="from">The player sending the message.</param>
    /// <param name="message">The message to send.</param>
    public partial void SendChatMessageToAll(IPlayer from, string message);

    /// <summary>
    /// Sends a game text message to all players.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="time">The duration to display the message.</param>
    /// <param name="style">The style of the message.</param>
    public partial void SendGameTextToAll(string message, [MarshalUsing(typeof(MillisecondsMarshaller))] TimeSpan time, int style);

    /// <summary>
    /// Hides a game text message for all players.
    /// </summary>
    /// <param name="style">The style of the message to hide.</param>
    public partial void HideGameTextForAll(int style);

    /// <summary>
    /// Sends a death message to all players.
    /// </summary>
    /// <param name="killer">The player who killed.</param>
    /// <param name="killee">The player who was killed.</param>
    /// <param name="weapon">The weapon used.</param>
    public partial void SendDeathMessageToAll(IPlayer killer, IPlayer killee, int weapon);

    /// <summary>
    /// Sends an empty death message to all players.
    /// </summary>
    public partial void SendEmptyDeathMessageToAll();

    /// <summary>
    /// Creates an explosion for all players.
    /// </summary>
    /// <param name="vec">The position of the explosion.</param>
    /// <param name="type">The type of explosion.</param>
    /// <param name="radius">The radius of the explosion.</param>
    public partial void CreateExplosionForAll(Vector3 vec, int type, float radius);

    private partial void RequestPlayer(ref PeerNetworkData netData, ref PeerRequestParams parms, out Pair<NewConnectionResult, IPlayer> result);

    /// <summary>
    /// Requests a new player with the given network parameters.
    /// </summary>
    /// <param name="netData">The network data for the player.</param>
    /// <param name="parms">The request parameters.</param>
    /// <returns>A tuple containing the result of the connection and the player instance.</returns>
    public (NewConnectionResult, IPlayer) RequestPlayer(ref PeerNetworkData netData, ref PeerRequestParams parms)
    {
        RequestPlayer(ref netData, ref parms, out var result);
        return result;
    }

    /// <summary>
    /// Broadcasts a packet to all players.
    /// </summary>
    /// <param name="data">The data span with the length in bits.</param>
    /// <param name="channel">The channel to use.</param>
    /// <param name="skipFrom">The player to exclude from the broadcast.</param>
    /// <param name="dispatchEvents">Whether to dispatch packet-related events.</param>
    public partial void BroadcastPacket(SpanLite<byte> data, int channel, IPlayer skipFrom = default, bool dispatchEvents = true);

    /// <summary>
    /// Broadcasts an RPC to all players.
    /// </summary>
    /// <param name="id">The RPC ID.</param>
    /// <param name="data">The data span with the length in bits.</param>
    /// <param name="channel">The channel to use.</param>
    /// <param name="skipFrom">The player to exclude from the broadcast.</param>
    /// <param name="dispatchEvents">Whether to dispatch RPC-related events.</param>
    public partial void BroadcastRPC(int id, SpanLite<byte> data, int channel, IPlayer skipFrom = default, bool dispatchEvents = true);

    /// <summary>
    /// Checks if a player name is valid.
    /// </summary>
    /// <param name="name">The name to validate.</param>
    /// <returns><c>true</c> if the name is valid; otherwise, <c>false</c>.</returns>
    public partial bool IsNameValid(string name);

    /// <summary>
    /// Allows or disallows the use of a specific character in player names.
    /// </summary>
    /// <param name="character">The character to allow or disallow.</param>
    /// <param name="allow">Whether to allow the character.</param>
    public partial void AllowNickNameCharacter(char character, bool allow);

    /// <summary>
    /// Checks if a specific character is allowed in player names.
    /// </summary>
    /// <param name="character">The character to check.</param>
    /// <returns><c>true</c> if the character is allowed; otherwise, <c>false</c>.</returns>
    public partial bool IsNickNameCharacterAllowed(char character);

    /// <summary>
    /// Gets the default colour assigned to a player ID when they first connect.
    /// </summary>
    /// <param name="pid">The player ID.</param>
    /// <returns>The default colour.</returns>
    public partial Colour GetDefaultColour(int pid);

    /// <summary>
    /// Converts this instance to a read-only player pool.
    /// </summary>
    /// <returns>A read-only player pool.</returns>
    public IReadOnlyPool<IPlayer> AsPool()
    {
        return (IReadOnlyPool<IPlayer>)this;
    }
}