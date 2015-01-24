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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode
{
    /// <summary>
    ///     Base class for a SA-MP game mode.
    /// </summary>
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
        public event EventHandler<EventArgs> PlayerConnected;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerDisconnect" /> callback is being called.
        ///     This callback is called when a player disconnects from the server.
        /// </summary>
        public event EventHandler<DisconnectEventArgs> PlayerDisconnected;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerCleanup" /> callback is being called.
        ///     This callback is called after a player has disconnected.
        /// </summary>
        /// <remarks>
        ///     Because <see cref="GtaPlayer" /> probably is the first listener of this event,
        ///     the <see cref="GtaPlayer" /> object is already disposed before any other listeners are called.
        ///     It is better to either use the <see cref="PlayerDisconnected" /> event or <see cref="GtaPlayer.Cleanup" />
        /// </remarks>
        public event EventHandler<DisconnectEventArgs> PlayerCleanup;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerSpawn" /> callback is being called.
        ///     This callback is called when a player spawns.
        /// </summary>
        public event EventHandler<SpawnEventArgs> PlayerSpawned;

        /// <summary>
        ///     Occurs when the <see cref="OnGameModeInit" /> callback is being called.
        ///     This callback is triggered when the gamemode starts.
        /// </summary>
        public event EventHandler<DeathEventArgs> PlayerDied;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleSpawn" /> callback is being called.
        ///     This callback is called when a vehicle respawns.
        /// </summary>
        public event EventHandler<EventArgs> VehicleSpawned;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleDeath" /> callback is being called.
        ///     This callback is called when a vehicle is destroyed - either by exploding or becoming submerged in water.
        /// </summary>
        /// <remarks>
        ///     This callback will also be called when a vehicle enters water, but the vehicle can be saved from destruction by
        ///     teleportation or driving out (if only partially submerged). The callback won't be called a second time, and the
        ///     vehicle may disappear when the driver exits, or after a short time.
        /// </remarks>
        public event EventHandler<PlayerEventArgs> VehicleDied;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerText(GtaPlayer,TextEventArgs)" /> callback is being called.
        ///     Called when a player sends a chat message.
        /// </summary>
        public event EventHandler<TextEventArgs> PlayerText;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerCommandText(GtaPlayer,CommandTextEventArgs)" /> callback is being called.
        ///     This callback is called when a player enters a command into the client chat window, e.g. /help.
        /// </summary>
        public event EventHandler<CommandTextEventArgs> PlayerCommandText;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerRequestClass(GtaPlayer,RequestClassEventArgs)" /> callback is being called.
        ///     Called when a player changes class at class selection (and when class selection first appears).
        /// </summary>
        public event EventHandler<RequestClassEventArgs> PlayerRequestClass;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEnterVehicle(GtaPlayer,EnterVehicleEventArgs)" /> callback is being called.
        ///     This callback is called when a player starts to enter a vehicle, meaning the player is not in vehicle yet at the
        ///     time this callback is called.
        /// </summary>
        public event EventHandler<EnterVehicleEventArgs> PlayerEnterVehicle;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerExitVehicle(GtaPlayer,PlayerVehicleEventArgs)" /> callback is being called.
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
        public event EventHandler<StateEventArgs> PlayerStateChanged;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEnterCheckpoint(GtaPlayer,EventArgs)" /> callback is being called.
        ///     This callback is called when a player enters the checkpoint set for that player.
        /// </summary>
        public event EventHandler<EventArgs> PlayerEnterCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerLeaveCheckpoint(GtaPlayer,EventArgs)" /> callback is being called.
        ///     This callback is called when a player leaves the checkpoint set for that player.
        /// </summary>
        public event EventHandler<EventArgs> PlayerLeaveCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEnterRaceCheckpoint(GtaPlayer,EventArgs)" /> callback is being called.
        ///     This callback is called when a player enters a race checkpoint.
        /// </summary>
        public event EventHandler<EventArgs> PlayerEnterRaceCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerLeaveRaceCheckpoint(GtaPlayer,EventArgs)" /> callback is being called.
        ///     This callback is called when a player leaves the race checkpoint.
        /// </summary>
        public event EventHandler<EventArgs> PlayerLeaveRaceCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnRconCommand(RconEventArgs)" /> callback is being called.
        ///     This callback is called when a command is sent through the server console, remote RCON, or via the in-game /rcon
        ///     command.
        /// </summary>
        public event EventHandler<RconEventArgs> RconCommand;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerRequestSpawn(GtaPlayer,RequestSpawnEventArgs)" /> callback is being called.
        ///     Called when a player attempts to spawn via class selection.
        /// </summary>
        public event EventHandler<RequestSpawnEventArgs> PlayerRequestSpawn;

        /// <summary>
        ///     Occurs when the <see cref="OnObjectMoved(GlobalObject,EventArgs)" /> callback is being called.
        ///     This callback is called when an object is moved after <see cref="Native.MoveObject(int,Vector,float,Vector)" />
        ///     (when it stops moving).
        /// </summary>
        public event EventHandler<EventArgs> ObjectMoved;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerObjectMoved(PlayerObject,EventArgs)" /> callback is being called.
        ///     This callback is called when a player object is moved after
        ///     <see cref="Native.MovePlayerObject(int,int,Vector,float,Vector)" /> (when it stops
        ///     moving).
        /// </summary>
        public event EventHandler<EventArgs> PlayerObjectMoved;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerPickUpPickup(Pickup,PlayerEventArgs)" /> callback is being called.
        ///     Called when a player picks up a pickup created with <see cref="Native.CreatePickup" />.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerPickUpPickup;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleMod(GtaVehicle,VehicleModEventArgs)" /> callback is being called.
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
        public event EventHandler<EnterModShopEventArgs> PlayerEnterExitModShop;

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
        public event EventHandler<PlayerEventArgs> VehicleDamageStatusUpdated;

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
        ///     Occurs when the <see cref="OnPlayerSelectedMenuRow(GtaPlayer,MenuRowEventArgs)" /> callback is being called.
        ///     This callback is called when a player selects an item from a menu.
        /// </summary>
        public event EventHandler<MenuRowEventArgs> PlayerSelectedMenuRow;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerExitedMenu(GtaPlayer,EventArgs)" /> callback is being called.
        ///     Called when a player exits a menu.
        /// </summary>
        public event EventHandler<EventArgs> PlayerExitedMenu;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerInteriorChange" /> callback is being called.
        ///     Called when a player changes interior.
        /// </summary>
        /// <remarks>
        ///     This is also called when <see cref="Native.SetPlayerInterior" /> is used.
        /// </remarks>
        public event EventHandler<InteriorChangedEventArgs> PlayerInteriorChanged;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerKeyStateChange" /> callback is being called.
        ///     This callback is called when the state of any supported key is changed (pressed/released). Directional keys do not
        ///     trigger this callback.
        /// </summary>
        public event EventHandler<KeyStateChangedEventArgs> PlayerKeyStateChanged;

        /// <summary>
        ///     Occurs when the <see cref="OnRconLoginAttempt(RconLoginAttemptEventArgs)" /> callback is being called.
        ///     This callback is called when someone tries to login to RCON, succesful or not.
        /// </summary>
        /// <remarks>
        ///     This callback is only called when /rcon login is used.
        /// </remarks>
        public event EventHandler<RconLoginAttemptEventArgs> RconLoginAttempt;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerUpdate(GtaPlayer,PlayerUpdateEventArgs)" /> callback is being called.
        ///     This callback is called everytime a client/player updates the server with their status.
        /// </summary>
        /// <remarks>
        ///     This callback is called very frequently per second per player, only use it when you know what it's meant for.
        /// </remarks>
        public event EventHandler<PlayerUpdateEventArgs> PlayerUpdate;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerStreamIn(GtaPlayer,PlayerEventArgs)" /> callback is being called.
        ///     This callback is called when a player is streamed by some other player's client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerStreamIn;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerStreamOut(GtaPlayer,PlayerEventArgs)" /> callback is being called.
        ///     This callback is called when a player is streamed out from some other player's client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerStreamOut;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleStreamIn(GtaVehicle,PlayerEventArgs)" /> callback is being called.
        ///     Called when a vehicle is streamed to a player's client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> VehicleStreamIn;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleStreamOut(GtaVehicle,PlayerEventArgs)" /> callback is being called.
        ///     This callback is called when a vehicle is streamed out from some player's client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> VehicleStreamOut;

        /// <summary>
        ///     Occurs when the <see cref="OnTrailerUpdate(GtaVehicle,TrailerEventArgs)" /> callback is being called.
        ///     This callback is called when a player sent a trailer update.
        /// </summary>
        public event EventHandler<TrailerEventArgs> TrailerUpdate;

        /// <summary>
        ///     Occurs when the <see cref="OnDialogResponse(GtaPlayer,DialogResponseEventArgs)" /> callback is being called.
        ///     This callback is called when a player responds to a dialog shown using <see cref="Native.ShowPlayerDialog" /> by
        ///     either clicking a button, pressing ENTER/ESC or double-clicking a list item (if using a list style dialog).
        /// </summary>
        public event EventHandler<DialogResponseEventArgs> DialogResponse;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerTakeDamage(GtaPlayer,DamagePlayerEventArgs)" /> callback is being called.
        ///     This callback is called when a player takes damage.
        /// </summary>
        public event EventHandler<DamagePlayerEventArgs> PlayerTakeDamage;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerGiveDamage(GtaPlayer,DamagePlayerEventArgs)" /> callback is being called.
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
        public event EventHandler<DamagePlayerEventArgs> PlayerGiveDamage;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerClickMap(GtaPlayer,PositionEventArgs)" /> callback is being called.
        ///     This callback is called when a player places a target/waypoint on the pause menu map (by right-clicking).
        /// </summary>
        /// <remarks>
        ///     The Z value provided is only an estimate; you may find it useful to use a plugin like the MapAndreas plugin to get
        ///     a more accurate Z coordinate (or for teleportation; use <see cref="Native.SetPlayerPosFindZ(int,Vector)" />).
        /// </remarks>
        public event EventHandler<PositionEventArgs> PlayerClickMap;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerClickTextDraw(GtaPlayer,ClickTextDrawEventArgs)" /> callback is being called.
        ///     This callback is called when a player clicks on a textdraw or cancels the select mode(ESC).
        /// </summary>
        /// <remarks>
        ///     The clickable area is defined by <see cref="Native.TextDrawTextSize" />. The x and y parameters passed to that
        ///     function must not be zero or negative.
        /// </remarks>
        public event EventHandler<ClickTextDrawEventArgs> PlayerClickTextDraw;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerClickPlayerTextDraw(GtaPlayer,ClickPlayerTextDrawEventArgs)" /> callback is
        ///     being
        ///     called.
        ///     This callback is called when a player clicks on a player-textdraw. It is not called when player cancels the select
        ///     mode (ESC) - however, <see cref="OnPlayerClickTextDraw(GtaPlayer,ClickTextDrawEventArgs)" /> is.
        /// </summary>
        public event EventHandler<ClickPlayerTextDrawEventArgs> PlayerClickPlayerTextDraw;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerClickPlayer(GtaPlayer, ClickPlayerEventArgs)" /> callback is being called.
        ///     Called when a player double-clicks on a player on the scoreboard.
        /// </summary>
        /// <remarks>
        ///     There is currently only one 'source' (<see cref="PlayerClickSource.Scoreboard" />). The existence of this argument
        ///     suggests that more sources may be supported in the future.
        /// </remarks>
        public event EventHandler<ClickPlayerEventArgs> PlayerClickPlayer;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEditGlobalObject(GtaPlayer,EditGlobalObjectEventArgs)" /> callback is being
        ///     called.
        ///     This callback is called when a player ends global object edition mode.
        /// </summary>
        public event EventHandler<EditGlobalObjectEventArgs> PlayerEditGlobalObject;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEditPlayerObject(GtaPlayer,EditPlayerObjectEventArgs)" /> callback is being
        ///     called.
        ///     This callback is called when a player ends player object edition mode.
        /// </summary>
        public event EventHandler<EditPlayerObjectEventArgs> PlayerEditPlayerObject;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEditAttachedObject(GtaPlayer,EditAttachedObjectEventArgs)" /> callback is being
        ///     called.
        ///     This callback is called when a player ends attached object edition mode.
        /// </summary>
        /// <remarks>
        ///     Editions should be discarded if response was '0' (cancelled). This must be done by storing the offsets etc. in an
        ///     array BEFORE using EditAttachedObject.
        /// </remarks>
        public event EventHandler<EditAttachedObjectEventArgs> PlayerEditAttachedObject;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerSelectGlobalObject" /> callback is being called.
        ///     This callback is called when a player selects an object after <see cref="Native.SelectObject" /> has been used.
        /// </summary>
        public event EventHandler<SelectGlobalObjectEventArgs> PlayerSelectGlobalObject;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerSelectPlayerObject" /> callback is being called.
        ///     This callback is called when a player selects an object after <see cref="Native.SelectObject" /> has been used.
        /// </summary>
        public event EventHandler<SelectPlayerObjectEventArgs> PlayerSelectPlayerObject;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerWeaponShot(GtaPlayer,WeaponShotEventArgs)" /> callback is being called.
        ///     This callback is called when a player fires a shot from a weapon.
        /// </summary>
        /// <remarks>
        ///     <see cref="BulletHitType.None" />: the fX, fY and fZ parameters are normal coordinates;
        ///     Others: the fX, fY and fZ are offsets from the center of hitid.
        /// </remarks>
        public event EventHandler<WeaponShotEventArgs> PlayerWeaponShot;

        /// <summary>
        ///     Occurs when the <see cref="OnIncomingConnection(ConnectionEventArgs)" /> callback is being called.
        ///     This callback is called when an IP address attempts a connection to the server.
        /// </summary>
        public event EventHandler<ConnectionEventArgs> IncomingConnection;

        /// <summary>
        ///     Occurs when the <see cref="OnTick(EventArgs)" /> callback is being called. This callback is called every
        ///     tick(50 times per second).
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
        ///     Raises the <see cref="Initialized" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnInitialized(EventArgs e)
        {
            if (Initialized != null)
                Initialized(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Exited" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnExited(EventArgs e)
        {
            if (Exited != null)
                Exited(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerConnected" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerConnected(GtaPlayer player, EventArgs e)
        {
            if (PlayerConnected != null)
                PlayerConnected(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerDisconnected" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="DisconnectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerDisconnected(GtaPlayer player, DisconnectEventArgs e)
        {
            if (PlayerDisconnected != null)
                PlayerDisconnected(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerCleanup" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="DisconnectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerCleanup(GtaPlayer player, DisconnectEventArgs e)
        {
            if (PlayerCleanup != null)
                PlayerCleanup(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerSpawned" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerSpawned(GtaPlayer player, SpawnEventArgs e)
        {
            if (PlayerSpawned != null)
                PlayerSpawned(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerDied" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="DeathEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerDied(GtaPlayer player, DeathEventArgs e)
        {
            if (PlayerDied != null)
                PlayerDied(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleSpawned" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleSpawned(GtaVehicle vehicle, EventArgs e)
        {
            if (VehicleSpawned != null)
                VehicleSpawned(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleDied" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleDied(GtaVehicle vehicle, PlayerEventArgs e)
        {
            if (VehicleDied != null)
                VehicleDied(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerText" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="TextEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerText(GtaPlayer player, TextEventArgs e)
        {
            if (PlayerText != null)
                PlayerText(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerCommandText" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="TextEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerCommandText(GtaPlayer player, CommandTextEventArgs e)
        {
            if (PlayerCommandText != null)
                PlayerCommandText(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerRequestClass" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="RequestClassEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerRequestClass(GtaPlayer player, RequestClassEventArgs e)
        {
            if (PlayerRequestClass != null)
                PlayerRequestClass(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEnterVehicle" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EnterVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEnterVehicle(GtaPlayer player, EnterVehicleEventArgs e)
        {
            if (PlayerEnterVehicle != null)
                PlayerEnterVehicle(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerExitVehicle" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerExitVehicle(GtaPlayer player, PlayerVehicleEventArgs e)
        {
            if (PlayerExitVehicle != null)
                PlayerExitVehicle(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerStateChanged" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="StateEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerStateChanged(GtaPlayer player, StateEventArgs e)
        {
            if (PlayerStateChanged != null)
                PlayerStateChanged(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEnterCheckpoint" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEnterCheckpoint(GtaPlayer player, EventArgs e)
        {
            if (PlayerEnterCheckpoint != null)
                PlayerEnterCheckpoint(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerLeaveCheckpoint" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerLeaveCheckpoint(GtaPlayer player, EventArgs e)
        {
            if (PlayerLeaveCheckpoint != null)
                PlayerLeaveCheckpoint(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEnterRaceCheckpoint" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEnterRaceCheckpoint(GtaPlayer player, EventArgs e)
        {
            if (PlayerEnterRaceCheckpoint != null)
                PlayerEnterRaceCheckpoint(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerLeaveRaceCheckpoint" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerLeaveRaceCheckpoint(GtaPlayer player, EventArgs e)
        {
            if (PlayerLeaveRaceCheckpoint != null)
                PlayerLeaveRaceCheckpoint(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="RconCommand" /> event.
        /// </summary>
        /// <param name="e">An <see cref="RconEventArgs" /> that contains the event data. </param>
        protected virtual void OnRconCommand(RconEventArgs e)
        {
            if (RconCommand != null)
                RconCommand(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerRequestSpawn" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerRequestSpawn(GtaPlayer player, RequestSpawnEventArgs e)
        {
            if (PlayerRequestSpawn != null)
                PlayerRequestSpawn(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="ObjectMoved" /> event.
        /// </summary>
        /// <param name="globalObject">The global-object triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnObjectMoved(GlobalObject globalObject, EventArgs e)
        {
            if (ObjectMoved != null)
                ObjectMoved(globalObject, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerObjectMoved" /> event.
        /// </summary>
        /// <param name="playerObject">The player-object triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerObjectMoved(PlayerObject playerObject, EventArgs e)
        {
            if (PlayerObjectMoved != null)
                PlayerObjectMoved(playerObject, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerPickUpPickup" /> event.
        /// </summary>
        /// <param name="pickup">The pickup triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerPickUpPickup(Pickup pickup, PlayerEventArgs e)
        {
            if (PlayerPickUpPickup != null)
                PlayerPickUpPickup(pickup, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleMod" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="VehicleModEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleMod(GtaVehicle vehicle, VehicleModEventArgs e)
        {
            if (VehicleMod != null)
                VehicleMod(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEnterExitModShop" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EnterModShopEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEnterExitModShop(GtaPlayer player, EnterModShopEventArgs e)
        {
            if (PlayerEnterExitModShop != null)
                PlayerEnterExitModShop(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehiclePaintjobApplied" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="VehiclePaintjobEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehiclePaintjobApplied(GtaVehicle vehicle, VehiclePaintjobEventArgs e)
        {
            if (VehiclePaintjobApplied != null)
                VehiclePaintjobApplied(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleResprayed" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="VehicleResprayedEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleResprayed(GtaVehicle vehicle, VehicleResprayedEventArgs e)
        {
            if (VehicleResprayed != null)
                VehicleResprayed(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleDamageStatusUpdated" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleDamageStatusUpdated(GtaVehicle vehicle, PlayerEventArgs e)
        {
            if (VehicleDamageStatusUpdated != null)
                VehicleDamageStatusUpdated(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="UnoccupiedVehicleUpdated" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="UnoccupiedVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnUnoccupiedVehicleUpdated(GtaVehicle vehicle, UnoccupiedVehicleEventArgs e)
        {
            if (UnoccupiedVehicleUpdated != null)
                UnoccupiedVehicleUpdated(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerSelectedMenuRow" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="MenuRowEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerSelectedMenuRow(GtaPlayer player, MenuRowEventArgs e)
        {
            if (PlayerSelectedMenuRow != null)
                PlayerSelectedMenuRow(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerExitedMenu" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerExitedMenu(GtaPlayer player, EventArgs e)
        {
            if (PlayerExitedMenu != null)
                PlayerExitedMenu(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerInteriorChanged" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="InteriorChangedEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerInteriorChanged(GtaPlayer player, InteriorChangedEventArgs e)
        {
            if (PlayerInteriorChanged != null)
                PlayerInteriorChanged(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerKeyStateChanged" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="KeyStateChangedEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerKeyStateChanged(GtaPlayer player, KeyStateChangedEventArgs e)
        {
            if (PlayerKeyStateChanged != null)
                PlayerKeyStateChanged(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="RconLoginAttempt" /> event.
        /// </summary>
        /// <param name="e">An <see cref="RconLoginAttemptEventArgs" /> that contains the event data. </param>
        protected virtual void OnRconLoginAttempt(RconLoginAttemptEventArgs e)
        {
            if (RconLoginAttempt != null)
                RconLoginAttempt(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerUpdate" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerUpdate(GtaPlayer player, PlayerUpdateEventArgs e)
        {
            if (PlayerUpdate != null)
                PlayerUpdate(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerStreamIn" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerStreamIn(GtaPlayer player, PlayerEventArgs e)
        {
            if (PlayerStreamIn != null)
                PlayerStreamIn(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerStreamOut" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerStreamOut(GtaPlayer player, PlayerEventArgs e)
        {
            if (PlayerStreamOut != null)
                PlayerStreamOut(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleStreamIn" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleStreamIn(GtaVehicle vehicle, PlayerEventArgs e)
        {
            if (VehicleStreamIn != null)
                VehicleStreamIn(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleStreamOut" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleStreamOut(GtaVehicle vehicle, PlayerEventArgs e)
        {
            if (VehicleStreamOut != null)
                VehicleStreamOut(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="TrailerUpdate" /> event.
        /// </summary>
        /// <param name="trailer">The trailer triggering the event.</param>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnTrailerUpdate(GtaVehicle trailer, TrailerEventArgs e)
        {
            if (TrailerUpdate != null)
                TrailerUpdate(trailer, e);
        }

        /// <summary>
        ///     Raises the <see cref="DialogResponse" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="DialogResponseEventArgs" /> that contains the event data. </param>
        protected virtual void OnDialogResponse(GtaPlayer player, DialogResponseEventArgs e)
        {
            if (DialogResponse != null)
                DialogResponse(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerTakeDamage" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="DamagePlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerTakeDamage(GtaPlayer player, DamagePlayerEventArgs e)
        {
            if (PlayerTakeDamage != null)
                PlayerTakeDamage(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerGiveDamage" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="DamagePlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerGiveDamage(GtaPlayer player, DamagePlayerEventArgs e)
        {
            if (PlayerGiveDamage != null)
                PlayerGiveDamage(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerClickMap" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PositionEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerClickMap(GtaPlayer player, PositionEventArgs e)
        {
            if (PlayerClickMap != null)
                PlayerClickMap(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerClickTextDraw" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="ClickTextDrawEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerClickTextDraw(GtaPlayer player, ClickTextDrawEventArgs e)
        {
            if (PlayerClickTextDraw != null)
                PlayerClickTextDraw(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerClickPlayerTextDraw" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="ClickTextDrawEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerClickPlayerTextDraw(GtaPlayer player, ClickPlayerTextDrawEventArgs e)
        {
            if (PlayerClickPlayerTextDraw != null)
                PlayerClickPlayerTextDraw(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerClickPlayer" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="ClickPlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerClickPlayer(GtaPlayer player, ClickPlayerEventArgs e)
        {
            if (PlayerClickPlayer != null)
                PlayerClickPlayer(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEditGlobalObject" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EditGlobalObjectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEditGlobalObject(GtaPlayer player, EditGlobalObjectEventArgs e)
        {
            if (PlayerEditGlobalObject != null)
                PlayerEditGlobalObject(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEditPlayerObject" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EditPlayerObjectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEditPlayerObject(GtaPlayer player, EditPlayerObjectEventArgs e)
        {
            if (PlayerEditPlayerObject != null)
                PlayerEditPlayerObject(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEditAttachedObject" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EditAttachedObjectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEditAttachedObject(GtaPlayer player, EditAttachedObjectEventArgs e)
        {
            if (PlayerEditAttachedObject != null)
                PlayerEditAttachedObject(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerSelectGlobalObject" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="SelectGlobalObjectEventArgs" /> that contains the event data.</param>
        protected virtual void OnPlayerSelectGlobalObject(GtaPlayer player, SelectGlobalObjectEventArgs e)
        {
            if (PlayerSelectGlobalObject != null)
                PlayerSelectGlobalObject(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerSelectPlayerObject" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="SelectPlayerObjectEventArgs" /> that contains the event data.</param>
        protected virtual void OnPlayerSelectPlayerObject(GtaPlayer player, SelectPlayerObjectEventArgs e)
        {
            if (PlayerSelectPlayerObject != null)
                PlayerSelectPlayerObject(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerWeaponShot" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="WeaponShotEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerWeaponShot(GtaPlayer player, WeaponShotEventArgs e)
        {
            if (PlayerWeaponShot != null)
                PlayerWeaponShot(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="IncomingConnection" /> event.
        /// </summary>
        /// <param name="e">An <see cref="ConnectionEventArgs" /> that contains the event dEventArgsata. </param>
        protected virtual void OnIncomingConnection(ConnectionEventArgs e)
        {
            if (IncomingConnection != null)
                IncomingConnection(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Tick" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnTick(EventArgs e)
        {
            if (Tick != null)
                Tick(this, e);
        }
    }
}