#include "SampSharp.h"
#include "PathUtil.h"
#include "Natives.h"

using namespace std;

MonoMethod * SampSharp::onGameModeInit;
MonoMethod * SampSharp::onGameModeExit;
MonoMethod * SampSharp::onPlayerConnect;
MonoMethod * SampSharp::onPlayerDisconnect;
MonoMethod * SampSharp::onPlayerSpawn;
MonoMethod * SampSharp::onPlayerDeath;
MonoMethod * SampSharp::onVehicleSpawn;
MonoMethod * SampSharp::onVehicleDeath;
MonoMethod * SampSharp::onPlayerText;
MonoMethod * SampSharp::onPlayerCommandText;
MonoMethod * SampSharp::onPlayerRequestClass;
MonoMethod * SampSharp::onPlayerEnterVehicle;
MonoMethod * SampSharp::onPlayerExitVehicle;
MonoMethod * SampSharp::onPlayerStateChange;
MonoMethod * SampSharp::onPlayerEnterCheckpoint;
MonoMethod * SampSharp::onPlayerLeaveCheckpoint;
MonoMethod * SampSharp::onPlayerEnterRaceCheckpoint;
MonoMethod * SampSharp::onPlayerLeaveRaceCheckpoint;
MonoMethod * SampSharp::onRconCommand;
MonoMethod * SampSharp::onPlayerRequestSpawn;
MonoMethod * SampSharp::onObjectMoved;
MonoMethod * SampSharp::onPlayerObjectMoved;
MonoMethod * SampSharp::onPlayerPickUpPickup;
MonoMethod * SampSharp::onVehicleMod;
MonoMethod * SampSharp::onEnterExitModShop;
MonoMethod * SampSharp::onVehiclePaintjob;
MonoMethod * SampSharp::onVehicleRespray;
MonoMethod * SampSharp::onVehicleDamageStatusUpdate;
MonoMethod * SampSharp::onUnoccupiedVehicleUpdate;
MonoMethod * SampSharp::onPlayerSelectedMenuRow;
MonoMethod * SampSharp::onPlayerExitedMenu;
MonoMethod * SampSharp::onPlayerInteriorChange;
MonoMethod * SampSharp::onPlayerKeyStateChange;
MonoMethod * SampSharp::onRconLoginAttempt;
MonoMethod * SampSharp::onPlayerUpdate;
MonoMethod * SampSharp::onPlayerStreamIn;
MonoMethod * SampSharp::onPlayerStreamOut;
MonoMethod * SampSharp::onVehicleStreamIn;
MonoMethod * SampSharp::onVehicleStreamOut;
MonoMethod * SampSharp::onDialogResponse;
MonoMethod * SampSharp::onPlayerTakeDamage;
MonoMethod * SampSharp::onPlayerGiveDamage;
MonoMethod * SampSharp::onPlayerClickMap;
MonoMethod * SampSharp::onPlayerClickTextDraw;
MonoMethod * SampSharp::onPlayerClickPlayerTextDraw;
MonoMethod * SampSharp::onPlayerClickPlayer;
MonoMethod * SampSharp::onPlayerEditObject;
MonoMethod * SampSharp::onPlayerEditAttachedObject;
MonoMethod * SampSharp::onPlayerSelectObject;
MonoMethod * SampSharp::onPlayerWeaponShot;
MonoMethod * SampSharp::onIncomingConnection;
MonoMethod * SampSharp::onTimerTick;
MonoMethod * SampSharp::onTick;
MonoMethod * SampSharp::dispose;

MonoImage * SampSharp::gameModeImage;
MonoImage * SampSharp::baseModeImage;
MonoDomain * SampSharp::rootDomain;
uint32_t SampSharp::gameModeHandle;

void SampSharp::Load(string baseModePath, string gameModePath, string gameModeNamespace, string gameModeClass, bool debug) {

	#ifdef _WIN32
	//On windows, use the embedded mono tools
	mono_set_dirs(PathUtil::GetLibDirectory().c_str(), PathUtil::GetConfigDirectory().c_str());
	#endif

	mono_debug_init(MONO_DEBUG_FORMAT_MONO);

	//Initialize mono runtime
	rootDomain = mono_jit_init(PathUtil::GetPathInBin(gameModePath).c_str());

	//Generate symbols if needed
	if (debug == true) {
		GenerateSymbols(baseModePath);
		GenerateSymbols(gameModePath);
	}

	//Load the gamemode's assembly
	gameModeImage = mono_assembly_get_image(mono_domain_assembly_open(mono_domain_get(), (char *)PathUtil::GetPathInBin(gameModePath).c_str()));
	baseModeImage = mono_assembly_get_image(mono_domain_assembly_open(mono_domain_get(), (char *)PathUtil::GetPathInBin(baseModePath).c_str()));

	//Load all sa-mp natives
	LoadNatives(); 

	//Create instance of the gamemode's class
	MonoClass * gameModeClassType = mono_class_from_name(gameModeImage, gameModeNamespace.c_str(), gameModeClass.c_str());
	MonoObject * gameModeObject = mono_object_new(mono_domain_get(), gameModeClassType);
	gameModeHandle = mono_gchandle_new(gameModeObject, true);
	mono_runtime_object_init(gameModeObject);

	//Load all sa-mp events
	LoadEvents(gameModeClass.c_str());
}

