using System;
using GameMode.Definitions;
using GameMode.Events;
using GameMode.World;

namespace GameMode
{
    /// <summary>
    /// Represents a SA:MP gamemode.
    /// </summary>
    public abstract class BaseMode
    {
        /// <summary>
        /// Initalizes a new instance of the BaseMode class.
        /// </summary>
        protected BaseMode()
        {
            Player.RegisterEvents(this, Player.Find);
        }

        #region Events

        /// <summary>
        /// Occurs when the <see cref="OnGameModeInit"/> is being called.
        /// This callback is triggered when the gamemode starts.
        /// </summary>
        public event GameModeHandler Initialized;

        /// <summary>
        /// Occurs when the <see cref="OnGameModeExit"/> is being called.
        /// This callback is called when a gamemode ends.
        /// </summary>
        public event GameModeHandler Exited;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerConnect"/> is being called.
        /// This callback is called when a player connects to the server.
        /// </summary>
        public event PlayerHandler PlayerConnected;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerDisconnect"/> is being called.
        /// This callback is called when a player disconnects from the server.
        /// </summary>
        public event PlayerDisconnectedHandler PlayerDisconnected;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerSpawn"/> is being called.
        /// This callback is called when a player spawns.
        /// </summary>
        public event PlayerHandler PlayerSpawned;

        /// <summary>
        /// Occurs when the <see cref="OnGameModeInit"/> is being called.
        /// This callback is triggered when the gamemode starts.
        /// </summary>
        public event PlayerDeathHandler PlayerDied;

        /// <summary>
        /// Occurs when the <see cref="OnVehicleSpawn"/> is being called.
        /// This callback is called when a vehicle respawns.
        /// </summary>
        public event VehicleSpawnedHandler VehicleSpawned;

        /// <summary>
        /// Occurs when the <see cref="OnVehicleDeath"/> is being called.
        /// This callback is called when a vehicle is destroyed - either by exploding or becoming submerged in water.
        /// </summary>
        /// <remarks>
        /// This callback will also be called when a vehicle enters water, but the vehicle can be saved from destruction by teleportation or driving out (if only partially submerged). The callback won't be called a second time, and the vehicle may disappear when the driver exits, or after a short time.
        /// </remarks>
        public event PlayerVehicleHandler VehicleDied;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerText"/> is being called.
        /// Called when a player sends a chat message.
        /// </summary>
        public event PlayerTextHandler PlayerText;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerCommandText"/> is being called.
        /// This callback is called when a player enters a command into the client chat window, e.g. /help.
        /// </summary>
        public event PlayerTextHandler PlayerCommandText;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerRequestClass"/> is being called.
        /// Called when a player changes class at class selection (and when class selection first appears).
        /// </summary>
        public event PlayerRequestClassHandler PlayerRequestClass;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerEnterVehicle"/> is being called.
        /// This callback is called when a player starts to enter a vehicle, meaning the player is not in vehicle yet at the time this callback is called.
        /// </summary>
        public event PlayerEnterVehicleHandler PlayerEnterVehicle;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerExitVehicle"/> is being called.
        /// This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        /// Not called if the player falls off a bike or is removed from a vehicle by other means such as using <see cref="Native.SetPlayerPos(int,Vector)"/>.
        /// </remarks>
        public event PlayerVehicleHandler PlayerExitVehicle;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerStateChange"/> is being called.
        /// This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        /// Not called if the player falls off a bike or is removed from a vehicle by other means such as using <see cref="Native.SetPlayerPos(int,Vector)"/>.
        /// </remarks>
        public event PlayerStateHandler PlayerStateChanged;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerEnterCheckpoint"/> is being called.
        /// This callback is called when a player enters the checkpoint set for that player.
        /// </summary>
        public event PlayerHandler PlayerEnterCheckpoint;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerLeaveCheckpoint"/> is being called.
        /// This callback is called when a player leaves the checkpoint set for that player.
        /// </summary>
        public event PlayerHandler PlayerLeaveCheckpoint;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerEnterRaceCheckpoint"/> is being called.
        /// This callback is called when a player enters a race checkpoint.
        /// </summary>
        public event PlayerHandler PlayerEnterRaceCheckpoint;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerLeaveRaceCheckpoint"/> is being called.
        /// This callback is called when a player leaves the race checkpoint.
        /// </summary>
        public event PlayerHandler PlayerLeaveRaceCheckpoint;

        /// <summary>
        /// Occurs when the <see cref="OnRconCommand"/> is being called.
        /// This callback is called when a command is sent through the server console, remote RCON, or via the in-game /rcon command.
        /// </summary>
        public event RconHandler RconCommand;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerRequestSpawn"/> is being called.
        /// Called when a player attempts to spawn via class selection.
        /// </summary>
        public event PlayerHandler PlayerRequestSpawn;

        /// <summary>
        /// Occurs when the <see cref="OnObjectMoved"/> is being called.
        /// This callback is called when an object is moved after <see cref="Native.MoveObject"/> (when it stops moving).
        /// </summary>
        public event ObjectHandler ObjectMoved;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerObjectMoved"/> is being called.
        /// This callback is called when a player object is moved after <see cref="Native.MovePlayerObject"/> (when it stops moving).
        /// </summary>
        public event PlayerObjectHandler PlayerObjectMoved;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerPickUpPickup"/> is being called.
        /// Called when a player picks up a pickup created with <see cref="Native.CreatePickup"/>.
        /// </summary>
        public event PlayerPickupHandler PlayerPickUpPickup;

