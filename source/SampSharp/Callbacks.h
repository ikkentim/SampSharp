#pragma once

#include <iostream>

#include <mono/metadata/threads.h>

#include "SampSharp.h"

static void SAMPGDK_TIMER_CALL p_TimerCallback(int timerid, void * data) {
	void *args[2];
	args[0] = &timerid;
	args[1] = data;
	bool response = CSampSharp::instance->CallCallback(CSampSharp::instance->onTimerTick, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnGameModeInit() {
	return CSampSharp::instance->CallCallback(CSampSharp::instance->onGameModeInit, NULL);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnGameModeExit() {
	return CSampSharp::instance->CallCallback(CSampSharp::instance->onGameModeExit, NULL);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerConnect(int playerid) {
	void *args[1];
	args[0] = &playerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerConnect, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerDisconnect(int playerid, int reason) {
	void *args[2];
	args[0] = &playerid;
	args[1] = &reason;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerDisconnect, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerSpawn(int playerid) {
	void *args[1];
	args[0] = &playerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerSpawn, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerDeath(int playerid, int killerid, int reason) {
	void *args[3];
	args[0] = &playerid;
	args[1] = &killerid;
	args[2] = &reason;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerDeath, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnVehicleSpawn(int vehicleid) {
	void *args[1];
	args[0] = &vehicleid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onVehicleSpawn, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnVehicleDeath(int vehicleid, int killerid) {
	void *args[2];
	args[0] = &vehicleid;
	args[1] = &killerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onVehicleDeath, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerText(int playerid, const char * text) {
	void *args[2];
	args[0] = &playerid;
	args[1] = mono_string_new(mono_domain_get(), text);

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerText, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerCommandText(int playerid, const char * cmdtext) {
	void *args[2];
	args[0] = &playerid;
	args[1] = mono_string_new(mono_domain_get(), cmdtext);

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerCommandText, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerRequestClass(int playerid, int classid) {
	void *args[2];
	args[0] = &playerid;
	args[1] = &classid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerRequestClass, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerEnterVehicle(int playerid, int vehicleid, bool ispassenger) {
	void *args[3];
	args[0] = &playerid;
	args[1] = &vehicleid;
	args[2] = &ispassenger;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerEnterVehicle, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerExitVehicle(int playerid, int vehicleid) {
	void *args[2];
	args[0] = &playerid;
	args[1] = &vehicleid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerExitVehicle, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerStateChange(int playerid, int newstate, int oldstate) {
	void *args[3];
	args[0] = &playerid;
	args[1] = &newstate;
	args[2] = &oldstate;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerStateChange, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerEnterCheckpoint(int playerid) {
	void *args[1];
	args[0] = &playerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerEnterCheckpoint, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerLeaveCheckpoint(int playerid) {
	void *args[1];
	args[0] = &playerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerLeaveCheckpoint, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerEnterRaceCheckpoint(int playerid) {
	void *args[1];
	args[0] = &playerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerEnterRaceCheckpoint, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerLeaveRaceCheckpoint(int playerid) {
	void *args[1];
	args[0] = &playerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerLeaveRaceCheckpoint, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnRconCommand(const char * cmd) {
	mono_thread_attach(CSampSharp::instance->rootDomain);

	void *args[1];

	args[0] = mono_string_new(mono_domain_get(), cmd);

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onRconCommand, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerRequestSpawn(int playerid) {
	void *args[1];
	args[0] = &playerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerRequestSpawn, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnObjectMoved(int objectid) {
	void *args[1];
	args[0] = &objectid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onObjectMoved, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerObjectMoved(int playerid, int objectid) {
	void *args[2];
	args[0] = &playerid;
	args[1] = &objectid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerObjectMoved, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerPickUpPickup(int playerid, int pickupid) {
	void *args[2];
	args[0] = &playerid;
	args[1] = &pickupid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerPickUpPickup, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnVehicleMod(int playerid, int vehicleid, int componentid) {
	void *args[3];
	args[0] = &playerid;
	args[1] = &vehicleid;
	args[2] = &componentid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onVehicleMod, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnEnterExitModShop(int playerid, int enterexit, int interiorid) {
	void *args[3];
	args[0] = &playerid;
	args[1] = &enterexit;
	args[2] = &interiorid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onEnterExitModShop, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnVehiclePaintjob(int playerid, int vehicleid, int paintjobid) {
	void *args[3];
	args[0] = &playerid;
	args[1] = &vehicleid;
	args[2] = &paintjobid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onVehiclePaintjob, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnVehicleRespray(int playerid, int vehicleid, int color1, int color2) {
	void *args[4];
	args[0] = &playerid;
	args[1] = &vehicleid;
	args[2] = &color1;
	args[3] = &color2;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onVehicleRespray, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnVehicleDamageStatusUpdate(int vehicleid, int playerid) {
	void *args[2];
	args[0] = &vehicleid;
	args[1] = &playerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onVehicleDamageStatusUpdate, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnUnoccupiedVehicleUpdate(int vehicleid, int playerid, int passenger_seat) {
	void *args[3];
	args[0] = &vehicleid;
	args[1] = &playerid;
	args[2] = &passenger_seat;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onUnoccupiedVehicleUpdate, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerSelectedMenuRow(int playerid, int row) {
	void *args[2];
	args[0] = &playerid;
	args[1] = &row;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerSelectedMenuRow, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerExitedMenu(int playerid) {
	void *args[1];
	args[0] = &playerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerExitedMenu, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerInteriorChange(int playerid, int newinteriorid, int oldinteriorid) {
	void *args[3];
	args[0] = &playerid;
	args[1] = &newinteriorid;
	args[2] = &oldinteriorid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerInteriorChange, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerKeyStateChange(int playerid, int newkeys, int oldkeys) {
	void *args[3];
	args[0] = &playerid;
	args[1] = &newkeys;
	args[2] = &oldkeys;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerKeyStateChange, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnRconLoginAttempt(const char * ip, const char * password, bool success) {
	void *args[3];
	args[0] = mono_string_new(mono_domain_get(), ip);
	args[1] = mono_string_new(mono_domain_get(), password);
	args[2] = &success;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onRconLoginAttempt, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerUpdate(int playerid) {
	void *args[1];
	args[0] = &playerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerUpdate, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerStreamIn(int playerid, int forplayerid) {
	void *args[2];
	args[0] = &playerid;
	args[1] = &forplayerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerStreamIn, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerStreamOut(int playerid, int forplayerid) {
	void *args[2];
	args[0] = &playerid;
	args[1] = &forplayerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerStreamOut, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnVehicleStreamIn(int vehicleid, int forplayerid) {
	void *args[2];
	args[0] = &vehicleid;
	args[1] = &forplayerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onVehicleStreamIn, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnVehicleStreamOut(int vehicleid, int forplayerid) {
	void *args[2];
	args[0] = &vehicleid;
	args[1] = &forplayerid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onVehicleStreamOut, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnDialogResponse(int playerid, int dialogid, int response, int listitem, const char * inputtext) {
	void *args[5];
	args[0] = &playerid;
	args[1] = &dialogid;
	args[2] = &response;
	args[3] = &listitem;
	args[4] = mono_string_new(mono_domain_get(), inputtext);

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onDialogResponse, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerTakeDamage(int playerid, int issuerid, float amount, int weaponid, int bodypart) {
	void *args[5];
	args[0] = &playerid;
	args[1] = &issuerid;
	args[2] = &amount;
	args[3] = &weaponid;
	args[4] = &bodypart;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerTakeDamage, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerGiveDamage(int playerid, int damagedid, float amount, int weaponid, int bodypart) {
	void *args[5];
	args[0] = &playerid;
	args[1] = &damagedid;
	args[2] = &amount;
	args[3] = &weaponid;
	args[4] = &bodypart;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerGiveDamage, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerClickMap(int playerid, float fX, float fY, float fZ) {
	void *args[4];
	args[0] = &playerid;
	args[1] = &fX;
	args[2] = &fY;
	args[3] = &fZ;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerClickMap, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerClickTextDraw(int playerid, int clickedid) {
	void *args[2];
	args[0] = &playerid;
	args[1] = &clickedid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerClickTextDraw, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerClickPlayerTextDraw(int playerid, int playertextid) {
	void *args[2];
	args[0] = &playerid;
	args[1] = &playertextid;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerClickPlayerTextDraw, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerClickPlayer(int playerid, int clickedplayerid, int source) {
	void *args[3];
	args[0] = &playerid;
	args[1] = &clickedplayerid;
	args[2] = &source;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerClickPlayer, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerEditObject(int playerid, bool playerobject, int objectid, int response, float fX, float fY, float fZ, float fRotX, float fRotY, float fRotZ) {
	void *args[10];
	args[0] = &playerid;
	args[1] = &playerobject;
	args[2] = &objectid;
	args[3] = &response;
	args[4] = &fX;
	args[5] = &fY;
	args[6] = &fZ;
	args[7] = &fRotX;
	args[8] = &fRotY;
	args[9] = &fRotZ;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerEditObject, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerEditAttachedObject(int playerid, int response, int index, int modelid, int boneid, float fOffsetX, float fOffsetY, float fOffsetZ, float fRotX, float fRotY, float fRotZ, float fScaleX, float fScaleY, float fScaleZ) {
	void *args[14];
	args[0] = &playerid;
	args[1] = &response;
	args[2] = &index;
	args[3] = &modelid;
	args[4] = &boneid;
	args[5] = &fOffsetX;
	args[6] = &fOffsetY;
	args[7] = &fOffsetZ;
	args[8] = &fRotX;
	args[9] = &fRotY;
	args[10] = &fRotZ;
	args[11] = &fScaleX;
	args[12] = &fScaleY;
	args[13] = &fScaleZ;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerEditAttachedObject, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerSelectObject(int playerid, int type, int objectid, int modelid, float fX, float fY, float fZ) {
	void *args[7];
	args[0] = &playerid;
	args[1] = &type;
	args[2] = &objectid;
	args[3] = &modelid;
	args[4] = &fX;
	args[5] = &fY;
	args[6] = &fZ;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerSelectObject, args);
}

PLUGIN_EXPORT bool PLUGIN_CALL OnPlayerWeaponShot(int playerid, int weaponid, int hittype, int hitid, float fX, float fY, float fZ) {
	void *args[7];
	args[0] = &playerid;
	args[1] = &weaponid;
	args[2] = &hittype;
	args[3] = &hitid;
	args[4] = &fX;
	args[5] = &fY;
	args[6] = &fZ;

	return CSampSharp::instance->CallCallback(CSampSharp::instance->onPlayerWeaponShot, args);
}