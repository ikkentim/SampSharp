// SampSharp
// Copyright 2015 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#include "GameMode.h"
#include <fstream>
#include <assert.h>
#include <iostream>
#include <fstream>
#include <time.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/threads.h>
#include <mono/metadata/exception.h>
#include <mono/metadata/debug-helpers.h>
#include "natives.h"
#include "MonoRuntime.h"
#include "PathUtil.h"
#include "monohelper.h"

using std::string;
using sampgdk::logprintf;

bool GameMode::isLoaded_;
MonoDomain *GameMode::domain_;
GameMode::GameModeImage GameMode::gameMode_;
GameMode::GameModeImage GameMode::baseMode_;
GameMode::CallbackMap GameMode::callbacks_;
uint32_t GameMode::gameModeHandle_;
GameMode::TimerMap GameMode::timers_;
GameMode::ExtensionList GameMode::extensions_;

MonoMethod *GameMode::tickMethod_;
MonoClass *GameMode::paramLengthClass_;
MonoMethod *GameMode::paramLengthGetMethod_;

bool GameMode::Load(std::string namespaceName, std::string className) {
    assert(MonoRuntime::IsLoaded());

    /* Build paths */
    string dirPath = PathUtil::GetPathInBin("gamemode/");
    string libraryPath = PathUtil::GetPathInBin("gamemode/")
        .append(namespaceName).append(".dll");
    string configPath = PathUtil::GetPathInBin("gamemode/")
        .append(namespaceName).append(".dll.config");

    /* Check for existance of gamemode */
    std::ifstream ifile(libraryPath.c_str());
    if (!ifile) {
        logprintf("ERROR: library does not exist!");
        isLoaded_ = false;
        return false;
    }

    mono_domain_set_config(mono_domain_get(), dirPath.c_str(), 
        configPath.c_str());

    domain_ = mono_domain_get();
    gameMode_.image = mono_assembly_get_image(
        mono_assembly_open(libraryPath.c_str(), NULL));

    if (!gameMode_.image) {
        logprintf("ERROR: Couldn't open image!");
        isLoaded_ = false;
        return false;
    }

    gameMode_.klass = mono_class_from_name(gameMode_.image, 
        namespaceName.c_str(), className.c_str());

    if (!gameMode_.klass) {
        logprintf("ERROR: Couldn't find class %s:%s!",
            namespaceName.c_str(), className.c_str());
        isLoaded_ = false;
        return false;
    }

    baseMode_.klass = mono_class_get_parent(gameMode_.klass);

    if (!baseMode_.klass || strcmp("BaseMode",
        mono_class_get_name(baseMode_.klass)) != 0) {
        logprintf("ERROR: Parent type of %s::%s is not BaseMode!",
            namespaceName.c_str(), className.c_str());
        isLoaded_ = false;
        return false;
    }

    baseMode_.image = mono_class_get_image(baseMode_.klass);
    
    /* Add all internal calls. */
    LoadNatives();
    mono_add_internal_call(
        "SampSharp.GameMode.Natives.Native::RegisterExtension", 
        (void *)RegisterExtension);
    mono_add_internal_call(
        "SampSharp.GameMode.Natives.Native::SetTimer", (void *)SetRefTimer);
    mono_add_internal_call(
        "SampSharp.GameMode.Natives.Native::KillTimer", (void *)KillRefTimer);

    MonoObject *gamemode_obj = mono_object_new
        (mono_domain_get(), gameMode_.klass);
    gameModeHandle_ = mono_gchandle_new(gamemode_obj, false);
    mono_runtime_object_init(gamemode_obj);

    isLoaded_ = true;
    return true;
}

