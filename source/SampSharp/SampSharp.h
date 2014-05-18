#pragma once

#include <iostream>
#include <fstream>
#include <cstring> //strcpy
#include <time.h>

#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/mono-debug.h>
#include <mono/metadata/debug-helpers.h>

using namespace std;

class SampSharp
{
public:
	static void Load(string baseModePath, string gamemodePath, string gameModeNamespace, string gameModeClass, bool debug);
	static void Unload();
	static bool Reload();
	static bool CallEvent(MonoMethod * method, void ** params);

	static MonoDomain * rootDomain;
	static MonoMethod * onGameModeInit;
	static MonoMethod * onGameModeExit;
	static MonoMethod * onPlayerConnect;
	static MonoMethod * onPlayerDisconnect;
	static MonoMethod * onPlayerSpawn;
	static MonoMethod * onPlayerDeath;
	static MonoMethod * onVehicleSpawn;
	static MonoMethod * onVehicleDeath;
	static MonoMethod * onPlayerText;
	static MonoMethod * onPlayerCommandText;
	static MonoMethod * onPlayerRequestClass;
	static MonoMethod * onPlayerEnterVehicle;
	static MonoMethod * onPlayerExitVehicle;
	static MonoMethod * onPlayerStateChange;
	static MonoMethod * onPlayerEnterCheckpoint;
	static MonoMethod * onPlayerLeaveCheckpoint;
	static MonoMethod * onPlayerEnterRaceCheckpoint;
	static MonoMethod * onPlayerLeaveRaceCheckpoint;
	static MonoMethod * onRconCommand;
	static MonoMethod * onPlayerRequestSpawn;
	static MonoMethod * onObjectMoved;
	static MonoMethod * onPlayerObjectMoved;
	static MonoMethod * onPlayerPickUpPickup;
	static MonoMethod * onVehicleMod;
	static MonoMethod * onEnterExitModShop;
	static MonoMethod * onVehiclePaintjob;
	static MonoMethod * onVehicleRespray;
	static MonoMethod * onVehicleDamageStatusUpdate;
	static MonoMethod * onUnoccupiedVehicleUpdate;
	static MonoMethod * onPlayerSelectedMenuRow;
	static MonoMethod * onPlayerExitedMenu;
	static MonoMethod * onPlayerInteriorChange;
	static MonoMethod * onPlayerKeyStateChange;
	static MonoMethod * onRconLoginAttempt;
	static MonoMethod * onPlayerUpdate;
	static MonoMethod * onPlayerStreamIn;
	static MonoMethod * onPlayerStreamOut;
	static MonoMethod * onVehicleStreamIn;
	static MonoMethod * onVehicleStreamOut;
	static MonoMethod * onDialogResponse;
	static MonoMethod * onPlayerTakeDamage;
	static MonoMethod * onPlayerGiveDamage;
	static MonoMethod * onPlayerClickMap;
	static MonoMethod * onPlayerClickTextDraw;
	static MonoMethod * onPlayerClickPlayerTextDraw;
	static MonoMethod * onPlayerClickPlayer;
	static MonoMethod * onPlayerEditObject;
	static MonoMethod * onPlayerEditAttachedObject;
	static MonoMethod * onPlayerSelectObject;
	static MonoMethod * onPlayerWeaponShot;
	static MonoMethod * onIncomingConnection;
	static MonoMethod * onTimerTick;
	static MonoMethod * onTick;
	static MonoMethod * dispose;
 
private:
	static MonoMethod * LoadEvent(const char * cname, const char * name);
	static void LoadEvents(const char * gmClass);
	static void GenerateSymbols(string path);

	static MonoImage * gameModeImage;
	static MonoImage * baseModeImage;
	static uint32_t gameModeHandle;
};

