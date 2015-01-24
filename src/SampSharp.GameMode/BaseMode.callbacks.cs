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
using SampSharp.GameMode.Display;
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
        ///     This callback is triggered when a timer ticks.
        /// </summary>
        /// <param name="timerid">The ID of the ticking timer.</param>
        /// <param name="args">The args object as parsed with <see cref="Native.SetTimer" />.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnTimerTick(int timerid, object args)
        {
            /*
             * Pass straight trough to TimerTick. Set the args as sender.
             */
            if (TimerTick != null && args != null)
                TimerTick(args, EventArgs.Empty);

            return true;
        }

        /// <summary>
        ///     This callback is triggered when the game mode starts.
        /// </summary>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnGameModeInit()
        {
            OnInitialized(EventArgs.Empty);

            return true;
        }

        /// <summary>
        ///     This callback is called when a game mode ends.
        /// </summary>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnGameModeExit()
        {
            OnExited(EventArgs.Empty);

            return true;
        }

        /// <summary>
        ///     This callback is called when a player connects to the server.
        /// </summary>
        /// <param name="playerid">The ID of the player that connected.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerConnect(int playerid)
        {
            OnPlayerConnected(GtaPlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        /// <summary>
        ///     This callback is called when a player disconnects from the server.
        /// </summary>
        /// <param name="playerid">ID of the player that disconnected.</param>
        /// <param name="reason">The reason for the disconnection.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerDisconnect(int playerid, int reason)
        {
            var args = new DisconnectEventArgs((DisconnectReason) reason);

            OnPlayerDisconnected(GtaPlayer.FindOrCreate(playerid), args);
            OnPlayerCleanup(GtaPlayer.FindOrCreate(playerid), args);

            return true;
        }

        /// <summary>
        ///     This callback is called when a player spawns.
        /// </summary>
        /// <param name="playerid">The ID of the player that spawned.</param>
        /// <returns>Return False in this callback to force the player back to class selection when they next respawn.</returns>
        internal bool OnPlayerSpawn(int playerid)
        {
            var args = new SpawnEventArgs();

            OnPlayerSpawned(GtaPlayer.FindOrCreate(playerid), args);

            return !args.ReturnToClassSelection;
        }

        /// <summary>
        ///     This callback is called when a player dies.
        /// </summary>
        /// <param name="playerid">The ID of the player that died.</param>
        /// <param name="killerid">
        ///     The ID of the player that killed the player who died, or <see cref="Misc.InvalidPlayerId" /> if
        ///     there was none.
        /// </param>
        /// <param name="reason">The ID of the reason for the player's death.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerDeath(int playerid, int killerid, int reason)
        {
            OnPlayerDied(GtaPlayer.FindOrCreate(playerid),
                new DeathEventArgs(killerid == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(killerid),
                    (Weapon) reason));

            return true;
        }

        /// <summary>
        ///     This callback is called when a vehicle respawns.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle that spawned.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnVehicleSpawn(int vehicleid)
        {
            OnVehicleSpawned(GtaVehicle.FindOrCreate(vehicleid), EventArgs.Empty);

            return true;
        }

        /// <summary>
        ///     This callback is called when a vehicle is destroyed - either by exploding or becoming submerged in water.
        /// </summary>
        /// <remarks>
        ///     This callback will also be called when a vehicle enters water, but the vehicle can be saved from destruction by
        ///     teleportation or driving out (if only partially submerged). The callback won't be called a second time, and the
        ///     vehicle may disappear when the driver exits, or after a short time.
        /// </remarks>
        /// <param name="vehicleid">The ID of the vehicle that was destroyed.</param>
        /// <param name="killerid">
        ///     The ID of the player that reported (synced) the vehicle's destruction (name is misleading).
        ///     Generally the driver or a passenger (if any) or the closest player.
        /// </param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnVehicleDeath(int vehicleid, int killerid)
        {
            OnVehicleDied(GtaVehicle.FindOrCreate(vehicleid),
                new PlayerEventArgs(killerid == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(killerid)));

            return true;
        }

        /// <summary>
        ///     Called when a player sends a chat message.
        /// </summary>
        /// <param name="playerid">The ID of the player who typed the text.</param>
        /// <param name="text">The text the player typed.</param>
        /// <returns>Returning False in this callback will stop the text from being sent.</returns>
        internal bool OnPlayerText(int playerid, string text)
        {
            var args = new TextEventArgs(text);

            OnPlayerText(GtaPlayer.FindOrCreate(playerid), args);

            return !args.SendToPlayers;
        }

        /// <summary>
        ///     This callback is called when a player enters a command into the client chat window, e.g. /help.
        /// </summary>
        /// <param name="playerid">The ID of the player that executed the command.</param>
        /// <param name="cmdtext">The command that was executed (including the slash).</param>
        /// <returns>False if the command was not processed, otherwise True.</returns>
        internal bool OnPlayerCommandText(int playerid, string cmdtext)
        {
            var args = new CommandTextEventArgs(cmdtext);

            OnPlayerCommandText(GtaPlayer.FindOrCreate(playerid), args);

            return args.Success;
        }

        /// <summary>
        ///     Called when a player changes class at class selection (and when class selection first appears).
        /// </summary>
        /// <param name="playerid">The ID of the player that changed class.</param>
        /// <param name="classid">The ID of the current class being viewed.</param>
        /// <returns>
        ///     Returning False in this callback will prevent the player from spawning. The player can be forced to spawn when
        ///     <see cref="Native.SpawnPlayer" /> is used, however the player will re-enter class selection the next time they die.
        /// </returns>
        internal bool OnPlayerRequestClass(int playerid, int classid)
        {
            var args = new RequestClassEventArgs(classid);

            OnPlayerRequestClass(GtaPlayer.FindOrCreate(playerid), args);

            return !args.PreventSpawning;
        }

        /// <summary>
        ///     This callback is called when a player starts to enter a vehicle, meaning the player is not in vehicle yet at the
        ///     time this callback is called.
        /// </summary>
        /// <param name="playerid">ID of the player who attempts to enter a vehicle.</param>
        /// <param name="vehicleid">ID of the vehicle the player is attempting to enter.</param>
        /// <param name="ispassenger">False if entering as driver. True if entering as passenger.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerEnterVehicle(int playerid, int vehicleid, bool ispassenger)
        {
            var player = GtaPlayer.FindOrCreate(playerid);
            OnPlayerEnterVehicle(player,
                new EnterVehicleEventArgs(player, GtaVehicle.FindOrCreate(vehicleid), ispassenger));

            return true;
        }

        /// <summary>
        ///     This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        ///     Not called if the player falls off a bike or is removed from a vehicle by other means such as using
        ///     <see cref="Native.SetPlayerPos(int,Vector)" />.
        /// </remarks>
        /// <param name="playerid">The ID of the player who exited the vehicle.</param>
        /// <param name="vehicleid">The ID of the vehicle the player is exiting.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerExitVehicle(int playerid, int vehicleid)
        {
            var player = GtaPlayer.FindOrCreate(playerid);
            OnPlayerExitVehicle(player,
                new PlayerVehicleEventArgs(player, GtaVehicle.FindOrCreate(vehicleid)));

            return true;
        }

        /// <summary>
        ///     This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        ///     Not called if the player falls off a bike or is removed from a vehicle by other means such as using
        ///     <see cref="Native.SetPlayerPos(int,Vector)" />.
        /// </remarks>
        /// <param name="playerid">The ID of the player that changed state.</param>
        /// <param name="newstate">The player's new state.</param>
        /// <param name="oldstate">The player's previous state.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerStateChange(int playerid, int newstate, int oldstate)
        {
            OnPlayerStateChanged(GtaPlayer.FindOrCreate(playerid), new StateEventArgs((PlayerState) newstate, (PlayerState) oldstate));

            return true;
        }

        /// <summary>
        ///     This callback is called when a player enters the checkpoint set for that player.
        /// </summary>
        /// <param name="playerid">The player who entered the checkpoint.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerEnterCheckpoint(int playerid)
        {
            OnPlayerEnterCheckpoint(GtaPlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        /// <summary>
        ///     This callback is called when a player leaves the checkpoint set for that player.
        /// </summary>
        /// <param name="playerid">The player who left the checkpoint.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerLeaveCheckpoint(int playerid)
        {
            OnPlayerLeaveCheckpoint(GtaPlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        /// <summary>
        ///     This callback is called when a player enters a race checkpoint.
        /// </summary>
        /// <param name="playerid">The ID of the player who entered the race checkpoint.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerEnterRaceCheckpoint(int playerid)
        {
            OnPlayerEnterRaceCheckpoint(GtaPlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        /// <summary>
        ///     This callback is called when a player leaves the race checkpoint.
        /// </summary>
        /// <param name="playerid">The player who left the race checkpoint.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerLeaveRaceCheckpoint(int playerid)
        {
            OnPlayerLeaveRaceCheckpoint(GtaPlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        /// <summary>
        ///     This callback is called when a command is sent through the server console, remote RCON, or via the in-game /rcon
        ///     command.
        /// </summary>
        /// <param name="command">A string containing the command that was typed, as well as any passed parameters.</param>
        /// <returns>
        ///     False if the command was not processed, it will be passed to another script or True if the command was
        ///     processed, will not be passed to other scripts.
        /// </returns>
        internal bool OnRconCommand(string command)
        {
            var args = new RconEventArgs(command);
            OnRconCommand(args);

            return args.Success;
        }

        /// <summary>
        ///     Called when a player attempts to spawn via class selection.
        /// </summary>
        /// <param name="playerid">The ID of the player who requested to spawn.</param>
        /// <returns>Returning False in this callback will prevent the player from spawning.</returns>
        internal bool OnPlayerRequestSpawn(int playerid)
        {
            var args = new RequestSpawnEventArgs();

            OnPlayerRequestSpawn(GtaPlayer.FindOrCreate(playerid), args);

            return !args.PreventSpawning;
        }

        /// <summary>
        ///     This callback is called when an object is moved after <see cref="Native.MoveObject(int,Vector,float,Vector)" />
        ///     (when it stops moving).
        /// </summary>
        /// <remarks>
        ///     SetObjectPos does not work when used in this callback. To fix it, delete and re-create the object, or use a timer.
        /// </remarks>
        /// <param name="objectid">The ID of the object that was moved.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnObjectMoved(int objectid)
        {
            OnObjectMoved(GlobalObject.FindOrCreate(objectid), EventArgs.Empty);

            return true;
        }

        /// <summary>
        ///     This callback is called when a player object is moved after
        ///     <see cref="Native.MovePlayerObject(int,int,Vector,float,Vector)" /> (when it stops
        ///     moving).
        /// </summary>
        /// <param name="playerid">The playerid the object is assigned to.</param>
        /// <param name="objectid">The ID of the player-object that was moved.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerObjectMoved(int playerid, int objectid)
        {
            OnPlayerObjectMoved(PlayerObject.FindOrCreate(GtaPlayer.FindOrCreate(playerid), objectid), EventArgs.Empty);

            return true;
        }

        /// <summary>
        ///     Called when a player picks up a pickup created with <see cref="Native.CreatePickup" />.
        /// </summary>
        /// <param name="playerid">The ID of the player that picked up the pickup.</param>
        /// <param name="pickupid">The ID of the pickup, returned by <see cref="Native.CreatePickup" />.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerPickUpPickup(int playerid, int pickupid)
        {
            OnPlayerPickUpPickup(Pickup.FindOrCreate(pickupid), new PlayerEventArgs(GtaPlayer.FindOrCreate(playerid)));

            return true;
        }

        /// <summary>
        ///     This callback is called when a vehicle is modded.
        /// </summary>
        /// <remarks>
        ///     This callback is not called by <see cref="Native.AddVehicleComponent" />.
        /// </remarks>
        /// <param name="playerid">The ID of the driver of the vehicle.</param>
        /// <param name="vehicleid">The ID of the vehicle which is modded.</param>
        /// <param name="componentid">The ID of the component which was added to the vehicle.</param>
        /// <returns>Return False to desync the mod (or an invalid mod) from propagating and / or crashing players.</returns>
        internal bool OnVehicleMod(int playerid, int vehicleid, int componentid)
        {
            var args = new VehicleModEventArgs(GtaPlayer.FindOrCreate(playerid), componentid);

            OnVehicleMod(GtaVehicle.FindOrCreate(vehicleid),args);

            return !args.PreventPropagation;
        }

        /// <summary>
        ///     This callback is called when a player enters or exits a mod shop.
        /// </summary>
        /// <param name="playerid">The ID of the player that entered or exited the mod shop.</param>
        /// <param name="enterexit">1 if the player entered or 0 if they exited.</param>
        /// <param name="interiorid">The interior ID of the mod shop that the player is entering (or 0 if exiting).</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnEnterExitModShop(int playerid, int enterexit, int interiorid)
        {
            OnPlayerEnterExitModShop(GtaPlayer.FindOrCreate(playerid),
                new EnterModShopEventArgs((EnterExit) enterexit, interiorid));

            return true;
        }

        /// <summary>
        ///     Called when a player changes the paintjob of their vehicle (in a mod shop).
        /// </summary>
        /// <param name="playerid">The ID of the player whose vehicle was modded.</param>
        /// <param name="vehicleid">The ID of the vehicle that changed paintjob.</param>
        /// <param name="paintjobid">The ID of the new paintjob.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnVehiclePaintjob(int playerid, int vehicleid, int paintjobid)
        {
            OnVehiclePaintjobApplied(GtaVehicle.FindOrCreate(vehicleid),
                new VehiclePaintjobEventArgs(GtaPlayer.FindOrCreate(playerid), paintjobid));
            

            return true;
        }

        /// <summary>
        ///     The callback name is deceptive, this callback is called when a player exits a mod shop, regardless of whether the
        ///     vehicle's colors were changed, and is NEVER called for pay 'n' spray garages.
        /// </summary>
        /// <remarks>
        ///     Misleadingly, this callback is not called for pay 'n' spray (only mod shops).
        /// </remarks>
        /// <param name="playerid">The ID of the player that is driving the vehicle.</param>
        /// <param name="vehicleid">The ID of the vehicle that was resprayed.</param>
        /// <param name="color1">The color that the vehicle's primary color was changed to.</param>
        /// <param name="color2">The color that the vehicle's secondary color was changed to.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnVehicleRespray(int playerid, int vehicleid, int color1, int color2)
        {
            OnVehicleResprayed(GtaVehicle.FindOrCreate(vehicleid),
                new VehicleResprayedEventArgs(GtaPlayer.FindOrCreate(playerid), color1, color2));

            return true;
        }

        /// <summary>
        ///     This callback is called when a vehicle element such as doors, tires, panels, or lights get damaged.
        /// </summary>
        /// <remarks>
        ///     This does not include vehicle health changes.
        /// </remarks>
        /// <param name="vehicleid">The ID of the vehicle that was damaged.</param>
        /// <param name="playerid">The ID of the player who synced the damage (who had the car damaged).</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnVehicleDamageStatusUpdate(int vehicleid, int playerid)
        {
            OnVehicleDamageStatusUpdated(GtaVehicle.FindOrCreate(vehicleid),
                new PlayerEventArgs(GtaPlayer.FindOrCreate(playerid)));

            return true;
        }

        /// <summary>
        ///     This callback is called every time an unoccupied vehicle updates the server with their status.
        /// </summary>
        /// <remarks>
        ///     This callback is called very frequently per second per unoccupied vehicle. You should refrain from implementing
        ///     intensive calculations or intensive file writing/reading operations in this callback.
        /// </remarks>
        /// <param name="vehicleid">The vehicleid that the callback is processing.</param>
        /// <param name="playerid">The playerid that the callback is processing (the playerid affecting the vehicle).</param>
        /// <param name="passengerSeat">The passenger seat of the playerid moving the vehicle. 0 if they're not in the vehicle.</param>
        /// <param name="newX">The new X coordinate of the vehicle.</param>
        /// <param name="newY">The new y coordinate of the vehicle.</param>
        /// <param name="newZ">The new z coordinate of the vehicle.</param>
        /// <param name="velX">The new X velocity of the vehicle. <b>This parameter was added in 0.3z R4</b></param>
        /// <param name="velY">The new Y velocity of the vehicle. <b>This parameter was added in 0.3z R4</b></param>
        /// <param name="velZ">The new Z velocity of the vehicle. <b>This parameter was added in 0.3z R4</b></param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnUnoccupiedVehicleUpdate(int vehicleid, int playerid, int passengerSeat, float newX,
            float newY, float newZ, float velX, float velY, float velZ)
        {
            var args = new UnoccupiedVehicleEventArgs(GtaPlayer.FindOrCreate(playerid), passengerSeat,
                new Vector(newX, newY, newZ), new Vector(velX, velY, velZ));
            OnUnoccupiedVehicleUpdated(GtaVehicle.FindOrCreate(vehicleid), args);

            return !args.PreventPropagation;
        }

        /// <summary>
        ///     This callback is called when a player selects an item from a menu.
        /// </summary>
        /// <param name="playerid">The ID of the player that selected an item on the menu.</param>
        /// <param name="row">The row that was selected.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerSelectedMenuRow(int playerid, int row)
        {
            OnPlayerSelectedMenuRow(GtaPlayer.FindOrCreate(playerid), new MenuRowEventArgs(row));

            return true;
        }

        /// <summary>
        ///     Called when a player exits a menu.
        /// </summary>
        /// <param name="playerid">The ID of the player that exited the menu.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerExitedMenu(int playerid)
        {
            OnPlayerExitedMenu(GtaPlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        /// <summary>
        ///     Called when a player changes interior.
        /// </summary>
        /// <remarks>
        ///     This is also called when <see cref="Native.SetPlayerInterior" /> is used.
        /// </remarks>
        /// <param name="playerid">The playerid who changed interior.</param>
        /// <param name="newinteriorid">The interior the player is now in.</param>
        /// <param name="oldinteriorid">The interior the player was in.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerInteriorChange(int playerid, int newinteriorid, int oldinteriorid)
        {
            OnPlayerInteriorChanged(GtaPlayer.FindOrCreate(playerid), new InteriorChangedEventArgs(newinteriorid, oldinteriorid));

            return true;
        }

        /// <summary>
        ///     This callback is called when the state of any supported key is changed (pressed/released). Directional keys do not
        ///     trigger this callback.
        /// </summary>
        /// <param name="playerid">ID of the player who pressed/released a key.</param>
        /// <param name="newkeys">A map of the keys currently held.</param>
        /// <param name="oldkeys">A map of the keys held prior to the current change.</param>
        /// <returns>
        ///     True - Allows this callback to be called in other scripts. False - Callback will not be called in other
        ///     scripts. It is always called first in game modes so returning False there blocks filter scripts from seeing it.
        /// </returns>
        internal bool OnPlayerKeyStateChange(int playerid, int newkeys, int oldkeys)
        {
            OnPlayerKeyStateChanged(GtaPlayer.FindOrCreate(playerid), new KeyStateChangedEventArgs((Keys) newkeys, (Keys) oldkeys));

            return true;
        }

        /// <summary>
        ///     This callback is called when someone tries to login to RCON, successful or not.
        /// </summary>
        /// <remarks>
        ///     This callback is only called when /rcon login is used.
        /// </remarks>
        /// <param name="ip">The IP of the player that tried to login to RCON.</param>
        /// <param name="password">The password used to login with.</param>
        /// <param name="success">False if the password was incorrect or True if it was correct.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnRconLoginAttempt(string ip, string password, bool success)
        {
            OnRconLoginAttempt(new RconLoginAttemptEventArgs(ip, password, success));

            return true;
        }

        /// <summary>
        ///     This callback is called every time a client/player updates the server with their status.
        /// </summary>
        /// <remarks>
        ///     This callback is called very frequently per second per player, only use it when you know what it's meant for.
        /// </remarks>
        /// <param name="playerid">ID of the player sending an update packet.</param>
        /// <returns>False - Update from this player will not be replicated to other clients.</returns>
        internal bool OnPlayerUpdate(int playerid)
        {
            var args = new PlayerUpdateEventArgs();

            OnPlayerUpdate(GtaPlayer.FindOrCreate(playerid),  args);

            return !args.PreventPropagation;
        }

        /// <summary>
        ///     This callback is called when a player is streamed by some other player's client.
        /// </summary>
        /// <param name="playerid">The ID of the player who has been streamed.</param>
        /// <param name="forplayerid">The ID of the player that streamed the other player in.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerStreamIn(int playerid, int forplayerid)
        {
            OnPlayerStreamIn(GtaPlayer.FindOrCreate(playerid), new PlayerEventArgs(GtaPlayer.FindOrCreate(forplayerid)));

            return true;
        }

        /// <summary>
        ///     This callback is called when a player is streamed out from some other player's client.
        /// </summary>
        /// <param name="playerid">The player who has been streamed out.</param>
        /// <param name="forplayerid">The player who has streamed out the other player.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerStreamOut(int playerid, int forplayerid)
        {
            OnPlayerStreamOut(GtaPlayer.FindOrCreate(playerid), new PlayerEventArgs(GtaPlayer.FindOrCreate(forplayerid)));

            return true;
        }

        /// <summary>
        ///     Called when a vehicle is streamed to a player's client.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle that streamed in for the player.</param>
        /// <param name="forplayerid">The ID of the player who the vehicle streamed in for.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnVehicleStreamIn(int vehicleid, int forplayerid)
        {
            OnVehicleStreamIn(GtaVehicle.FindOrCreate(vehicleid), new PlayerEventArgs(GtaPlayer.FindOrCreate(forplayerid)));

            return true;
        }

        /// <summary>
        ///     This callback is called when a vehicle is streamed out from some player's client.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle that streamed out.</param>
        /// <param name="forplayerid">The ID of the player who is no longer streaming the vehicle.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnVehicleStreamOut(int vehicleid, int forplayerid)
        {
            OnVehicleStreamOut(GtaVehicle.FindOrCreate(vehicleid), new PlayerEventArgs(GtaPlayer.FindOrCreate(forplayerid)));

            return true;
        }

        /// <summary>
        ///     This callback is called when a player sent a trailer update.
        /// </summary>
        /// <param name="playerId">The ID of the player who sent a trailer update</param>
        /// <param name="vehicleId">The Trailer being updated</param>
        /// <returns>
        ///     Return false if the update from this player and vehicle will not be replicated to other clients
        ///     or return true Indicates that this update can be processed normally and sent to other players.
        /// </returns>
        /// <remarks>
        ///     The trailer's position will still be updated internally on the server
        /// </remarks>
        internal bool OnTrailerUpdate(int playerId, int vehicleId)
        {
            var args = new TrailerEventArgs(GtaPlayer.FindOrCreate(playerId));
            OnTrailerUpdate(GtaVehicle.FindOrCreate(vehicleId), args);

            return !args.PreventPropagation;
        }

        /// <summary>
        ///     This callback is called when a player responds to a dialog shown using <see cref="Native.ShowPlayerDialog" /> by
        ///     either clicking a button, pressing ENTER/ESC or double-clicking a list item (if using a list style dialog).
        /// </summary>
        /// <param name="playerid">The ID of the player that responded to the dialog.</param>
        /// <param name="dialogid">
        ///     The ID of the dialog the player responded to, assigned in <see cref="Native.ShowPlayerDialog" />
        ///     .
        /// </param>
        /// <param name="response">1 for left button and 0 for right button (if only one button shown, always 1).</param>
        /// <param name="listitem">
        ///     The ID of the list item selected by the player (starts at 0) (only if using a list style
        ///     dialog).
        /// </param>
        /// <param name="inputtext">The text entered into the input box by the player or the selected list item text.</param>
        /// <returns>
        ///     Returning False in this callback will pass the dialog to another script in case no matching code were found in
        ///     your gamemode's callback.
        /// </returns>
        internal bool OnDialogResponse(int playerid, int dialogid, int response, int listitem, string inputtext)
        {
            OnDialogResponse(GtaPlayer.FindOrCreate(playerid), new DialogResponseEventArgs(GtaPlayer.FindOrCreate(playerid), dialogid, response, listitem, inputtext));

            return true;
        }

        /// <summary>
        ///     This callback is called when a player takes damage.
        /// </summary>
        /// <param name="playerid">The ID of the player that took damage.</param>
        /// <param name="issuerid">The ID of the player that caused the damage. INVALID_PLAYER_ID if self-inflicted.</param>
        /// <param name="amount">The amount of damage the player took (health and armor combined).</param>
        /// <param name="weaponid">The ID of the weapon/reason for the damage.</param>
        /// <param name="bodypart">The body part that was hit.</param>
        /// <returns>
        ///     True: Allows this callback to be called in other scripts. False Callback will not be called in other scripts.
        ///     It is always called first in game modes so returning False there blocks filter scripts from seeing it.
        /// </returns>
        internal bool OnPlayerTakeDamage(int playerid, int issuerid, float amount, int weaponid, int bodypart)
        {
            OnPlayerTakeDamage(GtaPlayer.FindOrCreate(playerid),
                new DamagePlayerEventArgs(issuerid == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(issuerid),
                    amount, (Weapon) weaponid, (BodyPart) bodypart));

            return true;
        }

        /// <summary>
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
        ///     most cases it's better to trust the client who is being damaged to report their health/armor (TakeDamage). SA-MP
        ///     normally does this. GiveDamage provides some extra information which may be useful when you require a different
        ///     level of trust.
        /// </remarks>
        /// <param name="playerid">The ID of the player that gave damage.</param>
        /// <param name="damagedid">The ID of the player that received damage.</param>
        /// <param name="amount">The amount of health/armor damagedid has lost (combined).</param>
        /// <param name="weaponid">The reason that caused the damage.</param>
        /// <param name="bodypart">The body part that was hit.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerGiveDamage(int playerid, int damagedid, float amount, int weaponid, int bodypart)
        {
            OnPlayerGiveDamage(GtaPlayer.FindOrCreate(playerid),
                new DamagePlayerEventArgs(damagedid == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(damagedid),
                    amount, (Weapon) weaponid, (BodyPart) bodypart));

            return true;
        }

        /// <summary>
        ///     This callback is called when a player places a target/waypoint on the pause menu map (by right-clicking).
        /// </summary>
        /// <remarks>
        ///     The Z value provided is only an estimate; you may find it useful to use a plugin like the MapAndreas plugin to get
        ///     a more accurate Z coordinate (or for teleportation; use <see cref="Native.SetPlayerPosFindZ(int,Vector)" />).
        /// </remarks>
        /// <param name="playerid">The ID of the player that placed a target/waypoint.</param>
        /// <param name="fX">The X float coordinate where the player clicked.</param>
        /// <param name="fY">The Y float coordinate where the player clicked.</param>
        /// <param name="fZ">The Z float coordinate where the player clicked (inaccurate - see note below).</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerClickMap(int playerid, float fX, float fY, float fZ)
        {
            OnPlayerClickMap(GtaPlayer.FindOrCreate(playerid), new PositionEventArgs(new Vector(fX, fY, fZ)));

            return true;
        }

        /// <summary>
        ///     This callback is called when a player clicks on a text draw or cancels the select mode(ESC).
        /// </summary>
        /// <remarks>
        ///     The clickable area is defined by <see cref="Native.TextDrawTextSize" />. The x and y parameters passed to that
        ///     function must not be zero or negative.
        /// </remarks>
        /// <param name="playerid">The ID of the player that clicked on the textdraw.</param>
        /// <param name="clickedid">The ID of the clicked textdraw. INVALID_TEXT_DRAW if selection was cancelled.</param>
        /// <returns>
        ///     Returning True in this callback will prevent it being called in other scripts. This should be used to signal
        ///     that the textdraw on which they clicked was 'found' and no further processing is needed. You should return False if
        ///     the textdraw on which they clicked wasn't found.
        /// </returns>
        internal bool OnPlayerClickTextDraw(int playerid, int clickedid)
        {
            var player = GtaPlayer.FindOrCreate(playerid);
            OnPlayerClickTextDraw(player,
                new ClickTextDrawEventArgs(player, clickedid == TextDraw.InvalidId ? null : TextDraw.FindOrCreate(clickedid)));

            return true;
        }

        /// <summary>
        ///     This callback is called when a player clicks on a player-text draw. It is not called when player cancels the select
        ///     mode (ESC) - however, <see cref="OnPlayerClickTextDraw(GtaPlayer,ClickTextDrawEventArgs)" /> is.
        /// </summary>
        /// <param name="playerid">The ID of the player that selected a textdraw.</param>
        /// <param name="playertextid">The ID of the player-textdraw that the player selected.</param>
        /// <returns>
        ///     Returning True in this callback will prevent it being called in other scripts. This should be used to signal
        ///     that the textdraw on which they clicked was 'found' and no further processing is needed. You should return False if
        ///     the textdraw on which they clicked wasn't found.
        /// </returns>
        internal bool OnPlayerClickPlayerTextDraw(int playerid, int playertextid)
        {
            var player = GtaPlayer.FindOrCreate(playerid);
            OnPlayerClickPlayerTextDraw(player,
                new ClickPlayerTextDrawEventArgs(player, playertextid == PlayerTextDraw.InvalidId
                    ? null
                    : PlayerTextDraw.FindOrCreate(player, playertextid)));

            return true;
        }

        /// <summary>
        ///     Called when a player double-clicks on a player on the scoreboard.
        /// </summary>
        /// <param name="playerid">The ID of the player that clicked on a player on the scoreboard.</param>
        /// <param name="clickedplayerid">The ID of the player that was clicked on.</param>
        /// <param name="source">The source of the player's click.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerClickPlayer(int playerid, int clickedplayerid, int source)
        {
            OnPlayerClickPlayer(GtaPlayer.FindOrCreate(playerid),
                new ClickPlayerEventArgs(
                    clickedplayerid == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(clickedplayerid),
                    (PlayerClickSource) source));

            return true;
        }

        /// <summary>
        ///     This callback is called when a player ends object edition mode.
        /// </summary>
        /// <param name="playerid">The ID of the player that edited an object.</param>
        /// <param name="playerobject">0 if it is a global object or 1 if it is a playerobject.</param>
        /// <param name="objectid">The ID of the edited object.</param>
        /// <param name="response">The type of response.</param>
        /// <param name="fX">The X offset for the object that was edited.</param>
        /// <param name="fY">The Y offset for the object that was edited.</param>
        /// <param name="fZ">The Z offset for the object that was edited.</param>
        /// <param name="fRotX">The X rotation for the object that was edited.</param>
        /// <param name="fRotY">The Y rotation for the object that was edited.</param>
        /// <param name="fRotZ">The Z rotation for the object that was edited.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerEditObject(int playerid, bool playerobject, int objectid, int response, float fX, float fY,
            float fZ, float fRotX, float fRotY, float fRotZ)
        {
            var player = GtaPlayer.FindOrCreate(playerid);
            if (playerobject)
            {
                OnPlayerEditPlayerObject(player,
                    new EditPlayerObjectEventArgs(player, PlayerObject.FindOrCreate(player, objectid),
                        (EditObjectResponse) response, new Vector(fX, fY, fZ), new Vector(fRotX, fRotY, fRotZ)));
            }
            else
            {
                OnPlayerEditGlobalObject(player,
                    new EditGlobalObjectEventArgs(player, GlobalObject.FindOrCreate(objectid), (EditObjectResponse) response,
                        new Vector(fX, fY, fZ), new Vector(fRotX, fRotY, fRotZ)));
            }

            return true;
        }

        /// <summary>
        ///     This callback is called when a player ends attached object edition mode.
        /// </summary>
        /// <remarks>
        ///     Editions should be discarded if response was '0' (canceled). This must be done by storing the offsets etc. in an
        ///     array BEFORE using EditAttachedObject.
        /// </remarks>
        /// <param name="playerid">The ID of the player that ended edition mode.</param>
        /// <param name="response">0 if they canceled (ESC) or 1 if they clicked the save icon.</param>
        /// <param name="index">Slot ID of the attached object that was edited.</param>
        /// <param name="modelid">The model of the attached object that was edited.</param>
        /// <param name="boneid">The bone of the attached object that was edited.</param>
        /// <param name="fOffsetX">The X offset for the attached object that was edited.</param>
        /// <param name="fOffsetY">The Y offset for the attached object that was edited.</param>
        /// <param name="fOffsetZ">The Z offset for the attached object that was edited.</param>
        /// <param name="fRotX">The X rotation for the attached object that was edited.</param>
        /// <param name="fRotY">The Y rotation for the attached object that was edited.</param>
        /// <param name="fRotZ">The Z rotation for the attached object that was edited.</param>
        /// <param name="fScaleX">The X scale for the attached object that was edited.</param>
        /// <param name="fScaleY">The Y scale for the attached object that was edited.</param>
        /// <param name="fScaleZ">The Z scale for the attached object that was edited.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerEditAttachedObject(int playerid, int response, int index, int modelid, int boneid,
            float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX,
            float fScaleY, float fScaleZ)
        {
            OnPlayerEditAttachedObject(GtaPlayer.FindOrCreate(playerid),
                new EditAttachedObjectEventArgs((EditObjectResponse) response, index, modelid, boneid,
                    new Vector(fOffsetX, fOffsetY, fOffsetZ), new Vector(fRotX, fRotY, fRotZ),
                    new Vector(fScaleX, fScaleY, fScaleZ)));

            return true;
        }

        /// <summary>
        ///     This callback is called when a player selects an object after <see cref="Native.SelectObject" /> has been used.
        /// </summary>
        /// <param name="playerid">The ID of the player that selected an object.</param>
        /// <param name="type">The type of selection.</param>
        /// <param name="objectid">The ID of the selected object.</param>
        /// <param name="modelid">The model of the selected object.</param>
        /// <param name="fX">The X position of the selected object.</param>
        /// <param name="fY">The Y position of the selected object.</param>
        /// <param name="fZ">The Z position of the selected object.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnPlayerSelectObject(int playerid, int type, int objectid, int modelid, float fX, float fY,
            float fZ)
        {
            switch ((ObjectType) type)
            {
                case ObjectType.GlobalObject:
                    OnPlayerSelectGlobalObject(GtaPlayer.FindOrCreate(playerid),
                        new SelectGlobalObjectEventArgs(GtaPlayer.FindOrCreate(playerid), GlobalObject.FindOrCreate(objectid), modelid,
                            new Vector(fX, fY, fZ)));
                    break;
                case ObjectType.PlayerObject:
                    var player = GtaPlayer.FindOrCreate(playerid);

                    OnPlayerSelectPlayerObject(player, 
                        new SelectPlayerObjectEventArgs(GtaPlayer.FindOrCreate(playerid), PlayerObject.FindOrCreate(player, objectid), modelid,
                            new Vector(fX, fY, fZ)));
                    break;
            }
            //OnPlayerSelectObject(GtaPlayer.FindOrCreate(playerid), new SelectGlobalObjectEventArgs((ObjectType) type, objectid, modelid,
            //    new Vector(fX, fY, fZ)));

            return true;
        }

        /// <summary>
        ///     This callback is called when a player fires a shot from a weapon.
        /// </summary>
        /// <remarks>
        ///     BULLET_HIT_TYPE_NONE: the fX, fY and fZ parameters are normal coordinates;
        ///     Others: the fX, fY and fZ are offsets from the center of hitid.
        /// </remarks>
        /// <param name="playerid">The ID of the player that shot a weapon.</param>
        /// <param name="weaponid">The ID of the weapon shot by the player.</param>
        /// <param name="hittype">The type of thing the shot hit (none, player, vehicle, or (player)object).</param>
        /// <param name="hitid">The ID of the player, vehicle or object that was hit.</param>
        /// <param name="fX">The X coordinate that the shot hit.</param>
        /// <param name="fY">The Y coordinate that the shot hit.</param>
        /// <param name="fZ">The Z coordinate that the shot hit.</param>
        /// <returns> False: Prevent the bullet from causing damage. True: Allow the bullet to cause damage.</returns>
        internal bool OnPlayerWeaponShot(int playerid, int weaponid, int hittype, int hitid, float fX, float fY,
            float fZ)
        {
            var args = new WeaponShotEventArgs((Weapon) weaponid, (BulletHitType) hittype, hitid, new Vector(fX, fY, fZ));

            OnPlayerWeaponShot(GtaPlayer.FindOrCreate(playerid), args);

            return !args.PreventDamage;
        }

        /// <summary>
        ///     This callback is called when an IP address attempts a connection to the server.
        /// </summary>
        /// <param name="playerid">The ID of the player attempting to connect.</param>
        /// <param name="ipAddress">The IP address of the player attempting to connect.</param>
        /// <param name="port">The port of the attempted connection.</param>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnIncomingConnection(int playerid, string ipAddress, int port)
        {
            OnIncomingConnection(new ConnectionEventArgs(playerid, ipAddress, port));

            return true;
        }

        /// <summary>
        ///     This callback is called every tick.
        /// </summary>
        /// <returns>This callback does not handle returns.</returns>
        internal bool OnTick()
        {
            OnTick(EventArgs.Empty);

            return true;
        }
    }
}