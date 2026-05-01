using System.Numerics;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Provides functionality for controlling the SA:MP server.
/// </summary>
public interface IServerService
{
    /// <summary>
    /// Gets the highest actor identifier created on the server.
    /// </summary>
    int ActorPoolSize { get; }

    /// <summary>
    /// Gets the maximum number of players that can join the server, as set by the server variable 'maxplayers' in server.cfg.
    /// </summary>
    int MaxPlayers { get; }

    /// <summary>
    /// Gets the player actor identifier created on the server.
    /// </summary>
    int PlayerPoolSize { get; }

    /// <summary>
    /// Returns the up time of the actual server (not the SA-MP server) in milliseconds.
    /// </summary>
    /// <remarks>The tick count will eventually overflow after roughly 24 days.</remarks>
    int TickCount { get; }

    /// <summary>
    /// Gets the tick rate of the server.
    /// </summary>
    /// <remarks>The tick rate is 0 if the server just started.</remarks>
    int TickRate { get; }

    /// <summary>
    /// Gets the vehicle actor identifier created on the server.
    /// </summary>
    int VehiclePoolSize { get; }

    /// <summary>
    /// Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.
    /// </summary>
    /// <param name="teamId">The team for the player to spawn with.</param>
    /// <param name="modelId">The skin for the player to spawn with.</param>
    /// <param name="spawnPosition">The position for the player to spawn at.</param>
    /// <param name="angle">The angle of the player to spawn at.</param>
    /// <param name="weapon1">The first weapon for the player to spawn with.</param>
    /// <param name="weapon1Ammo">The amount of ammunition of the first weapon for the player to spawn with.</param>
    /// <param name="weapon2">The second weapon for the player to spawn with.</param>
    /// <param name="weapon2Ammo">The amount of ammunition of the second weapon for the player to spawn with.</param>
    /// <param name="weapon3">The third weapon for the player to spawn with.</param>
    /// <param name="weapon3Ammo">The amount of ammunition of the third weapon for the player to spawn with.</param>
    /// <returns>The identifier of the class which was added.</returns>
    /// <remarks>
    /// The maximum class ID is 319 (starting from 0, so a total of 320 classes). When this limit is reached, any more classes that are added will replace ID
    /// 319.
    /// </remarks>
    int AddPlayerClass(int teamId, int modelId, Vector3 spawnPosition, float angle, Weapon weapon1 = Weapon.None, int weapon1Ammo = 0,
        Weapon weapon2 = Weapon.None, int weapon2Ammo = 0, Weapon weapon3 = Weapon.None, int weapon3Ammo = 0);

    /// <summary>
    /// Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.
    /// </summary>
    /// <param name="modelId">The skin for the player to spawn with.</param>
    /// <param name="spawnPosition">The position for the player to spawn at.</param>
    /// <param name="angle">The angle of the player to spawn at.</param>
    /// <param name="weapon1">The first weapon for the player to spawn with.</param>
    /// <param name="weapon1Ammo">The amount of ammunition of the first weapon for the player to spawn with.</param>
    /// <param name="weapon2">The second weapon for the player to spawn with.</param>
    /// <param name="weapon2Ammo">The amount of ammunition of the second weapon for the player to spawn with.</param>
    /// <param name="weapon3">The third weapon for the player to spawn with.</param>
    /// <param name="weapon3Ammo">The amount of ammunition of the third weapon for the player to spawn with.</param>
    /// <returns>The identifier of the class which was added.</returns>
    /// <remarks>
    /// The maximum class ID is 319 (starting from 0, so a total of 320 classes). When this limit is reached, any more classes that are added will replace ID
    /// 319.
    /// </remarks>
    int AddPlayerClass(int modelId, Vector3 spawnPosition, float angle, Weapon weapon1 = Weapon.None, int weapon1Ammo = 0, Weapon weapon2 = Weapon.None,
        int weapon2Ammo = 0, Weapon weapon3 = Weapon.None, int weapon3Ammo = 0);

    /// <summary>
    /// Blocks an IP address from further communication with the server for a set amount of time (with wildcards allowed). Players trying to connect to the
    /// server with a blocked IP address will receive the generic "You are banned from this server." message. Players that are online on the specified IP before the
    /// block will timeout after specific amount of seconds and, upon reconnect, will receive the same message. Effect takes place only when server is running (it is
    /// not persistent).
    /// </summary>
    /// <param name="ipAddress">The ip address to block.</param>
    /// <param name="time">The time that the connection will be blocked for. Use a 0-length timespan for an indefinite block.</param>
    void BlockIpAddress(string ipAddress, TimeSpan time = default);

    /// <summary>
    /// Connect an NPC to the server.
    /// </summary>
    /// <param name="name">The name the NPC should connect as. Must follow the same rules as normal player names.</param>
    /// <param name="script">The NPC script name that is located in the "npcmodes" folder (without the .amx extension).</param>
    void ConnectNpc(string name, string script);

    /// <summary>
    /// Disable all the interior entrances and exits in the game (the yellow arrows at doors).
    /// </summary>
    /// <remarks>
    /// This function will only work if it has been used BEFORE a player connects (it is recommended to use it in OnGameModeInit). It will not remove a
    /// connected player's markers.
    /// </remarks>
    void DisableInteriorEnterExits();

