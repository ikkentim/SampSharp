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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode
{
    public abstract partial class BaseMode
    {
        internal bool OnTimerTick(int timerid, object args)
        {
            /*
             * Pass straight trough to TimerTick. Set the args as sender.
             */
            if (TimerTick != null && args != null)
                TimerTick(args, EventArgs.Empty);

            return true;
        }

        internal bool OnGameModeInit()
        {
            OnInitialized(EventArgs.Empty);

            return true;
        }

        internal bool OnGameModeExit()
        {
            OnExited(EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerConnect(int playerid)
        {
            OnPlayerConnected(BasePlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerDisconnect(int playerid, int reason)
        {
            var args = new DisconnectEventArgs((DisconnectReason) reason);

            OnPlayerDisconnected(BasePlayer.FindOrCreate(playerid), args);
            OnPlayerCleanup(BasePlayer.FindOrCreate(playerid), args);

            return true;
        }

        internal bool OnPlayerSpawn(int playerid)
        {
            var args = new SpawnEventArgs();

            OnPlayerSpawned(BasePlayer.FindOrCreate(playerid), args);

            return !args.ReturnToClassSelection;
        }

        internal bool OnPlayerDeath(int playerid, int killerid, int reason)
        {
            OnPlayerDied(BasePlayer.FindOrCreate(playerid),
                new DeathEventArgs(killerid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(killerid),
                    (Weapon) reason));

            return true;
        }

        internal bool OnVehicleSpawn(int vehicleid)
        {
            OnVehicleSpawned(BaseVehicle.FindOrCreate(vehicleid), EventArgs.Empty);

            return true;
        }

        internal bool OnVehicleDeath(int vehicleid, int killerid)
        {
            OnVehicleDied(BaseVehicle.FindOrCreate(vehicleid),
                new PlayerEventArgs(killerid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(killerid)));

            return true;
        }

        internal bool OnPlayerText(int playerid, string text)
        {
            var args = new TextEventArgs(text);

            OnPlayerText(BasePlayer.FindOrCreate(playerid), args);

            return args.SendToPlayers;
        }

        internal bool OnPlayerCommandText(int playerid, string cmdtext)
        {
            var args = new CommandTextEventArgs(cmdtext);

            OnPlayerCommandText(BasePlayer.FindOrCreate(playerid), args);

            return args.Success;
        }

        internal bool OnPlayerRequestClass(int playerid, int classid)
        {
            var args = new RequestClassEventArgs(classid);

            OnPlayerRequestClass(BasePlayer.FindOrCreate(playerid), args);

            return !args.PreventSpawning;
        }

        internal bool OnPlayerEnterVehicle(int playerid, int vehicleid, bool ispassenger)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            OnPlayerEnterVehicle(player,
                new EnterVehicleEventArgs(player, BaseVehicle.FindOrCreate(vehicleid), ispassenger));

            return true;
        }

        internal bool OnPlayerExitVehicle(int playerid, int vehicleid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            OnPlayerExitVehicle(player,
                new PlayerVehicleEventArgs(player, BaseVehicle.FindOrCreate(vehicleid)));

            return true;
        }

        internal bool OnPlayerStateChange(int playerid, int newstate, int oldstate)
        {
            OnPlayerStateChanged(BasePlayer.FindOrCreate(playerid),
                new StateEventArgs((PlayerState) newstate, (PlayerState) oldstate));

            return true;
        }

        internal bool OnPlayerEnterCheckpoint(int playerid)
        {
            OnPlayerEnterCheckpoint(BasePlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerLeaveCheckpoint(int playerid)
        {
            OnPlayerLeaveCheckpoint(BasePlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerEnterRaceCheckpoint(int playerid)
        {
            OnPlayerEnterRaceCheckpoint(BasePlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerLeaveRaceCheckpoint(int playerid)
        {
            OnPlayerLeaveRaceCheckpoint(BasePlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        internal bool OnRconCommand(string command)
        {
            var args = new RconEventArgs(command);
            OnRconCommand(args);

            return args.Success;
        }

        internal bool OnPlayerRequestSpawn(int playerid)
        {
            var args = new RequestSpawnEventArgs();

            OnPlayerRequestSpawn(BasePlayer.FindOrCreate(playerid), args);

            return !args.PreventSpawning;
        }

        internal bool OnObjectMoved(int objectid)
        {
            OnObjectMoved(GlobalObject.FindOrCreate(objectid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerObjectMoved(int playerid, int objectid)
        {
            OnPlayerObjectMoved(PlayerObject.FindOrCreate(BasePlayer.FindOrCreate(playerid), objectid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerPickUpPickup(int playerid, int pickupid)
        {
            OnPlayerPickUpPickup(Pickup.FindOrCreate(pickupid), new PlayerEventArgs(BasePlayer.FindOrCreate(playerid)));

            return true;
        }

        internal bool OnVehicleMod(int playerid, int vehicleid, int componentid)
        {
            var args = new VehicleModEventArgs(BasePlayer.FindOrCreate(playerid), componentid);

            OnVehicleMod(BaseVehicle.FindOrCreate(vehicleid), args);

            return !args.PreventPropagation;
        }

        internal bool OnEnterExitModShop(int playerid, int enterexit, int interiorid)
        {
            OnPlayerEnterExitModShop(BasePlayer.FindOrCreate(playerid),
                new EnterModShopEventArgs((EnterExit) enterexit, interiorid));

            return true;
        }

        internal bool OnVehiclePaintjob(int playerid, int vehicleid, int paintjobid)
        {
            OnVehiclePaintjobApplied(BaseVehicle.FindOrCreate(vehicleid),
                new VehiclePaintjobEventArgs(BasePlayer.FindOrCreate(playerid), paintjobid));


            return true;
        }

        internal bool OnVehicleRespray(int playerid, int vehicleid, int color1, int color2)
        {
            OnVehicleResprayed(BaseVehicle.FindOrCreate(vehicleid),
                new VehicleResprayedEventArgs(BasePlayer.FindOrCreate(playerid), color1, color2));

            return true;
        }

        internal bool OnVehicleDamageStatusUpdate(int vehicleid, int playerid)
        {
            OnVehicleDamageStatusUpdated(BaseVehicle.FindOrCreate(vehicleid),
                new PlayerEventArgs(BasePlayer.FindOrCreate(playerid)));

            return true;
        }

        internal bool OnUnoccupiedVehicleUpdate(int vehicleid, int playerid, int passengerSeat, float newX,
            float newY, float newZ, float velX, float velY, float velZ)
        {
            var args = new UnoccupiedVehicleEventArgs(BasePlayer.FindOrCreate(playerid), passengerSeat,
                new Vector3(newX, newY, newZ), new Vector3(velX, velY, velZ));
            OnUnoccupiedVehicleUpdated(BaseVehicle.FindOrCreate(vehicleid), args);

            return !args.PreventPropagation;
        }

        internal bool OnPlayerSelectedMenuRow(int playerid, int row)
        {
            OnPlayerSelectedMenuRow(BasePlayer.FindOrCreate(playerid), new MenuRowEventArgs(row));

            return true;
        }

        internal bool OnPlayerExitedMenu(int playerid)
        {
            OnPlayerExitedMenu(BasePlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerInteriorChange(int playerid, int newinteriorid, int oldinteriorid)
        {
            OnPlayerInteriorChanged(BasePlayer.FindOrCreate(playerid),
                new InteriorChangedEventArgs(newinteriorid, oldinteriorid));

            return true;
        }

        internal bool OnPlayerKeyStateChange(int playerid, int newkeys, int oldkeys)
        {
            OnPlayerKeyStateChanged(BasePlayer.FindOrCreate(playerid),
                new KeyStateChangedEventArgs((Keys) newkeys, (Keys) oldkeys));

            return true;
        }

        internal bool OnRconLoginAttempt(string ip, string password, bool success)
        {
            OnRconLoginAttempt(new RconLoginAttemptEventArgs(ip, password, success));

            return true;
        }

        internal bool OnPlayerUpdate(int playerid)
        {
            var args = new PlayerUpdateEventArgs();

            OnPlayerUpdate(BasePlayer.FindOrCreate(playerid), args);

            return !args.PreventPropagation;
        }

        internal bool OnPlayerStreamIn(int playerid, int forplayerid)
        {
            OnPlayerStreamIn(BasePlayer.FindOrCreate(playerid), new PlayerEventArgs(BasePlayer.FindOrCreate(forplayerid)));

            return true;
        }

        internal bool OnPlayerStreamOut(int playerid, int forplayerid)
        {
            OnPlayerStreamOut(BasePlayer.FindOrCreate(playerid), new PlayerEventArgs(BasePlayer.FindOrCreate(forplayerid)));

            return true;
        }

        internal bool OnVehicleStreamIn(int vehicleid, int forplayerid)
        {
            OnVehicleStreamIn(BaseVehicle.FindOrCreate(vehicleid),
                new PlayerEventArgs(BasePlayer.FindOrCreate(forplayerid)));

            return true;
        }

        internal bool OnVehicleStreamOut(int vehicleid, int forplayerid)
        {
            OnVehicleStreamOut(BaseVehicle.FindOrCreate(vehicleid),
                new PlayerEventArgs(BasePlayer.FindOrCreate(forplayerid)));

            return true;
        }

        internal bool OnTrailerUpdate(int playerId, int vehicleId)
        {
            var args = new TrailerEventArgs(BasePlayer.FindOrCreate(playerId));
            OnTrailerUpdate(BaseVehicle.FindOrCreate(vehicleId), args);

            return !args.PreventPropagation;
        }

        internal bool OnDialogResponse(int playerid, int dialogid, int response, int listitem, string inputtext)
        {
            OnDialogResponse(BasePlayer.FindOrCreate(playerid),
                new DialogResponseEventArgs(BasePlayer.FindOrCreate(playerid), dialogid, response, listitem, inputtext));

            return true;
        }

        internal bool OnPlayerTakeDamage(int playerid, int issuerid, float amount, int weaponid, int bodypart)
        {
            OnPlayerTakeDamage(BasePlayer.FindOrCreate(playerid),
                new DamageEventArgs(issuerid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(issuerid),
                    amount, (Weapon) weaponid, (BodyPart) bodypart));

            return true;
        }

        internal bool OnPlayerGiveDamage(int playerid, int damagedid, float amount, int weaponid, int bodypart)
        {
            OnPlayerGiveDamage(BasePlayer.FindOrCreate(playerid),
                new DamageEventArgs(damagedid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(damagedid),
                    amount, (Weapon) weaponid, (BodyPart) bodypart));

            return true;
        }

        internal bool OnPlayerClickMap(int playerid, float fX, float fY, float fZ)
        {
            OnPlayerClickMap(BasePlayer.FindOrCreate(playerid), new PositionEventArgs(new Vector3(fX, fY, fZ)));

            return true;
        }

        internal bool OnPlayerClickTextDraw(int playerid, int clickedid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            OnPlayerClickTextDraw(player,
                new ClickTextDrawEventArgs(player,
                    clickedid == TextDraw.InvalidId ? null : TextDraw.FindOrCreate(clickedid)));

            return true;
        }

        internal bool OnPlayerClickPlayerTextDraw(int playerid, int playertextid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            OnPlayerClickPlayerTextDraw(player,
                new ClickPlayerTextDrawEventArgs(player, playertextid == PlayerTextDraw.InvalidId
                    ? null
                    : PlayerTextDraw.FindOrCreate(player, playertextid)));

            return true;
        }

        internal bool OnPlayerClickPlayer(int playerid, int clickedplayerid, int source)
        {
            OnPlayerClickPlayer(BasePlayer.FindOrCreate(playerid),
                new ClickPlayerEventArgs(
                    clickedplayerid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(clickedplayerid),
                    (PlayerClickSource) source));

            return true;
        }

        internal bool OnPlayerEditObject(int playerid, bool playerobject, int objectid, int response, float fX, float fY,
            float fZ, float fRotX, float fRotY, float fRotZ)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            if (playerobject)
            {
                OnPlayerEditPlayerObject(player,
                    new EditPlayerObjectEventArgs(player, PlayerObject.FindOrCreate(player, objectid),
                        (EditObjectResponse) response, new Vector3(fX, fY, fZ), new Vector3(fRotX, fRotY, fRotZ)));
            }
            else
            {
                OnPlayerEditGlobalObject(player,
                    new EditGlobalObjectEventArgs(player, GlobalObject.FindOrCreate(objectid),
                        (EditObjectResponse) response,
                        new Vector3(fX, fY, fZ), new Vector3(fRotX, fRotY, fRotZ)));
            }

            return true;
        }

        internal bool OnPlayerEditAttachedObject(int playerid, int response, int index, int modelid, int boneid,
            float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX,
            float fScaleY, float fScaleZ)
        {
            OnPlayerEditAttachedObject(BasePlayer.FindOrCreate(playerid),
                new EditAttachedObjectEventArgs((EditObjectResponse) response, index, modelid, (Bone) boneid,
                    new Vector3(fOffsetX, fOffsetY, fOffsetZ), new Vector3(fRotX, fRotY, fRotZ),
                    new Vector3(fScaleX, fScaleY, fScaleZ)));

            return true;
        }

        internal bool OnPlayerSelectObject(int playerid, int type, int objectid, int modelid, float fX, float fY,
            float fZ)
        {
            switch ((ObjectType) type)
            {
                case ObjectType.GlobalObject:
                    OnPlayerSelectGlobalObject(BasePlayer.FindOrCreate(playerid),
                        new SelectGlobalObjectEventArgs(BasePlayer.FindOrCreate(playerid),
                            GlobalObject.FindOrCreate(objectid), modelid,
                            new Vector3(fX, fY, fZ)));
                    break;
                case ObjectType.PlayerObject:
                    var player = BasePlayer.FindOrCreate(playerid);

                    OnPlayerSelectPlayerObject(player,
                        new SelectPlayerObjectEventArgs(BasePlayer.FindOrCreate(playerid),
                            PlayerObject.FindOrCreate(player, objectid), modelid,
                            new Vector3(fX, fY, fZ)));
                    break;
            }

            return true;
        }

        internal bool OnPlayerWeaponShot(int playerid, int weaponid, int hittype, int hitid, float fX, float fY,
            float fZ)
        {
            var args = new WeaponShotEventArgs((Weapon) weaponid, (BulletHitType) hittype, hitid,
                new Vector3(fX, fY, fZ));

            OnPlayerWeaponShot(BasePlayer.FindOrCreate(playerid), args);

            return !args.PreventDamage;
        }

        internal bool OnIncomingConnection(int playerid, string ipAddress, int port)
        {
            OnIncomingConnection(new ConnectionEventArgs(playerid, ipAddress, port));

            return true;
        }

        internal bool OnVehicleSirenStateChange(int playerid, int vehicleid, bool newstate)
        {
            OnVehicleSirenStateChange(BaseVehicle.FindOrCreate(vehicleid),
                new SirenStateEventArgs(BasePlayer.FindOrCreate(playerid), newstate));
            return true;
        }

        internal bool OnActorStreamIn(int actorid, int forplayerid)
        {
            OnActorStreamIn(Actor.FindOrCreate(actorid), new PlayerEventArgs(BasePlayer.FindOrCreate(forplayerid)));

            return true;
        }

        internal bool OnActorStreamOut(int actorid, int forplayerid)
        {
            OnActorStreamOut(Actor.FindOrCreate(actorid), new PlayerEventArgs(BasePlayer.FindOrCreate(forplayerid)));

            return true;
        }

        internal bool OnPlayerGiveDamageActor(int playerid, int damagedActorid, float amount, int weaponid, int bodypart)
        {
            OnPlayerGiveDamageActor(Actor.FindOrCreate(damagedActorid),
                new DamageEventArgs(BasePlayer.FindOrCreate(playerid), amount, (Weapon) weaponid, (BodyPart) bodypart));

            return true;
        }

        internal bool OnTick()
        {
            OnTick(EventArgs.Empty);

            return true;
        }

        internal bool OnCallbackException(Exception exception)
        {
            var args = new ExceptionEventArgs(exception);
            OnCallbackException(args);

            return args.Handled;
        }
    }
}