void SampSharp::LoadEvents(const char * gmClass)
{
	onGameModeInit = LoadEvent(gmClass, "OnGameModeInit");
	onGameModeExit = LoadEvent(gmClass, "OnGameModeExit");
	onPlayerConnect = LoadEvent(gmClass, "OnPlayerConnect");
	onPlayerDisconnect = LoadEvent(gmClass, "OnPlayerDisconnect");
	onPlayerSpawn = LoadEvent(gmClass, "OnPlayerSpawn");
	onPlayerDeath = LoadEvent(gmClass, "OnPlayerDeath");
	onVehicleSpawn = LoadEvent(gmClass, "OnVehicleSpawn");
	onVehicleDeath = LoadEvent(gmClass, "OnVehicleDeath");
	onPlayerText = LoadEvent(gmClass, "OnPlayerText");
	onPlayerCommandText = LoadEvent(gmClass, "OnPlayerCommandText");
	onPlayerRequestClass = LoadEvent(gmClass, "OnPlayerRequestClass");
	onPlayerEnterVehicle = LoadEvent(gmClass, "OnPlayerEnterVehicle");
	onPlayerExitVehicle = LoadEvent(gmClass, "OnPlayerExitVehicle");
	onPlayerStateChange = LoadEvent(gmClass, "OnPlayerStateChange");
	onPlayerEnterCheckpoint = LoadEvent(gmClass, "OnPlayerEnterCheckpoint");
	onPlayerLeaveCheckpoint = LoadEvent(gmClass, "OnPlayerLeaveCheckpoint");
	onPlayerEnterRaceCheckpoint = LoadEvent(gmClass, "OnPlayerEnterRaceCheckpoint");
	onPlayerLeaveRaceCheckpoint = LoadEvent(gmClass, "OnPlayerLeaveRaceCheckpoint");
	onRconCommand = LoadEvent(gmClass, "OnRconCommand");
	onPlayerRequestSpawn = LoadEvent(gmClass, "OnPlayerRequestSpawn");
	onObjectMoved = LoadEvent(gmClass, "OnObjectMoved");
	onPlayerObjectMoved = LoadEvent(gmClass, "OnPlayerObjectMoved");
	onPlayerPickUpPickup = LoadEvent(gmClass, "OnPlayerPickUpPickup");
	onVehicleMod = LoadEvent(gmClass, "OnVehicleMod");
	onEnterExitModShop = LoadEvent(gmClass, "OnEnterExitModShop");
	onVehiclePaintjob = LoadEvent(gmClass, "OnVehiclePaintjob");
	onVehicleRespray = LoadEvent(gmClass, "OnVehicleRespray");
	onVehicleDamageStatusUpdate = LoadEvent(gmClass, "OnVehicleDamageStatusUpdate");
	onUnoccupiedVehicleUpdate = LoadEvent(gmClass, "OnUnoccupiedVehicleUpdate");
	onPlayerSelectedMenuRow = LoadEvent(gmClass, "OnPlayerSelectedMenuRow");
	onPlayerExitedMenu = LoadEvent(gmClass, "OnPlayerExitedMenu");
	onPlayerInteriorChange = LoadEvent(gmClass, "OnPlayerInteriorChange");
	onPlayerKeyStateChange = LoadEvent(gmClass, "OnPlayerKeyStateChange");
	onRconLoginAttempt = LoadEvent(gmClass, "OnRconLoginAttempt");
	onPlayerUpdate = LoadEvent(gmClass, "OnPlayerUpdate");
	onPlayerStreamIn = LoadEvent(gmClass, "OnPlayerStreamIn");
	onPlayerStreamOut = LoadEvent(gmClass, "OnPlayerStreamOut");
	onVehicleStreamIn = LoadEvent(gmClass, "OnVehicleStreamIn");
	onVehicleStreamOut = LoadEvent(gmClass, "OnVehicleStreamOut");
	onDialogResponse = LoadEvent(gmClass, "OnDialogResponse");
	onPlayerTakeDamage = LoadEvent(gmClass, "OnPlayerTakeDamage");
	onPlayerGiveDamage = LoadEvent(gmClass, "OnPlayerGiveDamage");
	onPlayerClickMap = LoadEvent(gmClass, "OnPlayerClickMap");
	onPlayerClickTextDraw = LoadEvent(gmClass, "OnPlayerClickTextDraw");
	onPlayerClickPlayerTextDraw = LoadEvent(gmClass, "OnPlayerClickPlayerTextDraw");
	onPlayerClickPlayer = LoadEvent(gmClass, "OnPlayerClickPlayer");
	onPlayerEditObject = LoadEvent(gmClass, "OnPlayerEditObject");
	onPlayerEditAttachedObject = LoadEvent(gmClass, "OnPlayerEditAttachedObject");
	onPlayerSelectObject = LoadEvent(gmClass, "OnPlayerSelectObject");
	onPlayerWeaponShot = LoadEvent(gmClass, "OnPlayerWeaponShot");
	onIncomingConnection = LoadEvent(gmClass, "OnIncomingConnection");
	onTimerTick = LoadEvent(gmClass, "OnTimerTick");
	onTick = LoadEvent(gmClass, "OnTick");
	dispose = LoadEvent(gmClass, "Dispose");
}

