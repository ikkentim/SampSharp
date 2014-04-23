// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;
using System.Runtime.CompilerServices;
using GameMode.Definitions;
using GameMode.World;

namespace GameMode
{
    public static partial class Native
    {
        /// <summary>
        ///     This function sends a message to a specific player with a chosen color in the chat. The whole line in the chatbox
        ///     will be in the set color unless colour embedding is used.<br />
        /// </summary>
        /// <param name="playerid">The ID of the player to display the message to.</param>
        /// <param name="color">The color of the message.</param>
        /// <param name="message">The text that will be displayed (max 144 characters).</param>
        /// <returns>
        ///     True: The function was successful (the message was sucessfully displayed (NOTE: success will be returned even
        ///     if the message is too long (more than 144 characters) and fails to be sent)). False: The function failed (The
        ///     message was not displayed (player not connected?)).
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendClientMessage(int playerid, int color, string message);

        /// <summary>
        ///     Displays a message in chat to all players. This is a multi-player equivalent of <see cref="SendClientMessage" />.
        ///     <br />
        /// </summary>
        /// <param name="color">The color of the message (RGBA Hex format).</param>
        /// <param name="message">The message to show (max 144 characters).</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendClientMessageToAll(int color, string message);

        /// <summary>
        ///     Sends a message in the name of a player to another player on the server. The message will appear in the chat box
        ///     but can only be seen by the user specified with <paramref name="playerid" />. The line will start with the
        ///     <paramref name="senderid" />'s name in his color, followed by the <paramref name="message" /> in white.
        /// </summary>
        /// <param name="playerid">The ID of the player who will recieve the message</param>
        /// <param name="senderid">The sender's ID. If invalid, the message will not be sent.</param>
        /// <param name="message">The message that will be sent.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendPlayerMessageToPlayer(int playerid, int senderid, string message);

        /// <summary>
        ///     Sends a message in the name of a player to all other players on the server. The line will start with the
        ///     <paramref name="senderid" />'s name in their color, followed by the <paramref name="message" /> in white.
        /// </summary>
        /// <param name="senderid">The ID of the sender. If invalid, the message will not be sent.</param>
        /// <param name="message">The message that will be sent.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendPlayerMessageToAll(int senderid, string message);

        /// <summary>
        ///     Adds a death to the 'killfeed' on the right-hand side of the screen.
        /// </summary>
        /// <param name="killer">The ID of the killer (can be <see cref="Misc.InvalidPlayerId" />).</param>
        /// <param name="killee">The ID of the player that died.</param>
        /// <param name="weapon">
        ///     The reason (not always a weapon) for the <paramref name="killee" />'s death. Special icons can
        ///     also be used (ICON_CONNECT and ICON_DISCONNECT).
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendDeathMessage(int killer, int killee, int weapon);

        /// <summary>
        ///     Shows 'game text' (on-screen text) for a certain length of time for all players.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="time">The duration of the text being shown in milliseconds.</param>
        /// <param name="style">The style of text to be displayed.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GameTextForAll(string text, int time, int style);

        /// <summary>
        ///     Shows 'game text' (on-screen text) for a certain length of time for a specific player.
        /// </summary>
        /// <param name="playerid">The ID of the player to show the gametext for.</param>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="time">The duration of the text being shown in milliseconds.</param>
        /// <param name="style">The style of text to be displayed.</param>
        /// <returns>True on success, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GameTextForPlayer(int playerid, string text, int time, int style);

        /// <summary>
        ///     Returns the uptime of the actual server in milliseconds.
        /// </summary>
        /// <returns>Uptime of the SA:MP server(NOT the physical box).</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetTickCount();

        /// <summary>
        ///     Returns the maximum number of players that can join the server, as set by the server var 'maxplayers' in
        ///     server.cfg.
        /// </summary>
        /// <returns>The maximum number of players that can join the server.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetMaxPlayers();

        /// <summary>
        ///     Set the name of the game mode, which appears in the server browser.
        /// </summary>
        /// <param name="text">GameMode name.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetGameModeText(string text);

        /// <summary>
        ///     This function is used to change the amount of teams used in the gamemode. It has no obvious way of being used, but
        ///     can help to indicate the number of teams used for better (more effective) internal handling. This function should
        ///     only be used in the <see cref="BaseMode.OnGameModeInit" /> callback.
        /// </summary>
        /// <remarks>
        ///     You can pass 2 billion here if you like, this function has no effect at all.
        /// </remarks>
        /// <param name="count">Number of teams the gamemode knows.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetTeamCount(int count);

