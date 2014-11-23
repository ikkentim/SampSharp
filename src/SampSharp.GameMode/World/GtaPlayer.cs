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
using System.Linq;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World
{
    /// <summary>
    ///     Represents a SA-MP player.
    /// </summary>
    public class GtaPlayer : IdentifiedPool<GtaPlayer>, IIdentifiable, IWorldObject
    {
        #region Fields

        /// <summary>
        ///     Gets an ID commonly returned by methods to point out that no player matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.InvalidPlayerId;

        /// <summary>
        ///     Max number of attachedobjects attached to a player.
        /// </summary>
        public const int MaxAttachedObjects = Misc.MaxPlayerAttachedObjects;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initalizes a new instance of the GtaPlayer class.
        /// </summary>
        /// <param name="id">The ID of the player to initialize.</param>
        public GtaPlayer(int id)
        {
            //Fill properties
            Id = id;
            PVars = new PVarCollection(this);
            Key = new KeyChangeHandlerSet();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a collections of Player Variables of this Player.
        /// </summary>
        public PVarCollection PVars { get; private set; }

        /// <summary>
        ///     Gets a set of KeyHandlers for different keystates.
        /// </summary>
        public KeyChangeHandlerSet Key { get; private set; }

        /// <summary>
        ///     Gets the ID of this Player.
        /// </summary>
        public int Id { get; private set; }

        #endregion

        #region Players properties

        /// <summary>
        ///     Gets or sets the name of this Player.
        /// </summary>
        public virtual string Name
        {
            get { return Native.GetPlayerName(Id); }
            set { Native.SetPlayerName(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the facing angle of this Player.
        /// </summary>
        public virtual float Angle
        {
            get { return Native.GetPlayerFacingAngle(Id); }
            set { Native.SetPlayerFacingAngle(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the interior of this Player.
        /// </summary>
        public virtual int Interior
        {
            get { return Native.GetPlayerInterior(Id); }
            set { Native.SetPlayerInterior(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the virtual world of this Player.
        /// </summary>
        public virtual int VirtualWorld
        {
            get { return Native.GetPlayerVirtualWorld(Id); }
            set { Native.SetPlayerVirtualWorld(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the health of this Player.
        /// </summary>
        public virtual float Health
        {
            get { return Native.GetPlayerHealth(Id); }
            set { Native.SetPlayerHealth(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the armour of this Player.
        /// </summary>
        public virtual float Armour
        {
            get { return Native.GetPlayerArmour(Id); }
            set { Native.SetPlayerArmour(Id, value); }
        }

        /// <summary>
        ///     Gets the ammo of the Weapon this Player is currently holding.
        /// </summary>
        public virtual int WeaponAmmo
        {
            get { return Native.GetPlayerAmmo(Id); }
        }

        /// <summary>
        ///     Gets the WeaponState of the Weapon this Player is currently holding.
        /// </summary>
        public virtual WeaponState WeaponState
        {
            get { return (WeaponState) Native.GetPlayerWeaponState(Id); }
        }

        /// <summary>
        ///     Gets the Weapon this Player is currently holding.
        /// </summary>
        public virtual Weapon Weapon
        {
            get { return (Weapon) Native.GetPlayerWeapon(Id); }
        }

        /// <summary>
        ///     Gets the Player this Player is aiming at.
        /// </summary>
        public virtual GtaPlayer TargetPlayer
        {
            get
            {
                int target = Native.GetPlayerTargetPlayer(Id);
                return target == InvalidId ? null : Find(target);
            }
        }

        /// <summary>
        ///     Gets or sets the team this Player is in.
        /// </summary>
        public virtual int Team
        {
            get { return Native.GetPlayerTeam(Id); }
            set { Native.SetPlayerTeam(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the score of this Player.
        /// </summary>
        public virtual int Score
        {
            get { return Native.GetPlayerScore(Id); }
            set { Native.SetPlayerScore(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the drunkness level of this Player.
        /// </summary>
        public virtual int DrunkLevel
        {
            get { return Native.GetPlayerDrunkLevel(Id); }
            set { Native.SetPlayerDrunkLevel(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the Color of this Player.
        /// </summary>
        public virtual Color Color
        {
            get { return new Color(Native.GetPlayerColor(Id)); }
            set { Native.SetPlayerColor(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the skin of this Player.
        /// </summary>
        public virtual int Skin
        {
            get { return Native.GetPlayerSkin(Id); }
            set { Native.SetPlayerSkin(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the money of this Player.
        /// </summary>
        public virtual int Money
        {
            get { return Native.GetPlayerMoney(Id); }
            set
            {
                Native.ResetPlayerMoney(Id);
                Native.GivePlayerMoney(Id, value);
            }
        }

        /// <summary>
        ///     Gets the state of this Player.
        /// </summary>
        public virtual PlayerState State
        {
            get { return (PlayerState) Native.GetPlayerState(Id); }
        }

        /// <summary>
        ///     Gets the IP of this Player.
        /// </summary>
        public virtual string IP
        {
            get { return Native.GetPlayerIp(Id); }
        }

        /// <summary>
        ///     Gets the ping of this Player.
        /// </summary>
        public virtual int Ping
        {
            get { return Native.GetPlayerPing(Id); }
        }

        /// <summary>
        ///     Gets or sets the wanted level of this Player.
        /// </summary>
        public virtual int WantedLevel
        {
            get { return Native.GetPlayerWantedLevel(Id); }
            set { Native.SetPlayerWantedLevel(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the FightStyle of this Player.
        /// </summary>
        public virtual FightStyle FightStyle
        {
            get { return (FightStyle) Native.GetPlayerFightingStyle(Id); }
            set { Native.SetPlayerFightingStyle(Id, (int) value); }
        }

        /// <summary>
        ///     Gets or sets the velocity of this Player.
        /// </summary>
        public virtual Vector Velocity
        {
            get
            {
                float x, y, z;
                Native.GetPlayerVelocity(Id, out x, out y, out z);
                return new Vector(x, y, z);
            }
            set { Native.SetPlayerVelocity(Id, value.X, value.Y, value.Z); }
        }

        /// <summary>
        ///     Gets the vehicle seat this Player sits on.
        /// </summary>
        public virtual int VehicleSeat
        {
            get { return Native.GetPlayerVehicleSeat(Id); }
        }

        /// <summary>
        ///     Gets the index of the animation this Player is playing.
        /// </summary>
        public virtual int AnimationIndex
        {
            get { return Native.GetPlayerAnimationIndex(Id); }
        }

        /// <summary>
        ///     Gets or sets the SpecialAction of this Player.
        /// </summary>
        public virtual SpecialAction SpecialAction
        {
            get { return (SpecialAction) Native.GetPlayerSpecialAction(Id); }
            set { Native.SetPlayerSpecialAction(Id, value); }
        }

        /// <summary>
        ///     Gets or sets the position of this Players's camera.
        /// </summary>
        public virtual Vector CameraPosition
        {
            get { return Native.GetPlayerCameraPos(Id); }
            set { Native.SetPlayerCameraPos(Id, value); }
        }

        /// <summary>
        ///     Gets the front vector of this Player's camera.
        /// </summary>
        public virtual Vector CameraFrontVector
        {
            get { return Native.GetPlayerCameraFrontVector(Id); }
        }

        /// <summary>
        ///     Gets the mode of this Player's camera.
        /// </summary>
        public virtual CameraMode CameraMode
        {
            get { return (CameraMode) Native.GetPlayerCameraMode(Id); }
        }

        /// <summary>
        ///     Gets whether this Player is currently in any vehicle.
        /// </summary>
        public virtual bool InAnyVehicle
        {
            get { return Native.IsPlayerInAnyVehicle(Id); }
        }

        /// <summary>
        ///     Gets whether this Player is in his checkpoint.
        /// </summary>
        public virtual bool InCheckpoint
        {
            get { return Native.IsPlayerInCheckpoint(Id); }
        }

        /// <summary>
        ///     Gets whether this Player is in his race-checkpoint.
        /// </summary>
        public virtual bool InRaceCheckpoint
        {
            get { return Native.IsPlayerInRaceCheckpoint(Id); }
        }

        /// <summary>
        ///     Gets the Vehicle that this Player is surfing.
        /// </summary>
        public virtual GtaVehicle SurfingVehicle
        {
            get
            {
                int vehicleid = Native.GetPlayerSurfingVehicleID(Id);
                return vehicleid == GtaVehicle.InvalidId ? null : GtaVehicle.Find(vehicleid);
            }
        }

        /// <summary>
        ///     Gets the Vehicle this Player is currently in.
        /// </summary>
        public virtual GtaVehicle Vehicle
        {
            get
            {
                int vehicleid = Native.GetPlayerVehicleID(Id); //Returns 0, not Vehicle.InvalidId!
                return vehicleid == 0 ? null : GtaVehicle.Find(vehicleid);
            }
        }

        /// <summary>
        ///     Gets whether this Player is connected to the server.
        /// </summary>
        public virtual bool IsConnected
        {
            get { return Native.IsPlayerConnected(Id); }
        }


        /// <summary>
        ///     Gets or sets the rotation of this Player.
        /// </summary>
        /// <remarks>
        ///     Only the Z angle can be set!
        /// </remarks>
        public virtual Vector Rotation
        {
            get { return new Vector(0, 0, Angle); }
            set { Angle = value.Z; }
        }

        /// <summary>
        ///     Gets or sets the position of this Player.
        /// </summary>
        public virtual Vector Position
        {
            get { return Native.GetPlayerPos(Id); }
            set { Native.SetPlayerPos(Id, value); }
        }

        #endregion

        #region SAMP properties

        /// <summary>
        ///     Gets whether this Player is an actual player or an NPC.
        /// </summary>
        public virtual bool IsNPC
        {
            get { return Native.IsPlayerNPC(Id); }
        }

        /// <summary>
        ///     Gets whether this Player is logged into RCON.
        /// </summary>
        public virtual bool IsAdmin
        {
            get { return Native.IsPlayerAdmin(Id); }
        }

        /// <summary>
        ///     Gets whether this Player is alive;
        /// </summary>
        public virtual bool IsAlive
        {
            get { return !new[] {PlayerState.None, PlayerState.Spectating, PlayerState.Wasted}.Contains(State); }
        }

        /// <summary>
        ///     Gets this Player's network stats and saves them into a string.
        /// </summary>
        public virtual string NetworkStats
        {
            get { return Native.GetPlayerNetworkStats(Id); }
        }

        /// <summary>
        ///     Gets this Player's game version.
        /// </summary>
        public virtual string Version
        {
            get { return Native.GetPlayerVersion(Id); }
        }

        /// <summary>
        ///     Gets this Player's GPCI string.
        /// </summary>
        public virtual string GPCI
        {
            get { return Native.gpci(Id); }
        }

        /// <summary>
        ///     Gets the maximum number of players that can join the server, as set by the server var 'maxplayers' in server.cfg.
        /// </summary>
        public static int MaxPlayers
        {
            get { return Native.GetMaxPlayers(); }
        }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerConnect" /> is being called.
        ///     This callback is called when a player connects to the server.
        /// </summary>
        public event EventHandler<PlayerEventArgs> Connected;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerDisconnect" /> is being called.
        ///     This callback is called when a player disconnects from the server.
        /// </summary>
        public event EventHandler<PlayerDisconnectedEventArgs> Disconnected;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerDisconnect" /> is being called.
        ///     This callback is called after a player disconnects from the server.
        /// </summary>
        public event EventHandler<PlayerDisconnectedEventArgs> Cleanup;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerSpawn" /> is being called.
        ///     This callback is called when a player spawns.
        /// </summary>
        public event EventHandler<PlayerEventArgs> Spawned;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnGameModeInit" /> is being called.
        ///     This callback is triggered when the gamemode starts.
        /// </summary>
        public event EventHandler<PlayerDeathEventArgs> Died;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerText" /> is being called.
        ///     Called when a player sends a chat message.
        /// </summary>
        public event EventHandler<PlayerTextEventArgs> Text;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerCommandText" /> is being called.
        ///     This callback is called when a player enters a command into the client chat window, e.g. /help.
        /// </summary>
        public event EventHandler<PlayerTextEventArgs> CommandText;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerRequestClass" /> is being called.
        ///     Called when a player changes class at class selection (and when class selection first appears).
        /// </summary>
        public event EventHandler<PlayerRequestClassEventArgs> RequestClass;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerEnterVehicle" /> is being called.
        ///     This callback is called when a player starts to enter a vehicle, meaning the player is not in vehicle yet at the
        ///     time this callback is called.
        /// </summary>
        public event EventHandler<PlayerVehicleEventArgs> EnterVehicle;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerExitVehicle" /> is being called.
        ///     This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        ///     Not called if the player falls off a bike or is removed from a vehicle by other means such as using
        ///     <see cref="Native.SetPlayerPos(int,Vector)" />.
        /// </remarks>
        public event EventHandler<PlayerVehicleEventArgs> ExitVehicle;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerStateChange" /> is being called.
        ///     This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        ///     Not called if the player falls off a bike or is removed from a vehicle by other means such as using
        ///     <see cref="Native.SetPlayerPos(int,Vector)" />.
        /// </remarks>
        public event EventHandler<PlayerStateEventArgs> StateChanged;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerEnterCheckpoint" /> is being called.
        ///     This callback is called when a player enters the checkpoint set for that player.
        /// </summary>
        public event EventHandler<PlayerEventArgs> EnterCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerLeaveCheckpoint" /> is being called.
        ///     This callback is called when a player leaves the checkpoint set for that player.
        /// </summary>
        public event EventHandler<PlayerEventArgs> LeaveCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerEnterRaceCheckpoint" /> is being called.
        ///     This callback is called when a player enters a race checkpoint.
        /// </summary>
        public event EventHandler<PlayerEventArgs> EnterRaceCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerLeaveRaceCheckpoint" /> is being called.
        ///     This callback is called when a player leaves the race checkpoint.
        /// </summary>
        public event EventHandler<PlayerEventArgs> LeaveRaceCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerRequestSpawn" /> is being called.
        ///     Called when a player attempts to spawn via class selection.
        /// </summary>
        public event EventHandler<PlayerEventArgs> RequestSpawn;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerPickUpPickup" /> is being called.
        ///     Called when a player picks up a pickup created with <see cref="Native.CreatePickup" />.
        /// </summary>
        public event EventHandler<PlayerPickupEventArgs> PickUpPickup;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnEnterExitModShop" /> is being called.
        ///     This callback is called when a player enters or exits a mod shop.
        /// </summary>
        public event EventHandler<PlayerEnterModShopEventArgs> EnterExitModShop;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerSelectedMenuRow" /> is being called.
        ///     This callback is called when a player selects an item from a menu.
        /// </summary>
        public event EventHandler<PlayerSelectedMenuRowEventArgs> SelectedMenuRow;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerExitedMenu" /> is being called.
        ///     Called when a player exits a menu.
        /// </summary>
        public event EventHandler<PlayerEventArgs> ExitedMenu;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerInteriorChange" /> is being called.
        ///     Called when a player changes interior.
        /// </summary>
        /// <remarks>
        ///     This is also called when <see cref="Native.SetPlayerInterior" /> is used.
        /// </remarks>
        public event EventHandler<PlayerInteriorChangedEventArgs> InteriorChanged;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerKeyStateChange" /> is being called.
        ///     This callback is called when the state of any supported key is changed (pressed/released). Directional keys do not
        ///     trigger this callback.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> KeyStateChanged;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerUpdate" /> is being called.
        ///     This callback is called everytime a client/player updates the server with their status.
        /// </summary>
        /// <remarks>
        ///     This callback is called very frequently per second per player, only use it when you know what it's meant for.
        /// </remarks>
        public event EventHandler<PlayerEventArgs> Update;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerStreamIn" /> is being called.
        ///     This callback is called when a player is streamed by some other player's client.
        /// </summary>
        public event EventHandler<StreamPlayerEventArgs> StreamIn;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerStreamOut" /> is being called.
        ///     This callback is called when a player is streamed out from some other player's client.
        /// </summary>
        public event EventHandler<StreamPlayerEventArgs> StreamOut;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnDialogResponse" /> is being called.
        ///     This callback is called when a player responds to a dialog shown using <see cref="Native.ShowPlayerDialog" /> by
        ///     either clicking a button, pressing ENTER/ESC or double-clicking a list item (if using a list style dialog).
        /// </summary>
        public event EventHandler<DialogResponseEventArgs> DialogResponse;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerTakeDamage" /> is being called.
        ///     This callback is called when a player takes damage.
        /// </summary>
        public event EventHandler<PlayerDamageEventArgs> TakeDamage;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerGiveDamage" /> is being called.
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
        public event EventHandler<PlayerDamageEventArgs> GiveDamage;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerClickMap" /> is being called.
        ///     This callback is called when a player places a target/waypoint on the pause menu map (by right-clicking).
        /// </summary>
        /// <remarks>
        ///     The Z value provided is only an estimate; you may find it useful to use a plugin like the MapAndreas plugin to get
        ///     a more accurate Z coordinate (or for teleportation; use <see cref="Native.SetPlayerPosFindZ(int,Vector)" />).
        /// </remarks>
        public event EventHandler<PlayerClickMapEventArgs> ClickMap;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerClickTextDraw" /> is being called.
        ///     This callback is called when a player clicks on a textdraw.
        /// </summary>
        /// <remarks>
        ///     The clickable area is defined by <see cref="Native.TextDrawTextSize" />. The x and y parameters passed to that
        ///     function must not be zero or negative.
        /// </remarks>
        public event EventHandler<PlayerClickTextDrawEventArgs> ClickTextDraw;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerClickTextDraw" /> is being called.
        ///     This callback is called when a player cancels the textdraw select mode(ESC).
        /// </summary>
        public event EventHandler<PlayerEventArgs> CancelClickTextDraw;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerClickPlayerTextDraw" /> is being called.
        ///     This callback is called when a player clicks on a player-textdraw. It is not called when player cancels the select
        ///     mode (ESC) - however, <see cref="BaseMode.OnPlayerClickTextDraw" /> is.
        /// </summary>
        public event EventHandler<PlayerClickTextDrawEventArgs> ClickPlayerTextDraw;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerClickPlayer" /> is being called.
        ///     Called when a player double-clicks on a player on the scoreboard.
        /// </summary>
        /// <remarks>
        ///     There is currently only one 'source' (<see cref="PlayerClickSource.Scoreboard" />). The existence of this argument
        ///     suggests that more sources may be supported in the future.
        /// </remarks>
        public event EventHandler<PlayerClickPlayerEventArgs> ClickPlayer;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerEditObject" /> is being called.
        ///     This callback is called when a player ends object edition mode.
        /// </summary>
        public event EventHandler<PlayerEditObjectEventArgs> EditObject;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerEditAttachedObject" /> is being called.
        ///     This callback is called when a player ends attached object edition mode.
        /// </summary>
        /// <remarks>
        ///     Editions should be discarded if response was '0' (cancelled). This must be done by storing the offsets etc. in an
        ///     array BEFORE using EditAttachedObject.
        /// </remarks>
        public event EventHandler<PlayerEditAttachedObjectEventArgs> EditAttachedObject;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerSelectObject" /> is being called.
        ///     This callback is called when a player selects an object after <see cref="Native.SelectObject" /> has been used.
        /// </summary>
        public event EventHandler<PlayerSelectObjectEventArgs> SelectObject;

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnPlayerWeaponShot" /> is being called.
        ///     This callback is called when a player fires a shot from a weapon.
        /// </summary>
        /// <remarks>
        ///     <see cref="BulletHitType.None" />: the fX, fY and fZ parameters are normal coordinates;
        ///     Others: the fX, fY and fZ are offsets from the center of hitid.
        /// </remarks>
        public event EventHandler<WeaponShotEventArgs> WeaponShot;

        #endregion

        #region Players natives

        /// <summary>
        ///     This function can be used to change the spawn information of a specific player. It allows you to automatically set
        ///     someone's spawn weapons, their team, skin and spawn position, normally used in case of minigames or automatic-spawn
        ///     systems. This function is more crash-safe then using <see cref="Native.SetPlayerSkin" /> in
        ///     <see cref="BaseMode.OnPlayerSpawn" /> and/or <see cref="BaseMode.OnPlayerRequestClass" />.
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
        public virtual void SetSpawnInfo(int team, int skin, Vector position, float rotation,
            Weapon weapon1 = Weapon.None,
            int weapon1Ammo = 0, Weapon weapon2 = Weapon.None, int weapon2Ammo = 0, Weapon weapon3 = Weapon.None,
            int weapon3Ammo = 0)
        {
            CheckDisposure();

            Native.SetSpawnInfo(Id, team, skin, position, rotation, weapon1, weapon1Ammo, weapon2, weapon2Ammo,
                weapon3, weapon3Ammo);
        }

        /// <summary>
        ///     (Re)Spawns a player.
        /// </summary>
        public virtual void Spawn()
        {
            CheckDisposure();

            Native.SpawnPlayer(Id);
        }

        /// <summary>
        ///     Restore the camera to a place behind the player, after using a function like SetPlayerCameraPos.
        /// </summary>
        public virtual void PutCameraBehindPlayer()
        {
            Native.SetCameraBehindPlayer(Id);
        }

        /// <summary>
        ///     This sets this Player's position then adjusts the Player's z-coordinate to the nearest solid ground under the
        ///     position.
        /// </summary>
        /// <param name="position">The position to move this Player to.</param>
        public virtual void SetPositionFindZ(Vector position)
        {
            CheckDisposure();

            Native.SetPlayerPosFindZ(Id, position);
        }

        /// <summary>
        ///     Check if this Player is in range of a point.
        /// </summary>
        /// <param name="range">The furthest distance the player can be from the point to be in range.</param>
        /// <param name="point">The point to check the range to.</param>
        /// <returns>True if this Player is in range of the point, otherwise False.</returns>
        public virtual bool IsInRangeOfPoint(float range, Vector point)
        {
            CheckDisposure();

            return Native.IsPlayerInRangeOfPoint(Id, range, point);
        }

        /// <summary>
        ///     Calculate the distance between this Player and a map coordinate.
        /// </summary>
        /// <param name="point">The point to calculate the distance from.</param>
        /// <returns>The distance between the player and the point as a float.</returns>
        public virtual float GetDistanceFromPoint(Vector point)
        {
            CheckDisposure();

            return Native.GetPlayerDistanceFromPoint(Id, point);
        }

        /// <summary>
        ///     Checks if a Player is streamed in this Player's client.
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
        public virtual bool IsPlayerStreamedIn(GtaPlayer other)
        {
            CheckDisposure();

            if (other == null)
                throw new ArgumentNullException("other");

            return Native.IsPlayerStreamedIn(other.Id, Id);
        }

        /// <summary>
        ///     Set the ammo of this Player's weapon.
        /// </summary>
        /// <param name="weapon">The weapon to set the ammo of.</param>
        /// <param name="ammo">The amount of ammo to set.</param>
        public virtual void SetAmmo(Weapon weapon, int ammo)
        {
            CheckDisposure();

            Native.SetPlayerAmmo(Id, (int) weapon, ammo);
        }

        /// <summary>
        ///     Give this Player a Weapon with a specified amount of ammo.
        /// </summary>
        /// <param name="weapon">The Weapon to give to this Player.</param>
        /// <param name="ammo">The amount of ammo to give to this Player.</param>
        public virtual void GiveWeapon(Weapon weapon, int ammo)
        {
            CheckDisposure();

            Native.GivePlayerWeapon(Id, (int) weapon, ammo);
        }


        /// <summary>
        ///     Removes all weapons from this Player.
        /// </summary>
        public virtual void ResetWeapons()
        {
            CheckDisposure();

            Native.ResetPlayerWeapons(Id);
        }

        /// <summary>
        ///     Sets the armed weapon of this Player.
        /// </summary>
        /// <param name="weapon">The weapon that the player should be armed with.</param>
        public virtual void SetArmedWeapon(Weapon weapon)
        {
            CheckDisposure();

            Native.SetPlayerArmedWeapon(Id, (int) weapon);
        }

        /// <summary>
        ///     Get the Weapon and ammo in this Player's weapon slot.
        /// </summary>
        /// <param name="slot">The weapon slot to get data for (0-12).</param>
        /// <param name="weapon">The variable in which to store the weapon, passed by reference.</param>
        /// <param name="ammo">The variable in which to store the ammo, passed by reference.</param>
        public virtual void GetWeaponData(int slot, out Weapon weapon, out int ammo)
        {
            CheckDisposure();

            int weaponid;
            Native.GetPlayerWeaponData(Id, slot, out weaponid, out ammo);
            weapon = (Weapon) weaponid;
        }

        /// <summary>
        ///     Give money to this Player.
        /// </summary>
        /// <param name="money">The amount of money to give this Player. Use a minus value to take money.</param>
        public virtual void GiveMoney(int money)
        {
            CheckDisposure();

            Native.GivePlayerMoney(Id, money);
        }

        /// <summary>
        ///     Reset this Player's money to $0.
        /// </summary>
        public virtual void ResetMoney()
        {
            CheckDisposure();

            Native.ResetPlayerMoney(Id);
        }

        /// <summary>
        ///     Check which keys this Player is pressing.
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
            CheckDisposure();

            int keysDown;
            Native.GetPlayerKeys(Id, out keysDown, out updown, out leftright);
            keys = (Keys) keysDown;
        }

        /// <summary>
        ///     Sets the clock of this Player to a specific value. This also changes the daytime. (night/day etc.)
        /// </summary>
        /// <param name="hour">Hour to set (0-23).</param>
        /// <param name="minutes">Minutes to set (0-59).</param>
        public virtual void SetTime(int hour, int minutes)
        {
            CheckDisposure();

            Native.SetPlayerTime(Id, hour, minutes);
        }

        /// <summary>
        ///     Get this Player's current game time. Set by <see cref="Native.SetWorldTime" />, <see cref="Native.SetWorldTime" />,
        ///     or by <see cref="ToggleClock" />.
        /// </summary>
        /// <param name="hour">The variable to store the hour in, passed by reference.</param>
        /// <param name="minutes">The variable to store the minutes in, passed by reference.</param>
        public virtual void GetTime(out int hour, out int minutes)
        {
            CheckDisposure();

            Native.GetPlayerTime(Id, out hour, out minutes);
        }

        /// <summary>
        ///     Show/Hide the in-game clock (top right corner) for this Player.
        /// </summary>
        /// <remarks>
        ///     Time is not synced with other players!
        /// </remarks>
        /// <param name="toggle">True to show, False to hide.</param>
        public virtual void ToggleClock(bool toggle)
        {
            CheckDisposure();

            Native.TogglePlayerClock(Id, toggle);
        }

        /// <summary>
        ///     Set this Player's weather. If <see cref="ToggleClock" /> has been used to enable the clock, weather changes will
        ///     interpolate (gradually change), otherwise will change instantly.
        /// </summary>
        /// <param name="weather">The weather to set.</param>
        public virtual void SetWeather(int weather)
        {
            CheckDisposure();

            Native.SetPlayerWeather(Id, weather);
        }

        /// <summary>
        ///     Forces this Player to go back to class selection.
        /// </summary>
        /// <remarks>
        ///     The player will not return to class selection until they re-spawn. This can be achieved with
        ///     <see cref="ToggleSpectating" />
        /// </remarks>
        public virtual void ForceClassSelection()
        {
            CheckDisposure();

            Native.ForceClassSelection(Id);
        }

        /// <summary>
        ///     Display the cursor and allow this Player to select a textdraw.
        /// </summary>
        /// <param name="hoverColor">The color of the textdraw when hovering over with mouse.</param>
        public virtual void SelectTextDraw(Color hoverColor)
        {
            CheckDisposure();

            Native.SelectTextDraw(Id, hoverColor);
        }

        /// <summary>
        ///     Cancel textdraw selection with the mouse.
        /// </summary>
        public virtual void CancelSelectTextDraw()
        {
            CheckDisposure();

            Native.CancelSelectTextDraw(Id);
        }

        /// <summary>
        ///     This function plays a crime report for this Player - just like in single-player when CJ commits a crime.
        /// </summary>
        /// <param name="suspectid">The ID of the suspect player which will be described in the crime report.</param>
        /// <param name="crime">The crime ID, which will be reported as a 10-code (i.e. 10-16 if 16 was passed as the crimeid).</param>
        public virtual void PlayCrimeReport(int suspectid, int crime)
        {
            CheckDisposure();

            Native.PlayCrimeReportForPlayer(Id, suspectid, crime);
        }

        /// <summary>
        ///     Play an 'audio stream' for this Player. Normal audio files also work (e.g. MP3).
        /// </summary>
        /// <param name="url">
        ///     The url to play. Valid formats are mp3 and ogg/vorbis. A link to a .pls (playlist) file will play
        ///     that playlist.
        /// </param>
        /// <param name="position">The position at which to play the audio. Has no effect unless usepos is set to True.</param>
        /// <param name="distance">The distance over which the audio will be heard. Has no effect unless usepos is set to True.</param>
        public virtual void PlayAudioStream(string url, Vector position, float distance)
        {
            CheckDisposure();

            Native.PlayAudioStreamForPlayer(Id, url, position.X, position.Y, position.Z, distance, true);
        }

        /// <summary>
        ///     Play an 'audio stream' for this Player. Normal audio files also work (e.g. MP3).
        /// </summary>
        /// <param name="url">
        ///     The url to play. Valid formats are mp3 and ogg/vorbis. A link to a .pls (playlist) file will play
        ///     that playlist.
        /// </param>
        public virtual void PlayAudioStream(string url)
        {
            CheckDisposure();

            Native.PlayAudioStreamForPlayer(Id, url, 0, 0, 0, 0, false);
        }

        /// <summary>
        ///     Stops the current audio stream for this Player.
        /// </summary>
        public virtual void StopAudioStream()
        {
            CheckDisposure();

            Native.StopAudioStreamForPlayer(Id);
        }

        /// <summary>
        ///     Loads or unloads an interior script for this Player. (for example the ammunation menu)
        /// </summary>
        /// <param name="shopname"></param>
        public virtual void SetShopName(string shopname)
        {
            CheckDisposure();

            Native.SetPlayerShopName(Id, shopname);
        }

        /// <summary>
        ///     Set the skill level of a certain weapon type for this Player.
        /// </summary>
        /// <remarks>
        ///     The skill parameter is NOT the weapon ID, it is the skill type.
        /// </remarks>
        /// <param name="skill">The weapon type you want to set the skill of.</param>
        /// <param name="level">
        ///     The skill level to set for that weapon, ranging from 0 to 999. (A level out of range will max it
        ///     out)
        /// </param>
        public virtual void SetSkillLevel(WeaponSkill skill, int level)
        {
            CheckDisposure();

            Native.SetPlayerSkillLevel(Id, (int) skill, level);
        }

        /// <summary>
        ///     Removes a standard San Andreas model for this Player within a specified range.
        /// </summary>
        /// <param name="modelid">The model to remove.</param>
        /// <param name="point">The point around which the objects will be removed.</param>
        /// <param name="radius">The radius. Objects within this radius from the coordinates above will be removed.</param>
        public virtual void RemoveBuilding(int modelid, Vector point, float radius)
        {
            CheckDisposure();

            Native.RemoveBuildingForPlayer(Id, modelid, point.X, point.Y, point.Z, radius);
        }

        /// <summary>
        ///     Attach an object to a specific bone on this Player.
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
        public virtual bool SetAttachedObject(int index, int modelid, int bone, Vector offset, Vector rotation,
            Vector scale, Color materialcolor1, Color materialcolor2)
        {
            CheckDisposure();

            return Native.SetPlayerAttachedObject(Id, index, modelid, bone, offset.X, offset.Y, offset.Z,
                rotation.X, rotation.Y, rotation.Z, scale.X, scale.Y, scale.Z,
                materialcolor1.GetColorValue(ColorFormat.ARGB), materialcolor2.GetColorValue(ColorFormat.ARGB));
        }

        /// <summary>
        ///     Remove an attached object from this Player.
        /// </summary>
        /// <param name="index">The index of the object to remove (set with <see cref="SetAttachedObject" />).</param>
        /// <returns>True on success, False otherwise.</returns>
        public virtual bool RemoveAttachedObject(int index)
        {
            CheckDisposure();

            return Native.RemovePlayerAttachedObject(Id, index);
        }

        /// <summary>
        ///     Check if this Player has an object attached in the specified index (slot).
        /// </summary>
        /// <param name="index">The index (slot) to check.</param>
        /// <returns>True if the slot is used, False otherwise.</returns>
        public virtual bool IsAttachedObjectSlotUsed(int index)
        {
            CheckDisposure();

            return Native.IsPlayerAttachedObjectSlotUsed(Id, index);
        }

        /// <summary>
        ///     Enter edition mode for an attached object.
        /// </summary>
        /// <param name="index">The index (slot) of the attached object to edit.</param>
        /// <returns>True on success, False otherwise.</returns>
        public virtual bool DoEditAttachedObject(int index)
        {
            CheckDisposure();

            return Native.EditAttachedObject(Id, index);
        }

        /// <summary>
        ///     Creates a chat bubble above this Player's name tag.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="color">The text color.</param>
        /// <param name="drawdistance">The distance from where players are able to see the chat bubble.</param>
        /// <param name="expiretime">The time in miliseconds the bubble should be displayed for.</param>
        public virtual void SetChatBubble(string text, Color color, float drawdistance,
            int expiretime)
        {
            CheckDisposure();

            Native.SetPlayerChatBubble(Id, text, color.GetColorValue(ColorFormat.RGBA), drawdistance, expiretime);
        }

        /// <summary>
        ///     Puts this Player in a vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle for the player to be put in.</param>
        /// <param name="seatid">The ID of the seat to put the player in.</param>
        public virtual void PutInVehicle(GtaVehicle vehicle, int seatid)
        {
            CheckDisposure();

            if (vehicle == null)
                throw new ArgumentNullException("vehicle");

            Native.PutPlayerInVehicle(Id, vehicle.Id, seatid);
        }

        /// <summary>
        ///     Puts this Player in a vehicle as driver.
        /// </summary>
        /// <param name="vehicle">The vehicle for the player to be put in.</param>
        public virtual void PutInVehicle(GtaVehicle vehicle)
        {
            PutInVehicle(vehicle, 0);
        }

        /// <summary>
        ///     Removes/ejects this Player from his vehicle.
        /// </summary>
        /// <remarks>
        ///     The exiting animation is not synced for other players.
        ///     This function will not work when used in <see cref="BaseMode.OnPlayerEnterVehicle" />, because the player isn't in
        ///     the vehicle when the callback is called. Use <see cref="BaseMode.OnPlayerStateChange" /> instead.
        /// </remarks>
        public virtual void RemoveFromVehicle()
        {
            CheckDisposure();

            Native.RemovePlayerFromVehicle(Id);
        }

        /// <summary>
        ///     Toggles whether this Player can control themselves, basically freezes them.
        /// </summary>
        /// <param name="toggle">False to freeze the player or True to unfreeze them.</param>
        public virtual void ToggleControllable(bool toggle)
        {
            CheckDisposure();

            Native.TogglePlayerControllable(Id, toggle);
        }

        /// <summary>
        ///     Plays the specified sound for this Player at a specific point.
        /// </summary>
        /// <param name="soundid">The sound to play.</param>
        /// <param name="point">Point for the sound to play at.</param>
        public virtual void PlaySound(int soundid, Vector point)
        {
            CheckDisposure();

            Native.PlayerPlaySound(Id, soundid, point.X, point.Y, point.Z);
        }

        /// <summary>
        ///     Plays the specified sound for this Player.
        /// </summary>
        /// <param name="soundid">The sound to play.</param>
        public virtual void PlaySound(int soundid)
        {
            CheckDisposure();

            Native.PlayerPlaySound(Id, soundid, 0, 0, 0);
        }

        /// <summary>
        ///     Apply an animation to this Player.
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
            CheckDisposure();

            Native.ApplyAnimation(Id, animlib, animname, fDelta, loop, lockx, locky, freeze, time, forcesync);
        }

        /// <summary>
        ///     Apply an animation to this Player.
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
            CheckDisposure();

            Native.ApplyAnimation(Id, animlib, animname, fDelta, loop, lockx, locky, freeze, time, false);
        }

        /// <summary>
        ///     Clears all animations for this Player.
        /// </summary>
        /// <param name="forcesync">Specifies whether the animation should be shown to streamed in players.</param>
        public virtual void ClearAnimations(bool forcesync)
        {
            CheckDisposure();

            Native.ClearAnimations(Id, forcesync);
        }

        /// <summary>
        ///     Clears all animations for this Player.
        /// </summary>
        public virtual void ClearAnimations()
        {
            CheckDisposure();

            Native.ClearAnimations(Id, false);
        }

        /// <summary>
        ///     Get the animation library/name this Player is playing.
        /// </summary>
        /// <param name="animlib">String variable that stores the animation library.</param>
        /// <param name="animname">String variable that stores the animation name.</param>
        /// <returns>True on success, False otherwise.</returns>
        public virtual bool GetAnimationName(out string animlib, out string animname)
        {
            CheckDisposure();

            return Native.GetAnimationName(AnimationIndex, out animlib, 64, out animname, 64);
        }

        /// <summary>
        ///     Sets a checkpoint (red circle) for this Player. Also shows a red blip on the radar.
        /// </summary>
        /// <remarks>
        ///     Checkpoints created on server-created objects will appear down on the 'real' ground, but will still function
        ///     correctly.
        ///     There is no fix available for this issue. A pickup can be used instead.
        /// </remarks>
        /// <param name="point">The point to set the checkpoint at.</param>
        /// <param name="size">The size of the checkpoint.</param>
        public virtual void SetCheckpoint(Vector point, float size)
        {
            CheckDisposure();

            Native.SetPlayerCheckpoint(Id, point, size);
        }

        /// <summary>
        ///     Disable any initialized checkpoints for this Player.
        /// </summary>
        public virtual void DisableCheckpoint()
        {
            CheckDisposure();

            Native.DisablePlayerCheckpoint(Id);
        }

        /// <summary>
        ///     Creates a race checkpoint. When this Player enters it, the <see cref="EnterRaceCheckpoint" /> callback is called.
        /// </summary>
        /// <param name="type">Type of checkpoint.</param>
        /// <param name="point">The point to set the checkpoint at.</param>
        /// <param name="nextPosition">Coordinates of the next point, for the arrow facing direction.</param>
        /// <param name="size">Size (diameter) of the checkpoint</param>
        public virtual void SetRaceCheckpoint(CheckpointType type, Vector point, Vector nextPosition, float size)
        {
            CheckDisposure();

            Native.SetPlayerRaceCheckpoint(Id, type, point, nextPosition, size);
        }

        /// <summary>
        ///     Disable any initialized race checkpoints for this Player.
        /// </summary>
        public virtual void DisableRaceCheckpoint()
        {
            CheckDisposure();

            Native.DisablePlayerRaceCheckpoint(Id);
        }

        /// <summary>
        ///     Set the world boundaries for this Player - players can not go out of the boundaries.
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
            CheckDisposure();

            Native.SetPlayerWorldBounds(Id, xMax, xMin, yMax, yMin);
        }

        /// <summary>
        ///     Change the colour of this Player's nametag and radar blip for another Player.
        /// </summary>
        /// <param name="player">The player whose color will be changed.</param>
        /// <param name="color">New color.</param>
        public virtual void SetPlayerMarker(GtaPlayer player, Color color)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            Native.SetPlayerMarkerForPlayer(Id, player.Id, color.GetColorValue(ColorFormat.RGBA));
        }

        /// <summary>
        ///     This functions allows you to toggle the drawing of player nametags, healthbars and armor bars which display above
        ///     their head. For use of a similar function like this on a global level, <see cref="Native.ShowNameTags" /> function.
        /// </summary>
        /// <remarks>
        ///     <see cref="Native.ShowNameTags" /> must be set to True to be able to show name tags with
        ///     <see cref="ShowNameTagForPlayer" />.
        /// </remarks>
        /// <param name="player">Player whose name tag will be shown or hidden.</param>
        /// <param name="show">True to show name tag, False to hide name tag.</param>
        public virtual void ShowNameTagForPlayer(GtaPlayer player, bool show)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            Native.ShowPlayerNameTagForPlayer(Id, player.Id, show);
        }

        /// <summary>
        ///     This function allows you to place your own icons on the map, enabling you to emphasise the locations of banks,
        ///     airports or whatever else you want. A total of 63 icons are available in GTA: San Andreas, all of which can be used
        ///     using this function. You can also specify the color of the icon, which allows you to change the square icon (ID:
        ///     0).
        /// </summary>
        /// <param name="iconid">The player's icon ID, ranging from 0 to 99, to be used in RemovePlayerMapIcon.</param>
        /// <param name="position">The coordinates of the place where you want the icon to be.</param>
        /// <param name="markertype">The icon to set.</param>
        /// <param name="color">The color of the icon, this should only be used with the square icon (ID: 0).</param>
        /// <param name="style">The style of icon.</param>
        /// <returns>True if it was successful, False otherwise (e.g. the player isn't connected).</returns>
        public virtual bool SetMapIcon(int iconid, Vector position, PlayerMarkersMode markertype, Color color,
            MapIconType style)
        {
            CheckDisposure();

            return Native.SetPlayerMapIcon(Id, iconid, position, markertype, color, (int) style);
        }

        /// <summary>
        ///     Removes a map icon that was set earlier for this Player.
        /// </summary>
        /// <param name="iconid">The ID of the icon to remove. This is the second parameter of <see cref="SetMapIcon" />.</param>
        public virtual void RemovePlayerMapIcon(int iconid)
        {
            CheckDisposure();

            Native.RemovePlayerMapIcon(Id, iconid);
        }

        /// <summary>
        ///     Set the direction this Player's camera looks at. To be used in combination with SetPlayerCameraPos.
        /// </summary>
        /// <param name="point">The coordinates for this Player's camera to look at.</param>
        /// <param name="cut">The style the camera-position changes.</param>
        public virtual void SetCameraLookAt(Vector point, CameraCut cut)
        {
            CheckDisposure();

            Native.SetPlayerCameraLookAt(Id, point, cut);
        }

        /// <summary>
        ///     Set the direction this Player's camera looks at. To be used in combination with SetPlayerCameraPos.
        /// </summary>
        /// <param name="point">The coordinates for this Player's camera to look at.</param>
        public virtual void SetCameraLookAt(Vector point)
        {
            CheckDisposure();

            Native.SetPlayerCameraLookAt(Id, point, CameraCut.Cut);
        }

        /// <summary>
        ///     Move this Player's camera from one position to another, within the set time.
        /// </summary>
        /// <param name="from">The position the camera should start to move from.</param>
        /// <param name="to">The position the camera should move to.</param>
        /// <param name="time">Time in milliseconds.</param>
        /// <param name="cut">The jumpcut to use. Defaults to CameraCut.Cut. Set to CameraCut. Move for a smooth movement.</param>
        public virtual void InterpolateCameraPos(Vector from, Vector to, int time, CameraCut cut)
        {
            CheckDisposure();

            Native.InterpolateCameraPos(Id, from, to, time, cut);
        }

        /// <summary>
        ///     Interpolate this Player's camera's 'look at' point between two coordinates with a set speed.
        /// </summary>
        /// <param name="from">The position the camera should start to move from.</param>
        /// <param name="to">The position the camera should move to.</param>
        /// <param name="time">Time in milliseconds to complete interpolation.</param>
        /// <param name="cut">The 'jumpcut' to use. Defaults to CameraCut.Cut (pointless). Set to CameraCut.Move for interpolation.</param>
        public virtual void InterpolateCameraLookAt(Vector from, Vector to, int time, CameraCut cut)
        {
            CheckDisposure();

            Native.InterpolateCameraLookAt(Id, from, to, time, cut);
        }

        /// <summary>
        ///     Checks if this Player is in a specific vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <returns>True if player is in the vehicle, otherwise False.</returns>
        public virtual bool IsInVehicle(GtaVehicle vehicle)
        {
            CheckDisposure();

            return Native.IsPlayerInVehicle(Id, vehicle.Id);
        }

        /// <summary>
        ///     Toggle stunt bonuses for this Player.
        /// </summary>
        /// <param name="enable">True to enable stunt bonuses, False to disable them.</param>
        public virtual void EnableStuntBonus(bool enable)
        {
            CheckDisposure();

            Native.EnableStuntBonusForPlayer(Id, enable);
        }

        /// <summary>
        ///     Toggle this Player's spectate mode.
        /// </summary>
        /// <remarks>
        ///     When the spectating is turned off, OnPlayerSpawn will automatically be called.
        /// </remarks>
        /// <param name="toggle">True to enable spectating and False to disable.</param>
        public virtual void ToggleSpectating(bool toggle)
        {
            CheckDisposure();

            Native.TogglePlayerSpectating(Id, toggle);
        }

        /// <summary>
        ///     Makes this Player spectate (watch) another player.
        /// </summary>
        /// <remarks>
        ///     Order is CRITICAL! Ensure that you use <see cref="ToggleSpectating" /> before <see cref="SpectatePlayer(GtaPlayer,SpectateMode)" />.
        /// </remarks>
        /// <param name="targetPlayer">The Player that should be spectated.</param>
        /// <param name="mode">The mode to spectate with.</param>
        public virtual void SpectatePlayer(GtaPlayer targetPlayer, SpectateMode mode)
        {
            CheckDisposure();

            if (targetPlayer == null)
                throw new ArgumentNullException("targetPlayer");

            Native.PlayerSpectatePlayer(Id, targetPlayer.Id, (int) mode);
        }

        /// <summary>
        ///     Makes this Player spectate (watch) another player.
        /// </summary>
        /// <remarks>
        ///     Order is CRITICAL! Ensure that you use <see cref="ToggleSpectating" /> before <see cref="SpectatePlayer(GtaPlayer,SpectateMode)" />.
        /// </remarks>
        /// <param name="targetPlayer">The Player that should be spectated.</param>
        public virtual void SpectatePlayer(GtaPlayer targetPlayer)
        {
            CheckDisposure();

            if (targetPlayer == null)
                throw new ArgumentNullException("targetPlayer");

            SpectatePlayer(targetPlayer, SpectateMode.Normal);
        }

        /// <summary>
        ///     Sets this Player to spectate another vehicle, i.e. see what its driver sees.
        /// </summary>
        /// <remarks>
        ///     Order is CRITICAL! Ensure that you use <see cref="ToggleSpectating" /> before <see cref="SpectateVehicle(GtaVehicle,SpectateMode)" />.
        /// </remarks>
        /// <param name="targetVehicle">The vehicle to spectate.</param>
        /// <param name="mode">Spectate mode.</param>
        public virtual void SpectateVehicle(GtaVehicle targetVehicle, SpectateMode mode)
        {
            CheckDisposure();

            if (targetVehicle == null)
                throw new ArgumentNullException("targetVehicle");

            Native.PlayerSpectateVehicle(Id, targetVehicle.Id, (int) mode);
        }

        /// <summary>
        ///     Sets this Player to spectate another vehicle, i.e. see what its driver sees.
        /// </summary>
        /// <remarks>
        ///     Order is CRITICAL! Ensure that you use <see cref="ToggleSpectating" /> before <see cref="SpectateVehicle(GtaVehicle,SpectateMode)" />.
        /// </remarks>
        /// <param name="targetVehicle">The vehicle to spectate.</param>
        public virtual void SpectateVehicle(GtaVehicle targetVehicle)
        {
            CheckDisposure();

            if (targetVehicle == null)
                throw new ArgumentNullException("targetVehicle");

            SpectateVehicle(targetVehicle, SpectateMode.Normal);
        }

        /// <summary>
        ///     Returns the ID of the object this Player is surfing on.
        /// </summary>
        /// <returns>
        ///     The ID of the moving object the player is surfing. If the player isn't surfing a moving object, it will return
        ///     <see cref="Misc.InvalidObjectId" />
        /// </returns>
        public virtual int GetSurfingObjectID()
        {
            CheckDisposure();

            return Native.GetPlayerSurfingObjectID(Id);
        }

        /// <summary>
        ///     Starts recording this Player's movements to a file, which can then be reproduced by an NPC.
        /// </summary>
        /// <param name="recordtype">The type of recording.</param>
        /// <param name="recordname">
        ///     Name of the file which will hold the recorded data. It will be saved in scriptfiles, with an
        ///     automatically added .rec extension.
        /// </param>
        public virtual void StartRecordingPlayerData(PlayerRecordingType recordtype, string recordname)
        {
            CheckDisposure();

            Native.StartRecordingPlayerData(Id, (int) recordtype, recordname);
        }

        /// <summary>
        ///     Stops all the recordings that had been started with <see cref="StartRecordingPlayerData" /> for this Player.
        /// </summary>
        public virtual void StopRecordingPlayerData()
        {
            CheckDisposure();

            Native.StopRecordingPlayerData(Id);
        }

        #endregion

        #region SAMP natives

        /// <summary>
        ///     This function sends a message to this Player with a chosen color in the chat. The whole line in the chatbox will be
        ///     in the set color unless colour embedding is used.
        /// </summary>
        /// <param name="color">The color of the message.</param>
        /// <param name="message">The text that will be displayed.</param>
        public virtual void SendClientMessage(Color color, string message)
        {
            CheckDisposure();

            if (message.Length > 144)
            {
                Native.SendClientMessage(Id, color.GetColorValue(ColorFormat.RGBA), message.Substring(0, 144));
                SendClientMessage(color, message.Substring(144));
            }
            else
            {
                Native.SendClientMessage(Id, color.GetColorValue(ColorFormat.RGBA), message);
            }
        }

        /// <summary>
        ///     Kicks this Player from the server. They will have to quit the game and re-connect if they wish to continue playing.
        /// </summary>
        public virtual void Kick()
        {
            CheckDisposure();

            Native.Kick(Id);
        }

        /// <summary>
        ///     Ban this Player. The ban will be IP-based, and be saved in the samp.ban file in the
        ///     server's root directory. <see cref="Ban(string)" /> allows you to ban with a reason, while you can ban and unban
        ///     IPs
        ///     using the RCON banip and unbanip commands.
        /// </summary>
        public virtual void Ban()
        {
            CheckDisposure();

            Native.Ban(Id);
        }

        /// <summary>
        ///     Ban this Player with a reason.
        /// </summary>
        /// <param name="reason">The reason for the ban.</param>
        public virtual void Ban(string reason)
        {
            CheckDisposure();

            Native.BanEx(Id, reason);
        }

        /// <summary>
        ///     This function sends a message to this Player with a chosen color in the chat. The whole line in the chatbox will be
        ///     in the set color unless colour embedding is used.
        /// </summary>
        /// <param name="color">The color of the message.</param>
        /// <param name="messageFormat">The composite format string of the text that will be displayed (max 144 characters).</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public virtual void SendClientMessage(Color color, string messageFormat, params object[] args)
        {
            SendClientMessage(color, string.Format(messageFormat, args));
        }

        /// <summary>
        ///     This function sends a message to this Player in white in the chat. The whole line in the chatbox will be
        ///     in the set color unless colour embedding is used.
        /// </summary>
        /// <param name="message">The text that will be displayed.</param>
        public virtual void SendClientMessage(string message)
        {
            SendClientMessage(Color.White, message);
        }

        /// <summary>
        ///     This function sends a message to this Player in white in the chat. The whole line in the chatbox will be
        ///     in the set color unless colour embedding is used.
        /// </summary>
        /// <param name="messageFormat">The composite format string of the text that will be displayed (max 144 characters).</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public virtual void SendClientMessage(string messageFormat, params object[] args)
        {
            SendClientMessage(Color.White, string.Format(messageFormat, args));
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
                Native.SendClientMessageToAll(color.GetColorValue(ColorFormat.RGBA), message.Substring(0, 144));
                SendClientMessageToAll(color, message.Substring(144));
            }
            else
            {
                Native.SendClientMessageToAll(color.GetColorValue(ColorFormat.RGBA), message);
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
            SendClientMessageToAll(Color.White, message);
        }

        /// <summary>
        ///     Displays a message in white in chat to all players..
        /// </summary>
        /// <param name="messageFormat">The composite format string of the text that will be displayed.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void SendClientMessageToAll(string messageFormat, params object[] args)
        {
            SendClientMessageToAll(Color.White, string.Format(messageFormat, args));
        }

        /// <summary>
        ///     Sends a message in the name this Player to another player on the server. The message will appear in the chat box
        ///     but can only be seen by <paramref name="receiver" />. The line will start with the this Player's name in his color,
        ///     followed by the <paramref name="message" /> in white.
        /// </summary>
        /// <param name="receiver">The Player who will recieve the message</param>
        /// <param name="message">The message that will be sent.</param>
        public virtual void SendPlayerMessageToPlayer(GtaPlayer receiver, string message)
        {
            CheckDisposure();

            if (receiver == null)
                throw new ArgumentNullException("receiver");

            Native.SendPlayerMessageToPlayer(receiver.Id, Id, message);
        }

        /// <summary>
        ///     Sends a message in the name of this Player to all other players on the server. The line will start with the this
        ///     Player's name in their color, followed by the <paramref name="message" /> in white.
        /// </summary>
        /// <param name="message">The message that will be sent.</param>
        public virtual void SendPlayerMessageToAll(string message)
        {
            CheckDisposure();

            Native.SendPlayerMessageToAll(Id, message);
        }

        /// <summary>
        ///     Shows 'game text' (on-screen text) for a certain length of time for all players.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="time">The duration of the text being shown in milliseconds.</param>
        /// <param name="style">The style of text to be displayed.</param>
        public static void GameTextForAll(string text, int time, int style)
        {
            Native.GameTextForAll(text, time, style);
        }

        /// <summary>
        ///     Shows 'game text' (on-screen text) for a certain length of time for this Player.
        /// </summary>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="time">The duration of the text being shown in milliseconds.</param>
        /// <param name="style">The style of text to be displayed.</param>
        public virtual void GameText(string text, int time, int style)
        {
            CheckDisposure();

            Native.GameTextForPlayer(Id, text, time, style);
        }

        /// <summary>
        ///     Create an explosion at the specified coordinates.
        /// </summary>
        /// <param name="position">The position of the explosion.</param>
        /// <param name="type">The type of explosion.</param>
        /// <param name="radius">The explosion radius.</param>
        public static void CreateExplosionForAll(Vector position, int type, float radius)
        {
            Native.CreateExplosion(position, type, radius);
        }

        /// <summary>
        ///     Create an explosion at the specified coordinates.
        /// </summary>
        /// <param name="position">The position of the explosion.</param>
        /// <param name="type">The type of explosion.</param>
        /// <param name="radius">The explosion radius.</param>
        /// <param name="interior">The interior of the explosion.</param>
        public static void CreateExposionForAll(Vector position, int type, float radius, int interior)
        {
            foreach (GtaPlayer p in All.Where(p => p.Interior == interior))
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
        public static void CreateExposionForAll(Vector position, int type, float radius, int interior, int virtualworld)
        {
            foreach (GtaPlayer p in All.Where(p => p.Interior == interior && p.VirtualWorld == virtualworld))
                p.CreateExplosion(position, type, radius);
        }

        /// <summary>
        ///     Creates an explosion for a player.
        ///     Only the specific player will see explosion and feel its effects.
        ///     This is useful when you want to isolate explosions from other players or to make them only appear in specific
        ///     virtual worlds.
        /// </summary>
        /// <param name="position">The position of the explosion.</param>
        /// <param name="type">The explosion type.</param>
        /// <param name="radius">The radius of the explosion.</param>
        public virtual void CreateExplosion(Vector position, int type, float radius)
        {
            CheckDisposure();

            Native.CreateExplosionForPlayer(Id, position, type, radius);
        }

        /// <summary>
        ///     Adds a death to the 'killfeed' on the right-hand side of the screen of this Player.
        /// </summary>
        /// <param name="killer">The Player that killer the <paramref name="killee" />.</param>
        /// <param name="killee">The player that has been killed.</param>
        /// <param name="weapon">The reason for this Player's death.</param>
        public virtual void SendDeathMessage(GtaPlayer killer, GtaPlayer killee, Weapon weapon)
        {
            CheckDisposure();

            Native.SendDeathMessageToPlayer(Id, killer == null ? InvalidId : killer.Id,
                killee == null ? InvalidId : killee.Id, (int) weapon);
        }

        /// <summary>
        ///     Adds a death to the 'killfeed' on the right-hand side of the screen.
        /// </summary>
        /// <param name="killer">The Player that killer the <paramref name="killee" />.</param>
        /// <param name="killee">The player that has been killed.</param>
        /// <param name="weapon">The reason for this Player's death.</param>
        public static void SendDeathMessageToAll(GtaPlayer killer, GtaPlayer killee, Weapon weapon)
        {
            Native.SendDeathMessage(killer == null ? InvalidId : killer.Id, killee == null ? InvalidId : killee.Id,
                (int) weapon);
        }

        #endregion

        #region Event raisers

        /// <summary>
        ///     Raises the <see cref="Connected" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnConnected(PlayerEventArgs e)
        {
            if (Connected != null)
                Connected(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Disconnected" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerDisconnectedEventArgs" /> that contains the event data. </param>
        public virtual void OnDisconnected(PlayerDisconnectedEventArgs e)
        {
            if (Disconnected != null)
                Disconnected(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Cleanup" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerDisconnectedEventArgs" /> that contains the event data. </param>
        public virtual void OnCleanup(PlayerDisconnectedEventArgs e)
        {
            if (Cleanup != null)
                Cleanup(this, e);

            Dispose();
        }


        /// <summary>
        ///     Raises the <see cref="Spawned" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnSpawned(PlayerEventArgs e)
        {
            if (Spawned != null)
                Spawned(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Died" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerDeathEventArgs" /> that contains the event data. </param>
        public virtual void OnDeath(PlayerDeathEventArgs e)
        {
            if (Died != null)
                Died(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Text" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerTextEventArgs" /> that contains the event data. </param>
        public virtual void OnText(PlayerTextEventArgs e)
        {
            if (Text != null)
                Text(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="CommandText" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerTextEventArgs" /> that contains the event data. </param>
        public virtual void OnCommandText(PlayerTextEventArgs e)
        {
            if (CommandText != null)
                CommandText(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="RequestClass" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerRequestClassEventArgs" /> that contains the event data. </param>
        public virtual void OnRequestClass(PlayerRequestClassEventArgs e)
        {
            if (RequestClass != null)
                RequestClass(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EnterVehicle" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEnterVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnEnterVehicle(PlayerEnterVehicleEventArgs e)
        {
            if (EnterVehicle != null)
                EnterVehicle(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ExitVehicle" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        public virtual void OnExitVehicle(PlayerVehicleEventArgs e)
        {
            if (ExitVehicle != null)
                ExitVehicle(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="StateChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerStateEventArgs" /> that contains the event data. </param>
        public virtual void OnStateChanged(PlayerStateEventArgs e)
        {
            if (StateChanged != null)
                StateChanged(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EnterCheckpoint" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnEnterCheckpoint(PlayerEventArgs e)
        {
            if (EnterCheckpoint != null)
                EnterCheckpoint(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="LeaveCheckpoint" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnLeaveCheckpoint(PlayerEventArgs e)
        {
            if (LeaveCheckpoint != null)
                LeaveCheckpoint(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EnterRaceCheckpoint" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnEnterRaceCheckpoint(PlayerEventArgs e)
        {
            if (EnterRaceCheckpoint != null)
                EnterRaceCheckpoint(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="LeaveRaceCheckpoint" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnLeaveRaceCheckpoint(PlayerEventArgs e)
        {
            if (LeaveRaceCheckpoint != null)
                LeaveRaceCheckpoint(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="RequestSpawn" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnRequestSpawn(PlayerEventArgs e)
        {
            if (RequestSpawn != null)
                RequestSpawn(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PickUpPickup" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerPickupEventArgs" /> that contains the event data. </param>
        public virtual void OnPickUpPickup(PlayerPickupEventArgs e)
        {
            if (PickUpPickup != null)
                PickUpPickup(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EnterExitModShop" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEnterModShopEventArgs" /> that contains the event data. </param>
        public virtual void OnEnterExitModShop(PlayerEnterModShopEventArgs e)
        {
            if (EnterExitModShop != null)
                EnterExitModShop(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="SelectedMenuRow" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerSelectedMenuRowEventArgs" /> that contains the event data. </param>
        public virtual void OnSelectedMenuRow(PlayerSelectedMenuRowEventArgs e)
        {
            if (SelectedMenuRow != null)
                SelectedMenuRow(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ExitedMenu" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnExitedMenu(PlayerEventArgs e)
        {
            if (ExitedMenu != null)
                ExitedMenu(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="InteriorChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerInteriorChangedEventArgs" /> that contains the event data. </param>
        public virtual void OnInteriorChanged(PlayerInteriorChangedEventArgs e)
        {
            if (InteriorChanged != null)
                InteriorChanged(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="KeyStateChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerKeyStateChangedEventArgs" /> that contains the event data. </param>
        public virtual void OnKeyStateChanged(PlayerKeyStateChangedEventArgs e)
        {
            if (KeyStateChanged != null)
                KeyStateChanged(this, e);

            Key.Handle(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Update" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnUpdate(PlayerEventArgs e)
        {
            if (Update != null)
                Update(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="StreamIn" /> event.
        /// </summary>
        /// <param name="e">An <see cref="StreamPlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnStreamIn(StreamPlayerEventArgs e)
        {
            if (StreamIn != null)
                StreamIn(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="StreamOut" /> event.
        /// </summary>
        /// <param name="e">An <see cref="StreamPlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnStreamOut(StreamPlayerEventArgs e)
        {
            if (StreamOut != null)
                StreamOut(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="DialogResponse" /> event.
        /// </summary>
        /// <param name="e">An <see cref="DialogResponseEventArgs" /> that contains the event data. </param>
        public virtual void OnDialogResponse(DialogResponseEventArgs e)
        {
            if (DialogResponse != null)
                DialogResponse(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="TakeDamage" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerDamageEventArgs" /> that contains the event data. </param>
        public virtual void OnTakeDamage(PlayerDamageEventArgs e)
        {
            if (TakeDamage != null)
                TakeDamage(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="GiveDamage" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerDamageEventArgs" /> that contains the event data. </param>
        public virtual void OnGiveDamage(PlayerDamageEventArgs e)
        {
            if (GiveDamage != null)
                GiveDamage(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ClickMap" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerClickMapEventArgs" /> that contains the event data. </param>
        public virtual void OnClickMap(PlayerClickMapEventArgs e)
        {
            if (ClickMap != null)
                ClickMap(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ClickTextDraw" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerClickTextDrawEventArgs" /> that contains the event data. </param>
        public virtual void OnClickTextDraw(PlayerClickTextDrawEventArgs e)
        {
            if (ClickTextDraw != null)
                ClickTextDraw(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="CancelClickTextDraw" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnCancelClickTextDraw(PlayerEventArgs e)
        {
            if (CancelClickTextDraw != null)
                CancelClickTextDraw(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ClickPlayerTextDraw" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerClickTextDrawEventArgs" /> that contains the event data. </param>
        public virtual void OnClickPlayerTextDraw(PlayerClickTextDrawEventArgs e)
        {
            if (ClickPlayerTextDraw != null)
                ClickPlayerTextDraw(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="ClickPlayer" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerClickPlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnClickPlayer(PlayerClickPlayerEventArgs e)
        {
            if (ClickPlayer != null)
                ClickPlayer(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EditObject" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEditObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnEditObject(PlayerEditObjectEventArgs e)
        {
            if (EditObject != null)
                EditObject(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="EditAttachedObject" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEditAttachedObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnEditAttachedObject(PlayerEditAttachedObjectEventArgs e)
        {
            if (EditAttachedObject != null)
                EditAttachedObject(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="SelectObject" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerSelectObjectEventArgs" /> that contains the event data. </param>
        public virtual void OnSelectObject(PlayerSelectObjectEventArgs e)
        {
            if (SelectObject != null)
                SelectObject(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="WeaponShot" /> event.
        /// </summary>
        /// <param name="e">An <see cref="WeaponShotEventArgs" /> that contains the event data. </param>
        public virtual void OnWeaponShot(WeaponShotEventArgs e)
        {
            if (WeaponShot != null)
                WeaponShot(this, e);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        ///     A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("Player(Id:{0}, Name:{1})", Id, Name);
        }

        #endregion
    }
}