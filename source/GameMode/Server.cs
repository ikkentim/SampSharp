using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GameMode.Definitions;
using GameMode.Events;
using GameMode.World;

namespace GameMode
{
    public class Server
    {
        #region Fields

        private static readonly Dictionary<int, TimerTickHandler> TimerHandlers = new Dictionary<int, TimerTickHandler>();

        #endregion

        #region SA:MP Natives
        /// <summary>
        /// This function can be used to change the spawn information of a specific player. It allows you to automatically set someone's spawn weapons, their team, skin and spawn position, normally used in case of minigames or automatic-spawn systems. This function is more crash-safe then using SetPlayerSkin in OnPlayerSpawn and/or OnPlayerRequestClass, even though this has been fixed in 0.2.
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
        /// (Re)Spawns a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to spawn.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SpawnPlayer(int playerid);

        /// <summary>
        /// Set a player's position.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the position of.</param>
        /// <param name="x">The X coordinate to position the player at.</param>
        /// <param name="y">The Y coordinate to position the player at.</param>
        /// <param name="z">The Z coordinate to position the player at.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerPos(int playerid, float x, float y, float z);

        /// <summary>
        /// This sets the players position then adjusts the players z-coordinate to the nearest solid ground under the position.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the position of.</param>
        /// <param name="x">The X coordinate to position the player at.</param>
        /// <param name="y">The Y coordinate to position the player at.</param>
        /// <param name="z">The Z coordinate to position the player at.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerPosFindZ(int playerid, float x, float y, float z);

        /// <summary>
        /// Get the X Y Z coordinates of a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the position of</param>
        /// <param name="x">A float to store the X coordinate in, passed by reference.</param>
        /// <param name="y">A float to store the Y coordinate in, passed by reference.</param>
        /// <param name="z">A float to store the Z coordinate in, passed by reference.</param>
        /// <returns>This function doesn't return a specific value</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerPos(int playerid, out float x, out float y, out float z);

        /// <summary>
        /// Set a player's facing angle.
        /// </summary>
        /// <remarks>
        /// Angles are reversed in GTA:SA - 90 degrees would be East in the real world, but in GTA:SA 90 is in fact West. North and South are still 0/360 and 180. To convert this, simply do 360 - angle.
        /// </remarks>
        /// <param name="playerid">The ID of the player to set the facing angle of.</param>
        /// <param name="angle">The angle the player should face.</param>
        /// <returns>This function doesn't return a specific value</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerFacingAngle(int playerid, float angle);

        /// <summary>
        /// Return angle of the direction the player is facing.
        /// </summary>
        /// <param name="playerid">The player you want to get the angle of.</param>
        /// <param name="angle">The Float to store the angle in, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerFacingAngle(int playerid, out float angle);

        /// <summary>
        /// Check if a player is in range of a point.
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
        /// Calculate the distance between a player and a map coordinate.
        /// </summary>
        /// <param name="playerid">The ID of the player to calculate the distance from.</param>
        /// <param name="x">The X map coordinate.</param>
        /// <param name="y">The Y map coordinate.</param>
        /// <param name="z">The Z map coordinate.</param>
        /// <returns>The distance between the player and the point as a float.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern float GetPlayerDistanceFromPoint(int playerid, float x, float y, float z);

        /// <summary>
        /// Checks if a player is streamed in another player's client.
        /// </summary>
        /// <remarks>
        /// Players aren't streamed in on their own client, so if playerid is the same as forplayerid it will return false!
        /// </remarks>
        /// <remarks>
        /// Players stream out if they are more than 150 meters away (see server.cfg - stream_distance)
        /// </remarks>
        /// <param name="playerid">The ID of the player to check is streamed in.</param>
        /// <param name="forplayerid">The ID of the player to check if playerid is streamed in for.</param>
        /// <returns>True if forplayerid is streamed in for playerid, False if not.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerStreamedIn(int playerid, int forplayerid);

        /// <summary>
        /// Set the player's interior.
        /// </summary>
        /// <param name="playerid">The ID of the player to setthe interior of.</param>
        /// <param name="interiorid">The interior ID to set the player in.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerInterior(int playerid, int interiorid);

        /// <summary>
        /// Retrieves the player's current interior.
        /// </summary>
        /// <param name="playerid">The player to get the interior ID of.</param>
        /// <returns>The interior ID the player is currently in.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerInterior(int playerid);

        /// <summary>
        /// Set the health level of a player.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the health of.</param>
        /// <param name="health">The value to set the player's health to.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerHealth(int playerid, float health);

        /// <summary>
        /// The function GetPlayerHealth allows you to retrieve the health of a player. Useful for cheat detection, among other things.
        /// </summary>
        /// <param name="playerid">The ID of the player.</param>
        /// <param name="health">Float to store health, passed by reference.</param>
        /// <returns>True if succeeded, False if not.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerHealth(int playerid, out float health);

        /// <summary>
        /// Set a player's armour level.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the armour of.</param>
        /// <param name="armour">The amount of armour to set, as a percentage (float).</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerArmour(int playerid, float armour);

        /// <summary>
        /// This function stores the armour of a player into a variable.
        /// </summary>
        /// <param name="playerid">The ID of the player that you want to get the armour of.</param>
        /// <param name="armour">The float to to store the armour in, passed by reference.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerArmour(int playerid, out float armour);

