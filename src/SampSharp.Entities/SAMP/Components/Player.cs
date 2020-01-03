// SampSharp
// Copyright 2019 Tim Potze
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
using System.Linq;
using SampSharp.Entities.SAMP.Definitions;
using SampSharp.Entities.SAMP.NativeComponents;

namespace SampSharp.Entities.SAMP.Components
{
    /// <summary>
    /// Represents a component which provides the data and functionality of a player.
    /// </summary>
    public class Player : Component
    {
        #region Players properties

        /// <summary>
        /// Gets or sets the name of this player.
        /// </summary>
        public virtual string Name
        {
            get
            {
                GetComponent<NativePlayer>().GetPlayerName(out var name, SampLimits.MaxPlayerNameLength);
                return name;
            }
            set => GetComponent<NativePlayer>().SetPlayerName(value);
        }

        /// <summary>
        /// Gets or sets the facing angle of this player.
        /// </summary>
        public virtual float Angle
        {
            get
            {
                GetComponent<NativePlayer>().GetPlayerFacingAngle(out var angle);
                return angle;
            }
            set => GetComponent<NativePlayer>().SetPlayerFacingAngle(value);
        }

        /// <summary>
        /// Gets or sets the interior of this player.
        /// </summary>
        public virtual int Interior
        {
            get => GetComponent<NativePlayer>().GetPlayerInterior();
            set => GetComponent<NativePlayer>().SetPlayerInterior(value);
        }

        /// <summary>
        /// Gets or sets the virtual world of this player.
        /// </summary>
        public virtual int VirtualWorld
        {
            get => GetComponent<NativePlayer>().GetPlayerVirtualWorld();
            set => GetComponent<NativePlayer>().SetPlayerVirtualWorld(value);
        }

        /// <summary>
        /// Gets or sets the health of this player.
        /// </summary>
        public virtual float Health
        {
            get
            {
                GetComponent<NativePlayer>().GetPlayerHealth(out var health);
                return health;
            }
            set => GetComponent<NativePlayer>().SetPlayerHealth(value);
        }

        /// <summary>
        /// Gets or sets the armor of this player.
        /// </summary>
        public virtual float Armour
        {
            get
            {
                GetComponent<NativePlayer>().GetPlayerArmour(out var armour);
                return armour;
            }
            set => GetComponent<NativePlayer>().SetPlayerArmour(value);
        }

        /// <summary>
        /// Gets the ammo of the Weapon this player is currently holding.
        /// </summary>
        public virtual int WeaponAmmo => GetComponent<NativePlayer>().GetPlayerAmmo();

        /// <summary>
        /// Gets the WeaponState of the Weapon this player is currently holding.
        /// </summary>
        public virtual WeaponState WeaponState => (WeaponState) GetComponent<NativePlayer>().GetPlayerWeaponState();

        /// <summary>
        /// Gets the Weapon this player is currently holding.
        /// </summary>
        public virtual Weapon Weapon => (Weapon) GetComponent<NativePlayer>().GetPlayerWeapon();

        /// <summary>
        /// Gets the Player this player is aiming at.
        /// </summary>
        public virtual Entity TargetPlayer
        {
            get
            {
                var id = GetComponent<NativePlayer>().GetPlayerTargetPlayer();
                return id == NativePlayer.InvalidId ? null : Entity.Manager.Get(SampEntities.GetPlayerId(id));
            }
        }

        /// <summary>
        /// Gets or sets the team this player is in.
        /// </summary>
        public virtual int Team
        {
            get => GetComponent<NativePlayer>().GetPlayerTeam();
            set => GetComponent<NativePlayer>().SetPlayerTeam(value);
        }

        /// <summary>
        /// Gets or sets the score of this player.
        /// </summary>
        public virtual int Score
        {
            get => GetComponent<NativePlayer>().GetPlayerScore();
            set => GetComponent<NativePlayer>().SetPlayerScore(value);
        }

        /// <summary>
        /// Gets or sets the drunkenness level of this player.
        /// </summary>
        public virtual int DrunkLevel
        {
            get => GetComponent<NativePlayer>().GetPlayerDrunkLevel();
            set => GetComponent<NativePlayer>().SetPlayerDrunkLevel(value);
        }

        /// <summary>
        /// Gets or sets the Color of this player.
        /// </summary>
        public virtual Color Color
        {
            get => new Color(GetComponent<NativePlayer>().GetPlayerColor());
            set => GetComponent<NativePlayer>().SetPlayerColor(value);
        }

        /// <summary>
        /// Gets or sets the skin of this player.
        /// </summary>
        public virtual int Skin
        {
            get => GetComponent<NativePlayer>().GetPlayerSkin();
            set => GetComponent<NativePlayer>().SetPlayerSkin(value);
        }

        /// <summary>
        /// Gets or sets the money of this player.
        /// </summary>
        public virtual int Money
        {
            get => GetComponent<NativePlayer>().GetPlayerMoney();
            set
            {
                GetComponent<NativePlayer>().ResetPlayerMoney();
                GetComponent<NativePlayer>().GivePlayerMoney(value);
            }
        }

        /// <summary>
        /// Gets the state of this player.
        /// </summary>
        public virtual PlayerState State => (PlayerState) GetComponent<NativePlayer>().GetPlayerState();

        /// <summary>
        /// Gets the IP of this player.
        /// </summary>
        public virtual string Ip
        {
            get
            {
                GetComponent<NativePlayer>().GetPlayerIp(out var ip, 16);
                return ip;
            }
        }

        /// <summary>
        /// Gets the ping of this player.
        /// </summary>
        public virtual int Ping => GetComponent<NativePlayer>().GetPlayerPing();

        /// <summary>
        /// Gets or sets the wanted level of this player.
        /// </summary>
        public virtual int WantedLevel
        {
            get => GetComponent<NativePlayer>().GetPlayerWantedLevel();
            set => GetComponent<NativePlayer>().SetPlayerWantedLevel(value);
        }

        /// <summary>
        /// Gets or sets the FightStyle of this player.
        /// </summary>
        public virtual FightStyle FightStyle
        {
            get => (FightStyle) GetComponent<NativePlayer>().GetPlayerFightingStyle();
            set => GetComponent<NativePlayer>().SetPlayerFightingStyle((int) value);
        }

