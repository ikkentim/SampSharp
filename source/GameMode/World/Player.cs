using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using GameMode.Definitions;
using GameMode.Events;

namespace GameMode.World
{
    /// <summary>
    /// Represents a SA:MP player.
    /// </summary>
    public class Player : IDisposable
    {
        #region Fields

        protected static List<Player> PlayerInstances = new List<Player>();

        #endregion

        #region Factories

        /// <summary>
        /// Returns an instance of <see cref="Player"/> that deals with <paramref name="playerId"/>.
        /// </summary>
        /// <param name="playerId">The ID of the player we are dealing with.</param>
        /// <returns>An instance of <see cref="Player"/>.</returns>
        public static Player Find(int playerId)
        {
            //Find player in memory or initialize new player
            return PlayerInstances.FirstOrDefault(p => p.PlayerId == playerId) ?? new Player(playerId);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initalizes a new instance of the Player class.
        /// </summary>
        /// <param name="playerId">The ID of the player to initialize.</param>
        private Player(int playerId)
        {
            //Fill properties
            PlayerId = playerId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the ID of this Player.
        /// </summary>
        public int PlayerId { get; private set; }

        /// <summary>
        /// Gets or sets the position of this Player.
        /// </summary>
        public virtual Vector Position
        {
            get { return Server.GetPlayerPos(PlayerId); }
            set { Server.SetPlayerPos(PlayerId, value); }
        }

        /// <summary>
        /// Gets or sets the name of this Player.
        /// </summary>
        public virtual string Name
        {
            get { return Server.GetPlayerName(PlayerId); }
            set { Server.SetPlayerName(PlayerId, value); }
        }

        /// <summary>
        /// Gets the maximum number of players that can join the server, as set by the server var 'maxplayers' in server.cfg. 
        /// </summary>
        public static int MaxPlayers
        {
            get { return Server.GetMaxPlayers(); }
        }

        /// <summary>
        /// Gets a readonly set of all <see cref="Player"/> instances.
        /// </summary>
        public static IReadOnlyCollection<Player> Players
        {
            get { return PlayerInstances.AsReadOnly(); }
        }
        #endregion

        #region Events

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerConnect"/> is being called.
        /// This callback is called when a player connects to the server.
        /// </summary>
        public event PlayerHandler Connected;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerDisconnect"/> is being called.
        /// This callback is called when a player disconnects from the server.
        /// </summary>
        public event PlayerDisconnectedHandler Disconnected;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerSpawn"/> is being called.
        /// This callback is called when a player spawns.
        /// </summary>
        public event PlayerHandler Spawned;

        /// <summary>
        /// Occurs when the <see cref="Server.OnGameModeInit"/> is being called.
        /// This callback is triggered when the gamemode starts.
        /// </summary>
        public event PlayerDeathHandler Died;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerText"/> is being called.
        /// Called when a player sends a chat message.
        /// </summary>
        public event PlayerTextHandler Text;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerCommandText"/> is being called.
        /// This callback is called when a player enters a command into the client chat window, e.g. /help.
        /// </summary>
        public event PlayerTextHandler CommandText;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerRequestClass"/> is being called.
        /// Called when a player changes class at class selection (and when class selection first appears).
        /// </summary>
        public event PlayerRequestClassHandler RequestClass;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerEnterVehicle"/> is being called.
        /// This callback is called when a player starts to enter a vehicle, meaning the player is not in vehicle yet at the time this callback is called.
        /// </summary>
        public event PlayerEnterVehicleHandler EnterVehicle;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerExitVehicle"/> is being called.
        /// This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        /// Not called if the player falls off a bike or is removed from a vehicle by other means such as using <see cref="Server.SetPlayerPos(int,Vector)"/>.
        /// </remarks>
        public event PlayerVehicleHandler ExitVehicle;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerStateChange"/> is being called.
        /// This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        /// Not called if the player falls off a bike or is removed from a vehicle by other means such as using <see cref="Server.SetPlayerPos(int,Vector)"/>.
        /// </remarks>
        public event PlayerStateHandler StateChanged;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerEnterCheckpoint"/> is being called.
        /// This callback is called when a player enters the checkpoint set for that player.
        /// </summary>
        public event PlayerHandler EnterCheckpoint;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerLeaveCheckpoint"/> is being called.
        /// This callback is called when a player leaves the checkpoint set for that player.
        /// </summary>
        public event PlayerHandler LeaveCheckpoint;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerEnterRaceCheckpoint"/> is being called.
        /// This callback is called when a player enters a race checkpoint.
        /// </summary>
        public event PlayerHandler EnterRaceCheckpoint;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerLeaveRaceCheckpoint"/> is being called.
        /// This callback is called when a player leaves the race checkpoint.
        /// </summary>
        public event PlayerHandler LeaveRaceCheckpoint;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerRequestSpawn"/> is being called.
        /// Called when a player attempts to spawn via class selection.
        /// </summary>
        public event PlayerHandler RequestSpawn;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerPickUpPickup"/> is being called.
        /// Called when a player picks up a pickup created with <see cref="Server.CreatePickup"/>.
        /// </summary>
        public event PlayerPickupHandler PickUpPickup;

        /// <summary>
        /// Occurs when the <see cref="Server.OnEnterExitModShop"/> is being called.
        /// This callback is called when a player enters or exits a mod shop.
        /// </summary>
        public event PlayerEnterModShopHandler EnterExitModShop;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerSelectedMenuRow"/> is being called.
        /// This callback is called when a player selects an item from a menu.
        /// </summary>
        public event PlayerSelectedMenuRowHandler SelectedMenuRow;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerExitedMenu"/> is being called.
        /// Called when a player exits a menu.
        /// </summary>
        public event PlayerHandler ExitedMenu;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerInteriorChange"/> is being called.
        /// Called when a player changes interior.
        /// </summary>
        /// <remarks>
        /// This is also called when <see cref="Server.SetPlayerInterior"/> is used.
        /// </remarks>
        public event PlayerInteriorChangedHandler InteriorChanged;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerKeyStateChange"/> is being called.
        /// This callback is called when the state of any supported key is changed (pressed/released). Directional keys do not trigger this callback.
        /// </summary>
        public event PlayerKeyStateChangedHandler KeyStateChanged;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerUpdate"/> is being called.
        /// This callback is called everytime a client/player updates the server with their status.
        /// </summary>
        /// <remarks>
        /// This callback is called very frequently per second per player, only use it when you know what it's meant for.
        /// </remarks>
        public event PlayerHandler Update;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerStreamIn"/> is being called.
        /// This callback is called when a player is streamed by some other player's client.
        /// </summary>
        public event PlayerStreamHandler StreamIn;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerStreamOut"/> is being called.
        /// This callback is called when a player is streamed out from some other player's client.
        /// </summary>
        public event PlayerStreamHandler StreamOut;

        /// <summary>
        /// Occurs when the <see cref="Server.OnDialogResponse"/> is being called.
        /// This callback is called when a player responds to a dialog shown using <see cref="Server.ShowPlayerDialog"/> by either clicking a button, pressing ENTER/ESC or double-clicking a list item (if using a list style dialog).
        /// </summary>
        public event DialogResponseHandler DialogResponse;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerTakeDamage"/> is being called.
        /// This callback is called when a player takes damage.
        /// </summary>
        public event PlayerDamageHandler TakeDamage;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerGiveDamage"/> is being called.
        /// This callback is called when a player gives damage to another player.
        /// </summary>
        /// <remarks>
        /// One thing you can do with GiveDamage is detect when other players report that they have damaged a certain player, and that player hasn't taken any health loss. You can flag those players as suspicious.
        /// You can also set all players to the same team (so they don't take damage from other players) and process all health loss from other players manually.
        /// You might have a server where players get a wanted level if they attack Cop players (or some specific class). In that case you might trust GiveDamage over TakeDamage.
        /// There should be a lot you can do with it. You just have to keep in mind the levels of trust between clients. In most cases it's better to trust the client who is being damaged to report their health/armour (TakeDamage). SA-MP normally does this. GiveDamage provides some extra information which may be useful when you require a different level of trust.
        /// </remarks>
        public event PlayerDamageHandler GiveDamage;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerClickMap"/> is being called.
        /// This callback is called when a player places a target/waypoint on the pause menu map (by right-clicking).
        /// </summary>
        /// <remarks>
        /// The Z value provided is only an estimate; you may find it useful to use a plugin like the MapAndreas plugin to get a more accurate Z coordinate (or for teleportation; use <see cref="Server.SetPlayerPosFindZ(int,Vector)"/>).
        /// </remarks>
        public event PlayerClickMapHandler ClickMap;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerClickTextDraw"/> is being called.
        /// This callback is called when a player clicks on a textdraw or cancels the select mode(ESC).
        /// </summary>
        /// <remarks>
        /// The clickable area is defined by <see cref="Server.TextDrawTextSize"/>. The x and y parameters passed to that function must not be zero or negative.
        /// </remarks>
        public event PlayerClickTextDrawHandler ClickTextDraw;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerClickPlayerTextDraw"/> is being called.
        /// This callback is called when a player clicks on a player-textdraw. It is not called when player cancels the select mode (ESC) - however, <see cref="Server.OnPlayerClickTextDraw"/> is.
        /// </summary>
        public event PlayerClickTextDrawHandler ClickPlayerTextDraw;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerClickPlayer"/> is being called.
        /// Called when a player double-clicks on a player on the scoreboard.
        /// </summary>
        /// <remarks>
        /// There is currently only one 'source' (<see cref="PlayerClickSource.Scoreboard"/>). The existence of this argument suggests that more sources may be supported in the future.
        /// </remarks>
        public event PlayerClickPlayerHandler ClickPlayer;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerEditObject"/> is being called.
        /// This callback is called when a player ends object edition mode.
        /// </summary>
        public event PlayerEditObjectHandler EditObject;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerEditAttachedObject"/> is being called.
        /// This callback is called when a player ends attached object edition mode.
        /// </summary>
        /// <remarks>
        /// Editions should be discarded if response was '0' (cancelled). This must be done by storing the offsets etc. in an array BEFORE using EditAttachedObject.
        /// </remarks>
        public event PlayerEditAttachedObjectHandler EditAttachedObject;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerSelectObject"/> is being called.
        /// This callback is called when a player selects an object after <see cref="Server.SelectObject"/> has been used.
        /// </summary>
        public event PlayerSelectObjectHandler PlayerSelectObject;

        /// <summary>
        /// Occurs when the <see cref="Server.OnPlayerWeaponShot"/> is being called.
        /// This callback is called when a player fires a shot from a weapon.
        /// </summary>
        /// <remarks>
        /// <see cref="BulletHitType.None"/>: the fX, fY and fZ parameters are normal coordinates;
        /// Others: the fX, fY and fZ are offsets from the center of hitid.
        /// </remarks>
        public event WeaponShotHandler WeaponShot;

        #endregion

        #region Methods

        public virtual void OnConnected(PlayerEventArgs e)
        {
            if(Connected != null)
            Connected(this, e);
        }

        public virtual void OnDisconnected(PlayerDisconnectedEventArgs e)
        {
            if (Disconnected != null)
                Disconnected(this, e);

            Dispose();
        }

        public virtual void OnSpawned(PlayerEventArgs e)
        {
            if (Spawned != null)
                Spawned(this, e);
        }

        public virtual void OnDeath(PlayerDeathEventArgs e)
        {
            if (Died != null)
                Died(this, e);
        }

        public virtual void OnText(PlayerTextEventArgs e)
        {
            if (Text != null)
                Text(this, e);
        }

        public virtual void OnCommandText(PlayerTextEventArgs e)
        {
            if (CommandText != null)
                CommandText(this, e);
        }

        public virtual void OnRequestClass(PlayerRequestClassEventArgs e)
        {
            if (RequestClass != null)
                RequestClass(this, e);
        }

        public virtual void OnEnterVehicle(PlayerEnterVehicleEventArgs e)
        {
            if (EnterVehicle != null)
                EnterVehicle(this, e);
        }

        public virtual void OnExitVehicle(PlayerVehicleEventArgs e)
        {
            if (ExitVehicle != null)
                ExitVehicle(this, e);
        }

        public virtual void OnStateChanged(PlayerStateEventArgs e)
        {
            if (StateChanged != null)
                StateChanged(this, e);
        }

        public virtual void OnEnterCheckpoint(PlayerEventArgs e)
        {
            if (EnterCheckpoint != null)
                EnterCheckpoint(this, e);
        }

        public virtual void OnLeaveCheckpoint(PlayerEventArgs e)
        {
            if (LeaveCheckpoint != null)
                LeaveCheckpoint(this, e);
        }

        public virtual void OnEnterRaceCheckpoint(PlayerEventArgs e)
        {
            if (EnterRaceCheckpoint != null)
                EnterRaceCheckpoint(this, e);
        }

        public virtual void OnLeaveRaceCheckpoint(PlayerEventArgs e)
        {
            if (LeaveRaceCheckpoint != null)
                LeaveRaceCheckpoint(this, e);
        }

        /*
         'On'-methods yet to add:
            RequestSpawn;
            PickUpPickup;
            EnterExitModShop;
            SelectedMenuRow;
            ExitedMenu;
            InteriorChanged;
            KeyStateChanged;
            Update;
            StreamIn;
            StreamOut;
            DialogResponse;
            TakeDamage;
            GiveDamage;
            ClickMap;
            ClickTextDraw;
            ClickPlayerTextDraw;
            ClickPlayer;
            EditObject;
            EditAttachedObject;
            PlayerSelectObject;
            WeaponShot;
         */
        public override string ToString()
        {
            return string.Format("Player(Id:{0}, Name:{1}, Position:{2})",PlayerId, Name, Position);
        }

        public override int GetHashCode()
        {
            return PlayerId;
        }

        /// <summary>
        /// Removes this Player from memory. It is best to dispose the object when the player has disconnected.
        /// </summary>
        public virtual void Dispose()
        {
            PlayerInstances.Remove(this);
        }

        #endregion

    }
}
