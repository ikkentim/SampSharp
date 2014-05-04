#pragma once

#include <mono/jit/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/debug-helpers.h>

using namespace std;

class CSampSharp
{
public:
	CSampSharp(string bmPath, string gmPath, string gmNamespace, string gmClass, bool generateSymbols);
	~CSampSharp();

	bool CallCallback(MonoMethod * method, void ** params);

	static CSampSharp * instance;

	MonoDomain * rootDomain;

	MonoMethod * onGameModeInit;
	MonoMethod * onGameModeExit;
	MonoMethod * onPlayerConnect;
	MonoMethod * onPlayerDisconnect;
	MonoMethod * onPlayerSpawn;
	MonoMethod * onPlayerDeath;
	MonoMethod * onVehicleSpawn;
	MonoMethod * onVehicleDeath;
	MonoMethod * onPlayerText;
	MonoMethod * onPlayerCommandText;
	MonoMethod * onPlayerRequestClass;
	MonoMethod * onPlayerEnterVehicle;
	MonoMethod * onPlayerExitVehicle;
	MonoMethod * onPlayerStateChange;
	MonoMethod * onPlayerEnterCheckpoint;
	MonoMethod * onPlayerLeaveCheckpoint;
	MonoMethod * onPlayerEnterRaceCheckpoint;
	MonoMethod * onPlayerLeaveRaceCheckpoint;
	MonoMethod * onRconCommand;
	MonoMethod * onPlayerRequestSpawn;
	MonoMethod * onObjectMoved;
	MonoMethod * onPlayerObjectMoved;
	MonoMethod * onPlayerPickUpPickup;
	MonoMethod * onVehicleMod;
	MonoMethod * onEnterExitModShop;
	MonoMethod * onVehiclePaintjob;
	MonoMethod * onVehicleRespray;
	MonoMethod * onVehicleDamageStatusUpdate;
	MonoMethod * onUnoccupiedVehicleUpdate;
	MonoMethod * onPlayerSelectedMenuRow;
	MonoMethod * onPlayerExitedMenu;
	MonoMethod * onPlayerInteriorChange;
	MonoMethod * onPlayerKeyStateChange;
	MonoMethod * onRconLoginAttempt;
	MonoMethod * onPlayerUpdate;
	MonoMethod * onPlayerStreamIn;
	MonoMethod * onPlayerStreamOut;
	MonoMethod * onVehicleStreamIn;
	MonoMethod * onVehicleStreamOut;
	MonoMethod * onDialogResponse;
	MonoMethod * onPlayerTakeDamage;
	MonoMethod * onPlayerGiveDamage;
	MonoMethod * onPlayerClickMap;
	MonoMethod * onPlayerClickTextDraw;
	MonoMethod * onPlayerClickPlayerTextDraw;
	MonoMethod * onPlayerClickPlayer;
	MonoMethod * onPlayerEditObject;
	MonoMethod * onPlayerEditAttachedObject;
	MonoMethod * onPlayerSelectObject;
	MonoMethod * onPlayerWeaponShot;
	MonoMethod * onIncomingConnection;
	MonoMethod * onTimerTick;
	MonoMethod * onTick;
 
private:
	MonoMethod * LoadCallback(const char * cname, const char * name);
	char * GetTimeStamp();
	void GenerateSymbols(string path);
	MonoImage * gameModeImage;
	MonoImage * baseModeImage;
	uint32_t gameModeHandle;
};

