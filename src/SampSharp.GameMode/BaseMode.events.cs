// SampSharp
// Copyright (C) 2015 Tim Potze
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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode
{
    public abstract partial class BaseMode
    {
        /// <summary>
        ///     Occurs when the <see cref="OnGameModeInit" /> callback is being called.
        ///     This callback is triggered when the gamemode starts.
        /// </summary>
        public event EventHandler<EventArgs> Initialized;

        /// <summary>
        ///     Occurs when the <see cref="OnGameModeExit" /> callback is being called.
        ///     This callback is called when a gamemode ends.
        /// </summary>
        public event EventHandler<EventArgs> Exited;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerConnect" /> callback is being called.
        ///     This callback is called when a player connects to the server.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerConnected;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerDisconnect" /> callback is being called.
        ///     This callback is called when a player disconnects from the server.
        /// </summary>
        public event EventHandler<PlayerDisconnectedEventArgs> PlayerDisconnected;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerCleanup" /> callback is being called.
        ///     This callback is called after a player has disconnected.
        /// </summary>
        /// <remarks>
        ///     Because <see cref="GtaPlayer" /> probably is the first listener of this event,
        ///     the <see cref="GtaPlayer" /> object is already disposed before any other listeners are called.
        ///     It is better to either use the <see cref="PlayerDisconnected" /> event or <see cref="GtaPlayer.Cleanup" />
        /// </remarks>
        public event EventHandler<PlayerDisconnectedEventArgs> PlayerCleanup;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerSpawn" /> callback is being called.
        ///     This callback is called when a player spawns.
        /// </summary>
        public event EventHandler<PlayerSpawnEventArgs> PlayerSpawned;

        /// <summary>
        ///     Occurs when the <see cref="OnGameModeInit" /> callback is being called.
        ///     This callback is triggered when the gamemode starts.
        /// </summary>
        public event EventHandler<PlayerDeathEventArgs> PlayerDied;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleSpawn" /> callback is being called.
        ///     This callback is called when a vehicle respawns.
        /// </summary>
        public event EventHandler<VehicleEventArgs> VehicleSpawned;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleDeath" /> callback is being called.
        ///     This callback is called when a vehicle is destroyed - either by exploding or becoming submerged in water.
        /// </summary>
        /// <remarks>
        ///     This callback will also be called when a vehicle enters water, but the vehicle can be saved from destruction by
        ///     teleportation or driving out (if only partially submerged). The callback won't be called a second time, and the
        ///     vehicle may disappear when the driver exits, or after a short time.
        /// </remarks>
        public event EventHandler<PlayerVehicleEventArgs> VehicleDied;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerText" /> callback is being called.
        ///     Called when a player sends a chat message.
        /// </summary>
        public event EventHandler<PlayerTextEventArgs> PlayerText;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerCommandText" /> callback is being called.
        ///     This callback is called when a player enters a command into the client chat window, e.g. /help.
        /// </summary>
        public event EventHandler<PlayerCommandTextEventArgs> PlayerCommandText;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerRequestClass" /> callback is being called.
        ///     Called when a player changes class at class selection (and when class selection first appears).
        /// </summary>
        public event EventHandler<PlayerRequestClassEventArgs> PlayerRequestClass;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEnterVehicle" /> callback is being called.
        ///     This callback is called when a player starts to enter a vehicle, meaning the player is not in vehicle yet at the
        ///     time this callback is called.
        /// </summary>
        public event EventHandler<PlayerEnterVehicleEventArgs> PlayerEnterVehicle;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerExitVehicle" /> callback is being called.
        ///     This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        ///     Not called if the player falls off a bike or is removed from a vehicle by other means such as using
        ///     <see cref="Native.SetPlayerPos(int,Vector)" />.
        /// </remarks>
        public event EventHandler<PlayerVehicleEventArgs> PlayerExitVehicle;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerStateChange" /> callback is being called.
        ///     This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        ///     Not called if the player falls off a bike or is removed from a vehicle by other means such as using
        ///     <see cref="Native.SetPlayerPos(int,Vector)" />.
        /// </remarks>
        public event EventHandler<PlayerStateEventArgs> PlayerStateChanged;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEnterCheckpoint" /> callback is being called.
        ///     This callback is called when a player enters the checkpoint set for that player.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerEnterCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerLeaveCheckpoint" /> callback is being called.
        ///     This callback is called when a player leaves the checkpoint set for that player.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerLeaveCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEnterRaceCheckpoint" /> callback is being called.
        ///     This callback is called when a player enters a race checkpoint.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerEnterRaceCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerLeaveRaceCheckpoint" /> callback is being called.
        ///     This callback is called when a player leaves the race checkpoint.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerLeaveRaceCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnRconCommand" /> callback is being called.
        ///     This callback is called when a command is sent through the server console, remote RCON, or via the in-game /rcon
        ///     command.
        /// </summary>
        public event EventHandler<RconEventArgs> RconCommand;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerRequestSpawn" /> callback is being called.
        ///     Called when a player attempts to spawn via class selection.
        /// </summary>
        public event EventHandler<PlayerRequestSpawnEventArgs> PlayerRequestSpawn;

        /// <summary>
        ///     Occurs when the <see cref="OnObjectMoved" /> callback is being called.
        ///     This callback is called when an object is moved after <see cref="Native.MoveObject(int,Vector,float,Vector)" />
        ///     (when it stops moving).
        /// </summary>
        public event EventHandler<ObjectEventArgs> ObjectMoved;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerObjectMoved" /> callback is being called.
        ///     This callback is called when a player object is moved after
        ///     <see cref="Native.MovePlayerObject(int,int,Vector,float,Vector)" /> (when it stops
        ///     moving).
        /// </summary>
        public event EventHandler<PlayerObjectEventArgs> PlayerObjectMoved;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerPickUpPickup" /> callback is being called.
        ///     Called when a player picks up a pickup created with <see cref="Native.CreatePickup" />.
        /// </summary>
        public event EventHandler<PlayerPickupEventArgs> PlayerPickUpPickup;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleMod" /> callback is being called.
        ///     This callback is called when a vehicle is modded.
        /// </summary>
        /// <remarks>
        ///     This callback is not called by <see cref="Native.AddVehicleComponent" />.
        /// </remarks>
        public event EventHandler<VehicleModEventArgs> VehicleMod;

        /// <summary>
        ///     Occurs when the <see cref="OnEnterExitModShop" /> callback is being called.
        ///     This callback is called when a player enters or exits a mod shop.
        /// </summary>
        public event EventHandler<PlayerEnterModShopEventArgs> PlayerEnterExitModShop;

        /// <summary>
        ///     Occurs when the <see cref="OnVehiclePaintjob" /> callback is being called.
        ///     Called when a player changes the paintjob of their vehicle (in a modshop).
        /// </summary>
        public event EventHandler<VehiclePaintjobEventArgs> VehiclePaintjobApplied;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleRespray" /> callback is being called.
        ///     The callback name is deceptive, this callback is called when a player exits a mod shop, regardless of whether the
        ///     vehicle's colors were changed, and is NEVER called for pay 'n' spray garages.
        /// </summary>
        /// <remarks>
        ///     Misleadingly, this callback is not called for pay 'n' spray (only modshops).
        /// </remarks>
        public event EventHandler<VehicleResprayedEventArgs> VehicleResprayed;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleDamageStatusUpdate" /> callback is being called.
        ///     This callback is called when a vehicle element such as doors, tires, panels, or lights get damaged.
        /// </summary>
        /// <remarks>
        ///     This does not include vehicle health changes.
        /// </remarks>
        public event EventHandler<PlayerVehicleEventArgs> VehicleDamageStatusUpdated;

        /// <summary>
        ///     Occurs when the <see cref="OnUnoccupiedVehicleUpdate" /> callback is being called.
        ///     This callback is called everytime an unoccupied vehicle updates the server with their status.
        /// </summary>
        /// <remarks>
        ///     This callback is called very frequently per second per unoccupied vehicle. You should refrain from implementing
        ///     intensive calculations or intensive file writing/reading operations in this callback.
        /// </remarks>
        public event EventHandler<UnoccupiedVehicleEventArgs> UnoccupiedVehicleUpdated;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerSelectedMenuRow" /> callback is being called.
        ///     This callback is called when a player selects an item from a menu.
        /// </summary>
        public event EventHandler<PlayerSelectedMenuRowEventArgs> PlayerSelectedMenuRow;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerExitedMenu" /> callback is being called.
        ///     Called when a player exits a menu.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerExitedMenu;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerInteriorChange" /> callback is being called.
        ///     Called when a player changes interior.
        /// </summary>
        /// <remarks>
        ///     This is also called when <see cref="Native.SetPlayerInterior" /> is used.
        /// </remarks>
        public event EventHandler<PlayerInteriorChangedEventArgs> PlayerInteriorChanged;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerKeyStateChange" /> callback is being called.
        ///     This callback is called when the state of any supported key is changed (pressed/released). Directional keys do not
        ///     trigger this callback.
        /// </summary>
        public event EventHandler<PlayerKeyStateChangedEventArgs> PlayerKeyStateChanged;

        /// <summary>
        ///     Occurs when the <see cref="OnRconLoginAttempt" /> callback is being called.
        ///     This callback is called when someone tries to login to RCON, succesful or not.
        /// </summary>
        /// <remarks>
        ///     This callback is only called when /rcon login is used.
        /// </remarks>
        public event EventHandler<RconLoginAttemptEventArgs> RconLoginAttempt;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerUpdate" /> callback is being called.
        ///     This callback is called everytime a client/player updates the server with their status.
        /// </summary>
        /// <remarks>
        ///     This callback is called very frequently per second per player, only use it when you know what it's meant for.
        /// </remarks>
        public event EventHandler<PlayerEventArgs> PlayerUpdate;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerStreamIn" /> callback is being called.
        ///     This callback is called when a player is streamed by some other player's client.
        /// </summary>
        public event EventHandler<StreamPlayerEventArgs> PlayerStreamIn;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerStreamOut" /> callback is being called.
        ///     This callback is called when a player is streamed out from some other player's client.
        /// </summary>
        public event EventHandler<StreamPlayerEventArgs> PlayerStreamOut;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleStreamIn" /> callback is being called.
        ///     Called when a vehicle is streamed to a player's client.
        /// </summary>
        public event EventHandler<PlayerVehicleEventArgs> VehicleStreamIn;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleStreamOut" /> callback is being called.
        ///     This callback is called when a vehicle is streamed out from some player's client.
        /// </summary>
        public event EventHandler<PlayerVehicleEventArgs> VehicleStreamOut;

        /// <summary>
        ///     Occurs when the <see cref="OnTrailerUpdate" /> callback is being called.
        ///     This callback is called when a player sent a trailer update.
        /// </summary>
        public event EventHandler<PlayerTrailerEventArgs> TrailerUpdate;

        /// <summary>
        ///     Occurs when the <see cref="OnDialogResponse" /> callback is being called.
        ///     This callback is called when a player responds to a dialog shown using <see cref="Native.ShowPlayerDialog" /> by
        ///     either clicking a button, pressing ENTER/ESC or double-clicking a list item (if using a list style dialog).
        /// </summary>
        public event EventHandler<DialogResponseEventArgs> DialogResponse;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerTakeDamage" /> callback is being called.
        ///     This callback is called when a player takes damage.
        /// </summary>
        public event EventHandler<PlayerDamageEventArgs> PlayerTakeDamage;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerGiveDamage" /> callback is being called.
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
        public event EventHandler<PlayerDamageEventArgs> PlayerGiveDamage;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerClickMap" /> callback is being called.
        ///     This callback is called when a player places a target/waypoint on the pause menu map (by right-clicking).
        /// </summary>
        /// <remarks>
        ///     The Z value provided is only an estimate; you may find it useful to use a plugin like the MapAndreas plugin to get
        ///     a more accurate Z coordinate (or for teleportation; use <see cref="Native.SetPlayerPosFindZ(int,Vector)" />).
        /// </remarks>
        public event EventHandler<PlayerClickMapEventArgs> PlayerClickMap;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerClickTextDraw" /> callback is being called.
        ///     This callback is called when a player clicks on a textdraw or cancels the select mode(ESC).
        /// </summary>
        /// <remarks>
        ///     The clickable area is defined by <see cref="Native.TextDrawTextSize" />. The x and y parameters passed to that
        ///     function must not be zero or negative.
        /// </remarks>
        public event EventHandler<PlayerClickTextDrawEventArgs> PlayerClickTextDraw;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerClickPlayerTextDraw" /> callback is being called.
        ///     This callback is called when a player clicks on a player-textdraw. It is not called when player cancels the select
        ///     mode (ESC) - however, <see cref="OnPlayerClickTextDraw" /> is.
        /// </summary>
        public event EventHandler<PlayerClickTextDrawEventArgs> PlayerClickPlayerTextDraw;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerClickPlayer" /> callback is being called.
        ///     Called when a player double-clicks on a player on the scoreboard.
        /// </summary>
        /// <remarks>
        ///     There is currently only one 'source' (<see cref="PlayerClickSource.Scoreboard" />). The existence of this argument
        ///     suggests that more sources may be supported in the future.
        /// </remarks>
        public event EventHandler<PlayerClickPlayerEventArgs> PlayerClickPlayer;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEditObject" /> callback is being called.
        ///     This callback is called when a player ends object edition mode.
        /// </summary>
        public event EventHandler<PlayerEditObjectEventArgs> PlayerEditObject;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEditAttachedObject" /> callback is being called.
        ///     This callback is called when a player ends attached object edition mode.
        /// </summary>
        /// <remarks>
        ///     Editions should be discarded if response was '0' (cancelled). This must be done by storing the offsets etc. in an
        ///     array BEFORE using EditAttachedObject.
        /// </remarks>
        public event EventHandler<PlayerEditAttachedObjectEventArgs> PlayerEditAttachedObject;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerSelectObject" /> callback is being called.
        ///     This callback is called when a player selects an object after <see cref="Native.SelectObject" /> has been used.
        /// </summary>
        public event EventHandler<PlayerSelectObjectEventArgs> PlayerSelectObject;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerWeaponShot" /> callback is being called.
        ///     This callback is called when a player fires a shot from a weapon.
        /// </summary>
        /// <remarks>
        ///     <see cref="BulletHitType.None" />: the fX, fY and fZ parameters are normal coordinates;
        ///     Others: the fX, fY and fZ are offsets from the center of hitid.
        /// </remarks>
        public event EventHandler<WeaponShotEventArgs> PlayerWeaponShot;

        /// <summary>
        ///     Occurs when the <see cref="OnIncomingConnection" /> callback is being called.
        ///     This callback is called when an IP address attempts a connection to the server.
        /// </summary>
        public event EventHandler<ConnectionEventArgs> IncomingConnection;

        /// <summary>
        ///     Occurs when the <see cref="OnTick" /> callback is being called.
        ///     This callback is called every tick(50 times per second).
        /// </summary>
        /// <remarks>
        ///     USE WITH CARE!
        /// </remarks>
        public event EventHandler<EventArgs> Tick;

        /// <summary>
        ///     Occurs when the <see cref="OnTimerTick" /> callback is being called.
        ///     This callback is called when a timer ticks.
        /// </summary>
        public event EventHandler<EventArgs> TimerTick;



        /// <summary>
        /// Raises the <see cref="Initialized"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data. </param>
        protected virtual void OnInitialized(EventArgs e)
        {
            if (Initialized != null)
                Initialized(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Exited"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data. </param>
        protected virtual void OnExited(EventArgs e)
        {
            if (Exited != null)
                Exited(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerConnected"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerConnected"/> that contains the event data. </param>
        protected virtual void OnPlayerConnected(PlayerEventArgs e)
        {
            if (PlayerConnected != null)
                PlayerConnected(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerDisconnected"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerDisconnectedEventArgs"/> that contains the event data. </param>
        protected virtual void OnPlayerDisconnected(PlayerDisconnectedEventArgs e)
        {
            if (PlayerDisconnected != null)
                PlayerDisconnected(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerCleanup"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerDisconnectedEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerCleanup(PlayerDisconnectedEventArgs e)
        {
            if (PlayerCleanup != null)
                PlayerCleanup(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerSpawned"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerSpawned(PlayerSpawnEventArgs e)
        {
            if (PlayerSpawned != null)
                PlayerSpawned(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerDied"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerDeathEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerDied(PlayerDeathEventArgs e)
        {
            if (PlayerDied != null)
                PlayerDied(this, e);
        }

        /// <summary>
        /// Raises the <see cref="VehicleSpawned"/> event.
        /// </summary>
        /// <param name="e">An <see cref="VehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleSpawned(VehicleEventArgs e)
        {
            if (VehicleSpawned != null)
                VehicleSpawned(this, e);
        }

        /// <summary>
        /// Raises the <see cref="VehicleDied"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleDied(PlayerVehicleEventArgs e)
        {
            if (VehicleDied != null)
                VehicleDied(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerText"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerTextEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerText(PlayerTextEventArgs e)
        {
            if (PlayerText != null)
                PlayerText(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerCommandText"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerTextEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerCommandText(PlayerCommandTextEventArgs e)
        {
            if (PlayerCommandText != null)
                PlayerCommandText(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerRequestClass"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerRequestClassEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerRequestClass(PlayerRequestClassEventArgs e)
        {
            if (PlayerRequestClass != null)
                PlayerRequestClass(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerEnterVehicle"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEnterVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEnterVehicle(PlayerEnterVehicleEventArgs e)
        {
            if (PlayerEnterVehicle != null)
                PlayerEnterVehicle(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerExitVehicle"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerExitVehicle(PlayerVehicleEventArgs e)
        {
            if (PlayerExitVehicle != null)
                PlayerExitVehicle(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerStateChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerStateEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerStateChanged(PlayerStateEventArgs e)
        {
            if (PlayerStateChanged != null)
                PlayerStateChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerEnterCheckpoint"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEnterCheckpoint(PlayerEventArgs e)
        {
            if (PlayerEnterCheckpoint != null)
                PlayerEnterCheckpoint(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerLeaveCheckpoint"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerLeaveCheckpoint(PlayerEventArgs e)
        {
            if (PlayerLeaveCheckpoint != null)
                PlayerLeaveCheckpoint(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerEnterRaceCheckpoint"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEnterRaceCheckpoint(PlayerEventArgs e)
        {
            if (PlayerEnterRaceCheckpoint != null)
                PlayerEnterRaceCheckpoint(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerLeaveRaceCheckpoint"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerLeaveRaceCheckpoint(PlayerEventArgs e)
        {
            if (PlayerLeaveRaceCheckpoint != null)
                PlayerLeaveRaceCheckpoint(this, e);
        }

        /// <summary>
        /// Raises the <see cref="RconCommand"/> event.
        /// </summary>
        /// <param name="e">An <see cref="RconEventArgs" /> that contains the event data. </param>
        protected virtual void OnRconCommand(RconEventArgs e)
        {

            if (RconCommand != null)
                RconCommand(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerRequestSpawn"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerRequestSpawn(PlayerRequestSpawnEventArgs e)
        {
            if (PlayerRequestSpawn != null)
                PlayerRequestSpawn(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ObjectMoved"/> event.
        /// </summary>
        /// <param name="e">An <see cref="ObjectEventArgs" /> that contains the event data. </param>
        protected virtual void OnObjectMoved(ObjectEventArgs e)
        {
            if (ObjectMoved != null)
                ObjectMoved(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerObjectMoved"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerObjectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerObjectMoved(PlayerObjectEventArgs e)
        {
            if (PlayerObjectMoved != null)
                PlayerObjectMoved(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerPickUpPickup"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerPickupEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerPickUpPickup(PlayerPickupEventArgs e)
        {
            if (PlayerPickUpPickup != null)
                PlayerPickUpPickup(this, e);
        }

        /// <summary>
        /// Raises the <see cref="VehicleMod"/> event.
        /// </summary>
        /// <param name="e">An <see cref="VehicleModEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleMod(VehicleModEventArgs e)
        {
            if (VehicleMod != null)
                VehicleMod(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerEnterExitModShop"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEnterModShopEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEnterExitModShop(PlayerEnterModShopEventArgs e)
        {
            if (PlayerEnterExitModShop != null)
                PlayerEnterExitModShop(this, e);
        }

        /// <summary>
        /// Raises the <see cref="VehiclePaintjobApplied"/> event.
        /// </summary>
        /// <param name="e">An <see cref="VehiclePaintjobEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehiclePaintjobApplied(VehiclePaintjobEventArgs e)
        {
            if (VehiclePaintjobApplied != null)
                VehiclePaintjobApplied(this, e);
        }

        /// <summary>
        /// Raises the <see cref="VehicleResprayed"/> event.
        /// </summary>
        /// <param name="e">An <see cref="VehicleResprayedEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleResprayed(VehicleResprayedEventArgs e)
        {
            if (VehicleResprayed != null)
                VehicleResprayed(this, e);
        }

        /// <summary>
        /// Raises the <see cref="VehicleDamageStatusUpdated"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleDamageStatusUpdated(PlayerVehicleEventArgs e)
        {
            if (VehicleDamageStatusUpdated != null)
                VehicleDamageStatusUpdated(this, e);
        }

        /// <summary>
        /// Raises the <see cref="UnoccupiedVehicleUpdated"/> event.
        /// </summary>
        /// <param name="e">An <see cref="UnoccupiedVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnUnoccupiedVehicleUpdated(UnoccupiedVehicleEventArgs e)
        {
            if (UnoccupiedVehicleUpdated != null)
                UnoccupiedVehicleUpdated(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerSelectedMenuRow"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerSelectedMenuRowEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerSelectedMenuRow(PlayerSelectedMenuRowEventArgs e)
        {
            if (PlayerSelectedMenuRow != null)
                PlayerSelectedMenuRow(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerExitedMenu"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerExitedMenu(PlayerEventArgs e)
        {
            if (PlayerExitedMenu != null)
                PlayerExitedMenu(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerInteriorChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerInteriorChangedEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerInteriorChanged(PlayerInteriorChangedEventArgs e)
        {
            if (PlayerInteriorChanged != null)
                PlayerInteriorChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerKeyStateChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerKeyStateChangedEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerKeyStateChanged(PlayerKeyStateChangedEventArgs e)
        {
            if (PlayerKeyStateChanged != null)
                PlayerKeyStateChanged(this, e);
        }

        /// <summary>
        /// Raises the <see cref="RconLoginAttempt"/> event.
        /// </summary>
        /// <param name="e">An <see cref="RconLoginAttemptEventArgs" /> that contains the event data. </param>
        protected virtual void OnRconLoginAttempt(RconLoginAttemptEventArgs e)
        {
            if (RconLoginAttempt != null)
                RconLoginAttempt(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerUpdate"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerUpdate(PlayerEventArgs e)
        {
            if (PlayerUpdate != null)
                PlayerUpdate(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerStreamIn"/> event.
        /// </summary>
        /// <param name="e">An <see cref="StreamPlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerStreamIn(StreamPlayerEventArgs e)
        {
            if (PlayerStreamIn != null)
                PlayerStreamIn(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerStreamOut"/> event.
        /// </summary>
        /// <param name="e">An <see cref="StreamPlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerStreamOut(StreamPlayerEventArgs e)
        {
            if (PlayerStreamOut != null)
                PlayerStreamOut(this, e);
        }

        /// <summary>
        /// Raises the <see cref="VehicleStreamIn"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleStreamIn(PlayerVehicleEventArgs e)
        {
            if (VehicleStreamIn != null)
                VehicleStreamIn(this, e);
        }

        /// <summary>
        /// Raises the <see cref="VehicleStreamOut"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleStreamOut(PlayerVehicleEventArgs e)
        {
            if (VehicleStreamOut != null)
                VehicleStreamOut(this, e);
        }

        /// <summary>
        /// Raises the <see cref="TrailerUpdate"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnTrailerUpdate(PlayerTrailerEventArgs e)
        {
            if (TrailerUpdate != null)
                TrailerUpdate(this, e);
        }

        /// <summary>
        /// Raises the <see cref="DialogResponse"/> event.
        /// </summary>
        /// <param name="e">An <see cref="DialogResponseEventArgs" /> that contains the event data. </param>
        protected virtual void OnDialogResponse(DialogResponseEventArgs e)
        {
            if (DialogResponse != null)
                DialogResponse(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerTakeDamage"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerDamageEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerTakeDamage(PlayerDamageEventArgs e)
        {
            if (PlayerTakeDamage != null)
                PlayerTakeDamage(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerGiveDamage"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerDamageEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerGiveDamage(PlayerDamageEventArgs e)
        {
            if (PlayerGiveDamage != null)
                PlayerGiveDamage(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerClickMap"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerClickMapEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerClickMap(PlayerClickMapEventArgs e)
        {
            if (PlayerClickMap != null)
                PlayerClickMap(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerClickTextDraw"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerClickTextDrawEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerClickTextDraw(PlayerClickTextDrawEventArgs e)
        {
            if (PlayerClickTextDraw != null)
                PlayerClickTextDraw(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerClickPlayerTextDraw"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerClickTextDrawEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerClickPlayerTextDraw(PlayerClickTextDrawEventArgs e)
        {
            if (PlayerClickPlayerTextDraw != null)
                PlayerClickPlayerTextDraw(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerClickPlayer"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerClickPlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerClickPlayer(PlayerClickPlayerEventArgs e)
        {
            if (PlayerClickPlayer != null)
                PlayerClickPlayer(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerEditObject"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEditObjectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEditObject(PlayerEditObjectEventArgs e)
        {
            if (PlayerEditObject != null)
                PlayerEditObject(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerEditAttachedObject"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEditAttachedObjectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEditAttachedObject(PlayerEditAttachedObjectEventArgs e)
        {
            if (PlayerEditAttachedObject != null)
                PlayerEditAttachedObject(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerSelectObject"/> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerSelectObjectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerSelectObject(PlayerSelectObjectEventArgs e)
        {
            if (PlayerSelectObject != null)
                PlayerSelectObject(this, e);
        }

        /// <summary>
        /// Raises the <see cref="PlayerWeaponShot"/> event.
        /// </summary>
        /// <param name="e">An <see cref="WeaponShotEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerWeaponShot(WeaponShotEventArgs e)
        {
            if (PlayerWeaponShot != null)
                PlayerWeaponShot(this, e);
        }

        /// <summary>
        /// Raises the <see cref="IncomingConnection"/> event.
        /// </summary>
        /// <param name="e">An <see cref="ConnectionEventArgs" /> that contains the event dEventArgsata. </param>
        protected virtual void OnIncomingConnection(ConnectionEventArgs e)
        {
            if (IncomingConnection != null)
                IncomingConnection(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Tick"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnTick(EventArgs e)
        {
            if (Tick != null)
                Tick(this, e);
        }
    }
}