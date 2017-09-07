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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode
{
    public abstract partial class BaseMode
    {
        /// <summary>
        ///     Occurs when the <see cref="OnInitialized" /> callback is being called.
        ///     This callback is triggered when the gamemode starts.
        /// </summary>
        public event EventHandler<EventArgs> Initialized;

        /// <summary>
        ///     Occurs when the <see cref="OnExited" /> callback is being called.
        ///     This callback is called when a gamemode ends.
        /// </summary>
        public event EventHandler<EventArgs> Exited;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerConnected" /> callback is being called.
        ///     This callback is called when a player connects to the server.
        /// </summary>
        public event EventHandler<EventArgs> PlayerConnected;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerDisconnected" /> callback is being called.
        ///     This callback is called when a player disconnects from the server.
        /// </summary>
        public event EventHandler<DisconnectEventArgs> PlayerDisconnected;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerCleanup" /> callback is being called.
        ///     This callback is called after a player has disconnected.
        /// </summary>
        /// <remarks>
        ///     Because <see cref="BasePlayer" /> probably is the first listener of this event,
        ///     the <see cref="BasePlayer" /> object is already disposed before any other listeners are called.
        ///     It is better to either use the <see cref="PlayerDisconnected" /> event or <see cref="BasePlayer.Cleanup" />
        /// </remarks>
        public event EventHandler<DisconnectEventArgs> PlayerCleanup;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerSpawned" /> callback is being called.
        ///     This callback is called when a player spawns.
        /// </summary>
        public event EventHandler<SpawnEventArgs> PlayerSpawned;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerDied" /> callback is being called.
        ///     This callback is triggered when the gamemode starts.
        /// </summary>
        public event EventHandler<DeathEventArgs> PlayerDied;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleSpawned" /> callback is being called.
        ///     This callback is called when a vehicle respawns.
        /// </summary>
        public event EventHandler<EventArgs> VehicleSpawned;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleDied" /> callback is being called.
        ///     This callback is called when a vehicle is destroyed - either by exploding or becoming submerged in water.
        /// </summary>
        /// <remarks>
        ///     This callback will also be called when a vehicle enters water, but the vehicle can be saved from destruction by
        ///     teleportation or driving out (if only partially submerged). The callback won't be called a second time, and the
        ///     vehicle may disappear when the driver exits, or after a short time.
        /// </remarks>
        public event EventHandler<PlayerEventArgs> VehicleDied;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerText(BasePlayer,TextEventArgs)" /> callback is being called.
        ///     Called when a player sends a chat message.
        /// </summary>
        public event EventHandler<TextEventArgs> PlayerText;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerCommandText(BasePlayer,CommandTextEventArgs)" /> callback is being called.
        ///     This callback is called when a player enters a command into the client chat window, e.g. /help.
        /// </summary>
        public event EventHandler<CommandTextEventArgs> PlayerCommandText;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerRequestClass(BasePlayer,RequestClassEventArgs)" /> callback is being called.
        ///     Called when a player changes class at class selection (and when class selection first appears).
        /// </summary>
        public event EventHandler<RequestClassEventArgs> PlayerRequestClass;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEnterVehicle(BasePlayer,EnterVehicleEventArgs)" /> callback is being called.
        ///     This callback is called when a player starts to enter a vehicle, meaning the player is not in vehicle yet at the
        ///     time this callback is called.
        /// </summary>
        public event EventHandler<EnterVehicleEventArgs> PlayerEnterVehicle;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerExitVehicle(BasePlayer,PlayerVehicleEventArgs)" /> callback is being called.
        ///     This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        ///     Not called if the player falls off a bike or is removed from a vehicle by other means such as setting
        ///     <see cref="BasePlayer.Position" />.
        /// </remarks>
        public event EventHandler<PlayerVehicleEventArgs> PlayerExitVehicle;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerStateChanged" /> callback is being called.
        ///     This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        ///     Not called if the player falls off a bike or is removed from a vehicle by other means such as setting
        ///     <see cref="BasePlayer.Position" />.
        /// </remarks>
        public event EventHandler<StateEventArgs> PlayerStateChanged;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEnterCheckpoint(BasePlayer,EventArgs)" /> callback is being called.
        ///     This callback is called when a player enters the checkpoint set for that player.
        /// </summary>
        public event EventHandler<EventArgs> PlayerEnterCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerLeaveCheckpoint(BasePlayer,EventArgs)" /> callback is being called.
        ///     This callback is called when a player leaves the checkpoint set for that player.
        /// </summary>
        public event EventHandler<EventArgs> PlayerLeaveCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEnterRaceCheckpoint(BasePlayer,EventArgs)" /> callback is being called.
        ///     This callback is called when a player enters a race checkpoint.
        /// </summary>
        public event EventHandler<EventArgs> PlayerEnterRaceCheckpoint;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerLeaveRaceCheckpoint(BasePlayer,EventArgs)" /> callback is being called.
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
        ///     Occurs when the <see cref="OnPlayerRequestSpawn(BasePlayer,RequestSpawnEventArgs)" /> callback is being called.
        ///     Called when a player attempts to spawn via class selection.
        /// </summary>
        public event EventHandler<RequestSpawnEventArgs> PlayerRequestSpawn;

        /// <summary>
        ///     Occurs when the <see cref="OnObjectMoved(GlobalObject,EventArgs)" /> callback is being called.
        ///     This callback is called when an object is moved using <see cref="GlobalObject.Move(Vector3,float)" /> or
        ///     <see cref="GlobalObject.Move(Vector3,float, Vector3)" /> (when it stops moving).
        /// </summary>
        public event EventHandler<EventArgs> ObjectMoved;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerObjectMoved(PlayerObject,EventArgs)" /> callback is being called.
        ///     This callback is called when an object is moved using <see cref="PlayerObject.Move(Vector3,float)" /> or
        ///     <see cref="PlayerObject.Move(Vector3,float, Vector3)" /> (when it stops moving).
        /// </summary>
        public event EventHandler<EventArgs> PlayerObjectMoved;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerPickUpPickup(Pickup,PlayerEventArgs)" /> callback is being called.
        ///     Called when a player picks up a pickup created with <see cref="Pickup" />.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerPickUpPickup;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleMod(BaseVehicle,VehicleModEventArgs)" /> callback is being called.
        ///     This callback is called when a vehicle is modded.
        /// </summary>
        /// <remarks>
        ///     This callback is not called by <see cref="BaseVehicle.AddComponent" />.
        /// </remarks>
        public event EventHandler<VehicleModEventArgs> VehicleMod;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEnterExitModShop" /> callback is being called.
        ///     This callback is called when a player enters or exits a mod shop.
        /// </summary>
        public event EventHandler<EnterModShopEventArgs> PlayerEnterExitModShop;

        /// <summary>
        ///     Occurs when the <see cref="OnVehiclePaintjobApplied" /> callback is being called.
        ///     Called when a player changes the paintjob of their vehicle (in a modshop).
        /// </summary>
        public event EventHandler<VehiclePaintjobEventArgs> VehiclePaintjobApplied;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleResprayed" /> callback is being called.
        ///     The callback name is deceptive, this callback is called when a player exits a mod shop, regardless of whether the
        ///     vehicle's colors were changed, and is NEVER called for pay 'n' spray garages.
        /// </summary>
        /// <remarks>
        ///     Misleadingly, this callback is not called for pay 'n' spray (only modshops).
        /// </remarks>
        public event EventHandler<VehicleResprayedEventArgs> VehicleResprayed;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleDamageStatusUpdated" /> callback is being called.
        ///     This callback is called when a vehicle element such as doors, tires, panels, or lights get damaged.
        /// </summary>
        /// <remarks>
        ///     This does not include vehicle health changes.
        /// </remarks>
        public event EventHandler<PlayerEventArgs> VehicleDamageStatusUpdated;

        /// <summary>
        ///     Occurs when the <see cref="OnUnoccupiedVehicleUpdated" /> callback is being called.
        ///     This callback is called everytime an unoccupied vehicle updates the server with their status.
        /// </summary>
        /// <remarks>
        ///     This callback is called very frequently per second per unoccupied vehicle. You should refrain from implementing
        ///     intensive calculations or intensive file writing/reading operations in this callback.
        /// </remarks>
        public event EventHandler<UnoccupiedVehicleEventArgs> UnoccupiedVehicleUpdated;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerSelectedMenuRow(BasePlayer,MenuRowEventArgs)" /> callback is being called.
        ///     This callback is called when a player selects an item from a menu.
        /// </summary>
        public event EventHandler<MenuRowEventArgs> PlayerSelectedMenuRow;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerExitedMenu(BasePlayer,EventArgs)" /> callback is being called.
        ///     Called when a player exits a menu.
        /// </summary>
        public event EventHandler<EventArgs> PlayerExitedMenu;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerInteriorChanged" /> callback is being called.
        ///     Called when a player changes interior.
        /// </summary>
        /// <remarks>
        ///     This is also called when <see cref="BasePlayer.Interior" /> is set.
        /// </remarks>
        public event EventHandler<InteriorChangedEventArgs> PlayerInteriorChanged;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerKeyStateChanged" /> callback is being called.
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
        ///     Occurs when the <see cref="OnPlayerUpdate(BasePlayer,PlayerUpdateEventArgs)" /> callback is being called.
        ///     This callback is called everytime a client/player updates the server with their status.
        /// </summary>
        /// <remarks>
        ///     This callback is called very frequently per second per player, only use it when you know what it's meant for.
        /// </remarks>
        public event EventHandler<PlayerUpdateEventArgs> PlayerUpdate;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerStreamIn(BasePlayer,PlayerEventArgs)" /> callback is being called.
        ///     This callback is called when a player is streamed by some other player's client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerStreamIn;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerStreamOut(BasePlayer,PlayerEventArgs)" /> callback is being called.
        ///     This callback is called when a player is streamed out from some other player's client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> PlayerStreamOut;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleStreamIn(BaseVehicle,PlayerEventArgs)" /> callback is being called.
        ///     Called when a vehicle is streamed to a player's client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> VehicleStreamIn;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleStreamOut(BaseVehicle,PlayerEventArgs)" /> callback is being called.
        ///     This callback is called when a vehicle is streamed out from some player's client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> VehicleStreamOut;

        /// <summary>
        ///     Occurs when the <see cref="OnTrailerUpdate(BaseVehicle,TrailerEventArgs)" /> callback is being called.
        ///     This callback is called when a player sent a trailer update.
        /// </summary>
        public event EventHandler<TrailerEventArgs> TrailerUpdate;

        /// <summary>
        ///     Occurs when the <see cref="OnDialogResponse(BasePlayer,DialogResponseEventArgs)" /> callback is being called.
        ///     This callback is called when a player responds to a dialog shown using <see cref="Dialog" /> by
        ///     either clicking a button, pressing ENTER/ESC or double-clicking a list item (if using a list style dialog).
        /// </summary>
        public event EventHandler<DialogResponseEventArgs> DialogResponse;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerTakeDamage(BasePlayer,DamageEventArgs)" /> callback is being called.
        ///     This callback is called when a player takes damage.
        /// </summary>
        public event EventHandler<DamageEventArgs> PlayerTakeDamage;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerGiveDamage(BasePlayer,DamageEventArgs)" /> callback is being called.
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
        public event EventHandler<DamageEventArgs> PlayerGiveDamage;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerClickMap(BasePlayer,PositionEventArgs)" /> callback is being called.
        ///     This callback is called when a player places a target/waypoint on the pause menu map (by right-clicking).
        /// </summary>
        /// <remarks>
        ///     The Z value provided is only an estimate; you may find it useful to use a plugin like the MapAndreas plugin to get
        ///     a more accurate Z coordinate (or for teleportation; use <see cref="BasePlayer.SetPositionFindZ" />).
        /// </remarks>
        public event EventHandler<PositionEventArgs> PlayerClickMap;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerClickTextDraw(BasePlayer,ClickTextDrawEventArgs)" /> callback is being called.
        ///     This callback is called when a player clicks on a textdraw or cancels the select mode(ESC).
        /// </summary>
        /// <remarks>
        ///     The clickable area is defined by <see cref="TextDraw.Width" /> and <see cref="TextDraw.Height" />.
        /// </remarks>
        public event EventHandler<ClickTextDrawEventArgs> PlayerClickTextDraw;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerClickPlayerTextDraw(BasePlayer,ClickPlayerTextDrawEventArgs)" /> callback is
        ///     being
        ///     called.
        ///     This callback is called when a player clicks on a player-textdraw. It is not called when player cancels the select
        ///     mode (ESC) - however, <see cref="OnPlayerClickTextDraw(BasePlayer,ClickTextDrawEventArgs)" /> is.
        /// </summary>
        public event EventHandler<ClickPlayerTextDrawEventArgs> PlayerClickPlayerTextDraw;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerClickPlayer(BasePlayer, ClickPlayerEventArgs)" /> callback is being called.
        ///     Called when a player double-clicks on a player on the scoreboard.
        /// </summary>
        /// <remarks>
        ///     There is currently only one 'source' (<see cref="PlayerClickSource.Scoreboard" />). The existence of this argument
        ///     suggests that more sources may be supported in the future.
        /// </remarks>
        public event EventHandler<ClickPlayerEventArgs> PlayerClickPlayer;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEditGlobalObject(BasePlayer,EditGlobalObjectEventArgs)" /> callback is being
        ///     called.
        ///     This callback is called when a player ends global object edition mode.
        /// </summary>
        public event EventHandler<EditGlobalObjectEventArgs> PlayerEditGlobalObject;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEditPlayerObject(BasePlayer,EditPlayerObjectEventArgs)" /> callback is being
        ///     called.
        ///     This callback is called when a player ends player object edition mode.
        /// </summary>
        public event EventHandler<EditPlayerObjectEventArgs> PlayerEditPlayerObject;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerEditAttachedObject(BasePlayer,EditAttachedObjectEventArgs)" /> callback is being
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
        ///     This callback is called when a player selects an object after <see cref="GlobalObject.Select" /> has been used.
        /// </summary>
        public event EventHandler<SelectGlobalObjectEventArgs> PlayerSelectGlobalObject;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerSelectPlayerObject" /> callback is being called.
        ///     This callback is called when a player selects an object after <see cref="PlayerObject.Select" /> has been used.
        /// </summary>
        public event EventHandler<SelectPlayerObjectEventArgs> PlayerSelectPlayerObject;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerWeaponShot(BasePlayer,WeaponShotEventArgs)" /> callback is being called.
        ///     This callback is called when a player fires a shot from a weapon.
        /// </summary>
        public event EventHandler<WeaponShotEventArgs> PlayerWeaponShot;

        /// <summary>
        ///     Occurs when the <see cref="OnIncomingConnection(ConnectionEventArgs)" /> callback is being called.
        ///     This callback is called when an IP address attempts a connection to the server.
        /// </summary>
        public event EventHandler<ConnectionEventArgs> IncomingConnection;

        /// <summary>
        ///     Occurs when the <see cref="OnPlayerGiveDamageActor(Actor,DamageEventArgs)" /> callback is being called.
        ///     This callback is called when a player gives damage to an actor.
        /// </summary>
        public event EventHandler<DamageEventArgs> PlayerGiveDamageActor;

        /// <summary>
        ///     Occurs when the <see cref="OnVehicleSirenStateChange(BaseVehicle,SirenStateEventArgs)" /> callback is being called.
        ///     This callback is called when a vehicle's siren is toggled.
        /// </summary>
        public event EventHandler<SirenStateEventArgs> VehicleSirenStateChange;

        /// <summary>
        ///     Occurs when the <see cref="OnActorStreamIn(Actor, PlayerEventArgs)" /> callback is being called.
        ///     This callback is called when an actor is streamed in by a player's client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> ActorStreamIn;

        /// <summary>
        ///     Occurs when the <see cref="OnActorStreamOut(Actor, PlayerEventArgs)" /> callback is being called.
        ///     This callback is called when an actor is streamed out by a player's client.
        /// </summary>
        public event EventHandler<PlayerEventArgs> ActorStreamOut;

        /// <summary>
        ///     Occurs when the <see cref="OnTick(EventArgs)" /> callback is being called. This callback is called every
        ///     tick(50 times per second).
        /// </summary>
        public event EventHandler<EventArgs> Tick;
        
        /// <summary>
        ///     Raises the <see cref="Initialized" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnInitialized(EventArgs e)
        {
            Initialized?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="Exited" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnExited(EventArgs e)
        {
            Exited?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerConnected" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerConnected(BasePlayer player, EventArgs e)
        {
            PlayerConnected?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerDisconnected" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="DisconnectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerDisconnected(BasePlayer player, DisconnectEventArgs e)
        {
            PlayerDisconnected?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerCleanup" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="DisconnectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerCleanup(BasePlayer player, DisconnectEventArgs e)
        {
            PlayerCleanup?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerSpawned" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerSpawned(BasePlayer player, SpawnEventArgs e)
        {
            PlayerSpawned?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerDied" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="DeathEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerDied(BasePlayer player, DeathEventArgs e)
        {
            PlayerDied?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleSpawned" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleSpawned(BaseVehicle vehicle, EventArgs e)
        {
            VehicleSpawned?.Invoke(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleDied" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleDied(BaseVehicle vehicle, PlayerEventArgs e)
        {
            VehicleDied?.Invoke(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerText" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="TextEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerText(BasePlayer player, TextEventArgs e)
        {
            PlayerText?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerCommandText" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="TextEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerCommandText(BasePlayer player, CommandTextEventArgs e)
        {
            PlayerCommandText?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerRequestClass" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="RequestClassEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerRequestClass(BasePlayer player, RequestClassEventArgs e)
        {
            PlayerRequestClass?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEnterVehicle" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EnterVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEnterVehicle(BasePlayer player, EnterVehicleEventArgs e)
        {
            PlayerEnterVehicle?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerExitVehicle" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerExitVehicle(BasePlayer player, PlayerVehicleEventArgs e)
        {
            PlayerExitVehicle?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerStateChanged" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="StateEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerStateChanged(BasePlayer player, StateEventArgs e)
        {
            PlayerStateChanged?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEnterCheckpoint" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEnterCheckpoint(BasePlayer player, EventArgs e)
        {
            PlayerEnterCheckpoint?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerLeaveCheckpoint" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerLeaveCheckpoint(BasePlayer player, EventArgs e)
        {
            PlayerLeaveCheckpoint?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEnterRaceCheckpoint" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEnterRaceCheckpoint(BasePlayer player, EventArgs e)
        {
            PlayerEnterRaceCheckpoint?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerLeaveRaceCheckpoint" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerLeaveRaceCheckpoint(BasePlayer player, EventArgs e)
        {
            PlayerLeaveRaceCheckpoint?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="RconCommand" /> event.
        /// </summary>
        /// <param name="e">An <see cref="RconEventArgs" /> that contains the event data. </param>
        protected virtual void OnRconCommand(RconEventArgs e)
        {
            RconCommand?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerRequestSpawn" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerRequestSpawn(BasePlayer player, RequestSpawnEventArgs e)
        {
            PlayerRequestSpawn?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="ObjectMoved" /> event.
        /// </summary>
        /// <param name="globalObject">The global-object triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnObjectMoved(GlobalObject globalObject, EventArgs e)
        {
            ObjectMoved?.Invoke(globalObject, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerObjectMoved" /> event.
        /// </summary>
        /// <param name="playerObject">The player-object triggering the event.</param>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerObjectMoved(PlayerObject playerObject, EventArgs e)
        {
            PlayerObjectMoved?.Invoke(playerObject, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerPickUpPickup" /> event.
        /// </summary>
        /// <param name="pickup">The pickup triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerPickUpPickup(Pickup pickup, PlayerEventArgs e)
        {
            PlayerPickUpPickup?.Invoke(pickup, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleMod" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="VehicleModEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleMod(BaseVehicle vehicle, VehicleModEventArgs e)
        {
            VehicleMod?.Invoke(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEnterExitModShop" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EnterModShopEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEnterExitModShop(BasePlayer player, EnterModShopEventArgs e)
        {
            PlayerEnterExitModShop?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehiclePaintjobApplied" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="VehiclePaintjobEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehiclePaintjobApplied(BaseVehicle vehicle, VehiclePaintjobEventArgs e)
        {
            VehiclePaintjobApplied?.Invoke(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleResprayed" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="VehicleResprayedEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleResprayed(BaseVehicle vehicle, VehicleResprayedEventArgs e)
        {
            VehicleResprayed?.Invoke(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleDamageStatusUpdated" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleDamageStatusUpdated(BaseVehicle vehicle, PlayerEventArgs e)
        {
            VehicleDamageStatusUpdated?.Invoke(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="UnoccupiedVehicleUpdated" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="UnoccupiedVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnUnoccupiedVehicleUpdated(BaseVehicle vehicle, UnoccupiedVehicleEventArgs e)
        {
            UnoccupiedVehicleUpdated?.Invoke(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerSelectedMenuRow" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="MenuRowEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerSelectedMenuRow(BasePlayer player, MenuRowEventArgs e)
        {
            PlayerSelectedMenuRow?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerExitedMenu" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerExitedMenu(BasePlayer player, EventArgs e)
        {
            PlayerExitedMenu?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerInteriorChanged" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="InteriorChangedEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerInteriorChanged(BasePlayer player, InteriorChangedEventArgs e)
        {
            PlayerInteriorChanged?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerKeyStateChanged" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="KeyStateChangedEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerKeyStateChanged(BasePlayer player, KeyStateChangedEventArgs e)
        {
            PlayerKeyStateChanged?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="RconLoginAttempt" /> event.
        /// </summary>
        /// <param name="e">An <see cref="RconLoginAttemptEventArgs" /> that contains the event data. </param>
        protected virtual void OnRconLoginAttempt(RconLoginAttemptEventArgs e)
        {
            RconLoginAttempt?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerUpdate" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerUpdate(BasePlayer player, PlayerUpdateEventArgs e)
        {
            PlayerUpdate?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerStreamIn" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerStreamIn(BasePlayer player, PlayerEventArgs e)
        {
            PlayerStreamIn?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerStreamOut" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerStreamOut(BasePlayer player, PlayerEventArgs e)
        {
            PlayerStreamOut?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleStreamIn" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleStreamIn(BaseVehicle vehicle, PlayerEventArgs e)
        {
            VehicleStreamIn?.Invoke(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleStreamOut" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle triggering the event.</param>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnVehicleStreamOut(BaseVehicle vehicle, PlayerEventArgs e)
        {
            VehicleStreamOut?.Invoke(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="TrailerUpdate" /> event.
        /// </summary>
        /// <param name="trailer">The trailer triggering the event.</param>
        /// <param name="e">An <see cref="PlayerVehicleEventArgs" /> that contains the event data. </param>
        protected virtual void OnTrailerUpdate(BaseVehicle trailer, TrailerEventArgs e)
        {
            TrailerUpdate?.Invoke(trailer, e);
        }

        /// <summary>
        ///     Raises the <see cref="DialogResponse" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="DialogResponseEventArgs" /> that contains the event data. </param>
        protected virtual void OnDialogResponse(BasePlayer player, DialogResponseEventArgs e)
        {
            DialogResponse?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerTakeDamage" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="DamageEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerTakeDamage(BasePlayer player, DamageEventArgs e)
        {
            PlayerTakeDamage?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerGiveDamage" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="DamageEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerGiveDamage(BasePlayer player, DamageEventArgs e)
        {
            PlayerGiveDamage?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerClickMap" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="PositionEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerClickMap(BasePlayer player, PositionEventArgs e)
        {
            PlayerClickMap?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerClickTextDraw" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="ClickTextDrawEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerClickTextDraw(BasePlayer player, ClickTextDrawEventArgs e)
        {
            PlayerClickTextDraw?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerClickPlayerTextDraw" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="ClickTextDrawEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerClickPlayerTextDraw(BasePlayer player, ClickPlayerTextDrawEventArgs e)
        {
            PlayerClickPlayerTextDraw?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerClickPlayer" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="ClickPlayerEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerClickPlayer(BasePlayer player, ClickPlayerEventArgs e)
        {
            PlayerClickPlayer?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEditGlobalObject" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EditGlobalObjectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEditGlobalObject(BasePlayer player, EditGlobalObjectEventArgs e)
        {
            PlayerEditGlobalObject?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEditPlayerObject" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EditPlayerObjectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEditPlayerObject(BasePlayer player, EditPlayerObjectEventArgs e)
        {
            PlayerEditPlayerObject?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerEditAttachedObject" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="EditAttachedObjectEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerEditAttachedObject(BasePlayer player, EditAttachedObjectEventArgs e)
        {
            PlayerEditAttachedObject?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerSelectGlobalObject" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="SelectGlobalObjectEventArgs" /> that contains the event data.</param>
        protected virtual void OnPlayerSelectGlobalObject(BasePlayer player, SelectGlobalObjectEventArgs e)
        {
            PlayerSelectGlobalObject?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerSelectPlayerObject" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="SelectPlayerObjectEventArgs" /> that contains the event data.</param>
        protected virtual void OnPlayerSelectPlayerObject(BasePlayer player, SelectPlayerObjectEventArgs e)
        {
            PlayerSelectPlayerObject?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerWeaponShot" /> event.
        /// </summary>
        /// <param name="player">The player triggering the event.</param>
        /// <param name="e">An <see cref="WeaponShotEventArgs" /> that contains the event data. </param>
        protected virtual void OnPlayerWeaponShot(BasePlayer player, WeaponShotEventArgs e)
        {
            PlayerWeaponShot?.Invoke(player, e);
        }

        /// <summary>
        ///     Raises the <see cref="IncomingConnection" /> event.
        /// </summary>
        /// <param name="e">An <see cref="ConnectionEventArgs" /> that contains the event dEventArgsata. </param>
        protected virtual void OnIncomingConnection(ConnectionEventArgs e)
        {
            IncomingConnection?.Invoke(this, e);
        }

        /// <summary>
        ///     Raises the <see cref="VehicleSirenStateChange" /> event.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <param name="e">The <see cref="SirenStateEventArgs" /> instance containing the event data.</param>
        protected void OnVehicleSirenStateChange(BaseVehicle vehicle, SirenStateEventArgs e)
        {
            VehicleSirenStateChange?.Invoke(vehicle, e);
        }

        /// <summary>
        ///     Raises the <see cref="ActorStreamIn" /> event.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="e">The <see cref="PlayerEventArgs" /> instance containing the event data.</param>
        protected void OnActorStreamIn(Actor actor, PlayerEventArgs e)
        {
            ActorStreamIn?.Invoke(actor, e);
        }

        /// <summary>
        ///     Raises the <see cref="ActorStreamOut" /> event.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="e">The <see cref="PlayerEventArgs" /> instance containing the event data.</param>
        protected void OnActorStreamOut(Actor actor, PlayerEventArgs e)
        {
            ActorStreamOut?.Invoke(actor, e);
        }

        /// <summary>
        ///     Raises the <see cref="PlayerGiveDamageActor" /> event.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="e">The <see cref="DamageEventArgs" /> instance containing the event data.</param>
        protected void OnPlayerGiveDamageActor(Actor actor, DamageEventArgs e)
        {
            PlayerGiveDamageActor?.Invoke(actor, e);
        }

        /// <summary>
        ///     Raises the <see cref="Tick" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected virtual void OnTick(EventArgs e)
        {
            Tick?.Invoke(this, e);
        }
    }
}