        /// <summary>
        /// Occurs when the <see cref="OnVehicleMod"/> is being called.
        /// This callback is called when a vehicle is modded.
        /// </summary>
        /// <remarks>
        /// This callback is not called by <see cref="Native.AddVehicleComponent"/>.
        /// </remarks>
        public event VehicleModHandler VehicleMod;

        /// <summary>
        /// Occurs when the <see cref="OnEnterExitModShop"/> is being called.
        /// This callback is called when a player enters or exits a mod shop.
        /// </summary>
        public event PlayerEnterModShopHandler PlayerEnterExitModShop;

        /// <summary>
        /// Occurs when the <see cref="OnVehiclePaintjob"/> is being called.
        /// Called when a player changes the paintjob of their vehicle (in a modshop).
        /// </summary>
        public event VehiclePaintjobHandler VehiclePaintjobApplied;

        /// <summary>
        /// Occurs when the <see cref="OnVehicleRespray"/> is being called.
        /// The callback name is deceptive, this callback is called when a player exits a mod shop, regardless of whether the vehicle's colors were changed, and is NEVER called for pay 'n' spray garages.
        /// </summary>
        /// <remarks>
        /// Misleadingly, this callback is not called for pay 'n' spray (only modshops).
        /// </remarks>
        public event VehicleResprayedHandler VehicleResprayed;

        /// <summary>
        /// Occurs when the <see cref="OnVehicleDamageStatusUpdate"/> is being called.
        /// This callback is called when a vehicle element such as doors, tires, panels, or lights get damaged.
        /// </summary>
        /// <remarks>
        /// This does not include vehicle health changes.
        /// </remarks>
        public event PlayerHandler VehicleDamageStatusUpdated;

        /// <summary>
        /// Occurs when the <see cref="OnUnoccupiedVehicleUpdate"/> is being called.
        /// This callback is called everytime an unoccupied vehicle updates the server with their status.
        /// </summary>
        /// <remarks>
        /// This callback is called very frequently per second per unoccupied vehicle. You should refrain from implementing intensive calculations or intensive file writing/reading operations in this callback.
        /// </remarks>
        public event UnoccupiedVehicleUpdatedHandler UnoccupiedVehicleUpdated;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerSelectedMenuRow"/> is being called.
        /// This callback is called when a player selects an item from a menu.
        /// </summary>
        public event PlayerSelectedMenuRowHandler PlayerSelectedMenuRow;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerExitedMenu"/> is being called.
        /// Called when a player exits a menu.
        /// </summary>
        public event PlayerHandler PlayerExitedMenu;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerInteriorChange"/> is being called.
        /// Called when a player changes interior.
        /// </summary>
        /// <remarks>
        /// This is also called when <see cref="Native.SetPlayerInterior"/> is used.
        /// </remarks>
        public event PlayerInteriorChangedHandler PlayerInteriorChanged;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerKeyStateChange"/> is being called.
        /// This callback is called when the state of any supported key is changed (pressed/released). Directional keys do not trigger this callback.
        /// </summary>
        public event PlayerKeyStateChangedHandler PlayerKeyStateChanged;

        /// <summary>
        /// Occurs when the <see cref="OnRconLoginAttempt"/> is being called.
        /// This callback is called when someone tries to login to RCON, succesful or not.
        /// </summary>
        /// <remarks>
        /// This callback is only called when /rcon login is used.
        /// </remarks>
        public event RconLoginAttemptHandler RconLoginAttempt;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerUpdate"/> is being called.
        /// This callback is called everytime a client/player updates the server with their status.
        /// </summary>
        /// <remarks>
        /// This callback is called very frequently per second per player, only use it when you know what it's meant for.
        /// </remarks>
        public event PlayerHandler PlayerUpdate;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerStreamIn"/> is being called.
        /// This callback is called when a player is streamed by some other player's client.
        /// </summary>
        public event StreamPlayerHandler PlayerStreamIn;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerStreamOut"/> is being called.
        /// This callback is called when a player is streamed out from some other player's client.
        /// </summary>
        public event StreamPlayerHandler PlayerStreamOut;

        /// <summary>
        /// Occurs when the <see cref="OnVehicleStreamIn"/> is being called.
        /// Called when a vehicle is streamed to a player's client.
        /// </summary>
        public event PlayerVehicleHandler VehicleStreamIn;

        /// <summary>
        /// Occurs when the <see cref="OnVehicleStreamOut"/> is being called.
        /// This callback is called when a vehicle is streamed out from some player's client.
        /// </summary>
        public event PlayerVehicleHandler VehicleStreamOut;