        /// <summary>
        /// Gets or sets the velocity of this player.
        /// </summary>
        public virtual Vector3 Velocity
        {
            get
            {
                GetComponent<NativePlayer>().GetPlayerVelocity(out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
            set => GetComponent<NativePlayer>().SetPlayerVelocity(value.X, value.Y, value.Z);
        }

        /// <summary>
        /// Gets the vehicle seat this player sits on.
        /// </summary>
        public virtual int VehicleSeat => GetComponent<NativePlayer>().GetPlayerVehicleSeat();

        /// <summary>
        /// Gets the index of the animation this player is playing.
        /// </summary>
        public virtual int AnimationIndex => GetComponent<NativePlayer>().GetPlayerAnimationIndex();

        /// <summary>
        /// Gets or sets the SpecialAction of this player.
        /// </summary>
        public virtual SpecialAction SpecialAction
        {
            get => (SpecialAction) GetComponent<NativePlayer>().GetPlayerSpecialAction();
            set => GetComponent<NativePlayer>().SetPlayerSpecialAction((int) value);
        }

        /// <summary>
        /// Gets or sets the position of the camera of this player.
        /// </summary>
        public virtual Vector3 CameraPosition
        {
            get
            {
                GetComponent<NativePlayer>().GetPlayerCameraPos(out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
            set => GetComponent<NativePlayer>().SetPlayerCameraPos(value.X, value.Y, value.Z);
        }

        /// <summary>
        /// Gets the front Vector3 of this player's camera.
        /// </summary>
        public virtual Vector3 CameraFrontVector
        {
            get
            {
                GetComponent<NativePlayer>().GetPlayerCameraFrontVector(out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
        }

        /// <summary>
        /// Gets the mode of this player's camera.
        /// </summary>
        public virtual CameraMode CameraMode => (CameraMode) GetComponent<NativePlayer>().GetPlayerCameraMode();

        /// <summary>
        /// Gets the Actor this player is aiming at.
        /// </summary>
        public virtual Entity TargetActor
        {
            get
            {
                var id = GetComponent<NativePlayer>().GetPlayerTargetActor();
                return id == NativeActor.InvalidId ? null : Entity.Manager.Get(SampEntities.GetActorId(id));
            }
        }

        /// <summary>
        /// Gets the GlobalObject the camera of this player is pointing at.
        /// </summary>
        public virtual Entity CameraTargetGlobalObject
        {
            get
            {
                var id = GetComponent<NativePlayer>().GetPlayerCameraTargetObject();
                return id == NativeObject.InvalidId ? null : Entity.Manager.Get(SampEntities.GetObjectId(id));
            }
        }

        /// <summary>
        /// Gets the PlayerObject the camera of this player is pointing at.
        /// </summary>
        public virtual Entity CameraTargetPlayerObject
        {
            get
            {
                var id = GetComponent<NativePlayer>().GetPlayerCameraTargetObject();
                return id == NativePlayerObject.InvalidId
                    ? null
                    : Entity.Manager.Get(SampEntities.GetPlayerObjectId(Entity.Id, id));
            }
        }

        /// <summary>
        /// Gets the GtaVehicle the camera of this player is pointing at.
        /// </summary>
        public virtual Entity CameraTargetVehicle
        {
            get
            {
                var id = GetComponent<NativePlayer>().GetPlayerCameraTargetVehicle();
                return id == NativeVehicle.InvalidId ? null : Entity.Manager.Get(SampEntities.GetVehicleId(id));
            }
        }

        /// <summary>
        /// Gets the GtaPlayer the camera of this player is pointing at.
        /// </summary>
        public virtual Entity CameraTargetPlayer
        {
            get
            {
                var id = GetComponent<NativePlayer>().GetPlayerCameraTargetPlayer();
                return id == NativePlayer.InvalidId ? null : Entity.Manager.Get(SampEntities.GetPlayerId(id));
            }
        }

        /// <summary>
        /// Gets the GtaPlayer the camera of this player is pointing at.
        /// </summary>
        public virtual Entity CameraTargetActor
        {
            get
            {
                var id = GetComponent<NativePlayer>().GetPlayerCameraTargetActor();
                return id == NativeActor.InvalidId ? null : Entity.Manager.Get(SampEntities.GetActorId(id));
            }
        }

        /// <summary>
        /// Gets whether this player is currently in any vehicle.
        /// </summary>
        public virtual bool InAnyVehicle => GetComponent<NativePlayer>().IsPlayerInAnyVehicle();

        /// <summary>
        /// Gets whether this player is in his checkpoint.
        /// </summary>
        public virtual bool InCheckpoint => GetComponent<NativePlayer>().IsPlayerInCheckpoint();

        /// <summary>
        /// Gets whether this player is in his race-checkpoint.
        /// </summary>
        public virtual bool InRaceCheckpoint => GetComponent<NativePlayer>().IsPlayerInRaceCheckpoint();

        /// <summary>
        /// Gets the Vehicle that this player is surfing.
        /// </summary>
        public virtual Entity SurfingVehicle
        {
            get
            {
                var id = GetComponent<NativePlayer>().GetPlayerSurfingVehicleID();
                return id == NativeVehicle.InvalidId ? null : Entity.Manager.Get(SampEntities.GetVehicleId(id));
            }
        }

        /// <summary>
        /// Gets the object that this player is surfing.
        /// </summary>
        public virtual Entity SurfingGlobalObject // TODO: Rename, maybe?
        {
            get
            {
                var id = GetComponent<NativePlayer>().GetPlayerSurfingObjectID();
                return id == NativeObject.InvalidId ? null : Entity.Manager.Get(SampEntities.GetObjectId(id));
            }
        }

        /// <summary>
        /// Gets the player object that this player is surfing.
        /// </summary>
        public virtual Entity SurfingPlayerObject
        {
            get
            {
                var id = GetComponent<NativePlayer>().GetPlayerSurfingObjectID();
                return id == NativeObject.InvalidId ? null : Entity.Manager.Get(SampEntities.GetObjectId(id));
            }
        }

        /// <summary>
        /// Gets the Vehicle this player is currently in.
        /// </summary>
        public virtual Entity Vehicle
        {
            get
            {
                var id = GetComponent<NativePlayer>().GetPlayerVehicleID(); //Returns 0, not NativeVehicle.InvalidId!
                return id == 0 ? null : Entity.Manager.Get(SampEntities.GetVehicleId(id));
            }
        }

        /// <summary>
        /// Gets whether this player is connected to the server.
        /// </summary>
        public virtual bool IsConnected => GetComponent<NativePlayer>().IsPlayerConnected();


        /// <summary>
        /// Gets or sets the rotation of this player.
        /// </summary>
        /// <remarks>
        /// Only the Z angle can be set!
        /// </remarks>
        public virtual Vector3 Rotation
        {
            get => new Vector3(0, 0, Angle);
            set => Angle = value.Z;
        }

        /// <summary>
        /// Gets or sets the position of this player.
        /// </summary>
        public virtual Vector3 Position
        {
            get
            {
                GetComponent<NativePlayer>().GetPlayerPos(out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
            set => GetComponent<NativePlayer>().SetPlayerPos(value.X, value.Y, value.Z);
        }

        #endregion

        #region SAMP properties

        /// <summary>
        /// Gets whether this player is an actual player or an NPC.
        /// </summary>
        public virtual bool IsNpc => GetComponent<NativePlayer>().IsPlayerNPC();

        /// <summary>
        /// Gets whether this player is logged into RCON.
        /// </summary>
        public virtual bool IsAdmin => GetComponent<NativePlayer>().IsPlayerAdmin();

        /// <summary>
        /// Gets a value indicating whether this player is alive.
        /// </summary>
        public virtual bool IsAlive
            => !new[] {PlayerState.None, PlayerState.Spectating, PlayerState.Wasted}.Contains(State);


        /// <summary>
        /// Gets this player's network stats and saves them into a string.
        /// </summary>
        [Obsolete("Use the properties ConnectionStatus, BytesReceived, BytesSent and others instead")]
        public virtual string NetworkStats
        {
            get
            {
                GetComponent<NativePlayer>().GetPlayerNetworkStats(out var stats, 256);
                return stats;
            }
        }

        /// <summary>
        /// Gets this player's game version.
        /// </summary>
        public virtual string Version
        {
            get
            {
                GetComponent<NativePlayer>().GetPlayerVersion(out var version, 64);
                return version;
            }
        }

        /// <summary>
        /// Gets this player's GPCI string.
        /// </summary>
        public virtual string Gpci
        {
            get
            {
                GetComponent<NativePlayer>().GPCI(out var result, 64);
                return result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this player is selecting a textdraw.
        /// </summary>
        public virtual bool IsSelectingTextDraw { get; private set; }

        /// <summary>
        /// Gets the amount of time (in milliseconds) that a player has been connected to the server for.
        /// </summary>
        public virtual int ConnectedTime => GetComponent<NativePlayer>().GetConnectedTime();

        /// <summary>
        /// Gets the number of messages the server has received from the player.
        /// </summary>
        public virtual int MessagesReceived => GetComponent<NativePlayer>().GetMessagesReceived();

        /// <summary>
        /// Gets the number of messages the player has received in the last second.
        /// </summary>
        public virtual int MessagesReceivedPerSecond => GetComponent<NativePlayer>().GetMessagesReceivedPerSecond();

        /// <summary>
        /// Gets the number of messages the server has sent to the player.
        /// </summary>
        public virtual int MessagesSent => GetComponent<NativePlayer>().GetMessagesSent();

        /// <summary>
        /// Get the amount of information (in bytes) that the server has sent to the player.
        /// </summary>
        public virtual int BytesReceived => GetComponent<NativePlayer>().GetBytesReceived();

        /// <summary>
        /// Get the amount of information (in bytes) that the server has received from the player.
        /// </summary>
        public virtual int BytesSent => GetComponent<NativePlayer>().GetBytesSent();

        /// <summary>
        /// Gets the packet loss percentage of a player. Packet loss means data the player is sending to the server is being
        /// lost (or vice-versa).
        /// </summary>
        /// <remarks>
        /// This value has been found to be currently unreliable. The value is not as expected when compared to the
        /// client, therefore this function should not be used as a packet loss kicker.
        /// </remarks>
        [Obsolete("This value is unreliable. See remarks for details.")]
        public virtual float PacketLossPercent => GetComponent<NativePlayer>().GetPacketLossPercent();

        /// <summary>
        /// Get a player's connection status.
        /// </summary>
        public virtual ConnectionStatus ConnectionStatus =>
            (ConnectionStatus) GetComponent<NativePlayer>().GetConnectionStatus();

        #endregion


        #region Players natives

        /// <summary>
        /// This function can be used to change the spawn information of a specific player. It allows you to automatically set
        /// someone's spawn weapons, their team, skin and spawn position, normally used in case of mini games or
        /// automatic-spawn systems.
        /// </summary>
        /// <param name="team">The Team-ID of the chosen player.</param>
        /// <param name="skin">The skin which the player will spawn with.</param>
        /// <param name="position">The player's spawn position.</param>
        /// <param name="rotation">The direction in which the player needs to be facing after spawning.</param>
        /// <param name="weapon1">The first spawn-weapon for the player.</param>
        /// <param name="weapon1Ammo">The amount of ammunition for the primary spawn-weapon.</param>
        /// <param name="weapon2">The second spawn-weapon for the player.</param>
        /// <param name="weapon2Ammo">The amount of ammunition for the second spawn-weapon.</param>
        /// <param name="weapon3">The third spawn-weapon for the player.</param>
        /// <param name="weapon3Ammo">The amount of ammunition for the third spawn-weapon.</param>
        public virtual void SetSpawnInfo(int team, int skin, Vector3 position, float rotation,
            Weapon weapon1 = Weapon.None,
            int weapon1Ammo = 0, Weapon weapon2 = Weapon.None, int weapon2Ammo = 0, Weapon weapon3 = Weapon.None,
            int weapon3Ammo = 0)
        {
            GetComponent<NativePlayer>().SetSpawnInfo(team, skin, position.X, position.Y, position.Z, rotation,
                (int) weapon1,
                weapon1Ammo,
                (int) weapon2, weapon2Ammo,
                (int) weapon3, weapon3Ammo);
        }

        /// <summary>
        /// (Re)Spawns a player.
        /// </summary>
        public virtual void Spawn()
        {
            GetComponent<NativePlayer>().SpawnPlayer();
        }

        /// <summary>
        /// Restore the camera to a place behind the player, after using a function like <see cref="CameraPosition" />.
        /// </summary>
        public virtual void PutCameraBehindPlayer()
        {
            GetComponent<NativePlayer>().SetCameraBehindPlayer();
        }

        /// <summary>
        /// This sets this player's position then adjusts the Player's z-coordinate to the nearest solid
        /// ground under the
        /// position.
        /// </summary>
        /// <param name="position">The position to move this player to.</param>
        public virtual void SetPositionFindZ(Vector3 position)
        {
            GetComponent<NativePlayer>().SetPlayerPosFindZ(position.X, position.Y, position.Z);
        }

        /// <summary>
        /// Check if this player is in range of a point.
        /// </summary>
        /// <param name="range">The furthest distance the player can be from the point to be in range.</param>
        /// <param name="point">The point to check the range to.</param>
        /// <returns>True if this player is in range of the point, otherwise False.</returns>
        public virtual bool IsInRangeOfPoint(float range, Vector3 point)
        {
            return GetComponent<NativePlayer>().IsPlayerInRangeOfPoint(range, point.X, point.Y, point.Z);
        }

        /// <summary>
        /// Calculate the distance between this player and a map coordinate.
        /// </summary>
        /// <param name="point">The point to calculate the distance from.</param>
        /// <returns>The distance between the player and the point as a float.</returns>
        public virtual float GetDistanceFromPoint(Vector3 point)
        {
            return GetComponent<NativePlayer>().GetPlayerDistanceFromPoint(point.X, point.Y, point.Z);
        }

        /// <summary>
        /// Checks if the specified <paramref name="player" /> is streamed in this player's client.
        /// </summary>
        /// <remarks>
        /// Players aren't streamed in on their own client, so if this player is the same as the other Player, it will return
        /// false!
        /// </remarks>
        /// <remarks>
        /// Players stream out if they are more than 150 meters away (see server.cfg - stream_distance)
        /// </remarks>
        /// <param name="player">The player to check is streamed in.</param>
        /// <returns>True if the other Player is streamed in for this player, False if not.</returns>
        public virtual bool IsPlayerStreamedIn(Entity player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            return GetComponent<NativePlayer>().IsPlayerStreamedIn(player.Id);
        }

        /// <summary>
        /// Set the ammo of this player's weapon.
        /// </summary>
        /// <param name="weapon">The weapon to set the ammo of.</param>
        /// <param name="ammo">The amount of ammo to set.</param>
        public virtual void SetAmmo(Weapon weapon, int ammo)
        {
            GetComponent<NativePlayer>().SetPlayerAmmo((int) weapon, ammo);
        }

        /// <summary>
        /// Give this player a <see cref="Weapon" /> with a specified amount of ammo.
        /// </summary>
        /// <param name="weapon">The Weapon to give to this player.</param>
        /// <param name="ammo">The amount of ammo to give to this player.</param>
        public virtual void GiveWeapon(Weapon weapon, int ammo)
        {
            GetComponent<NativePlayer>().GivePlayerWeapon((int) weapon, ammo);
        }


        /// <summary>
        /// Removes all weapons from this player.
        /// </summary>
        public virtual void ResetWeapons()
        {
            GetComponent<NativePlayer>().ResetPlayerWeapons();
        }

        /// <summary>
        /// Sets the armed weapon of this player.
        /// </summary>
        /// <param name="weapon">The weapon that the player should be armed with.</param>
        public virtual void SetArmedWeapon(Weapon weapon)
        {
            GetComponent<NativePlayer>().SetPlayerArmedWeapon((int) weapon);
        }

        /// <summary>
        /// Get the <see cref="Weapon" /> and ammo in this player's weapon slot.
        /// </summary>
        /// <param name="slot">The weapon slot to get data for (0-12).</param>
        /// <param name="weapon">The variable in which to store the weapon, passed by reference.</param>
        /// <param name="ammo">The variable in which to store the ammo, passed by reference.</param>
        public virtual void GetWeaponData(int slot, out Weapon weapon, out int ammo)
        {
            GetComponent<NativePlayer>().GetPlayerWeaponData(slot, out var weaponId, out ammo);
            weapon = (Weapon) weaponId;
        }

        /// <summary>
        /// Give money to this player.
        /// </summary>
        /// <param name="money">The amount of money to give this player. Use a minus value to take money.</param>
        public virtual void GiveMoney(int money)
        {
            GetComponent<NativePlayer>().GivePlayerMoney(money);
        }

        /// <summary>
        /// Reset this player's money to $0.
        /// </summary>
        public virtual void ResetMoney()
        {
            GetComponent<NativePlayer>().ResetPlayerMoney();
        }

        /// <summary>
        /// Check which keys this player is pressing.
        /// </summary>
        /// <remarks>
        /// Only the FUNCTION of keys can be detected; not actual keys. You can not detect if the player presses space, but you
        /// can detect if they press sprint (which can be mapped (assigned) to ANY key, but is space by default)).
        /// </remarks>
        /// <param name="keys">A set of bits containing this player's key states</param>
        /// <param name="upDown">Up or Down value, passed by reference.</param>
        /// <param name="leftRight">Left or Right value, passed by reference.</param>
        public virtual void GetKeys(out Keys keys, out int upDown, out int leftRight)
        {
            GetComponent<NativePlayer>().GetPlayerKeys(out var keysDown, out upDown, out leftRight);
            keys = (Keys) keysDown;
        }

        /// <summary>
        /// Sets the clock of this player to a specific value. This also changes the daytime. (night/day
        /// etc.)
        /// </summary>
        /// <param name="hour">Hour to set (0-23).</param>
        /// <param name="minutes">Minutes to set (0-59).</param>
        public virtual void SetTime(int hour, int minutes)
        {
            GetComponent<NativePlayer>().SetPlayerTime(hour, minutes);
        }

        /// <summary>
        /// Get this player's current game time. Set by <see cref="IServer.SetWorldTime" />, or by <see cref="ToggleClock" />.
        /// </summary>
        /// <param name="hour">The variable to store the hour in, passed by reference.</param>
        /// <param name="minutes">The variable to store the minutes in, passed by reference.</param>
        public virtual void GetTime(out int hour, out int minutes)
        {
            GetComponent<NativePlayer>().GetPlayerTime(out hour, out minutes);
        }

        /// <summary>
        /// Show/Hide the in-game clock (top right corner) for this player.
        /// </summary>
        /// <remarks>
        /// Time is not synced with other players!
        /// </remarks>
        /// <param name="toggle">True to show, False to hide.</param>
        public virtual void ToggleClock(bool toggle)
        {
            GetComponent<NativePlayer>().TogglePlayerClock(toggle);
        }

        /// <summary>
        /// Set this player's weather. If <see cref="ToggleClock" /> has been used to enable the clock,
        /// weather changes will
        /// interpolate (gradually change), otherwise will change instantly.
        /// </summary>
        /// <param name="weather">The weather to set.</param>
        public virtual void SetWeather(int weather)
        {
            GetComponent<NativePlayer>().SetPlayerWeather(weather);
        }

        /// <summary>
        /// Forces this player to go back to class selection.
        /// </summary>
        /// <remarks>
        /// The player will not return to class selection until they re-spawn. This can be achieved with
        /// <see cref="ToggleSpectating" />
        /// </remarks>
        public virtual void ForceClassSelection()
        {
            GetComponent<NativePlayer>().ForceClassSelection();
        }

        /// <summary>
        /// Display the cursor and allow this player to select a text draw.
        /// </summary>
        /// <param name="hoverColor">The color of the text draw when hovering over with mouse.</param>
        public virtual void SelectTextDraw(Color hoverColor)
        {
            IsSelectingTextDraw = true;
            GetComponent<NativePlayer>().SelectTextDraw(hoverColor);
        }

        /// <summary>
        /// Cancel text draw selection with the mouse for this player.
        /// </summary>
        public virtual void CancelSelectTextDraw()
        {
            IsSelectingTextDraw = false;
            GetComponent<NativePlayer>().CancelSelectTextDraw();
        }

        /// <summary>
        /// This function plays a crime report for this player - just like in single-player when CJ commits
        /// a
        /// crime.
        /// </summary>
        /// <param name="suspect">The suspect player which will be described in the crime report.</param>
        /// <param name="crime">The crime ID, which will be reported as a 10-code (i.e. 10-16 if 16 was passed as the crime ID).</param>
        public virtual void PlayCrimeReport(Entity suspect, int crime)
        {
            if (suspect == null) throw new ArgumentNullException(nameof(suspect));


            GetComponent<NativePlayer>().PlayCrimeReportForPlayer(suspect.Id, crime);
        }

        /// <summary>
        /// Play an 'audio stream' for this player. Normal audio files also work (e.g. MP3).
        /// </summary>
        /// <param name="url">
        /// The url to play. Valid formats are mp3 and ogg/vorbis. A link to a .pls (playlist) file will play
        /// that playlist.
        /// </param>
        /// <param name="position">The position at which to play the audio.</param>
        /// <param name="distance">The distance over which the audio will be heard.</param>
        public virtual void PlayAudioStream(string url, Vector3 position, float distance)
        {
            GetComponent<NativePlayer>()
                .PlayAudioStreamForPlayer(url, position.X, position.Y, position.Z, distance, true);
        }

        /// <summary>
        /// Play an 'audio stream' for this player. Normal audio files also work (e.g. MP3).
        /// </summary>
        /// <param name="url">
        /// The url to play. Valid formats are mp3 and ogg/vorbis. A link to a .pls (playlist) file will play
        /// that playlist.
        /// </param>
        public virtual void PlayAudioStream(string url)
        {
            GetComponent<NativePlayer>().PlayAudioStreamForPlayer(url, 0, 0, 0, 0, false);
        }

        /// <summary>
        /// Allows you to disable collisions between vehicles for a player.
        /// </summary>
        /// <param name="disable">if set to <c>true</c> disables the collision between vehicles.</param>
        public virtual void DisableRemoteVehicleCollisions(bool disable)
        {
            GetComponent<NativePlayer>().DisableRemoteVehicleCollisions(disable);
        }

        /// <summary>
        /// Toggles camera targeting functions for a player.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> the functionality is enabled.</param>
        public virtual void EnablePlayerCameraTarget(bool enable)
        {
            GetComponent<NativePlayer>().EnablePlayerCameraTarget(enable);
        }

        /// <summary>
        /// Stops the current audio stream for this player.
        /// </summary>
        public virtual void StopAudioStream()
        {
            GetComponent<NativePlayer>().StopAudioStreamForPlayer();
        }

        /// <summary>
        /// Loads or unloads an interior script for this player. (for example the Ammunation menu)
        /// </summary>
        /// <param name="shopName">The name of the shop, see <see cref="ShopName" /> for shop names.</param>
        public virtual void SetShopName(string shopName)
        {
            GetComponent<NativePlayer>().SetPlayerShopName(shopName);
        }

        /// <summary>
        /// Set the skill level of a certain weapon type for this player.
        /// </summary>
        /// <remarks>
        /// The skill parameter is NOT the weapon ID, it is the skill type.
        /// </remarks>
        /// <param name="skill">The <see cref="WeaponSkill" /> you want to set the skill of.</param>
        /// <param name="level">
        /// The skill level to set for that weapon, ranging from 0 to 999. (A level out of range will max it
        /// out)
        /// </param>
        public virtual void SetSkillLevel(WeaponSkill skill, int level)
        {
            GetComponent<NativePlayer>().SetPlayerSkillLevel((int) skill, level);
        }

        /// <summary>
        /// Attach an object to a specific bone on this player.
        /// </summary>
        /// <param name="index">The index (slot) to assign the object to (0-9).</param>
        /// <param name="modelId">The model to attach.</param>
        /// <param name="bone">The bone to attach the object to.</param>
        /// <param name="offset">offset for the object position.</param>
        /// <param name="rotation">rotation of the object.</param>
        /// <param name="scale">scale of the object.</param>
        /// <param name="materialColor1">The first object color to set.</param>
        /// <param name="materialColor2">The second object color to set.</param>
        /// <returns>True on success, False otherwise.</returns>
        public virtual bool SetAttachedObject(int index, int modelId, Bone bone, Vector3 offset, Vector3 rotation,
            Vector3 scale, Color materialColor1, Color materialColor2)
        {
            return GetComponent<NativePlayer>().SetPlayerAttachedObject(index, modelId, (int) bone, offset.X, offset.Y,
                offset.Z,
                rotation.X, rotation.Y, rotation.Z, scale.X, scale.Y, scale.Z,
                materialColor1.ToInteger(ColorFormat.ARGB), materialColor2.ToInteger(ColorFormat.ARGB));
        }

        /// <summary>
        /// Remove an attached object from this player.
        /// </summary>
        /// <param name="index">The index of the object to remove (set with <see cref="SetAttachedObject" />).</param>
        /// <returns>True on success, False otherwise.</returns>
        public virtual bool RemoveAttachedObject(int index)
        {
            return GetComponent<NativePlayer>().RemovePlayerAttachedObject(index);
        }

        /// <summary>
        /// Check if this player has an object attached in the specified index (slot).
        /// </summary>
        /// <param name="index">The index (slot) to check.</param>
        /// <returns>True if the slot is used, False otherwise.</returns>
        public virtual bool IsAttachedObjectSlotUsed(int index)
        {
            return GetComponent<NativePlayer>().IsPlayerAttachedObjectSlotUsed(index);
        }

        /// <summary>
        /// Enter edition mode for an attached object.
        /// </summary>
        /// <param name="index">The index (slot) of the attached object to edit.</param>
        /// <returns>True on success, False otherwise.</returns>
        public virtual bool DoEditAttachedObject(int index)
        {
            return GetComponent<NativePlayer>().EditAttachedObject(index);
        }

        /// <summary>
        /// Creates a chat bubble above this player's name tag.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="color">The text color.</param>
        /// <param name="drawDistance">The distance from where players are able to see the chat bubble.</param>
        /// <param name="expireTime">The time in milliseconds the bubble should be displayed for.</param>
        public virtual void SetChatBubble(string text, Color color, float drawDistance,
            int expireTime)
        {
            GetComponent<NativePlayer>()
                .SetPlayerChatBubble(text, color.ToInteger(ColorFormat.RGBA), drawDistance, expireTime);
        }

        /// <summary>
        /// Puts this player in a vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle for the player to be put in.</param>
        /// <param name="seatId">The ID of the seat to put the player in.</param>
        public virtual void PutInVehicle(Entity vehicle, int seatId)
        {
            // TODO: Ensure vehicle
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            GetComponent<NativePlayer>().PutPlayerInVehicle(vehicle.Id, seatId);
        }

        /// <summary>
        /// Puts this player in a vehicle as driver.
        /// </summary>
        /// <param name="vehicle">The vehicle for the player to be put in.</param>
        public virtual void PutInVehicle(Entity vehicle)
        {
            PutInVehicle(vehicle, 0);
        }

        /// <summary>
        /// Removes/ejects this player from his vehicle.
        /// </summary>
        /// <remarks>
        /// The exiting animation is not synced for other players. This function will not work when used in the
        /// OnPlayerEnterVehicle event, because the player isn't in the vehicle when the callback is called. Use the
        /// OnPlayerStateChanged event instead.
        /// </remarks>
        public virtual void RemoveFromVehicle()
        {
            GetComponent<NativePlayer>().RemovePlayerFromVehicle();
        }

        /// <summary>
        /// Toggles whether this player can control themselves, basically freezes them.
        /// </summary>
        /// <param name="toggle">False to freeze the player or True to unfreeze them.</param>
        public virtual void ToggleControllable(bool toggle)
        {
            GetComponent<NativePlayer>().TogglePlayerControllable(toggle);
        }

        /// <summary>
        /// Plays the specified sound for this player at a specific point.
        /// </summary>
        /// <param name="soundId">The sound to play.</param>
        /// <param name="point">Point for the sound to play at.</param>
        public virtual void PlaySound(int soundId, Vector3 point)
        {
            GetComponent<NativePlayer>().PlayerPlaySound(soundId, point.X, point.Y, point.Z);
        }

        /// <summary>
        /// Plays the specified sound for this player.
        /// </summary>
        /// <param name="soundId">The sound to play.</param>
        public virtual void PlaySound(int soundId)
        {
            GetComponent<NativePlayer>().PlayerPlaySound(soundId, 0, 0, 0);
        }

        /// <summary>
        /// Apply an animation to this player.
        /// </summary>
        /// <remarks>
        /// The <paramref name="forceSync" /> parameter, in most cases is not needed since players sync animations themselves.
        /// The <paramref name="forceSync" /> parameter can force all players who can see this player to play the animation
        /// regardless of whether the player is performing that animation. This is useful in circumstances where the player
        /// can't sync the animation themselves. For example, they may be paused.
        /// </remarks>
        /// <param name="animationLibrary">The name of the animation library in which the animation to apply is in.</param>
        /// <param name="animationName">The name of the animation, within the library specified.</param>
        /// <param name="fDelta">The speed to play the animation (use 4.1).</param>
        /// <param name="loop">Set to True for looping otherwise set to False for playing animation sequence only once.</param>
        /// <param name="lockX">
        /// Set to False to return player to original x position after animation is complete for moving
        /// animations. The opposite effect occurs if set to True.
        /// </param>
        /// <param name="lockY">
        /// Set to False to return player to original y position after animation is complete for moving
        /// animations. The opposite effect occurs if set to True.
        /// </param>
        /// <param name="freeze">Will freeze the player in position after the animation finishes.</param>
        /// <param name="time">Timer in milliseconds. For a never ending loop it should be 0.</param>
        /// <param name="forceSync">Set to <c>true</c> to force the player to sync animation with other players in all instances</param>
        public virtual void ApplyAnimation(string animationLibrary, string animationName, float fDelta, bool loop,
            bool lockX,
            bool lockY, bool freeze, int time, bool forceSync)
        {
            GetComponent<NativePlayer>()
                .ApplyAnimation(animationLibrary, animationName, fDelta, loop, lockX, lockY, freeze, time, forceSync);
        }

        /// <summary>
        /// Apply an animation to this player.
        /// </summary>
        /// <param name="animationLibrary">The name of the animation library in which the animation to apply is in.</param>
        /// <param name="animationName">The name of the animation, within the library specified.</param>
        /// <param name="fDelta">The speed to play the animation (use 4.1).</param>
        /// <param name="loop">Set to True for looping otherwise set to False for playing animation sequence only once.</param>
        /// <param name="lockX">
        /// Set to False to return player to original x position after animation is complete for moving
        /// animations. The opposite effect occurs if set to True.
        /// </param>
        /// <param name="lockY">
        /// Set to False to return player to original y position after animation is complete for moving
        /// animations. The opposite effect occurs if set to True.
        /// </param>
        /// <param name="freeze">Will freeze the player in position after the animation finishes.</param>
        /// <param name="time">Timer in milliseconds. For a never ending loop it should be 0.</param>
        public virtual void ApplyAnimation(string animationLibrary, string animationName, float fDelta, bool loop,
            bool lockX,
            bool lockY, bool freeze, int time)
        {
            GetComponent<NativePlayer>()
                .ApplyAnimation(animationLibrary, animationName, fDelta, loop, lockX, lockY, freeze, time, false);
        }

        /// <summary>
        /// Clears all animations for this player.
        /// </summary>
        /// <param name="forceSync">Specifies whether the animation should be shown to streamed in players.</param>
        public virtual void ClearAnimations(bool forceSync)
        {
            GetComponent<NativePlayer>().ClearAnimations(forceSync);
        }

        /// <summary>
        /// Clears all animations for this player.
        /// </summary>
        public virtual void ClearAnimations()
        {
            GetComponent<NativePlayer>().ClearAnimations(false);
        }

        /// <summary>
        /// Get the animation library/name this player is playing.
        /// </summary>
        /// <param name="animationLibrary">String variable that stores the animation library.</param>
        /// <param name="animationName">String variable that stores the animation name.</param>
        /// <returns>True on success, False otherwise.</returns>
        public virtual bool GetAnimationName(out string animationLibrary, out string animationName)
        {
            return GetComponent<NativePlayer>()
                .GetAnimationName(AnimationIndex, out animationLibrary, 64, out animationName, 64);
        }

        /// <summary>
        /// Sets a checkpoint (red circle) for this player. Also shows a red blip on the radar.
        /// </summary>
        /// <remarks>
        /// Checkpoints created on server-created objects will appear down on the 'real' ground, but will still function
        /// correctly.
        /// There is no fix available for this issue. A pickup can be used instead.
        /// </remarks>
        /// <param name="point">The point to set the checkpoint at.</param>
        /// <param name="size">The size of the checkpoint.</param>
        public virtual void SetCheckpoint(Vector3 point, float size)
        {
            GetComponent<NativePlayer>().SetPlayerCheckpoint(point.X, point.Y, point.Z, size);
        }

        /// <summary>
        /// Disable any initialized checkpoints for this player.
        /// </summary>
        public virtual void DisableCheckpoint()
        {
            GetComponent<NativePlayer>().DisablePlayerCheckpoint();
        }

        /// <summary>
        /// Creates a race checkpoint. When this player enters it, EnterRaceCheckpoint event is called.
        /// </summary>
        /// <param name="type">Type of checkpoint.</param>
        /// <param name="point">The point to set the checkpoint at.</param>
        /// <param name="nextPosition">Coordinates of the next point, for the arrow facing direction.</param>
        /// <param name="size">Length (diameter) of the checkpoint</param>
        public virtual void SetRaceCheckpoint(CheckpointType type, Vector3 point, Vector3 nextPosition, float size)
        {
            GetComponent<NativePlayer>().SetPlayerRaceCheckpoint((int) type, point.X, point.Y, point.Z, nextPosition.X,
                nextPosition.Y,
                nextPosition.Z, size);
        }

        /// <summary>
        /// Disable any initialized race checkpoints for this player.
        /// </summary>
        public virtual void DisableRaceCheckpoint()
        {
            GetComponent<NativePlayer>().DisablePlayerRaceCheckpoint();
        }

        /// <summary>
        /// Set the world boundaries for this player - players can not go out of the boundaries.
        /// </summary>
        /// <remarks>
        /// You can reset the player world bounds by setting the parameters to 20000.0000, -20000.0000, 20000.0000,
        /// -20000.0000.
        /// </remarks>
        /// <param name="xMax">The maximum X coordinate the player can go to.</param>
        /// <param name="xMin">The minimum X coordinate the player can go to.</param>
        /// <param name="yMax">The maximum Y coordinate the player can go to.</param>
        /// <param name="yMin">The minimum Y coordinate the player can go to.</param>
        public virtual void SetWorldBounds(float xMax, float xMin, float yMax, float yMin)
        {
            GetComponent<NativePlayer>().SetPlayerWorldBounds(xMax, xMin, yMax, yMin);
        }

        /// <summary>
        /// Change the color of this player's name tag and radar blip for another Player.
        /// </summary>
        /// <param name="player">The player whose color will be changed.</param>
        /// <param name="color">New color.</param>
        public virtual void SetPlayerMarker(Entity player, Color color)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            GetComponent<NativePlayer>().SetPlayerMarkerForPlayer(player.Id, color.ToInteger(ColorFormat.RGBA));
        }

        /// <summary>
        /// This functions allows you to toggle the drawing of player name tags, health bars and armor bars which display above
        /// their head. For use of a similar function like this on a global level, <see cref="IServer.ShowNameTags" />
        /// function.
        /// </summary>
        /// <remarks>
        /// <see cref="IServer.ShowNameTags" /> must be set to <c>true</c> to be able to show name tags with
        /// <see cref="ShowNameTagForPlayer" />.
        /// </remarks>
        /// <param name="player">The player whose name tag will be shown or hidden.</param>
        /// <param name="show">True to show name tag, False to hide name tag.</param>
        public virtual void ShowNameTagForPlayer(Entity player, bool show)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            GetComponent<NativePlayer>().ShowPlayerNameTagForPlayer(player.Id, show);
        }

        /// <summary>
        /// Set the direction this player's camera looks at. To be used in combination with
        /// <see cref="CameraPosition" />.
        /// </summary>
        /// <param name="point">The coordinates for this player's camera to look at.</param>
        /// <param name="cut">The style the camera-position changes.</param>
        public virtual void SetCameraLookAt(Vector3 point, CameraCut cut)
        {
            GetComponent<NativePlayer>().SetPlayerCameraLookAt(point.X, point.Y, point.Z, (int) cut);
        }

        /// <summary>
        /// Set the direction this player's camera looks at. To be used in combination with
        /// <see cref="CameraPosition" />.
        /// </summary>
        /// <param name="point">The coordinates for this player's camera to look at.</param>
        public virtual void SetCameraLookAt(Vector3 point)
        {
            SetCameraLookAt(point, CameraCut.Cut);
        }

        /// <summary>
        /// Move this player's camera from one position to another, within the set time.
        /// </summary>
        /// <param name="from">The position the camera should start to move from.</param>
        /// <param name="to">The position the camera should move to.</param>
        /// <param name="time">Time in milliseconds.</param>
        /// <param name="cut">The jump cut to use. Defaults to CameraCut.Cut. Set to CameraCut. Move for a smooth movement.</param>
        public virtual void InterpolateCameraPosition(Vector3 from, Vector3 to, int time, CameraCut cut)
        {
            GetComponent<NativePlayer>()
                .InterpolateCameraPos(from.X, from.Y, from.Z, to.X, to.Y, to.Z, time, (int) cut);
        }

        /// <summary>
        /// Interpolate this player's camera's 'look at' point between two coordinates with a set speed.
        /// </summary>
        /// <param name="from">The position the camera should start to move from.</param>
        /// <param name="to">The position the camera should move to.</param>
        /// <param name="time">Time in milliseconds to complete interpolation.</param>
        /// <param name="cut">The jump cut to use. Defaults to CameraCut.Cut (pointless). Set to CameraCut.Move for interpolation.</param>
        public virtual void InterpolateCameraLookAt(Vector3 from, Vector3 to, int time, CameraCut cut)
        {
            GetComponent<NativePlayer>()
                .InterpolateCameraLookAt(from.X, from.Y, from.Z, to.X, to.Y, to.Z, time, (int) cut);
        }

        /// <summary>
        /// Checks if this player is in a specific vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <returns>True if player is in the vehicle; False otherwise.</returns>
        public virtual bool IsInVehicle(Entity vehicle)
        {
            // TODO: ensure vehicle type
            return GetComponent<NativePlayer>().IsPlayerInVehicle(vehicle.Id);
        }

        /// <summary>
        /// Toggle stunt bonuses for this player.
        /// </summary>
        /// <param name="enable">True to enable stunt bonuses, False to disable them.</param>
        public virtual void EnableStuntBonus(bool enable)
        {
            GetComponent<NativePlayer>().EnableStuntBonusForPlayer(enable);
        }

        /// <summary>
        /// Toggle this player's spectate mode.
        /// </summary>
        /// <remarks>
        /// When the spectating is turned off, OnPlayerSpawn will automatically be called.
        /// </remarks>
        /// <param name="toggle">True to enable spectating and False to disable.</param>
        public virtual void ToggleSpectating(bool toggle)
        {
            GetComponent<NativePlayer>().TogglePlayerSpectating(toggle);
        }

        /// <summary>
        /// Makes this player spectate (watch) another player.
        /// </summary>
        /// <remarks>
        /// Order is CRITICAL! Ensure that you use <see cref="ToggleSpectating" /> before
        /// <see cref="SpectatePlayer(Entity,SpectateMode)" />.
        /// </remarks>
        /// <param name="targetPlayer">The Player that should be spectated.</param>
        /// <param name="mode">The mode to spectate with.</param>
        public virtual void SpectatePlayer(Entity targetPlayer, SpectateMode mode)
        {
            if (targetPlayer == null)
                throw new ArgumentNullException(nameof(targetPlayer));

            GetComponent<NativePlayer>().PlayerSpectatePlayer(targetPlayer.Id, (int) mode);
        }

        /// <summary>
        /// Makes this player spectate (watch) another player.
        /// </summary>
        /// <remarks>
        /// Order is CRITICAL! Ensure that you use <see cref="ToggleSpectating" /> before
        /// <see cref="SpectatePlayer(Entity,SpectateMode)" />.
        /// </remarks>
        /// <param name="targetPlayer">The Player that should be spectated.</param>
        public virtual void SpectatePlayer(Entity targetPlayer)
        {
            if (targetPlayer == null)
                throw new ArgumentNullException(nameof(targetPlayer));

            SpectatePlayer(targetPlayer, SpectateMode.Normal);
        }

        /// <summary>
        /// Sets this player to spectate another vehicle, i.e. see what its driver sees.
        /// </summary>
        /// <remarks>
        /// Order is CRITICAL! Ensure that you use <see cref="ToggleSpectating" /> before
        /// <see cref="SpectateVehicle(Entity,SpectateMode)" />.
        /// </remarks>
        /// <param name="targetVehicle">The vehicle to spectate.</param>
        /// <param name="mode">Spectate mode.</param>
        public virtual void SpectateVehicle(Entity targetVehicle, SpectateMode mode)
        {
            if (targetVehicle == null)
                throw new ArgumentNullException(nameof(targetVehicle));

            GetComponent<NativePlayer>().PlayerSpectateVehicle(targetVehicle.Id, (int) mode);
        }

        /// <summary>
        /// Sets this player to spectate another vehicle, i.e. see what its driver sees.
        /// </summary>
        /// <remarks>
        /// Order is CRITICAL! Ensure that you use <see cref="ToggleSpectating" /> before
        /// <see cref="SpectateVehicle(Entity,SpectateMode)" />.
        /// </remarks>
        /// <param name="targetVehicle">The vehicle to spectate.</param>
        public virtual void SpectateVehicle(Entity targetVehicle)
        {
            if (targetVehicle == null)
                throw new ArgumentNullException(nameof(targetVehicle));

            SpectateVehicle(targetVehicle, SpectateMode.Normal);
        }

        /// <summary>
        /// Starts recording this player's movements to a file, which can then be reproduced by an NPC.
        /// </summary>
        /// <param name="recordingType">The type of recording.</param>
        /// <param name="recordingName">
        /// Name of the file which will hold the recorded data. It will be saved in the scriptfiles folder, with an
        /// automatically added .rec extension.
        /// </param>
        public virtual void StartRecordingPlayerData(PlayerRecordingType recordingType, string recordingName)
        {
            GetComponent<NativePlayer>().StartRecordingPlayerData((int) recordingType, recordingName);
        }

        /// <summary>
        /// Stops all the recordings that had been started with <see cref="StartRecordingPlayerData" /> for this
        /// <see cref="Entity" />.
        /// </summary>
        public virtual void StopRecordingPlayerData()
        {
            GetComponent<NativePlayer>().StopRecordingPlayerData();
        }

        /// <summary>
        /// Retrieves the start and end (hit) position of the last bullet a player fired.
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="hitPosition">The hit position.</param>
        public virtual void GetLastShot(out Vector3 origin, out Vector3 hitPosition)
        {
            GetComponent<NativePlayer>()
                .GetPlayerLastShotVectors(out var ox, out var oy, out var oz, out var hx, out var hy, out var hz);

            origin = new Vector3(ox, oy, oz);
            hitPosition = new Vector3(hx, hy, hz);
        }

        #endregion

        #region SAMP natives

        /// <summary>
        /// This function sends a message to this player with a chosen color in the chat. The whole line in
        /// the chat box will be
        /// in the set color unless color embedding is used.
        /// </summary>
        /// <param name="color">The color of the message.</param>
        /// <param name="message">The text that will be displayed.</param>
        public virtual void SendClientMessage(Color color, string message)
        {
            if (message.Length > 144)
            {
                GetComponent<NativePlayer>()
                    .SendClientMessage(color.ToInteger(ColorFormat.RGBA), message.Substring(0, 144));
                SendClientMessage(color, message.Substring(144));
            }
            else
            {
                GetComponent<NativePlayer>().SendClientMessage(color.ToInteger(ColorFormat.RGBA), message);
            }
        }

        /// <summary>
        /// Kicks this player from the server. They will have to quit the game and re-connect if they wish
        /// to
        /// continue playing.
        /// </summary>
        public virtual void Kick()
        {
            GetComponent<NativePlayer>().Kick();
        }

        /// <summary>
        /// Ban this player. The ban will be IP-based, and be saved in the samp.ban file in the server's root directory.
        /// <see cref="Ban(string)" /> allows you to ban with a reason, while you can ban and unban IPs using the RCON banip and
        /// unbanip commands.
        /// </summary>
        public virtual void Ban()
        {
            GetComponent<NativePlayer>().Ban();
        }

        /// <summary>
        /// Ban this player with a reason.
        /// </summary>
        /// <param name="reason">The reason for the ban.</param>
        public virtual void Ban(string reason)
        {
            GetComponent<NativePlayer>().BanEx(reason);
        }

        /// <summary>
        /// This function sends a message to this player with a chosen color in the chat. The whole line in the chatbox will be in
        /// the set color unless color embedding is used.
        /// </summary>
        /// <param name="color">The color of the message.</param>
        /// <param name="messageFormat">The composite format string of the text that will be displayed (max 144 characters).</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public virtual void SendClientMessage(Color color, string messageFormat, params object[] args)
        {
            SendClientMessage(color, string.Format(messageFormat, args));
        }

        /// <summary>
        /// This function sends a message to this player in white in the chat. The whole line in the chat box will be in the set
        /// color unless color embedding is used.
        /// </summary>
        /// <param name="message">The text that will be displayed.</param>
        public virtual void SendClientMessage(string message)
        {
            SendClientMessage(Color.White, message);
        }

        /// <summary>
        /// This function sends a message to this player in white in the chat. The whole line in the chat box will be in the set
        /// color unless color embedding is used.
        /// </summary>
        /// <param name="messageFormat">The composite format string of the text that will be displayed (max 144 characters).</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public virtual void SendClientMessage(string messageFormat, params object[] args)
        {
            SendClientMessage(Color.White, string.Format(messageFormat, args));
        }

        /// <summary>
        /// Sends a message in the name the specified <paramref name="sender" /> to this player. The message will appear in the
        /// chat box but can only be seen by this player. The line will start with the the sender's name in their color, followed
        /// by the <paramref name="message" /> in white.
        /// </summary>
        /// <param name="sender">The player which has sent the message.</param>
        /// <param name="message">The message that will be sent.</param>
        public virtual void SendPlayerMessageToPlayer(Entity sender, string message)
        {
            // TODO: check sender is player
            if (sender == null)
                throw new ArgumentNullException(nameof(sender));

            GetComponent<NativePlayer>().SendPlayerMessageToPlayer(sender.Id, message);
        }

        /// <summary>
        /// Shows 'game text' (on-screen text) for a certain length of time for this player.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="time">The duration of the text being shown in milliseconds.</param>
        /// <param name="style">The style of text to be displayed.</param>
        public virtual void GameText(string text, int time, int style)
        {
            GetComponent<NativePlayer>().GameTextForPlayer(text, time, style);
        }

        /// <summary>
        /// Creates an explosion for a <see cref="Entity" />.
        /// Only the specific player will see explosion and feel its effects.
        /// This is useful when you want to isolate explosions from other players or to make them only appear in specific
        /// virtual worlds.
        /// </summary>
        /// <param name="position">The position of the explosion.</param>
        /// <param name="type">The explosion type.</param>
        /// <param name="radius">The radius of the explosion.</param>
        public virtual void CreateExplosion(Vector3 position, ExplosionType type, float radius)
        {
            GetComponent<NativePlayer>()
                .CreateExplosionForPlayer(position.X, position.Y, position.Z, (int) type, radius);
        }

        /// <summary>
        /// Adds a death to the kill feed on the right-hand side of the screen of this player.
        /// </summary>
        /// <param name="killer">The <see cref="Entity" /> that killer the <paramref name="killee" />.</param>
        /// <param name="killee">The <see cref="Entity" /> that has been killed.</param>
        /// <param name="weapon">The reason for this player's death.</param>
        public virtual void SendDeathMessage(Entity killer, Entity killee, Weapon weapon)
        {
            GetComponent<NativePlayer>()
                .SendDeathMessageToPlayer(killer?.Id ?? NativePlayer.InvalidId, killee?.Id ?? NativePlayer.InvalidId,
                    (int) weapon);
        }

        /// <summary>
        /// Attaches a player's camera to an object.
        /// </summary>
        /// <param name="object">The object to attach the camera to.</param>
        public virtual void AttachCameraToObject(Entity @object)
        {
            if (@object == null) throw new ArgumentNullException(nameof(@object));

            GetComponent<NativePlayer>().AttachCameraToObject(@object.Id);

            GetComponent<NativePlayer>().AttachCameraToPlayerObject(@object.GetComponent<NativePlayerObject>().Id);
        }

        /// <summary>
        /// Lets this player edit the specified <paramref name="object" />.
        /// </summary>
        /// <param name="object">The object to edit.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="object" /> is null.</exception>
        public virtual void Edit(Entity @object)
        {
            if (@object == null)
                throw new ArgumentNullException(nameof(@object));

            if (@object.GetComponent<NativeObject>() != null)
                GetComponent<NativePlayer>().EditObject(@object.Id);
            else if (@object.GetComponent<NativePlayerObject>() != null)
                GetComponent<NativePlayer>()
                    .EditPlayerObject(@object.GetComponent<NativePlayerObject>()
                        .Id); // Need just the player object component of the handle.
            else
                throw new ArgumentException("Target must be of type object or player object", nameof(@object));
        }

        /// <summary>
        /// Lets this player select an object.
        /// </summary>
        public void Select()
        {
            GetComponent<NativePlayer>().SelectObject();
        }

        #endregion
    }
}