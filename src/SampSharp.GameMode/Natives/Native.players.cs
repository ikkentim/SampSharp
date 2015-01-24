// SampSharp
// Copyright 2015 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Runtime.CompilerServices;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Natives
{
    public static partial class Native
    {
        /// <summary>
        ///     This function can be used to change the spawn information of a specific player. It allows you to automatically set
        ///     someone's spawn weapons, their team, skin and spawn position, normally used in case of minigames or automatic-spawn
        ///     systems. This function is more crash-safe then using SetPlayerSkin in OnPlayerSpawn and/or OnPlayerRequestClass,
        ///     even though this has been fixed in 0.2.
        /// </summary>
        /// <param name="playerid">The PlayerID of who you want to set the spawn information.</param>
        /// <param name="team">The Team-ID of the chosen player.</param>
        /// <param name="skin">The skin which the player will spawn with.</param>
        /// <param name="x">The X-coordinate of the player's spawn position.</param>
        /// <param name="y">The Y-coordinate of the player's spawn position.</param>
        /// <param name="z">The Z-coordinate of the player's spawn position.</param>
        /// <param name="rotation">The direction in which the player needs to be facing after spawning.</param>
        /// <param name="weapon1">The first spawn-weapon for the player.</param>
        /// <param name="weapon1Ammo">The amount of ammunition for the primary spawnweapon.</param>
        /// <param name="weapon2">The second spawn-weapon for the player.</param>
        /// <param name="weapon2Ammo">The amount of ammunition for the second spawnweapon.</param>
        /// <param name="weapon3">The third spawn-weapon for the player.</param>
        /// <param name="weapon3Ammo">The amount of ammunition for the third spawnweapon.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetSpawnInfo(int playerid, int team, int skin, float x, float y, float z,
            float rotation, int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        /// <summary>
        ///     (Re)Spawns a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to spawn.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SpawnPlayer(int playerid);

        /// <summary>
        ///     Set a player's position.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the position of.</param>
        /// <param name="x">The X coordinate to position the player at.</param>
        /// <param name="y">The Y coordinate to position the player at.</param>
        /// <param name="z">The Z coordinate to position the player at.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerPos(int playerid, float x, float y, float z);

        /// <summary>
        ///     This sets the players position then adjusts the players z-coordinate to the nearest solid ground under the
        ///     position.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the position of.</param>
        /// <param name="x">The X coordinate to position the player at.</param>
        /// <param name="y">The Y coordinate to position the player at.</param>
        /// <param name="z">The Z coordinate to position the player at.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerPosFindZ(int playerid, float x, float y, float z);

        /// <summary>
        ///     Get the X Y Z coordinates of a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the position of</param>
        /// <param name="x">A float to store the X coordinate in, passed by reference.</param>
        /// <param name="y">A float to store the Y coordinate in, passed by reference.</param>
        /// <param name="z">A float to store the Z coordinate in, passed by reference.</param>
        /// <returns>This function doesn't return a specific value</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerPos(int playerid, out float x, out float y, out float z);

        /// <summary>
        ///     Set a player's facing angle.
        /// </summary>
        /// <remarks>
        ///     Angles are reversed in GTA:SA - 90 degrees would be East in the real world, but in GTA:SA 90 is in fact West. North
        ///     and South are still 0/360 and 180. To convert this, simply do 360 - angle.
        /// </remarks>
        /// <param name="playerid">The ID of the player to set the facing angle of.</param>
        /// <param name="angle">The angle the player should face.</param>
        /// <returns>This function doesn't return a specific value</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerFacingAngle(int playerid, float angle);

        /// <summary>
        ///     Return angle of the direction the player is facing.
        /// </summary>
        /// <param name="playerid">The player you want to get the angle of.</param>
        /// <param name="angle">The Float to store the angle in, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerFacingAngle(int playerid, out float angle);

        /// <summary>
        ///     Check if a player is in range of a point.
        /// </summary>
        /// <param name="playerid">The ID of the player.</param>
        /// <param name="range">The furthest distance the player can be from the point to be in range.</param>
        /// <param name="x">The X coordinate of the point to check the range to.</param>
        /// <param name="y">The Y coordinate of the point to check the range to.</param>
        /// <param name="z">The Z coordinate of the point to check the range to.</param>
        /// <returns>True if the player is in range of the point, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInRangeOfPoint(int playerid, float range, float x, float y, float z);

        /// <summary>
        ///     Calculate the distance between a player and a map coordinate.
        /// </summary>
        /// <param name="playerid">The ID of the player to calculate the distance from.</param>
        /// <param name="x">The X map coordinate.</param>
        /// <param name="y">The Y map coordinate.</param>
        /// <param name="z">The Z map coordinate.</param>
        /// <returns>The distance between the player and the point as a float.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern float GetPlayerDistanceFromPoint(int playerid, float x, float y, float z);

        /// <summary>
        ///     Checks if a player is streamed in another player's client.
        /// </summary>
        /// <remarks>
        ///     Players aren't streamed in on their own client, so if playerid is the same as forplayerid it will return false!
        /// </remarks>
        /// <remarks>
        ///     Players stream out if they are more than 150 meters away (see server.cfg - stream_distance)
        /// </remarks>
        /// <param name="playerid">The ID of the player to check is streamed in.</param>
        /// <param name="forplayerid">The ID of the player to check if playerid is streamed in for.</param>
        /// <returns>True if forplayerid is streamed in for playerid, False if not.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerStreamedIn(int playerid, int forplayerid);

        /// <summary>
        ///     Set the player's interior.
        /// </summary>
        /// <param name="playerid">The ID of the player to setthe interior of.</param>
        /// <param name="interiorid">The interior ID to set the player in.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerInterior(int playerid, int interiorid);

        /// <summary>
        ///     Retrieves the player's current interior.
        /// </summary>
        /// <param name="playerid">The player to get the interior ID of.</param>
        /// <returns>The interior ID the player is currently in.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerInterior(int playerid);

        /// <summary>
        ///     Set the health level of a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the health of.</param>
        /// <param name="health">The value to set the player's health to.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerHealth(int playerid, float health);

        /// <summary>
        ///     The function GetPlayerHealth allows you to retrieve the health of a player. Useful for cheat detection, among other
        ///     things.
        /// </summary>
        /// <param name="playerid">The ID of the player.</param>
        /// <param name="health">Float to store health, passed by reference.</param>
        /// <returns>True if succeeded, False if not.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerHealth(int playerid, out float health);

        /// <summary>
        ///     Set a player's armour level.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the armour of.</param>
        /// <param name="armour">The amount of armour to set, as a percentage (float).</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerArmour(int playerid, float armour);

        /// <summary>
        ///     This function stores the armour of a player into a variable.
        /// </summary>
        /// <param name="playerid">The ID of the player that you want to get the armour of.</param>
        /// <param name="armour">The float to to store the armour in, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerArmour(int playerid, out float armour);

        /// <summary>
        ///     Set the ammo of a player's weapon.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the weapon ammo of.</param>
        /// <param name="weaponid">The ID of the weapon to set the ammo of.</param>
        /// <param name="ammo">The amount of ammo to set.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerAmmo(int playerid, int weaponid, int ammo);

        /// <summary>
        ///     Returns the amount of ammunition the player has in his active weapon slot.
        /// </summary>
        /// <remarks>
        ///     The ammo can hold 16-bit values, therefore values over 32767 will return erroneous values.
        /// </remarks>
        /// <param name="playerid">ID of the player.</param>
        /// <returns>The amount of ammunition the player has in his active weapon slot.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerAmmo(int playerid);

        /// <summary>
        ///     Checks the state of a player's weapon.
        /// </summary>
        /// <param name="playerid">The ID of the player to obtain the state of.</param>
        /// <returns>The state of the player's weapon.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerWeaponState(int playerid);

        /// <summary>
        ///     Check who a player is aiming at.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the target of.</param>
        /// <returns>The ID of the target player, or <see cref="Misc.InvalidPlayerId" /> if none.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerTargetPlayer(int playerid);

        /// <summary>
        ///     Set the team of a player.
        /// </summary>
        /// <remarks>
        ///     Players can not damage/kill players on the same team unless they use a knife to slit their throat. Players are also
        ///     unable to damage vehicles driven by a player from the same team. This can be enabled with
        ///     <see cref="EnableVehicleFriendlyFire" />.
        ///     255 (or <see cref="Misc.NoTeam" />) is the default team to be able to shoot other players, not 0.
        /// </remarks>
        /// <param name="playerid">The ID of the player you want to set the team of.</param>
        /// <param name="teamid">The team to put the player in. Use <see cref="Misc.NoTeam" /> to remove the player from any team.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerTeam(int playerid, int teamid);

        /// <summary>
        ///     Get the ID of the team the player is on.
        /// </summary>
        /// <param name="playerid">The ID of the player to return the team of.</param>
        /// <returns>
        ///     The ID of the team the player is on, or 255 (defined as <see cref="Misc.NoTeam" />) if they aren't on a team
        ///     (default).
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerTeam(int playerid);

        /// <summary>
        ///     Set a player's score. Players' scores are shown in the scoreboard (hold TAB).
        /// </summary>
        /// <param name="playerid">The ID of the player to set the score of.</param>
        /// <param name="score">The value to set the player's score to.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerScore(int playerid, int score);

        /// <summary>
        ///     This function returns a player's score as it was set using <see cref="SetPlayerScore" />
        /// </summary>
        /// <param name="playerid">The player to get the score of.</param>
        /// <returns>The player's score.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerScore(int playerid);

        /// <summary>
        ///     Checks the player's level of drunkenness.
        /// </summary>
        /// <remarks>
        ///     If the level is less than 2000, the player is sober. The player's level of drunkness goes down slowly automatically
        ///     (26 levels per second) but will always reach zero at the end. The higher drunkenness levels affect the player's
        ///     camera, and the car driving handling. The level of drunkenness increases when the player drinks from a bottle (You
        ///     can use <see cref="SetPlayerSpecialAction(int,SpecialAction)" /> to give them bottles).
        /// </remarks>
        /// <param name="playerid">The player you want to check the drunkenness level of.</param>
        /// <returns>An integer with the level of drunkenness of the player.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerDrunkLevel(int playerid);

        /// <summary>
        ///     Sets the drunk level of a player which makes the player's camera sway and vehicles hard to control.
        /// </summary>
        /// <remarks>
        ///     Players' drunk level will automatically decrease over time, based on their FPS (players with 50 FPS will lose 50
        ///     'levels' per second. This is useful for determining a player's FPS!).
        ///     In the drunk level will decrement and stop at zero.
        ///     Levels over 2000 make the player drunk (camera swaying and vehicles difficult to control).
        ///     Max drunk level is 50000.
        ///     While the drunk level is above 5000, the player's HUD (radar etc.) will be hidden.
        /// </remarks>
        /// <param name="playerid">The ID of the player to set the drunkenness of.</param>
        /// <param name="level">The level of drunkenness to set.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerDrunkLevel(int playerid, int level);

        /// <summary>
        ///     This function allows you to change the color of a player currently in-game.
        /// </summary>
        /// <param name="playerid">The player to change the color of.</param>
        /// <param name="color">The color to set, as an integer</param>
        /// <returns>his function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerColor(int playerid, int color);

        /// <summary>
        ///     This function returns the color the player is currently using.
        /// </summary>
        /// <remarks>
        ///     GetPlayerColor will return nothing unless SetPlayerColor has been used!
        /// </remarks>
        /// <param name="playerid">The player you want to know the color of.</param>
        /// <returns>The players color.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerColor(int playerid);

        /// <summary>
        ///     Set the skin of a player.
        /// </summary>
        /// <remarks>
        ///     If a player's skin is set when they are crouching, in a vehicle, or performing certain animations, they will become
        ///     frozen or otherwise glitched. This can be fixed by using <see cref="TogglePlayerControllable" />. Players can be
        ///     detected as being crouched through <see cref="GetPlayerSpecialAction" />.
        /// </remarks>
        /// <param name="playerid">The ID of the player to set the skin of.</param>
        /// <param name="skinid">The skin the player should use.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerSkin(int playerid, int skinid);

        /// <summary>
        ///     Returns the class of the players skin.
        /// </summary>
        /// <param name="playerid">The player you want to get the skin from.</param>
        /// <returns>The skin id (0 if invalid).</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerSkin(int playerid);

        /// <summary>
        ///     Give a player a weapon with a specified amount of ammo.
        /// </summary>
        /// <param name="playerid">The ID of the player to give a weapon to.</param>
        /// <param name="weaponid">The ID of the weapon to give to the player.</param>
        /// <param name="ammo">The amount of ammo to give to the player.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GivePlayerWeapon(int playerid, int weaponid, int ammo);

        /// <summary>
        ///     Removes all weapons from a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to remove the weapons of.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ResetPlayerWeapons(int playerid);

        /// <summary>
        ///     Sets the armed weapon of the player.
        /// </summary>
        /// <param name="playerid">The ID of the player to arm with a weapon.</param>
        /// <param name="weaponid">The ID of the weapon that the player should be armed with.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerArmedWeapon(int playerid, int weaponid);

        /// <summary>
        ///     Get the weapon and ammo in a specific player's weapon slot.
        /// </summary>
        /// <param name="playerid">The ID of the player whose weapon data to retrieve.</param>
        /// <param name="slot">The weapon slot to get data for (0-12).</param>
        /// <param name="weapon">The variable in which to store the weapon ID, passed by reference.</param>
        /// <param name="ammo">The variable in which to store the ammo, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerWeaponData(int playerid, int slot, out int weapon, out int ammo);

        /// <summary>
        ///     Give (or take) money to/from a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to give money to.</param>
        /// <param name="money">The amount of money to give the player. Use a minus value to take money.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GivePlayerMoney(int playerid, int money);

        /// <summary>
        ///     Reset a player's money to $0.
        /// </summary>
        /// <param name="playerid">The ID of the player to reset the money of.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ResetPlayerMoney(int playerid);

        /// <summary>
        ///     Sets the name of a player.
        /// </summary>
        /// <remarks>
        ///     If you set the player's name to the same name except different cased letters (i.e. "heLLO" to "hello"), it will not
        ///     work. If used in <see cref="BaseMode.OnPlayerConnect" />, the new name will not be shown for the connecting player.
        /// </remarks>
        /// <param name="playerid">The ID of the player to set the name of.</param>
        /// <param name="name">The name to set.</param>
        /// <returns>
        ///     1 if the name was changed, 0 if the player is already using that name or -1 when the name cannot be changed.
        ///     (it's in use, too long or has invalid characters)
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int SetPlayerName(int playerid, string name);

        /// <summary>
        ///     Retrieves the amount of money a player has.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the money of.</param>
        /// <returns>The amount of money the player has.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerMoney(int playerid);

        /// <summary>
        ///     Get a player's current state.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the current state of.</param>
        /// <returns>The player's current state as an integer.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerState(int playerid);

        /// <summary>
        ///     Get the specified player's IP and store it in a string.
        /// </summary>
        /// <remarks>
        ///     This function does not work when used in <see cref="BaseMode.OnPlayerDisconnect" /> because the player is already
        ///     disconnected. It will return an invalid IP (255.255.255.255). Save players' IPs under
        ///     <see cref="BaseMode.OnPlayerConnect" /> if they need to be used under <see cref="BaseMode.OnPlayerConnect" />.
        /// </remarks>
        /// <param name="playerid">The ID of the player to get the IP of.</param>
        /// <param name="ip">The string to store the player's IP in, passed by reference</param>
        /// <param name="size">The maximum size of the IP. (Recommended 16)</param>
        /// <returns>True on success, False otherwise.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerIp(int playerid, out string ip, int size);

        /// <summary>
        ///     Get the ping of a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the ping of.</param>
        /// <returns>The current ping of the player (expressed in milliseconds).</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerPing(int playerid);

        /// <summary>
        ///     Returns the ID of the player's current weapon.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the weapon of.</param>
        /// <returns>The ID of the player's current weapon.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerWeapon(int playerid);

        /// <summary>
        ///     Check which keys a player is pressing.
        /// </summary>
        /// <remarks>
        ///     Only the FUNCTION of keys can be detected; not actual keys. You can not detect if a player presses space, but you
        ///     can detect if they press sprint (which can be mapped (assigned) to ANY key, but is space by default)).
        /// </remarks>
        /// <param name="playerid">The ID of the player to detect the keys of.</param>
        /// <param name="keys">A set of bits containing the player's key states</param>
        /// <param name="updown">Up or Down value, passed by reference.</param>
        /// <param name="leftright">Left or Right value, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerKeys(int playerid, out int keys, out int updown, out int leftright);

        /// <summary>
        ///     Get a player's name.
        /// </summary>
        /// <remarks>
        ///     A player's name can be up to 24 characters long.
        ///     This is defined as <see cref="Limits.MaxPlayerName" />.
        ///     Strings to store names in should be made this size, plus one extra cell for the null terminating character.
        /// </remarks>
        /// <param name="playerid">The ID of the player to get the name of.</param>
        /// <param name="name">The string to store the name in, passed by reference.</param>
        /// <param name="size">The length of the string that should be stored.</param>
        /// <returns>The length of the player's name.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerName(int playerid, out string name, int size);

        /// <summary>
        ///     Sets the clock of the player to a specific value. This also changes the daytime. (night/day etc.)
        /// </summary>
        /// <param name="playerid">The ID of the player.</param>
        /// <param name="hour">Hour to set (0-23).</param>
        /// <param name="minute">Minutes to set (0-59).</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerTime(int playerid, int hour, int minute);

        /// <summary>
        ///     Get the player's current game time. Set by <see cref="SetWorldTime" />, <see cref="SetWorldTime" />, or by
        ///     <see cref="TogglePlayerClock" />.
        /// </summary>
        /// <param name="playerid">The ID of the player that you want to get the time of.</param>
        /// <param name="hour">The variable to store the hour in, passed by reference.</param>
        /// <param name="minute">The variable to store the minutes in, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerTime(int playerid, out int hour, out int minute);

        /// <summary>
        ///     Show/Hide the in-game clock (top right corner) for a specific player.
        /// </summary>
        /// <remarks>
        ///     Time is not synced with other players!
        /// </remarks>
        /// <param name="playerid">The player whose clock you want to enable/disable.</param>
        /// <param name="toggle">True to show, False to hide.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TogglePlayerClock(int playerid, bool toggle);

        /// <summary>
        ///     Set a player's weather. If <see cref="TogglePlayerClock" /> has been used to enable a player's clock, weather
        ///     changes will interpolate (gradually change), otherwise will change instantly.
        /// </summary>
        /// <param name="playerid">The ID of the player whose weather to set.</param>
        /// <param name="weather">The weather to set.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerWeather(int playerid, int weather);

        /// <summary>
        ///     Forces a player to go back to class selection.
        /// </summary>
        /// <remarks>
        ///     The player will not return to class selection until they re-spawn. This can be achieved with
        ///     <see cref="TogglePlayerSpectating" />
        /// </remarks>
        /// <param name="playerid">The player to send back to class selection.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ForceClassSelection(int playerid);

        /// <summary>
        ///     Set a player's wanted level (6 brown stars under HUD).
        /// </summary>
        /// <param name="playerid">The ID of the player to set the wanted level of.</param>
        /// <param name="level">The wanted level to set for the player (0-6).</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerWantedLevel(int playerid, int level);

        /// <summary>
        ///     Gets the wanted level of a player.
        /// </summary>
        /// <param name="playerid">The ID of the player that you want to get the wanted level of.</param>
        /// <returns>The player's wanted level.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerWantedLevel(int playerid);

        /// <summary>
        ///     Set a player's special fighting style. To use in-game, aim and press the 'secondary attack' key (ENTER by default).
        /// </summary>
        /// <remarks>
        ///     This does not affect normal fist attacks - only special/secondary attacks (aim + press 'secondary attack' key).
        /// </remarks>
        /// <param name="playerid">The ID of player to set the fighting style of.</param>
        /// <param name="style">The fighting style that should be set.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerFightingStyle(int playerid, int style);

        /// <summary>
        ///     Returns what fighting style the player currently using.
        /// </summary>
        /// <param name="playerid">The player you want to know the fighting style of.</param>
        /// <returns>Returns the fighting style of the player.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerFightingStyle(int playerid);

        /// <summary>
        ///     Makes the player move in that direction at the given speed.
        /// </summary>
        /// <param name="playerid">The player to apply the speed to.</param>
        /// <param name="x">How much speed in the X direction will be applied.</param>
        /// <param name="y">How much speed in the Y direction will be applied.</param>
        /// <param name="z">How much speed in the Z direction will be applied.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerVelocity(int playerid, float x, float y, float z);

        /// <summary>
        ///     Gets the velocity at which the player is moving in the three directions, X, Y and Z. This can be useful for
        ///     speedometers.
        /// </summary>
        /// <param name="playerid">The player to get the speed from.</param>
        /// <param name="x">The float to store the X velocity in, passed by reference.</param>
        /// <param name="y">The float to store the Y velocity in, passed by reference.</param>
        /// <param name="z">The float to store the Z velocity in, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerVelocity(int playerid, out float x, out float y, out float z);

        /// <summary>
        ///     This function plays a crime report for a player - just like in single-player when CJ commits a crime.
        /// </summary>
        /// <param name="playerid">The ID of the player that will hear the crime report.</param>
        /// <param name="suspectid">The ID of the suspect player which will be described in the crime report.</param>
        /// <param name="crime">The crime ID, which will be reported as a 10-code (i.e. 10-16 if 16 was passed as the crimeid).</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayCrimeReportForPlayer(int playerid, int suspectid, int crime);

        /// <summary>
        ///     Play an 'audio stream' for a player. Normal audio files also work (e.g. MP3).
        /// </summary>
        /// <param name="playerid">The ID of the player to play the audio for.</param>
        /// <param name="url">
        ///     The url to play. Valid formats are mp3 and ogg/vorbis. A link to a .pls (playlist) file will play
        ///     that playlist.
        /// </param>
        /// <param name="posX">The X position at which to play the audio. Default 0.0. Has no effect unless usepos is set to True.</param>
        /// <param name="posY">The Y position at which to play the audio. Default 0.0. Has no effect unless usepos is set to True.</param>
        /// <param name="posZ">The Z position at which to play the audio. Default 0.0. Has no effect unless usepos is set to True.</param>
        /// <param name="distance">The distance over which the audio will be heard. Has no effect unless usepos is set to True.</param>
        /// <param name="usepos">Use the positions and distance specified. Default False.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayAudioStreamForPlayer(int playerid, string url, float posX, float posY, float posZ,
            float distance, bool usepos);

        /// <summary>
        ///     Stops the current audio stream for a player.
        /// </summary>
        /// <param name="playerid">The player you want to stop the audio stream for.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StopAudioStreamForPlayer(int playerid);

        /// <summary>
        ///     Loads or unloads an interior script for a player. (for example the ammunation menu)
        /// </summary>
        /// <param name="playerid">The ID of the player to load the interior script for.</param>
        /// <param name="shopname"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerShopName(int playerid, string shopname);

        /// <summary>
        ///     Set the skill level of a certain weapon type for a player.
        /// </summary>
        /// <remarks>
        ///     The skill parameter is NOT the weapon ID, it is the skill type.
        /// </remarks>
        /// <param name="playerid">The ID of the player to set the weapon skill of.</param>
        /// <param name="skill">The weapon type you want to set the skill of.</param>
        /// <param name="level">
        ///     The skill level to set for that weapon, ranging from 0 to 999. (A level out of range will max it
        ///     out)
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerSkillLevel(int playerid, int skill, int level);

        /// <summary>
        ///     Get the ID of the vehicle that the player is surfing.
        /// </summary>
        /// <param name="playerid">The ID of the player you want to know the surfing vehicle ID of.</param>
        /// <returns>
        ///     The ID of the vehicle that the player is surfing, or <see cref="Misc.InvalidVehicleId" /> if they are not
        ///     surfing or the vehicle has no driver.
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerSurfingVehicleID(int playerid);

        /// <summary>
        ///     Returns the ID of the object the player is surfing on.
        /// </summary>
        /// <param name="playerid">The ID of the player surfing the object.</param>
        /// <returns>
        ///     The ID of the moving object the player is surfing. If the player isn't surfing a moving object, it will return
        ///     <see cref="Misc.InvalidObjectId" />
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerSurfingObjectID(int playerid);

        /// <summary>
        ///     Removes a standard San Andreas model for a single player within a specified range.
        /// </summary>
        /// <param name="playerid">The ID of the player to remove the objects for.</param>
        /// <param name="modelid">The model to remove.</param>
        /// <param name="x">The X coordinate around which the objects will be removed.</param>
        /// <param name="y">The Y coordinate around which the objects will be removed.</param>
        /// <param name="z">The Z coordinate around which the objects will be removed.</param>
        /// <param name="radius">The radius. Objects within this radius from the coordinates above will be removed.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemoveBuildingForPlayer(int playerid, int modelid, float x, float y, float z,
            float radius);

        /// <summary>
        ///     Attach an object to a specific bone on a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to attach the object to.</param>
        /// <param name="index">The index (slot) to assign the object to (0-9).</param>
        /// <param name="modelid">The model to attach.</param>
        /// <param name="bone">The bone to attach the object to.</param>
        /// <param name="offsetX">X axis offset for the object position.</param>
        /// <param name="offsetY">Y axis offset for the object position.</param>
        /// <param name="offsetZ">Z axis offset for the object position.</param>
        /// <param name="rotX">X axis rotation of the object.</param>
        /// <param name="rotY">Y axis rotation of the object.</param>
        /// <param name="rotZ">Z axis rotation of the object.</param>
        /// <param name="scaleX">X axis scale of the object.</param>
        /// <param name="scaleY">Y axis scale of the object.</param>
        /// <param name="scaleZ">Z axis scale of the object.</param>
        /// <param name="materialcolor1">The first object color to set, as an integer or hex in ARGB color format.</param>
        /// <param name="materialcolor2">The second object color to set, as an integer or hex in ARGB color format.</param>
        /// <returns>True on success, False otherwise.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerAttachedObject(int playerid, int index, int modelid, int bone, float offsetX,
            float offsetY, float offsetZ, float rotX, float rotY, float rotZ, float scaleX, float scaleY,
            float scaleZ, int materialcolor1, int materialcolor2);

        /// <summary>
        ///     Remove an attached object from a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to remove the object from.</param>
        /// <param name="index">
        ///     The index of the object to remove (set with
        ///     <see cref="SetPlayerAttachedObject(int,int,int,int,Vector,Vector,Vector,Color,Color)" />).
        /// </param>
        /// <returns>True on success, False otherwise.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemovePlayerAttachedObject(int playerid, int index);

        /// <summary>
        ///     Check if a player has an object attached in the specified index (slot).
        /// </summary>
        /// <param name="playerid">The ID of the player to check.</param>
        /// <param name="index">The index (slot) to check.</param>
        /// <returns>True if the slot is used, False otherwise.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerAttachedObjectSlotUsed(int playerid, int index);

        /// <summary>
        ///     Enter edition mode for an attached object.
        /// </summary>
        /// <param name="playerid">The ID of the player to enter in to edition mode.</param>
        /// <param name="index">The index (slot) of the attached object to edit.</param>
        /// <returns>True on success, False otherwise.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EditAttachedObject(int playerid, int index);

        /// <summary>
        ///     Creates a textdraw for a single player. This can be used as a way around the global text-draw limit.
        /// </summary>
        /// <param name="playerid">The ID of the player to create the textdraw for.</param>
        /// <param name="x">X-Coordinate.</param>
        /// <param name="y">Y-Coordinate.</param>
        /// <param name="text">The text in the textdraw.</param>
        /// <returns>The ID of the created textdraw.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePlayerTextDraw(int playerid, float x, float y, string text);

        /// <summary>
        ///     Destroy a player-textdraw.
        /// </summary>
        /// <param name="playerid">The ID of the player who's player-textdraw to destroy.</param>
        /// <param name="text">The ID of the textdraw to destroy.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawDestroy(int playerid, int text);

        /// <summary>
        ///     Sets the width and height of the letters in a player-textdraw.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-textdraw to set the letter size of.</param>
        /// <param name="text">The ID of the player-textdraw to change the letter size of.</param>
        /// <param name="x">Width of a char.</param>
        /// <param name="y">Height of a char.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawLetterSize(int playerid, int text, float x, float y);

        /// <summary>
        ///     Change the size of a player-textdraw (box if <see cref="PlayerTextDrawUseBox" /> is enabled and/or clickable area
        ///     for use with <see cref="PlayerTextDrawSetSelectable" />).
        /// </summary>
        /// <remarks>
        ///     When used with <see cref="PlayerTextDrawAlignment(int,int,TextDrawAlignment)" /> of alignment 3 (right), the x and
        ///     y are the coordinates of the left most corner of the box. For alignment 2 (center) the x and y values need to
        ///     inverted (switch the two) and the x value is the overall width of the box. For all other alignments the x and y
        ///     coordinates are for the right most corner of the box.
        ///     The TextDraw box starts 10.0 units up and 5.0 to the left as the origin (<see cref="TextDrawCreate" /> coordinate)
        ///     This function defines the clickable area for use with <see cref="PlayerTextDrawSetSelectable" />, whether a box is
        ///     shown or not.
        /// </remarks>
        /// <param name="playerid">The ID of the player whose player-textdraw to set the size of.</param>
        /// <param name="text">The ID of the player-textdraw to set the size of.</param>
        /// <param name="x">he size on the X axis (left/right) following the same 640x480 grid as <see cref="TextDrawCreate" />.</param>
        /// <param name="y">The size on the Y axis (up/down) following the same 640x480 grid as <see cref="TextDrawCreate" />.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawTextSize(int playerid, int text, float x, float y);

        /// <summary>
        ///     Set the text alignment of a player-textdraw.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-textdraw to set the alignment of.</param>
        /// <param name="text">The ID of the player-textdraw to set the alignment of.</param>
        /// <param name="alignment">Algihnment of the player-textdraw, 1-left, 2-centered, 3-right.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawAlignment(int playerid, int text, int alignment);

        /// <summary>
        ///     Sets the text color of a player-textdraw.
        /// </summary>
        /// <remarks>
        ///     You can also use Gametext colors in textdraws.
        /// </remarks>
        /// <param name="playerid">The ID of the player who's textdraw to set the color of.</param>
        /// <param name="text">The TextDraw to change.</param>
        /// <param name="color">The color in hexadecimal format.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawColor(int playerid, int text, int color);

        /// <summary>
        ///     Toggle the box on a player-textdraw.
        /// </summary>
        /// <param name="playerid">The ID of the player whose textdraw to toggle the box of.</param>
        /// <param name="text">The ID of the player-textdraw to toggle the box of.</param>
        /// <param name="use">True to use a box or False to not use a box.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawUseBox(int playerid, int text, bool use);

        /// <summary>
        ///     Adjusts the text box colour (only used if <see cref="TextDrawUseBox" /> 'use' parameter is True).
        /// </summary>
        /// <remarks>
        ///     <see cref="PlayerTextDrawUseBox" /> must be used in conjunction with this (duh).
        /// </remarks>
        /// <param name="playerid">The ID of the player who's textdraw to set the color of.</param>
        /// <param name="text">The TextDraw to change the box color of.</param>
        /// <param name="color">The color in hexadecimal format.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawBoxColor(int playerid, int text, int color);

        /// <summary>
        ///     Adds a shadow to the lower right side of the text in a player-textdraw. The shadow font matches the text font. The
        ///     shadow can be cut by the box area if the size is set too big for the area.
        /// </summary>
        /// <remarks>
        ///     <see cref="PlayerTextDrawUseBox" /> must be used in conjunction with this (duh).
        /// </remarks>
        /// <param name="playerid">The ID of the player whose player-textdraw to set the shadow of.</param>
        /// <param name="text">The ID of the player-textdraw to change the shadow of.</param>
        /// <param name="size">The size of the shadow.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetShadow(int playerid, int text, int size);

        /// <summary>
        ///     Set the outline of a player-textdraw. The outline colour cannot be changed unless
        ///     <see cref="PlayerTextDrawBackgroundColor" /> is used.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-textdraw to set the outline of.</param>
        /// <param name="text">The ID of the player-textdraw to set the outline of.</param>
        /// <param name="size">The thickness of the outline.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetOutline(int playerid, int text, int size);

        /// <summary>
        ///     Adjust the background color of a player-textdraw.
        /// </summary>
        /// <remarks>
        ///     If <see cref="PlayerTextDrawSetOutline" /> is used with size > 0, the outline color will match the color used in
        ///     <see cref="PlayerTextDrawBackgroundColor" />.
        ///     Changing the value of color seems to alter the color used in <see cref="PlayerTextDrawColor" />.
        /// </remarks>
        /// <param name="playerid">The ID of the player whose player-textdraw to set the background color of.</param>
        /// <param name="text">The ID of the player-textdraw to set the background color of.</param>
        /// <param name="color">The color that the textdraw should be set to.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawBackgroundColor(int playerid, int text, int color);

        /// <summary>
        ///     Change the font of a player-textdraw.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-textdraw to change the font of.</param>
        /// <param name="text">The ID of the player-textdraw to change the font of</param>
        /// <param name="font">
        ///     There are four font styles as shown below. A font value greater than 3 does not display, and
        ///     anything greater than 16 crashes.
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawFont(int playerid, int text, int font);

        /// <summary>
        ///     Appears to scale text spacing to a proportional ratio. Useful when using PlayerTextDrawLetterSize to ensure the
        ///     text has even character spacing.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-textdraw to set the proportionality of.</param>
        /// <param name="text">The ID of the player-textdraw to set the proportionality of.</param>
        /// <param name="set">True to enable proportionality, False to disable.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetProportional(int playerid, int text, bool set);

        /// <summary>
        ///     Toggles whether a player-textdraw can be selected or not.
        /// </summary>
        /// <remarks>
        ///     <see cref="PlayerTextDrawSetSelectable" /> MUST be used BEFORE the textdraw is shown to the player.
        /// </remarks>
        /// <param name="playerid">The ID of the player whose player-textdraw to make selectable.</param>
        /// <param name="text">The ID of the player-textdraw to set the selectability of.</param>
        /// <param name="set">et the player-textdraw selectable (True) or non-selectable (False). By default this is False.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetSelectable(int playerid, int text, bool set);

        /// <summary>
        ///     Show a player-textdraw to the player it was created for.
        /// </summary>
        /// <param name="playerid">The ID of the player to show the textdraw for.</param>
        /// <param name="text">The ID of the textdraw to show.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawShow(int playerid, int text);

        /// <summary>
        ///     Hide a player-textdraw from the player it was created for.
        /// </summary>
        /// <param name="playerid">The ID of the player to hide the textdraw for.</param>
        /// <param name="text">The ID of the textdraw to hide.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawHide(int playerid, int text);

        /// <summary>
        ///     Change the text of a player-textdraw.
        /// </summary>
        /// <remarks>
        ///     Although the textdraw string limit is 1024 characters, if colour codes (e.g. ~r~) are used beyond the 255th
        ///     character it may crash the client.
        /// </remarks>
        /// <param name="playerid">The ID of the player who's textdraw string to set.</param>
        /// <param name="text">The ID of the textdraw to change.</param>
        /// <param name="str">The new string for the TextDraw.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetString(int playerid, int text, string str);

        /// <summary>
        ///     Sets a player textdraw 2D preview sprite of a specified model ID.
        /// </summary>
        /// <param name="playerid">The PlayerTextDraw player ID.</param>
        /// <param name="text">The textdraw id that will display the 3D preview.</param>
        /// <param name="modelindex">The model ID to display.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetPreviewModel(int playerid, int text, int modelindex);

        /// <summary>
        ///     Sets the rotation and zoom of a 3D model preview player-textdraw.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-textdraw to change.</param>
        /// <param name="text">The ID of the player-textdraw to change.</param>
        /// <param name="rotX">The X rotation value.</param>
        /// <param name="rotY">The Y rotation value.</param>
        /// <param name="rotZ">The Z rotation value.</param>
        /// <param name="zoom">
        ///     The zoom value, default value 1.0, smaller values make the camera closer and larger values make the
        ///     camera further away.
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetPreviewRot(int playerid, int text, float rotX, float rotY,
            float rotZ, float zoom);

        /// <summary>
        ///     Set the color of a vehicle in a player-textdraw model preview (if a vehicle is shown).
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-textdraw to change.</param>
        /// <param name="text">The ID of the player's player-textdraw to change.</param>
        /// <param name="color1">The color to set the vehicle's primary color to.</param>
        /// <param name="color2">The color to set the vehicle's secondary color to.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetPreviewVehCol(int playerid, int text, int color1, int color2);

        /// <summary>
        ///     Sets an integer to a player variable.
        /// </summary>
        /// <remarks>
        ///     Variables aren't reset until after <see cref="BaseMode.OnPlayerDisconnect" /> is called, so the values are still
        ///     accessible in <see cref="BaseMode.OnPlayerDisconnect" />.
        /// </remarks>
        /// <param name="playerid">The ID of the player whose player variable will be set.</param>
        /// <param name="varname">The name of the player variable.</param>
        /// <param name="value">The integer to be set.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPVarInt(int playerid, string varname, int value);

        /// <summary>
        ///     Gets a player variable as an integer.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player variable to get.</param>
        /// <param name="varname">The name of the player variable. (case-insensitive)</param>
        /// <returns>The integer value from the specified player variable.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPVarInt(int playerid, string varname);

        /// <summary>
        ///     Saves a string into a player variable.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player variable will be set.</param>
        /// <param name="varname">The name of the player variable.</param>
        /// <param name="value">The string you want to save in the player variable.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPVarString(int playerid, string varname, string value);

        /// <summary>
        ///     Gets a player variable as a string.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player variable to get.</param>
        /// <param name="varname">The name of the player variable, set by <see cref="SetPVarString" />.</param>
        /// <param name="value">The array in which to store the string value in, passed by reference.</param>
        /// <param name="size">The maximum length of the returned string.</param>
        /// <returns>The length of the string.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPVarString(int playerid, string varname, out string value, int size);

        /// <summary>
        ///     Saves a float variable into a player variable.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player variable will be set.</param>
        /// <param name="varname">The name of the player variable.</param>
        /// <param name="value">The float you want to save in the player variable.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPVarFloat(int playerid, string varname, float value);

        /// <summary>
        ///     Gets a player variable as a float.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player variable you want to get.</param>
        /// <param name="varname">The name of the player variable.</param>
        /// <returns>The float from the specified player variable.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern float GetPVarFloat(int playerid, string varname);

        /// <summary>
        ///     Deletes a previously set player variable.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player variable to delete.</param>
        /// <param name="varname">The name of the player variable to delete.</param>
        /// <returns>True on success, False on failure (pVar not set or player not connected).</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DeletePVar(int playerid, string varname);

        /// <summary>
        ///     Each PVar (player-variable) has its own unique identification number for lookup, this function returns the highest
        ///     ID set for a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the upper PVar index of..</param>
        /// <returns>The highest set PVar ID.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPVarsUpperIndex(int playerid);

        /// <summary>
        ///     Retrieve the name of a player's variable via the index.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player variable to get the name of.</param>
        /// <param name="index">The index of the player's pVar.</param>
        /// <param name="varname">A string to store the pVar's name in, passed by reference.</param>
        /// <param name="size">The max length of the returned string</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPVarNameAtIndex(int playerid, int index, out string varname, int size);

        /// <summary>
        ///     Gets the type (integer, float or string) of a player variable.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player variable to get the type of.</param>
        /// <param name="varname">The name of the player variable to get the type of.</param>
        /// <returns>Returns the type of the PVar.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPVarType(int playerid, string varname);

        /// <summary>
        ///     Creates a chat bubble above a player's name tag.
        /// </summary>
        /// <param name="playerid">The player which should have the chat bubble.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="color">The text color.</param>
        /// <param name="drawdistance">The distance from where players are able to see the chat bubble.</param>
        /// <param name="expiretime">The time in miliseconds the bubble should be displayed for.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerChatBubble(int playerid, string text, int color, float drawdistance,
            int expiretime);

        /// <summary>
        ///     Puts a player in a vehicle
        /// </summary>
        /// <param name="playerid">The ID of the player to put in a vehicle.</param>
        /// <param name="vehicleid">The ID of the vehicle for the player to be put in.</param>
        /// <param name="seatid">The ID of the seat to put the player in.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PutPlayerInVehicle(int playerid, int vehicleid, int seatid);

        /// <summary>
        ///     This function gets the ID of the vehicle the player is currently in. Note: NOT the model id of the vehicle. See
        ///     <see cref="GetVehicleModel" /> for that.
        /// </summary>
        /// <param name="playerid">The ID of the player in the vehicle that you want to get the ID of.</param>
        /// <returns>ID of the vehicle or 0 if not in a vehicle.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerVehicleID(int playerid);

        /// <summary>
        ///     Find out what seat a player is in.
        /// </summary>
        /// <remarks>
        ///     Sometimes the result can be 128 which is an invalid seat ID. Circumstances of this are not yet known, but it is
        ///     best to discard information when returned seat number is 128.
        /// </remarks>
        /// <param name="playerid">The ID of the player you want to get the seat of.</param>
        /// <returns>
        ///     Seat ID (-1 if the player is not in a vehicle, 0: driver, 1: co-driver, 2,3: back seat passengers, 4,...: the
        ///     more seats (i.e coach)).
        /// </returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerVehicleSeat(int playerid);

        /// <summary>
        ///     Removes/ejects a player from their vehicle.
        /// </summary>
        /// <remarks>
        ///     The exiting animation is not synced for other players.
        ///     This function will not work when used in <see cref="BaseMode.OnPlayerEnterVehicle(int,int,bool)" />, because the player isn't in
        ///     the vehicle when the callback is called. Use <see cref="BaseMode.OnPlayerStateChange" /> instead.
        /// </remarks>
        /// <param name="playerid">The ID of the player to remove from their vehicle.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemovePlayerFromVehicle(int playerid);

        /// <summary>
        ///     Toggles whether a player can control themselves, basically freezes them.
        /// </summary>
        /// <param name="playerid">The ID of the player to toggle the controllability of.</param>
        /// <param name="toggle">False to freeze the player or True to unfreeze them.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TogglePlayerControllable(int playerid, bool toggle);

        /// <summary>
        ///     Plays the specified sound for a player.
        /// </summary>
        /// <remarks>
        ///     Only use the coordinates if you want the sound to be played at a certain position. Set coordinates all to 0 to just
        ///     play the sound.
        /// </remarks>
        /// <param name="playerid">The ID of the player for whom to play the sound.</param>
        /// <param name="soundid">The sound to play.</param>
        /// <param name="x">X coordinate for the sound to play at. (0 for no position)</param>
        /// <param name="y">Y coordinate for the sound to play at. (0 for no position)</param>
        /// <param name="z">Z coordinate for the sound to play at. (0 for no position)</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerPlaySound(int playerid, int soundid, float x, float y, float z);

        /// <summary>
        ///     Apply an animation to a player.
        /// </summary>
        /// <remarks>
        ///     The <paramref name="forcesync" /> parameter, which defaults to False, in most cases is not needed since players
        ///     sync animations themselves. The <paramref name="forcesync" /> parameter can force all players who can see
        ///     <paramref name="playerid" /> to play the animation regardless of whether the player is performing that animation.
        ///     This is useful in circumstances where the player can't sync the animation themselves. For example, they may be
        ///     paused.
        /// </remarks>
        /// <param name="playerid">The ID of the player to apply the animation to.</param>
        /// <param name="animlib">The name of the animation library in which the animation to apply is in.</param>
        /// <param name="animname">The name of the animation, within the library specified.</param>
        /// <param name="fDelta">The speed to play the animation (use 4.1).</param>
        /// <param name="loop">Set to True for looping otherwise set to False for playing animation sequence only once.</param>
        /// <param name="lockx">
        ///     Set to False to return player to original x position after animation is complete for moving
        ///     animations. The opposite effect occurs if set to True.
        /// </param>
        /// <param name="locky">
        ///     Set to False to return player to original y position after animation is complete for moving
        ///     animations. The opposite effect occurs if set to True.
        /// </param>
        /// <param name="freeze">Will freeze the player in position after the animation finishes.</param>
        /// <param name="time">Timer in milliseconds. For a never ending loop it should be 0.</param>
        /// <param name="forcesync">Set to True to force playerid to sync animation with other players in all instances</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ApplyAnimation(int playerid, string animlib, string animname, float fDelta, bool loop,
            bool lockx, bool locky, bool freeze, int time, bool forcesync);

        /// <summary>
        ///     Clears all animations for the given player.
        /// </summary>
        /// <param name="playerid">The ID of the player to clear the animations of.</param>
        /// <param name="forcesync">Specifies whether the animation should be shown to streamed in players.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ClearAnimations(int playerid, bool forcesync);

        /// <summary>
        ///     Returns the index of any running applied animations.
        /// </summary>
        /// <param name="playerid">ID of the player of whom you want to get the animation index of.</param>
        /// <returns>0 if there is no animation applied, otherwise the index of the playing animation.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerAnimationIndex(int playerid);

        /// <summary>
        ///     Get the animation library/name for the index.
        /// </summary>
        /// <param name="index">The animation index, returned by <see cref="GetPlayerAnimationIndex" />.</param>
        /// <param name="animlib">String variable that stores the animation library.</param>
        /// <param name="animlibSize">Length of the string that stores the animation library.</param>
        /// <param name="animname">String variable that stores the animation name.</param>
        /// <param name="animnameSize">Length of the string that stores the animation name.</param>
        /// <returns>True on success, False otherwise.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetAnimationName(int index, out string animlib, int animlibSize, out string animname,
            int animnameSize);

        /// <summary>
        ///     Retrieves a player's current special action.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the special action of.</param>
        /// <returns>The special action of the player.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerSpecialAction(int playerid);

        /// <summary>
        ///     This Function allows to set players special action.
        /// </summary>
        /// <param name="playerid">The player that should perform the action.</param>
        /// <param name="actionid">The action that should be performed.</param>
        /// ob
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerSpecialAction(int playerid, int actionid);

        /// <summary>
        ///     Sets a checkpoint (red circle) for a player. Also shows a red blip on the radar.
        /// </summary>
        /// <remarks>
        ///     Checkpoints created on server-created objects (<see cref="CreateObject(int,Vector,Vector,float)" />/
        ///     <see cref="CreatePlayerObject(int,int,Vector,Vector,float)" />) will
        ///     appear down on the 'real' ground, but will still function correctly. There is no fix available for this issue. A
        ///     pickup can be used instead.
        /// </remarks>
        /// <param name="playerid">The ID of the player to set the checkpoint of.</param>
        /// <param name="x">The X coordinate to set the checkpoint at.</param>
        /// <param name="y">The Y coordinate to set the checkpoint at.</param>
        /// <param name="z">The Z coordinate to set the checkpoint at.</param>
        /// <param name="size">The size of the checkpoint.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerCheckpoint(int playerid, float x, float y, float z, float size);

        /// <summary>
        ///     Disable any initialized checkpoints for a specific player, since you can only have one at any given time.
        /// </summary>
        /// <param name="playerid">The player to disable the current checkpoint for.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisablePlayerCheckpoint(int playerid);

        /// <summary>
        ///     Creates a race checkpoint. When the player enters it, the OnPlayerEnterRaceCheckpoint callback is called.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the checkpoint for.</param>
        /// <param name="type">
        ///     Type of checkpoint.0-Normal, 1-Finish, 2-Nothing(Only the checkpoint without anything on it), 3-Air
        ///     normal, 4-Air finish.
        /// </param>
        /// <param name="x">X-Coordinate.</param>
        /// <param name="y">X-Coordinate.</param>
        /// <param name="z">X-Coordinate.</param>
        /// <param name="nextx">X-Coordinate of the next point, for the arrow facing direction.</param>
        /// <param name="nexty">X-Coordinate of the next point, for the arrow facing direction.</param>
        /// <param name="nextz">X-Coordinate of the next point, for the arrow facing direction.</param>
        /// <param name="size">Length (diameter) of the checkpoint</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerRaceCheckpoint(int playerid, int type, float x, float y, float z, float nextx,
            float nexty, float nextz, float size);

        /// <summary>
        ///     Disable any initialized race checkpoints for a specific player, since you can only have one at any given time.
        /// </summary>
        /// <param name="playerid">The player to disable the current checkpoint for.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisablePlayerRaceCheckpoint(int playerid);

        /// <summary>
        ///     Set the world boundaries for a player - players can not go out of the boundaries.
        /// </summary>
        /// <remarks>
        ///     You can reset the player world bounds by setting the parameters to 20000.0000, -20000.0000, 20000.0000,
        ///     -20000.0000.
        /// </remarks>
        /// <param name="playerid">The ID of the player to set the boundaries of.</param>
        /// <param name="xMax">The maximum X coordinate the player can go to.</param>
        /// <param name="xMin">The minimum X coordinate the player can go to.</param>
        /// <param name="yMax">The maximum Y coordinate the player can go to.</param>
        /// <param name="yMin">The minimum Y coordinate the player can go to.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerWorldBounds(int playerid, float xMax, float xMin, float yMax, float yMin);

        /// <summary>
        ///     Change the colour of a player's nametag and radar blip for another player.
        /// </summary>
        /// <param name="playerid">The player that will see the player's changed blip/nametag color.</param>
        /// <param name="showplayerid">The player whose color will be changed.</param>
        /// <param name="color">New color. Set to 0 for an invisible blip.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerMarkerForPlayer(int playerid, int showplayerid, int color);

        /// <summary>
        ///     This functions allows you to toggle the drawing of player nametags, healthbars and armor bars which display above
        ///     their head. For use of a similar function like this on a global level, <see cref="ShowNameTags" /> function.
        /// </summary>
        /// <remarks>
        ///     <see cref="ShowNameTags" /> must be set to True to be able to show name tags with
        ///     <see cref="ShowPlayerNameTagForPlayer" />.
        /// </remarks>
        /// <param name="playerid">Player who will see the results of this function.</param>
        /// <param name="showplayerid">Player whose name tag will be shown or hidden.</param>
        /// <param name="show">True to show name tag, False to hide name tag.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowPlayerNameTagForPlayer(int playerid, int showplayerid, bool show);

        /// <summary>
        ///     This function allows you to place your own icons on the map, enabling you to emphasise the locations of banks,
        ///     airports or whatever else you want. A total of 63 icons are available in GTA: San Andreas, all of which can be used
        ///     using this function. You can also specify the color of the icon, which allows you to change the square icon (ID:
        ///     0).
        /// </summary>
        /// <param name="playerid">The ID of the player to set the map icon for.</param>
        /// <param name="iconid">The player's icon ID, ranging from 0 to 99, to be used in RemovePlayerMapIcon.</param>
        /// <param name="x">The X coordinate of the place where you want the icon to be.</param>
        /// <param name="y">The Y coordinate of the place where you want the icon to be.</param>
        /// <param name="z">The Z coordinate of the place where you want the icon to be.</param>
        /// <param name="markertype">The icon to set.</param>
        /// <param name="color">The color of the icon, this should only be used with the square icon (ID: 0).</param>
        /// <param name="style">The style of icon.</param>
        /// <returns>True if it was successful, False otherwise (e.g. the player isn't connected).</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerMapIcon(int playerid, int iconid, float x, float y, float z, int markertype,
            int color, int style);

        /// <summary>
        ///     Removes a map icon that was set earlier for a player.
        /// </summary>
        /// <param name="playerid">The ID of the player whose icon to remove.</param>
        /// <param name="iconid">
        ///     The ID of the icon to remove. This is the second parameter of
        ///     <see cref="SetPlayerMapIcon(int,int,Vector,PlayerMarkersMode,int,int)" />.
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemovePlayerMapIcon(int playerid, int iconid);

        /// <summary>
        ///     Enable/Disable the teleporting ability for a player by right-clicking on the map.
        /// </summary>
        /// <remarks>
        ///     This function will work only if <see cref="AllowAdminTeleport" /> is working, and you have to be an admin.
        /// </remarks>
        /// <param name="playerid">playerid</param>
        /// <param name="allow">True-allow, False-disallow</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        [Obsolete("This function is deprecated. Use the OnPlayerClickMap callback instead.")]
        public static extern bool AllowPlayerTeleport(int playerid, bool allow);

        /// <summary>
        ///     Sets the camera to a specific position for a player.
        /// </summary>
        /// <param name="playerid">ID of the player.</param>
        /// <param name="x">New x-position of the camera.</param>
        /// <param name="y">New y-position of the camera.</param>
        /// <param name="z">New z-position of the camera.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerCameraPos(int playerid, float x, float y, float z);

        /// <summary>
        ///     Set the direction a player's camera looks at. To be used in combination with SetPlayerCameraPos.
        /// </summary>
        /// <param name="playerid">The player to change the camera of.</param>
        /// <param name="x">The X coordinate for the player's camera to look at.</param>
        /// <param name="y">The Y coordinate for the player's camera to look at.</param>
        /// <param name="z">The Z coordinate for the player's camera to look at.</param>
        /// <param name="cut">The style the camera-position changes.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerCameraLookAt(int playerid, float x, float y, float z, int cut);

        /// <summary>
        ///     Restore the camera to a place behind the player, after using a function like SetPlayerCameraPos.
        /// </summary>
        /// <param name="playerid">The player you want to restore the camera for.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetCameraBehindPlayer(int playerid);

        /// <summary>
        ///     Get the position of the player's camera.
        /// </summary>
        /// <remarks>
        ///     Player's camera positions are only updated once a second, unless aiming.
        /// </remarks>
        /// <param name="playerid">The ID of the player to get the camera position of.</param>
        /// <param name="x">A float variable to store the X coordinate in, passed by reference.</param>
        /// <param name="y">A float variable to store the Y coordinate in, passed by reference.</param>
        /// <param name="z">A float variable to store the Z coordinate in, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerCameraPos(int playerid, out float x, out float y, out float z);

        /// <summary>
        ///     This function will return the current direction of player's aiming in 3-D space, the coords are relative to the
        ///     camera position, see <see cref="GetPlayerCameraPos(int)" />.
        /// </summary>
        /// <param name="playerid">The ID of the player you want to obtain the camera front vector of.</param>
        /// <param name="x">A float to store the X coordinate, passed by reference.</param>
        /// <param name="y">A float to store the Y coordinate, passed by reference.</param>
        /// <param name="z">A float to store the Z coordinate, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerCameraFrontVector(int playerid, out float x, out float y, out float z);

        /// <summary>
        ///     Returns the current GTA camera mode for the requested player. The camera modes are useful in determining whether a
        ///     player is aiming, doing a passenger driveby etc
        /// </summary>
        /// <param name="playerid">The ID of the player whose camera mode to retrieve</param>
        /// <returns>The camera mode as an integer (or -1 if player is not connected)</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerCameraMode(int playerid);

        /// <summary>
        ///     You can use this function to attach the player camera to objects.
        /// </summary>
        /// <remarks>
        ///     You need to create the object first, before attempting to attach a player camera for that.
        /// </remarks>
        /// <param name="playerid">The ID of the player which will have your camera attached on object.</param>
        /// <param name="objectid">The object id which you want to attach the player camera.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachCameraToObject(int playerid, int objectid);

        /// <summary>
        ///     Attaches a player's camera to a player-object. They are able to move their camera while it is attached to an
        ///     object. Can be used with <see cref="MovePlayerObject(int,int,Vector,float,Vector)" /> and
        ///     <see cref="AttachPlayerObjectToVehicle(int,int,int,Vector,Vector)" />.
        /// </summary>
        /// <param name="playerid">The ID of the player which will have their camera attached to a player-object.</param>
        /// <param name="playerobjectid">	The ID of the player-object to which the player's camera will be attached.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachCameraToPlayerObject(int playerid, int playerobjectid);

        /// <summary>
        ///     Move a player's camera from one position to another, within the set time.
        /// </summary>
        /// <param name="playerid">The ID of the player the camera should be moved for.</param>
        /// <param name="fromX">The X position the camera should start to move from.</param>
        /// <param name="fromY">The Y position the camera should start to move from.</param>
        /// <param name="fromZ">The Z position the camera should start to move from.</param>
        /// <param name="toX">The X position the camera should move to.</param>
        /// <param name="toY">The Y position the camera should move to.</param>
        /// <param name="toZ">The Z position the camera should move to.</param>
        /// <param name="time">Time in milliseconds.</param>
        /// <param name="cut">The jumpcut to use. Defaults to CameraCut.Cut. Set to CameraCut.Move for a smooth movement.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool InterpolateCameraPos(int playerid, float fromX, float fromY, float fromZ, float toX,
            float toY, float toZ, int time, int cut);

        /// <summary>
        ///     Interpolate a player's camera's 'look at' point between two coordinates with a set speed. Can be be used with
        ///     <see cref="InterpolateCameraPos(int,Vector,Vector,int,CameraCut)" />.
        /// </summary>
        /// <param name="playerid">The ID of the player the camera should be moved for.</param>
        /// <param name="fromX">The X position the camera should start to move from.</param>
        /// <param name="fromY">The Y position the camera should start to move from.</param>
        /// <param name="fromZ">The Z position the camera should start to move from.</param>
        /// <param name="toX">The X position the camera should move to.</param>
        /// <param name="toY">The Y position the camera should move to.</param>
        /// <param name="toZ">The Z position the camera should move to.</param>
        /// <param name="time">Time in milliseconds to complete interpolation.</param>
        /// <param name="cut">The 'jumpcut' to use. Defaults to CameraCut.Cut (pointless). Set to CameraCut.Move for interpolation.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool InterpolateCameraLookAt(int playerid, float fromX, float fromY, float fromZ, float toX,
            float toY, float toZ, int time, int cut);

        /// <summary>
        ///     This function can be used to check if a player is connected to the server via SA:MP.
        /// </summary>
        /// <remarks>
        ///     This function can be omitted in a lot of cases. Many other natives already have some sort of connection check
        ///     built in.
        /// </remarks>
        /// <param name="playerid">The playerid you would like to check.</param>
        /// <returns>Returns true if the player is connected and false if the player is not.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerConnected(int playerid);

        /// <summary>
        ///     Checks if a player is in a specific vehicle.
        /// </summary>
        /// <param name="playerid">ID of the player.</param>
        /// <param name="vehicleid">ID of the vehicle.</param>
        /// <returns>True if player is in the vehicle, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInVehicle(int playerid, int vehicleid);

        /// <summary>
        ///     Check if a player is inside any vehicle.
        /// </summary>
        /// <param name="playerid">The ID of the player to check.</param>
        /// <returns>True if player is in a vehicle, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInAnyVehicle(int playerid);

        /// <summary>
        ///     Check if the player is currently inside a checkpoint, this could be used for properties or teleport points for
        ///     example.
        /// </summary>
        /// <param name="playerid">The player you want to know the status of.</param>
        /// <returns>True if player is in his checkpoint, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInCheckpoint(int playerid);

        /// <summary>
        ///     Check if the player is inside their current set race checkpoint (
        ///     <see cref="SetPlayerRaceCheckpoint(int,CheckpointType,Vector,Vector,float)" />).
        /// </summary>
        /// <param name="playerid">The ID of the player to check.</param>
        /// <returns>True if player is in his checkpoint, otherwise False.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInRaceCheckpoint(int playerid);

        /// <summary>
        ///     Set the virtual world of a player. They can only see other players or vehicles if they are in that same world.
        /// </summary>
        /// <remarks>
        ///     The default virtual world is 0.
        /// </remarks>
        /// <param name="playerid">The ID of the player you want to set the virtual world of.</param>
        /// <param name="worldid">The virtual world ID to put the player in.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerVirtualWorld(int playerid, int worldid);

        /// <summary>
        ///     Retrieves the current virtual world the player is in. Note this is not the same as the interior.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the virtual world of.</param>
        /// <returns>The ID of the world the player is currently in.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerVirtualWorld(int playerid);

        /// <summary>
        ///     Toggle stunt bonuses for a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to toggle stunt bonuses for.</param>
        /// <param name="enable">True to enable stunt bonuses, False to disable them.</param>
        /// <returns>This function doesn't return a specific value</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableStuntBonusForPlayer(int playerid, bool enable);

        /// <summary>
        ///     Enables or disables stunt bonuses for all players.
        /// </summary>
        /// <param name="enable">True to enable stunt bonuses, False to disable.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableStuntBonusForAll(bool enable);

        /// <summary>
        ///     Toggle a player's spectate mode.
        /// </summary>
        /// <remarks>
        ///     When the spectating is turned off, OnPlayerSpawn will automatically be called.
        /// </remarks>
        /// <param name="playerid">The ID of the player who should spectate.</param>
        /// <param name="toggle">True to enable spectating and False to disable.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TogglePlayerSpectating(int playerid, bool toggle);

        /// <summary>
        ///     Makes a player spectate (watch) another player.
        /// </summary>
        /// <remarks>
        ///     Order is CRITICAL! Ensure that you use <see cref="TogglePlayerSpectating" /> before
        ///     <see cref="PlayerSpectatePlayer" />.
        /// </remarks>
        /// <param name="playerid">The ID of the player that will spectate.</param>
        /// <param name="targetplayerid">The ID of the player that should be spectated.</param>
        /// <param name="mode">The mode to spectate with.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerSpectatePlayer(int playerid, int targetplayerid, int mode);

        /// <summary>
        ///     Sets a player to spectate another vehicle, i.e. see what its driver sees.
        /// </summary>
        /// <remarks>
        ///     Order is CRITICAL! Ensure that you use <see cref="TogglePlayerSpectating" /> before
        ///     <see cref="PlayerSpectatePlayer" />.
        /// </remarks>
        /// <param name="playerid">Player ID.</param>
        /// <param name="targetvehicleid">ID of the vehicle to spectate.</param>
        /// <param name="mode">Spectate mode.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerSpectateVehicle(int playerid, int targetvehicleid, int mode);

        /// <summary>
        ///     Starts recording the player's movements to a file, which can then be reproduced by an NPC.
        /// </summary>
        /// <param name="playerid">The ID of the player you want to record.</param>
        /// <param name="recordtype">The type of recording.</param>
        /// <param name="recordname">
        ///     Name of the file which will hold the recorded data. It will be saved in scriptfiles, with an
        ///     automatically added .rec extension.
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StartRecordingPlayerData(int playerid, int recordtype, string recordname);

        /// <summary>
        ///     Stops all the recordings that had been started with <see cref="StartRecordingPlayerData" /> for a specific player.
        /// </summary>
        /// <param name="playerid">The player you want to stop the recordings of.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StopRecordingPlayerData(int playerid);

        /// <summary>
        ///     Creates an explosion for a player.
        ///     Only the specific player will see explosion and feel its effects.
        ///     This is useful when you want to isolate explosions from other players or to make them only appear in specific
        ///     virtual worlds.
        /// </summary>
        /// <param name="playerid">The ID of the player to create the explosion for.</param>
        /// <param name="x">The X coordinate of the explosion.</param>
        /// <param name="y">The Y coordinate of the explosion.</param>
        /// <param name="z">The Z coordinate of the explosion.</param>
        /// <param name="type">The explosion type.</param>
        /// <param name="radius">The radius of the explosion.</param>
        /// <returns>This function does not return any specific values.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CreateExplosionForPlayer(int playerid, float x, float y, float z, int type,
            float radius);

        /// <summary>
        ///     This function can be used to change the spawn information of a specific player. It allows you to automatically set
        ///     someone's spawn weapons, their team, skin and spawn position, normally used in case of minigames or automatic-spawn
        ///     systems. This function is more crash-safe then using <see cref="SetPlayerSkin" /> in
        ///     <see cref="BaseMode.OnPlayerSpawn" /> and/or <see cref="BaseMode.OnPlayerRequestClass(int,int)" />.
        /// </summary>
        /// <param name="playerid">The PlayerID of who you want to set the spawn information.</param>
        /// <param name="team">The Team-ID of the chosen player.</param>
        /// <param name="skin">The skin which the player will spawn with.</param>
        /// <param name="x">The X-coordinate of the player's spawn position.</param>
        /// <param name="y">The Y-coordinate of the player's spawn position.</param>
        /// <param name="z">The Z-coordinate of the player's spawn position.</param>
        /// <param name="rotation">The direction in which the player needs to be facing after spawning.</param>
        /// <param name="weapon1">The first spawn-weapon for the player.</param>
        /// <param name="weapon1Ammo">The amount of ammunition for the primary spawnweapon.</param>
        /// <param name="weapon2">The second spawn-weapon for the player.</param>
        /// <param name="weapon2Ammo">The amount of ammunition for the second spawnweapon.</param>
        /// <param name="weapon3">The third spawn-weapon for the player.</param>
        /// <param name="weapon3Ammo">The amount of ammunition for the third spawnweapon.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetSpawnInfo(int playerid, int team, int skin, float x, float y, float z,
            float rotation, Weapon weapon1, int weapon1Ammo, Weapon weapon2, int weapon2Ammo, Weapon weapon3,
            int weapon3Ammo)
        {
            return SetSpawnInfo(playerid, team, skin, x, y, z, rotation, (int) weapon1, weapon1Ammo, (int) weapon2,
                weapon2Ammo, (int) weapon3, weapon3Ammo);
        }

        /// <summary>
        ///     This function can be used to change the spawn information of a specific player. It allows you to automatically set
        ///     someone's spawn weapons, their team, skin and spawn position, normally used in case of minigames or automatic-spawn
        ///     systems. This function is more crash-safe then using <see cref="SetPlayerSkin" /> in
        ///     <see cref="BaseMode.OnPlayerSpawn" /> and/or <see cref="BaseMode.OnPlayerRequestClass(int,int)" />.
        /// </summary>
        /// <param name="playerid">The PlayerID of who you want to set the spawn information.</param>
        /// <param name="team">The Team-ID of the chosen player.</param>
        /// <param name="skin">The skin which the player will spawn with.</param>
        /// <param name="position">The player's spawn position.</param>
        /// <param name="rotation">The direction in which the player needs to be facing after spawning.</param>
        /// <param name="weapon1">The first spawn-weapon for the player.</param>
        /// <param name="weapon1Ammo">The amount of ammunition for the primary spawnweapon.</param>
        /// <param name="weapon2">The second spawn-weapon for the player.</param>
        /// <param name="weapon2Ammo">The amount of ammunition for the second spawnweapon.</param>
        /// <param name="weapon3">The third spawn-weapon for the player.</param>
        /// <param name="weapon3Ammo">The amount of ammunition for the third spawnweapon.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetSpawnInfo(int playerid, int team, int skin, Vector position,
            float rotation, Weapon weapon1, int weapon1Ammo, Weapon weapon2, int weapon2Ammo, Weapon weapon3,
            int weapon3Ammo)
        {
            return SetSpawnInfo(playerid, team, skin, position.X, position.Y, position.Z, rotation, (int) weapon1,
                weapon1Ammo, (int) weapon2,
                weapon2Ammo, (int) weapon3, weapon3Ammo);
        }

        /// <summary>
        ///     Set a player's position.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the position of.</param>
        /// <param name="position">The position to move the player to.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetPlayerPos(int playerid, Vector position)
        {
            return SetPlayerPos(playerid, position.X, position.Y, position.Z);
        }

        /// <summary>
        ///     This sets the players position then adjusts the players z-coordinate to the nearest solid ground under the
        ///     position.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the position of.</param>
        /// <param name="position">The position to move the player to.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetPlayerPosFindZ(int playerid, Vector position)
        {
            return SetPlayerPosFindZ(playerid, position.X, position.Y, position.Z);
        }

        /// <summary>
        ///     Get the coordinates of a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the position of.</param>
        /// <returns>The position of the player.</returns>
        public static Vector GetPlayerPos(int playerid)
        {
            float x, y, z;
            GetPlayerPos(playerid, out x, out y, out z);
            return new Vector(x, y, z);
        }

        /// <summary>
        ///     Check if a player is in range of a point.
        /// </summary>
        /// <param name="playerid">The ID of the player.</param>
        /// <param name="range">The furthest distance the player can be from the point to be in range.</param>
        /// <param name="point">The point to check the range to.</param>
        /// <returns>True if the player is in range of the point, otherwise False.</returns>
        public static bool IsPlayerInRangeOfPoint(int playerid, float range, Vector point)
        {
            return IsPlayerInRangeOfPoint(playerid, range, point.X, point.Y, point.Z);
        }

        /// <summary>
        ///     Return angle of the direction the player is facing.
        /// </summary>
        /// <param name="playerid">The player you want to get the angle of.</param>
        /// <returns>The angle of the player.</returns>
        public static float GetPlayerFacingAngle(int playerid)
        {
            float angle;
            GetPlayerFacingAngle(playerid, out angle);
            return angle;
        }

        /// <summary>
        ///     Calculate the distance between a player and a map coordinate.
        /// </summary>
        /// <param name="playerid">The ID of the player to calculate the distance from.</param>
        /// <param name="point">The point to check the distance from.</param>
        /// <returns>The distance between the player and the point as a float.</returns>
        public static float GetPlayerDistanceFromPoint(int playerid, Vector point)
        {
            return GetPlayerDistanceFromPoint(playerid, point.X, point.Y, point.Z);
        }

        /// <summary>
        ///     The function GetPlayerHealth allows you to retrieve the health of a player. Useful for cheat detection, among other
        ///     things.
        /// </summary>
        /// <param name="playerid">The ID of the player.</param>
        /// <returns>The health of the player.</returns>
        public static float GetPlayerHealth(int playerid)
        {
            float health;
            GetPlayerHealth(playerid, out health);
            return health;
        }

        /// <summary>
        ///     This function stores the armour of a player into a variable.
        /// </summary>
        /// <param name="playerid">The ID of the player that you want to get the armour of.</param>
        /// <returns>The amount of armour the player has.</returns>
        public static float GetPlayerArmour(int playerid)
        {
            float armour;
            GetPlayerArmour(playerid, out armour);
            return armour;
        }

        /// <summary>
        ///     Give a player a weapon with a specified amount of ammo.
        /// </summary>
        /// <param name="playerid">The ID of the player to give a weapon to.</param>
        /// <param name="weapon">The weapon to give to the player.</param>
        /// <param name="ammo">The amount of ammo to give to the player.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool GivePlayerWeapon(int playerid, Weapon weapon, int ammo)
        {
            return GivePlayerWeapon(playerid, (int) weapon, ammo);
        }

        /// <summary>
        ///     Get the specified player's IP and store it in a string.
        /// </summary>
        /// <remarks>
        ///     This function does not work when used in <see cref="BaseMode.OnPlayerDisconnect" /> because the player is already
        ///     disconnected. It will return an invalid IP (255.255.255.255). Save players' IPs under
        ///     <see cref="BaseMode.OnPlayerConnect" /> if they need to be used under <see cref="BaseMode.OnPlayerConnect" />.
        /// </remarks>
        /// <param name="playerid">The ID of the player to get the IP of.</param>
        /// <returns>The player's IP.</returns>
        public static string GetPlayerIp(int playerid)
        {
            string ip;
            GetPlayerIp(playerid, out ip, 16);
            return ip;
        }

        /// <summary>
        ///     Get a player's name.
        /// </summary>
        /// <remarks>
        ///     A player's name can be up to 24 characters long.
        ///     This is defined as <see cref="Limits.MaxPlayerName" />.
        ///     Strings to store names in should be made this size, plus one extra cell for the null terminating character.
        /// </remarks>
        /// <param name="playerid">The ID of the player to get the name of.</param>
        /// <returns>The name of the player.</returns>
        public static string GetPlayerName(int playerid)
        {
            string name;
            GetPlayerName(playerid, out name, Limits.MaxPlayerName);
            return name;
        }

        /// <summary>
        ///     Gets a player variable as a string.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player variable to get.</param>
        /// <param name="varname">The name of the player variable, set by <see cref="SetPVarString" />.</param>
        /// <returns>The string from the player variable.</returns>
        public static string GetPVarString(int playerid, string varname)
        {
            string value;
            GetPVarString(playerid, varname, out value, 64);
            return value;
        }

        /// <summary>
        ///     Retrieve the name of a player's variable via the index.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player variable to get the name of.</param>
        /// <param name="index">The index of the player's pVar.</param>
        /// <returns>This name of the player's variable.</returns>
        public static string GetPVarNameAtIndex(int playerid, int index)
        {
            string varname;
            GetPVarNameAtIndex(playerid, index, out varname, 64);
            return varname;
        }

        /// <summary>
        ///     Sets the armed weapon of the player.
        /// </summary>
        /// <param name="playerid">The ID of the player to arm with a weapon.</param>
        /// <param name="weapon">The weapon that the player should be armed with.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetPlayerArmedWeapon(int playerid, Weapon weapon)
        {
            return SetPlayerArmedWeapon(playerid, (int) weapon);
        }

        /// <summary>
        ///     Get the weapon and ammo in a specific player's weapon slot.
        /// </summary>
        /// <param name="playerid">The ID of the player whose weapon data to retrieve.</param>
        /// <param name="slot">The weapon slot to get data for (0-12).</param>
        /// <param name="weapon">The variable in which to store the weapon, passed by reference.</param>
        /// <param name="ammo">The variable in which to store the ammo, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool GetPlayerWeaponData(int playerid, int slot, out Weapon weapon, out int ammo)
        {
            int weaponid;
            bool result = GetPlayerWeaponData(playerid, slot, out weaponid, out ammo);
            weapon = (Weapon) weaponid;
            return result;
        }

        /// <summary>
        ///     Check which keys a player is pressing.
        /// </summary>
        /// <remarks>
        ///     Only the FUNCTION of keys can be detected; not actual keys. You can not detect if a player presses space, but you
        ///     can detect if they press sprint (which can be mapped (assigned) to ANY key, but is space by default)).
        /// </remarks>
        /// <param name="playerid">The ID of the player to detect the keys of.</param>
        /// <param name="keys">A set containing the player's key states</param>
        /// <param name="updown">Up or Down value, passed by reference.</param>
        /// <param name="leftright">Left or Right value, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool GetPlayerKeys(int playerid, out Keys keys, out int updown, out int leftright)
        {
            int outkeys;
            bool response = GetPlayerKeys(playerid, out outkeys, out updown, out leftright);
            keys = (Keys) outkeys;
            return response;
        }

        /// <summary>
        ///     Set a player's special fighting style. To use in-game, aim and press the 'secondary attack' key (ENTER by default).
        /// </summary>
        /// <remarks>
        ///     This does not affect normal fist attacks - only special/secondary attacks (aim + press 'secondary attack' key).
        /// </remarks>
        /// <param name="playerid">The ID of player to set the fighting style of.</param>
        /// <param name="style">The fighting style that should be set.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetPlayerFightingStyle(int playerid, FightStyle style)
        {
            return SetPlayerFightingStyle(playerid, (int) style);
        }

        /// <summary>
        ///     Attach an object to a specific bone on a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to attach the object to.</param>
        /// <param name="index">The index (slot) to assign the object to (0-9).</param>
        /// <param name="modelid">The model to attach.</param>
        /// <param name="bone">The bone to attach the object to.</param>
        /// <param name="offset">offset for the object position.</param>
        /// <param name="rotation">rotation of the object.</param>
        /// <param name="scale"> scale of the object.</param>
        /// <param name="materialcolor1">The first object color to set.</param>
        /// <param name="materialcolor2">The second object color to set.</param>
        /// <returns>True on success, False otherwise.</returns>
        public static bool SetPlayerAttachedObject(int playerid, int index, int modelid, int bone, Vector offset,
            Vector rotation, Vector scale, Color materialcolor1, Color materialcolor2)
        {
            return SetPlayerAttachedObject(playerid, index, modelid, bone, offset.X, offset.Y, offset.Z, rotation.X,
                rotation.Y, rotation.Z, scale.X, scale.Y, scale.Z, materialcolor1.GetColorValue(ColorFormat.ARGB),
                materialcolor2.GetColorValue(ColorFormat.ARGB));
        }


        /// <summary>
        ///     Set the text alignment of a player-textdraw.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-textdraw to set the alignment of.</param>
        /// <param name="text">The ID of the player-textdraw to set the alignment of.</param>
        /// <param name="alignment">Alignment of the player-textdraw.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool PlayerTextDrawAlignment(int playerid, int text, TextDrawAlignment alignment)
        {
            return PlayerTextDrawAlignment(playerid, text, (int) alignment);
        }

        /// <summary>
        ///     Change the font of a player-textdraw.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-textdraw to change the font of.</param>
        /// <param name="text">The ID of the player-textdraw to change the font of</param>
        /// <param name="font">The font to use in this player-textdraw.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool PlayerTextDrawFont(int playerid, int text, TextDrawFont font)
        {
            return PlayerTextDrawFont(playerid, text, (int) font);
        }

        /// <summary>
        ///     Sets the rotation and zoom of a 3D model preview player-textdraw.
        /// </summary>
        /// <param name="playerid">The ID of the player whose player-textdraw to change.</param>
        /// <param name="text">The ID of the player-textdraw to change.</param>
        /// <param name="rotation">The rotation value.</param>
        /// <param name="fZoom">
        ///     The zoom value, default value 1.0, smaller values make the camera closer and larger values make the
        ///     camera further away.
        /// </param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool PlayerTextDrawSetPreviewRot(int playerid, int text, Vector rotation, float fZoom = 1.0f)
        {
            return PlayerTextDrawSetPreviewRot(playerid, text, rotation.X, rotation.Y, rotation.Z, fZoom);
        }

        /// <summary>
        ///     Plays the specified sound for a player.
        /// </summary>
        /// <remarks>
        ///     Only use the coordinates if you want the sound to be played at a certain position. Set coordinates all to 0 to just
        ///     play the sound.
        /// </remarks>
        /// <param name="playerid">The ID of the player for whom to play the sound.</param>
        /// <param name="soundid">The sound to play.</param>
        /// <param name="position">coordinates for the sound to play at. (0,0,0 for no position)</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool PlayerPlaySound(int playerid, int soundid, Vector position = new Vector())
        {
            return PlayerPlaySound(playerid, soundid, position.X, position.Y, position.Z);
        }

        /// <summary>
        ///     This Function allows to set players special action.
        /// </summary>
        /// <param name="playerid">The player that should perform the action.</param>
        /// <param name="action">The action that should be performed.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetPlayerSpecialAction(int playerid, SpecialAction action)
        {
            return SetPlayerSpecialAction(playerid, (int) action);
        }

        /// <summary>
        ///     Sets a checkpoint (red circle) for a player. Also shows a red blip on the radar.
        /// </summary>
        /// <remarks>
        ///     Checkpoints created on server-created objects (<see cref="CreateObject(int,Vector,Vector,float)" />/
        ///     <see cref="CreatePlayerObject(int,int,Vector,Vector,float)" />) will
        ///     appear down on the 'real' ground, but will still function correctly. There is no fix available for this issue. A
        ///     pickup can be used instead.
        /// </remarks>
        /// <param name="playerid">The ID of the player to set the checkpoint of.</param>
        /// <param name="position">The coordinate to set the checkpoint at.</param>
        /// <param name="size">The size of the checkpoint.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetPlayerCheckpoint(int playerid, Vector position, float size)
        {
            return SetPlayerCheckpoint(playerid, position.X, position.Y, position.Z, size);
        }

        /// <summary>
        ///     Creates a race checkpoint. When the player enters it, the <see cref="BaseMode.OnPlayerEnterRaceCheckpoint(int)" />
        ///     callback is called.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the checkpoint for.</param>
        /// <param name="type">Type of checkpoint.</param>
        /// <param name="point">Position of the checkpoint.</param>
        /// <param name="nextPosition">Position of the next point, for the arrow facing direction.</param>
        /// <param name="size">Length (diameter) of the checkpoint</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetPlayerRaceCheckpoint(int playerid, CheckpointType type, Vector point, Vector nextPosition,
            float size)
        {
            return SetPlayerRaceCheckpoint(playerid, (int) type, point.X, point.Y, point.Z, nextPosition.X,
                nextPosition.Y, nextPosition.Z, size);
        }


        /// <summary>
        ///     This function allows you to place your own icons on the map, enabling you to emphasise the locations of banks,
        ///     airports or whatever else you want. A total of 63 icons are available in GTA: San Andreas, all of which can be used
        ///     using this function. You can also specify the color of the icon, which allows you to change the square icon (ID:
        ///     0).
        /// </summary>
        /// <param name="playerid">The ID of the player to set the map icon for.</param>
        /// <param name="iconid">The player's icon ID, ranging from 0 to 99, to be used in RemovePlayerMapIcon.</param>
        /// <param name="position">The coordinates of the place where you want the icon to be.</param>
        /// <param name="markertype">The icon to set.</param>
        /// <param name="color">The color of the icon, this should only be used with the square icon (ID: 0).</param>
        /// <param name="style">The style of icon.</param>
        /// <returns>True if it was successful, False otherwise (e.g. the player isn't connected).</returns>
        public static bool SetPlayerMapIcon(int playerid, int iconid, Vector position, PlayerMarkersMode markertype,
            int color, int style)
        {
            return SetPlayerMapIcon(playerid, iconid, position.X, position.Y, position.Z, (int) markertype, color, style);
        }

        /// <summary>
        ///     Sets the camera to a specific position for a player.
        /// </summary>
        /// <param name="playerid">ID of the player.</param>
        /// <param name="position">New position of the camera.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetPlayerCameraPos(int playerid, Vector position)
        {
            return SetPlayerCameraPos(playerid, position.X, position.Y, position.Z);
        }

        /// <summary>
        ///     Set the direction a player's camera looks at. To be used in combination with SetPlayerCameraPos.
        /// </summary>
        /// <param name="playerid">The player to change the camera of.</param>
        /// <param name="point">The coordinates for the player's camera to look at.</param>
        /// <param name="cut">The style the camera-position changes.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool SetPlayerCameraLookAt(int playerid, Vector point, CameraCut cut)
        {
            return SetPlayerCameraLookAt(playerid, point.X, point.Y, point.Z, (int) cut);
        }

        /// <summary>
        ///     Get the position of the player's camera.
        /// </summary>
        /// <remarks>
        ///     Player's camera positions are only updated once a second, unless aiming.
        /// </remarks>
        /// <param name="playerid">The ID of the player to get the camera position of.</param>
        /// <returns>The position of the camera.</returns>
        public static Vector GetPlayerCameraPos(int playerid)
        {
            float x, y, z;
            GetPlayerCameraPos(playerid, out x, out y, out z);
            return new Vector(x, y, z);
        }

        /// <summary>
        ///     This function will return the current direction of player's aiming in 3-D space, the coords are relative to the
        ///     camera position, see <see cref="GetPlayerCameraPos(int)" />.
        /// </summary>
        /// <param name="playerid">The ID of the player you want to obtain the camera front vector of.</param>
        /// <returns>This camera front vector of the player.</returns>
        public static Vector GetPlayerCameraFrontVector(int playerid)
        {
            float x, y, z;
            GetPlayerCameraFrontVector(playerid, out x, out y, out z);
            return new Vector(x, y, z);
        }

        /// <summary>
        ///     Move a player's camera from one position to another, within the set time.
        /// </summary>
        /// <param name="playerid">The ID of the player the camera should be moved for.</param>
        /// <param name="from">The position the camera should start to move from.</param>
        /// <param name="to">The position the camera should move to.</param>
        /// <param name="time">Time in milliseconds.</param>
        /// <param name="cut">The jumpcut to use.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool InterpolateCameraPos(int playerid, Vector from, Vector to, int time, CameraCut cut)
        {
            return InterpolateCameraPos(playerid, from.X, from.Y, from.Z, to.X, to.Y, to.Z, time, (int) cut);
        }

        /// <summary>
        ///     Interpolate a player's camera's 'look at' point between two coordinates with a set speed. Can be be used with
        ///     <see cref="InterpolateCameraPos(int,Vector,Vector,int,CameraCut)" />.
        /// </summary>
        /// <param name="playerid">The ID of the player the camera should be moved for.</param>
        /// <param name="from">The position the camera should start to move from.</param>
        /// <param name="to">The position the camera should move to.</param>
        /// <param name="time">Time in milliseconds to complete interpolation.</param>
        /// <param name="cut">The 'jumpcut' to use.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        public static bool InterpolateCameraLookAt(int playerid, Vector from, Vector to, int time, CameraCut cut)
        {
            return InterpolateCameraLookAt(playerid, from.X, from.Y, from.Z, to.X, to.Y, to.Z, time, (int) cut);
        }

        /// <summary>
        ///     Creates an explosion for a player.
        ///     Only the specific player will see explosion and feel its effects.
        ///     This is useful when you want to isolate explosions from other players or to make them only appear in specific
        ///     virtual worlds.
        /// </summary>
        /// <param name="playerid">The ID of the player to create the explosion for.</param>
        /// <param name="position">The position of the explosion.</param>
        /// <param name="type">The explosion type.</param>
        /// <param name="radius">The radius of the explosion.</param>
        /// <returns>This function does not return any specific values.</returns>
        public static bool CreateExplosionForPlayer(int playerid, Vector position, int type, float radius)
        {
            return CreateExplosionForPlayer(playerid, position.X, position.Y, position.Z, type, radius);
        }
    }
}