        /// <summary>
        ///     Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.
        /// </summary>
        /// <param name="modelid">The skin which the player will spawn with.</param>
        /// <param name="spawnX">The X coordinate of the spawnpoint of this class.</param>
        /// <param name="spawnY">The Y coordinate of the spawnpoint of this class.</param>
        /// <param name="spawnZ">The Z coordinate of the spawnpoint of this class.</param>
        /// <param name="zAngle">The direction in which the player should face after spawning.</param>
        /// <param name="weapon1">The first spawn-weapon for the player.</param>
        /// <param name="weapon1Ammo">The amount of ammunition for the primary spawnweapon.</param>
        /// <param name="weapon2">The second spawn-weapon for the player.</param>
        /// <param name="weapon2Ammo">The amount of ammunition for the second spawnweapon.</param>
        /// <param name="weapon3">The third spawn-weapon for the player.</param>
        /// <param name="weapon3Ammo">The amount of ammunition for the third spawnweapon.</param>
        /// <returns>
        ///     The ID of the class which was just added. 300 if the class limit (300) was reached. The highest possible class
        ///     ID is 299.
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddPlayerClass(int modelid, float spawnX, float spawnY, float spawnZ, float zAngle,
            int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        /// <summary>
        ///     This function is exactly the same as the <see cref="AddPlayerClass" /> function, with the addition of a team
        ///     parameter.
        /// </summary>
        /// <param name="teamid">The team you want the player to spawn in.</param>
        /// <param name="modelid">The skin which the player will spawn with.</param>
        /// <param name="spawnX">The X coordinate of the class' spawn position.</param>
        /// <param name="spawnY">The Y coordinate of the class' spawn position.</param>
        /// <param name="spawnZ">The Z coordinate of the class' spawn position.</param>
        /// <param name="zAngle">The direction in which the player will face after spawning.</param>
        /// <param name="weapon1">The first spawn-weapon for the player.</param>
        /// <param name="weapon1Ammo">The amount of ammunition for the first spawnweapon.</param>
        /// <param name="weapon2">The second spawn-weapon for the player.</param>
        /// <param name="weapon2Ammo">The amount of ammunition for the second spawnweapon.</param>
        /// <param name="weapon3">The third spawn-weapon for the player.</param>
        /// <param name="weapon3Ammo">The amount of ammunition for the third spawnweapon.</param>
        /// <returns>The ID of the class that was just created.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddPlayerClassEx(int teamid, int modelid, float spawnX, float spawnY, float spawnZ,
            float zAngle, int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        /// <summary>
        ///     Adds a 'static' vehicle (models are pre-loaded for players)to the gamemode. Can only be used when the server first
        ///     starts (in <see cref="BaseMode.OnGameModeInit" />).
        /// </summary>
        /// <param name="modelid">The Model ID for the vehicle.</param>
        /// <param name="spawnX">The X-coordinate for the vehicle.</param>
        /// <param name="spawnY">The Y-coordinate for the vehicle.</param>
        /// <param name="spawnZ">The Z-coordinate for the vehicle.</param>
        /// <param name="zAngle">Direction of vehicle - angle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <returns>
        ///     The vehicle ID of the vehicle created (1 - <see cref="Limits.MaxVehicles" />).
        ///     <see cref="Misc.InvalidVehicleId" /> (65535) if vehicle was not created (vehicle limit reached or invalid vehicle
        ///     model ID passed).
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddStaticVehicle(int modelid, float spawnX, float spawnY, float spawnZ,
            float zAngle, int color1, int color2);

        /// <summary>
        ///     Adds a 'static' vehicle (models are pre-loaded for players)to the gamemode. Can only be used when the server first
        ///     starts (under <see cref="BaseMode.OnGameModeInit" />). Differs from <see cref="AddStaticVehicle" /> in only one
        ///     way: allows a respawn time to be set for when the vehicle is left unoccupied by the driver.
        /// </summary>
        /// <param name="modelid">The Model ID for the vehicle.</param>
        /// <param name="spawnX">The X-coordinate for the vehicle.</param>
        /// <param name="spawnY">The Y-coordinate for the vehicle.</param>
        /// <param name="spawnZ">The Z-coordinate for the vehicle.</param>
        /// <param name="zAngle">The facing - angle for the vehicle.</param>
        /// <param name="color1">The primary color ID.</param>
        /// <param name="color2">The secondary color ID.</param>
        /// <param name="respawnDelay">The delay until the car is respawned without a driver, in seconds.</param>
        /// <returns>
        ///     The vehicle ID of the vehicle created (1 - <see cref="Limits.MaxVehicles" />).
        ///     <see cref="Misc.InvalidVehicleId" /> (65535) if vehicle was not created (vehicle limit reached or invalid vehicle
        ///     model ID passed).
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddStaticVehicleEx(int modelid, float spawnX, float spawnY, float spawnZ,
            float zAngle, int color1, int color2, int respawnDelay);

        /// <summary>
        ///     This function adds a 'static' pickup to the game. These pickups support weapons, health, armor etc., with the
        ///     ability to function without scripting them (weapons/health/armor will be given automatically).
        /// </summary>
        /// <param name="model">The model of the pickup.</param>
        /// <param name="type">The pickup spawn type.</param>
        /// <param name="x">The X coordinate to create the pickup at.</param>
        /// <param name="y">The Y coordinate to create the pickup at.</param>
        /// <param name="z">The Z coordinate to create the pickup at.</param>
        /// <param name="virtualworld">The virtual world ID of the pickup. Use -1 to show the pickup in all worlds.</param>
        /// <returns>1 if the pickup is successfully created.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddStaticPickup(int model, int type, float x, float y, float z, int virtualworld);

        /// <summary>
        ///     This function does exactly the same as <see cref="AddStaticPickup" />, except it returns a pickup ID which can be
        ///     used to destroy it afterwards and be tracked using <see cref="BaseMode.OnPlayerPickUpPickup" />.
        /// </summary>
        /// <param name="model">The model of the pickup.</param>
        /// <param name="type">The pickup spawn type.</param>
        /// <param name="x">The X coordinate to create the pickup at.</param>
        /// <param name="y">The Y coordinate to create the pickup at.</param>
        /// <param name="z">The Z coordinate to create the pickup at.</param>
        /// <param name="virtualworld">The virtual world ID of the pickup. Use -1 to make the pickup show in all worlds.</param>
        /// <returns>The ID of the created pickup, -1 on failure (pickup max limit).</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePickup(int model, int type, float x, float y, float z, int virtualworld);

        /// <summary>
        ///     Destroys a pickup.
        /// </summary>
        /// <param name="pickupid">The ID of the pickup to destroy.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyPickup(int pickupid);

        /// <summary>
        ///     Toggle the drawing of player nametags, healthbars and armor bars above players.
        /// </summary>
        /// <param name="show">False to disable, True to enable.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowNameTags(bool show);

        /// <summary>
        ///     A function that can be used in <see cref="BaseMode.OnGameModeInit" /> to enable or disable the players markers,
        ///     which would normally be shown on the radar. If you want to change the marker settings at some other point in the
        ///     gamemode, have a look at <see cref="SetPlayerMarkerForPlayer" /> which does exactly that.
        /// </summary>
        /// <param name="mode">The mode you want to use.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowPlayerMarkers(int mode);

        /// <summary>
        ///     Ends the currently active gamemode.
        /// </summary>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GameModeExit();

        /// <summary>
        ///     Sets the world time to a specific hour.
        /// </summary>
        /// <param name="hour">Which time to set.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetWorldTime(int hour);

        /// <summary>
        ///     Get the name of a weapon.
        /// </summary>
        /// <param name="weaponid">The ID of the weapon to get the name of.</param>
        /// <param name="name">An array to store the weapon's name in, passed by reference.</param>
        /// <param name="size">The length of the weapon name.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetWeaponName(int weaponid, out string name, int size);

        /// <summary>
        ///     With this function you can enable or disable tire popping.
        /// </summary>
        /// <param name="enable">True to enable, False to disable tire popping.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableTirePopping(bool enable);

        /// <summary>
        ///     Enable friendly fire for team vehicles.
        /// </summary>
        /// <remarks>
        ///     Players will be unable to damage teammates' vehicles (<see cref="SetPlayerTeam" /> must be used!)
        /// </remarks>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableVehicleFriendlyFire();

        /// <summary>
        ///     Toggle whether the usage of weapons in interiors is allowed or not.
        /// </summary>
        /// <param name="allow">True to enable weapons in interiors (enabled by default), False to disable weapons in interiors.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AllowInteriorWeapons(bool allow);

        /// <summary>
        ///     Set the world weather for all players.
        /// </summary>
        /// <param name="weatherid">The weather to set.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetWeather(int weatherid);

        /// <summary>
        ///     Set the gravity for all players.
        /// </summary>
        /// <param name="gravity">The value that the gravity should be set to (between -50 and 50).</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetGravity(float gravity);

        /// <summary>
        ///     This function will determine whether RCON admins will be teleported to their waypoint when they set one.
        /// </summary>
        /// <param name="allow">False to disable and True to enable.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        [Obsolete("This function is deprecated. Use the OnPlayerClickMap callback instead.")]
        public static extern bool AllowAdminTeleport(bool allow);

        /// <summary>
        ///     Set the amount of money dropped when a player dies.
        /// </summary>
        /// <remarks>
        ///     This function does not work in the current SA:MP version.
        /// </remarks>
        /// <param name="amount">Tthe amount of money dropped when a player dies.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        [Obsolete("This function is deprecated. Use the OnPlayerDeath callback and CreatePickup method instead.")]
        public static extern bool SetDeathDropAmount(int amount);

        /// <summary>
        ///     Create an explosion at the specified coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the explosion.</param>
        /// <param name="y">The Y coordinate of the explosion.</param>
        /// <param name="z">The Z coordinate of the explosion.</param>
        /// <param name="type">The type of explosion.</param>
        /// <param name="radius">The explosion radius.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CreateExplosion(float x, float y, float z, int type, float radius);

        /// <summary>
        ///     This function allows to turn on zone / area names such as the "Vinewood" or "Doherty" text at the bottom-right of
        ///     the screen as they enter the area. This is a gamemode option and should be set in the callback
        ///     <see cref="BaseMode.OnGameModeInit" />.
        /// </summary>
        /// <param name="enable">A toggle option for whether or not you'd like zone names on or off. False is off and True is on.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        [Obsolete("This function is deprecated. You must create your own textdraws to show zone names.")]
        public static extern bool EnableZoneNames(bool enable);

        /// <summary>
        ///     Uses standard player walking animation (animation of CJ) instead of custom animations for every skin (e.g. skating
        ///     for skater skins).
        /// </summary>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool UsePlayerPedAnims();

        /// <summary>
        ///     Disable all the interior entrances and exits in the game (the yellow arrows at doors).
        /// </summary>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableInteriorEnterExits();

        /// <summary>
        ///     Set the maximum distance to display the names of players.
        /// </summary>
        /// <param name="distance">The distance to set.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetNameTagDrawDistance(float distance);

        /// <summary>
        ///     Disables the nametag Line-Of-Sight checking so players can see nametags through objects.
        /// </summary>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableNameTagLOS();

        /// <summary>
        ///     Set a radius limitation for the chat. Only players at a certain distance from the player will see their message in
        ///     the chat. Also changes the distance at which a player can see other players on the map at the same distance.
        /// </summary>
        /// <param name="chatRadius">Radius limit.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool LimitGlobalChatRadius(float chatRadius);

        /// <summary>
        ///     Set the player marker radius.
        /// </summary>
        /// <param name="markerRadius">The radius that markers will show at.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool LimitPlayerMarkerRadius(float markerRadius);

        /// <summary>
        ///     Connect an NPC to the server.
        /// </summary>
        /// <param name="name">The name the NPC should connect as. Must follow the same rules as normal player names.</param>
        /// <param name="script">The NPC script name that is located in the npcmodes folder (without the .amx extension).</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ConnectNPC(string name, string script);

        /// <summary>
        ///     Check if a player is an actual player or an NPC.
        /// </summary>
        /// <param name="playerid">The ID of the player to check.</param>
        /// <returns>True if the player is an NPC, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerNPC(int playerid);

        /// <summary>
        ///     Check if a player is logged into RCON.
        /// </summary>
        /// <param name="playerid">The ID of the player to check.</param>
        /// <returns>True if the player is logged into RCON, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerAdmin(int playerid);

        /// <summary>
        ///     Kicks a player from the server. They will have to quit the game and re-connect if they wish to continue playing.
        /// </summary>
        /// <param name="playerid">The ID of the player to kick.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Kick(int playerid);

        /// <summary>
        ///     Ban a player who currently in the server. The ban will be IP-based, and be saved in the samp.ban file in the
        ///     server's root directory. <see cref="BanEx" /> allows you to ban with a reason, while you can ban and unban IPs
        ///     using the RCON banip and unbanip commands.
        /// </summary>
        /// <param name="playerid">The ID of the player to ban.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Ban(int playerid);

        /// <summary>
        ///     Ban a player with a reason.
        /// </summary>
        /// <param name="playerid">The ID of the player to ban.</param>
        /// <param name="reason">The reason for the ban.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool BanEx(int playerid, string reason);

        /// <summary>
        ///     Sends an RCON command.
        /// </summary>
        /// <param name="command">The RCON command to be executed.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendRconCommand(string command);

        /// <summary>
        ///     Retrieve a string server variable, for example 'hostname'. Typing 'varlist' in the console will display a list of
        ///     available server variables.
        /// </summary>
        /// <param name="varname">The name of the string variable to retrieve.</param>
        /// <param name="value">An array to store the retrieved string in.</param>
        /// <param name="size">Maximum length of the return string.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetServerVarAsString(string varname, out string value, int size);

        /// <summary>
        ///     Get the integer value of a server variable, for example 'port'.
        /// </summary>
        /// <param name="varname">A string containing the name of the integer variable to retrieve.</param>
        /// <returns>The value of the specified server variable as an integer.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetServerVarAsInt(string varname);

        /// <summary>
        ///     Gets a boolean parameter from the server.cfg file for use in scripts. Typing varlist in the server will give a list
        ///     of server.cfg vars and their types.
        /// </summary>
        /// <param name="varname">Name of the server.cfg var you want to get the value of.</param>
        /// <returns>The value of the specified server var as a boolean.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetServerVarAsBool(string varname);

        /// <summary>
        ///     Gets a player's network stats and saves them into a string.
        /// </summary>
        /// <param name="playerid">The ID of the player you want to get the networkstats of.</param>
        /// <param name="retstr">The string to store the networkstats in, passed by reference.</param>
        /// <param name="size">The length of the string that should be stored.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerNetworkStats(int playerid, out string retstr, int size);

        /// <summary>
        ///     Gets the server's network stats and stores them in a string.
        /// </summary>
        /// <param name="retstr">The string to store the network stats in, passed by reference.</param>
        /// <param name="size">The length of the string to be stored.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetNetworkStats(out string retstr, int size);

        /// <summary>
        ///     Returns the SA-MP client revision as reported by the player.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the version of.</param>
        /// <param name="version">The string to store the player's version in, passed by reference.</param>
        /// <param name="len">The maximum size of the version.</param>
        /// <returns>True on success, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerVersion(int playerid, out string version, int len);

        /// <summary>
        ///     Create a menu.
        /// </summary>
        /// <param name="title">The title for the new menu.</param>
        /// <param name="columns">How many colums shall the new menu have.</param>
        /// <param name="x">The X position of the menu (640x460 canvas - 0 would put the menu at the far left).</param>
        /// <param name="y">The Y position of the menu (640x460 canvas - 0 would put the menu at the far top).</param>
        /// <param name="col1Width"> The width for the first column.</param>
        /// <param name="col2Width"> The width for the second column.</param>
        /// <returns>The ID of the new menu or -1 on failure.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreateMenu(string title, int columns, float x, float y, float col1Width,
            float col2Width);

