#include "SampSharp.h"

#include <mono/metadata/threads.h>
#include "PathUtil.h"
#include "Natives.h"

using namespace std;

MonoMethod *SampSharp::onTimerTick;
MonoMethod *SampSharp::onTick;

MonoDomain *SampSharp::rootDomain;

MonoImage *SampSharp::gameModeImage;
MonoImage *SampSharp::baseModeImage;

MonoClass *SampSharp::gameModeClassType;
MonoClass *SampSharp::baseModeClassType;

uint32_t SampSharp::gameModeHandle;

EventMap SampSharp::events;

void SampSharp::Load(string baseModePath, string gameModePath, string gameModeNamespace, string gameModeClass, bool debug) {

	#ifdef _WIN32
	//On windows, use the embedded mono tools
	mono_set_dirs(PathUtil::GetLibDirectory().c_str(), PathUtil::GetConfigDirectory().c_str());
	#endif

	mono_debug_init(MONO_DEBUG_FORMAT_MONO);

	//Initialize mono runtime
	rootDomain = mono_jit_init(PathUtil::GetPathInBin(gameModePath).c_str());

	//Generate symbols if needed
	#ifdef _WIN32
	if (debug == true) {
		GenerateSymbols(baseModePath);
		GenerateSymbols(gameModePath);
	}
	#endif

	//Load the gamemode's assembly
	gameModeImage = mono_assembly_get_image(mono_domain_assembly_open(mono_domain_get(), (char *)PathUtil::GetPathInBin(gameModePath).c_str()));
	baseModeImage = mono_assembly_get_image(mono_domain_assembly_open(mono_domain_get(), (char *)PathUtil::GetPathInBin(baseModePath).c_str()));

	//Load all sa-mp natives
	LoadNatives(); 

	//Create instance of the gamemode's class
	baseModeClassType = mono_class_from_name(baseModeImage, "SampSharp.GameMode", "BaseMode");
	gameModeClassType = mono_class_from_name(gameModeImage, gameModeNamespace.c_str(), gameModeClass.c_str());

	MonoObject *gameModeObject = mono_object_new(mono_domain_get(), gameModeClassType);
	gameModeHandle = mono_gchandle_new(gameModeObject, true);
	mono_runtime_object_init(gameModeObject);

	//Load tick events
	onTimerTick = LoadEvent(gameModeClass.c_str(), "OnTimerTick");
	onTick = LoadEvent(gameModeClass.c_str(), "OnTick");
}

#ifdef _WIN32
void SampSharp::GenerateSymbols(string path)
{
	
	string mdbpath = PathUtil::GetLibDirectory().append("mono/4.5/pdb2mdb.exe");
	char *cmdbpath = new char[mdbpath.size() + 1];
	strcpy(cmdbpath, mdbpath.c_str());
	
	MonoAssembly *mdbconverter = mono_domain_assembly_open(rootDomain, cmdbpath);
	if (mdbconverter) {
		char *argv[2];
		argv[0] = cmdbpath;
		argv[1] = (char *) path.c_str();

		sampgdk::logprintf("[SampSharp] Generating symbol file for %s.", argv[1]);
		mono_jit_exec(rootDomain, mdbconverter, 2, argv);
	}

	delete cmdbpath;
}
#endif

char *GetTimeStamp() {
	//Get current time
	time_t now = time(0);

	//Format timestamp 
	char timestamp[32];
	
	strftime(timestamp, sizeof(timestamp), "[%d/%m/%Y %H:%M:%S]", localtime(&now));

	char *timestamp2 = new char[32];
	strcpy(timestamp2, timestamp);
	return  timestamp2;
}