bool GameMode::Unload() {
    if (!isLoaded_) {
        return false;
    }

    /* Clear found methods. */
    tickMethod_ = NULL;
    paramLengthClass_ = NULL;
    paramLengthGetMethod_ = NULL;

    /* Clear timers. */
    logprintf("Stopping timers...");
    for (TimerMap::iterator iter = timers_.begin(); 
        iter != timers_.end(); iter++) {
        int id = iter->first;
        RefTimer timer = (iter->second);

        mono_gchandle_free(timer.handle);
        KillTimer(id);
    }
    timers_.clear();

    /* Clear extensions. */
    logprintf("Unloading extensions...");
    for (ExtensionList::iterator iter = extensions_.begin(); 
        iter != extensions_.end(); iter++) {
        mono_gchandle_free(*iter);
    }
    extensions_.clear();

    /* Clear callbacks. */
    logprintf("Clearing callbacks table...");
    for (CallbackMap::iterator iter = callbacks_.begin(); 
        iter != callbacks_.end(); iter++) {
        iter->second->params.clear();
        delete iter->second;
    }
    callbacks_.clear();

    /* Dispose of game mode. */
    mono_thread_attach(domain_);

    MonoMethod *method = LoadEvent("Dispose", 0);
 
    if (method) {
        logprintf("Disposing gamemode...");
        CallEvent(method, gameModeHandle_, NULL);
    }

    /* Release game mode. */
    mono_gchandle_free(gameModeHandle_);

    gameModeHandle_ = NULL;

    /* For now, I see no way of unloading and reloading images without problems.
     * Best to look at this again in the future.
     */
    //mono_image_close(gameMode_.image);
    //mono_image_close(baseMode_.image);

    //mono_images_cleanup();

    gameMode_.image = NULL;
    gameMode_.klass = NULL;

    baseMode_.image = NULL;
    baseMode_.klass = NULL;

    domain_ = NULL;

    isLoaded_ = false;
    return true;
}

int GameMode::SetRefTimer(int interval, bool repeat, MonoObject *params) {
    if (!isLoaded_) {
        return 0;
    }

    if (!params) {
        /* If no params are parsed, there is no need to keep a reference to it,
         * or the id.
         */
        return SetTimer(interval, repeat, ProcessTimerTick, NULL);
    }

    /* Stop the GC from collecting the params. */
    uint32_t handle = mono_gchandle_new(params, false);
    int id = SetTimer(interval, repeat, ProcessTimerTick, &handle);

    timers_[id] = { handle, repeat };
    return id;
}

bool GameMode::KillRefTimer(int id) {
    if (!isLoaded_) {
        return 0;
    }

    /* Delete the timer from the map. */
    if (timers_.find(id) == timers_.end())
    {
        auto timer = timers_[id];
        mono_gchandle_free(timer.handle);

        timers_.erase(id);
    }

    return KillTimer(id);
}

bool GameMode::RegisterExtension(MonoObject *extension) {
    if (!isLoaded_ || !extension) {
        return false;
    }

    for (ExtensionList::iterator iter = extensions_.begin(); 
        iter != extensions_.end(); iter++) {
        if (mono_gchandle_get_target(*iter) == extension) {
            return false;
        }
    }

    uint32_t handle = mono_gchandle_new(extension, false);
    extensions_.push_back(handle);
    return true;
}

void GameMode::ProcessTimerTick(int timerid, void *data) {
    if (!isLoaded_) {
        return;
    }

    static MonoMethod *method;

    if (method == NULL) {
        method = LoadEvent("OnTimerTick", 2);
    }

    void *args[2];
    args[0] = &timerid;
    args[1] = NULL;
    RefTimer *timer = NULL;
    if (timers_.find(timerid) != timers_.end()) {
        timer = &timers_[timerid];
        args[1] = mono_gchandle_get_target(timer->handle);
    }

    CallEvent(method, gameModeHandle_, args);

    /*
    * After OnTimerTick has been called and the timer is not repeating,
    * drop the handle and erase the timer from the map.
    */
    if (timer && !timer->repeating) {
        if (args[1]) {
            /*
             * Only free the handle, if it was asociated with an object.
             */
            mono_gchandle_free(timer->handle);
        }
        timers_.erase(timerid);
    }
}