    /// <summary>
    /// Enables or disables stunt bonuses for all players. If enabled, players will receive monetary rewards when performing a stunt in a vehicle (e.g. a
    /// wheelie).
    /// </summary>
    /// <param name="enable">if set to <see langword="true" /> stunt bonuses are enabled.</param>
    void EnableStuntBonus(bool enable);

    /// <summary>
    /// Enable friendly fire for team vehicles. Players will be unable to damage teammates' vehicles.
    /// </summary>
    void EnableVehicleFriendlyFire();

    /// <summary>
    /// Ends the current game mode.
    /// </summary>
    void GameModeExit();

    /// <summary>
    /// Get the boolean value of a console variable.
    /// </summary>
    /// <param name="variableName">The name of the variable.</param>
    /// <returns>The value of the specified console variable.</returns>
    bool GetConsoleVarAsBool(string variableName);

    /// <summary>
    /// Gets the integer value of a console variable.
    /// </summary>
    /// <param name="variableName">The name of the variable.</param>
    /// <returns>The value of the specified console variable.</returns>
    int GetConsoleVarAsInt(string variableName);

    /// <summary>
    /// Gets the string value of a console variable.
    /// </summary>
    /// <param name="variableName">The name of the variable.</param>
    /// <returns>The value of the specified console variable.</returns>
    string? GetConsoleVarAsString(string variableName);

    /// <summary>
    /// Set a radius limitation for the chat. Only players at a certain distance from the player will see their message in the chat. Also changes the distance
    /// at which a player can see other players on the map at the same distance.
    /// </summary>
    /// <param name="chatRadius">The range in which players will be able to see chat.</param>
    void LimitGlobalChatRadius(float chatRadius);

    /// <summary>
    /// Set the player marker radius.
    /// </summary>
    /// <param name="markerRadius">The radius that markers will show at.</param>
    void LimitPlayerMarkerRadius(float markerRadius);

    /// <summary>
    /// Use this function before any player connects (OnGameModeInit) to tell all clients that the script will control vehicle engines and lights. This
    /// prevents the game automatically turning the engine on/off when players enter/exit vehicles and headlights automatically coming on when it is dark.
    /// </summary>
    void ManualVehicleEngineAndLights();

    /// <summary>
    /// Sends an RCON (Remote Console) command.
    /// </summary>
    /// <param name="command">The RCON command to be executed.</param>
    void SendRconCommand(string command);

    /// <summary>
    /// Set the name of the game mode which appears in the server browser.
    /// </summary>
    /// <param name="text">The game mode name to display.</param>
    void SetGameModeText(string text);

    /// <summary>
    /// Set the server name (as shown in the SA:MP server browser).
    /// </summary>
    /// <param name="name">The new server name.</param>
    void SetServerName(string name);

    /// <summary>
    /// Set the map name (displayed in the query response's "map" field).
    /// </summary>
    /// <param name="name">The new map name.</param>
    void SetMapName(string name);

    /// <summary>
    /// Set the server language (displayed in the query response's "lan" field).
    /// </summary>
    /// <param name="language">The language identifier.</param>
    void SetLanguage(string language);

    /// <summary>
    /// Set the server website URL.
    /// </summary>
    /// <param name="url">The website URL.</param>
    void SetWebsiteUrl(string url);

    /// <summary>
    /// Set the connect password. Pass <see langword="null" /> or empty string to clear.
    /// </summary>
    /// <param name="password">The password, or null/empty to disable password auth.</param>
    void SetServerPassword(string? password);

    /// <summary>
    /// Set the RCON admin password. Pass <see langword="null" /> or empty string to clear.
    /// </summary>
    /// <param name="password">The RCON password, or null/empty to disable.</param>
    void SetAdminPassword(string? password);

    /// <summary>
    /// Set the maximum distance to display the names of players. The default draw distance is 70.0.
    /// </summary>
    /// <param name="distance">The distance to set.</param>
    void SetNameTagDrawDistance(float distance = 70);

    /// <summary>
    /// Sets the world time (for all players) to a specific hour.
    /// </summary>
    /// <param name="hour">The hour to set.</param>
    void SetWorldTime(int hour);

    /// <summary>
    /// Shows the name tags. This function can only be used in OnGameModeInit.
    /// </summary>
    /// <param name="show">if set to <see langword="true" /> name tags are enabled; otherwise the tags are disabled.</param>
    void ShowNameTags(bool show);

    /// <summary>
    /// Toggles player markers (blips on the radar). This function can only be used in OnGameModeInit..
    /// </summary>
    /// <param name="mode">The mode used for the markers.</param>
    void ShowPlayerMarkers(PlayerMarkersMode mode);

    /// <summary>
    /// Unblock an IP address that was previously blocked using <see cref="BlockIpAddress" />.
    /// </summary>
    /// <param name="ipAddress">The ip address to unblock.</param>
    void UnBlockIpAddress(string ipAddress);

    /// <summary>
    /// Uses standard player walking animation (animation of the CJ skin) instead of custom animations for every skin (e.g. skating for skater skins).
    /// </summary>
    /// <remarks>Only works when placed under OnGameModeInit.</remarks>
    void UsePlayerPedAnims();
}