        /// <summary>
        /// Occurs when the <see cref="OnDialogResponse"/> is being called.
        /// This callback is called when a player responds to a dialog shown using <see cref="Native.ShowPlayerDialog"/> by either clicking a button, pressing ENTER/ESC or double-clicking a list item (if using a list style dialog).
        /// </summary>
        public event DialogResponseHandler DialogResponse;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerTakeDamage"/> is being called.
        /// This callback is called when a player takes damage.
        /// </summary>
        public event PlayerDamageHandler PlayerTakeDamage;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerGiveDamage"/> is being called.
        /// This callback is called when a player gives damage to another player.
        /// </summary>
        /// <remarks>
        /// One thing you can do with GiveDamage is detect when other players report that they have damaged a certain player, and that player hasn't taken any health loss. You can flag those players as suspicious.
        /// You can also set all players to the same team (so they don't take damage from other players) and process all health loss from other players manually.
        /// You might have a server where players get a wanted level if they attack Cop players (or some specific class). In that case you might trust GiveDamage over TakeDamage.
        /// There should be a lot you can do with it. You just have to keep in mind the levels of trust between clients. In most cases it's better to trust the client who is being damaged to report their health/armour (TakeDamage). SA-MP normally does this. GiveDamage provides some extra information which may be useful when you require a different level of trust.
        /// </remarks>
        public event PlayerDamageHandler PlayerGiveDamage;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerClickMap"/> is being called.
        /// This callback is called when a player places a target/waypoint on the pause menu map (by right-clicking).
        /// </summary>
        /// <remarks>
        /// The Z value provided is only an estimate; you may find it useful to use a plugin like the MapAndreas plugin to get a more accurate Z coordinate (or for teleportation; use <see cref="Native.SetPlayerPosFindZ(int,Vector)"/>).
        /// </remarks>
        public event PlayerClickMapHandler PlayerClickMap;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerClickTextDraw"/> is being called.
        /// This callback is called when a player clicks on a textdraw or cancels the select mode(ESC).
        /// </summary>
        /// <remarks>
        /// The clickable area is defined by <see cref="Native.TextDrawTextSize"/>. The x and y parameters passed to that function must not be zero or negative.
        /// </remarks>
        public event PlayerClickTextDrawHandler PlayerClickTextDraw;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerClickPlayerTextDraw"/> is being called.
        /// This callback is called when a player clicks on a player-textdraw. It is not called when player cancels the select mode (ESC) - however, <see cref="OnPlayerClickTextDraw"/> is.
        /// </summary>
        public event PlayerClickTextDrawHandler PlayerClickPlayerTextDraw;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerClickPlayer"/> is being called.
        /// Called when a player double-clicks on a player on the scoreboard.
        /// </summary>
        /// <remarks>
        /// There is currently only one 'source' (<see cref="PlayerClickSource.Scoreboard"/>). The existence of this argument suggests that more sources may be supported in the future.
        /// </remarks>
        public event PlayerClickPlayerHandler PlayerClickPlayer;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerEditObject"/> is being called.
        /// This callback is called when a player ends object edition mode.
        /// </summary>
        public event PlayerEditObjectHandler PlayerEditObject;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerEditAttachedObject"/> is being called.
        /// This callback is called when a player ends attached object edition mode.
        /// </summary>
        /// <remarks>
        /// Editions should be discarded if response was '0' (cancelled). This must be done by storing the offsets etc. in an array BEFORE using EditAttachedObject.
        /// </remarks>
        public event PlayerEditAttachedObjectHandler PlayerEditAttachedObject;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerSelectObject"/> is being called.
        /// This callback is called when a player selects an object after <see cref="Native.SelectObject"/> has been used.
        /// </summary>
        public event PlayerSelectObjectHandler PlayerSelectObject;

        /// <summary>
        /// Occurs when the <see cref="OnPlayerWeaponShot"/> is being called.
        /// This callback is called when a player fires a shot from a weapon.
        /// </summary>
        /// <remarks>
        /// <see cref="BulletHitType.None"/>: the fX, fY and fZ parameters are normal coordinates;
        /// Others: the fX, fY and fZ are offsets from the center of hitid.
        /// </remarks>
        public event WeaponShotHandler PlayerWeaponShot;

        #endregion

        #region Callbacks

        /// <summary>
        /// This callback is triggered when a timer ticks.
        /// </summary>
        /// <param name="timerid">The ID of the ticking timer.</param>
        /// <param name="args">The args object as parsed with <see cref="Native.SetTimer"/>.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnTimerTick(int timerid, object args)
        {
            Console.WriteLine("TimerTick");
            var timer = args as Timer;

            if (timer != null)
                timer.OnTick(EventArgs.Empty);

            return true;
        }

