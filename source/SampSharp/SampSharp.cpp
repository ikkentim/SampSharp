#include <iostream>
#include <fstream>
#include <time.h>

#include "SampSharp.h"
#include "PathUtil.h"
#include "Natives.h"

using namespace std;

CSampSharp * CSampSharp::instance;

CSampSharp::CSampSharp(string bmPath, string gmPath, string gmNamespace, string gmClass, bool generateSymbols) {
	
	//Initialize the Mono runtime
	mono_set_dirs(PathUtil::GetLibDirectory().c_str(), PathUtil::GetConfigDirectory().c_str());
	
	mono_debug_init(MONO_DEBUG_FORMAT_MONO);
	rootDomain = mono_jit_init(PathUtil::GetPathInBin(gmPath).c_str());

	//Symbol generator
	if (generateSymbols == true) {
		//Construct path to pdb2mdb
		string mdbpath = PathUtil::GetBinDirectory().append("Mono/lib/mono/4.5/pdb2mdb.exe");
		char *cmdbpath = new char[mdbpath.size() + 1];
		std::strcpy(cmdbpath, mdbpath.c_str());

		MonoAssembly * mdbconverter = mono_domain_assembly_open(rootDomain, cmdbpath);
		if (mdbconverter) {
			int argc = 2;
			char * argv[2];
			argv[0] = cmdbpath;
			
			cout << "[SampSharp] Generating symbol file for " << gmPath << "." << endl;
			argv[1] = (char *) gmPath.c_str();
			mono_jit_exec(rootDomain, mdbconverter, argc, argv);

			cout << "[SampSharp] Generating symbol file for " << bmPath << "." << endl;
			argv[1] = (char *) bmPath.c_str();
			mono_jit_exec(rootDomain, mdbconverter, argc, argv);
		}

		delete cmdbpath;
	}

	//Load the gamemode's assembly
	MonoAssembly * pMonoAssembly = mono_domain_assembly_open(mono_domain_get(), PathUtil::GetPathInBin(gmPath).c_str());
	MonoAssembly * bMonoAssembly = mono_domain_assembly_open(mono_domain_get(), PathUtil::GetPathInBin(bmPath).c_str());

	gameModeImage = mono_assembly_get_image(pMonoAssembly);
	baseModeImage = mono_assembly_get_image(bMonoAssembly);

	//Load all SA:MP natives
	LoadNatives(); 

	//Create instance of the gamemode's class
	MonoClass * gameModeMonoClass = mono_class_from_name(gameModeImage, gmNamespace.c_str(), gmClass.c_str());
	MonoObject * gameMode = mono_object_new(mono_domain_get(), gameModeMonoClass);
	gameModeHandle = mono_gchandle_new(gameMode, true);
	mono_runtime_object_init(gameMode);

	//Load all SA:MP callbacks
	const char * gameModeClass = gmClass.c_str();
	onGameModeInit = LoadCallback(gameModeClass, "OnGameModeInit");
	onGameModeExit = LoadCallback(gameModeClass, "OnGameModeExit");
	onPlayerConnect = LoadCallback(gameModeClass, "OnPlayerConnect");
	onPlayerDisconnect = LoadCallback(gameModeClass, "OnPlayerDisconnect");
	onPlayerSpawn = LoadCallback(gameModeClass, "OnPlayerSpawn");
	onPlayerDeath = LoadCallback(gameModeClass, "OnPlayerDeath");
	onVehicleSpawn = LoadCallback(gameModeClass, "OnVehicleSpawn");
	onVehicleDeath = LoadCallback(gameModeClass, "OnVehicleDeath");
	onPlayerText = LoadCallback(gameModeClass, "OnPlayerText");
	onPlayerCommandText = LoadCallback(gameModeClass, "OnPlayerCommandText");
	onPlayerRequestClass = LoadCallback(gameModeClass, "OnPlayerRequestClass");
	onPlayerEnterVehicle = LoadCallback(gameModeClass, "OnPlayerEnterVehicle");
	onPlayerExitVehicle = LoadCallback(gameModeClass, "OnPlayerExitVehicle");
	onPlayerStateChange = LoadCallback(gameModeClass, "OnPlayerStateChange");
	onPlayerEnterCheckpoint = LoadCallback(gameModeClass, "OnPlayerEnterCheckpoint");
	onPlayerLeaveCheckpoint = LoadCallback(gameModeClass, "OnPlayerLeaveCheckpoint");
	onPlayerEnterRaceCheckpoint = LoadCallback(gameModeClass, "OnPlayerEnterRaceCheckpoint");
	onPlayerLeaveRaceCheckpoint = LoadCallback(gameModeClass, "OnPlayerLeaveRaceCheckpoint");
	onRconCommand = LoadCallback(gameModeClass, "OnRconCommand");
	onPlayerRequestSpawn = LoadCallback(gameModeClass, "OnPlayerRequestSpawn");
	onObjectMoved = LoadCallback(gameModeClass, "OnObjectMoved");
	onPlayerObjectMoved = LoadCallback(gameModeClass, "OnPlayerObjectMoved");
	onPlayerPickUpPickup = LoadCallback(gameModeClass, "OnPlayerPickUpPickup");
	onVehicleMod = LoadCallback(gameModeClass, "OnVehicleMod");
	onEnterExitModShop = LoadCallback(gameModeClass, "OnEnterExitModShop");
	onVehiclePaintjob = LoadCallback(gameModeClass, "OnVehiclePaintjob");
	onVehicleRespray = LoadCallback(gameModeClass, "OnVehicleRespray");
	onVehicleDamageStatusUpdate = LoadCallback(gameModeClass, "OnVehicleDamageStatusUpdate");
	onUnoccupiedVehicleUpdate = LoadCallback(gameModeClass, "OnUnoccupiedVehicleUpdate");
	onPlayerSelectedMenuRow = LoadCallback(gameModeClass, "OnPlayerSelectedMenuRow");
	onPlayerExitedMenu = LoadCallback(gameModeClass, "OnPlayerExitedMenu");
	onPlayerInteriorChange = LoadCallback(gameModeClass, "OnPlayerInteriorChange");
	onPlayerKeyStateChange = LoadCallback(gameModeClass, "OnPlayerKeyStateChange");
	onRconLoginAttempt = LoadCallback(gameModeClass, "OnRconLoginAttempt");
	onPlayerUpdate = LoadCallback(gameModeClass, "OnPlayerUpdate");
	onPlayerStreamIn = LoadCallback(gameModeClass, "OnPlayerStreamIn");
	onPlayerStreamOut = LoadCallback(gameModeClass, "OnPlayerStreamOut");
	onVehicleStreamIn = LoadCallback(gameModeClass, "OnVehicleStreamIn");
	onVehicleStreamOut = LoadCallback(gameModeClass, "OnVehicleStreamOut");
	onDialogResponse = LoadCallback(gameModeClass, "OnDialogResponse");
	onPlayerTakeDamage = LoadCallback(gameModeClass, "OnPlayerTakeDamage");
	onPlayerGiveDamage = LoadCallback(gameModeClass, "OnPlayerGiveDamage");
	onPlayerClickMap = LoadCallback(gameModeClass, "OnPlayerClickMap");
	onPlayerClickTextDraw = LoadCallback(gameModeClass, "OnPlayerClickTextDraw");
	onPlayerClickPlayerTextDraw = LoadCallback(gameModeClass, "OnPlayerClickPlayerTextDraw");
	onPlayerClickPlayer = LoadCallback(gameModeClass, "OnPlayerClickPlayer");
	onPlayerEditObject = LoadCallback(gameModeClass, "OnPlayerEditObject");
	onPlayerEditAttachedObject = LoadCallback(gameModeClass, "OnPlayerEditAttachedObject");
	onPlayerSelectObject = LoadCallback(gameModeClass, "OnPlayerSelectObject");
	onPlayerWeaponShot = LoadCallback(gameModeClass, "OnPlayerWeaponShot");
	onIncomingConnection = LoadCallback(gameModeClass, "OnIncomingConnection");
	onTimerTick = LoadCallback(gameModeClass, "OnTimerTick");
	onTick = LoadCallback(gameModeClass, "OnTick");
}

