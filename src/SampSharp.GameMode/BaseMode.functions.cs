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

using SampSharp.GameMode.API;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode
{
    public abstract partial class BaseMode
    {
        #region Natives

        private delegate bool ManualVehicleEngineAndLightsImpl();

        private delegate bool EnableStuntBonusForAllImpl(bool enable);

        private delegate bool UsePlayerPedAnimsImpl();

        private delegate bool ShowPlayerMarkersImpl(int mode);

        private delegate int AddPlayerClassExImpl(
            int teamid, int modelid, float spawnX, float spawnY, float spawnZ, float zAngle, int weapon1,
            int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        private delegate int AddPlayerClassImpl(
            int modelid, float spawnX, float spawnY, float spawnZ, float zAngle, int weapon1, int weapon1Ammo,
            int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        private delegate bool LimitGlobalChatRadiusImpl(float chatRadius);

        private delegate bool LimitPlayerMarkerRadiusImpl(float markerRadius);

        private delegate bool GameModeExitImpl();

        private delegate bool SetTeamCountImpl(int count);

        private delegate bool ShowNameTagsImpl(bool show);

        [Native("ManualVehicleEngineAndLights")]
        private static readonly ManualVehicleEngineAndLightsImpl NativeManualVehicleEngineAndLights = null;

        [Native("EnableStuntBonusForAll")]
        private static readonly EnableStuntBonusForAllImpl NativeEnableStuntBonusForAll =
            null;

        [Native("UsePlayerPedAnims")]
        private static readonly UsePlayerPedAnimsImpl UsePlayerPedAnims = null;

        [Native("ShowPlayerMarkers")]
        private static readonly ShowPlayerMarkersImpl NativeShowPlayerMarkers = null;

        [Native("AddPlayerClass")]
        private static readonly AddPlayerClassImpl NativeAddPlayerClass = null;
        [Native("AddPlayerClassEx")]
        private static readonly AddPlayerClassExImpl NativeAddPlayerClassEx = null;

        [Native("LimitGlobalChatRadius")]
        private static readonly LimitGlobalChatRadiusImpl NativeLimitGlobalChatRadius = null;

        [Native("LimitPlayerMarkerRadius")]
        private static readonly LimitPlayerMarkerRadiusImpl NativeLimitPlayerMarkerRadius =
            null;

        [Native("GameModeExit")]
        private static readonly GameModeExitImpl GameModeExit = null;

        [Native("ShowNameTags")]
        private static readonly ShowNameTagsImpl NativeShowNameTags = null;

        [Native("SetTeamCount")]
        private static readonly SetTeamCountImpl NativeSetTeamCount = null;

        private delegate bool SetGameModeTextImpl(string text);
        [Native("SetGameModeText")]
        private static readonly SetGameModeTextImpl NativeSetGameModeText = null;

        private delegate bool EnableTirePoppingImpl(bool enable);

        private delegate bool EnableVehicleFriendlyFireImpl();

        [Native("EnableTirePopping")]
        private static readonly EnableTirePoppingImpl NativeEnableTirePopping = null;

        [Native("EnableVehicleFriendlyFire")]
        private static readonly EnableVehicleFriendlyFireImpl
            NativeEnableVehicleFriendlyFire = null;

        [Native("AllowInteriorWeapons")]
        private static readonly AllowInteriorWeaponsImpl NativeAllowInteriorWeapons = null;

        private delegate bool AllowInteriorWeaponsImpl(bool allow);

        private delegate float GetGravityImpl();

        private delegate bool SetGravityImpl(float gravity);

        [Native("SetGravity")]
        private static readonly SetGravityImpl NativeSetGravity = null;
        [Native("GetGravity")]
        private static readonly GetGravityImpl NativeGetGravity = null;

        private delegate bool DisableInteriorEnterExitsImpl();

        private delegate bool DisableNameTagLOSImpl();

        private delegate bool SetNameTagDrawDistanceImpl(float distance);

        [Native("DisableInteriorEnterExits")]
        private static readonly DisableInteriorEnterExitsImpl
            NativeDisableInteriorEnterExits = null;

        [Native("SetNameTagDrawDistance")]
        private static readonly SetNameTagDrawDistanceImpl NativeSetNameTagDrawDistance =
            null;

        [Native("DisableNameTagLOS")]
        private static readonly DisableNameTagLOSImpl NativeDisableNameTagLOS = null;

        #endregion


        /// <summary>
        ///     Gets or sets the gravity.
        /// </summary>
        public static float Gravity
        {
            get { return NativeGetGravity(); }
            set { NativeSetGravity(value); }
        }

        /// <summary>
        ///     Disables the name tag line of sight test.
        /// </summary>
        public virtual void DisableNameTagLOS()
        {
            NativeDisableNameTagLOS();
        }

        /// <summary>
        ///     Set the name of the game mode, which appears in the server browser.
        /// </summary>
        /// <param name="text">GameMode name.</param>
        public virtual void SetGameModeText(string text)
        {
            NativeSetGameModeText(text);
        }

        /// <summary>
        ///     A function that can be used in <see cref="OnInitialized" /> to enable or disable the players markers,
        ///     which would normally be shown on the radar. If you want to change the marker settings at some other point in the
        ///     gamemode, have a look at <see cref="GtaPlayer.SetPlayerMarker" /> which does exactly that.
        /// </summary>
        /// <param name="mode">The mode you want to use.</param>
        public virtual void ShowPlayerMarkers(PlayerMarkersMode mode)
        {
            NativeShowPlayerMarkers((int)mode);
        }

        /// <summary>
        ///     Toggle the drawing of player name tags, health bars and armor bars above players.
        /// </summary>
        /// <param name="show">False to disable, True to enable.</param>
        public virtual void ShowNameTags(bool show)
        {
            NativeShowNameTags(show);
        }

        /// <summary>
        ///     Uses standard player walking animation (animation of CJ) instead of custom animations for every skin (e.g. skating
        ///     for skater skins).
        /// </summary>
        public virtual void UsePlayerPedAnimations()
        {
            UsePlayerPedAnims();
        }

        /// <summary>
        ///     Enable friendly fire for team vehicles.
        /// </summary>
        /// <remarks>
        ///     Players will be unable to damage teammates' vehicles (<see cref="GtaPlayer.Team" /> must be used!)
        /// </remarks>
        public virtual void EnableVehicleFriendlyFire()
        {
            NativeEnableVehicleFriendlyFire();
        }

        /// <summary>
        ///     Set the maximum distance to display the names of players.
        /// </summary>
        /// <param name="distance">The distance to set.</param>
        public virtual void SetNameTagDrawDistance(float distance)
        {
            NativeSetNameTagDrawDistance(distance);
        }

        /// <summary>
        ///     Disable all the interior entrances and exits in the game (the yellow arrows at doors).
        /// </summary>
        public virtual void DisableInteriorEnterExits()
        {
            NativeDisableInteriorEnterExits();
        }

        /// <summary>
        ///     This function is used to change the amount of teams used in the game mode. It has no obvious way of being used, but
        ///     can help to indicate the number of teams used for better (more effective) internal handling. This function should
        ///     only be used in the <see cref="OnInitialized" /> callback.
        /// </summary>
        /// <remarks>
        ///     You can pass 2 billion here if you like, this function has no effect at all.
        /// </remarks>
        /// <param name="count">Number of teams the gamemode knows.</param>
        public virtual void SetTeamCount(int count)
        {
            NativeSetTeamCount(count);
        }

        /// <summary>
        ///     Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.
        /// </summary>
        /// <param name="modelid">The skin which the player will spawn with.</param>
        /// <param name="position">The coordinate of the spawn point of this class.</param>
        /// <param name="zAngle">The direction in which the player should face after spawning.</param>
        /// <param name="weapon1">The first spawn-weapon for the player.</param>
        /// <param name="weapon1Ammo">The amount of ammunition for the primary spawn weapon.</param>
        /// <param name="weapon2">The second spawn-weapon for the player.</param>
        /// <param name="weapon2Ammo">The amount of ammunition for the second spawn weapon.</param>
        /// <param name="weapon3">The third spawn-weapon for the player.</param>
        /// <param name="weapon3Ammo">The amount of ammunition for the third spawn weapon.</param>
        /// <returns>
        ///     The ID of the class which was just added. 300 if the class limit (300) was reached. The highest possible class
        ///     ID is 299.
        /// </returns>
        public virtual int AddPlayerClass(int modelid, Vector3 position, float zAngle, Weapon weapon1, int weapon1Ammo,
            Weapon weapon2, int weapon2Ammo, Weapon weapon3, int weapon3Ammo)
        {
            return NativeAddPlayerClass(modelid, position.X, position.Y, position.Z, zAngle, (int) weapon1, weapon1Ammo,
                (int) weapon2, weapon2Ammo, (int) weapon3, weapon3Ammo);
        }

        /// <summary>
        ///     Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.
        /// </summary>
        /// <param name="modelid">The skin which the player will spawn with.</param>
        /// <param name="position">The coordinate of the spawn point of this class.</param>
        /// <param name="zAngle">The direction in which the player should face after spawning.</param>
        /// <param name="weapon1">The first spawn-weapon for the player.</param>
        /// <param name="weapon1Ammo">The amount of ammunition for the primary spawn weapon.</param>
        /// <param name="weapon2">The second spawn-weapon for the player.</param>
        /// <param name="weapon2Ammo">The amount of ammunition for the second spawn weapon.</param>
        /// <returns>
        ///     The ID of the class which was just added. 300 if the class limit (300) was reached. The highest possible class
        ///     ID is 299.
        /// </returns>
        public virtual int AddPlayerClass(int modelid, Vector3 position, float zAngle, Weapon weapon1, int weapon1Ammo,
            Weapon weapon2, int weapon2Ammo)
        {
            return AddPlayerClass(modelid, position, zAngle, weapon1, weapon1Ammo, weapon2, weapon2Ammo, Weapon.None, 0);
        }

        /// <summary>
        ///     Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.
        /// </summary>
        /// <param name="modelid">The skin which the player will spawn with.</param>
        /// <param name="position">The coordinate of the spawn point of this class.</param>
        /// <param name="zAngle">The direction in which the player should face after spawning.</param>
        /// <param name="weapon">The spawn-weapon for the player.</param>
        /// <param name="weaponAmmo">The amount of ammunition for the spawn weapon.</param>
        /// <returns>
        ///     The ID of the class which was just added. 300 if the class limit (300) was reached. The highest possible class
        ///     ID is 299.
        /// </returns>
        public virtual int AddPlayerClass(int modelid, Vector3 position, float zAngle, Weapon weapon, int weaponAmmo)
        {
            return AddPlayerClass(modelid, position, zAngle, weapon, weaponAmmo, Weapon.None, 0, Weapon.None, 0);
        }

        /// <summary>
        ///     Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.
        /// </summary>
        /// <param name="modelid">The skin which the player will spawn with.</param>
        /// <param name="position">The coordinate of the spawn point of this class.</param>
        /// <param name="zAngle">The direction in which the player should face after spawning.</param>
        /// <returns>
        ///     The ID of the class which was just added. 300 if the class limit (300) was reached. The highest possible class
        ///     ID is 299.
        /// </returns>
        public virtual int AddPlayerClass(int modelid, Vector3 position, float zAngle)
        {
            return AddPlayerClass(modelid, position, zAngle, Weapon.None, 0, Weapon.None, 0, Weapon.None, 0);
        }

        /// <summary>
        ///     Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.
        /// </summary>
        /// <param name="teamid">The team you want the player to spawn in.</param>
        /// <param name="modelid">The skin which the player will spawn with.</param>
        /// <param name="position">The coordinate of the class' spawn position.</param>
        /// <param name="zAngle">The direction in which the player will face after spawning.</param>
        /// <param name="weapon1">The first spawn-weapon for the player.</param>
        /// <param name="weapon1Ammo">The amount of ammunition for the first spawn weapon.</param>
        /// <param name="weapon2">The second spawn-weapon for the player.</param>
        /// <param name="weapon2Ammo">The amount of ammunition for the second spawn weapon.</param>
        /// <param name="weapon3">The third spawn-weapon for the player.</param>
        /// <param name="weapon3Ammo">The amount of ammunition for the third spawn weapon.</param>
        /// <returns>The ID of the class that was just created.</returns>
        public virtual int AddPlayerClass(int teamid, int modelid, Vector3 position, float zAngle, Weapon weapon1,
            int weapon1Ammo, Weapon weapon2, int weapon2Ammo, Weapon weapon3, int weapon3Ammo)
        {
            return NativeAddPlayerClassEx(teamid, modelid, position.X, position.Y, position.Z, zAngle, (int) weapon1,
                weapon1Ammo, (int) weapon2, weapon2Ammo, (int) weapon3, weapon3Ammo);
        }

        /// <summary>
        ///     Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.
        /// </summary>
        /// <param name="teamid">The team you want the player to spawn in.</param>
        /// <param name="modelid">The skin which the player will spawn with.</param>
        /// <param name="position">The coordinate of the class' spawn position.</param>
        /// <param name="zAngle">The direction in which the player will face after spawning.</param>
        /// <param name="weapon1">The first spawn-weapon for the player.</param>
        /// <param name="weapon1Ammo">The amount of ammunition for the first spawn weapon.</param>
        /// <param name="weapon2">The second spawn-weapon for the player.</param>
        /// <param name="weapon2Ammo">The amount of ammunition for the second spawn weapon.</param>
        /// <returns>The ID of the class that was just created.</returns>
        public virtual int AddPlayerClass(int teamid, int modelid, Vector3 position, float zAngle, Weapon weapon1,
            int weapon1Ammo, Weapon weapon2, int weapon2Ammo)
        {
            return AddPlayerClass(teamid, modelid, position, zAngle, weapon1, weapon1Ammo, weapon2, weapon2Ammo,
                Weapon.None, 0);
        }

        /// <summary>
        ///     Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.
        /// </summary>
        /// <param name="teamid">The team you want the player to spawn in.</param>
        /// <param name="modelid">The skin which the player will spawn with.</param>
        /// <param name="position">The coordinate of the class' spawn position.</param>
        /// <param name="zAngle">The direction in which the player will face after spawning.</param>
        /// <param name="weapon">The spawn-weapon for the player.</param>
        /// <param name="weaponAmmo">The amount of ammunition for the spawn weapon.</param>
        /// <returns>The ID of the class that was just created.</returns>
        public virtual int AddPlayerClass(int teamid, int modelid, Vector3 position, float zAngle, Weapon weapon,
            int weaponAmmo)
        {
            return AddPlayerClass(teamid, modelid, position, zAngle, weapon, weaponAmmo, Weapon.None, 0, Weapon.None, 0);
        }

        /// <summary>
        ///     Adds a class to class selection. Classes are used so players may spawn with a skin of their choice.
        /// </summary>
        /// <param name="teamid">The team you want the player to spawn in.</param>
        /// <param name="modelid">The skin which the player will spawn with.</param>
        /// <param name="position">The coordinate of the class' spawn position.</param>
        /// <param name="zAngle">The direction in which the player will face after spawning.</param>
        /// <returns>The ID of the class that was just created.</returns>
        public virtual int AddPlayerClass(int teamid, int modelid, Vector3 position, float zAngle)
        {
            return AddPlayerClass(teamid, modelid, position, zAngle, Weapon.None, 0, Weapon.None, 0, Weapon.None, 0);
        }

        /// <summary>
        ///     Enables or disables stunt bonuses for all players.
        /// </summary>
        /// <param name="enable">True to enable stunt bonuses, False to disable.</param>
        public virtual void EnableStuntBonusForAll(bool enable)
        {
            NativeEnableStuntBonusForAll(enable);
        }

        /// <summary>
        ///     Set a radius limitation for the chat. Only players at a certain distance from the player will see their message in
        ///     the chat. Also changes the distance at which a player can see other players on the map at the same distance.
        /// </summary>
        /// <param name="chatRadius">Radius limit.</param>
        public virtual void LimitGlobalChatRadius(float chatRadius)
        {
            NativeLimitGlobalChatRadius(chatRadius);
        }

        /// <summary>
        ///     Set the player marker radius.
        /// </summary>
        /// <param name="markerRadius">The radius that markers will show at.</param>
        public virtual void LimitPlayerMarkerRadius(float markerRadius)
        {
            NativeLimitPlayerMarkerRadius(markerRadius);
        }

        /// <summary>
        ///     Use this function before any player connects (<see cref="OnInitialized" />) to tell all clients that the
        ///     script will control vehicle engines and lights. This prevents the game automatically turning the engine on/off when
        ///     players enter/exit vehicles and headlights automatically coming on when it is dark.
        /// </summary>
        public virtual void ManualVehicleEngineAndLights()
        {
            NativeManualVehicleEngineAndLights();
        }

        /// <summary>
        ///     Ends and restarts the game mode.
        /// </summary>
        public virtual void Exit()
        {
            GameModeExit();
        }

        /// <summary>
        ///     Toggle whether the usage of weapons in interiors is allowed or not.
        /// </summary>
        /// <param name="allow">True to enable weapons in interiors (enabled by default), False to disable weapons in interiors.</param>
        public virtual void AllowInteriorWeapons(bool allow)
        {
            NativeAllowInteriorWeapons(allow);
        }

        /// <summary>
        ///     With this function you can enable or disable tire popping.
        /// </summary>
        /// <param name="enable">True to enable, False to disable tire popping.</param>
        public virtual void EnableTirePopping(bool enable)
        {
            NativeEnableTirePopping(enable);
        }

        /// <summary>
        ///     Sends an RCON command.
        /// </summary>
        /// <param name="command">The RCON command to be executed.</param>
        public virtual void SendRconCommand(string command)
        {
            Server.SendRconCommand(command);
        }
    }
}