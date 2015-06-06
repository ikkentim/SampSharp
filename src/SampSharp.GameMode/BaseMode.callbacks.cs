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
            OnPlayerConnected(GtaPlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerDisconnect(int playerid, int reason)
        {
            var args = new DisconnectEventArgs((DisconnectReason) reason);

            OnPlayerDisconnected(GtaPlayer.FindOrCreate(playerid), args);
            OnPlayerCleanup(GtaPlayer.FindOrCreate(playerid), args);

            return true;
        }

        internal bool OnPlayerSpawn(int playerid)
        {
            var args = new SpawnEventArgs();

            OnPlayerSpawned(GtaPlayer.FindOrCreate(playerid), args);

            return !args.ReturnToClassSelection;
        }

        internal bool OnPlayerDeath(int playerid, int killerid, int reason)
        {
            OnPlayerDied(GtaPlayer.FindOrCreate(playerid),
                new DeathEventArgs(killerid == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(killerid),
                    (Weapon) reason));

            return true;
        }

        internal bool OnVehicleSpawn(int vehicleid)
        {
            OnVehicleSpawned(GtaVehicle.FindOrCreate(vehicleid), EventArgs.Empty);

            return true;
        }

        internal bool OnVehicleDeath(int vehicleid, int killerid)
        {
            OnVehicleDied(GtaVehicle.FindOrCreate(vehicleid),
                new PlayerEventArgs(killerid == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(killerid)));

            return true;
        }

        internal bool OnPlayerText(int playerid, string text)
        {
            var args = new TextEventArgs(text);

            OnPlayerText(GtaPlayer.FindOrCreate(playerid), args);

            return args.SendToPlayers;
        }

        internal bool OnPlayerCommandText(int playerid, string cmdtext)
        {
            var args = new CommandTextEventArgs(cmdtext);

            OnPlayerCommandText(GtaPlayer.FindOrCreate(playerid), args);

            return args.Success;
        }

        internal bool OnPlayerRequestClass(int playerid, int classid)
        {
            var args = new RequestClassEventArgs(classid);

            OnPlayerRequestClass(GtaPlayer.FindOrCreate(playerid), args);

            return !args.PreventSpawning;
        }

        internal bool OnPlayerEnterVehicle(int playerid, int vehicleid, bool ispassenger)
        {
            GtaPlayer player = GtaPlayer.FindOrCreate(playerid);
            OnPlayerEnterVehicle(player,
                new EnterVehicleEventArgs(player, GtaVehicle.FindOrCreate(vehicleid), ispassenger));

            return true;
        }

        internal bool OnPlayerExitVehicle(int playerid, int vehicleid)
        {
            GtaPlayer player = GtaPlayer.FindOrCreate(playerid);
            OnPlayerExitVehicle(player,
                new PlayerVehicleEventArgs(player, GtaVehicle.FindOrCreate(vehicleid)));

            return true;
        }

        internal bool OnPlayerStateChange(int playerid, int newstate, int oldstate)
        {
            OnPlayerStateChanged(GtaPlayer.FindOrCreate(playerid),
                new StateEventArgs((PlayerState) newstate, (PlayerState) oldstate));

            return true;
        }

        internal bool OnPlayerEnterCheckpoint(int playerid)
        {
            OnPlayerEnterCheckpoint(GtaPlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerLeaveCheckpoint(int playerid)
        {
            OnPlayerLeaveCheckpoint(GtaPlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerEnterRaceCheckpoint(int playerid)
        {
            OnPlayerEnterRaceCheckpoint(GtaPlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerLeaveRaceCheckpoint(int playerid)
        {
            OnPlayerLeaveRaceCheckpoint(GtaPlayer.FindOrCreate(playerid), EventArgs.Empty);

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

            OnPlayerRequestSpawn(GtaPlayer.FindOrCreate(playerid), args);

            return !args.PreventSpawning;
        }

        internal bool OnObjectMoved(int objectid)
        {
            OnObjectMoved(GlobalObject.FindOrCreate(objectid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerObjectMoved(int playerid, int objectid)
        {
            OnPlayerObjectMoved(PlayerObject.FindOrCreate(GtaPlayer.FindOrCreate(playerid), objectid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerPickUpPickup(int playerid, int pickupid)
        {
            OnPlayerPickUpPickup(Pickup.FindOrCreate(pickupid), new PlayerEventArgs(GtaPlayer.FindOrCreate(playerid)));

            return true;
        }

        internal bool OnVehicleMod(int playerid, int vehicleid, int componentid)
        {
            var args = new VehicleModEventArgs(GtaPlayer.FindOrCreate(playerid), componentid);

            OnVehicleMod(GtaVehicle.FindOrCreate(vehicleid), args);

            return !args.PreventPropagation;
        }

        internal bool OnEnterExitModShop(int playerid, int enterexit, int interiorid)
        {
            OnPlayerEnterExitModShop(GtaPlayer.FindOrCreate(playerid),
                new EnterModShopEventArgs((EnterExit) enterexit, interiorid));

            return true;
        }

        internal bool OnVehiclePaintjob(int playerid, int vehicleid, int paintjobid)
        {
            OnVehiclePaintjobApplied(GtaVehicle.FindOrCreate(vehicleid),
                new VehiclePaintjobEventArgs(GtaPlayer.FindOrCreate(playerid), paintjobid));


            return true;
        }

        internal bool OnVehicleRespray(int playerid, int vehicleid, int color1, int color2)
        {
            OnVehicleResprayed(GtaVehicle.FindOrCreate(vehicleid),
                new VehicleResprayedEventArgs(GtaPlayer.FindOrCreate(playerid), color1, color2));

            return true;
        }

        internal bool OnVehicleDamageStatusUpdate(int vehicleid, int playerid)
        {
            OnVehicleDamageStatusUpdated(GtaVehicle.FindOrCreate(vehicleid),
                new PlayerEventArgs(GtaPlayer.FindOrCreate(playerid)));

            return true;
        }

        internal bool OnUnoccupiedVehicleUpdate(int vehicleid, int playerid, int passengerSeat, float newX,
            float newY, float newZ, float velX, float velY, float velZ)
        {
            var args = new UnoccupiedVehicleEventArgs(GtaPlayer.FindOrCreate(playerid), passengerSeat,
                new Vector(newX, newY, newZ), new Vector(velX, velY, velZ));
            OnUnoccupiedVehicleUpdated(GtaVehicle.FindOrCreate(vehicleid), args);

            return !args.PreventPropagation;
        }

        internal bool OnPlayerSelectedMenuRow(int playerid, int row)
        {
            OnPlayerSelectedMenuRow(GtaPlayer.FindOrCreate(playerid), new MenuRowEventArgs(row));

            return true;
        }

        internal bool OnPlayerExitedMenu(int playerid)
        {
            OnPlayerExitedMenu(GtaPlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        internal bool OnPlayerInteriorChange(int playerid, int newinteriorid, int oldinteriorid)
        {
            OnPlayerInteriorChanged(GtaPlayer.FindOrCreate(playerid),
                new InteriorChangedEventArgs(newinteriorid, oldinteriorid));

            return true;
        }

        internal bool OnPlayerKeyStateChange(int playerid, int newkeys, int oldkeys)
        {
            OnPlayerKeyStateChanged(GtaPlayer.FindOrCreate(playerid),
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

            OnPlayerUpdate(GtaPlayer.FindOrCreate(playerid), args);

            return !args.PreventPropagation;
        }

        internal bool OnPlayerStreamIn(int playerid, int forplayerid)
        {
            OnPlayerStreamIn(GtaPlayer.FindOrCreate(playerid), new PlayerEventArgs(GtaPlayer.FindOrCreate(forplayerid)));

            return true;
        }

        internal bool OnPlayerStreamOut(int playerid, int forplayerid)
        {
            OnPlayerStreamOut(GtaPlayer.FindOrCreate(playerid), new PlayerEventArgs(GtaPlayer.FindOrCreate(forplayerid)));

            return true;
        }

        internal bool OnVehicleStreamIn(int vehicleid, int forplayerid)
        {
            OnVehicleStreamIn(GtaVehicle.FindOrCreate(vehicleid),
                new PlayerEventArgs(GtaPlayer.FindOrCreate(forplayerid)));

            return true;
        }

        internal bool OnVehicleStreamOut(int vehicleid, int forplayerid)
        {
            OnVehicleStreamOut(GtaVehicle.FindOrCreate(vehicleid),
                new PlayerEventArgs(GtaPlayer.FindOrCreate(forplayerid)));

            return true;
        }

        internal bool OnTrailerUpdate(int playerId, int vehicleId)
        {
            var args = new TrailerEventArgs(GtaPlayer.FindOrCreate(playerId));
            OnTrailerUpdate(GtaVehicle.FindOrCreate(vehicleId), args);

            return !args.PreventPropagation;
        }

        internal bool OnDialogResponse(int playerid, int dialogid, int response, int listitem, string inputtext)
        {
            OnDialogResponse(GtaPlayer.FindOrCreate(playerid),
                new DialogResponseEventArgs(GtaPlayer.FindOrCreate(playerid), dialogid, response, listitem, inputtext));

            return true;
        }

        internal bool OnPlayerTakeDamage(int playerid, int issuerid, float amount, int weaponid, int bodypart)
        {
            OnPlayerTakeDamage(GtaPlayer.FindOrCreate(playerid),
                new DamageEventArgs(issuerid == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(issuerid),
                    amount, (Weapon) weaponid, (BodyPart) bodypart));

            return true;
        }

        internal bool OnPlayerGiveDamage(int playerid, int damagedid, float amount, int weaponid, int bodypart)
        {
            OnPlayerGiveDamage(GtaPlayer.FindOrCreate(playerid),
                new DamageEventArgs(damagedid == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(damagedid),
                    amount, (Weapon) weaponid, (BodyPart) bodypart));

            return true;
        }

        internal bool OnPlayerClickMap(int playerid, float fX, float fY, float fZ)
        {
            OnPlayerClickMap(GtaPlayer.FindOrCreate(playerid), new PositionEventArgs(new Vector(fX, fY, fZ)));

            return true;
        }

        internal bool OnPlayerClickTextDraw(int playerid, int clickedid)
        {
            GtaPlayer player = GtaPlayer.FindOrCreate(playerid);
            OnPlayerClickTextDraw(player,
                new ClickTextDrawEventArgs(player,
                    clickedid == TextDraw.InvalidId ? null : TextDraw.FindOrCreate(clickedid)));

            return true;
        }

        internal bool OnPlayerClickPlayerTextDraw(int playerid, int playertextid)
        {
            GtaPlayer player = GtaPlayer.FindOrCreate(playerid);
            OnPlayerClickPlayerTextDraw(player,
                new ClickPlayerTextDrawEventArgs(player, playertextid == PlayerTextDraw.InvalidId
                    ? null
                    : PlayerTextDraw.FindOrCreate(player, playertextid)));

            return true;
        }

        internal bool OnPlayerClickPlayer(int playerid, int clickedplayerid, int source)
        {
            OnPlayerClickPlayer(GtaPlayer.FindOrCreate(playerid),
                new ClickPlayerEventArgs(
                    clickedplayerid == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(clickedplayerid),
                    (PlayerClickSource) source));

            return true;
        }

        internal bool OnPlayerEditObject(int playerid, bool playerobject, int objectid, int response, float fX, float fY,
            float fZ, float fRotX, float fRotY, float fRotZ)
        {
            GtaPlayer player = GtaPlayer.FindOrCreate(playerid);
            if (playerobject)
            {
                OnPlayerEditPlayerObject(player,
                    new EditPlayerObjectEventArgs(player, PlayerObject.FindOrCreate(player, objectid),
                        (EditObjectResponse) response, new Vector(fX, fY, fZ), new Vector(fRotX, fRotY, fRotZ)));
            }
            else
            {
                OnPlayerEditGlobalObject(player,
                    new EditGlobalObjectEventArgs(player, GlobalObject.FindOrCreate(objectid),
                        (EditObjectResponse) response,
                        new Vector(fX, fY, fZ), new Vector(fRotX, fRotY, fRotZ)));
            }

            return true;
        }

        internal bool OnPlayerEditAttachedObject(int playerid, int response, int index, int modelid, int boneid,
            float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX,
            float fScaleY, float fScaleZ)
        {
            OnPlayerEditAttachedObject(GtaPlayer.FindOrCreate(playerid),
                new EditAttachedObjectEventArgs((EditObjectResponse) response, index, modelid, (Bone) boneid,
                    new Vector(fOffsetX, fOffsetY, fOffsetZ), new Vector(fRotX, fRotY, fRotZ),
                    new Vector(fScaleX, fScaleY, fScaleZ)));

            return true;
        }

        internal bool OnPlayerSelectObject(int playerid, int type, int objectid, int modelid, float fX, float fY,
            float fZ)
        {
            switch ((ObjectType) type)
            {
                case ObjectType.GlobalObject:
                    OnPlayerSelectGlobalObject(GtaPlayer.FindOrCreate(playerid),
                        new SelectGlobalObjectEventArgs(GtaPlayer.FindOrCreate(playerid),
                            GlobalObject.FindOrCreate(objectid), modelid,
                            new Vector(fX, fY, fZ)));
                    break;
                case ObjectType.PlayerObject:
                    GtaPlayer player = GtaPlayer.FindOrCreate(playerid);

                    OnPlayerSelectPlayerObject(player,
                        new SelectPlayerObjectEventArgs(GtaPlayer.FindOrCreate(playerid),
                            PlayerObject.FindOrCreate(player, objectid), modelid,
                            new Vector(fX, fY, fZ)));
                    break;
            }

            return true;
        }

        internal bool OnPlayerWeaponShot(int playerid, int weaponid, int hittype, int hitid, float fX, float fY,
            float fZ)
        {
            var args = new WeaponShotEventArgs((Weapon) weaponid, (BulletHitType) hittype, hitid, new Vector(fX, fY, fZ));

            OnPlayerWeaponShot(GtaPlayer.FindOrCreate(playerid), args);

            return !args.PreventDamage;
        }

        internal bool OnIncomingConnection(int playerid, string ipAddress, int port)
        {
            OnIncomingConnection(new ConnectionEventArgs(playerid, ipAddress, port));

            return true;
        }

        internal bool OnVehicleSirenStateChange(int playerid, int vehicleid, bool newstate)
        {
            OnVehicleSirenStateChange(GtaVehicle.FindOrCreate(vehicleid),
                new SirenStateEventArgs(GtaPlayer.FindOrCreate(playerid), newstate));
            return true;
        }

        internal bool OnActorStreamIn(int actorid, int forplayerid)
        {
            OnActorStreamIn(Actor.FindOrCreate(actorid), new PlayerEventArgs(GtaPlayer.FindOrCreate(forplayerid)));

            return true;
        }

        internal bool OnActorStreamOut(int actorid, int forplayerid)
        {
            OnActorStreamOut(Actor.FindOrCreate(actorid), new PlayerEventArgs(GtaPlayer.FindOrCreate(forplayerid)));

            return true;
        }

        internal bool OnPlayerGiveDamageActor(int playerid, int damagedActorid, float amount, int weaponid, int bodypart)
        {
            OnPlayerGiveDamageActor(Actor.FindOrCreate(damagedActorid),
                new DamageEventArgs(GtaPlayer.FindOrCreate(playerid), amount, (Weapon) weaponid, (BodyPart) bodypart));

            return true;
        }

        internal bool OnTick()
        {
            OnTick(EventArgs.Empty);

            return true;
        }
    }
}