        /// <summary>
        ///     Destroys the specified menu.
        /// </summary>
        /// <param name="menuid">The menu ID to destroy.</param>
        /// <returns>True if the destroying was successful, otherwise false.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyMenu(int menuid);

        /// <summary>
        ///     Adds an item to a specified menu.
        /// </summary>
        /// <param name="menuid">The menu id to add an item to.</param>
        /// <param name="column">The column to add the item to.</param>
        /// <param name="menutext">The title for the new menu item.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddMenuItem(int menuid, int column, string menutext);

        /// <summary>
        ///     Sets the caption of a column in a menu.
        /// </summary>
        /// <param name="menuid">ID of the menu which shall be manipulated.</param>
        /// <param name="column">Which column in the menu shall be manipulated.</param>
        /// <param name="columnheader">The caption-text for the column.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetMenuColumnHeader(int menuid, int column, string columnheader);

        /// <summary>
        ///     Shows a previously created menu for a player.
        /// </summary>
        /// <param name="menuid">The ID of the menu to show.</param>
        /// <param name="playerid">The ID of the player to whom the menu will be shown.</param>
        /// <returns>True on succeeded, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowMenuForPlayer(int menuid, int playerid);

        /// <summary>
        ///     Hides a menu for a player.
        /// </summary>
        /// <param name="menuid">The ID of the menu to hide.</param>
        /// <param name="playerid">The ID of the player that the menu will be hidden for.</param>
        /// <returns>True on succeeeded, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool HideMenuForPlayer(int menuid, int playerid);