void GameMode::ProcessTick() {
    if (!isLoaded_) {
        return;
    }

    if (!tickMethod_) {
        tickMethod_ = LoadEvent("OnTick", 0);
    }

    CallEvent(tickMethod_, gameModeHandle_, NULL);
}

GameMode::ParameterType GameMode::GetParameterType(MonoType *type)
{
    char *type_name = mono_type_get_name(type);
    if (!strcmp(type_name, "System.Int32")) {
        return PARAM_INT;
    }
    else if (!strcmp(type_name, "System.Single")) {
        return PARAM_FLOAT;
    }
    else if (!strcmp(type_name, "System.String")) {
        return PARAM_STRING;
    }
    else if (!strcmp(type_name, "System.Boolean")) {
        return PARAM_BOOL;
    }
    else if (!strcmp(type_name, "System.Int32[]")) {
        return PARAM_INT_ARRAY;
    }
    else if (!strcmp(type_name, "System.Single[]")) {
        return PARAM_FLOAT_ARRAY;
    }
    else if (!strcmp(type_name, "System.Boolean[]")) {
        return PARAM_BOOL_ARRAY;
    }

    return PARAM_INVALID;
}

bool GameMode::IsMethodValidCallback(MonoImage *image, MonoMethod *method,
    int param_count) {
    void *iter = NULL;
    MonoType* type = NULL;

    if (!image || !method) {
        return false;
    }

    MonoMethodSignature *sig = mono_method_get_signature(method, image,
        mono_method_get_token(method));

    if (param_count != mono_signature_get_param_count(sig)) {
        return false;
    }

    while (type = mono_signature_get_params(sig, &iter)) {
        if (GetParameterType(type) == PARAM_INVALID) {
            return false;
        }
    }

    return true;
}

MonoMethod *GameMode::FindMethodForCallbackInClass(const char *name,
    int param_count, MonoClass *klass) {
    MonoMethod *method;
    MonoImage *image;

    /* Find the first occurance of the callback name in the given class and
     * it's base classes. If a method was found, but it's signature contains 
     * unsupported types, look for other overloads in this class.
     */

    while (klass) {
        image = mono_class_get_image(klass);
        method = mono_class_get_method_from_name(klass, name, param_count);

        if (IsMethodValidCallback(image, method, param_count)) {
            return method;
        }

        if (method) {
            void *method_iter = NULL;
            while ((method = mono_class_get_methods(klass, &method_iter))) {
                if (!strcmp(mono_method_get_name(method), name) &&
                    IsMethodValidCallback(mono_class_get_image(
                    mono_method_get_class(method)), method, param_count)) {
                    return method;
                }
            }
        }

        klass = mono_class_get_parent(klass);
    }

    return NULL;
}

