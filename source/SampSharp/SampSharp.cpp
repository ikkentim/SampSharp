#include <iostream>
#include <fstream>
#include <time.h>

#include "SampSharp.h"
#include "PathUtil.h"
#include "Natives.h"

using namespace std;

CSampSharp * CSampSharp::instance;

CSampSharp::CSampSharp(char * basemode_path, char * gamemode_path, char * gamemode_namespace, char * gamemode_class, char * runtime_version, bool generate_symbols) {
	
	//Initialize the Mono runtime
	mono_set_dirs(PathUtil::GetLibDirectory().c_str(), PathUtil::GetConfigDirectory().c_str());
	mono_debug_init(MONO_DEBUG_FORMAT_MONO);
	rootDomain = mono_jit_init_version("SampSharp", runtime_version);

	if (generate_symbols == true) {
		string mdbpath = PathUtil::GetBinDirectory().append("Mono/lib/mono/4.5/pdb2mdb.exe");
		char *cmdbpath = new char[mdbpath.size() + 1];
		std::strcpy(cmdbpath, mdbpath.c_str());

		MonoAssembly * mdbconverter = mono_domain_assembly_open(rootDomain, cmdbpath);

		if (mdbconverter) {
			int argc = 2;
			char * argv[2];
			argv[0] = cmdbpath;
			
			cout << "[SampSharp] Generating symbol file for " << gamemode_path << "." << endl;
			argv[1] = gamemode_path;
			mono_jit_exec(rootDomain, mdbconverter, argc, argv);

			cout << "[SampSharp] Generating symbol file for " << basemode_path << "." << endl;
			argv[1] = basemode_path;
			mono_jit_exec(rootDomain, mdbconverter, argc, argv);
		}

		delete cmdbpath;
	}
	//Load the gamemode's assembly
	MonoAssembly * pMonoAssembly = mono_domain_assembly_open(mono_domain_get(), PathUtil::GetBinDirectory().append(gamemode_path).c_str());
	MonoAssembly * bMonoAssembly = mono_domain_assembly_open(mono_domain_get(), PathUtil::GetBinDirectory().append(basemode_path).c_str());

	gameModeImage = mono_assembly_get_image(pMonoAssembly);
	baseModeImage = mono_assembly_get_image(bMonoAssembly);

	//Load all SA:MP natives
	LoadNatives(); 

	//Create instance of the gamemode's class
	gameModeClass = mono_class_from_name(gameModeImage, gamemode_namespace, gamemode_class);

	gameMode = mono_object_new(mono_domain_get(), gameModeClass);
	mono_runtime_object_init(gameMode);

	//Load all SA:MP callbacks
	onGameModeInit = LoadCallback(gamemode_class, "OnGameModeInit()");
	onGameModeExit = LoadCallback(gamemode_class, "OnGameModeExit()");
	onPlayerConnect = LoadCallback(gamemode_class, "OnPlayerConnect(int)");
	onPlayerDisconnect = LoadCallback(gamemode_class, "OnPlayerDisconnect(int,int)");
	onPlayerSpawn = LoadCallback(gamemode_class, "OnPlayerSpawn(int)");
	onPlayerDeath = LoadCallback(gamemode_class, "OnPlayerDeath(int,int,int)");
	onVehicleSpawn = LoadCallback(gamemode_class, "OnVehicleSpawn(int)");
	onVehicleDeath = LoadCallback(gamemode_class, "OnVehicleDeath(int,int)");
	onPlayerText = LoadCallback(gamemode_class, "OnPlayerText(int,string)");
	onPlayerCommandText = LoadCallback(gamemode_class, "OnPlayerCommandText(int,string)");
	onPlayerRequestClass = LoadCallback(gamemode_class, "OnPlayerRequestClass(int,int)");
	onPlayerEnterVehicle = LoadCallback(gamemode_class, "OnPlayerEnterVehicle(int,int,bool)");
	onPlayerExitVehicle = LoadCallback(gamemode_class, "OnPlayerExitVehicle(int,int)");
	onPlayerStateChange = LoadCallback(gamemode_class, "OnPlayerStateChange(int,int,int)");
	onPlayerEnterCheckpoint = LoadCallback(gamemode_class, "OnPlayerEnterCheckpoint(int)");
	onPlayerLeaveCheckpoint = LoadCallback(gamemode_class, "OnPlayerLeaveCheckpoint(int)");
	onPlayerEnterRaceCheckpoint = LoadCallback(gamemode_class, "OnPlayerEnterRaceCheckpoint(int)");
	onPlayerLeaveRaceCheckpoint = LoadCallback(gamemode_class, "OnPlayerLeaveRaceCheckpoint(int)");
	onRconCommand = LoadCallback(gamemode_class, "OnRconCommand(string)");
	onPlayerRequestSpawn = LoadCallback(gamemode_class, "OnPlayerRequestSpawn(int)");
	onObjectMoved = LoadCallback(gamemode_class, "OnObjectMoved(int)");
	onPlayerObjectMoved = LoadCallback(gamemode_class, "OnPlayerObjectMoved(int,int)");
	onPlayerPickUpPickup = LoadCallback(gamemode_class, "OnPlayerPickUpPickup(int,int)");
	onVehicleMod = LoadCallback(gamemode_class, "OnVehicleMod(int,int,int)");
	onEnterExitModShop = LoadCallback(gamemode_class, "OnEnterExitModShop(int,int,int)");
	onVehiclePaintjob = LoadCallback(gamemode_class, "OnVehiclePaintjob(int,int,int)");
	onVehicleRespray = LoadCallback(gamemode_class, "OnVehicleRespray(int,int,int,int)");
	onVehicleDamageStatusUpdate = LoadCallback(gamemode_class, "OnVehicleDamageStatusUpdate(int,int)");
	onUnoccupiedVehicleUpdate = LoadCallback(gamemode_class, "OnUnoccupiedVehicleUpdate(int,int,int)");
	onPlayerSelectedMenuRow = LoadCallback(gamemode_class, "OnPlayerSelectedMenuRow(int,int)");
	onPlayerExitedMenu = LoadCallback(gamemode_class, "OnPlayerExitedMenu(int)");
	onPlayerInteriorChange = LoadCallback(gamemode_class, "OnPlayerInteriorChange(int,int,int)");
	onPlayerKeyStateChange = LoadCallback(gamemode_class, "OnPlayerKeyStateChange(int,int,int)");
	onRconLoginAttempt = LoadCallback(gamemode_class, "OnRconLoginAttempt(string,string,bool)");
	onPlayerUpdate = LoadCallback(gamemode_class, "OnPlayerUpdate(int)");
	onPlayerStreamIn = LoadCallback(gamemode_class, "OnPlayerStreamIn(int,int)");
	onPlayerStreamOut = LoadCallback(gamemode_class, "OnPlayerStreamOut(int,int)");
	onVehicleStreamIn = LoadCallback(gamemode_class, "OnVehicleStreamIn(int,int)");
	onVehicleStreamOut = LoadCallback(gamemode_class, "OnVehicleStreamOut(int,int)");
	onDialogResponse = LoadCallback(gamemode_class, "OnDialogResponse(int,int,int,int,string)");
	onPlayerTakeDamage = LoadCallback(gamemode_class, "OnPlayerTakeDamage(int,int,float,int,int)");
	onPlayerGiveDamage = LoadCallback(gamemode_class, "OnPlayerGiveDamage(int,int,float,int,int)");
	onPlayerClickMap = LoadCallback(gamemode_class, "OnPlayerClickMap(int,float,float,float)");
	onPlayerClickTextDraw = LoadCallback(gamemode_class, "OnPlayerClickTextDraw(int,int)");
	onPlayerClickPlayerTextDraw = LoadCallback(gamemode_class, "OnPlayerClickPlayerTextDraw(int,int)");
	onPlayerClickPlayer = LoadCallback(gamemode_class, "OnPlayerClickPlayer(int,int,int)");
	onPlayerEditObject = LoadCallback(gamemode_class, "OnPlayerEditObject(int,bool,int,int,float,float,float,float,float,float)");
	onPlayerEditAttachedObject = LoadCallback(gamemode_class, "OnPlayerEditAttachedObject(int,int,int,int,int,float,float,float,float,float,float,float,float,float)");
	onPlayerSelectObject = LoadCallback(gamemode_class, "OnPlayerSelectObject(int,int,int,int,float,float,float)");
	onPlayerWeaponShot = LoadCallback(gamemode_class, "OnPlayerWeaponShot(int,int,int,int,float,float,float)");
	onTimerTick = LoadCallback(gamemode_class, "OnTimerTick(int,object)");
}