        /// <summary>
        /// Set the ammo of a player's weapon.
        /// </summary>
        /// <param name="playerid">The ID of the player to set the weapon ammo of.</param>
        /// <param name="weaponid">The ID of the weapon to set the ammo of.</param>
        /// <param name="ammo">The amount of ammo to set.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerAmmo(int playerid, int weaponid, int ammo);

        /// <summary>
        /// Returns the amount of ammunition the player has in his active weapon slot.
        /// </summary>
        /// <remarks>
        /// The ammo can hold 16-bit values, therefore values over 32767 will return erroneous values.
        /// </remarks>
        /// <param name="playerid">ID of the player.</param>
        /// <returns>The amount of ammunition the player has in his active weapon slot.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerAmmo(int playerid);

        /// <summary>
        /// Checks the state of a player's weapon.
        /// </summary>
        /// <param name="playerid">The ID of the player to obtain the state of.</param>
        /// <returns>The state of the player's weapon.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerWeaponState(int playerid);

        /// <summary>
        /// Check who a player is aiming at.
        /// </summary>
        /// <param name="playerid">The ID of the player to get the target of.</param>
        /// <returns>The ID of the target player, or <see cref="GameMode.Definitions.Misc.PlayerId"/> if none.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerTargetPlayer(int playerid);

        /// <summary>
        /// Set the team of a player.
        /// </summary>
        /// <remarks>
        /// Players can not damage/kill players on the same team unless they use a knife to slit their throat. As of SA-MP 0.3x, players are also unable to damage vehicles driven by a player from the same team. This can be enabled with <see cref="GameMode.Server.EnableVehicleFriendlyFire"/>.
        /// 255 (or <see cref="GameMode.Definitions.Misc.NoTeam"/>) is the default team to be able to shoot other players, not 0.
        /// </remarks>
        /// <param name="playerid">The ID of the player you want to set the team of.</param>
        /// <param name="teamid">The team to put the player in. Use <see cref="GameMode.Definitions.Misc.NoTeam"/> to remove the player from any team.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerTeam(int playerid, int teamid);

        /// <summary>
        /// Get the ID of the team the player is on.
        /// </summary>
        /// <param name="playerid">The ID of the player to return the team of.</param>
        /// <returns>The ID of the team the player is on, or 255 (defined as <see cref="GameMode.Definitions.Misc.NoTeam"/>) if they aren't on a team (default).</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerTeam(int playerid);

        /// <summary>
        /// Set a player's score. Players' scores are shown in the scoreboard (hold TAB).
        /// </summary>
        /// <param name="playerid">The ID of the player to set the score of.</param>
        /// <param name="score">The value to set the player's score to.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerScore(int playerid, int score);

        /// <summary>
        /// This function returns a player's score as it was set using <see cref="GameMode.Server.SetPlayerScore"/>
        /// </summary>
        /// <param name="playerid">The player to get the score of.</param>
        /// <returns>The player's score.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerScore(int playerid);

        /// <summary>
        /// Checks the player's level of drunkenness.
        /// </summary>
        /// <remarks>
        /// If the level is less than 2000, the player is sober. The player's level of drunkness goes down slowly automatically (26 levels per second) but will always reach 2000 at the end (in 0.3b it will stop at zero). The higher drunkenness levels affect the player's camera, and the car driving handling. The level of drunkenness increases when the player drinks from a bottle (You can use <see cref="GameMode.Server.SetPlayerSpecialAction"/> to give them bottles).
        /// </remarks>
        /// <param name="playerid">The player you want to check the drunkenness level of.</param>
        /// <returns>An integer with the level of drunkenness of the player.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerDrunkLevel(int playerid);