        /// <summary>
        /// This callback is triggered when the gamemode starts.
        /// </summary>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnGameModeInit()
        {
            var args = new GameModeEventArgs();

            if (Initialized != null)
                Initialized(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a gamemode ends.
        /// </summary>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnGameModeExit()
        {
            var args = new GameModeEventArgs();

            if (Exited != null)
                Exited(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player connects to the server.
        /// </summary>
        /// <param name="playerid">The ID of the player that connected.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerConnect(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerConnected != null)
                PlayerConnected(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player disconnects from the server.
        /// </summary>
        /// <param name="playerid">ID of the player that disconnected.</param>
        /// <param name="reason">The reason for the disconnection.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerDisconnect(int playerid, int reason)
        {
            var args = new PlayerDisconnectedEventArgs(playerid, (DisconnectReason) reason);

            if (PlayerDisconnected != null)
                PlayerDisconnected(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player spawns.
        /// </summary>
        /// <param name="playerid">The ID of the player that spawned.</param>
        /// <returns>Return False in this callback to force the player back to class selection when they next respawn.</returns>
        public virtual bool OnPlayerSpawn(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerSpawned != null)
                PlayerSpawned(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player dies.
        /// </summary>
        /// <param name="playerid">The ID of the player that died.</param>
        /// <param name="killerid">The ID of the player that killed the player who died, or <see cref="Misc.InvalidPlayerId"/> if there was none.</param>
        /// <param name="reason">The ID of the reason for the player's death.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerDeath(int playerid, int killerid, int reason)
        {
            var args = new PlayerDeathEventArgs(playerid, killerid, (Weapon) reason);

            if (PlayerDied != null)
                PlayerDied(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a vehicle respawns.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle that spawned.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnVehicleSpawn(int vehicleid)
        {
            var args = new VehicleEventArgs(vehicleid);

            if (VehicleSpawned != null)
                VehicleSpawned(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a vehicle is destroyed - either by exploding or becoming submerged in water.
        /// </summary>
        /// <remarks>
        /// This callback will also be called when a vehicle enters water, but the vehicle can be saved from destruction by teleportation or driving out (if only partially submerged). The callback won't be called a second time, and the vehicle may disappear when the driver exits, or after a short time.
        /// </remarks>
        /// <param name="vehicleid">The ID of the vehicle that was destroyed.</param>
        /// <param name="killerid">The ID of the player that reported (synced) the vehicle's destruction (name is misleading). Generally the driver or a passenger (if any) or the closest player.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnVehicleDeath(int vehicleid, int killerid)
        {
            var args = new PlayerVehicleEventArgs(killerid, vehicleid);

            if (VehicleDied != null)
                VehicleDied(this, args);

            return args.Success;
        }

        /// <summary>
        /// Called when a player sends a chat message.
        /// </summary>
        /// <param name="playerid">The ID of the player who typed the text.</param>
        /// <param name="text">The text the player typed.</param>
        /// <returns>Returning False in this callback will stop the text from being sent.</returns>
        public virtual bool OnPlayerText(int playerid, string text)
        {
            var args = new PlayerTextEventArgs(playerid, text);

            if (PlayerText != null)
                PlayerText(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player enters a command into the client chat window, e.g. /help.
        /// </summary>
        /// <param name="playerid">The ID of the player that executed the command.</param>
        /// <param name="cmdtext">The command that was executed (including the slash).</param>
        /// <returns>False if the command was not processed, otherwise True.</returns>
        public virtual bool OnPlayerCommandText(int playerid, string cmdtext)
        {
            var args = new PlayerTextEventArgs(playerid, cmdtext) {Success = false};

            if (PlayerCommandText != null)
                PlayerCommandText(this, args);

            return args.Success;
        }

        /// <summary>
        /// Called when a player changes class at class selection (and when class selection first appears).
        /// </summary>
        /// <param name="playerid">The ID of the player that changed class.</param>
        /// <param name="classid">The ID of the current class being viewed.</param>
        /// <returns>Returning False in this callback will prevent the player from spawning. The player can be forced to spawn when <see cref="Native.SpawnPlayer"/> is used, however the player will re-enter class selection the next time they die.</returns>
        public virtual bool OnPlayerRequestClass(int playerid, int classid)
        {
            var args = new PlayerRequestClassEventArgs(playerid, classid);

            if (PlayerRequestClass != null)
                PlayerRequestClass(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player starts to enter a vehicle, meaning the player is not in vehicle yet at the time this callback is called.
        /// </summary>
        /// <param name="playerid">ID of the player who attempts to enter a vehicle.</param>
        /// <param name="vehicleid">ID of the vehicle the player is attempting to enter.</param>
        /// <param name="ispassenger">False if entering as driver. True if entering as passenger.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerEnterVehicle(int playerid, int vehicleid, bool ispassenger)
        {
            var args = new PlayerEnterVehicleEventArgs(playerid, vehicleid, ispassenger);

            if (PlayerEnterVehicle != null)
                PlayerEnterVehicle(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        /// Not called if the player falls off a bike or is removed from a vehicle by other means such as using <see cref="Native.SetPlayerPos(int,Vector)"/>.
        /// </remarks>
        /// <param name="playerid">The ID of the player who exited the vehicle.</param>
        /// <param name="vehicleid">The ID of the vehicle the player is exiting.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerExitVehicle(int playerid, int vehicleid)
        {
            var args = new PlayerVehicleEventArgs(playerid, vehicleid);

            if (PlayerExitVehicle != null)
                PlayerExitVehicle(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player exits a vehicle.
        /// </summary>
        /// <remarks>
        /// Not called if the player falls off a bike or is removed from a vehicle by other means such as using <see cref="Native.SetPlayerPos(int,Vector)"/>.
        /// </remarks>
        /// <param name="playerid">The ID of the player that changed state.</param>
        /// <param name="newstate">The player's new state.</param>
        /// <param name="oldstate">The player's previous state.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerStateChange(int playerid, int newstate, int oldstate)
        {
            var args = new PlayerStateEventArgs(playerid, (PlayerState) newstate, (PlayerState) oldstate);

            if (PlayerStateChanged != null)
                PlayerStateChanged(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player enters the checkpoint set for that player.
        /// </summary>
        /// <param name="playerid">The player who entered the checkpoint.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerEnterCheckpoint(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerEnterCheckpoint != null)
                PlayerEnterCheckpoint(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player leaves the checkpoint set for that player.
        /// </summary>
        /// <param name="playerid">The player who left the checkpoint.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerLeaveCheckpoint(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerLeaveCheckpoint != null)
                PlayerLeaveCheckpoint(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player enters a race checkpoint.
        /// </summary>
        /// <param name="playerid">The ID of the player who entered the race checkpoint.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerEnterRaceCheckpoint(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerEnterRaceCheckpoint != null)
                PlayerEnterRaceCheckpoint(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player leaves the race checkpoint.
        /// </summary>
        /// <param name="playerid">The player who left the race checkpoint.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerLeaveRaceCheckpoint(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerLeaveRaceCheckpoint != null)
                PlayerLeaveRaceCheckpoint(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a command is sent through the server console, remote RCON, or via the in-game /rcon command.
        /// </summary>
        /// <param name="command">A string containing the command that was typed, as well as any passed parameters.</param>
        /// <returns>False if the command was not processed, it will be passed to another script or True if the command was processed, will not be passed to other scripts.</returns>
        public virtual bool OnRconCommand(string command)
        {
            var args = new RconEventArgs(command);

            if (RconCommand != null)
                RconCommand(this, args);

            return args.Success;
        }

        /// <summary>
        /// Called when a player attempts to spawn via class selection.
        /// </summary>
        /// <param name="playerid">The ID of the player who requested to spawn.</param>
        /// <returns>Returning False in this callback will prevent the player from spawning.</returns>
        public virtual bool OnPlayerRequestSpawn(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerRequestSpawn != null)
                PlayerRequestSpawn(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when an object is moved after <see cref="Native.MoveObject"/> (when it stops moving).
        /// </summary>
        /// <remarks>
        /// SetObjectPos does not work when used in this callback. To fix it, delete and re-create the object, or use a timer.
        /// </remarks>
        /// <param name="objectid">The ID of the object that was moved.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnObjectMoved(int objectid)
        {
            var args = new ObjectEventArgs(objectid);

            if (ObjectMoved != null)
                ObjectMoved(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player object is moved after <see cref="Native.MovePlayerObject"/> (when it stops moving).
        /// </summary>
        /// <param name="playerid">The playerid the object is assigned to.</param>
        /// <param name="objectid">The ID of the player-object that was moved.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerObjectMoved(int playerid, int objectid)
        {
            var args = new PlayerObjectEventArgs(playerid, objectid);

            if (PlayerObjectMoved != null)
                PlayerObjectMoved(this, args);

            return args.Success;
        }

        /// <summary>
        /// Called when a player picks up a pickup created with <see cref="Native.CreatePickup"/>.
        /// </summary>
        /// <param name="playerid">The ID of the player that picked up the pickup.</param>
        /// <param name="pickupid">The ID of the pickup, returned by <see cref="Native.CreatePickup"/>.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerPickUpPickup(int playerid, int pickupid)
        {
            var args = new PlayerPickupEventArgs(playerid, pickupid);

            if (PlayerPickUpPickup != null)
                PlayerPickUpPickup(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a vehicle is modded.
        /// </summary>
        /// <remarks>
        /// This callback is not called by <see cref="Native.AddVehicleComponent"/>.
        /// </remarks>
        /// <param name="playerid">The ID of the driver of the vehicle.</param>
        /// <param name="vehicleid">The ID of the vehicle which is modded.</param>
        /// <param name="componentid">The ID of the component which was added to the vehicle.</param>
        /// <returns>Return False to desync the mod (or an invalid mod) from propagating and / or crashing players.</returns>
        public virtual bool OnVehicleMod(int playerid, int vehicleid, int componentid)
        {
            var args = new VehicleModEventArgs(playerid, vehicleid, componentid);

            if (VehicleMod != null)
                VehicleMod(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player enters or exits a mod shop.
        /// </summary>
        /// <param name="playerid">The ID of the player that entered or exited the modshop.</param>
        /// <param name="enterexit">1 if the player entered or 0 if they exited.</param>
        /// <param name="interiorid">The interior ID of the modshop that the player is entering (or 0 if exiting).</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnEnterExitModShop(int playerid, int enterexit, int interiorid)
        {
            var args = new PlayerEnterModShopEventArgs(playerid, (EnterExit) enterexit, interiorid);

            if (PlayerEnterExitModShop != null)
                PlayerEnterExitModShop(this, args);

            return args.Success;
        }

        /// <summary>
        /// Called when a player changes the paintjob of their vehicle (in a modshop).
        /// </summary>
        /// <param name="playerid">The ID of the player whos vehicle is modded.</param>
        /// <param name="vehicleid">The ID of the vehicle that changed paintjob.</param>
        /// <param name="paintjobid">The ID of the new paintjob.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnVehiclePaintjob(int playerid, int vehicleid, int paintjobid)
        {
            var args = new VehiclePaintjobEventArgs(playerid, vehicleid, paintjobid);

            if (VehiclePaintjobApplied != null)
                VehiclePaintjobApplied(this, args);

            return args.Success;
        }

        /// <summary>
        /// The callback name is deceptive, this callback is called when a player exits a mod shop, regardless of whether the vehicle's colors were changed, and is NEVER called for pay 'n' spray garages.
        /// </summary>
        /// <remarks>
        /// Misleadingly, this callback is not called for pay 'n' spray (only modshops).
        /// </remarks>
        /// <param name="playerid">The ID of the player that is driving the vehicle.</param>
        /// <param name="vehicleid">The ID of the vehicle that was resprayed.</param>
        /// <param name="color1">The color that the vehicle's primary color was changed to.</param>
        /// <param name="color2">The color that the vehicle's secondary color was changed to.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnVehicleRespray(int playerid, int vehicleid, int color1, int color2)
        {
            var args = new VehicleResprayedEventArgs(playerid, vehicleid, color1, color2);

            if (VehicleResprayed != null)
                VehicleResprayed(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a vehicle element such as doors, tires, panels, or lights get damaged.
        /// </summary>
        /// <remarks>
        /// This does not include vehicle health changes.
        /// </remarks>
        /// <param name="vehicleid">The ID of the vehicle that was damaged.</param>
        /// <param name="playerid">The ID of the player who synced the damage (who had the car damaged).</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnVehicleDamageStatusUpdate(int vehicleid, int playerid)
        {
            var args = new PlayerVehicleEventArgs(playerid, vehicleid);

            if (VehicleDamageStatusUpdated != null)
                VehicleDamageStatusUpdated(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called everytime an unoccupied vehicle updates the server with their status.
        /// </summary>
        /// <remarks>
        /// This callback is called very frequently per second per unoccupied vehicle. You should refrain from implementing intensive calculations or intensive file writing/reading operations in this callback.
        /// </remarks>
        /// <param name="vehicleid">The vehicleid that the callback is processing.</param>
        /// <param name="playerid">The playerid that the callback is processing (the playerid affecting the vehicle).</param>
        /// <param name="passengerSeat">The passenger seat of the playerid moving the vehicle. 0 if they're not in the vehicle.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnUnoccupiedVehicleUpdate(int vehicleid, int playerid, int passengerSeat)
        {
            var args = new UnoccupiedVehicleEventArgs(playerid, vehicleid, passengerSeat);

            if (UnoccupiedVehicleUpdated != null)
                UnoccupiedVehicleUpdated(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player selects an item from a menu.
        /// </summary>
        /// <param name="playerid">The ID of the player that selected an item on the menu.</param>
        /// <param name="row">The row that was selected.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerSelectedMenuRow(int playerid, int row)
        {
            var args = new PlayerSelectedMenuRowEventArgs(playerid, row);

            if (PlayerSelectedMenuRow != null)
                PlayerSelectedMenuRow(this, args);

            return args.Success;
        }

        /// <summary>
        /// Called when a player exits a menu.
        /// </summary>
        /// <param name="playerid">The ID of the player that exited the menu.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerExitedMenu(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerExitedMenu != null)
                PlayerExitedMenu(this, args);

            return args.Success;
        }

        /// <summary>
        /// Called when a player changes interior.
        /// </summary>
        /// <remarks>
        /// This is also called when <see cref="Native.SetPlayerInterior"/> is used.
        /// </remarks>
        /// <param name="playerid">The playerid who changed interior.</param>
        /// <param name="newinteriorid">The interior the player is now in.</param>
        /// <param name="oldinteriorid">The interior the player was in.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerInteriorChange(int playerid, int newinteriorid, int oldinteriorid)
        {
            var args = new PlayerInteriorChangedEventArgs(playerid, newinteriorid, oldinteriorid);

            if (PlayerInteriorChanged != null)
                PlayerInteriorChanged(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when the state of any supported key is changed (pressed/released). Directional keys do not trigger this callback.
        /// </summary>
        /// <param name="playerid">ID of the player who pressed/released a key.</param>
        /// <param name="newkeys">A map of the keys currently held.</param>
        /// <param name="oldkeys">A map of the keys held prior to the current change.</param>
        /// <returns> True - Allows this callback to be called in other scripts. False - Callback will not be called in other scripts. It is always called first in gamemodes so returning False there blocks filterscripts from seeing it.</returns>
        public virtual bool OnPlayerKeyStateChange(int playerid, int newkeys, int oldkeys)
        {
            var args = new PlayerKeyStateChangedEventArgs(playerid, (Keys) newkeys, (Keys) oldkeys);

            if (PlayerKeyStateChanged != null)
                PlayerKeyStateChanged(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when someone tries to login to RCON, succesful or not.
        /// </summary>
        /// <remarks>
        /// This callback is only called when /rcon login is used.
        /// </remarks>
        /// <param name="ip">The IP of the player that tried to login to RCON.</param>
        /// <param name="password">The password used to login with.</param>
        /// <param name="success">False if the password was incorrect or True if it was correct.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnRconLoginAttempt(string ip, string password, bool success)
        {
            var args = new RconLoginAttemptEventArgs(ip, password, success);

            if (RconLoginAttempt != null)
                RconLoginAttempt(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called everytime a client/player updates the server with their status.
        /// </summary>
        /// <remarks>
        /// This callback is called very frequently per second per player, only use it when you know what it's meant for.
        /// </remarks>
        /// <param name="playerid">ID of the player sending an update packet.</param>
        /// <returns>False - Update from this player will not be replicated to other clients.</returns>
        public virtual bool OnPlayerUpdate(int playerid)
        {
            var args = new PlayerEventArgs(playerid);

            if (PlayerUpdate != null)
                PlayerUpdate(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player is streamed by some other player's client.
        /// </summary>
        /// <param name="playerid">The ID of the player who has been streamed.</param>
        /// <param name="forplayerid">The ID of the player that streamed the other player in.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerStreamIn(int playerid, int forplayerid)
        {
            var args = new StreamPlayerEventArgs(playerid, forplayerid);

            if (PlayerStreamIn != null)
                PlayerStreamIn(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player is streamed out from some other player's client.
        /// </summary>
        /// <param name="playerid">The player who has been destreamed.</param>
        /// <param name="forplayerid">The player who has destreamed the other player.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerStreamOut(int playerid, int forplayerid)
        {
            var args = new StreamPlayerEventArgs(playerid, forplayerid);

            if (PlayerStreamOut != null)
                PlayerStreamOut(this, args);

            return args.Success;
        }

        /// <summary>
        /// Called when a vehicle is streamed to a player's client.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle that streamed in for the player.</param>
        /// <param name="forplayerid">The ID of the player who the vehicle streamed in for.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnVehicleStreamIn(int vehicleid, int forplayerid)
        {
            var args = new PlayerVehicleEventArgs(forplayerid, vehicleid);

            if (VehicleStreamIn != null)
                VehicleStreamIn(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a vehicle is streamed out from some player's client.
        /// </summary>
        /// <param name="vehicleid">The ID of the vehicle that streamed out.</param>
        /// <param name="forplayerid">The ID of the player who is no longer streaming the vehicle.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnVehicleStreamOut(int vehicleid, int forplayerid)
        {
            var args = new PlayerVehicleEventArgs(forplayerid, vehicleid);

            if (VehicleStreamOut != null)
                VehicleStreamOut(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player responds to a dialog shown using <see cref="Native.ShowPlayerDialog"/> by either clicking a button, pressing ENTER/ESC or double-clicking a list item (if using a list style dialog).
        /// </summary>
        /// <param name="playerid">The ID of the player that responded to the dialog.</param>
        /// <param name="dialogid">The ID of the dialog the player responded to, assigned in <see cref="Native.ShowPlayerDialog"/>.</param>
        /// <param name="response">1 for left button and 0 for right button (if only one button shown, always 1).</param>
        /// <param name="listitem">The ID of the list item selected by the player (starts at 0) (only if using a list style dialog).</param>
        /// <param name="inputtext">The text entered into the input box by the player or the selected list item text.</param>
        /// <returns>Returning False in this callback will pass the dialog to another script in case no matching code were found in your gamemode's callback.</returns>
        public virtual bool OnDialogResponse(int playerid, int dialogid, int response, int listitem, string inputtext)
        {
            var args = new DialogResponseEventArgs(playerid, dialogid, response, listitem, inputtext);

            if (DialogResponse != null)
                DialogResponse(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player takes damage.
        /// </summary>
        /// <param name="playerid">The ID of the player that took damage.</param>
        /// <param name="issuerid">The ID of the player that caused the damage. INVALID_PLAYER_ID if self-inflicted.</param>
        /// <param name="amount">The amount of dagmage the player took (health and armour combined).</param>
        /// <param name="weaponid">The ID of the weapon/reason for the damage.</param>
        /// <param name="bodypart">The body part that was hit.</param>
        /// <returns> True: Allows this callback to be called in other scripts. False Callback will not be called in other scripts. It is always called first in gamemodes so returning False there blocks filterscripts from seeing it.</returns>
        public virtual bool OnPlayerTakeDamage(int playerid, int issuerid, float amount, int weaponid, int bodypart)
        {
            var args = new PlayerDamageEventArgs(playerid, issuerid, amount, (Weapon) weaponid, (BodyPart) bodypart);

            if (PlayerTakeDamage != null)
                PlayerTakeDamage(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player gives damage to another player.
        /// </summary>
        /// <remarks>
        /// One thing you can do with GiveDamage is detect when other players report that they have damaged a certain player, and that player hasn't taken any health loss. You can flag those players as suspicious.
        /// You can also set all players to the same team (so they don't take damage from other players) and process all health loss from other players manually.
        /// You might have a server where players get a wanted level if they attack Cop players (or some specific class). In that case you might trust GiveDamage over TakeDamage.
        /// There should be a lot you can do with it. You just have to keep in mind the levels of trust between clients. In most cases it's better to trust the client who is being damaged to report their health/armour (TakeDamage). SA-MP normally does this. GiveDamage provides some extra information which may be useful when you require a different level of trust.
        /// </remarks>
        /// <param name="playerid">The ID of the player that gave damage.</param>
        /// <param name="damagedid">The ID of the player that received damage.</param>
        /// <param name="amount">The amount of health/armour damagedid has lost (combined).</param>
        /// <param name="weaponid">The reason that caused the damage.</param>
        /// <param name="bodypart">The body part that was hit.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerGiveDamage(int playerid, int damagedid, float amount, int weaponid, int bodypart)
        {
            var args = new PlayerDamageEventArgs(playerid, damagedid, amount, (Weapon) weaponid, (BodyPart) bodypart);

            if (PlayerGiveDamage != null)
                PlayerGiveDamage(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player places a target/waypoint on the pause menu map (by right-clicking).
        /// </summary>
        /// <remarks>
        /// The Z value provided is only an estimate; you may find it useful to use a plugin like the MapAndreas plugin to get a more accurate Z coordinate (or for teleportation; use <see cref="Native.SetPlayerPosFindZ(int,Vector)"/>).
        /// </remarks>
        /// <param name="playerid">The ID of the player that placed a target/waypoint.</param>
        /// <param name="fX">The X float coordinate where the player clicked.</param>
        /// <param name="fY">The Y float coordinate where the player clicked.</param>
        /// <param name="fZ">The Z float coordinate where the player clicked (inaccurate - see note below).</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerClickMap(int playerid, float fX, float fY, float fZ)
        {
            var args = new PlayerClickMapEventArgs(playerid, new Vector(fX, fY, fZ));

            if (PlayerClickMap != null)
                PlayerClickMap(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player clicks on a textdraw or cancels the select mode(ESC).
        /// </summary>
        /// <remarks>
        /// The clickable area is defined by <see cref="Native.TextDrawTextSize"/>. The x and y parameters passed to that function must not be zero or negative.
        /// </remarks>
        /// <param name="playerid">The ID of the player that clicked on the textdraw.</param>
        /// <param name="clickedid">The ID of the clicked textdraw. INVALID_TEXT_DRAW if selection was cancelled.</param>
        /// <returns>Returning True in this callback will prevent it being called in other scripts. This should be used to signal that the textdraw on which they clicked was 'found' and no further processing is needed. You should return False if the textdraw on which they clicked wasn't found.</returns>
        public virtual bool OnPlayerClickTextDraw(int playerid, int clickedid)
        {
            var args = new PlayerClickTextDrawEventArgs(playerid, clickedid);

            if (PlayerClickTextDraw != null)
                PlayerClickTextDraw(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player clicks on a player-textdraw. It is not called when player cancels the select mode (ESC) - however, <see cref="OnPlayerClickTextDraw"/> is.
        /// </summary>
        /// <param name="playerid">The ID of the player that selected a textdraw.</param>
        /// <param name="playertextid">The ID of the player-textdraw that the player selected.</param>
        /// <returns>Returning True in this callback will prevent it being called in other scripts. This should be used to signal that the textdraw on which they clicked was 'found' and no further processing is needed. You should return False if the textdraw on which they clicked wasn't found.</returns>
        public virtual bool OnPlayerClickPlayerTextDraw(int playerid, int playertextid)
        {
            var args = new PlayerClickTextDrawEventArgs(playerid, playertextid);

            if (PlayerClickPlayerTextDraw != null)
                PlayerClickPlayerTextDraw(this, args);

            return args.Success;
        }

        /// <summary>
        /// Called when a player double-clicks on a player on the scoreboard.
        /// </summary>
        /// <remarks>
        /// There is currently only one 'source' (0 - CLICK_SOURCE_SCOREBOARD). The existence of this argument suggests that more sources may be supported in the future.
        /// </remarks>
        /// <param name="playerid">The ID of the player that clicked on a player on the scoreboard.</param>
        /// <param name="clickedplayerid">The ID of the player that was clicked on.</param>
        /// <param name="source">The source of the player's click.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerClickPlayer(int playerid, int clickedplayerid, int source)
        {
            var args = new PlayerClickPlayerEventArgs(playerid, clickedplayerid, (PlayerClickSource) source);

            if (PlayerClickPlayer != null)
                PlayerClickPlayer(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player ends object edition mode.
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
        public virtual bool OnPlayerEditObject(int playerid, bool playerobject, int objectid, int response, float fX,
            float fY,
            float fZ, float fRotX, float fRotY, float fRotZ)
        {
            var args = new PlayerEditObjectEventArgs(playerid, playerobject, objectid, (EditObjectResponse) response,
                new Vector(fX, fY, fZ), new Rotation(fRotX, fRotY, fRotZ));

            if (PlayerEditObject != null)
                PlayerEditObject(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player ends attached object edition mode.
        /// </summary>
        /// <remarks>
        /// Editions should be discarded if response was '0' (cancelled). This must be done by storing the offsets etc. in an array BEFORE using EditAttachedObject.
        /// </remarks>
        /// <param name="playerid">The ID of the player that ended edition mode.</param>
        /// <param name="response">0 if they cancelled (ESC) or 1 if they clicked the save icon.</param>
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
        public virtual bool OnPlayerEditAttachedObject(int playerid, int response, int index, int modelid, int boneid,
            float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX,
            float fScaleY, float fScaleZ)
        {
            var args = new PlayerEditAttachedObjectEventArgs(playerid, (EditObjectResponse) response, index, modelid,
                boneid, new Vector(fOffsetX, fOffsetY, fOffsetZ), new Rotation(fRotX, fRotY, fRotZ),
                new Vector(fScaleX, fScaleY, fScaleZ));

            if (PlayerEditAttachedObject != null)
                PlayerEditAttachedObject(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player selects an object after <see cref="Native.SelectObject"/> has been used.
        /// </summary>
        /// <param name="playerid">The ID of the player that selected an object.</param>
        /// <param name="type">The type of selection.</param>
        /// <param name="objectid">The ID of the selected object.</param>
        /// <param name="modelid">The model of the selected object.</param>
        /// <param name="fX">The X position of the selected object.</param>
        /// <param name="fY">The Y position of the selected object.</param>
        /// <param name="fZ">The Z position of the selected object.</param>
        /// <returns>This callback does not handle returns.</returns>
        public virtual bool OnPlayerSelectObject(int playerid, int type, int objectid, int modelid, float fX, float fY,
            float fZ)
        {
            var args = new PlayerSelectObjectEventArgs(playerid, (ObjectType) type, objectid, modelid,
                new Vector(fX, fY, fZ));

            if (PlayerSelectObject != null)
                PlayerSelectObject(this, args);

            return args.Success;
        }

        /// <summary>
        /// This callback is called when a player fires a shot from a weapon.
        /// </summary>
        /// <remarks>
        /// BULLET_HIT_TYPE_NONE: the fX, fY and fZ parameters are normal coordinates;
        /// Others: the fX, fY and fZ are offsets from the center of hitid.
        /// </remarks>
        /// <param name="playerid">The ID of the player that shot a weapon.</param>
        /// <param name="weaponid">The ID of the weapon shot by the player.</param>
        /// <param name="hittype">The type of thing the shot hit (none, player, vehicle, or (player)object).</param>
        /// <param name="hitid">The ID of the player, vehicle or object that was hit.</param>
        /// <param name="fX">The X coordinate that the shot hit.</param>
        /// <param name="fY">The Y coordinate that the shot hit.</param>
        /// <param name="fZ">The Z coordinate that the shot hit.</param>
        /// <returns> False: Prevent the bullet from causing damage. True: Allow the bullet to cause damage.</returns>
        public virtual bool OnPlayerWeaponShot(int playerid, int weaponid, int hittype, int hitid, float fX, float fY,
            float fZ)
        {
            var args = new WeaponShotEventArgs(playerid, (Weapon) weaponid, (BulletHitType) hittype, hitid,
                new Vector(fX, fY, fZ));

            if (PlayerWeaponShot != null)
                PlayerWeaponShot(this, args);

            return args.Success;
        }

        #endregion
    }
}
