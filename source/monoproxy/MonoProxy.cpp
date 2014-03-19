#include "MonoProxy.h"

#include "PathUtil.h"
#include "Natives.h"

CMonoProxy * CMonoProxy::p_instance;

CMonoProxy::CMonoProxy(char * basemode_path, char * gamemode_path, char * gamemode_namespace, char * gamemode_class, char * runtime_version) {
	
	//Initialize the Mono runtime
	mono_set_dirs(PathUtil::GetLibDirectory().c_str(), PathUtil::GetConfigDirectory().c_str());
	mono_debug_init(MONO_DEBUG_FORMAT_MONO);
	m_pRootDomain = mono_jit_init_version("SAMP.Mono.Proxy", runtime_version);

	//Load the gamemode's assembly
	MonoAssembly * pMonoAssembly = mono_domain_assembly_open(mono_domain_get(), PathUtil::GetBinDirectory().append(gamemode_path).c_str());
	MonoAssembly * bMonoAssembly = mono_domain_assembly_open(mono_domain_get(), PathUtil::GetBinDirectory().append(basemode_path).c_str());

	m_pGameModeImage = mono_assembly_get_image(pMonoAssembly);
	m_pBaseModeImage = mono_assembly_get_image(bMonoAssembly);

	//Load all SA:MP natives
	LoadNatives(); 

	//Create instance of the gamemode's class
	m_pGameModeClass = mono_class_from_name(m_pGameModeImage, gamemode_namespace, gamemode_class);

	m_pGameMode = mono_object_new(mono_domain_get(), m_pGameModeClass);
	mono_runtime_object_init(m_pGameMode);

	//Load all SA:MP callbacks
	m_cOnGameModeInit = LoadCallback(gamemode_class, "OnGameModeInit()");
	m_cOnGameModeExit = LoadCallback(gamemode_class, "OnGameModeExit()");
	m_cOnPlayerConnect = LoadCallback(gamemode_class, "OnPlayerConnect(int)");
	m_cOnPlayerDisconnect = LoadCallback(gamemode_class, "OnPlayerDisconnect(int,int)");
	m_cOnPlayerSpawn = LoadCallback(gamemode_class, "OnPlayerSpawn(int)");
	m_cOnPlayerDeath = LoadCallback(gamemode_class, "OnPlayerDeath(int,int,int)");
	m_cOnVehicleSpawn = LoadCallback(gamemode_class, "OnVehicleSpawn(int)");
	m_cOnVehicleDeath = LoadCallback(gamemode_class, "OnVehicleDeath(int,int)");
	m_cOnPlayerText = LoadCallback(gamemode_class, "OnPlayerText(int,string)");
	m_cOnPlayerCommandText = LoadCallback(gamemode_class, "OnPlayerCommandText(int,string)");
	m_cOnPlayerRequestClass = LoadCallback(gamemode_class, "OnPlayerRequestClass(int,int)");
	m_cOnPlayerEnterVehicle = LoadCallback(gamemode_class, "OnPlayerEnterVehicle(int,int,bool)");
	m_cOnPlayerExitVehicle = LoadCallback(gamemode_class, "OnPlayerExitVehicle(int,int)");
	m_cOnPlayerStateChange = LoadCallback(gamemode_class, "OnPlayerStateChange(int,int,int)");
	m_cOnPlayerEnterCheckpoint = LoadCallback(gamemode_class, "OnPlayerEnterCheckpoint(int)");
	m_cOnPlayerLeaveCheckpoint = LoadCallback(gamemode_class, "OnPlayerLeaveCheckpoint(int)");
	m_cOnPlayerEnterRaceCheckpoint = LoadCallback(gamemode_class, "OnPlayerEnterRaceCheckpoint(int)");
	m_cOnPlayerLeaveRaceCheckpoint = LoadCallback(gamemode_class, "OnPlayerLeaveRaceCheckpoint(int)");
	m_cOnRconCommand = LoadCallback(gamemode_class, "OnRconCommand(string)");
	m_cOnPlayerRequestSpawn = LoadCallback(gamemode_class, "OnPlayerRequestSpawn(int)");
	m_cOnObjectMoved = LoadCallback(gamemode_class, "OnObjectMoved(int)");
	m_cOnPlayerObjectMoved = LoadCallback(gamemode_class, "OnPlayerObjectMoved(int,int)");
	m_cOnPlayerPickUpPickup = LoadCallback(gamemode_class, "OnPlayerPickUpPickup(int,int)");
	m_cOnVehicleMod = LoadCallback(gamemode_class, "OnVehicleMod(int,int,int)");
	m_cOnEnterExitModShop = LoadCallback(gamemode_class, "OnEnterExitModShop(int,int,int)");
	m_cOnVehiclePaintjob = LoadCallback(gamemode_class, "OnVehiclePaintjob(int,int,int)");
	m_cOnVehicleRespray = LoadCallback(gamemode_class, "OnVehicleRespray(int,int,int,int)");
	m_cOnVehicleDamageStatusUpdate = LoadCallback(gamemode_class, "OnVehicleDamageStatusUpdate(int,int)");
	m_cOnUnoccupiedVehicleUpdate = LoadCallback(gamemode_class, "OnUnoccupiedVehicleUpdate(int,int,int)");
	m_cOnPlayerSelectedMenuRow = LoadCallback(gamemode_class, "OnPlayerSelectedMenuRow(int,int)");
	m_cOnPlayerExitedMenu = LoadCallback(gamemode_class, "OnPlayerExitedMenu(int)");
	m_cOnPlayerInteriorChange = LoadCallback(gamemode_class, "OnPlayerInteriorChange(int,int,int)");
	m_cOnPlayerKeyStateChange = LoadCallback(gamemode_class, "OnPlayerKeyStateChange(int,int,int)");
	m_cOnRconLoginAttempt = LoadCallback(gamemode_class, "OnRconLoginAttempt(string,string,bool)");
	m_cOnPlayerUpdate = LoadCallback(gamemode_class, "OnPlayerUpdate(int)");
	m_cOnPlayerStreamIn = LoadCallback(gamemode_class, "OnPlayerStreamIn(int,int)");
	m_cOnPlayerStreamOut = LoadCallback(gamemode_class, "OnPlayerStreamOut(int,int)");
	m_cOnVehicleStreamIn = LoadCallback(gamemode_class, "OnVehicleStreamIn(int,int)");
	m_cOnVehicleStreamOut = LoadCallback(gamemode_class, "OnVehicleStreamOut(int,int)");
	m_cOnDialogResponse = LoadCallback(gamemode_class, "OnDialogResponse(int,int,int,int,string)");
	m_cOnPlayerTakeDamage = LoadCallback(gamemode_class, "OnPlayerTakeDamage(int,int,float,int,int)");
	m_cOnPlayerGiveDamage = LoadCallback(gamemode_class, "OnPlayerGiveDamage(int,int,float,int,int)");
	m_cOnPlayerClickMap = LoadCallback(gamemode_class, "OnPlayerClickMap(int,float,float,float)");
	m_cOnPlayerClickTextDraw = LoadCallback(gamemode_class, "OnPlayerClickTextDraw(int,int)");
	m_cOnPlayerClickPlayerTextDraw = LoadCallback(gamemode_class, "OnPlayerClickPlayerTextDraw(int,int)");
	m_cOnPlayerClickPlayer = LoadCallback(gamemode_class, "OnPlayerClickPlayer(int,int,int)");
	m_cOnPlayerEditObject = LoadCallback(gamemode_class, "OnPlayerEditObject(int,bool,int,int,float,float,float,float,float,float)");
	m_cOnPlayerEditAttachedObject = LoadCallback(gamemode_class, "OnPlayerEditAttachedObject(int,int,int,int,int,float,float,float,float,float,float,float,float,float)");
	m_cOnPlayerSelectObject = LoadCallback(gamemode_class, "OnPlayerSelectObject(int,int,int,int,float,float,float)");
	m_cOnPlayerWeaponShot = LoadCallback(gamemode_class, "OnPlayerWeaponShot(int,int,int,int,float,float,float)");
	m_cOnTimerTick = LoadCallback(gamemode_class, "OnTimerTick(int,object)");
}

MonoMethod * CMonoProxy::LoadCallback(const char * cname, const char * name) {
	//Construct method name
	char * cl_buffer = new char[256];
	char * bl_buffer = new char[256];

	sprintf(cl_buffer, "%s:%s", cname, name);
	sprintf(bl_buffer, "BaseMode:%s", name);

	//Create method description and find method in image
	MonoMethodDesc * m_methodDescription = mono_method_desc_new(cl_buffer, false);
	MonoMethod * m_method = mono_method_desc_search_in_image(m_methodDescription, m_pGameModeImage);
	mono_method_desc_free(m_methodDescription);
	if (!m_method)
	{
		m_methodDescription = mono_method_desc_new(bl_buffer, false);
		m_method = mono_method_desc_search_in_image(m_methodDescription, m_pBaseModeImage);
		mono_method_desc_free(m_methodDescription);
	}

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
