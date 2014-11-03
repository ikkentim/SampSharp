// Main pawn gamemode.
//
// Satisfies the samp-server, loads the empty filterscript
// and lets SampSharp do it's work.
#include <a_samp>
#include <streamer>

main() return;
public OnGameModeInit()	return SendRconCommand("loadfs empty"), 1;
public OnGameModeExit() return 1;
public OnFilterScriptInit() return 1;
public OnFilterScriptExit() return 1;
public OnPlayerRequestClass(playerid, classid) return 1;
public OnPlayerConnect(playerid) return 1;
public OnPlayerDisconnect(playerid, reason) return 1;
public OnPlayerSpawn(playerid)return 1;
public OnPlayerDeath(playerid, killerid, reason)return 1;
public OnVehicleSpawn(vehicleid)return 1;
public OnVehicleDeath(vehicleid, killerid)return 1;
public OnPlayerText(playerid, text[])return 1;
public OnPlayerCommandText(playerid, cmdtext[])return 0;
public OnPlayerEnterVehicle(playerid, vehicleid, ispassenger)return 1;
public OnPlayerExitVehicle(playerid, vehicleid)return 1;
public OnPlayerStateChange(playerid, newstate, oldstate)return 1;
public OnPlayerEnterCheckpoint(playerid) return 1;
public OnPlayerLeaveCheckpoint(playerid) return 1;
public OnPlayerEnterRaceCheckpoint(playerid) return 1;
public OnPlayerLeaveRaceCheckpoint(playerid) return 1;
public OnRconCommand(cmd[]) return 1;
public OnPlayerRequestSpawn(playerid) return 1;
public OnObjectMoved(objectid) return 1;
public OnPlayerObjectMoved(playerid, objectid) return 1;
public OnPlayerPickUpPickup(playerid, pickupid) return 1;
public OnVehicleMod(playerid, vehicleid, componentid) return 1;
public OnVehiclePaintjob(playerid, vehicleid, paintjobid) return 1;
public OnVehicleRespray(playerid, vehicleid, color1, color2) return 1;
public OnPlayerSelectedMenuRow(playerid, row) return 1;
public OnPlayerExitedMenu(playerid) return 1;
public OnPlayerInteriorChange(playerid, newinteriorid, oldinteriorid) return 1;
public OnPlayerKeyStateChange(playerid, newkeys, oldkeys) return 1;
public OnRconLoginAttempt(ip[], password[], success) return 1;
public OnPlayerUpdate(playerid) return 1;
public OnPlayerStreamIn(playerid, forplayerid) return 1;
public OnPlayerStreamOut(playerid, forplayerid) return 1;
public OnVehicleStreamIn(vehicleid, forplayerid) return 1;
public OnVehicleStreamOut(vehicleid, forplayerid) return 1;
public OnDialogResponse(playerid, dialogid, response, listitem, inputtext[]) return 1;
public OnPlayerClickPlayer(playerid, clickedplayerid, source) return 1;