        /// <summary>
        ///     Check whether the given menu has been created.
        /// </summary>
        /// <param name="menuid">The ID of the menu to check.</param>
        /// <returns>True if valid, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsValidMenu(int menuid);

        /// <summary>
        ///     Disable a menu.
        /// </summary>
        /// <param name="menuid">The menu to disable.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableMenu(int menuid);

        /// <summary>
        ///     Disable a specific row in a menu.
        /// </summary>
        /// <param name="menuid">The menu to disable a row of.</param>
        /// <param name="row">The row to disable.</param>
        /// <returns>True on succeeded, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableMenuRow(int menuid, int row);

        /// <summary>
        ///     Gets the ID of the menu the player is currently viewing.
        /// </summary>
        /// <param name="playerid">The ID of the player to check whether the menu is show for.</param>
        /// <returns>The ID of the player's currently shown menu or 255 on failure.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerMenu(int playerid);

        /// <summary>
        ///     Creates a textdraw.
        /// </summary>
        /// <param name="x">X-Coordinate.</param>
        /// <param name="y">Y-Coordinate.</param>
        /// <param name="text">The text in the textdraw.</param>
        /// <returns>The ID of the created textdraw.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int TextDrawCreate(float x, float y, string text);

        /// <summary>
        ///     Destroys a textdraw.
        /// </summary>
        /// <param name="text">The ID of the textdraw to destroy.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawDestroy(int text);

        /// <summary>
        ///     Sets the width and height of the letters.
        /// </summary>
        /// <param name="text">The TextDraw to change.</param>
        /// <param name="x">Width of a char.</param>
        /// <param name="y">Height of a char.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawLetterSize(int text, float x, float y);

