#include "MonoProxy.h"

#include "PathUtil.h"
#include "Natives.h"

CMonoProxy * CMonoProxy::p_instance;

CMonoProxy::CMonoProxy(char * gamemode_path, char * gamemode_namespace, char * gamemode_class, char * runtime_version) {
	
	//Initialize the Mono runtime
	mono_set_dirs(PathUtil::GetLibDirectory().c_str(), PathUtil::GetConfigDirectory().c_str());
	mono_debug_init(MONO_DEBUG_FORMAT_MONO);
	m_pRootDomain = mono_jit_init_version("SAMP.Mono.Proxy", runtime_version);

	//Load the gamemode's assembly
	MonoAssembly * pMonoAssembly = mono_domain_assembly_open(mono_domain_get(), PathUtil::GetBinDirectory().append(gamemode_path).c_str());
	m_pGameModeImage = mono_assembly_get_image(pMonoAssembly);

	//Load all SA:MP natives
	LoadNatives(); 

	//Create instance of the gamemode's class
	m_pGameModeClass = mono_class_from_name(m_pGameModeImage, gamemode_namespace, gamemode_class);

	m_pGameMode = mono_object_new(mono_domain_get(), m_pGameModeClass);
	mono_runtime_object_init(m_pGameMode);

	//Load all SA:MP callbacks
	m_cOnGameModeInit = LoadCallback("TestMode.GameMode:OnGameModeInit()");
	m_cOnGameModeExit = LoadCallback("TestMode.GameMode:OnGameModeExit()");
	m_cOnPlayerConnect = LoadCallback("TestMode.GameMode:OnPlayerConnect(int)");
	m_cOnPlayerDisconnect = LoadCallback("TestMode.GameMode:OnPlayerDisconnect(int,int)");
	m_cOnPlayerSpawn = LoadCallback("TestMode.GameMode:OnPlayerSpawn(int)");
	m_cOnPlayerDeath = LoadCallback("TestMode.GameMode:OnPlayerDeath(int,int,int)");
	m_cOnVehicleSpawn = LoadCallback("TestMode.GameMode:OnVehicleSpawn(int)");
	m_cOnVehicleDeath = LoadCallback("TestMode.GameMode:OnVehicleDeath(int,int)");
	m_cOnPlayerText = LoadCallback("TestMode.GameMode:OnPlayerText(int,string)");
	m_cOnPlayerCommandText = LoadCallback("TestMode.GameMode:OnPlayerCommandText(int,string)");
	m_cOnPlayerRequestClass = LoadCallback("TestMode.GameMode:OnPlayerRequestClass(int,int)");
	m_cOnPlayerEnterVehicle = LoadCallback("TestMode.GameMode:OnPlayerEnterVehicle(int,int,bool)");
	m_cOnPlayerExitVehicle = LoadCallback("TestMode.GameMode:OnPlayerExitVehicle(int,int)");
	m_cOnPlayerStateChange = LoadCallback("TestMode.GameMode:OnPlayerStateChange(int,int,int)");
	m_cOnPlayerEnterCheckpoint = LoadCallback("TestMode.GameMode:OnPlayerEnterCheckpoint(int)");
	m_cOnPlayerLeaveCheckpoint = LoadCallback("TestMode.GameMode:OnPlayerLeaveCheckpoint(int)");
	m_cOnPlayerEnterRaceCheckpoint = LoadCallback("TestMode.GameMode:OnPlayerEnterRaceCheckpoint(int)");
	m_cOnPlayerLeaveRaceCheckpoint = LoadCallback("TestMode.GameMode:OnPlayerLeaveRaceCheckpoint(int)");
	m_cOnRconCommand = LoadCallback("TestMode.GameMode:OnRconCommand(string)");
	m_cOnPlayerRequestSpawn = LoadCallback("TestMode.GameMode:OnPlayerRequestSpawn(int)");
	m_cOnObjectMoved = LoadCallback("TestMode.GameMode:OnObjectMoved(int)");
	m_cOnPlayerObjectMoved = LoadCallback("TestMode.GameMode:OnPlayerObjectMoved(int,int)");
	m_cOnPlayerPickUpPickup = LoadCallback("TestMode.GameMode:OnPlayerPickUpPickup(int,int)");
	m_cOnVehicleMod = LoadCallback("TestMode.GameMode:OnVehicleMod(int,int,int)");
	m_cOnEnterExitModShop = LoadCallback("TestMode.GameMode:OnEnterExitModShop(int,int,int)");
	m_cOnVehiclePaintjob = LoadCallback("TestMode.GameMode:OnVehiclePaintjob(int,int,int)");
	m_cOnVehicleRespray = LoadCallback("TestMode.GameMode:OnVehicleRespray(int,int,int,int)");
	m_cOnVehicleDamageStatusUpdate = LoadCallback("TestMode.GameMode:OnVehicleDamageStatusUpdate(int,int)");
	m_cOnUnoccupiedVehicleUpdate = LoadCallback("TestMode.GameMode:OnUnoccupiedVehicleUpdate(int,int,int)");
	m_cOnPlayerSelectedMenuRow = LoadCallback("TestMode.GameMode:OnPlayerSelectedMenuRow(int,int)");
	m_cOnPlayerExitedMenu = LoadCallback("TestMode.GameMode:OnPlayerExitedMenu(int)");
	m_cOnPlayerInteriorChange = LoadCallback("TestMode.GameMode:OnPlayerInteriorChange(int,int,int)");
	m_cOnPlayerKeyStateChange = LoadCallback("TestMode.GameMode:OnPlayerKeyStateChange(int,int,int)");
	m_cOnRconLoginAttempt = LoadCallback("TestMode.GameMode:OnRconLoginAttempt(string,string,bool)");
	m_cOnPlayerUpdate = LoadCallback("TestMode.GameMode:OnPlayerUpdate(int)");
	m_cOnPlayerStreamIn = LoadCallback("TestMode.GameMode:OnPlayerStreamIn(int,int)");
	m_cOnPlayerStreamOut = LoadCallback("TestMode.GameMode:OnPlayerStreamOut(int,int)");
	m_cOnVehicleStreamIn = LoadCallback("TestMode.GameMode:OnVehicleStreamIn(int,int)");
	m_cOnVehicleStreamOut = LoadCallback("TestMode.GameMode:OnVehicleStreamOut(int,int)");
	m_cOnDialogResponse = LoadCallback("TestMode.GameMode:OnDialogResponse(int,int,int,int,string)");
	m_cOnPlayerTakeDamage = LoadCallback("TestMode.GameMode:OnPlayerTakeDamage(int,int,float,int,int)");
	m_cOnPlayerGiveDamage = LoadCallback("TestMode.GameMode:OnPlayerGiveDamage(int,int,float,int,int)");
	m_cOnPlayerClickMap = LoadCallback("TestMode.GameMode:OnPlayerClickMap(int,float,float,float)");
	m_cOnPlayerClickTextDraw = LoadCallback("TestMode.GameMode:OnPlayerClickTextDraw(int,int)");
	m_cOnPlayerClickPlayerTextDraw = LoadCallback("TestMode.GameMode:OnPlayerClickPlayerTextDraw(int,int)");
	m_cOnPlayerClickPlayer = LoadCallback("TestMode.GameMode:OnPlayerClickPlayer(int,int,int)");
	m_cOnPlayerEditObject = LoadCallback("TestMode.GameMode:OnPlayerEditObject(int,bool,int,int,float,float,float,float,float,float)");
	m_cOnPlayerEditAttachedObject = LoadCallback("TestMode.GameMode:OnPlayerEditAttachedObject(int,int,int,int,int,float,float,float,float,float,float,float,float,float)");
	m_cOnPlayerSelectObject = LoadCallback("TestMode.GameMode:OnPlayerSelectObject(int,int,int,int,float,float,float)");
	m_cOnPlayerWeaponShot = LoadCallback("TestMode.GameMode:OnPlayerWeaponShot(int,int,int,int,float,float,float)");
	m_cOnTimerTick = LoadCallback("TestMode.GameMode:OnTimerTick(int,object)");
}

MonoMethod * CMonoProxy::LoadCallback(const char * name) {
	//Create method description and find method in image
	MonoMethodDesc * m_methodDescription = mono_method_desc_new(name, true);
	//MonoMethod * m_method = mono_class_get_method_from_name(m_pGameModeClass, name, param_count/*param_count*/);
	MonoMethod * m_method = mono_method_desc_search_in_image(m_methodDescription, m_pGameModeImage);

	//Free memory and return method
	mono_method_desc_free(m_methodDescription);

	return m_method;
}

bool CMonoProxy::CallCallback(MonoMethod* method, void **params) {
	//Invoke method and cast the response to a boolean
	return *(bool *)mono_object_unbox(mono_runtime_invoke(method, m_pGameMode, params, NULL));
}

CMonoProxy::~CMonoProxy() {
	//Cleanup Mono runtime
	mono_jit_cleanup(mono_domain_get());
}
