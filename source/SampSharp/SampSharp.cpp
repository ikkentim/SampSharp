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
MonoClass *SampSharp::parameterLengthAttributeClassType;

MonoMethod *SampSharp::parameterLengthAttributeIndexGetMethod;

uint32_t SampSharp::gameModeHandle;

EventMap SampSharp::events;

void SampSharp::Load(string baseModePath, string gameModePath, string gameModeNamespace, string gameModeClass, bool debug) {

	#ifdef _WIN32
	mono_set_dirs(PathUtil::GetLibDirectory().c_str(), PathUtil::GetConfigDirectory().c_str());
	#endif

	mono_debug_init(MONO_DEBUG_FORMAT_MONO);

	rootDomain = mono_jit_init(PathUtil::GetPathInBin(gameModePath).c_str());

	#ifdef _WIN32
	if (debug == true) {
		GenerateSymbols(baseModePath);
		GenerateSymbols(gameModePath);
	}
	#endif

	gameModeImage = mono_assembly_get_image(mono_domain_assembly_open(mono_domain_get(), (char *)PathUtil::GetPathInBin(gameModePath).c_str()));
	baseModeImage = mono_assembly_get_image(mono_domain_assembly_open(mono_domain_get(), (char *)PathUtil::GetPathInBin(baseModePath).c_str()));

	LoadNatives(); 

	baseModeClassType = mono_class_from_name(baseModeImage, "SampSharp.GameMode", "BaseMode");
	gameModeClassType = mono_class_from_name(gameModeImage, gameModeNamespace.c_str(), gameModeClass.c_str());

	parameterLengthAttributeClassType = mono_class_from_name(baseModeImage, "SampSharp.GameMode", "ParameterLengthAttribute");
	parameterLengthAttributeIndexGetMethod = mono_property_get_get_method(mono_class_get_property_from_name(parameterLengthAttributeClassType, "Index"));

	MonoObject *gameModeObject = mono_object_new(mono_domain_get(), gameModeClassType);
	gameModeHandle = mono_gchandle_new(gameModeObject, true);
	mono_runtime_object_init(gameModeObject);

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
	time_t now = time(0);
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

int SampSharp::GetParamLengthIndex(MonoMethod *method, int idx) {
	
	MonoCustomAttrInfo *attr = mono_custom_attrs_from_param(method, idx + 1);
	if (!attr) {
		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app);
		cout << "[SampSharp] ERROR: No attribute info for " << mono_method_get_name(method) << "@" << idx << endl;
		logfile << GetTimeStamp() << "ERROR: No attribute info for " << mono_method_get_name(method) << "@" << idx << endl;
		logfile.close();
		return -1;
	}

	MonoObject *attrObj = mono_custom_attrs_get_attr(attr, parameterLengthAttributeClassType);
	if (!attrObj) {
		ofstream logfile;
		logfile.open("SampSharp_errors.log", ios::app);
		cout << "[SampSharp] ERROR: Array parameter has no specified size: " << mono_method_get_name(method) << "@" << idx << endl;
		logfile << GetTimeStamp() << "ERROR: Array parameter has no specified size: " << mono_method_get_name(method) << "@" << idx << endl;
		logfile.close();
		return -1;
	}

	return *(int*)mono_object_unbox(mono_runtime_invoke(parameterLengthAttributeIndexGetMethod, attrObj, NULL, NULL));
}
bool SampSharp::HandleEvent(AMX *amx, const char *name, cell *params, cell *retval) {
	if (strcmp(name, "OnPlayerConnect") == 0) {
		cout << "Char test: \u00D6" << endl;
		SendClientMessageToAll(-1, "Char test: \u00D6");
		SendClientMessageToAll(-1, "Test2: \x0089");
	}

	const int param_count = params[0] / sizeof(cell);

	if (strlen(name) == 0 || param_count > 16) {
		return true;
	}

	mono_thread_attach(SampSharp::rootDomain);

	//detect unknown methods
	if (events.find(name) == events.end())
	{
		//find method
		MonoMethod *m_method = mono_class_get_method_from_name(gameModeClassType, name, param_count);
		
		MonoImage *image = gameModeImage;
		if (!m_method) {
			m_method = mono_class_get_method_from_name(baseModeClassType, name, param_count);
			image = baseModeImage;
		}

		if (!m_method){
			events[name] = NULL;
			return true;
		}

		//iterate params
		void *iter = NULL;
		int iter_idx = 0;
		ParamMap params;

		MonoType* type = NULL;
		MonoMethodSignature *sig = mono_method_get_signature(m_method, image, mono_method_get_token(m_method));
		while (type = mono_signature_get_params(sig, &iter)) {
			string type_name = mono_type_get_name(type);

			if (!type_name.compare("System.Int32")) {
				param_t *par = new param_t;
				par->type = PARAM_INT;
				params[iter_idx] = par;
			}
			else if (!type_name.compare("System.Single")) {
				param_t *par = new param_t;
				par->type = PARAM_FLOAT;
				params[iter_idx] = par;
			}
			else if (!type_name.compare("System.String")) {
				param_t *par = new param_t;
				par->type = PARAM_STRING;
				params[iter_idx] = par;
			}
			else if (!type_name.compare("System.Boolean")) {
				param_t *par = new param_t;
				par->type = PARAM_BOOL;
				params[iter_idx] = par;
			}
			else if (!type_name.compare("System.Int32[]")) {
				param_t *par = new param_t;
				par->type = PARAM_INT_ARRAY;
				params[iter_idx] = par;
				
				int index = GetParamLengthIndex(m_method, iter_idx);
				if (index == -1) {
					events[name] = NULL;
					return true;
				}
				par->length_idx = index;
	
			}
			else if (!type_name.compare("System.Single[]")) {
				param_t *par = new param_t;
				par->type = PARAM_FLOAT_ARRAY;
				params[iter_idx] = par;

				int index = GetParamLengthIndex(m_method, iter_idx);
				if (index == -1) {
					events[name] = NULL;
					return true;
				}
				par->length_idx = index;
			}
			else if (!type_name.compare("System.Boolean[]")) {
				param_t *par = new param_t;
				par->type = PARAM_BOOL_ARRAY;
				params[iter_idx] = par;

				int index = GetParamLengthIndex(m_method, iter_idx);
				if (index == -1) {
					events[name] = NULL;
					return true;
				}
				par->length_idx = index;
			}
			else {
				ofstream logfile;
				logfile.open("SampSharp_errors.log", ios::app);
				cout << "[SampSharp] ERROR: Incompatible parameter type: " << type_name << " in " << name << endl;
				logfile << GetTimeStamp() << "ERROR: Incompatible parameter type: " << type_name << " in " << name << endl;
				logfile.close();
				events[name] = NULL;
				return true;
			}

			iter_idx++;
		}
		
		event_t *event_add = new event_t;
		event_add->method = m_method;
		event_add->params = params;
		events[name] = event_add;
	}

	event_t *event_p = events[name];
	
	//call known events
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
			int len = NULL;
			cell *addr = NULL;
			MonoArray *arr;

			for (int i = 0; i < param_count; i++) {
				switch (event_p->params[i]->type)
				{
				case PARAM_INT:
				case PARAM_FLOAT:
				case PARAM_BOOL:
					args[i] = &params[i + 1];
					break;
				case PARAM_STRING:
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
				case PARAM_INT_ARRAY:
					len = params[event_p->params[i]->length_idx];
					arr = mono_array_new(mono_domain_get(), mono_get_int32_class(), len);

					if (len > 0) {
						cell* addr = NULL;
						amx_GetAddr(amx, params[i + 1], &addr);

						for (int i = 0; i < len; i++) {
							mono_array_set(arr, int, i, *(addr + i));
						}
					}
					args[i] = arr;
					break;
				case PARAM_FLOAT_ARRAY:
					len = params[event_p->params[i]->length_idx];
					arr = mono_array_new(mono_domain_get(), mono_get_int32_class(), len);

					if (len > 0) {
						cell* addr = NULL;
						amx_GetAddr(amx, params[i + 1], &addr);

						for (int i = 0; i < len; i++) {
							mono_array_set(arr, float, i, amx_ctof(*(addr + i)));
						}
					}
					args[i] = arr;
					break;
				case PARAM_BOOL_ARRAY:
					len = params[event_p->params[i]->length_idx];
					arr = mono_array_new(mono_domain_get(), mono_get_int32_class(), len);

					if (len > 0) {
						cell* addr = NULL;
						amx_GetAddr(amx, params[i + 1], &addr);

						for (int i = 0; i < len; i++) {
							mono_array_set(arr, bool, i, *(addr + i));
						}
					}
					args[i] = arr;
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