MonoMethod *GameMode::FindMethodForCallback(const char *name, 
    int param_count, uint32_t &handle) {
    MonoMethod *method;

    /* Look in the game mode. */
    method = FindMethodForCallbackInClass(name, param_count, gameMode_.klass);

    if (method) {
        handle = gameModeHandle_;
        return method;
    }

    /* Look in the extensions. */
    for (ExtensionList::iterator iter = extensions_.begin(); 
        !method && iter != extensions_.end(); iter++) {
        MonoClass *klass = mono_object_get_class(
            mono_gchandle_get_target(handle = *iter));

        method = FindMethodForCallbackInClass(name, param_count, klass);

        if (method) {
            return method;
        }
    }

    return NULL;
}
void GameMode::ProcessPublicCall(AMX *amx, const char *name, cell *params, 
    cell *retval) {

    CallbackSignature *signature;

    if (!isLoaded_) {
        return;
    }

    int param_count = params[0] / sizeof(cell);
    if (strlen(name) == 0 || param_count > MAX_CALLBACK_PARAM_COUNT) {
        logprintf("[SampSharp] WARNING: Skipped callback with %d parameters.",
            param_count);
        return;
    }

    /* OnRconCommand can sometimes end up on different theads? 
     * Just to make sure, attach the current thread to the domain. 
     */
    mono_thread_attach(domain_);

    /* If the callback not known in the callbacks_ map, find the callback in
     * the game mode or one of the registered extensions.
     */
    if (callbacks_.find(name) == callbacks_.end()) {
        signature = new CallbackSignature;
        signature->method = FindMethodForCallback(name, param_count, 
            signature->handle);

        if (!signature->method) {
            callbacks_[name] = NULL;
            return;
        }
        
        MonoImage *image = mono_class_get_image(
            mono_method_get_class(signature->method));
        
        void *iter = NULL;
        int iter_idx = 0;

        MonoMethodSignature *sig = mono_method_get_signature(signature->method, 
            image, mono_method_get_token(signature->method));

        MonoType* type = NULL;
        while (type = mono_signature_get_params(sig, &iter)) {
            ParameterSignature parameter_signature;

            parameter_signature.type = GetParameterType(type);

            if (parameter_signature.type == PARAM_INT_ARRAY ||
                parameter_signature.type == PARAM_FLOAT_ARRAY ||
                parameter_signature.type == PARAM_BOOL_ARRAY) {
                parameter_signature.length_idx = GetParamLengthIndex(
                    signature->method, iter_idx);

                if (parameter_signature.length_idx == -1) {
                    callbacks_[name] = NULL;
                    return;
                }
            }

            signature->params[iter_idx++] = parameter_signature;
        }

        callbacks_[name] = signature;
    }

    if (signature = callbacks_[name]) {
        /* Handle calls without parameters. */
        if (!param_count) {
            int retint = CallEvent(signature->method, signature->handle, NULL);

            /* If there's a cell allocated for the return value and 
             * the callback was executed successfuly, fill the cell with 
             * the returned value. 
             */
            if (retval != NULL && retint != -1) {
                *retval = retint;
            }
            return;
        }

        /* Handle calls with parameters. */

        void *args[MAX_CALLBACK_PARAM_COUNT];
        int len = 0;
        cell *addr = NULL;
        MonoArray *arr;

        if (signature->params.size() != param_count)
        {
            logprintf("[SampSharp] ERROR: Parameters of callback %s "
                "does not match signature (called: %d, signature: %d)",
                name, param_count, signature->params.size());
            return;
        }

        for (int i = 0; i < param_count; i++) {
            switch (signature->params[i].type) {
            case PARAM_INT:
            case PARAM_FLOAT:
            case PARAM_BOOL: {
                args[i] = &params[i + 1];
                break;
            }
            case PARAM_STRING: {
                amx_GetAddr(amx, params[i + 1], &addr);
                amx_StrLen(addr, &len);

                if (len) {
                    len++;

                    char* text = new char[len];

                    amx_GetString(text, addr, 0, len);
                    args[i] = string_to_monostring(text, len);
                }
                else {
                    args[i] = mono_string_new(mono_domain_get(), "");
                }
                break;
            }

            case PARAM_INT_ARRAY: {
                len = params[signature->params[i].length_idx];
                arr = mono_array_new(mono_domain_get(), mono_get_int32_class(),
                    len);

                if (len > 0) {
                    cell* addr = NULL;
                    amx_GetAddr(amx, params[i + 1], &addr);

                    for (int i = 0; i < len; i++) {
                        mono_array_set(arr, int, i, *(addr + i));
                    }
                }
                args[i] = arr;
                break;
            }
            case PARAM_FLOAT_ARRAY: {
                len = params[signature->params[i].length_idx];
                arr = mono_array_new(mono_domain_get(), mono_get_int32_class(),
                    len);

                if (len > 0) {
                    cell* addr = NULL;
                    amx_GetAddr(amx, params[i + 1], &addr);

                    for (int i = 0; i < len; i++) {
                        mono_array_set(arr, float, i, amx_ctof(*(addr + i)));
                    }
                }
                args[i] = arr;
                break;
            }
            case PARAM_BOOL_ARRAY: {
                len = params[signature->params[i].length_idx];
                arr = mono_array_new(mono_domain_get(), mono_get_int32_class(),
                    len);

                if (len > 0) {
                    cell* addr = NULL;
                    amx_GetAddr(amx, params[i + 1], &addr);

                    for (int i = 0; i < len; i++) {
                        mono_array_set(arr, bool, i, !!*(addr + i));
                    }
                }
                args[i] = arr;
                break;
            }
            default:
                logprintf("[SampSharp] ERROR: Signature of %s contains "
                    "unsupported parameters.", name);
                return;
            }
        }

        int retint = CallEvent(signature->method, signature->handle, args);

        if (retval != NULL && retint != -1) {
            *retval = retint;
        }
    }
}