void SampSharp::GenerateSymbols(string path)
{
	#ifdef _WIN32
	string mdbpath = PathUtil::GetLibDirectory().append("mono/4.5/pdb2mdb.exe");
	char *cmdbpath = new char[mdbpath.size() + 1];
	strcpy(cmdbpath, mdbpath.c_str());
	
	MonoAssembly * mdbconverter = mono_domain_assembly_open(rootDomain, cmdbpath);
	if (mdbconverter) {
		char * argv[2];
		argv[0] = cmdbpath;
		argv[1] = (char *) path.c_str();

		sampgdk::logprintf("[SampSharp] Generating symbol file for %s.", argv[1]);
		mono_jit_exec(rootDomain, mdbconverter, 2, argv);
	}

	delete cmdbpath;
	#endif
}

char * GetTimeStamp() {
	//Get current time
	time_t now = time(0);

	//Format timestamp 
	char timestamp[32];
	
	strftime(timestamp, sizeof(timestamp), "[%d/%m/%Y %H:%M:%S]", localtime(&now));

	char * timestamp2 = new char[32];
	strcpy(timestamp2, timestamp);
	return  timestamp2;
}

MonoMethod * SampSharp::LoadEvent(const char * className, const char * name) {
	//Construct method name
	char * gamemodeBuffer = new char[256];
	char * basemodeBuffer = new char[256];
	sprintf(gamemodeBuffer, "%s:%s", className, name);
	sprintf(basemodeBuffer, "BaseMode:%s", name);

	//Look for the method in the gamemode image
	MonoMethodDesc * methodDescription = mono_method_desc_new(gamemodeBuffer, false);
	MonoMethod * method = mono_method_desc_search_in_image(methodDescription, gameModeImage);
	mono_method_desc_free(methodDescription);

	//If not found, look in base image
	if (!method) {
		methodDescription = mono_method_desc_new(basemodeBuffer, false);
		method = mono_method_desc_search_in_image(methodDescription, baseModeImage);
		mono_method_desc_free(methodDescription);

		//Recheck if method has been found or log it
		if (!method) {
			ofstream logfile;
			logfile.open("SampSharp_errors.log", ios::app);
			cout << "[SampSharp] ERROR: Method '" << name << "' not found in image!" << endl;
			logfile << GetTimeStamp() << "ERROR: Method '" << name << "' not found in image!" << endl;
			logfile.close();
		}
	}

	return method;
}

bool SampSharp::CallEvent(MonoMethod* method, void **params) {
	MonoObject * exception = NULL;

	//Check for a method or report to the log
	if (!method) {
		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app);
		cout << "[SampSharp] ERROR: No method given in CallEvent!" << endl;
		logfile << GetTimeStamp() << "ERROR: No method given in CallEvent!" << endl;
		logfile.close();

		return false;
	}

	//Invoke method in mono runtime
	MonoObject * response = mono_runtime_invoke(method, mono_gchandle_get_target(gameModeHandle), params, &exception);

	//Catch exceptions and report to the log
	if (exception) {
		char * stacktrace = mono_string_to_utf8(mono_object_to_string(exception, NULL));

		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app | ios::binary);
		cout << "[SampSharp] Exception thrown:" << endl << stacktrace << endl;
		logfile << GetTimeStamp() << " Exception thrown:" << "\r\n" << stacktrace << "\r\n";
		
		logfile.close();

		return false; //Default return value
	}

	//If no response has been given, report to the log
	if (!response) {
		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app);
		cout << "[SampSharp] ERROR: No response given in CallEvent!" << endl;
		logfile << GetTimeStamp() << "ERROR: No response given in CallEvent!" << endl;
		logfile.close();

		return false; //Default return value
	}

	//Cast response to bool and return it.
	return *(bool *)mono_object_unbox(response);
}

void SampSharp::Unload() {
	//Cleanup mono runtime
	mono_jit_cleanup(mono_domain_get());
}