        /// <summary>
        ///     Change the size of a textdraw (box if <see cref="TextDrawUseBox" /> is enabled and/or clickable area for use with
        ///     <see cref="TextDrawSetSelectable" />).
        /// </summary>
        /// <param name="text">The TextDraw to set the size of.</param>
        /// <param name="x">The size on the X axis (left/right) following the same 640x480 grid as TextDrawCreate.</param>
        /// <param name="y">The size on the Y axis (up/down) following the same 640x480 grid as TextDrawCreate.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawTextSize(int text, float x, float y);

        /// <summary>
        ///     Aligns the text in the draw area.
        /// </summary>
        /// <param name="text">The ID of the textdraw to set the alignment of.</param>
        /// <param name="alignment">1-left 2-centered 3-right.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawAlignment(int text, int alignment);

        /// <summary>
        ///     Sets the text color of a textdraw.
        /// </summary>
        /// <param name="text">The TextDraw to change.</param>
        /// <param name="color">The color in hexadecimal format.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawColor(int text, int color);

        /// <summary>
        ///     Toggle whether a textdraw uses a box.
        /// </summary>
        /// <param name="text">The textdraw to toggle the box on.</param>
        /// <param name="use">True to show a box or False to not show a box.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawUseBox(int text, bool use);

        /// <summary>
        ///     Adjusts the text box colour (only used if <see cref="TextDrawUseBox" /> is set to True).
        /// </summary>
        /// <remarks>
        ///     Opacity is set by the alpha intensity of colour (eg. color 0x000000FF has a solid black box opacity, whereas
        ///     0x000000AA has a semi-transparent black box opacity)
        /// </remarks>
        /// <param name="text">The TextDraw to change.</param>
        /// <param name="color">The colour.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawBoxColor(int text, int color);

        /// <summary>
        ///     Adds a shadow to the lower right side of the text. The shadow font matches the text font. The shadow can be cut by
        ///     the box area if the size is set too big for the area.
        /// </summary>
        /// <param name="text">The textdraw to change the shadow of.</param>
        /// <param name="size">The size of the shadow.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetShadow(int text, int size);

        /// <summary>
        ///     Sets the thickness of a textdraw's text's outline. <see cref="TextDrawBackgroundColor" /> can be used to change the
        ///     color.
        /// </summary>
        /// <param name="text">The ID of the text draw to set the outline thickness of.</param>
        /// <param name="size">The thickness of the outline, as an integer. 0 for no outline.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetOutline(int text, int size);

        /// <summary>
        ///     Adjusts the text draw area background color (the outline/shadow - NOT the box. For box color, see
        ///     <see cref="TextDrawBoxColor" />).
        /// </summary>
        /// <param name="text">The ID of the textdraw to set the background color of.</param>
        /// <param name="color">The color that the textdraw should be set to.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawBackgroundColor(int text, int color);

        /// <summary>
        ///     Changes the text font.
        /// </summary>
        /// <param name="text">The TextDraw to change.</param>
        /// <param name="font">
        ///     There are four font styles as shown below. Font value 4 specifies that this is a txd sprite; 5
        ///     specifies that this textdraw can display preview models. A font value greater than 5 does not display, and anything
        ///     greater than 16 crashes the client.
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawFont(int text, int font);

        /// <summary>
        ///     Appears to scale text spacing to a proportional ratio. Useful when using TextDrawLetterSize to ensure the text has
        ///     even character spacing.
        /// </summary>
        /// <param name="text">The ID of the textdraw to set the proportionality of.</param>
        /// <param name="set">True to enable proportionality, False to disable.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetProportional(int text, bool set);

        /// <summary>
        ///     Sets the text draw to be selectable or not.
        /// </summary>
        /// <param name="text">The textdraw id that should be made selectable.</param>
        /// <param name="set">Set the textdraw selectable (True) or non-selectable (False). By default this is False.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetSelectable(int text, bool set);

        /// <summary>
        ///     Shows a textdraw for a specific player.
        /// </summary>
        /// <param name="playerid">The ID of the player to show the textdraw for.</param>
        /// <param name="text">The ID of the textdraw to show.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawShowForPlayer(int playerid, int text);

        /// <summary>
        ///     Hides a textdraw for a specific player.
        /// </summary>
        /// <param name="playerid">The ID of the player that the textdraw should be hidden for.</param>
        /// <param name="text">The ID of the textdraw to hide.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawHideForPlayer(int playerid, int text);

        /// <summary>
        ///     Shows a textdraw for all players.
        /// </summary>
        /// <param name="text">The textdraw to show.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawShowForAll(int text);

        /// <summary>
        ///     Hides a text draw for all players.
        /// </summary>
        /// <param name="text">The TextDraw to hide.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawHideForAll(int text);

        /// <summary>
        ///     Changes the text on a textdraw.
        /// </summary>
        /// <param name="text">The TextDraw to change.</param>
        /// <param name="str">The new string for the TextDraw.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetString(int text, string str);

        /// <summary>
        ///     Set the model for a textdraw model preview.
        /// </summary>
        /// <param name="text">The textdraw id that will display the 3D preview.</param>
        /// <param name="modelindex">The GTA SA or SA:MP model ID to display.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetPreviewModel(int text, int modelindex);

        /// <summary>
        ///     Sets the rotation and zoom of a 3D model preview textdraw.
        /// </summary>
        /// <param name="text">The textdraw id that displays the 3D preview.</param>
        /// <param name="rotX">The X rotation value.</param>
        /// <param name="rotY">The Y rotation value.</param>
        /// <param name="rotZ">The Z rotation value.</param>
        /// <param name="zoom">
        ///     The zoom value, default value 1.0, smaller values make the camera closer and larger values make the
        ///     camera further away.
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetPreviewRot(int text, float rotX, float rotY, float rotZ, float zoom);