MonoMethod *SampSharp::LoadEvent(const char *className, const char *name) {
	char *gamemodeBuffer = new char[128];
	char *basemodeBuffer = new char[128];
	sprintf(gamemodeBuffer, "%s:%s", className, name);
	sprintf(basemodeBuffer, "BaseMode:%s", name);

	MonoMethodDesc *methodDescription = mono_method_desc_new(gamemodeBuffer, false);
	MonoMethod *method = mono_method_desc_search_in_image(methodDescription, gameModeImage);
	mono_method_desc_free(methodDescription);

	if (!method) {
		methodDescription = mono_method_desc_new(basemodeBuffer, false);
		method = mono_method_desc_search_in_image(methodDescription, baseModeImage);
		mono_method_desc_free(methodDescription);

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

bool SampSharp::HandleEvent(AMX *amx, const char *name, cell *params, cell *retval) {
	const int param_count = params[0] / sizeof(cell);

	if (param_count > 16) {
		return true;
	}

	mono_thread_attach(SampSharp::rootDomain);

	if (events.find(name) == events.end())
	{
		MonoMethod *m_method = mono_class_get_method_from_name(gameModeClassType, name, param_count);
		bool useBaseModeImage = false;
		if (!m_method) {
			m_method = mono_class_get_method_from_name(baseModeClassType, name, param_count);
			useBaseModeImage = true;
		}

		if (!m_method){
			events[name] = NULL;
			return true;
		}

		uint32_t token = mono_method_get_token(m_method);
		MonoMethodSignature *sig = mono_method_get_signature(m_method, useBaseModeImage ? baseModeImage : gameModeImage, token);
		
		void *iter = NULL;
		string format = "";
		MonoType* type = NULL;
		
		while (type = mono_signature_get_params(sig, &iter)) {
			string type_name = mono_type_get_name(type);

			if (!type_name.compare("System.Int32")) {
				format.append("i");	
			}
			else if (!type_name.compare("System.Single")) {
				format.append("f");
			}
			else if (!type_name.compare("System.String")) {
				format.append("s");
			}
			else if (!type_name.compare("System.Boolean")) {
				format.append("b");
			}
			else {
				events[name] = NULL;
				return true;
			}
		}
		
		event_t *event_add = new event_t;
		event_add->method = m_method;
		event_add->format = format;
		events[name] = event_add;
	}

	event_t *event_p = events[name];
	
	if (event_p)
	{
		if (!param_count) {
			int retint = SampSharp::CallEvent(event_p->method, NULL);
			if (retint != -1) {
				*retval = retint;
			}
			return false;
		}
		else {
			void *args[16];
			for (int i = 0; i < param_count; i++) {
				switch (event_p->format[i])
				{
				case 'i':
				case 'f':
				case 'b':
					args[i] = &params[i + 1];
					break;
				case 's':
					int len = NULL;
					cell *addr = NULL;

					amx_GetAddr(amx, params[i + 1], &addr);
					amx_StrLen(addr, &len);

					if (len) {
						len++;

						char* text = new char[len];

						amx_GetString(text, addr, 0, len);
						args[i] = mono_string_new(mono_domain_get(), text);
					}
					else {
						args[i] = mono_string_new(mono_domain_get(), "");
					}
					break;
				}
			}

			int retint = SampSharp::CallEvent(event_p->method, args);

			if (retint != -1) {
				*retval = retint;
			}
			return false;
		}
		return false;
	}

	return true;
}

int SampSharp::CallEvent(MonoMethod* method, void **params) {

	MonoObject *exception = NULL;
	
	if (!method) {
		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app);
		cout << "[SampSharp] ERROR: No method given in CallEvent!" << endl;
		logfile << GetTimeStamp() << "ERROR: No method given in CallEvent!" << endl;
		logfile.close();

		return false;
	}

	MonoObject *response = mono_runtime_invoke(method, mono_gchandle_get_target(gameModeHandle), params, &exception);

	if (exception) {
		char *stacktrace = mono_string_to_utf8(mono_object_to_string(exception, NULL));

		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app | ios::binary);
		cout << "[SampSharp] Exception thrown:" << endl << stacktrace << endl;
		logfile << GetTimeStamp() << " Exception thrown:" << "\r\n" << stacktrace << "\r\n";
		logfile.close();

		return -1;
	}

	if (!response) {
		return -1;
	}

	return *(bool *)mono_object_unbox(response) == true ? 1 : 0;;
}

void SampSharp::Unload() {
	mono_jit_cleanup(mono_domain_get());
}
