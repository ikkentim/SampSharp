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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
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
            OnPlayerConnected(new PlayerConnectEventArgs(BasePlayer.FindOrCreate(playerid)));

            return true;
        }

        [Callback]
        internal bool OnPlayerDisconnect(int playerid, int reason)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new PlayerDisconnectEventArgs(player, (DisconnectReason) reason);

            OnPlayerDisconnected(args);
            OnPlayerCleanup(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerSpawn(int playerid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new SpawnEventArgs(player);

            OnPlayerSpawned(args);

            return !args.ReturnToClassSelection;
        }

        [Callback]
        internal bool OnPlayerDeath(int playerid, int killerid, int reason)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var killer = killerid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(killerid);
            var args = new DeathEventArgs(player, killer, (Weapon) reason);
           
            OnPlayerDied(args);

            return true;
        }

        [Callback]
        internal bool OnVehicleSpawn(int vehicleid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;
            
            var args = new VehicleSpawnedEventArgs(vehicle);

            OnVehicleSpawned(args);

            return true;
        }

        [Callback]
        internal bool OnVehicleDeath(int vehicleid, int killerid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            var killer = killerid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(killerid);
            var args = new VehicleDiedEventArgs(killer, vehicle);

            OnVehicleDied(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerText(int playerid, string text)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new TextEventArgs(player, text);

            OnPlayerText(args);

            return args.SendToPlayers;
        }

        [Callback]
        internal bool OnPlayerCommandText(int playerid, string cmdtext)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new CommandTextEventArgs(player, cmdtext);

            OnPlayerCommandText(args);

            return args.Success;
        }

        [Callback]
        internal bool OnPlayerRequestClass(int playerid, int classid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new RequestClassEventArgs(player, classid);

            OnPlayerRequestClass(args);

            return !args.PreventSpawning;
        }

        [Callback]
        internal bool OnPlayerEnterVehicle(int playerid, int vehicleid, bool ispassenger)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            var player = BasePlayer.FindOrCreate(playerid);
            var args = new EnterVehicleEventArgs(player, vehicle, ispassenger);
            
            OnPlayerEnterVehicle(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerExitVehicle(int playerid, int vehicleid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            var player = BasePlayer.FindOrCreate(playerid);
            var isPassenger = (vehicle.Driver?.Id ?? -1) != player.Id;
            var args = new ExitVehicleEventArgs(player, vehicle, isPassenger);
            
            OnPlayerExitVehicle(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerStateChange(int playerid, int newstate, int oldstate)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new StateEventArgs(player, (PlayerState) newstate, (PlayerState) oldstate);
            
            OnPlayerStateChanged(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerEnterCheckpoint(int playerid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new PlayerEnterCheckpointEventArgs(player);
            
            OnPlayerEnterCheckpoint(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerLeaveCheckpoint(int playerid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new PlayerLeaveCheckpointEventArgs(player);
            
            OnPlayerLeaveCheckpoint(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerEnterRaceCheckpoint(int playerid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new PlayerEnterRaceCheckpointEventArgs(player);
            
            OnPlayerEnterRaceCheckpoint(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerLeaveRaceCheckpoint(int playerid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new PlayerLeaveRaceCheckpointEventArgs(player);

            OnPlayerLeaveRaceCheckpoint(args);

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
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new RequestSpawnEventArgs(player);

            OnPlayerRequestSpawn(args);

            return !args.PreventSpawning;
        }

        [Callback]
        internal bool OnObjectMoved(int objectid)
        {
            var @object = GlobalObject.Find(objectid);

            if (@object == null)
                return true;
          
            var args = new ObjectMovedEventArgs(@object);
            
            OnObjectMoved(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerObjectMoved(int playerid, int objectid)
        {
            var @object = PlayerObject.Find(BasePlayer.FindOrCreate(playerid), objectid);

            if (@object == null)
                return true;
            
            var args = new PlayerObjectEventArgs(@object);

            OnPlayerObjectMoved(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerPickUpPickup(int playerid, int pickupid)
        {
            var pickup = Pickup.Find(pickupid);

            if (pickup == null)
                return true;

            var player = BasePlayer.FindOrCreate(playerid);
            var args = new PlayerPickUpPickupEventArgs(player, pickup);

            OnPlayerPickUpPickup(args);

            return true;
        }

        [Callback]
        internal bool OnVehicleMod(int playerid, int vehicleid, int componentid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            var player = BasePlayer.FindOrCreate(playerid);
            var args = new VehicleModEventArgs(player, vehicle, componentid);

            OnVehicleMod(args);

            return !args.PreventPropagation;
        }

        [Callback]
        internal bool OnEnterExitModShop(int playerid, int enterexit, int interiorid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new EnterModShopEventArgs(player, (EnterExit) enterexit, interiorid);
            
            OnPlayerEnterExitModShop(args);

            return true;
        }

        [Callback]
        internal bool OnVehiclePaintjob(int playerid, int vehicleid, int paintjobid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            var player = BasePlayer.FindOrCreate(playerid);
            var args = new VehiclePaintjobEventArgs(player, vehicle, paintjobid);

            OnVehiclePaintjobApplied(args);


            return true;
        }

        [Callback]
        internal bool OnVehicleRespray(int playerid, int vehicleid, int color1, int color2)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            var player = BasePlayer.FindOrCreate(playerid);
            var args = new VehicleResprayedEventArgs(player, vehicle, color1, color2);

            OnVehicleResprayed(args);

            return true;
        }

        [Callback]
        internal bool OnVehicleDamageStatusUpdate(int vehicleid, int playerid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;

            var player = BasePlayer.FindOrCreate(playerid);
            var args = new VehicleDamageStatusUpdatedEventArgs(player, vehicle);

            OnVehicleDamageStatusUpdated(args);

            return true;
        }

        [Callback]
        internal bool OnUnoccupiedVehicleUpdate(int vehicleid, int playerid, int passengerSeat, float newX,
            float newY, float newZ, float velX, float velY, float velZ)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;
            
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new UnoccupiedVehicleEventArgs(player, vehicle, passengerSeat, new Vector3(newX, newY, newZ), new Vector3(velX, velY, velZ));
            
            OnUnoccupiedVehicleUpdated(args);

            return !args.PreventPropagation;
        }

        [Callback]
        internal bool OnPlayerSelectedMenuRow(int playerid, int row)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new MenuRowEventArgs(player, row);
            
            OnPlayerSelectedMenuRow(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerExitedMenu(int playerid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new PlayerExitMenuEventArgs(player);

            OnPlayerExitedMenu(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerInteriorChange(int playerid, int newinteriorid, int oldinteriorid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new InteriorChangedEventArgs(player, newinteriorid, oldinteriorid);

            OnPlayerInteriorChanged(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerKeyStateChange(int playerid, int newkeys, int oldkeys)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new KeyStateChangedEventArgs(player, (Keys) newkeys, (Keys) oldkeys);

            OnPlayerKeyStateChanged(args);

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
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new PlayerUpdateEventArgs(player);

            OnPlayerUpdate(args);

            return !args.PreventPropagation;
        }

        [Callback]
        internal bool OnPlayerStreamIn(int playerid, int forplayerid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var target = BasePlayer.FindOrCreate(forplayerid);
            var args = new PlayerStreamEventArgs(player, target, StreamMode.In);
            
            OnPlayerStreamIn(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerStreamOut(int playerid, int forplayerid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var target = BasePlayer.FindOrCreate(forplayerid);
            var args = new PlayerStreamEventArgs(player, target, StreamMode.Out);

            OnPlayerStreamOut(args);

            return true;
        }

        [Callback]
        internal bool OnVehicleStreamIn(int vehicleid, int forplayerid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;
            
            var player = BasePlayer.FindOrCreate(forplayerid);
            var args = new VehicleStreamEventArgs(player, vehicle, StreamMode.In);

            OnVehicleStreamIn(args);

            return true;
        }

        [Callback]
        internal bool OnVehicleStreamOut(int vehicleid, int forplayerid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;
            
            var player = BasePlayer.FindOrCreate(forplayerid);
            var args = new VehicleStreamEventArgs(player, vehicle, StreamMode.Out);

            OnVehicleStreamOut(args);

            return true;
        }

        [Callback]
        internal bool OnTrailerUpdate(int playerId, int vehicleid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);

            if (vehicle == null)
                return true;
            
            var player = BasePlayer.FindOrCreate(playerId);
            var args = new TrailerEventArgs(player, vehicle);
            
            OnTrailerUpdate(args);

            return !args.PreventPropagation;
        }

        [Callback]
        internal bool OnDialogResponse(int playerid, int dialogid, int response, int listitem, string inputtext)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new DialogResponseEventArgs(player, dialogid, response, listitem, inputtext);
            
            OnDialogResponse(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerTakeDamage(int playerid, int issuerid, float amount, int weaponid, int bodypart)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var issuer = issuerid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(issuerid);
            var args = new DamageEventArgs(player, issuer, amount, (Weapon) weaponid, (BodyPart) bodypart);
            
            OnPlayerTakeDamage(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerGiveDamage(int playerid, int damagedid, float amount, int weaponid, int bodypart)
        {
            var player = damagedid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(playerid);
            var issuer = BasePlayer.FindOrCreate(damagedid);
            var args = new DamageEventArgs(player, issuer, amount, (Weapon) weaponid, (BodyPart) bodypart);

            OnPlayerGiveDamage(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerClickMap(int playerid, float fX, float fY, float fZ)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new PositionEventArgs(player, new Vector3(fX, fY, fZ));

            OnPlayerClickMap(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerClickTextDraw(int playerid, int clickedid)
        {
            var clicked = clickedid == TextDraw.InvalidId ? null : TextDraw.Find(clickedid);

            if (clickedid != TextDraw.InvalidId && clicked == null)
                return true;

            var player = BasePlayer.FindOrCreate(playerid);
            var args = new ClickTextDrawEventArgs(player, clicked);
                
            OnPlayerClickTextDraw(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerClickPlayerTextDraw(int playerid, int playertextid)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var clicked = playertextid == PlayerTextDraw.InvalidId ? null : PlayerTextDraw.Find(player, playertextid);

            if (playertextid != TextDraw.InvalidId && clicked == null)
                return true;

            var args = new ClickPlayerTextDrawEventArgs(player, clicked);

            OnPlayerClickPlayerTextDraw(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerClickPlayer(int playerid, int clickedplayerid, int source)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new ClickPlayerEventArgs(player,
                clickedplayerid == BasePlayer.InvalidId ? null : BasePlayer.FindOrCreate(clickedplayerid),
                (PlayerClickSource) source);

            OnPlayerClickPlayer(args);

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

                var args = new EditPlayerObjectEventArgs(player, @object, (EditObjectResponse) response,
                    new Vector3(fX, fY, fZ), new Vector3(fRotX, fRotY, fRotZ));

                OnPlayerEditPlayerObject(args);
            }
            else
            {
                var @object = GlobalObject.FindOrCreate(objectid);

                if (@object == null)
                    return true;

                var args = new EditGlobalObjectEventArgs(player, @object, (EditObjectResponse) response,
                    new Vector3(fX, fY, fZ), new Vector3(fRotX, fRotY, fRotZ));

                OnPlayerEditGlobalObject(args);
            }

            return true;
        }

        [Callback]
        internal bool OnPlayerEditAttachedObject(int playerid, int response, int index, int modelid, int boneid,
            float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX,
            float fScaleY, float fScaleZ)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new EditAttachedObjectEventArgs(player, (EditObjectResponse) response, index, modelid,
                (Bone) boneid,
                new Vector3(fOffsetX, fOffsetY, fOffsetZ), new Vector3(fRotX, fRotY, fRotZ),
                new Vector3(fScaleX, fScaleY, fScaleZ));

            OnPlayerEditAttachedObject(args);

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
                    var args = new SelectGlobalObjectEventArgs(player, @object, modelid, new Vector3(fX, fY, fZ));

                    OnPlayerSelectGlobalObject(args);
                    break;
                }
                case ObjectType.PlayerObject:
                {
                    var player = BasePlayer.FindOrCreate(playerid);
                    var @object = PlayerObject.FindOrCreate(player, objectid);

                    if (@object == null)
                        return true;

                    var args = new SelectPlayerObjectEventArgs(player, @object, modelid, new Vector3(fX, fY, fZ));

                    OnPlayerSelectPlayerObject(args);
                    break;
                }
            }

            return true;
        }

        [Callback]
        internal bool OnPlayerWeaponShot(int playerid, int weaponid, int hittype, int hitid, float fX, float fY,
            float fZ)
        {
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new WeaponShotEventArgs(player, (Weapon) weaponid, (BulletHitType) hittype, hitid,
                new Vector3(fX, fY, fZ));

            OnPlayerWeaponShot(args);

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
            
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new SirenStateEventArgs(player, vehicle, newstate);

            OnVehicleSirenStateChange(args);

            return true;
        }

        [Callback]
        internal bool OnActorStreamIn(int actorid, int forplayerid)
        {
            var actor = Actor.Find(actorid);

            if (actor == null)
                return true;
            
            var player = BasePlayer.FindOrCreate(forplayerid);
            var args = new ActorStreamEventArgs(player, actor, StreamMode.In);

            OnActorStreamIn(args);

            return true;
        }

        [Callback]
        internal bool OnActorStreamOut(int actorid, int forplayerid)
        {
            var actor = Actor.Find(actorid);

            if (actor == null)
                return true;
            
            var player = BasePlayer.FindOrCreate(forplayerid);
            var args = new ActorStreamEventArgs(player, actor, StreamMode.Out);

            OnActorStreamOut(args);

            return true;
        }

        [Callback]
        internal bool OnPlayerGiveDamageActor(int playerid, int damagedActorid, float amount, int weaponid, int bodypart)
        {
            var actor = Actor.Find(damagedActorid);

            if (actor == null)
                return true;
            
            var player = BasePlayer.FindOrCreate(playerid);
            var args = new ActorDamageEventArgs(player, actor, amount, (Weapon) weaponid,
                (BodyPart) bodypart);

            OnPlayerGiveDamageActor(args);

            return true;
        }
    }
}