char * CSampSharp::GetTimeStamp() {
	time_t now = time(0);
	struct tm tstruct;
	char timestamp[32];
	tstruct = *localtime(&now);
	strftime(timestamp, sizeof(timestamp), "[%d/%m/%Y %H:%M:%S]", &tstruct);

	char * cpr = new char[32];
	strcpy(cpr, timestamp);
	return cpr;
}

MonoMethod * CSampSharp::LoadCallback(const char * cname, const char * name) {
	//Construct method name
	char * cl_buffer = new char[256];
	char * bl_buffer = new char[256];
	sprintf(cl_buffer, "%s:%s", cname, name);
	sprintf(bl_buffer, "BaseMode:%s", name);

	//Look for the method in the gamemode image
	MonoMethodDesc * m_methodDescription = mono_method_desc_new(cl_buffer, false);
	MonoMethod * m_method = mono_method_desc_search_in_image(m_methodDescription, gameModeImage);
	mono_method_desc_free(m_methodDescription);

	//If not found, look in base image
	if (!m_method)
	{
		m_methodDescription = mono_method_desc_new(bl_buffer, false);
		m_method = mono_method_desc_search_in_image(m_methodDescription, baseModeImage);
		mono_method_desc_free(m_methodDescription);
	}

	//Recheck if method has been found or log it
	if (!m_method)
	{
		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app);
		cout << "[SampSharp] ERROR: Method '" << name << "' not found in image!" << endl;
		logfile << GetTimeStamp() << "ERROR: Method '" << name << "' not found in image!" << endl;
		logfile.close();
	}
	return m_method;
}

