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
	MonoAssembly *pMonoAssembly = mono_domain_assembly_open(mono_domain_get(), PathUtil::GetBinDirectory().append(gamemode_path).c_str());
	m_pClassLibraryImage = mono_assembly_get_image(pMonoAssembly);

	//Load all SA:MP natives
	LoadNatives(); 

	//Create instance of the gamemode's class
	m_pClassServerClass = mono_class_from_name(m_pClassLibraryImage, gamemode_namespace, gamemode_class);

	m_pClassServer = mono_object_new(mono_domain_get(), m_pClassServerClass);
	mono_runtime_object_init(m_pClassServer);

	//Load all SA:MP callbacks
	std::string cname = std::string(gamemode_namespace).append(".").append(gamemode_class).append(":");
	m_cOnGameModeInit = LoadCallback(cname.append("OnGameModeInit()").c_str());
	m_cOnGameModeExit = LoadCallback(cname.append("OnGameModeExit()").c_str());
	m_cOnPlayerConnect = LoadCallback(cname.append("OnPlayerConnect(int)").c_str());
	m_cOnPlayerDisconnect = LoadCallback(cname.append("OnPlayerDisconnect(int,int)").c_str());
	m_cOnPlayerSpawn = LoadCallback(cname.append("OnPlayerSpawn(int)").c_str());
	m_cOnPlayerDeath = LoadCallback(cname.append("OnPlayerDeath(int,int,int)").c_str());
	m_cOnVehicleSpawn = LoadCallback(cname.append("OnVehicleSpawn(int)").c_str());
	m_cOnVehicleDeath = LoadCallback(cname.append("OnVehicleDeath(int,int)").c_str());
	m_cOnPlayerText = LoadCallback(cname.append("OnPlayerText(int,string)").c_str());
	m_cOnPlayerCommandText = LoadCallback(cname.append("OnPlayerCommandText(int,string)").c_str());
	m_cOnPlayerRequestClass = LoadCallback(cname.append("OnPlayerRequestClass(int,int)").c_str());
	m_cOnPlayerEnterVehicle = LoadCallback(cname.append("OnPlayerEnterVehicle(int,int,bool)").c_str());
	m_cOnPlayerExitVehicle = LoadCallback(cname.append("OnPlayerExitVehicle(int,int)").c_str());
	m_cOnPlayerStateChange = LoadCallback(cname.append("OnPlayerStateChange(int,int,int)").c_str());
	m_cOnPlayerEnterCheckpoint = LoadCallback(cname.append("OnPlayerEnterCheckpoint(int)").c_str());
	m_cOnPlayerLeaveCheckpoint = LoadCallback(cname.append("OnPlayerLeaveCheckpoint(int)").c_str());
	m_cOnPlayerEnterRaceCheckpoint = LoadCallback(cname.append("OnPlayerEnterRaceCheckpoint(int)").c_str());
	m_cOnPlayerLeaveRaceCheckpoint = LoadCallback(cname.append("OnPlayerLeaveRaceCheckpoint(int)").c_str());
	m_cOnRconCommand = LoadCallback(cname.append("OnRconCommandstring(string)").c_str());
	m_cOnPlayerRequestSpawn = LoadCallback(cname.append("OnPlayerRequestSpawn(int)").c_str());
	m_cOnObjectMoved = LoadCallback(cname.append("OnObjectMoved(int)").c_str());
	m_cOnPlayerObjectMoved = LoadCallback(cname.append("OnPlayerObjectMoved(int,int)").c_str());
	m_cOnPlayerPickUpPickup = LoadCallback(cname.append("OnPlayerPickUpPickup(int,int)").c_str());
	m_cOnVehicleMod = LoadCallback(cname.append("OnVehicleMod(int,int,int)").c_str());
	m_cOnEnterExitModShop = LoadCallback(cname.append("OnEnterExitModShop(int,int,int)").c_str());
	m_cOnVehiclePaintjob = LoadCallback(cname.append("OnVehiclePaintjob(int,int,int)").c_str());
	m_cOnVehicleRespray = LoadCallback(cname.append("OnVehicleRespray(int,int,int,int)").c_str());
	m_cOnVehicleDamageStatusUpdate = LoadCallback(cname.append("OnVehicleDamageStatusUpdate(int,int)").c_str());
	m_cOnUnoccupiedVehicleUpdate = LoadCallback(cname.append("OnUnoccupiedVehicleUpdate(int,int,int)").c_str());
	m_cOnPlayerSelectedMenuRow = LoadCallback(cname.append("OnPlayerSelectedMenuRow(int,int)").c_str());
	m_cOnPlayerExitedMenu = LoadCallback(cname.append("OnPlayerExitedMenu(int)").c_str());
	m_cOnPlayerInteriorChange = LoadCallback(cname.append("OnPlayerInteriorChange(int,int,int)").c_str());
	m_cOnPlayerKeyStateChange = LoadCallback(cname.append("OnPlayerKeyStateChange(int,int,int)").c_str());
	m_cOnRconLoginAttempt = LoadCallback(cname.append("OnRconLoginAttempt(string,string,bool)").c_str());
	m_cOnPlayerUpdate = LoadCallback(cname.append("OnPlayerUpdate(int)").c_str());
	m_cOnPlayerStreamIn = LoadCallback(cname.append("OnPlayerStreamIn(int,int)").c_str());
	m_cOnPlayerStreamOut = LoadCallback(cname.append("OnPlayerStreamOut(int,int)").c_str());
	m_cOnVehicleStreamIn = LoadCallback(cname.append("OnVehicleStreamIn(int,int)").c_str());
	m_cOnVehicleStreamOut = LoadCallback(cname.append("OnVehicleStreamOut(int,int)").c_str());
	m_cOnDialogResponse = LoadCallback(cname.append("OnDialogResponse(int,int,int,int,string)").c_str());
	m_cOnPlayerTakeDamage = LoadCallback(cname.append("OnPlayerTakeDamage(int,int,float,int,int)").c_str());
	m_cOnPlayerGiveDamage = LoadCallback(cname.append("OnPlayerGiveDamage(int,int,float,int,int)").c_str());
	m_cOnPlayerClickMap = LoadCallback(cname.append("OnPlayerClickMap(int,float,float,float)").c_str());
	m_cOnPlayerClickTextDraw = LoadCallback(cname.append("OnPlayerClickTextDraw(int,int)").c_str());
	m_cOnPlayerClickPlayerTextDraw = LoadCallback(cname.append("OnPlayerClickPlayerTextDraw(int,int)").c_str());
	m_cOnPlayerClickPlayer = LoadCallback(cname.append("OnPlayerClickPlayer(int,int,int)").c_str());
	m_cOnPlayerEditObject = LoadCallback(cname.append("OnPlayerEditObject(int,bool,int,int,float,float,float,float,float,float)").c_str());
	m_cOnPlayerEditAttachedObject = LoadCallback(cname.append("OnPlayerEditAttachedObject(int,int,int,int,int,float,float,float,float,float,float,float,float,float)").c_str());
	m_cOnPlayerSelectObject = LoadCallback(cname.append("OnPlayerSelectObject(int,int,int,int,float,float,float)").c_str());
	m_cOnPlayerWeaponShot = LoadCallback(cname.append("OnPlayerWeaponShot(int,int,int,int,float,float,float)").c_str());
	m_cOnTimerTick = LoadCallback(cname.append("OnTimerTick(int,object)").c_str());
}

MonoMethod* CMonoProxy::LoadCallback(const char *name) {
	//Create method description and find method in image
	MonoMethodDesc * m_methodDescription = mono_method_desc_new(name, true);
	MonoMethod * m_method = mono_method_desc_search_in_image(m_methodDescription, m_pClassLibraryImage);

	//Free memory and return method
	mono_method_desc_free(m_methodDescription);

	return m_method;
}

bool CMonoProxy::CallCallback(MonoMethod* method, void **params) {
	//Invoke method and cast the response to a boolean
	return *(bool*)mono_object_unbox(mono_runtime_invoke(method, m_pClassServer, params, NULL));
}

CMonoProxy::~CMonoProxy() {
	//Cleanup Mono runtime
	mono_jit_cleanup(mono_domain_get());
}