MonoMethod *GameMode::LoadEvent(const char *name, int param_count) {
    MonoMethod *method = mono_class_get_method_from_name(
        gameMode_.klass, name, param_count);

    if (!method) {
        method = mono_class_get_method_from_name(
            baseMode_.klass, name, param_count);
    }

    return method;
}

int GameMode::CallEvent(MonoMethod *method, uint32_t handle, void **params) {
    assert(method);
    assert(handle);

    MonoObject *exception;
    MonoObject *response = mono_runtime_invoke(method, 
        mono_gchandle_get_target(handle), params, &exception);

    if (exception) {
        char *stacktrace = mono_string_to_utf8(
            mono_object_to_string(exception, NULL));

        /* Cannot print the exception to logprintf; the buffer is too small. */
        std::cout << "[SampSharp] Exception thrown during execution of " 
            << mono_method_get_name(method) << ":" << std::endl 
            << stacktrace << std::endl;

        time_t now = time(0);
        char timestamp[32];
        strftime(timestamp, sizeof(timestamp), "[%d/%m/%Y %H:%M:%S]", localtime(&now));

        std::ofstream logfile;
        logfile.open("SampSharp_errors.log", std::ios::app | std::ios::binary);
        logfile << timestamp << " Exception thrown" << mono_method_get_name(method) << ":" << "\r\n" << stacktrace << "\r\n";
        logfile.close();
        return -1;
    }

    if (!response) {
        return -1;
    }

    return *(int *)mono_object_unbox(response);
}

int GameMode::GetParamLengthIndex(MonoMethod *method, int idx) {
    if (!paramLengthClass_) {
        paramLengthClass_ = mono_class_from_name(baseMode_.image,
            PARAM_LENGTH_ATTRIBUTE_NAMESPACE, PARAM_LENGTH_ATTRIBUTE_CLASS);
    }
    if (!paramLengthGetMethod_) {
        paramLengthGetMethod_ = mono_property_get_get_method(
            mono_class_get_property_from_name(paramLengthClass_, "Index"));
    }

    assert(paramLengthClass_);
    assert(paramLengthGetMethod_);

    MonoCustomAttrInfo *attr = mono_custom_attrs_from_param(method, idx + 1);
    if (!attr) {
        logprintf("[SampSharp] ERROR: No callback parameter info for %s @ %d", 
            mono_method_get_name(method), idx);
        return -1;
    }

    MonoObject *attrObj = mono_custom_attrs_get_attr(attr, paramLengthClass_);
    if (!attrObj) {
        logprintf("[SampSharp] ERROR: Array parameter has no specified size:"
            "%s @ %d",
            mono_method_get_name(method), idx);
        return -1;
    }

    return *(int*)mono_object_unbox(mono_runtime_invoke(paramLengthGetMethod_, 
        attrObj, NULL, NULL));
}