        /// <summary>
        ///     If a vehicle model is used in a 3D preview textdraw, this sets the two colour values for that vehicle.
        /// </summary>
        /// <param name="text">The textdraw id that is set to display a 3D vehicle model preview.</param>
        /// <param name="color1">The primary Color ID to set the vehicle to.</param>
        /// <param name="color2">The secondary Color ID to set the vehicle to.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetPreviewVehCol(int text, int color1, int color2);

        /// <summary>
        ///     Display the cursor and allow the player to select a textdraw.
        /// </summary>
        /// <param name="playerid">The ID of the player that should be able to select a textdraw.</param>
        /// <param name="hovercolor">The color of the textdraw when hovering over with mouse.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SelectTextDraw(int playerid, int hovercolor);

        /// <summary>
        ///     Cancel textdraw selection with the mouse.
        /// </summary>
        /// <param name="playerid">The ID of the player that should be the textdraw selection disabled.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CancelSelectTextDraw(int playerid);

        /// <summary>
        ///     Create a gangzone (colored radar area).
        /// </summary>
        /// <param name="minx">The X coordinate for the west side of the gangzone.</param>
        /// <param name="miny">The Y coordinate for the south side of the gangzone.</param>
        /// <param name="maxx">The X coordinate for the east side of the gangzone.</param>
        /// <param name="maxy">The Y coordinate for the north side of the gangzone.</param>
        /// <returns>The ID of the created zone.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GangZoneCreate(float minx, float miny, float maxx, float maxy);

        /// <summary>
        ///     Destroy a gangzone.
        /// </summary>
        /// <param name="zone">The ID of the zone to destroy.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneDestroy(int zone);

        /// <summary>
        ///     Show a gangzone for a player.
        /// </summary>
        /// <param name="playerid">The ID of the player you would like to show the gangzone for.</param>
        /// <param name="zone">The ID of the gang zone to show for the player.</param>
        /// <param name="color">The color to show the gang zone as. Alpha transparency supported.</param>
        /// <returns>True if the gangzone was shown, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneShowForPlayer(int playerid, int zone, int color);

        /// <summary>
        ///     GangZoneShowForAll shows a gangzone with the desired color to all players.
        /// </summary>
        /// <param name="zone">The ID of the gangzone to show.</param>
        /// <param name="color">The color of the gangzone.</param>
        /// <returns>True if the gangzone was shown, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneShowForAll(int zone, int color);

        /// <summary>
        ///     Hides a gangzone for a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to hide the gangzone for.</param>
        /// <param name="zone">The ID of the zone to hide.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneHideForPlayer(int playerid, int zone);

        /// <summary>
        ///     GangZoneHideForAll hides a gangzone from all players.
        /// </summary>
        /// <param name="zone">The zone to hide.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneHideForAll(int zone);

        /// <summary>
        ///     Makes a gangzone flash for a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to flash the gangzone for.</param>
        /// <param name="zone">The ID of the zone to flash.</param>
        /// <param name="flashcolor">The color the zone will flash.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneFlashForPlayer(int playerid, int zone, int flashcolor);

        /// <summary>
        ///     GangZoneFlashForAll flashes a gangzone for all players.
        /// </summary>
        /// <param name="zone">The zone to flash.</param>
        /// <param name="flashcolor">The color the zone will flash.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneFlashForAll(int zone, int flashcolor);

        /// <summary>
        ///     Stops a gangzone flashing for a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to stop the gangzone flashing for.</param>
        /// <param name="zone">The ID of the gangzonezone to stop flashing.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneStopFlashForPlayer(int playerid, int zone);

        /// <summary>
        ///     GangZoneStopFlashForAll stops a gangzone flashing for all players.
        /// </summary>
        /// <param name="zone">The zone to stop from flashing.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneStopFlashForAll(int zone);

        /// <summary>
        ///     Creates a 3D Text Label at a specific location in the world.
        /// </summary>
        /// <param name="text">The initial text string.</param>
        /// <param name="color">The text Color.</param>
        /// <param name="x">X-Coordinate.</param>
        /// <param name="y">Y-Coordinate.</param>
        /// <param name="z">Z-Coordinate.</param>
        /// <param name="drawDistance">The distance from where you are able to see the 3D Text Label.</param>
        /// <param name="virtualWorld">The virtual world in which you are able to see the 3D Text.</param>
        /// <param name="testLOS">Whether to test the line-of-sight so this text can't be seen through objects.</param>
        /// <returns>The ID of the newly created 3D Text Label.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int Create3DTextLabel(string text, int color, float x, float y, float z, float drawDistance,
            int virtualWorld, bool testLOS);

        /// <summary>
        ///     Delete a 3D text label.
        /// </summary>
        /// <param name="id">The ID of the 3D text label to delete.</param>
        /// <returns>True if the 3D text label was deleted, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Delete3DTextLabel(int id);

