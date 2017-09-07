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
using SampSharp.Core.Callbacks;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode
{
    public abstract partial class BaseMode
    {
        [Callback]
        internal bool OnGameModeInit()
        {
            OnInitialized(EventArgs.Empty);

            return true;
        }

        [Callback]
        internal bool OnGameModeExit()
        {
            OnExited(EventArgs.Empty);

            return true;
        }

        [Callback]
        internal bool OnPlayerConnect(int playerid)
        {
            OnPlayerConnected(BasePlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        [Callback]
        internal bool OnPlayerDisconnect(int playerid, int reason)
        {
            var args = new DisconnectEventArgs((DisconnectReason) reason);

            OnPlayerDisconnected(BasePlayer.FindOrCreate(playerid), args);
            OnPlayerCleanup(BasePlayer.FindOrCreate(playerid), args);

            return true;
        }

        [Callback]
        internal bool OnPlayerSpawn(int playerid)
        {
            var args = new SpawnEventArgs();

            OnPlayerSpawned(BasePlayer.FindOrCreate(playerid), args);

            return !args.ReturnToClassSelection;
        }

        [Callback]
        internal bool OnPlayerDeath(int playerid, int killerid, int reason)
        {
            OnPlayerDied(BasePlayer.FindOrCreate(playerid),
                new DeathEventArgs(killerid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(killerid),
                    (Weapon) reason));

            return true;
        }

        [Callback]
        internal bool OnVehicleSpawn(int vehicleid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            OnVehicleSpawned(vehicle, EventArgs.Empty);

            return true;
        }

        [Callback]
        internal bool OnVehicleDeath(int vehicleid, int killerid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            OnVehicleDied(vehicle, new PlayerEventArgs(killerid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(killerid)));

            return true;
        }

        [Callback]
        internal bool OnPlayerText(int playerid, string text)
        {
            var args = new TextEventArgs(text);

            OnPlayerText(BasePlayer.FindOrCreate(playerid), args);

            return args.SendToPlayers;
        }

        [Callback]
        internal bool OnPlayerCommandText(int playerid, string cmdtext)
        {
            var args = new CommandTextEventArgs(cmdtext);

            OnPlayerCommandText(BasePlayer.FindOrCreate(playerid), args);

            return args.Success;
        }

        [Callback]
        internal bool OnPlayerRequestClass(int playerid, int classid)
        {
            var args = new RequestClassEventArgs(classid);

            OnPlayerRequestClass(BasePlayer.FindOrCreate(playerid), args);

            return !args.PreventSpawning;
        }

        [Callback]
        internal bool OnPlayerEnterVehicle(int playerid, int vehicleid, bool ispassenger)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            var player = BasePlayer.FindOrCreate(playerid);
            OnPlayerEnterVehicle(player, new EnterVehicleEventArgs(player, vehicle, ispassenger));

            return true;
        }

        [Callback]
        internal bool OnPlayerExitVehicle(int playerid, int vehicleid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            var player = BasePlayer.FindOrCreate(playerid);
            OnPlayerExitVehicle(player, new PlayerVehicleEventArgs(player, vehicle));

            return true;
        }

        [Callback]
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

        [Callback]
        internal bool OnPlayerLeaveCheckpoint(int playerid)
        {
            OnPlayerLeaveCheckpoint(BasePlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        [Callback]
        internal bool OnPlayerEnterRaceCheckpoint(int playerid)
        {
            OnPlayerEnterRaceCheckpoint(BasePlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        [Callback]
        internal bool OnPlayerLeaveRaceCheckpoint(int playerid)
        {
            OnPlayerLeaveRaceCheckpoint(BasePlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        [Callback]
        internal bool OnRconCommand(string command)
        {
            var args = new RconEventArgs(command);
            OnRconCommand(args);

            return args.Success;
        }

        [Callback]
        internal bool OnPlayerRequestSpawn(int playerid)
        {
            var args = new RequestSpawnEventArgs();

            OnPlayerRequestSpawn(BasePlayer.FindOrCreate(playerid), args);

            return !args.PreventSpawning;
        }

        [Callback]
        internal bool OnObjectMoved(int objectid)
        {
            var @object = GlobalObject.Find(objectid);

            if (@object == null)
                return true;

            OnObjectMoved(@object, EventArgs.Empty);

            return true;
        }

        [Callback]
        internal bool OnPlayerObjectMoved(int playerid, int objectid)
        {
            var @object = PlayerObject.Find(BasePlayer.FindOrCreate(playerid), objectid);

            if (@object == null)
                return true;

            OnPlayerObjectMoved(@object, EventArgs.Empty);

            return true;
        }

        [Callback]
        internal bool OnPlayerPickUpPickup(int playerid, int pickupid)
        {
            var pickup = Pickup.Find(pickupid);

            if (pickup == null)
                return true;

            OnPlayerPickUpPickup(pickup, new PlayerEventArgs(BasePlayer.FindOrCreate(playerid)));

            return true;
        }

        [Callback]
        internal bool OnVehicleMod(int playerid, int vehicleid, int componentid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            var args = new VehicleModEventArgs(BasePlayer.FindOrCreate(playerid), componentid);

            OnVehicleMod(vehicle, args);

            return !args.PreventPropagation;
        }

        [Callback]
        internal bool OnEnterExitModShop(int playerid, int enterexit, int interiorid)
        {
            OnPlayerEnterExitModShop(BasePlayer.FindOrCreate(playerid),
                new EnterModShopEventArgs((EnterExit) enterexit, interiorid));

            return true;
        }

        [Callback]
        internal bool OnVehiclePaintjob(int playerid, int vehicleid, int paintjobid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            OnVehiclePaintjobApplied(vehicle,
                new VehiclePaintjobEventArgs(BasePlayer.FindOrCreate(playerid), paintjobid));


            return true;
        }

        [Callback]
        internal bool OnVehicleRespray(int playerid, int vehicleid, int color1, int color2)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            OnVehicleResprayed(vehicle, new VehicleResprayedEventArgs(BasePlayer.FindOrCreate(playerid), color1, color2));

            return true;
        }

        [Callback]
        internal bool OnVehicleDamageStatusUpdate(int vehicleid, int playerid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            OnVehicleDamageStatusUpdated(vehicle, new PlayerEventArgs(BasePlayer.FindOrCreate(playerid)));

            return true;
        }

        [Callback]
        internal bool OnUnoccupiedVehicleUpdate(int vehicleid, int playerid, int passengerSeat, float newX,
            float newY, float newZ, float velX, float velY, float velZ)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            var args = new UnoccupiedVehicleEventArgs(BasePlayer.FindOrCreate(playerid), passengerSeat,
                new Vector3(newX, newY, newZ), new Vector3(velX, velY, velZ));
            OnUnoccupiedVehicleUpdated(vehicle, args);

            return !args.PreventPropagation;
        }

        [Callback]
        internal bool OnPlayerSelectedMenuRow(int playerid, int row)
        {
            OnPlayerSelectedMenuRow(BasePlayer.FindOrCreate(playerid), new MenuRowEventArgs(row));

            return true;
        }

        [Callback]
        internal bool OnPlayerExitedMenu(int playerid)
        {
            OnPlayerExitedMenu(BasePlayer.FindOrCreate(playerid), EventArgs.Empty);

            return true;
        }

        [Callback]
        internal bool OnPlayerInteriorChange(int playerid, int newinteriorid, int oldinteriorid)
        {
            OnPlayerInteriorChanged(BasePlayer.FindOrCreate(playerid),
                new InteriorChangedEventArgs(newinteriorid, oldinteriorid));

            return true;
        }

        [Callback]
        internal bool OnPlayerKeyStateChange(int playerid, int newkeys, int oldkeys)
        {
            OnPlayerKeyStateChanged(BasePlayer.FindOrCreate(playerid),
                new KeyStateChangedEventArgs((Keys) newkeys, (Keys) oldkeys));

            return true;
        }

        [Callback]
        internal bool OnRconLoginAttempt(string ip, string password, bool success)
        {
            OnRconLoginAttempt(new RconLoginAttemptEventArgs(ip, password, success));

            return true;
        }

        [Callback]
        internal bool OnPlayerUpdate(int playerid)
        {
            var args = new PlayerUpdateEventArgs();

            OnPlayerUpdate(BasePlayer.FindOrCreate(playerid), args);

            return !args.PreventPropagation;
        }

        [Callback]
        internal bool OnPlayerStreamIn(int playerid, int forplayerid)
        {
            OnPlayerStreamIn(BasePlayer.FindOrCreate(playerid),
                new PlayerEventArgs(BasePlayer.FindOrCreate(forplayerid)));

            return true;
        }

        [Callback]
        internal bool OnPlayerStreamOut(int playerid, int forplayerid)
        {
            OnPlayerStreamOut(BasePlayer.FindOrCreate(playerid),
                new PlayerEventArgs(BasePlayer.FindOrCreate(forplayerid)));

            return true;
        }

        [Callback]
        internal bool OnVehicleStreamIn(int vehicleid, int forplayerid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            OnVehicleStreamIn(vehicle, new PlayerEventArgs(BasePlayer.FindOrCreate(forplayerid)));

            return true;
        }

        [Callback]
        internal bool OnVehicleStreamOut(int vehicleid, int forplayerid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            OnVehicleStreamOut(vehicle, new PlayerEventArgs(BasePlayer.FindOrCreate(forplayerid)));

            return true;
        }

        [Callback]
        internal bool OnTrailerUpdate(int playerId, int vehicleid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            var args = new TrailerEventArgs(BasePlayer.FindOrCreate(playerId));
            OnTrailerUpdate(vehicle, args);

            return !args.PreventPropagation;
        }

        [Callback]
        internal bool OnDialogResponse(int playerid, int dialogid, int response, int listitem, string inputtext)
        {
            OnDialogResponse(BasePlayer.FindOrCreate(playerid),
                new DialogResponseEventArgs(BasePlayer.FindOrCreate(playerid), dialogid, response, listitem, inputtext));

            return true;
        }

        [Callback]
        internal bool OnPlayerTakeDamage(int playerid, int issuerid, float amount, int weaponid, int bodypart)
        {
            OnPlayerTakeDamage(BasePlayer.FindOrCreate(playerid),
                new DamageEventArgs(issuerid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(issuerid),
                    amount, (Weapon) weaponid, (BodyPart) bodypart));

            return true;
        }

        [Callback]
        internal bool OnPlayerGiveDamage(int playerid, int damagedid, float amount, int weaponid, int bodypart)
        {
            OnPlayerGiveDamage(BasePlayer.FindOrCreate(playerid),
                new DamageEventArgs(damagedid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(damagedid),
                    amount, (Weapon) weaponid, (BodyPart) bodypart));

            return true;
        }

        [Callback]
        internal bool OnPlayerClickMap(int playerid, float fX, float fY, float fZ)
        {
            OnPlayerClickMap(BasePlayer.FindOrCreate(playerid), new PositionEventArgs(new Vector3(fX, fY, fZ)));

            return true;
        }

        [Callback]
        internal bool OnPlayerClickTextDraw(int playerid, int clickedid)
        {
            var clicked = clickedid == TextDraw.InvalidId ? null : TextDraw.Find(clickedid);

            if (clickedid != TextDraw.InvalidId && clicked == null)
                return true;

            var player = BasePlayer.FindOrCreate(playerid);
            OnPlayerClickTextDraw(player, new ClickTextDrawEventArgs(player, clicked));

            return true;
        }

        [Callback]
        internal bool OnPlayerClickPlayerTextDraw(int playerid, int playertextid)
        {
            var player = BasePlayer.FindOrCreate(playerid);

            var clicked = playertextid == PlayerTextDraw.InvalidId ? null : PlayerTextDraw.Find(player, playertextid);

            if (playertextid != TextDraw.InvalidId && clicked == null)
                return true;

            OnPlayerClickPlayerTextDraw(player, new ClickPlayerTextDrawEventArgs(player, clicked));

            return true;
        }

        [Callback]
        internal bool OnPlayerClickPlayer(int playerid, int clickedplayerid, int source)
        {
            OnPlayerClickPlayer(BasePlayer.FindOrCreate(playerid),
                new ClickPlayerEventArgs(
                    clickedplayerid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(clickedplayerid),
                    (PlayerClickSource) source));

            return true;
        }

        [Callback]
        internal bool OnPlayerEditObject(int playerid, bool playerobject, int objectid, int response, float fX, float fY,
            float fZ, float fRotX, float fRotY, float fRotZ)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            if (playerobject)
            {
                var @object = PlayerObject.FindOrCreate(player, objectid);

                if (@object == null)
                    return true;

                OnPlayerEditPlayerObject(player,
                    new EditPlayerObjectEventArgs(player, @object, (EditObjectResponse) response,
                        new Vector3(fX, fY, fZ), new Vector3(fRotX, fRotY, fRotZ)));
            }
            else
            {
                var @object = GlobalObject.FindOrCreate(objectid);

                if (@object == null)
                    return true;

                OnPlayerEditGlobalObject(player,
                    new EditGlobalObjectEventArgs(player, @object, (EditObjectResponse) response,
                        new Vector3(fX, fY, fZ), new Vector3(fRotX, fRotY, fRotZ)));
            }

            return true;
        }

        [Callback]
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

        [Callback]
        internal bool OnPlayerSelectObject(int playerid, int type, int objectid, int modelid, float fX, float fY,
            float fZ)
        {
            switch ((ObjectType) type)
            {
                case ObjectType.GlobalObject:
                {
                    var @object = GlobalObject.FindOrCreate(objectid);

                    if (@object == null)
                        return true;

                        var player = BasePlayer.FindOrCreate(playerid);

                        OnPlayerSelectGlobalObject(player,     new SelectGlobalObjectEventArgs(player, @object, modelid,  new Vector3(fX, fY, fZ)));
                    break;
                }
                case ObjectType.PlayerObject:
                    {
                        var player = BasePlayer.FindOrCreate(playerid);

                        var @object = PlayerObject.FindOrCreate(player, objectid);

                    if (@object == null)
                        return true;

                    OnPlayerSelectPlayerObject(player,  new SelectPlayerObjectEventArgs(player, @object, modelid,  new Vector3(fX, fY, fZ)));
                    break;
                }
            }

            return true;
        }

        [Callback]
        internal bool OnPlayerWeaponShot(int playerid, int weaponid, int hittype, int hitid, float fX, float fY,
            float fZ)
        {
            var args = new WeaponShotEventArgs((Weapon) weaponid, (BulletHitType) hittype, hitid,
                new Vector3(fX, fY, fZ));

            OnPlayerWeaponShot(BasePlayer.FindOrCreate(playerid), args);

            return !args.PreventDamage;
        }

        [Callback]
        internal bool OnIncomingConnection(int playerid, string ipAddress, int port)
        {
            OnIncomingConnection(new ConnectionEventArgs(playerid, ipAddress, port));

            return true;
        }

        [Callback]
        internal bool OnVehicleSirenStateChange(int playerid, int vehicleid, bool newstate)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            OnVehicleSirenStateChange(vehicle, new SirenStateEventArgs(BasePlayer.FindOrCreate(playerid), newstate));

            return true;
        }

        [Callback]
        internal bool OnActorStreamIn(int actorid, int forplayerid)
        {
            var actor = Actor.Find(actorid);

            if (actor == null)
                return true;

            OnActorStreamIn(actor, new PlayerEventArgs(BasePlayer.FindOrCreate(forplayerid)));

            return true;
        }

        [Callback]
        internal bool OnActorStreamOut(int actorid, int forplayerid)
        {
            var actor = Actor.Find(actorid);

            if (actor == null)
                return true;

            OnActorStreamOut(actor, new PlayerEventArgs(BasePlayer.FindOrCreate(forplayerid)));

            return true;
        }

        [Callback]
        internal bool OnPlayerGiveDamageActor(int playerid, int damagedActorid, float amount, int weaponid, int bodypart)
        {
            var actor = Actor.Find(damagedActorid);

            if (actor == null)
                return true;

            OnPlayerGiveDamageActor(actor, new DamageEventArgs(BasePlayer.FindOrCreate(playerid), amount, (Weapon) weaponid, (BodyPart) bodypart));

            return true;
        }
    }
}