MonoMethod * CSampSharp::LoadCallback(const char * cname, const char * name) {
	//Construct method name
	char * cl_buffer = new char[256];
	char * bl_buffer = new char[256];

	sprintf(cl_buffer, "%s:%s", cname, name);
	sprintf(bl_buffer, "BaseMode:%s", name);

	//Create method description and find method in image
	MonoMethodDesc * m_methodDescription = mono_method_desc_new(cl_buffer, false);
	MonoMethod * m_method = mono_method_desc_search_in_image(m_methodDescription, gameModeImage);
	mono_method_desc_free(m_methodDescription);
	if (!m_method)
	{
		m_methodDescription = mono_method_desc_new(bl_buffer, false);
		m_method = mono_method_desc_search_in_image(m_methodDescription, baseModeImage);
		mono_method_desc_free(m_methodDescription);
	}

	return m_method;
}

bool CSampSharp::CallCallback(MonoMethod* method, void **params) {
	//Call callback
	MonoObject * exception = NULL;
	MonoObject * response = mono_runtime_invoke(method, gameMode, params, &exception);

	//Catch exceptions
	if (exception) {
		char * stacktrace = mono_string_to_utf8(mono_object_to_string(exception, NULL));
		cout << "Exception thrown:\r\n " << stacktrace << endl;
		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app);

		time_t     now = time(0);
		struct tm  tstruct;
		char       timestamp[80];
		tstruct = *localtime(&now);
		strftime(timestamp, sizeof(timestamp), "[%d/%m/%Y %H:%M:%S]", &tstruct);

		logfile << timestamp << " Exception thrown:" << endl << stacktrace << endl;
		logfile.close();
		return false; //Default return value
	}

	//cast response to bool and return it.
	return *(bool *)mono_object_unbox(response);
}

CSampSharp::~CSampSharp() {
	//Cleanup Mono runtime
	mono_jit_cleanup(mono_domain_get());
}