        /// <summary>
        /// Sets the drunk level of a player which makes the player's camera sway and vehicles hard to control.
        /// </summary>
        /// <remarks>
        /// Players' drunk level will automatically decrease over time, based on their FPS (players with 50 FPS will lose 50 'levels' per second. This is useful for determining a player's FPS!).
        /// In 0.3a the drunk level will decrement and stop at 2000. In 0.3b+ the drunk level decrements to zero.)
        /// Levels over 2000 make the player drunk (camera swaying and vehicles difficult to control).
        /// Max drunk level is 50000.
        /// While the drunk level is above 5000, the player's HUD (radar etc.) will be hidden.
        /// </remarks>
        /// <param name="playerid">The ID of the player to set the drunkenness of.</param>
        /// <param name="level">The level of drunkenness to set.</param>
        /// <returns>This function doesn't return a specific value.</returns>
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerDrunkLevel(int playerid, int level);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerColor(int playerid, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerColor(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerSkin(int playerid, int skinid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerSkin(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GivePlayerWeapon(int playerid, int weaponid, int ammo);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ResetPlayerWeapons(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerArmedWeapon(int playerid, int weaponid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerWeaponData(int playerid, int slot, out int weapon, out int ammo);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GivePlayerMoney(int playerid, int money);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ResetPlayerMoney(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int SetPlayerName(int playerid, string name);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerMoney(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerState(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerIp(int playerid, out string ip, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerPing(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerWeapon(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerKeys(int playerid, out int keys, out int updown, out int leftright);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerName(int playerid, out string name, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerTime(int playerid, int hour, int minute);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerTime(int playerid, out int hour, out int minute);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TogglePlayerClock(int playerid, bool toggle);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerWeather(int playerid, int weather);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ForceClassSelection(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerWantedLevel(int playerid, int level);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerWantedLevel(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerFightingStyle(int playerid, int style);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerFightingStyle(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerVelocity(int playerid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerVelocity(int playerid, out float x, out float y, out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayCrimeReportForPlayer(int playerid, int suspectid, int crime);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayAudioStreamForPlayer(int playerid, string url, float posX, float posY, float posZ,
            float distance, bool usepos);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StopAudioStreamForPlayer(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerShopName(int playerid, string shopname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerSkillLevel(int playerid, int skill, int level);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerSurfingVehicleID(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerSurfingObjectID(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemoveBuildingForPlayer(int playerid, int modelid, float fX, float fY, float fZ,
            float fRadius);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerAttachedObject(int playerid, int index, int modelid, int bone, float fOffsetX,
            float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX, float fScaleY,
            float fScaleZ, int materialcolor1, int materialcolor2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemovePlayerAttachedObject(int playerid, int index);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerAttachedObjectSlotUsed(int playerid, int index);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EditAttachedObject(int playerid, int index);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePlayerTextDraw(int playerid, float x, float y, string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawDestroy(int playerid, int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawLetterSize(int playerid, int text, float x, float y);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawTextSize(int playerid, int text, float x, float y);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawAlignment(int playerid, int text, int alignment);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawColor(int playerid, int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawUseBox(int playerid, int text, bool use);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawBoxColor(int playerid, int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetShadow(int playerid, int text, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetOutline(int playerid, int text, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawBackgroundColor(int playerid, int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawFont(int playerid, int text, int font);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetProportional(int playerid, int text, bool set);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetSelectable(int playerid, int text, bool set);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawShow(int playerid, int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawHide(int playerid, int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetString(int playerid, int text, string str);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetPreviewModel(int playerid, int text, int modelindex);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetPreviewRot(int playerid, int text, float fRotX, float fRotY,
            float fRotZ, float fZoom);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerTextDrawSetPreviewVehCol(int playerid, int text, int color1, int color2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPVarInt(int playerid, string varname, int value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPVarInt(int playerid, string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPVarString(int playerid, string varname, string value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPVarString(int playerid, string varname, out string value, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPVarFloat(int playerid, string varname, float value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern float GetPVarFloat(int playerid, string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DeletePVar(int playerid, string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPVarsUpperIndex(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPVarNameAtIndex(int playerid, int index, out string varname, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPVarType(int playerid, string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerChatBubble(int playerid, string text, int color, float drawdistance,
            int expiretime);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PutPlayerInVehicle(int playerid, int vehicleid, int seatid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerVehicleID(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerVehicleSeat(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemovePlayerFromVehicle(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TogglePlayerControllable(int playerid, bool toggle);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerPlaySound(int playerid, int soundid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ApplyAnimation(int playerid, string animlib, string animname, float fDelta, bool loop,
            bool lockx, bool locky, bool freeze, int time, bool forcesync);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ClearAnimations(int playerid, bool forcesync);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerAnimationIndex(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetAnimationName(int index, out string animlib, int animlibSize, out string animname,
            int animnameSize);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerSpecialAction(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerSpecialAction(int playerid, int actionid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerCheckpoint(int playerid, float x, float y, float z, float size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisablePlayerCheckpoint(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerRaceCheckpoint(int playerid, int type, float x, float y, float z, float nextx,
            float nexty, float nextz, float size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisablePlayerRaceCheckpoint(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerWorldBounds(int playerid, float xMax, float xMin, float yMax, float yMin);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerMarkerForPlayer(int playerid, int showplayerid, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowPlayerNameTagForPlayer(int playerid, int showplayerid, bool show);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerMapIcon(int playerid, int iconid, float x, float y, float z, int markertype,
            int color, int style);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RemovePlayerMapIcon(int playerid, int iconid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AllowPlayerTeleport(int playerid, bool allow);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerCameraPos(int playerid, float x, float y, float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerCameraLookAt(int playerid, float x, float y, float z, int cut);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetCameraBehindPlayer(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerCameraPos(int playerid, out float x, out float y, out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerCameraFrontVector(int playerid, out float x, out float y, out float z);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerCameraMode(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachCameraToObject(int playerid, int objectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AttachCameraToPlayerObject(int playerid, int playerobjectid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool InterpolateCameraPos(int playerid, float fromX, float fromY, float fromZ, float toX,
            float toY, float toZ, int time, int cut);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool InterpolateCameraLookAt(int playerid, float fromX, float fromY, float fromZ, float toX,
            float toY, float toZ, int time, int cut);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerConnected(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInVehicle(int playerid, int vehicleid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInAnyVehicle(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInCheckpoint(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerInRaceCheckpoint(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetPlayerVirtualWorld(int playerid, int worldid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerVirtualWorld(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableStuntBonusForPlayer(int playerid, bool enable);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableStuntBonusForAll(bool enable);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TogglePlayerSpectating(int playerid, bool toggle);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerSpectatePlayer(int playerid, int targetplayerid, int mode);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool PlayerSpectateVehicle(int playerid, int targetvehicleid, int mode);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StartRecordingPlayerData(int playerid, int recordtype, string recordname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool StopRecordingPlayerData(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendClientMessage(int playerid, int color, string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendClientMessageToAll(int color, string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendPlayerMessageToPlayer(int playerid, int senderid, string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendPlayerMessageToAll(int senderid, string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendDeathMessage(int killer, int killee, int weapon);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GameTextForAll(string text, int time, int style);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GameTextForPlayer(int playerid, string text, int time, int style);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetTickCount();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetMaxPlayers();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetGameModeText(string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetTeamCount(int count);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddPlayerClass(int modelid, float spawnX, float spawnY, float spawnZ, float zAngle,
            int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddPlayerClassEx(int teamid, int modelid, float spawnX, float spawnY, float spawnZ,
            float zAngle, int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddStaticVehicle(int modelid, float spawnX, float spawnY, float spawnZ,
            float zAngle, int color1, int color2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddStaticVehicleEx(int modelid, float spawnX, float spawnY, float spawnZ,
            float zAngle, int color1, int color2, int respawnDelay);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddStaticPickup(int model, int type, float x, float y, float z, int virtualworld);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePickup(int model, int type, float x, float y, float z, int virtualworld);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyPickup(int pickup);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowNameTags(bool show);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowPlayerMarkers(int mode);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GameModeExit();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetWorldTime(int hour);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetWeaponName(int weaponid, out string name, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableTirePopping(bool enable);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableVehicleFriendlyFire();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AllowInteriorWeapons(bool allow);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetWeather(int weatherid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetGravity(float gravity);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AllowAdminTeleport(bool allow);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetDeathDropAmount(int amount);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CreateExplosion(float x, float y, float z, int type, float radius);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableZoneNames(bool enable);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool UsePlayerPedAnims();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableInteriorEnterExits();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetNameTagDrawDistance(float distance);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableNameTagLOS();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool LimitGlobalChatRadius(float chatRadius);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool LimitPlayerMarkerRadius(float markerRadius);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ConnectNPC(string name, string script);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerNPC(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerAdmin(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Kick(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Ban(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool BanEx(int playerid, string reason);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendRconCommand(string command);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetServerVarAsString(string varname, out string value, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetServerVarAsInt(string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetServerVarAsBool(string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerNetworkStats(int playerid, out string retstr, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetNetworkStats(out string retstr, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerVersion(int playerid, out string version, int len);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreateMenu(string title, int columns, float x, float y, float col1Width,
            float col2Width);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyMenu(int menuid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddMenuItem(int menuid, int column, string menutext);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetMenuColumnHeader(int menuid, int column, string columnheader);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowMenuForPlayer(int menuid, int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool HideMenuForPlayer(int menuid, int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsValidMenu(int menuid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableMenu(int menuid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableMenuRow(int menuid, int row);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerMenu(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int TextDrawCreate(float x, float y, string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawDestroy(int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawLetterSize(int text, float x, float y);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawTextSize(int text, float x, float y);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawAlignment(int text, int alignment);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawColor(int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawUseBox(int text, bool use);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawBoxColor(int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetShadow(int text, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetOutline(int text, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawBackgroundColor(int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawFont(int text, int font);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetProportional(int text, bool set);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetSelectable(int text, bool set);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawShowForPlayer(int playerid, int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawHideForPlayer(int playerid, int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawShowForAll(int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawHideForAll(int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetString(int text, string str);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetPreviewModel(int text, int modelindex);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetPreviewRot(int text, float fRotX, float fRotY, float fRotZ, float fZoom);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetPreviewVehCol(int text, int color1, int color2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SelectTextDraw(int playerid, int hovercolor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CancelSelectTextDraw(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GangZoneCreate(float minx, float miny, float maxx, float maxy);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneDestroy(int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneShowForPlayer(int playerid, int zone, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneShowForAll(int zone, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneHideForPlayer(int playerid, int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneHideForAll(int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneFlashForPlayer(int playerid, int zone, int flashcolor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneFlashForAll(int zone, int flashcolor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneStopFlashForPlayer(int playerid, int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneStopFlashForAll(int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int Create3DTextLabel(string text, int color, float x, float y, float z, float drawDistance,
            int virtualworld, bool testLOS);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Delete3DTextLabel(int id);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Attach3DTextLabelToPlayer(int id, int playerid, float offsetX, float offsetY,
            float offsetZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Attach3DTextLabelToVehicle(int id, int vehicleid, float offsetX, float offsetY,
            float offsetZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Update3DTextLabelText(int id, int color, string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePlayer3DTextLabel(int playerid, string text, int color, float x, float y, float z,
            float drawDistance, int attachedplayer, int attachedvehicle, bool testLOS);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DeletePlayer3DTextLabel(int playerid, int id);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool UpdatePlayer3DTextLabelText(int playerid, int id, int color, string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowPlayerDialog(int playerid, int dialogid, int style, string caption, string info,
            string button1, string button2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern int SetTimer(int interval, bool repeat, object args);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool KillTimer(int timerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        // ReSharper disable once InconsistentNaming
        public static extern bool gpci(int playerid, out string buffer, int size);

        #endregion

        #region Wrapping methods

        public static bool SetSpawnInfo(int playerid, int team, int skin, float x, float y, float z,
            float rotation, Weapon weapon1, int weapon1Ammo, Weapon weapon2, int weapon2Ammo, Weapon weapon3,
            int weapon3Ammo)
        {
            return SetSpawnInfo(playerid, team, skin, x, y, z, rotation, (int) weapon1, weapon1Ammo, (int) weapon2,
                weapon2Ammo, (int) weapon3, weapon3Ammo);
        }

        public static float GetPlayerFacingAngle(int playerid)
        {
            float angle;
            GetPlayerFacingAngle(playerid, out angle);
            return angle;
        }

        public static float GetPlayerHealth(int playerid)
        {
            float health;
            GetPlayerHealth(playerid, out health);
            return health;
        }

        public static float GetPlayerArmour(int playerid)
        {
            float armour;
            GetPlayerArmour(playerid, out armour);
            return armour;
        }

        public static string GetPlayerIp(int playerid)
        {
            string ip;
            GetPlayerIp(playerid, out ip, 16);
            return ip;
        }

        public static string GetPlayerName(int playerid)
        {
            string name;
            GetPlayerName(playerid, out name, Limits.MaxPlayerName);
            return name;
        }

        public static string GetPVarString(int playerid, string varname)
        {
            string value;
            GetPVarString(playerid, varname, out value, 64);
            return value;
        }

        public static string GetPVarNameAtIndex(int playerid, int index)
        {
            string varname;
            GetPVarNameAtIndex(playerid, index, out varname, 64);
            return varname;
        }

        public static string GetWeaponName(int weaponid)
        {
            string name;
            GetWeaponName(weaponid, out name, 32);
            return name;
        }

        public static string GetServerVarAsString(string varname)
        {
            string value;
            GetServerVarAsString(varname, out value, 64);
            return value;
        }

        public static string GetPlayerNetworkStats(int playerid)
        {
            string retstr;
            GetPlayerNetworkStats(playerid, out retstr, 256);
            return retstr;
        }

        public static string GetNetworkStats()
        {
            string retstr;
            GetNetworkStats(out retstr, 256);
            return retstr;
        }

        public static string GetPlayerVersion(int playerid)
        {
            string version;
            GetPlayerVersion(playerid, out version, 64);
            return version;
        }

        // ReSharper disable once InconsistentNaming
        public static string gpci(int playerid)
        {
            string buffer;
            gpci(playerid, out buffer, 64);
            return buffer;
        }

        public static int SetTimer(int interval, bool repeat, TimerTickHandler handler, object args)
        {
            int timerid = SetTimer(interval, repeat, args);

            TimerHandlers[timerid] = handler;
            return timerid;
        }

        #endregion

        #region Events

        public delegate bool TimerTickHandler(int timerid, object args);

        public delegate void GameModeHandler(object sender, GameModeEventArgs e);

        public delegate void PlayerHandler(object sender, PlayerEventArgs e);

        public delegate void PlayerDisconnectedHandler(object sender, PlayerDisconnectedEventArgs e);

        public delegate void PlayerDeathHandler(object sender, PlayerDeathEventArgs e);

        public delegate void VehicleSpawnedHandler(object sender, VehicleEventArgs e);

        public delegate void VehicleDeathHandler(object sender, PlayerVehicleEventArgs e);

        public delegate void PlayerTextHandler(object sender, PlayerTextEventArgs e);

        public delegate void PlayerRequestClassHandler(object sender, PlayerRequestClassEventArgs e);

        public delegate void PlayerEnterVehicleHandler(object sender, PlayerEnterVehicleEventArgs e);

        public delegate void PlayerAndVehicleHandler(object sender, PlayerVehicleEventArgs r);

        public delegate void PlayerStateHandler(object sender, PlayerStateEventArgs e);

        public delegate void RconHandler(object sender, RconEventArgs e);

        public delegate void ObjectHandler(object sender, ObjectEventArgs e);

        public delegate void PlayerObjectHandler(object sender, PlayerObjectEventArgs e);

        public delegate void PlayerPickupHandler(object sender, PlayerPickupEventArgs e);

        public delegate void VehicleModHandler(object sender, VehicleModEventArgs e);

        public delegate void PlayerEnterModShopHandler(object sender, PlayerEnterModShopEventArgs e);

        public delegate void VehiclePaintjobHandler(object sender, VehiclePaintjobEventArgs e);

        public delegate void VehicleResprayedHandler(object sender, VehicleResprayedEventArgs e);

        public delegate void UnoccupiedVehicleUpdatedHandler(object sender, UnoccupiedVehicleEventArgs e);

        public delegate void PlayerSelectedMenuRowHandler(object sender, PlayerSelectedMenuRowEventArgs e);

        public delegate void PlayerInteriorChangedHandler(object sender, PlayerInteriorChangedEventArgs e);

        public delegate void PlayerKeyStateChangedHandler(object sender, PlayerKeyStateChangedEventArgs e);

        public delegate void RconLoginAttemptHandler(object sender, RconLoginAttemptEventArgs e);

        public delegate void StreamPlayerHandler(object sender, StreamPlayerEventArgs e);

        public delegate void DialogResponseHandler(object sender, DialogResponseEventArgs e);

        public delegate void PlayerDamageHandler(object sender, PlayerDamageEventArgs e);

        public delegate void PlayerClickMapHandler(object sender, PlayerClickMapEventArgs e);

        public delegate void PlayerClickTextDrawHandler(object sender, PlayerClickTextDrawEventArgs e);

        public delegate void PlayerClickPlayerHandler(object sender, PlayerClickPlayerEventArgs e);

        public delegate void PlayerEditObjectHandler(object sender, PlayerEditObjectEventArgs e);

        public delegate void PlayerEditAttachedObjectHandler(object sender, PlayerEditAttachedObjectEventArgs e);

        public delegate void PlayerSelectObjectHandler(object sender, PlayerSelectObjectEventArgs e);

        public delegate void WeaponShotHandler(object sender, WeaponShotEventArgs e);

        public event GameModeHandler GameModeInitialized;

        public event GameModeHandler GameModeExited;

        public event PlayerHandler PlayerConnected;

        public event PlayerDisconnectedHandler PlayerDisconnected;

        public event PlayerHandler PlayerSpawned;

        public event PlayerDeathHandler PlayerDied;

        public event VehicleSpawnedHandler VehicleSpawned;

        public event VehicleDeathHandler VehicleDied;

        public event PlayerTextHandler PlayerText;

        public event PlayerTextHandler PlayerCommandText;

        public event PlayerRequestClassHandler PlayerRequestClass;

        public event PlayerEnterVehicleHandler PlayerEnterVehicle;

        public event PlayerAndVehicleHandler PlayerExitVehicle;

        public event PlayerStateHandler PlayerStateChanged;

        public event PlayerHandler PlayerEnterCheckpoint;

        public event PlayerHandler PlayerLeaveCheckpoint;

        public event PlayerHandler PlayerEnterRaceCheckpoint;

        public event PlayerHandler PlayerLeaveRaceCheckpoint;

        public event RconHandler RconCommand;

        public event PlayerHandler PlayerRequestSpawn;

        public event ObjectHandler ObjectMoved;

        public event PlayerObjectHandler PlayerObjectMoved;

        public event PlayerPickupHandler PlayerPickUpPickup;

        public event VehicleModHandler VehicleMod;

        public event PlayerEnterModShopHandler PlayerEnterExitModShop;

        public event VehiclePaintjobHandler VehiclePaintjobApplied;

        public event VehicleResprayedHandler VehicleResprayed;

        public event PlayerHandler VehicleDamageStatusUpdated;

        public event UnoccupiedVehicleUpdatedHandler UnoccupiedVehicleUpdated;

        public event PlayerSelectedMenuRowHandler PlayerSelectedMenuRow;

        public event PlayerHandler PlayerExitedMenu;

        public event PlayerInteriorChangedHandler PlayerInteriorChanged;

        public event PlayerKeyStateChangedHandler PlayerKeyStateChanged;

        public event RconLoginAttemptHandler RconLoginAttempt;

        public event PlayerHandler PlayerUpdate;

        public event StreamPlayerHandler StreamPlayerIn;

        public event StreamPlayerHandler StreamPlayerOut;

        public event PlayerAndVehicleHandler StreamVehicleIn;

        public event PlayerAndVehicleHandler StreamVehicleOut;

        public event DialogResponseHandler DialogResponse;

        public event PlayerDamageHandler PlayerTakeDamage;

        public event PlayerDamageHandler PlayerGiveDamage;

        public event PlayerClickMapHandler PlayerClickMap;

        public event PlayerClickTextDrawHandler PlayerClickTextDraw;

        public event PlayerClickTextDrawHandler PlayerClickPlayerTextDraw;

        public event PlayerClickPlayerHandler PlayerClickPlayer;

        public event PlayerEditObjectHandler PlayerEditObject;

        public event PlayerEditAttachedObjectHandler PlayerEditAttachedObject;

        public event PlayerSelectObjectHandler PlayerSelectObject;

        public event WeaponShotHandler PlayerWeaponShot;

        #endregion

        #region Callbacks

        public bool OnTimerTick(int timerid, object args)
        {
            if (TimerHandlers.ContainsKey(timerid) && !TimerHandlers[timerid](timerid, args))
                KillTimer(timerid);
            return true;
        }

        public virtual bool OnGameModeInit()
        {
            var args = new GameModeEventArgs();

            if (GameModeInitialized != null)
                GameModeInitialized(this, args);

            return args.Success;
        }


        public virtual bool OnGameModeExit()
        {
            var args = new GameModeEventArgs();

            if (GameModeExited != null)
                GameModeExited(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerConnect(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerConnected != null)
                PlayerConnected(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerDisconnect(int playerid, int reason)
        {
            var args = new PlayerDisconnectedEventArgs(playerid, (PlayerDisconnectReason)reason);

            if (PlayerDisconnected != null)
                PlayerDisconnected(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerSpawn(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerSpawned != null)
                PlayerSpawned(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerDeath(int playerid, int killerid, int reason)
        {
            var args = new PlayerDeathEventArgs(playerid, killerid, (Weapon)reason);

            if (PlayerDied != null)
                PlayerDied(this, args);

            return args.Success;
        }

        public virtual bool OnVehicleSpawn(int vehicleid)
        {
            var args = new VehicleEventArgs(vehicleid);

            if (VehicleSpawned != null)
                VehicleSpawned(this, args);

            return args.Success;
        }

        public virtual bool OnVehicleDeath(int vehicleid, int killerid)
        {
            var args = new PlayerVehicleEventArgs(killerid, vehicleid);

            if (VehicleDied != null)
                VehicleDied(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerText(int playerid, string text)
        {
            var args = new PlayerTextEventArgs(playerid, text);

            if (PlayerText != null)
                PlayerText(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerCommandText(int playerid, string cmdtext)
        {
            var args = new PlayerTextEventArgs(playerid, cmdtext) {Success = false};

            if (PlayerCommandText != null)
                PlayerCommandText(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerRequestClass(int playerid, int classid)
        {
            var args = new PlayerRequestClassEventArgs(playerid, classid);

            if (PlayerRequestClass != null)
                PlayerRequestClass(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerEnterVehicle(int playerid, int vehicleid, bool ispassenger)
        {
            var args = new PlayerEnterVehicleEventArgs(playerid, vehicleid, ispassenger);

            if (PlayerEnterVehicle != null)
                PlayerEnterVehicle(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerExitVehicle(int playerid, int vehicleid)
        {
            var args = new PlayerVehicleEventArgs(playerid, vehicleid);

            if (PlayerExitVehicle != null)
                PlayerExitVehicle(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerStateChange(int playerid, int newstate, int oldstate)
        {
            var args = new PlayerStateEventArgs(playerid, (PlayerState)newstate, (PlayerState)oldstate);

            if (PlayerStateChanged != null)
                PlayerStateChanged(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerEnterCheckpoint(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerEnterCheckpoint != null)
                PlayerEnterCheckpoint(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerLeaveCheckpoint(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerLeaveCheckpoint != null)
                PlayerLeaveCheckpoint(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerEnterRaceCheckpoint(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerEnterRaceCheckpoint != null)
                PlayerEnterRaceCheckpoint(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerLeaveRaceCheckpoint(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerLeaveRaceCheckpoint != null)
                PlayerLeaveRaceCheckpoint(this, args);

            return args.Success;
        }

        public virtual bool OnRconCommand(string command)
        {
            var args = new RconEventArgs(command);

            if (RconCommand != null)
                RconCommand(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerRequestSpawn(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerRequestSpawn != null)
                PlayerRequestSpawn(this, args);

            return args.Success;
        }

        public virtual bool OnObjectMoved(int objectid)
        {
            var args = new ObjectEventArgs(objectid);

            if (ObjectMoved != null)
                ObjectMoved(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerObjectMoved(int playerid, int objectid)
        {
            var args = new PlayerObjectEventArgs(playerid, objectid);

            if (PlayerObjectMoved != null)
                PlayerObjectMoved(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerPickUpPickup(int playerid, int pickupid)
        {
            var args = new PlayerPickupEventArgs(playerid, pickupid);

            if (PlayerPickUpPickup != null)
                PlayerPickUpPickup(this, args);

            return args.Success;
        }

        public virtual bool OnVehicleMod(int playerid, int vehicleid, int componentid)
        {
            var args = new VehicleModEventArgs(playerid, vehicleid, componentid);

            if (VehicleMod != null)
                VehicleMod(this, args);

            return args.Success;
        }

        public virtual bool OnEnterExitModShop(int playerid, int enterexit, int interiorid)
        {
            var args = new PlayerEnterModShopEventArgs(playerid, (EnterExit)enterexit, interiorid);

            if (PlayerEnterExitModShop != null)
                PlayerEnterExitModShop(this, args);

            return args.Success;
        }

        public virtual bool OnVehiclePaintjob(int playerid, int vehicleid, int paintjobid)
        {
            var args = new VehiclePaintjobEventArgs(playerid, vehicleid, paintjobid);

            if (VehiclePaintjobApplied != null)
                VehiclePaintjobApplied(this, args);

            return args.Success;
        }

        public virtual bool OnVehicleRespray(int playerid, int vehicleid, int color1, int color2)
        {
            var args = new VehicleResprayedEventArgs(playerid, vehicleid, color1, color2);

            if (VehicleResprayed != null)
                VehicleResprayed(this, args);

            return args.Success;
        }

        public virtual bool OnVehicleDamageStatusUpdate(int vehicleid, int playerid)
        {
            var args = new PlayerVehicleEventArgs(playerid, vehicleid);

            if (VehicleDamageStatusUpdated != null)
                VehicleDamageStatusUpdated(this, args);

            return args.Success;
        }

        public virtual bool OnUnoccupiedVehicleUpdate(int vehicleid, int playerid, int passengerSeat)
        {
            var args = new UnoccupiedVehicleEventArgs(playerid, vehicleid, passengerSeat);

            if (UnoccupiedVehicleUpdated != null)
                UnoccupiedVehicleUpdated(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerSelectedMenuRow(int playerid, int row)
        {
            var args = new PlayerSelectedMenuRowEventArgs(playerid, row);

            if (PlayerSelectedMenuRow != null)
                PlayerSelectedMenuRow(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerExitedMenu(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerExitedMenu != null)
                PlayerExitedMenu(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerInteriorChange(int playerid, int newinteriorid, int oldinteriorid)
        {
            var args = new PlayerInteriorChangedEventArgs(playerid, newinteriorid, oldinteriorid);

            if (PlayerInteriorChanged != null)
                PlayerInteriorChanged(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerKeyStateChange(int playerid, int newkeys, int oldkeys)
        {
            var args = new PlayerKeyStateChangedEventArgs(playerid, (Keys)newkeys, (Keys)oldkeys);

            if (PlayerKeyStateChanged != null)
                PlayerKeyStateChanged(this, args);

            return args.Success;
        }

        public virtual bool OnRconLoginAttempt(string ip, string password, bool success)
        {
            var args = new RconLoginAttemptEventArgs(ip, password, success);

            if (RconLoginAttempt != null)
                RconLoginAttempt(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerUpdate(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerUpdate != null)
                PlayerUpdate(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerStreamIn(int playerid, int forplayerid)
        {
            var args = new StreamPlayerEventArgs(playerid, forplayerid);

            if (StreamPlayerIn != null)
                StreamPlayerIn(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerStreamOut(int playerid, int forplayerid)
        {
            var args = new StreamPlayerEventArgs(playerid, forplayerid);

            if (StreamPlayerOut != null)
                StreamPlayerOut(this, args);

            return args.Success;
        }

        public virtual bool OnVehicleStreamIn(int vehicleid, int forplayerid)
        {
            var args = new PlayerVehicleEventArgs(forplayerid, vehicleid);

            if (StreamVehicleIn != null)
                StreamVehicleIn(this, args);

            return args.Success;
        }

        public virtual bool OnVehicleStreamOut(int vehicleid, int forplayerid)
        {
            var args = new PlayerVehicleEventArgs(forplayerid, vehicleid);

            if (StreamVehicleOut != null)
                StreamVehicleOut(this, args);

            return args.Success;
        }

        public virtual bool OnDialogResponse(int playerid, int dialogid, int response, int listitem, string inputtext)
        {
            var args = new DialogResponseEventArgs(playerid, dialogid, response, listitem, inputtext);

            if (DialogResponse != null)
                DialogResponse(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerTakeDamage(int playerid, int issuerid, float amount, int weaponid, int bodypart)
        {
            var args = new PlayerDamageEventArgs(playerid, issuerid, amount, (Weapon)weaponid, (BodyPart)bodypart);

            if (PlayerTakeDamage != null)
                PlayerTakeDamage(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerGiveDamage(int playerid, int damagedid, float amount, int weaponid, int bodypart)
        {
            var args = new PlayerDamageEventArgs(playerid, damagedid, amount, (Weapon)weaponid, (BodyPart)bodypart);

            if (PlayerGiveDamage != null)
                PlayerGiveDamage(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerClickMap(int playerid, float fX, float fY, float fZ)
        {
            var args = new PlayerClickMapEventArgs(playerid, new Position(fX, fY, fZ));

            if (PlayerClickMap != null)
                PlayerClickMap(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerClickTextDraw(int playerid, int clickedid)
        {
            var args = new PlayerClickTextDrawEventArgs(playerid, clickedid);

            if (PlayerClickTextDraw != null)
                PlayerClickTextDraw(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerClickPlayerTextDraw(int playerid, int playertextid)
        {
            var args = new PlayerClickTextDrawEventArgs(playerid, playertextid);

            if (PlayerClickPlayerTextDraw != null)
                PlayerClickPlayerTextDraw(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerClickPlayer(int playerid, int clickedplayerid, int source)
        {
            var args = new PlayerClickPlayerEventArgs(playerid, clickedplayerid, (PlayerClickSource)source);

            if (PlayerClickPlayer != null)
                PlayerClickPlayer(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerEditObject(int playerid, bool playerobject, int objectid, int response, float fX,
            float fY,
            float fZ, float fRotX, float fRotY, float fRotZ)
        {
            var args = new PlayerEditObjectEventArgs(playerid, playerobject, objectid, (EditObjectResponse) response,
                new Position(fX, fY, fZ), new Rotation(fRotX, fRotY, fRotZ));

            if (PlayerEditObject != null)
                PlayerEditObject(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerEditAttachedObject(int playerid, int response, int index, int modelid, int boneid,
            float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX,
            float fScaleY, float fScaleZ)
        {
            var args = new PlayerEditAttachedObjectEventArgs(playerid, (EditObjectResponse) response, index, modelid,
                boneid, new Position(fOffsetX, fOffsetY, fOffsetZ), new Rotation(fRotX, fRotY, fRotZ),
                new Position(fScaleX, fScaleY, fScaleZ));

            if (PlayerEditAttachedObject != null)
                PlayerEditAttachedObject(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerSelectObject(int playerid, int type, int objectid, int modelid, float fX, float fY,
            float fZ)
        {
            var args = new PlayerSelectObjectEventArgs(playerid, (ObjectType) type, objectid, modelid,
                new Position(fX, fY, fZ));

            if (PlayerSelectObject != null)
                PlayerSelectObject(this, args);

            return args.Success;
        }

        public virtual bool OnPlayerWeaponShot(int playerid, int weaponid, int hittype, int hitid, float fX, float fY,
            float fZ)
        {
            var args = new WeaponShotEventArgs(playerid, (Weapon)weaponid, (BulletHitType)hittype, hitid, new Position(fX, fY, fZ));

            if (PlayerWeaponShot != null)
                PlayerWeaponShot(this, args);

            return args.Success;
        }

        #endregion
    }
}
