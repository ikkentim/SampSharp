// SampSharp
// Copyright 2017 Tim Potze
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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World
{
    /// <summary>
    ///     Represents a SA-MP player.
    /// </summary>
    public partial class BasePlayer : IdentifiedPool<BasePlayer>, IWorldObject
    {
        internal bool IsNpcOverride { get; set; }

        /// <summary>
        ///     Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = 0xFFFF;

        /// <summary>
        ///     Maximum number of players which can exist.
        /// </summary>
        public const int Max = 1000;

        /// <summary>
        ///     Maximum number of attached objects attached to a player.
        /// </summary>
        public const int MaxAttachedObjects = 10;

        /// <summary>
        ///     Identifier indicating a player is not a member of any team.
        /// </summary>
        public const int NoTeam = 0xFF;

        /// <summary>
        ///     Maximum length of a player's name.
        /// </summary>
        public const int MaxNameLength = 24;

        /// <summary>
        ///     Maximum length of the text in a chat bubble.
        /// </summary>
        public const int MaxChatBubbleLength = 144;
        
        #region Constructors

        /// <summary>
        ///     Initializes the <see cref="BasePlayer" /> class.
        /// </summary>
        static BasePlayer()
        {
            DefaultClientMessageColor = Color.White;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BasePlayer" /> class.
        /// </summary>
        public BasePlayer()
        {
            PVars = new PVarCollection(this);
            Key = new KeyChangeHandlerSet();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the default color of a client message.
        /// </summary>
        public static Color DefaultClientMessageColor { get; set; }

        /// <summary>
        ///     Gets a collections of Player Variables of this Player.
        /// </summary>
        public PVarCollection PVars { get; }

        /// <summary>
        ///     Gets a set of KeyHandlers for different key states.
        /// </summary>
        public KeyChangeHandlerSet Key { get; }

        /// <summary>
        ///     Gets the size of the players pool.
        /// </summary>
        public static int PoolSize => PlayerInternal.Instance.GetPlayerPoolSize();

        #endregion

        #region Players properties

        /// <summary>
        ///     Gets or sets the name of this Player.
        /// </summary>
        public virtual string Name
        {
            get
            {
                PlayerInternal.Instance.GetPlayerName(Id, out var name, MaxNameLength);
                return name;
            }
            set => PlayerInternal.Instance.SetPlayerName(Id, value);
        }

        /// <summary>
        ///     Gets or sets the facing angle of this Player.
        /// </summary>
        public virtual float Angle
        {
            get
            {
                PlayerInternal.Instance.GetPlayerFacingAngle(Id, out var angle);
                return angle;
            }
            set => PlayerInternal.Instance.SetPlayerFacingAngle(Id, value);
        }

        /// <summary>
        ///     Gets or sets the interior of this Player.
        /// </summary>
        public virtual int Interior
        {
            get => PlayerInternal.Instance.GetPlayerInterior(Id);
            set => PlayerInternal.Instance.SetPlayerInterior(Id, value);
        }

        /// <summary>
        ///     Gets or sets the virtual world of this Player.
        /// </summary>
        public virtual int VirtualWorld
        {
            get => PlayerInternal.Instance.GetPlayerVirtualWorld(Id);
            set => PlayerInternal.Instance.SetPlayerVirtualWorld(Id, value);
        }

        /// <summary>
        ///     Gets or sets the health of this Player.
        /// </summary>
        public virtual float Health
        {
            get
            {
                PlayerInternal.Instance.GetPlayerHealth(Id, out var health);
                return health;
            }
            set => PlayerInternal.Instance.SetPlayerHealth(Id, value);
        }

        /// <summary>
        ///     Gets or sets the armor of this Player.
        /// </summary>
        public virtual float Armour
        {
            get
            {
                PlayerInternal.Instance.GetPlayerArmour(Id, out var armour);
                return armour;
            }
            set => PlayerInternal.Instance.SetPlayerArmour(Id, value);
        }

        /// <summary>
        ///     Gets the ammo of the Weapon this Player is currently holding.
        /// </summary>
        public virtual int WeaponAmmo => PlayerInternal.Instance.GetPlayerAmmo(Id);

        /// <summary>
        ///     Gets the WeaponState of the Weapon this Player is currently holding.
        /// </summary>
        public virtual WeaponState WeaponState => (WeaponState) PlayerInternal.Instance.GetPlayerWeaponState(Id);

        /// <summary>
        ///     Gets the Weapon this Player is currently holding.
        /// </summary>
        public virtual Weapon Weapon => (Weapon) PlayerInternal.Instance.GetPlayerWeapon(Id);

        /// <summary>
        ///     Gets the Player this Player is aiming at.
        /// </summary>
        public virtual BasePlayer TargetPlayer
        {
            get
            {
                var target = PlayerInternal.Instance.GetPlayerTargetPlayer(Id);
                return target == InvalidId ? null : Find(target);
            }
        }

        /// <summary>
        ///     Gets or sets the team this Player is in.
        /// </summary>
        public virtual int Team
        {
            get => PlayerInternal.Instance.GetPlayerTeam(Id);
            set => PlayerInternal.Instance.SetPlayerTeam(Id, value);
        }

        /// <summary>
        ///     Gets or sets the score of this Player.
        /// </summary>
        public virtual int Score
        {
            get => PlayerInternal.Instance.GetPlayerScore(Id);
            set => PlayerInternal.Instance.SetPlayerScore(Id, value);
        }

        /// <summary>
        ///     Gets or sets the drunkenness level of this Player.
        /// </summary>
        public virtual int DrunkLevel
        {
            get => PlayerInternal.Instance.GetPlayerDrunkLevel(Id);
            set => PlayerInternal.Instance.SetPlayerDrunkLevel(Id, value);
        }

        /// <summary>
        ///     Gets or sets the Color of this Player.
        /// </summary>
        public virtual Color Color
        {
            get => new Color(PlayerInternal.Instance.GetPlayerColor(Id));
            set => PlayerInternal.Instance.SetPlayerColor(Id, value);
        }

        /// <summary>
        ///     Gets or sets the skin of this Player.
        /// </summary>
        public virtual int Skin
        {
            get => PlayerInternal.Instance.GetPlayerSkin(Id);
            set => PlayerInternal.Instance.SetPlayerSkin(Id, value);
        }

        /// <summary>
        ///     Gets or sets the money of this Player.
        /// </summary>
        public virtual int Money
        {
            get => PlayerInternal.Instance.GetPlayerMoney(Id);
            set
            {
                PlayerInternal.Instance.ResetPlayerMoney(Id);
                PlayerInternal.Instance.GivePlayerMoney(Id, value);
            }
        }

        /// <summary>
        ///     Gets the state of this Player.
        /// </summary>
        public virtual PlayerState State => (PlayerState) PlayerInternal.Instance.GetPlayerState(Id);

        /// <summary>
        ///     Gets the IP of this Player.
        /// </summary>
        public virtual string IP
        {
            get
            {
                PlayerInternal.Instance.GetPlayerIp(Id, out var ip, 16);
                return ip;
            }
        }

        /// <summary>
        ///     Gets the ping of this Player.
        /// </summary>
        public virtual int Ping => PlayerInternal.Instance.GetPlayerPing(Id);

        /// <summary>
        ///     Gets or sets the wanted level of this Player.
        /// </summary>
        public virtual int WantedLevel
        {
            get => PlayerInternal.Instance.GetPlayerWantedLevel(Id);
            set => PlayerInternal.Instance.SetPlayerWantedLevel(Id, value);
        }

        /// <summary>
        ///     Gets or sets the FightStyle of this Player.
        /// </summary>
        public virtual FightStyle FightStyle
        {
            get => (FightStyle) PlayerInternal.Instance.GetPlayerFightingStyle(Id);
            set => PlayerInternal.Instance.SetPlayerFightingStyle(Id, (int) value);
        }

        /// <summary>
        ///     Gets or sets the velocity of this Player.
        /// </summary>
        public virtual Vector3 Velocity
        {
            get
            {
                PlayerInternal.Instance.GetPlayerVelocity(Id, out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
            set => PlayerInternal.Instance.SetPlayerVelocity(Id, value.X, value.Y, value.Z);
        }

        /// <summary>
        ///     Gets the vehicle seat this Player sits on.
        /// </summary>
        public virtual int VehicleSeat => PlayerInternal.Instance.GetPlayerVehicleSeat(Id);

        /// <summary>
        ///     Gets the index of the animation this Player is playing.
        /// </summary>
        public virtual int AnimationIndex => PlayerInternal.Instance.GetPlayerAnimationIndex(Id);

        /// <summary>
        ///     Gets or sets the SpecialAction of this Player.
        /// </summary>
        public virtual SpecialAction SpecialAction
        {
            get => (SpecialAction) PlayerInternal.Instance.GetPlayerSpecialAction(Id);
            set => PlayerInternal.Instance.SetPlayerSpecialAction(Id, (int) value);
        }

        /// <summary>
        ///     Gets or sets the position of the camera of this Players.
        /// </summary>
        public virtual Vector3 CameraPosition
        {
            get
            {
                PlayerInternal.Instance.GetPlayerCameraPos(Id, out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
            set => PlayerInternal.Instance.SetPlayerCameraPos(Id, value.X, value.Y, value.Z);
        }

        /// <summary>
        ///     Gets the front Vector3 of this Player's camera.
        /// </summary>
        public virtual Vector3 CameraFrontVector
        {
            get
            {
                PlayerInternal.Instance.GetPlayerCameraFrontVector(Id, out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
        }

        /// <summary>
        ///     Gets the mode of this Player's camera.
        /// </summary>
        public virtual CameraMode CameraMode => (CameraMode) PlayerInternal.Instance.GetPlayerCameraMode(Id);

        /// <summary>
        ///     Gets the Actor this Player is aiming at.
        /// </summary>
        public virtual Actor TargetActor
        {
            get
            {
                AssertNotDisposed();

                var id = PlayerInternal.Instance.GetPlayerTargetActor(Id);
                return id == Actor.InvalidId ? null : Actor.Find(id);
            }
        }

        /// <summary>
        ///     Gets the GlobalObject the camera of this player is pointing at.
        /// </summary>
        public virtual GlobalObject CameraTargetGlobalObject
        {
            get
            {
                AssertNotDisposed();

                var id = PlayerInternal.Instance.GetPlayerCameraTargetObject(Id);
                return id == GlobalObject.InvalidId ? null : GlobalObject.Find(id);
            }
        }

        /// <summary>
        ///     Gets the PlayerObject the camera of this player is pointing at.
        /// </summary>
        public virtual PlayerObject CameraTargetPlayerObject
        {
            get
            {
                AssertNotDisposed();

                var id = PlayerInternal.Instance.GetPlayerCameraTargetObject(Id);
                return id == PlayerObject.InvalidId ? null : PlayerObject.Find(this, id);
            }
        }

        /// <summary>
        ///     Gets the GtaVehicle the camera of this player is pointing at.
        /// </summary>
        public virtual BaseVehicle CameraTargetVehicle
        {
            get
            {
                AssertNotDisposed();

                var id = PlayerInternal.Instance.GetPlayerCameraTargetVehicle(Id);
                return id == BaseVehicle.InvalidId ? null : BaseVehicle.Find(id);
            }
        }

        /// <summary>
        ///     Gets the GtaPlayer the camera of this player is pointing at.
        /// </summary>
        public virtual BasePlayer CameraTargetPlayer
        {
            get
            {
                AssertNotDisposed();

                var id = PlayerInternal.Instance.GetPlayerCameraTargetPlayer(Id);
                return id == InvalidId ? null : Find(id);
            }
        }

        /// <summary>
        ///     Gets the GtaPlayer the camera of this player is pointing at.
        /// </summary>
        public virtual Actor CameraTargetActor
        {
            get
            {
                AssertNotDisposed();

                var id = PlayerInternal.Instance.GetPlayerCameraTargetActor(Id);
                return id == Actor.InvalidId ? null : Actor.Find(id);
            }
        }

        /// <summary>
        ///     Gets whether this Player is currently in any vehicle.
        /// </summary>
        public virtual bool InAnyVehicle => PlayerInternal.Instance.IsPlayerInAnyVehicle(Id);

        /// <summary>
        ///     Gets whether this Player is in his checkpoint.
        /// </summary>
        public virtual bool InCheckpoint => PlayerInternal.Instance.IsPlayerInCheckpoint(Id);

        /// <summary>
        ///     Gets whether this Player is in his race-checkpoint.
        /// </summary>
        public virtual bool InRaceCheckpoint => PlayerInternal.Instance.IsPlayerInRaceCheckpoint(Id);

        /// <summary>
        ///     Gets the Vehicle that this Player is surfing.
        /// </summary>
        public virtual BaseVehicle SurfingVehicle
        {
            get
            {
                var vehicleid = PlayerInternal.Instance.GetPlayerSurfingVehicleID(Id);
                return vehicleid == BaseVehicle.InvalidId ? null : BaseVehicle.Find(vehicleid);
            }
        }

        /// <summary>
        ///     Gets the <see cref="GlobalObject" /> that this Player is surfing.
        /// </summary>
        public virtual GlobalObject SurfingGlobalObject
        {
            get
            {
                var objectid = PlayerInternal.Instance.GetPlayerSurfingObjectID(Id);
                return objectid == GlobalObject.InvalidId ? null : GlobalObject.Find(objectid);
            }
        }
        
        /// <summary>
        ///     Gets the <see cref="PlayerObject" /> that this Player is surfing.
        /// </summary>
        public virtual PlayerObject SurfingPlayerObject
        {
            get
            {
                var objectid = PlayerInternal.Instance.GetPlayerSurfingObjectID(Id);
                return objectid == PlayerObject.InvalidId ? null : PlayerObject.Find(this, objectid);
            }
        }

        /// <summary>
        ///     Gets the Vehicle this Player is currently in.
        /// </summary>
        public virtual BaseVehicle Vehicle
        {
            get
            {
                var vehicleid = PlayerInternal.Instance.GetPlayerVehicleID(Id); //Returns 0, not Vehicle.InvalidId!
                return vehicleid == 0 ? null : BaseVehicle.Find(vehicleid);
            }
        }

        /// <summary>
        ///     Gets whether this Player is connected to the server.
        /// </summary>
        public virtual bool IsConnected => PlayerInternal.Instance.IsPlayerConnected(Id);


        /// <summary>
        ///     Gets or sets the rotation of this Player.
        /// </summary>
        /// <remarks>
        ///     Only the Z angle can be set!
        /// </remarks>
        public virtual Vector3 Rotation
        {
            get => new Vector3(0, 0, Angle);
            set => Angle = value.Z;
        }

        /// <summary>
        ///     Gets or sets the position of this Player.
        /// </summary>
        public virtual Vector3 Position
        {
            get
            {
                PlayerInternal.Instance.GetPlayerPos(Id, out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
            set => PlayerInternal.Instance.SetPlayerPos(Id, value.X, value.Y, value.Z);
        }

        #endregion

        #region SAMP properties

        /// <summary>
        ///     Gets whether this Player is an actual player or an NPC.
        /// </summary>
        public virtual bool IsNPC => IsNpcOverride || PlayerInternal.Instance.IsPlayerNPC(Id);

        /// <summary>
        ///     Gets whether this Player is logged into RCON.
        /// </summary>
        public virtual bool IsAdmin => PlayerInternal.Instance.IsPlayerAdmin(Id);

        /// <summary>
        ///     Gets a value indicating whether this Player is alive.
        /// </summary>
        public virtual bool IsAlive
            => !new[] {PlayerState.None, PlayerState.Spectating, PlayerState.Wasted}.Contains(State);


        /// <summary>
        ///     Gets this Player's network stats and saves them into a string.
        /// </summary>
        [Obsolete("Use the properties ConnectionStatus, BytesReceived, BytesSent and others instead")]
        public virtual string NetworkStats
        {
            get
            {
                PlayerInternal.Instance.GetPlayerNetworkStats(Id, out var stats, 256);
                return stats;
            }
        }

        /// <summary>
        ///     Gets this Player's game version.
        /// </summary>
        public virtual string Version
        {
            get
            {
                PlayerInternal.Instance.GetPlayerVersion(Id, out var version, 64);
                return version;
            }
        }

        /// <summary>
        ///     Gets this Player's GPCI string.
        /// </summary>
        public virtual string GPCI
        {
            get
            {
                PlayerInternal.Instance.GPCI(Id, out var result, 64);
                return result;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this player is selecting a textdraw.
        /// </summary>
        public virtual bool IsSelectingTextDraw { get; private set; }

        /// <summary>
        ///     Gets the amount of time (in milliseconds) that a player has been connected to the server for.
        /// </summary>
        public virtual int ConnectedTime => PlayerInternal.Instance.GetConnectedTime(Id);

        /// <summary>
        ///     Gets the number of messages the server has received from the player.
        /// </summary>
        public virtual int MessagesReceived => PlayerInternal.Instance.GetMessagesReceived(Id);

        /// <summary>
        ///     Gets the number of messages the player has received in the last second.
        /// </summary>
        public virtual int MessagesReceivedPerSecond => PlayerInternal.Instance.GetMessagesReceivedPerSecond(Id);

        /// <summary>
        ///     Gets the number of messages the server has sent to the player.
        /// </summary>
        public virtual int MessagesSent => PlayerInternal.Instance.GetMessagesSent(Id);

        /// <summary>
        ///     Get the amount of information (in bytes) that the server has sent to the player.
        /// </summary>
        public virtual int BytesReceived => PlayerInternal.Instance.GetBytesReceived(Id);

        /// <summary>
        ///     Get the amount of information (in bytes) that the server has received from the player.
        /// </summary>
        public virtual int BytesSent => PlayerInternal.Instance.GetBytesSent(Id);

        /// <summary>
        ///     Gets the packet loss percentage of a player. Packet loss means data the player is sending to the server is being
        ///     lost (or vice-versa).
        /// </summary>
        /// <remarks>This value has been found to be currently unreliable. The value is not as expected when compared to the client, therefore this function should not be used as a packet loss kicker.</remarks>
        [Obsolete("This value is unreliable. See remarks for details.")]
        public virtual float PacketLossPercent => PlayerInternal.Instance.GetPacketLossPercent(Id);

        /// <summary>
        ///     Get a player's connection status.
        /// </summary>
        public virtual ConnectionStatus ConnectionStatus => (ConnectionStatus)PlayerInternal.Instance.GetConnectionStatus(Id);


        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="OnConnected" /> is being called.
        ///     This callback is called when a player connects to the server.
        /// </summary>
        public event EventHandler<EventArgs> Connected;

        /// <summary>
        ///     Occurs when the <see cref="OnDisconnected" /> is being called.
        ///     This callback is called when a player disconnects from the server.
        /// </summary>
        public event EventHandler<DisconnectEventArgs> Disconnected;

        /// <summary>
        ///     Occurs when the <see cref="OnCleanup" /> is being called.
        ///     This callback is called after a player disconnects from the server.
        /// </summary>
        public event EventHandler<DisconnectEventArgs> Cleanup;

        /// <summary>
        ///     Occurs when the <see cref="OnSpawned" /> is being called.
        ///     This callback is called when a player spawns.
        /// </summary>
        public event EventHandler<SpawnEventArgs> Spawned;

        /// <summary>
        ///     Occurs when the <see cref="OnDeath" /> is being called.
        ///     This callback is triggered when the gamemode starts.
        /// </summary>
        public event EventHandler<DeathEventArgs> Died;

        /// <summary>
        ///     Occurs when the <see cref="OnText" /> is being called.
        ///     Called when a player sends a chat message.
        /// </summary>
        public event EventHandler<TextEventArgs> Text;

        /// <summary>
        ///     Occurs when the <see cref="OnCommandText" /> is being called.
        ///     This callback is called when a player enters a command into the client chat window, e.g. /help.
        /// </summary>
        public event EventHandler<CommandTextEventArgs> CommandText;

        /// <summary>
        ///     Occurs when the <see cref="OnRequestClass" /> is being called.
        ///     Called when a player changes class at class selection (and when class selection first appears).
        /// </summary>
        public event EventHandler<RequestClassEventArgs> RequestClass;

        /// <summary>
        ///     Occurs when the <see cref="OnEnterVehicle" /> is being called.
        ///     This callback is called when a player starts to enter a vehicle, meaning the player is not in vehicle yet at the
        ///     time this callback is called.
        /// </summary>
        public event EventHandler<EnterVehicleEventArgs> EnterVehicle;

        /// <summary>
        ///     Occurs when the <see cref="OnExitVehicle" /> is being called.
        ///     This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        ///     Not called if the player falls off a bike or is removed from a vehicle by other means such as setting
        ///     <see cref="Position" />.
        /// </remarks>
        public event EventHandler<PlayerVehicleEventArgs> ExitVehicle;

        /// <summary>
        ///     Occurs when the <see cref="OnStateChanged" /> is being called.
        ///     This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        ///     Not called if the player falls off a bike or is removed from a vehicle by other means such as setting
        ///     <see cref="Position" />.
        /// </remarks>
        public event EventHandler<StateEventArgs> StateChanged;

        /// <summary>
        ///     Occurs when the <see cref="OnEnterCheckpoint" /> is being called.
        ///     This callback is called when a player enters the checkpoint set for that player.
        /// </summary>
        public event EventHandler<EventArgs> EnterCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnLeaveCheckpoint" /> is being called.
        ///     This callback is called when a player leaves the checkpoint set for that player.
        /// </summary>
        public event EventHandler<EventArgs> LeaveCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnEnterRaceCheckpoint" /> is being called.
        ///     This callback is called when a player enters a race checkpoint.
        /// </summary>
        public event EventHandler<EventArgs> EnterRaceCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnLeaveRaceCheckpoint" /> is being called.
        ///     This callback is called when a player leaves the race checkpoint.
        /// </summary>
        public event EventHandler<EventArgs> LeaveRaceCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnRequestSpawn" /> is being called.
        ///     Called when a player attempts to spawn via class selection.
        /// </summary>
        public event EventHandler<RequestSpawnEventArgs> RequestSpawn;

        /// <summary>
        ///     Occurs when the <see cref="OnEnterExitModShop" /> is being called.
        ///     This callback is called when a player enters or exits a mod shop.
        /// </summary>
        public event EventHandler<EnterModShopEventArgs> EnterExitModShop;

        /// <summary>
        ///     Occurs when the <see cref="OnSelectedMenuRow" /> is being called.
        ///     This callback is called when a player selects an item from a menu.
        /// </summary>
        public event EventHandler<MenuRowEventArgs> SelectedMenuRow;

        /// <summary>
        ///     Occurs when the <see cref="OnExitedMenu" /> is being called.
        ///     Called when a player exits a menu.
        /// </summary>
        public event EventHandler<EventArgs> ExitedMenu;

        /// <summary>
        ///     Occurs when the <see cref="OnInteriorChanged" /> is being called.
        ///     Called when a player changes interior.
        /// </summary>
        /// <remarks>
        ///     This is also called when <see cref="Interior" /> is set.
        /// </remarks>
        public event EventHandler<InteriorChangedEventArgs> InteriorChanged;

        /// <summary>
        ///     Occurs when the <see cref="OnKeyStateChanged" /> is being called.
        ///     This callback is called when the state of any supported key is changed (pressed/released). Directional keys do not
        ///     trigger this callback.
        /// </summary>
        public event EventHandler<KeyStateChangedEventArgs> KeyStateChanged;

        /// <summary>
        ///     Occurs when the <see cref="OnUpdate" /> is being called.
        ///     This callback is called everytime a client/player updates the server with their status.
        /// </summary>
        /// <remarks>
        ///     This callback is called very frequently per second per player, only use it when you know what it's meant for.
        /// </remarks>
        public event EventHandler<PlayerUpdateEventArgs> Update;

        /// <summary>
        ///     Occurs when the <see cref="OnStreamIn" /> is being called.
        ///     This callback is called when a player is streamed by some other player's client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> StreamIn;

        /// <summary>
        ///     Occurs when the <see cref="OnStreamOut" /> is being called.
        ///     This callback is called when a player is streamed out from some other player's client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> StreamOut;

        /// <summary>
        ///     Occurs when the <see cref="OnDialogResponse" /> is being called.
        ///     This callback is called when a player responds to a dialog shown using <see cref="Dialog" /> by
        ///     either clicking a button, pressing ENTER/ESC or double-clicking a list item (if using a list style dialog).
        /// </summary>
        public event EventHandler<DialogResponseEventArgs> DialogResponse;

        /// <summary>
        ///     Occurs when the <see cref="OnTakeDamage" /> is being called.
        ///     This callback is called when a player takes damage.
        /// </summary>
        public event EventHandler<DamageEventArgs> TakeDamage;

        /// <summary>
        ///     Occurs when the <see cref="OnGiveDamage" /> is being called.
        ///     This callback is called when a player gives damage to another player.
        /// </summary>
        /// <remarks>
        ///     One thing you can do with GiveDamage is detect when other players report that they have damaged a certain player,
        ///     and that player hasn't taken any health loss. You can flag those players as suspicious.
        ///     You can also set all players to the same team (so they don't take damage from other players) and process all health
        ///     loss from other players manually.
        ///     You might have a server where players get a wanted level if they attack Cop players (or some specific class). In
        ///     that case you might trust GiveDamage over TakeDamage.
        ///     There should be a lot you can do with it. You just have to keep in mind the levels of trust between clients. In
        ///     most cases it's better to trust the client who is being damaged to report their health/armour (TakeDamage). SA-MP
        ///     normally does this. GiveDamage provides some extra information which may be useful when you require a different
        ///     level of trust.
        /// </remarks>
        public event EventHandler<DamageEventArgs> GiveDamage;

        /// <summary>
        ///     Occurs when the <see cref="OnClickMap" /> is being called.
        ///     This callback is called when a player places a target/waypoint on the pause menu map (by right-clicking).
        /// </summary>
        /// <remarks>
        ///     The Z value provided is only an estimate; you may find it useful to use a plugin like the MapAndreas plugin to get
        ///     a more accurate Z coordinate (or for teleportation; use <see cref="SetPositionFindZ" />).
        /// </remarks>
        public event EventHandler<PositionEventArgs> ClickMap;

        /// <summary>
        ///     Occurs when the <see cref="OnClickTextDraw" /> is being called.
        ///     This callback is called when a player clicks on a textdraw. It is not called when player cancels the select
        ///     mode (ESC)
        /// </summary>
        /// <remarks>
        ///     The clickable area is defined by <see cref="TextDraw.Width" /> and <see cref="TextDraw.Width" />. The x and y
        ///     parameters passed to that
        ///     function must not be zero or negative.
        /// </remarks>
        public event EventHandler<ClickTextDrawEventArgs> ClickTextDraw;

        /// <summary>
        ///     Occurs when the <see cref="OnClickPlayerTextDraw" /> is being called.
        ///     This callback is called when a player clicks on a player-textdraw. It is not called when player cancels the select
        ///     mode (ESC).
        /// </summary>
        public event EventHandler<ClickPlayerTextDrawEventArgs> ClickPlayerTextDraw;

        /// <summary>
        ///     Occurs when the <see cref="OnCancelClickTextDraw" /> is being called.
        ///     This callback is called when a player cancels the textdraw select mode(ESC).
        /// </summary>
        public event EventHandler<PlayerEventArgs> CancelClickTextDraw;

        /// <summary>
        ///     Occurs when the <see cref="OnClickPlayer" /> is being called.
        ///     Called when a player double-clicks on a player on the scoreboard.
        /// </summary>
        /// <remarks>
        ///     There is currently only one 'source' (<see cref="PlayerClickSource.Scoreboard" />). The existence of this argument
        ///     suggests that more sources may be supported in the future.
        /// </remarks>
        public event EventHandler<ClickPlayerEventArgs> ClickPlayer;

        /// <summary>
        ///     Occurs when the <see cref="OnEditGlobalObject" /> is being called.
        ///     This callback is called when a player ends object edition mode.
        /// </summary>
        public event EventHandler<EditGlobalObjectEventArgs> EditGlobalObject;

        /// <summary>
        ///     Occurs when the <see cref="OnEditPlayerObject" /> is being called.
        ///     This callback is called when a player ends object edition mode.
        /// </summary>
        public event EventHandler<EditPlayerObjectEventArgs> EditPlayerObject;

        /// <summary>
        ///     Occurs when the <see cref="OnEditAttachedObject" /> is being called.
        ///     This callback is called when a player ends attached object edition mode.
        /// </summary>
        /// <remarks>
        ///     Editions should be discarded if response was '0' (cancelled). This must be done by storing the offsets etc. in an
        ///     array BEFORE using EditAttachedObject.
        /// </remarks>
        public event EventHandler<EditAttachedObjectEventArgs> EditAttachedObject;

        /// <summary>
        ///     Occurs when the <see cref="OnSelectGlobalObject" /> is being called.
        ///     This callback is called when a player selects an object after <see cref="GlobalObject.Select" /> has been used.
        /// </summary>
        public event EventHandler<SelectGlobalObjectEventArgs> SelectGlobalObject;

        /// <summary>
        ///     Occurs when the <see cref="OnSelectPlayerObject" /> is being called.
        ///     This callback is called when a player selects an object after <see cref="PlayerObject.Select" /> has been used.
        /// </summary>
        public event EventHandler<SelectPlayerObjectEventArgs> SelectPlayerObject;

        /// <summary>
        ///     Occurs when the <see cref="OnWeaponShot" /> is being called.
        ///     This callback is called when a player fires a shot from a weapon.
        /// </summary>
        /// <remarks>
        ///     <see cref="BulletHitType.None" />: the fX, fY and fZ parameters are normal coordinates;
        ///     Others: the fX, fY and fZ are offsets from the center of hitid.
        /// </remarks>
        public event EventHandler<WeaponShotEventArgs> WeaponShot;

        /// <summary>
        ///     Occurs when the <see cref="OnPickUpPickup" /> is being called.
        ///     This callback is called when a player picks up a pickup.
        /// </summary>
        public event EventHandler<PickUpPickupEventArgs> PickUpPickup;

        #endregion

        #region Players natives

        /// <summary>
        ///     This function can be used to change the spawn information of a specific player. It allows you to automatically set
        ///     someone's spawn weapons, their team, skin and spawn position, normally used in case of mini games or
        ///     automatic-spawn systems.
        /// </summary>
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
        public virtual void SetSpawnInfo(int team, int skin, Vector3 position, float rotation,
            Weapon weapon1 = Weapon.None,
            int weapon1Ammo = 0, Weapon weapon2 = Weapon.None, int weapon2Ammo = 0, Weapon weapon3 = Weapon.None,
            int weapon3Ammo = 0)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetSpawnInfo(Id, team, skin, position.X, position.Y, position.Z, rotation, (int) weapon1,
                weapon1Ammo,
                (int) weapon2, weapon2Ammo,
                (int) weapon3, weapon3Ammo);
        }

        /// <summary>
        ///     (Re)Spawns a player.
        /// </summary>
        public virtual void Spawn()
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SpawnPlayer(Id);
        }

        /// <summary>
        ///     Restore the camera to a place behind the player, after using a function like <see cref="CameraPosition" />.
        /// </summary>
        public virtual void PutCameraBehindPlayer()
        {
            PlayerInternal.Instance.SetCameraBehindPlayer(Id);
        }

        /// <summary>
        ///     This sets this <see cref="BasePlayer" />'s position then adjusts the Player's z-coordinate to the nearest solid
        ///     ground under the
        ///     position.
        /// </summary>
        /// <param name="position">The position to move this Player to.</param>
        public virtual void SetPositionFindZ(Vector3 position)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetPlayerPosFindZ(Id, position.X, position.Y, position.Z);
        }

        /// <summary>
        ///     Check if this <see cref="BasePlayer" /> is in range of a point.
        /// </summary>
        /// <param name="range">The furthest distance the player can be from the point to be in range.</param>
        /// <param name="point">The point to check the range to.</param>
        /// <returns>True if this Player is in range of the point, otherwise False.</returns>
        public virtual bool IsInRangeOfPoint(float range, Vector3 point)
        {
            AssertNotDisposed();

            return PlayerInternal.Instance.IsPlayerInRangeOfPoint(Id, range, point.X, point.Y, point.Z);
        }

        /// <summary>
        ///     Calculate the distance between this <see cref="BasePlayer" /> and a map coordinate.
        /// </summary>
        /// <param name="point">The point to calculate the distance from.</param>
        /// <returns>The distance between the player and the point as a float.</returns>
        public virtual float GetDistanceFromPoint(Vector3 point)
        {
            AssertNotDisposed();

            return PlayerInternal.Instance.GetPlayerDistanceFromPoint(Id, point.X, point.Y, point.Z);
        }

        /// <summary>
        ///     Checks if a <see cref="BasePlayer" /> is streamed in this <see cref="BasePlayer" />'s client.
        /// </summary>
        /// <remarks>
        ///     Players aren't streamed in on their own client, so if this Player is the same as the other Player, it will return
        ///     false!
        /// </remarks>
        /// <remarks>
        ///     Players stream out if they are more than 150 meters away (see server.cfg - stream_distance)
        /// </remarks>
        /// <param name="other">The Player to check is streamed in.</param>
        /// <returns>True if the other Player is streamed in for this Player, False if not.</returns>
        public virtual bool IsPlayerStreamedIn(BasePlayer other)
        {
            AssertNotDisposed();

            if (other == null)
                throw new ArgumentNullException(nameof(other));

            return PlayerInternal.Instance.IsPlayerStreamedIn(other.Id, Id);
        }

        /// <summary>
        ///     Set the ammo of this <see cref="BasePlayer" />'s weapon.
        /// </summary>
        /// <param name="weapon">The weapon to set the ammo of.</param>
        /// <param name="ammo">The amount of ammo to set.</param>
        public virtual void SetAmmo(Weapon weapon, int ammo)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetPlayerAmmo(Id, (int) weapon, ammo);
        }

        /// <summary>
        ///     Give this <see cref="BasePlayer" /> a <see cref="Weapon" /> with a specified amount of ammo.
        /// </summary>
        /// <param name="weapon">The Weapon to give to this Player.</param>
        /// <param name="ammo">The amount of ammo to give to this Player.</param>
        public virtual void GiveWeapon(Weapon weapon, int ammo)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.GivePlayerWeapon(Id, (int) weapon, ammo);
        }


        /// <summary>
        ///     Removes all weapons from this <see cref="BasePlayer" />.
        /// </summary>
        public virtual void ResetWeapons()
        {
            AssertNotDisposed();

            PlayerInternal.Instance.ResetPlayerWeapons(Id);
        }

        /// <summary>
        ///     Sets the armed weapon of this <see cref="BasePlayer" />.
        /// </summary>
        /// <param name="weapon">The weapon that the player should be armed with.</param>
        public virtual void SetArmedWeapon(Weapon weapon)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetPlayerArmedWeapon(Id, (int) weapon);
        }

        /// <summary>
        ///     Get the <see cref="Weapon" /> and ammo in this <see cref="BasePlayer" />'s weapon slot.
        /// </summary>
        /// <param name="slot">The weapon slot to get data for (0-12).</param>
        /// <param name="weapon">The variable in which to store the weapon, passed by reference.</param>
        /// <param name="ammo">The variable in which to store the ammo, passed by reference.</param>
        public virtual void GetWeaponData(int slot, out Weapon weapon, out int ammo)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.GetPlayerWeaponData(Id, slot, out var weaponid, out ammo);
            weapon = (Weapon) weaponid;
        }

        /// <summary>
        ///     Give money to this <see cref="BasePlayer" />.
        /// </summary>
        /// <param name="money">The amount of money to give this <see cref="BasePlayer" />. Use a minus value to take money.</param>
        public virtual void GiveMoney(int money)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.GivePlayerMoney(Id, money);
        }

        /// <summary>
        ///     Reset this <see cref="BasePlayer" />'s money to $0.
        /// </summary>
        public virtual void ResetMoney()
        {
            AssertNotDisposed();

            PlayerInternal.Instance.ResetPlayerMoney(Id);
        }

        /// <summary>
        ///     Check which keys this <see cref="BasePlayer" /> is pressing.
        /// </summary>
        /// <remarks>
        ///     Only the FUNCTION of keys can be detected; not actual keys. You can not detect if the player presses space, but you
        ///     can detect if they press sprint (which can be mapped (assigned) to ANY key, but is space by default)).
        /// </remarks>
        /// <param name="keys">A set of bits containing this Player's key states</param>
        /// <param name="updown">Up or Down value, passed by reference.</param>
        /// <param name="leftright">Left or Right value, passed by reference.</param>
        public virtual void GetKeys(out Keys keys, out int updown, out int leftright)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.GetPlayerKeys(Id, out var keysDown, out updown, out leftright);
            keys = (Keys) keysDown;
        }

        /// <summary>
        ///     Sets the clock of this <see cref="BasePlayer" /> to a specific value. This also changes the daytime. (night/day
        ///     etc.)
        /// </summary>
        /// <param name="hour">Hour to set (0-23).</param>
        /// <param name="minutes">Minutes to set (0-59).</param>
        public virtual void SetTime(int hour, int minutes)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetPlayerTime(Id, hour, minutes);
        }

        /// <summary>
        ///     Get this <see cref="BasePlayer" />'s current game time. Set by <see cref="Server.SetWorldTime" />,
        ///     <see cref="Server.SetWorldTime" />,
        ///     or by <see cref="ToggleClock" />.
        /// </summary>
        /// <param name="hour">The variable to store the hour in, passed by reference.</param>
        /// <param name="minutes">The variable to store the minutes in, passed by reference.</param>
        public virtual void GetTime(out int hour, out int minutes)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.GetPlayerTime(Id, out hour, out minutes);
        }

        /// <summary>
        ///     Show/Hide the in-game clock (top right corner) for this <see cref="BasePlayer" />.
        /// </summary>
        /// <remarks>
        ///     Time is not synced with other players!
        /// </remarks>
        /// <param name="toggle">True to show, False to hide.</param>
        public virtual void ToggleClock(bool toggle)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.TogglePlayerClock(Id, toggle);
        }

        /// <summary>
        ///     Set this <see cref="BasePlayer" />'s weather. If <see cref="ToggleClock" /> has been used to enable the clock,
        ///     weather changes will
        ///     interpolate (gradually change), otherwise will change instantly.
        /// </summary>
        /// <param name="weather">The weather to set.</param>
        public virtual void SetWeather(int weather)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetPlayerWeather(Id, weather);
        }

        /// <summary>
        ///     Forces this <see cref="BasePlayer" /> to go back to class selection.
        /// </summary>
        /// <remarks>
        ///     The player will not return to class selection until they re-spawn. This can be achieved with
        ///     <see cref="ToggleSpectating" />
        /// </remarks>
        public virtual void ForceClassSelection()
        {
            AssertNotDisposed();

            PlayerInternal.Instance.ForceClassSelection(Id);
        }
        
        /// <summary>
        ///     Display the cursor and allow this <see cref="BasePlayer" /> to select a text draw.
        /// </summary>
        /// <param name="hoverColor">The color of the text draw when hovering over with mouse.</param>
        public virtual void SelectTextDraw(Color hoverColor)
        {
            AssertNotDisposed();

            IsSelectingTextDraw = true;
            PlayerInternal.Instance.SelectTextDraw(Id, hoverColor);
        }

        /// <summary>
        ///     Cancel text draw selection with the mouse for this <see cref="BasePlayer" />.
        /// </summary>
        public virtual void CancelSelectTextDraw()
        {
            AssertNotDisposed();

            IsSelectingTextDraw = false;
            PlayerInternal.Instance.CancelSelectTextDraw(Id);
        }

        /// <summary>
        ///     This function plays a crime report for this <see cref="BasePlayer" /> - just like in single-player when CJ commits
        ///     a
        ///     crime.
        /// </summary>
        /// <param name="suspect">The suspect player which will be described in the crime report.</param>
        /// <param name="crime">The crime ID, which will be reported as a 10-code (i.e. 10-16 if 16 was passed as the crimeid).</param>
        public virtual void PlayCrimeReport(BasePlayer suspect, int crime)
        {
            if (suspect == null) throw new ArgumentNullException(nameof(suspect));
            AssertNotDisposed();

            PlayerInternal.Instance.PlayCrimeReportForPlayer(Id, suspect.Id, crime);
        }

        /// <summary>
        ///     Play an 'audio stream' for this <see cref="BasePlayer" />. Normal audio files also work (e.g. MP3).
        /// </summary>
        /// <param name="url">
        ///     The url to play. Valid formats are mp3 and ogg/vorbis. A link to a .pls (playlist) file will play
        ///     that playlist.
        /// </param>
        /// <param name="position">The position at which to play the audio. Has no effect unless usepos is set to True.</param>
        /// <param name="distance">The distance over which the audio will be heard. Has no effect unless usepos is set to True.</param>
        public virtual void PlayAudioStream(string url, Vector3 position, float distance)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.PlayAudioStreamForPlayer(Id, url, position.X, position.Y, position.Z, distance, true);
        }

        /// <summary>
        ///     Play an 'audio stream' for this <see cref="BasePlayer" />. Normal audio files also work (e.g. MP3).
        /// </summary>
        /// <param name="url">
        ///     The url to play. Valid formats are mp3 and ogg/vorbis. A link to a .pls (playlist) file will play
        ///     that playlist.
        /// </param>
        public virtual void PlayAudioStream(string url)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.PlayAudioStreamForPlayer(Id, url, 0, 0, 0, 0, false);
        }

        /// <summary>
        ///     Allows you to disable collisions between vehicles for a player.
        /// </summary>
        /// <param name="disable">if set to <c>true</c> disables the collision between vehicles.</param>
        public virtual void DisableRemoteVehicleCollisions(bool disable)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.DisableRemoteVehicleCollisions(Id, disable);
        }

        /// <summary>
        ///     Toggles camera targeting functions for a player.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> the functionality is enabled.</param>
        public virtual void EnablePlayerCameraTarget(bool enable)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.EnablePlayerCameraTarget(Id, enable);
        }

        /// <summary>
        ///     Stops the current audio stream for this <see cref="BasePlayer" />.
        /// </summary>
        public virtual void StopAudioStream()
        {
            AssertNotDisposed();

            PlayerInternal.Instance.StopAudioStreamForPlayer(Id);
        }

        /// <summary>
        ///     Loads or unloads an interior script for this <see cref="BasePlayer" />. (for example the ammunation menu)
        /// </summary>
        /// <param name="shopname">The name of the shop.</param>
        public virtual void SetShopName(string shopname)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetPlayerShopName(Id, shopname);
        }

        /// <summary>
        ///     Set the skill level of a certain weapon type for this <see cref="BasePlayer" />.
        /// </summary>
        /// <remarks>
        ///     The skill parameter is NOT the weapon ID, it is the skill type.
        /// </remarks>
        /// <param name="skill">The <see cref="WeaponSkill" /> you want to set the skill of.</param>
        /// <param name="level">
        ///     The skill level to set for that weapon, ranging from 0 to 999. (A level out of range will max it
        ///     out)
        /// </param>
        public virtual void SetSkillLevel(WeaponSkill skill, int level)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetPlayerSkillLevel(Id, (int) skill, level);
        }

        /// <summary>
        ///     Attach an object to a specific bone on this <see cref="BasePlayer" />.
        /// </summary>
        /// <param name="index">The index (slot) to assign the object to (0-9).</param>
        /// <param name="modelid">The model to attach.</param>
        /// <param name="bone">The bone to attach the object to.</param>
        /// <param name="offset">offset for the object position.</param>
        /// <param name="rotation">rotation of the object.</param>
        /// <param name="scale">scale of the object.</param>
        /// <param name="materialcolor1">The first object color to set.</param>
        /// <param name="materialcolor2">The second object color to set.</param>
        /// <returns>True on success, False otherwise.</returns>
        public virtual bool SetAttachedObject(int index, int modelid, Bone bone, Vector3 offset, Vector3 rotation,
            Vector3 scale, Color materialcolor1, Color materialcolor2)
        {
            AssertNotDisposed();

            return PlayerInternal.Instance.SetPlayerAttachedObject(Id, index, modelid, (int) bone, offset.X, offset.Y, offset.Z,
                rotation.X, rotation.Y, rotation.Z, scale.X, scale.Y, scale.Z,
                materialcolor1.ToInteger(ColorFormat.ARGB), materialcolor2.ToInteger(ColorFormat.ARGB));
        }

        /// <summary>
        ///     Remove an attached object from this <see cref="BasePlayer" />.
        /// </summary>
        /// <param name="index">The index of the object to remove (set with <see cref="SetAttachedObject" />).</param>
        /// <returns>True on success, False otherwise.</returns>
        public virtual bool RemoveAttachedObject(int index)
        {
            AssertNotDisposed();

            return PlayerInternal.Instance.RemovePlayerAttachedObject(Id, index);
        }

        /// <summary>
        ///     Check if this <see cref="BasePlayer" /> has an object attached in the specified index (slot).
        /// </summary>
        /// <param name="index">The index (slot) to check.</param>
        /// <returns>True if the slot is used, False otherwise.</returns>
        public virtual bool IsAttachedObjectSlotUsed(int index)
        {
            AssertNotDisposed();

            return PlayerInternal.Instance.IsPlayerAttachedObjectSlotUsed(Id, index);
        }

        /// <summary>
        ///     Enter edition mode for an attached object.
        /// </summary>
        /// <param name="index">The index (slot) of the attached object to edit.</param>
        /// <returns>True on success, False otherwise.</returns>
        public virtual bool DoEditAttachedObject(int index)
        {
            AssertNotDisposed();

            return PlayerInternal.Instance.EditAttachedObject(Id, index);
        }

        /// <summary>
        ///     Creates a chat bubble above this <see cref="BasePlayer" />'s name tag.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="color">The text color.</param>
        /// <param name="drawdistance">The distance from where players are able to see the chat bubble.</param>
        /// <param name="expiretime">The time in miliseconds the bubble should be displayed for.</param>
        public virtual void SetChatBubble(string text, Color color, float drawdistance,
            int expiretime)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetPlayerChatBubble(Id, text, color.ToInteger(ColorFormat.RGBA), drawdistance, expiretime);
        }

        /// <summary>
        ///     Puts this <see cref="BasePlayer" /> in a <see cref="BaseVehicle" />.
        /// </summary>
        /// <param name="vehicle">The vehicle for the player to be put in.</param>
        /// <param name="seatid">The ID of the seat to put the player in.</param>
        public virtual void PutInVehicle(BaseVehicle vehicle, int seatid)
        {
            AssertNotDisposed();

            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            PlayerInternal.Instance.PutPlayerInVehicle(Id, vehicle.Id, seatid);
        }

        /// <summary>
        ///     Puts this <see cref="BasePlayer" /> in a vehicle as driver.
        /// </summary>
        /// <param name="vehicle">The vehicle for the player to be put in.</param>
        public virtual void PutInVehicle(BaseVehicle vehicle)
        {
            PutInVehicle(vehicle, 0);
        }

        /// <summary>
        ///     Removes/ejects this <see cref="BasePlayer" /> from his vehicle.
        /// </summary>
        /// <remarks>
        ///     The exiting animation is not synced for other players.
        ///     This function will not work when used in <see cref="OnEnterVehicle" />, because the
        ///     player isn't in
        ///     the vehicle when the callback is called. Use <see cref="OnStateChanged" /> instead.
        /// </remarks>
        public virtual void RemoveFromVehicle()
        {
            AssertNotDisposed();

            PlayerInternal.Instance.RemovePlayerFromVehicle(Id);
        }

        /// <summary>
        ///     Toggles whether this <see cref="BasePlayer" /> can control themselves, basically freezes them.
        /// </summary>
        /// <param name="toggle">False to freeze the player or True to unfreeze them.</param>
        public virtual void ToggleControllable(bool toggle)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.TogglePlayerControllable(Id, toggle);
        }

        /// <summary>
        ///     Plays the specified sound for this <see cref="BasePlayer" /> at a specific point.
        /// </summary>
        /// <param name="soundid">The sound to play.</param>
        /// <param name="point">Point for the sound to play at.</param>
        public virtual void PlaySound(int soundid, Vector3 point)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.PlayerPlaySound(Id, soundid, point.X, point.Y, point.Z);
        }

        /// <summary>
        ///     Plays the specified sound for this <see cref="BasePlayer" />.
        /// </summary>
        /// <param name="soundid">The sound to play.</param>
        public virtual void PlaySound(int soundid)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.PlayerPlaySound(Id, soundid, 0, 0, 0);
        }

        /// <summary>
        ///     Apply an animation to this <see cref="BasePlayer" />.
        /// </summary>
        /// <remarks>
        ///     The <paramref name="forcesync" /> parameter, in most cases is not needed since players sync animations themselves.
        ///     The <paramref name="forcesync" /> parameter can force all players who can see this Player to play the animation
        ///     regardless of whether the player is performing that animation. This is useful in circumstances where the player
        ///     can't sync the animation themselves. For example, they may be paused.
        /// </remarks>
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
        public virtual void ApplyAnimation(string animlib, string animname, float fDelta, bool loop, bool lockx,
            bool locky, bool freeze, int time, bool forcesync)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.ApplyAnimation(Id, animlib, animname, fDelta, loop, lockx, locky, freeze, time, forcesync);
        }

        /// <summary>
        ///     Apply an animation to this <see cref="BasePlayer" />.
        /// </summary>
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
        public virtual void ApplyAnimation(string animlib, string animname, float fDelta, bool loop, bool lockx,
            bool locky, bool freeze, int time)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.ApplyAnimation(Id, animlib, animname, fDelta, loop, lockx, locky, freeze, time, false);
        }

        /// <summary>
        ///     Clears all animations for this <see cref="BasePlayer" />.
        /// </summary>
        /// <param name="forcesync">Specifies whether the animation should be shown to streamed in players.</param>
        public virtual void ClearAnimations(bool forcesync)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.ClearAnimations(Id, forcesync);
        }

        /// <summary>
        ///     Clears all animations for this <see cref="BasePlayer" />.
        /// </summary>
        public virtual void ClearAnimations()
        {
            AssertNotDisposed();

            PlayerInternal.Instance.ClearAnimations(Id, false);
        }

        /// <summary>
        ///     Get the animation library/name this <see cref="BasePlayer" /> is playing.
        /// </summary>
        /// <param name="animlib">String variable that stores the animation library.</param>
        /// <param name="animname">String variable that stores the animation name.</param>
        /// <returns>True on success, False otherwise.</returns>
        public virtual bool GetAnimationName(out string animlib, out string animname)
        {
            AssertNotDisposed();

            return PlayerInternal.Instance.GetAnimationName(AnimationIndex, out animlib, 64, out animname, 64);
        }

        /// <summary>
        ///     Sets a checkpoint (red circle) for this <see cref="BasePlayer" />. Also shows a red blip on the radar.
        /// </summary>
        /// <remarks>
        ///     Checkpoints created on server-created objects will appear down on the 'real' ground, but will still function
        ///     correctly.
        ///     There is no fix available for this issue. A pickup can be used instead.
        /// </remarks>
        /// <param name="point">The point to set the checkpoint at.</param>
        /// <param name="size">The size of the checkpoint.</param>
        public virtual void SetCheckpoint(Vector3 point, float size)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetPlayerCheckpoint(Id, point.X, point.Y, point.Z, size);
        }

        /// <summary>
        ///     Disable any initialized checkpoints for this <see cref="BasePlayer" />.
        /// </summary>
        public virtual void DisableCheckpoint()
        {
            AssertNotDisposed();

            PlayerInternal.Instance.DisablePlayerCheckpoint(Id);
        }

        /// <summary>
        ///     Creates a race checkpoint. When this <see cref="BasePlayer" /> enters it, the <see cref="EnterRaceCheckpoint" />
        ///     callback is called.
        /// </summary>
        /// <param name="type">Type of checkpoint.</param>
        /// <param name="point">The point to set the checkpoint at.</param>
        /// <param name="nextPosition">Coordinates of the next point, for the arrow facing direction.</param>
        /// <param name="size">Length (diameter) of the checkpoint</param>
        public virtual void SetRaceCheckpoint(CheckpointType type, Vector3 point, Vector3 nextPosition, float size)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetPlayerRaceCheckpoint(Id, (int) type, point.X, point.Y, point.Z, nextPosition.X, nextPosition.Y,
                nextPosition.Z, size);
        }

        /// <summary>
        ///     Disable any initialized race checkpoints for this <see cref="BasePlayer" />.
        /// </summary>
        public virtual void DisableRaceCheckpoint()
        {
            AssertNotDisposed();

            PlayerInternal.Instance.DisablePlayerRaceCheckpoint(Id);
        }

        /// <summary>
        ///     Set the world boundaries for this <see cref="BasePlayer" /> - players can not go out of the boundaries.
        /// </summary>
        /// <remarks>
        ///     You can reset the player world bounds by setting the parameters to 20000.0000, -20000.0000, 20000.0000,
        ///     -20000.0000.
        /// </remarks>
        /// <param name="xMax">The maximum X coordinate the player can go to.</param>
        /// <param name="xMin">The minimum X coordinate the player can go to.</param>
        /// <param name="yMax">The maximum Y coordinate the player can go to.</param>
        /// <param name="yMin">The minimum Y coordinate the player can go to.</param>
        public virtual void SetWorldBounds(float xMax, float xMin, float yMax, float yMin)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetPlayerWorldBounds(Id, xMax, xMin, yMax, yMin);
        }

        /// <summary>
        ///     Change the color of this <see cref="BasePlayer" />'s name tag and radar blip for another Player.
        /// </summary>
        /// <param name="player">The player whose color will be changed.</param>
        /// <param name="color">New color.</param>
        public virtual void SetPlayerMarker(BasePlayer player, Color color)
        {
            AssertNotDisposed();

            if (player == null)
                throw new ArgumentNullException(nameof(player));

            PlayerInternal.Instance.SetPlayerMarkerForPlayer(Id, player.Id, color.ToInteger(ColorFormat.RGBA));
        }

        /// <summary>
        ///     This functions allows you to toggle the drawing of player name tags, health bars and armor bars which display above
        ///     their head. For use of a similar function like this on a global level, <see cref="BaseMode.ShowNameTags" />
        ///     function.
        /// </summary>
        /// <remarks>
        ///     <see cref="BaseMode.ShowNameTags" /> must be set to <c>true</c> to be able to show name tags with
        ///     <see cref="ShowNameTagForPlayer" />.
        /// </remarks>
        /// <param name="player">The <see cref="BasePlayer" /> whose name tag will be shown or hidden.</param>
        /// <param name="show">True to show name tag, False to hide name tag.</param>
        public virtual void ShowNameTagForPlayer(BasePlayer player, bool show)
        {
            AssertNotDisposed();

            if (player == null)
                throw new ArgumentNullException(nameof(player));

            PlayerInternal.Instance.ShowPlayerNameTagForPlayer(Id, player.Id, show);
        }

        /// <summary>
        ///     Set the direction this <see cref="BasePlayer" />'s camera looks at. To be used in combination with
        ///     <see cref="CameraPosition" />.
        /// </summary>
        /// <param name="point">The coordinates for this Player's camera to look at.</param>
        /// <param name="cut">The style the camera-position changes.</param>
        public virtual void SetCameraLookAt(Vector3 point, CameraCut cut)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SetPlayerCameraLookAt(Id, point.X, point.Y, point.Z, (int) cut);
        }

        /// <summary>
        ///     Set the direction this <see cref="BasePlayer" />'s camera looks at. To be used in combination with
        ///     <see cref="CameraPosition" />.
        /// </summary>
        /// <param name="point">The coordinates for this Player's camera to look at.</param>
        public virtual void SetCameraLookAt(Vector3 point)
        {
            AssertNotDisposed();

            SetCameraLookAt(point, CameraCut.Cut);
        }

        /// <summary>
        ///     Move this <see cref="BasePlayer" />'s camera from one position to another, within the set time.
        /// </summary>
        /// <param name="from">The position the camera should start to move from.</param>
        /// <param name="to">The position the camera should move to.</param>
        /// <param name="time">Time in milliseconds.</param>
        /// <param name="cut">The jump cut to use. Defaults to CameraCut.Cut. Set to CameraCut. Move for a smooth movement.</param>
        public virtual void InterpolateCameraPosition(Vector3 from, Vector3 to, int time, CameraCut cut)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.InterpolateCameraPos(Id, from.X, from.Y, from.Z, to.X, to.Y, to.Z, time, (int) cut);
        }

        /// <summary>
        ///     Interpolate this <see cref="BasePlayer" />'s camera's 'look at' point between two coordinates with a set speed.
        /// </summary>
        /// <param name="from">The position the camera should start to move from.</param>
        /// <param name="to">The position the camera should move to.</param>
        /// <param name="time">Time in milliseconds to complete interpolation.</param>
        /// <param name="cut">The jump cut to use. Defaults to CameraCut.Cut (pointless). Set to CameraCut.Move for interpolation.</param>
        public virtual void InterpolateCameraLookAt(Vector3 from, Vector3 to, int time, CameraCut cut)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.InterpolateCameraLookAt(Id, from.X, from.Y, from.Z, to.X, to.Y, to.Z, time, (int) cut);
        }

        /// <summary>
        ///     Checks if this <see cref="BasePlayer" /> is in a specific <see cref="BaseVehicle" />.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <returns>True if player is in the vehicle; False otherwise.</returns>
        public virtual bool IsInVehicle(BaseVehicle vehicle)
        {
            AssertNotDisposed();

            return PlayerInternal.Instance.IsPlayerInVehicle(Id, vehicle.Id);
        }

        /// <summary>
        ///     Toggle stunt bonuses for this <see cref="BasePlayer" />.
        /// </summary>
        /// <param name="enable">True to enable stunt bonuses, False to disable them.</param>
        public virtual void EnableStuntBonus(bool enable)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.EnableStuntBonusForPlayer(Id, enable);
        }

        /// <summary>
        ///     Toggle this <see cref="BasePlayer" />'s spectate mode.
        /// </summary>
        /// <remarks>
        ///     When the spectating is turned off, OnPlayerSpawn will automatically be called.
        /// </remarks>
        /// <param name="toggle">True to enable spectating and False to disable.</param>
        public virtual void ToggleSpectating(bool toggle)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.TogglePlayerSpectating(Id, toggle);
        }

        /// <summary>
        ///     Makes this <see cref="BasePlayer" /> spectate (watch) another player.
        /// </summary>
        /// <remarks>
        ///     Order is CRITICAL! Ensure that you use <see cref="ToggleSpectating" /> before
        ///     <see cref="SpectatePlayer(BasePlayer,SpectateMode)" />.
        /// </remarks>
        /// <param name="targetPlayer">The Player that should be spectated.</param>
        /// <param name="mode">The mode to spectate with.</param>
        public virtual void SpectatePlayer(BasePlayer targetPlayer, SpectateMode mode)
        {
            AssertNotDisposed();

            if (targetPlayer == null)
                throw new ArgumentNullException(nameof(targetPlayer));

            PlayerInternal.Instance.PlayerSpectatePlayer(Id, targetPlayer.Id, (int) mode);
        }

        /// <summary>
        ///     Makes this <see cref="BasePlayer" /> spectate (watch) another player.
        /// </summary>
        /// <remarks>
        ///     Order is CRITICAL! Ensure that you use <see cref="ToggleSpectating" /> before
        ///     <see cref="SpectatePlayer(BasePlayer,SpectateMode)" />.
        /// </remarks>
        /// <param name="targetPlayer">The Player that should be spectated.</param>
        public virtual void SpectatePlayer(BasePlayer targetPlayer)
        {
            AssertNotDisposed();

            if (targetPlayer == null)
                throw new ArgumentNullException(nameof(targetPlayer));

            SpectatePlayer(targetPlayer, SpectateMode.Normal);
        }

        /// <summary>
        ///     Sets this <see cref="BasePlayer" /> to spectate another vehicle, i.e. see what its driver sees.
        /// </summary>
        /// <remarks>
        ///     Order is CRITICAL! Ensure that you use <see cref="ToggleSpectating" /> before
        ///     <see cref="SpectateVehicle(BaseVehicle,SpectateMode)" />.
        /// </remarks>
        /// <param name="targetVehicle">The vehicle to spectate.</param>
        /// <param name="mode">Spectate mode.</param>
        public virtual void SpectateVehicle(BaseVehicle targetVehicle, SpectateMode mode)
        {
            AssertNotDisposed();

            if (targetVehicle == null)
                throw new ArgumentNullException(nameof(targetVehicle));

            PlayerInternal.Instance.PlayerSpectateVehicle(Id, targetVehicle.Id, (int) mode);
        }

        /// <summary>
        ///     Sets this <see cref="BasePlayer" /> to spectate another vehicle, i.e. see what its driver sees.
        /// </summary>
        /// <remarks>
        ///     Order is CRITICAL! Ensure that you use <see cref="ToggleSpectating" /> before
        ///     <see cref="SpectateVehicle(BaseVehicle,SpectateMode)" />.
        /// </remarks>
        /// <param name="targetVehicle">The vehicle to spectate.</param>
        public virtual void SpectateVehicle(BaseVehicle targetVehicle)
        {
            AssertNotDisposed();

            if (targetVehicle == null)
                throw new ArgumentNullException(nameof(targetVehicle));

            SpectateVehicle(targetVehicle, SpectateMode.Normal);
        }

        /// <summary>
        ///     Starts recording this <see cref="BasePlayer" />'s movements to a file, which can then be reproduced by an NPC.
        /// </summary>
        /// <param name="recordtype">The type of recording.</param>
        /// <param name="recordname">
        ///     Name of the file which will hold the recorded data. It will be saved in scriptfiles, with an
        ///     automatically added .rec extension.
        /// </param>
        public virtual void StartRecordingPlayerData(PlayerRecordingType recordtype, string recordname)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.StartRecordingPlayerData(Id, (int) recordtype, recordname);
        }

        /// <summary>
        ///     Stops all the recordings that had been started with <see cref="StartRecordingPlayerData" /> for this
        ///     <see cref="BasePlayer" />.
        /// </summary>
        public virtual void StopRecordingPlayerData()
        {
            AssertNotDisposed();

            PlayerInternal.Instance.StopRecordingPlayerData(Id);
        }

        /// <summary>
        ///     Retrieves the start and end (hit) position of the last bullet a player fired.
        /// </summary>
        /// <param name="origin">The origin.</param>
        /// <param name="hitPosition">The hit position.</param>
        public virtual void GetLastShot(out Vector3 origin, out Vector3 hitPosition)
        {
            PlayerInternal.Instance.GetPlayerLastShotVectors(Id, out var ox, out var oy, out var oz, out var hx, out var hy, out var hz);

            origin = new Vector3(ox, oy, oz);
            hitPosition = new Vector3(hx, hy, hz);
        }

        /// <summary>Place an icon/marker on this player's map. Can be used to mark locations such as banks and hospitals to players.</summary>
        /// <param name="iconId">The icon identifier.</param>
        /// <param name="position">The position of the map icon.</param>
        /// <param name="markerType">Type of the map icon marker.</param>
        /// <param name="color">The color of the map icon marker (for <see cref="MapIcon.ColoredSquareTriangleDynamic"/>).</param>
        /// <param name="markerStyle">The marker style.</param>
        public virtual void SetMapIcon(int iconId, Vector3 position, MapIcon markerType, Color color = default, MapIconType markerStyle = 0)
        {
            PlayerInternal.Instance.SetPlayerMapIcon(Id, iconId, position.X, position.Y, position.Z, (int)markerType, color,
                (int)markerStyle);
        }

        /// <summary>
        /// Removes a map icon that was set earlier for this player using <see cref="SetMapIcon"/>.
        /// </summary>
        /// <param name="iconId">The icon identifier.</param>
        public virtual void RemoveMapIcon(int iconId)
        {
            PlayerInternal.Instance.RemovePlayerMapIcon(Id, iconId);
        }

        #endregion

        #region SAMP natives

        /// <summary>
        ///     This function sends a message to this <see cref="BasePlayer" /> with a chosen color in the chat. The whole line in
        ///     the chat box will be
        ///     in the set color unless colour embedding is used.
        /// </summary>
        /// <param name="color">The color of the message.</param>
        /// <param name="message">The text that will be displayed.</param>
        public virtual void SendClientMessage(Color color, string message)
        {
            AssertNotDisposed();

            if (message.Length > 144)
            {
                PlayerInternal.Instance.SendClientMessage(Id, color.ToInteger(ColorFormat.RGBA), message.Substring(0, 144));
                SendClientMessage(color, message.Substring(144));
            }
            else
            {
                PlayerInternal.Instance.SendClientMessage(Id, color.ToInteger(ColorFormat.RGBA), message);
            }
        }

        /// <summary>
        ///     Kicks this <see cref="BasePlayer" /> from the server. They will have to quit the game and re-connect if they wish
        ///     to
        ///     continue playing.
        /// </summary>
        public virtual void Kick()
        {
            AssertNotDisposed();

            PlayerInternal.Instance.Kick(Id);
        }

        /// <summary>
        ///     Ban this <see cref="BasePlayer" />. The ban will be IP-based, and be saved in the samp.ban file in the
        ///     server's root directory. <see cref="Ban(string)" /> allows you to ban with a reason, while you can ban and unban
        ///     IPs
        ///     using the RCON banip and unbanip commands.
        /// </summary>
        public virtual void Ban()
        {
            AssertNotDisposed();

            PlayerInternal.Instance.Ban(Id);
        }

        /// <summary>
        ///     Ban this <see cref="BasePlayer" /> with a reason.
        /// </summary>
        /// <param name="reason">The reason for the ban.</param>
        public virtual void Ban(string reason)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.BanEx(Id, reason);
        }

        /// <summary>
        ///     This function sends a message to this <see cref="BasePlayer" /> with a chosen color in the chat. The whole line in
        ///     the chatbox will be
        ///     in the set color unless color embedding is used.
        /// </summary>
        /// <param name="color">The color of the message.</param>
        /// <param name="messageFormat">The composite format string of the text that will be displayed (max 144 characters).</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public virtual void SendClientMessage(Color color, string messageFormat, params object[] args)
        {
            SendClientMessage(color, string.Format(messageFormat, args));
        }

        /// <summary>
        ///     This function sends a message to this <see cref="BasePlayer" /> in white in the chat. The whole line in the chat
        ///     box
        ///     will be
        ///     in the set color unless color embedding is used.
        /// </summary>
        /// <param name="message">The text that will be displayed.</param>
        public virtual void SendClientMessage(string message)
        {
            SendClientMessage(DefaultClientMessageColor, message);
        }

        /// <summary>
        ///     This function sends a message to this <see cref="BasePlayer" /> in white in the chat. The whole line in the chat
        ///     box
        ///     will be
        ///     in the set color unless color embedding is used.
        /// </summary>
        /// <param name="messageFormat">The composite format string of the text that will be displayed (max 144 characters).</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public virtual void SendClientMessage(string messageFormat, params object[] args)
        {
            SendClientMessage(DefaultClientMessageColor, string.Format(messageFormat, args));
        }

        /// <summary>
        ///     Displays a message in chat to all players.
        /// </summary>
        /// <param name="color">The color of the message.</param>
        /// <param name="message">The message to show (max 144 characters).</param>
        public static void SendClientMessageToAll(Color color, string message)
        {
            if (message.Length > 144)
            {
                PlayerInternal.Instance.SendClientMessageToAll(color.ToInteger(ColorFormat.RGBA), message.Substring(0, 144));
                SendClientMessageToAll(color, message.Substring(144));
            }
            else
            {
                PlayerInternal.Instance.SendClientMessageToAll(color.ToInteger(ColorFormat.RGBA), message);
            }
        }

        /// <summary>
        ///     Displays a message in chat to all players.
        /// </summary>
        /// <param name="color">The color of the message.</param>
        /// <param name="messageFormat">The composite format string of the text that will be displayed.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void SendClientMessageToAll(Color color, string messageFormat, params object[] args)
        {
            SendClientMessageToAll(color, string.Format(messageFormat, args));
        }

        /// <summary>
        ///     Displays a message in white in chat to all players.
        /// </summary>
        /// <param name="message">The message to show.</param>
        public static void SendClientMessageToAll(string message)
        {
            SendClientMessageToAll(DefaultClientMessageColor, message);
        }

        /// <summary>
        ///     Displays a message in white in chat to all players.
        /// </summary>
        /// <param name="messageFormat">The composite format string of the text that will be displayed.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void SendClientMessageToAll(string messageFormat, params object[] args)
        {
            SendClientMessageToAll(DefaultClientMessageColor, string.Format(messageFormat, args));
        }

        /// <summary>
        ///     Sends a message in the name this <see cref="BasePlayer" /> to another player on the server. The message will appear
        ///     in the chat box
        ///     but can only be seen by <paramref name="receiver" />. The line will start with the this Player's name in his color,
        ///     followed by the <paramref name="message" /> in white.
        /// </summary>
        /// <param name="receiver">The <see cref="BasePlayer" /> who will recieve the message</param>
        /// <param name="message">The message that will be sent.</param>
        public virtual void SendPlayerMessageToPlayer(BasePlayer receiver, string message)
        {
            AssertNotDisposed();

            if (receiver == null)
                throw new ArgumentNullException(nameof(receiver));

            PlayerInternal.Instance.SendPlayerMessageToPlayer(receiver.Id, Id, message);
        }

        /// <summary>
        ///     Sends a message in the name of this <see cref="BasePlayer" /> to all other players on the server. The line will
        ///     start with the this
        ///     <see cref="BasePlayer" />'s name in their color, followed by the <paramref name="message" /> in white.
        /// </summary>
        /// <param name="message">The message that will be sent.</param>
        public virtual void SendPlayerMessageToAll(string message)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SendPlayerMessageToAll(Id, message);
        }

        /// <summary>
        ///     Shows 'game text' (on-screen text) for a certain length of time for all players.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="time">The duration of the text being shown in milliseconds.</param>
        /// <param name="style">The style of text to be displayed.</param>
        public static void GameTextForAll(string text, int time, int style)
        {
            PlayerInternal.Instance.GameTextForAll(text, time, style);
        }

        /// <summary>
        ///     Shows 'game text' (on-screen text) for a certain length of time for this <see cref="BasePlayer" />.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="time">The duration of the text being shown in milliseconds.</param>
        /// <param name="style">The style of text to be displayed.</param>
        public virtual void GameText(string text, int time, int style)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.GameTextForPlayer(Id, text, time, style);
        }

        /// <summary>
        ///     Create an explosion at the specified coordinates.
        /// </summary>
        /// <param name="position">The position of the explosion.</param>
        /// <param name="type">The type of explosion.</param>
        /// <param name="radius">The explosion radius.</param>
        public static void CreateExplosionForAll(Vector3 position, ExplosionType type, float radius)
        {
            PlayerInternal.Instance.CreateExplosion(position.X, position.Y, position.Z, (int)type, radius);
        }

        /// <summary>
        ///     Create an explosion at the specified coordinates.
        /// </summary>
        /// <param name="position">The position of the explosion.</param>
        /// <param name="type">The type of explosion.</param>
        /// <param name="radius">The explosion radius.</param>
        /// <param name="interior">The interior of the explosion.</param>
        public static void CreateExplosionForAll(Vector3 position, ExplosionType type, float radius, int interior)
        {
            foreach (var p in All.Where(p => p.Interior == interior))
                p.CreateExplosion(position, type, radius);
        }

        /// <summary>
        ///     Create an explosion at the specified coordinates.
        /// </summary>
        /// <param name="position">The position of the explosion.</param>
        /// <param name="type">The type of explosion.</param>
        /// <param name="radius">The explosion radius.</param>
        /// <param name="interior">The interior of the explosion.</param>
        /// <param name="virtualworld">The virtualworld of the explosion.</param>
        public static void CreateExplosionForAll(Vector3 position, ExplosionType type, float radius, int interior,
            int virtualworld)
        {
            foreach (var p in All.Where(p => p.Interior == interior && p.VirtualWorld == virtualworld))
                p.CreateExplosion(position, type, radius);
        }

        /// <summary>
        ///     Creates an explosion for a <see cref="BasePlayer" />.
        ///     Only the specific player will see explosion and feel its effects.
        ///     This is useful when you want to isolate explosions from other players or to make them only appear in specific
        ///     virtual worlds.
        /// </summary>
        /// <param name="position">The position of the explosion.</param>
        /// <param name="type">The explosion type.</param>
        /// <param name="radius">The radius of the explosion.</param>
        public virtual void CreateExplosion(Vector3 position, ExplosionType type, float radius)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.CreateExplosionForPlayer(Id, position.X, position.Y, position.Z, (int) type, radius);
        }

        /// <summary>
        ///     Adds a death to the kill feed on the right-hand side of the screen of this <see cref="BasePlayer" />.
        /// </summary>
        /// <param name="killer">The <see cref="BasePlayer" /> that killer the <paramref name="killee" />.</param>
        /// <param name="killee">The <see cref="BasePlayer" /> that has been killed.</param>
        /// <param name="weapon">The reason for this <see cref="BasePlayer" />'s death.</param>
        public virtual void SendDeathMessage(BasePlayer killer, BasePlayer killee, Weapon weapon)
        {
            AssertNotDisposed();

            PlayerInternal.Instance.SendDeathMessageToPlayer(Id, killer?.Id ?? InvalidId, killee?.Id ?? InvalidId, (int) weapon);
        }

        /// <summary>
        ///     Adds a death to the kill feed on the right-hand side of the screen.
        /// </summary>
        /// <param name="killer">The Player that killer the <paramref name="killee" />.</param>
        /// <param name="killee">The player that has been killed.</param>
        /// <param name="weapon">The reason for this Player's death.</param>
        public static void SendDeathMessageToAll(BasePlayer killer, BasePlayer killee, Weapon weapon)
        {
            PlayerInternal.Instance.SendDeathMessage(killer?.Id ?? InvalidId, killee?.Id ?? InvalidId, (int) weapon);
        }

        /// <summary>
        /// Cancels object editing mode for this player.
        /// </summary>
        public void CancelEdit()
        {
            PlayerInternal.Instance.CancelEdit(Id);
        }

        #endregion

        #region Event raisers

        /// <summary>
        ///     Raises the <see cref="Connected" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        public virtual void OnConnected(EventArgs e)
        {
            Connected?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Disconnected" /> event.
        /// </summary>
        /// <param name="e">An <see cref="DisconnectEventArgs" /> that contains the event data. </param>
        public virtual void OnDisconnected(DisconnectEventArgs e)
        {
            Disconnected?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Cleanup" /> event.
        /// </summary>
        /// <param name="e">An <see cref="DisconnectEventArgs" /> that contains the event data. </param>
        public virtual void OnCleanup(DisconnectEventArgs e)
        {
            Cleanup?.Invoke(this, e);

            Dispose();
        }

        /// <summary>
        ///     Raises the <see cref="Spawned" /> event.
        /// </summary>
        /// <param name="e">An <see cref="SpawnEventArgs" /> that contains the event data. </param>
        public virtual void OnSpawned(SpawnEventArgs e)
        {
            Spawned?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Died" /> event.
        /// </summary>
        /// <param name="e">An <see cref="DeathEventArgs" /> that contains the event data. </param>
        public virtual void OnDeath(DeathEventArgs e)
        {
            Died?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Text" /> event.
        /// </summary>
        /// <param name="e">An <see cref="TextEventArgs" /> that contains the event data. </param>
        public virtual void OnText(TextEventArgs e)
        {
            Text?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="CommandText" /> event.
        /// </summary>
        /// <param name="e">An <see cref="TextEventArgs" /> that contains the event data. </param>
        public virtual void OnCommandText(CommandTextEventArgs e)
        {
            CommandText?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="RequestClass" /> event.
        /// </summary>
        /// <param name="e">An <see cref="RequestClassEventArgs" /> that contains the event data. </param>
        public virtual void OnRequestClass(RequestClassEventArgs e)
        {
            RequestClass?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EnterVehicle" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EnterVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnEnterVehicle(EnterVehicleEventArgs e)
        {
            EnterVehicle?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ExitVehicle" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnExitVehicle(PlayerVehicleEventArgs e)
        {
            ExitVehicle?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="StateChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="StateEventArgs" /> that contains the event data. </param>
        public virtual void OnStateChanged(StateEventArgs e)
        {
            StateChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EnterCheckpoint" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        public virtual void OnEnterCheckpoint(EventArgs e)
        {
            EnterCheckpoint?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="LeaveCheckpoint" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        public virtual void OnLeaveCheckpoint(EventArgs e)
        {
            LeaveCheckpoint?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EnterRaceCheckpoint" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        public virtual void OnEnterRaceCheckpoint(EventArgs e)
        {
            EnterRaceCheckpoint?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="LeaveRaceCheckpoint" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        public virtual void OnLeaveRaceCheckpoint(EventArgs e)
        {
            LeaveRaceCheckpoint?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="RequestSpawn" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnRequestSpawn(RequestSpawnEventArgs e)
        {
            RequestSpawn?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EnterExitModShop" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EnterModShopEventArgs" /> that contains the event data. </param>
        public virtual void OnEnterExitModShop(EnterModShopEventArgs e)
        {
            EnterExitModShop?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="SelectedMenuRow" /> event.
        /// </summary>
        /// <param name="e">An <see cref="MenuRowEventArgs" /> that contains the event data. </param>
        public virtual void OnSelectedMenuRow(MenuRowEventArgs e)
        {
            SelectedMenuRow?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ExitedMenu" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        public virtual void OnExitedMenu(EventArgs e)
        {
            ExitedMenu?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="InteriorChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="InteriorChangedEventArgs" /> that contains the event data. </param>
        public virtual void OnInteriorChanged(InteriorChangedEventArgs e)
        {
            InteriorChanged?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="KeyStateChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="KeyStateChangedEventArgs" /> that contains the event data. </param>
        public virtual void OnKeyStateChanged(KeyStateChangedEventArgs e)
        {
            KeyStateChanged?.Invoke(this, e);

            Key.Handle(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Update" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnUpdate(PlayerUpdateEventArgs e)
        {
            Update?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="StreamIn" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnStreamIn(PlayerEventArgs e)
        {
            StreamIn?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="StreamOut" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnStreamOut(PlayerEventArgs e)
        {
            StreamOut?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="DialogResponse" /> event.
        /// </summary>
        /// <param name="e">An <see cref="DialogResponseEventArgs" /> that contains the event data. </param>
        public virtual void OnDialogResponse(DialogResponseEventArgs e)
        {
            DialogResponse?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="TakeDamage" /> event.
        /// </summary>
        /// <param name="e">An <see cref="DamageEventArgs" /> that contains the event data. </param>
        public virtual void OnTakeDamage(DamageEventArgs e)
        {
            TakeDamage?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="GiveDamage" /> event.
        /// </summary>
        /// <param name="e">An <see cref="DamageEventArgs" /> that contains the event data. </param>
        public virtual void OnGiveDamage(DamageEventArgs e)
        {
            GiveDamage?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ClickMap" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PositionEventArgs" /> that contains the event data. </param>
        public virtual void OnClickMap(PositionEventArgs e)
        {
            ClickMap?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ClickTextDraw" /> event.
        /// </summary>
        /// <param name="e">An <see cref="ClickTextDrawEventArgs" /> that contains the event data. </param>
        public virtual void OnClickTextDraw(ClickTextDrawEventArgs e)
        {
            ClickTextDraw?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ClickPlayerTextDraw" /> event.
        /// </summary>
        /// <param name="e">An <see cref="ClickPlayerTextDrawEventArgs" /> that contains the event data. </param>
        public virtual void OnClickPlayerTextDraw(ClickPlayerTextDrawEventArgs e)
        {
            ClickPlayerTextDraw?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="CancelClickTextDraw" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnCancelClickTextDraw(PlayerEventArgs e)
        {
            CancelClickTextDraw?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ClickPlayer" /> event.
        /// </summary>
        /// <param name="e">An <see cref="ClickPlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnClickPlayer(ClickPlayerEventArgs e)
        {
            ClickPlayer?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EditGlobalObject" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EditGlobalObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnEditGlobalObject(EditGlobalObjectEventArgs e)
        {
            EditGlobalObject?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EditPlayerObject" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EditPlayerObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnEditPlayerObject(EditPlayerObjectEventArgs e)
        {
            EditPlayerObject?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EditAttachedObject" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EditAttachedObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnEditAttachedObject(EditAttachedObjectEventArgs e)
        {
            EditAttachedObject?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="SelectGlobalObject" /> event.
        /// </summary>
        /// <param name="e">An <see cref="SelectGlobalObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnSelectGlobalObject(SelectGlobalObjectEventArgs e)
        {
            SelectGlobalObject?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="SelectPlayerObject" /> event.
        /// </summary>
        /// <param name="e">An <see cref="SelectPlayerObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnSelectPlayerObject(SelectPlayerObjectEventArgs e)
        {
            SelectPlayerObject?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="WeaponShot" /> event.
        /// </summary>
        /// <param name="e">An <see cref="WeaponShotEventArgs" /> that contains the event data. </param>
        public virtual void OnWeaponShot(WeaponShotEventArgs e)
        {
            WeaponShot?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PickUpPickup" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PickUpPickupEventArgs" /> that contains the event data. </param>
        public virtual void OnPickUpPickup(PickUpPickupEventArgs e)
        {
            PickUpPickup?.Invoke(this, e);
        }

        #endregion
    }
}