bool CSampSharp::CallCallback(MonoMethod* method, void **params) {

	MonoObject * exception = NULL;
	MonoObject * response;
	MonoObject * gameMode = mono_gchandle_get_target(gameModeHandle);

	//Check for a method
	if (!method) {
		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app);
		cout << "[SampSharp] ERROR: No method given in CallCallback!" << endl;
		logfile << GetTimeStamp() << "ERROR: No method given in CallCallback!" << endl;
		logfile.close();
		return false;
	}

	//Check the gamemode opbject
	if (!gameMode) {
		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app);
		cout << "[SampSharp] ERROR: GameMode object disappeared!" << endl;
		logfile << GetTimeStamp() << "ERROR: GameMode object disappeared!" << endl;
		logfile.close();
		return false;
	}

	//Call callback
	response = mono_runtime_invoke(method, gameMode, params, &exception);

	if(exception){
		MonoString * str = mono_object_to_string(exception, NULL);
		char * exc = mono_string_to_utf8(str);

		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app);
		cout << "[SampSharp] Uncought exception:" << exc << endl;
		logfile << GetTimeStamp() << "Uncought exception:" << exc << endl;
		logfile.close();
	}

	if (!response){
		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app);
		cout << "[SampSharp] ERROR: No response given in CallCallback!" << endl;
		logfile << GetTimeStamp() << "ERROR: No response given in CallCallback!" << endl;
		logfile.close();
		return false; //Default return value
	}

	//Catch exceptions
	if (exception) {
		char * stacktrace = mono_string_to_utf8(mono_object_to_string(exception, NULL));

		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app);
		cout << "[SampSharp] Exception thrown:" << endl << stacktrace << endl;
		logfile << GetTimeStamp() << " Exception thrown:" << endl << stacktrace << endl;
		logfile.close();

		return false; //Default return value
	}

	//Cast response to bool and return it.
	return *(bool *)mono_object_unbox(response);
}

CSampSharp::~CSampSharp() {
	//Cleanup Mono runtime
	mono_jit_cleanup(mono_domain_get());
}
