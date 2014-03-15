#include "MonoProxy.h"

#include "PathUtil.h"
#include "Natives.h"

CMonoProxy::CMonoProxy() {
	
	//Mono setup
	mono_set_dirs(PathUtil::GetLibDirectory().c_str(), PathUtil::GetConfigDirectory().c_str());
	mono_debug_init(MONO_DEBUG_FORMAT_MONO);
	m_pRootDomain = mono_jit_init_version("SAMP.Mono.Proxy", "v4.0.30319");

	//Load assembly
	MonoAssembly *pMonoAssembly = mono_domain_assembly_open(mono_domain_get(), PathUtil::GetBinDirectory().append("plugins/GameMode.dll").c_str());
	m_pClassLibraryImage = mono_assembly_get_image(pMonoAssembly);

	//Load natives
	LoadNatives(); 

	//Create instance of main class
	m_pClassServerClass = mono_class_from_name(m_pClassLibraryImage, "GameMode", "Server");

	m_pClassServer = mono_object_new(mono_domain_get(), m_pClassServerClass);
	mono_runtime_object_init(m_pClassServer);

	//Load callbacks
	m_cOnGameModeInit = LoadCallback("GameMode.Server:OnGameModeInit()");
	m_cOnGameModeExit = LoadCallback("GameMode.Server:OnGameModeExit()");
	m_cOnPlayerConnect = LoadCallback("GameMode.Server:OnPlayerConnect(int)");
	m_cOnPlayerDisconnect = LoadCallback("GameMode.Server:OnPlayerDisconnect(int,int)");
	m_cOnPlayerSpawn = LoadCallback("GameMode.Server:OnPlayerSpawn(int)");
	m_cOnPlayerDeath = LoadCallback("GameMode.Server:OnPlayerDeath(int,int,int)");
	m_cOnVehicleSpawn = LoadCallback("GameMode.Server:OnVehicleSpawn(int)");
	m_cOnVehicleDeath = LoadCallback("GameMode.Server:OnVehicleDeath(int,int)");
	m_cOnPlayerText = LoadCallback("GameMode.Server:OnPlayerText(int,string)");
	m_cOnPlayerCommandText = LoadCallback("GameMode.Server:OnPlayerCommandText(int,string)");
	m_cOnPlayerRequestClass = LoadCallback("GameMode.Server:OnPlayerRequestClass(int,int)");
	m_cOnPlayerEnterVehicle = LoadCallback("GameMode.Server:OnPlayerEnterVehicle(int,int,bool)");
	m_cOnPlayerExitVehicle = LoadCallback("GameMode.Server:OnPlayerExitVehicle(int,int)");
	m_cOnPlayerStateChange = LoadCallback("GameMode.Server:OnPlayerStateChange(int,int,int)");
	m_cOnPlayerEnterCheckpoint = LoadCallback("GameMode.Server:OnPlayerEnterCheckpoint(int)");
	m_cOnPlayerLeaveCheckpoint = LoadCallback("GameMode.Server:OnPlayerLeaveCheckpoint(int)");
	m_cOnPlayerEnterRaceCheckpoint = LoadCallback("GameMode.Server:OnPlayerEnterRaceCheckpoint(int)");
	m_cOnPlayerLeaveRaceCheckpoint = LoadCallback("GameMode.Server:OnPlayerLeaveRaceCheckpoint(int)");
	m_cOnRconCommand = LoadCallback("GameMode.Server:OnRconCommandstring(string)");
	m_cOnPlayerRequestSpawn = LoadCallback("GameMode.Server:OnPlayerRequestSpawn(int)");
	m_cOnObjectMoved = LoadCallback("GameMode.Server:OnObjectMoved(int)");
	m_cOnPlayerObjectMoved = LoadCallback("GameMode.Server:OnPlayerObjectMoved(int,int)");
	m_cOnPlayerPickUpPickup = LoadCallback("GameMode.Server:OnPlayerPickUpPickup(int,int)");
	m_cOnVehicleMod = LoadCallback("GameMode.Server:OnVehicleMod(int,int,int)");
	m_cOnEnterExitModShop = LoadCallback("GameMode.Server:OnEnterExitModShop(int,int,int)");
	m_cOnVehiclePaintjob = LoadCallback("GameMode.Server:OnVehiclePaintjob(int,int,int)");
	m_cOnVehicleRespray = LoadCallback("GameMode.Server:OnVehicleRespray(int,int,int,int)");
	m_cOnVehicleDamageStatusUpdate = LoadCallback("GameMode.Server:OnVehicleDamageStatusUpdate(int,int)");
	m_cOnUnoccupiedVehicleUpdate = LoadCallback("GameMode.Server:OnUnoccupiedVehicleUpdate(int,int,int)");
	m_cOnPlayerSelectedMenuRow = LoadCallback("GameMode.Server:OnPlayerSelectedMenuRow(int,int)");
	m_cOnPlayerExitedMenu = LoadCallback("GameMode.Server:OnPlayerExitedMenu(int)");
	m_cOnPlayerInteriorChange = LoadCallback("GameMode.Server:OnPlayerInteriorChange(int,int,int)");
	m_cOnPlayerKeyStateChange = LoadCallback("GameMode.Server:OnPlayerKeyStateChange(int,int,int)");
	m_cOnRconLoginAttempt = LoadCallback("GameMode.Server:OnRconLoginAttempt(string,string,bool)");
	m_cOnPlayerUpdate = LoadCallback("GameMode.Server:OnPlayerUpdate(int)");
	m_cOnPlayerStreamIn = LoadCallback("GameMode.Server:OnPlayerStreamIn(int,int)");
	m_cOnPlayerStreamOut = LoadCallback("GameMode.Server:OnPlayerStreamOut(int,int)");
	m_cOnVehicleStreamIn = LoadCallback("GameMode.Server:OnVehicleStreamIn(int,int)");
	m_cOnVehicleStreamOut = LoadCallback("GameMode.Server:OnVehicleStreamOut(int,int)");
	m_cOnDialogResponse = LoadCallback("GameMode.Server:OnDialogResponse(int,int,int,int,string)");
	m_cOnPlayerTakeDamage = LoadCallback("GameMode.Server:OnPlayerTakeDamage(int,int,float,int,int)");
	m_cOnPlayerGiveDamage = LoadCallback("GameMode.Server:OnPlayerGiveDamage(int,int,float,int,int)");
	m_cOnPlayerClickMap = LoadCallback("GameMode.Server:OnPlayerClickMap(int,float,float,float)");
	m_cOnPlayerClickTextDraw = LoadCallback("GameMode.Server:OnPlayerClickTextDraw(int,int)");
	m_cOnPlayerClickPlayerTextDraw = LoadCallback("GameMode.Server:OnPlayerClickPlayerTextDraw(int,int)");
	m_cOnPlayerClickPlayer = LoadCallback("GameMode.Server:OnPlayerClickPlayer(int,int,int)");
	m_cOnPlayerEditObject = LoadCallback("GameMode.Server:OnPlayerEditObject(int,bool,int,int,float,float,float,float,float,float)");
	m_cOnPlayerEditAttachedObject = LoadCallback("GameMode.Server:OnPlayerEditAttachedObject(int,int,int,int,int,float,float,float,float,float,float,float,float,float)");
	m_cOnPlayerSelectObject = LoadCallback("GameMode.Server:OnPlayerSelectObject(int,int,int,int,float,float,float)");
	m_cOnPlayerWeaponShot = LoadCallback("GameMode.Server:OnPlayerWeaponShot(int,int,int,int,float,float,float)");

}

MonoMethod* CMonoProxy::LoadCallback(const char *name) {
	MonoMethodDesc* m_methodDescription = mono_method_desc_new(name, true);
	MonoMethod* m_method = mono_method_desc_search_in_image(m_methodDescription, m_pClassLibraryImage);
	mono_method_desc_free(m_methodDescription);

	return m_method;
}

bool CMonoProxy::CallCallback(MonoMethod* method, void **params) {
	return *(bool*)mono_object_unbox(mono_runtime_invoke(method, m_pClassServer, params, NULL));
}

CMonoProxy::~CMonoProxy() {
}