        /// <summary>
        ///     Attatch a 3D text label to player.
        /// </summary>
        /// <param name="id">The ID of the 3D Text label to attach.</param>
        /// <param name="playerid">The ID of the player to attach the 3D text label to.</param>
        /// <param name="offsetX">The X offset from the player.</param>
        /// <param name="offsetY">The Y offset from the player.</param>
        /// <param name="offsetZ">The Z offset from the player.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Attach3DTextLabelToPlayer(int id, int playerid, float offsetX, float offsetY,
            float offsetZ);

        /// <summary>
        ///     Attaches a 3D Text Label to a specific vehicle.
        /// </summary>
        /// <param name="id">The 3D Text Label you want to attach.</param>
        /// <param name="vehicleid">The vehicle you want to attach the 3D Text Label to.</param>
        /// <param name="offsetX">The Offset-X coordinate of the vehicle.</param>
        /// <param name="offsetY">The Offset-Y coordinate of the vehicle.</param>
        /// <param name="offsetZ">The Offset-Z coordinate of the vehicle.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Attach3DTextLabelToVehicle(int id, int vehicleid, float offsetX, float offsetY,
            float offsetZ);

        /// <summary>
        ///     Updates a 3D Text Label text and color.
        /// </summary>
        /// <param name="id">The 3D Text Label you want to update.</param>
        /// <param name="color">The color the 3D Text Label should have from now on.</param>
        /// <param name="text">The new text which the 3D Text Label should have from now on.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Update3DTextLabelText(int id, int color, string text);

        /// <summary>
        ///     Creates a 3D Text Label only for a specific player.
        /// </summary>
        /// <param name="playerid">The player which should see the newly created 3DText Label.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="color">The text color.</param>
        /// <param name="x">X Coordinate (or offset if attached).</param>
        /// <param name="y">Y Coordinate (or offset if attached).</param>
        /// <param name="z">Z Coordinate (or offset if attached).</param>
        /// <param name="drawDistance">The distance where you are able to see the 3D Text Label.</param>
        /// <param name="attachedplayer">
        ///     The player you want to attach the 3D Text Label to. (None:
        ///     <see cref="Misc.InvalidPlayerId" />).
        /// </param>
        /// <param name="attachedvehicle">
        ///     The vehicle you want to attach the 3D Text Label to. (None:
        ///     <see cref="Misc.InvalidVehicleId" />).
        /// </param>
        /// <param name="testLOS">Whether to test the line-of-sight so this text can't be seen through walls.</param>
        /// <returns>The ID of the newly created Player 3D Text Label.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePlayer3DTextLabel(int playerid, string text, int color, float x, float y, float z,
            float drawDistance, int attachedplayer, int attachedvehicle, bool testLOS);

        /// <summary>
        ///     Destroy a 3D Text Label.
        /// </summary>
        /// <param name="playerid">The player whose 3D text label to destroy.</param>
        /// <param name="id">The ID of the 3D Text Label to destroy.</param>
        /// <returns>True if destroyed, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DeletePlayer3DTextLabel(int playerid, int id);

        /// <summary>
        ///     Updates a player 3D Text Label's text and color.
        /// </summary>
        /// <param name="playerid">The ID of the player for which the 3D Text Label was created.</param>
        /// <param name="id">The 3D Text Label you want to update.</param>
        /// <param name="color">The color the 3D Text Label should have from now on.</param>
        /// <param name="text">The new text which the 3D Text Label should have from now on.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool UpdatePlayer3DTextLabelText(int playerid, int id, int color, string text);

        /// <summary>
        ///     Shows the player a synchronous (only one at a time) dialog box.
        /// </summary>
        /// <param name="playerid">The ID of the player to show the dialog to.</param>
        /// <param name="dialogid">
        ///     An ID to assign this dialog to, so responses can be processed. Max dialogid is 32767. Using
        ///     negative values will close any open dialog.
        /// </param>
        /// <param name="style">The style of the dialog.</param>
        /// <param name="caption">
        ///     The title at the top of the dialog. The length of the caption can not exceed more than 64
        ///     characters before it starts to cut off.
        /// </param>
        /// <param name="info">The text to display in the main dialog. Use \n to start a new line and \t to tabulate.</param>
        /// <param name="button1">The text on the left button.</param>
        /// <param name="button2">The text on the right button. Leave it blank to hide it.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowPlayerDialog(int playerid, int dialogid, int style, string caption, string info,
            string button1, string button2);

        /// <summary>
        ///     Sets a timer to call a function after some time.
        /// </summary>
        /// <param name="interval">Interval in milliseconds.</param>
        /// <param name="repeat">Boolean if the timer should occur repeatedly or only once.</param>
        /// <param name="args">An object containing information about the timer.</param>
        /// <returns>The ID of the timer that was started.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int SetTimer(int interval, bool repeat, object args);

        /// <summary>
        ///     Kills (stops) a running timer.
        /// </summary>
        /// <param name="timerid">The ID of the timer to kill.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool KillTimer(int timerid);

        /// <summary>
        ///     Returns a hash build from information of the player's PC.
        /// </summary>
        /// <remarks>
        ///     It is a non-reversible (lossy) hash derived from information about your San Andreas installation path.
        ///     It is not a unique ID.
        ///     It was added to assist owners of large servers who deal with constant attacks from cheaters and botters.
        ///     It has been in SA-MP for 2 years.
        ///     ///
        /// </remarks>
        /// <param name="playerid">The ID of the player whose gpci you'd like</param>
        /// <param name="buffer">A string to store the gpci, passed by reference.</param>
        /// <param name="size">The length of the string that should be stored.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        // ReSharper disable once InconsistentNaming
        public static extern bool gpci(int playerid, out string buffer, int size);

        /// <summary>
        ///     Returns a hash build from information of the player's PC.
        /// </summary>
        /// <remarks>
        ///     It is a non-reversible (lossy) hash derived from information about your San Andreas installation path.
        ///     It is not a unique ID.
        ///     It was added to assist owners of large servers who deal with constant attacks from cheaters and botters.
        ///     It has been in SA-MP for 2 years.
        ///     ///
        /// </remarks>
        /// <param name="playerid">The ID of the player whose gpci you'd like</param>
        /// <returns>The gpci value.</returns>
        // ReSharper disable once InconsistentNaming
        public static string gpci(int playerid)
        {
            string buffer;
            gpci(playerid, out buffer, 64);
            return buffer;
        }

        /// <summary>
        ///     Get the name of a weapon.
        /// </summary>
        /// <param name="weaponid">The ID of the weapon to get the name of.</param>
        /// <returns>The weapon's name.</returns>
        public static string GetWeaponName(int weaponid)
        {
            string name;
            GetWeaponName(weaponid, out name, 32);
            return name;
        }

        /// <summary>
        ///     Retrieve a string server variable, for example 'hostname'. Typing 'varlist' in the console will display a list of
        ///     available server variables.
        /// </summary>
        /// <param name="varname">The name of the string variable to retrieve.</param>
        /// <returns>The servervar as string.</returns>
        public static string GetServerVarAsString(string varname)
        {
            string value;
            GetServerVarAsString(varname, out value, 64);
            return value;
        }

        /// <summary>
        ///     Gets a player's network stats and saves them into a string.
        /// </summary>
        /// <param name="playerid">The ID of the player you want to get the networkstats of.</param>
        /// <returns>A string containing the networkstats.</returns>
        public static string GetPlayerNetworkStats(int playerid)
        {
            string retstr;
            GetPlayerNetworkStats(playerid, out retstr, 256);
            return retstr;
        }

        /// <summary>
        ///     Gets the server's network stats and stores them in a string.
        /// </summary>
        /// <returns>A string containing the network stats.</returns>
        public static string GetNetworkStats()
        {
            string retstr;
            GetNetworkStats(out retstr, 256);
            return retstr;
        }

        /// <summary>
        ///     Gets the version of a player's client.
        /// </summary>
        /// <param name="playerid">The player whose version to check.</param>
        /// <returns>The version of the client.</returns>
        public static string GetPlayerVersion(int playerid)
        {
            string version;
            GetPlayerVersion(playerid, out version, 64);
            return version;
        }

        /// <summary>
        ///     Creates a 3D Text Label at a specific location in the world.
        /// </summary>
        /// <param name="text">The initial text string.</param>
        /// <param name="color">The text Color.</param>
        /// <param name="position">The coordinates.</param>
        /// <param name="drawDistance">The distance from where you are able to see the 3D Text Label.</param>
        /// <param name="virtualWorld">The virtual world in which you are able to see the 3D Text.</param>
        /// <param name="testLOS">Whether to test the line-of-sight so this text can't be seen through objects.</param>
        /// <returns>The ID of the newly created 3D Text Label.</returns>
        public static int Create3DTextLabel(string text, Color color, Vector position, float drawDistance,
            int virtualWorld, bool testLOS)
        {
            return Create3DTextLabel(text, color, position.X, position.Y, position.Z, drawDistance, virtualWorld,
                testLOS);
        }

        /// <summary>
        ///     Creates a 3D Text Label only for a specific player.
        /// </summary>
        /// <param name="playerid">The player which should see the newly created 3DText Label.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="color">The text color.</param>
        /// <param name="position">The coordinates (or offset if attached).</param>
        /// <param name="drawDistance">The distance where you are able to see the 3D Text Label.</param>
        /// <param name="attachedplayer">
        ///     The player you want to attach the 3D Text Label to. (None:
        ///     <see cref="Misc.InvalidPlayerId" />).
        /// </param>
        /// <param name="attachedvehicle">
        ///     The vehicle you want to attach the 3D Text Label to. (None:
        ///     <see cref="Misc.InvalidVehicleId" />).
        /// </param>
        /// <param name="testLOS">Whether to test the line-of-sight so this text can't be seen through walls.</param>
        /// <returns>The ID of the newly created Player 3D Text Label.</returns>
        public static int CreatePlayer3DTextLabel(int playerid, string text, int color, Vector position,
            float drawDistance, int attachedplayer, int attachedvehicle, bool testLOS)
        {
            return CreatePlayer3DTextLabel(0, text, color, position.X, position.Y, position.Z, drawDistance,
                attachedplayer, attachedvehicle, testLOS);
        }

        /// <summary>
        ///     Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.
        /// </summary>
        /// <param name="modelid">The skin which the player will spawn with.</param>
        /// <param name="position">The coordinate of the spawnpoint of this class.</param>
        /// <param name="zAngle">The direction in which the player should face after spawning.</param>
        /// <param name="weapon1">The first spawn-weapon for the player.</param>
        /// <param name="weapon1Ammo">The amount of ammunition for the primary spawnweapon.</param>
        /// <param name="weapon2">The second spawn-weapon for the player.</param>
        /// <param name="weapon2Ammo">The amount of ammunition for the second spawnweapon.</param>
        /// <param name="weapon3">The third spawn-weapon for the player.</param>
        /// <param name="weapon3Ammo">The amount of ammunition for the third spawnweapon.</param>
        /// <returns>
        ///     The ID of the class which was just added. 300 if the class limit (300) was reached. The highest possible class
        ///     ID is 299.
        /// </returns>
        public static int AddPlayerClass(int modelid, Vector position, float zAngle, Weapon weapon1, int weapon1Ammo,
            Weapon weapon2, int weapon2Ammo, Weapon weapon3, int weapon3Ammo)
        {
            return AddPlayerClass(modelid, position.X, position.Y, position.Z, zAngle, (int) weapon1, weapon1Ammo,
                (int) weapon2, weapon2Ammo, (int) weapon3, weapon3Ammo);
        }

        /// <summary>
        ///     This function is exactly the same as the <see cref="AddPlayerClass" /> function, with the addition of a team
        ///     parameter.
        /// </summary>
        /// <param name="teamid">The team you want the player to spawn in.</param>
        /// <param name="modelid">The skin which the player will spawn with.</param>
        /// <param name="position">The coordinate of the class' spawn position.</param>
        /// <param name="zAngle">The direction in which the player will face after spawning.</param>
        /// <param name="weapon1">The first spawn-weapon for the player.</param>
        /// <param name="weapon1Ammo">The amount of ammunition for the first spawnweapon.</param>
        /// <param name="weapon2">The second spawn-weapon for the player.</param>
        /// <param name="weapon2Ammo">The amount of ammunition for the second spawnweapon.</param>
        /// <param name="weapon3">The third spawn-weapon for the player.</param>
        /// <param name="weapon3Ammo">The amount of ammunition for the third spawnweapon.</param>
        /// <returns>The ID of the class that was just created.</returns>
        public static int AddPlayerClassEx(int teamid, int modelid, Vector position, float zAngle, Weapon weapon1,
            int weapon1Ammo, Weapon weapon2, int weapon2Ammo, Weapon weapon3, int weapon3Ammo)
        {
            return AddPlayerClassEx(teamid, modelid, position.X, position.Y, position.Z, zAngle, (int) weapon1,
                weapon1Ammo, (int) weapon2, weapon2Ammo, (int) weapon3, weapon3Ammo);
        }
    }
}