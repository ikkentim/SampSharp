#pragma once

#include <mono/mini/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/mono-debug.h>
#include <mono/metadata/debug-helpers.h>

class CSampSharp
{
public:
	CSampSharp(char * basemode_path, char * gamemode_path, char * gamemode_namespace, char * gamemode_class, char * runtime_version);
	~CSampSharp();

	bool CallCallback(MonoMethod * method, void ** params);

	static CSampSharp * p_instance;

	MonoDomain * m_pRootDomain;

	MonoMethod * m_cOnGameModeInit;
	MonoMethod * m_cOnGameModeExit;
	MonoMethod * m_cOnPlayerConnect;
	MonoMethod * m_cOnPlayerDisconnect;
	MonoMethod * m_cOnPlayerSpawn;
	MonoMethod * m_cOnPlayerDeath;
	MonoMethod * m_cOnVehicleSpawn;
	MonoMethod * m_cOnVehicleDeath;
	MonoMethod * m_cOnPlayerText;
	MonoMethod * m_cOnPlayerCommandText;
	MonoMethod * m_cOnPlayerRequestClass;
	MonoMethod * m_cOnPlayerEnterVehicle;
	MonoMethod * m_cOnPlayerExitVehicle;
	MonoMethod * m_cOnPlayerStateChange;
	MonoMethod * m_cOnPlayerEnterCheckpoint;
	MonoMethod * m_cOnPlayerLeaveCheckpoint;
	MonoMethod * m_cOnPlayerEnterRaceCheckpoint;
	MonoMethod * m_cOnPlayerLeaveRaceCheckpoint;
	MonoMethod * m_cOnRconCommand;
	MonoMethod * m_cOnPlayerRequestSpawn;
	MonoMethod * m_cOnObjectMoved;
	MonoMethod * m_cOnPlayerObjectMoved;
	MonoMethod * m_cOnPlayerPickUpPickup;
	MonoMethod * m_cOnVehicleMod;
	MonoMethod * m_cOnEnterExitModShop;
	MonoMethod * m_cOnVehiclePaintjob;
	MonoMethod * m_cOnVehicleRespray;
	MonoMethod * m_cOnVehicleDamageStatusUpdate;
	MonoMethod * m_cOnUnoccupiedVehicleUpdate;
	MonoMethod * m_cOnPlayerSelectedMenuRow;
	MonoMethod * m_cOnPlayerExitedMenu;
	MonoMethod * m_cOnPlayerInteriorChange;
	MonoMethod * m_cOnPlayerKeyStateChange;
	MonoMethod * m_cOnRconLoginAttempt;
	MonoMethod * m_cOnPlayerUpdate;
	MonoMethod * m_cOnPlayerStreamIn;
	MonoMethod * m_cOnPlayerStreamOut;
	MonoMethod * m_cOnVehicleStreamIn;
	MonoMethod * m_cOnVehicleStreamOut;
	MonoMethod * m_cOnDialogResponse;
	MonoMethod * m_cOnPlayerTakeDamage;
	MonoMethod * m_cOnPlayerGiveDamage;
	MonoMethod * m_cOnPlayerClickMap;
	MonoMethod * m_cOnPlayerClickTextDraw;
	MonoMethod * m_cOnPlayerClickPlayerTextDraw;
	MonoMethod * m_cOnPlayerClickPlayer;
	MonoMethod * m_cOnPlayerEditObject;
	MonoMethod * m_cOnPlayerEditAttachedObject;
	MonoMethod * m_cOnPlayerSelectObject;
	MonoMethod * m_cOnPlayerWeaponShot;
	MonoMethod * m_cOnTimerTick;
 
private:
	MonoMethod * LoadCallback(const char * cname, const char * name);
	MonoImage * m_pGameModeImage;
	MonoImage * m_pBaseModeImage;

	MonoObject * m_pGameMode;
	MonoClass * m_pGameModeClass;
};

