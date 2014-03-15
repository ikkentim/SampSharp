// Copyright (C) 2011-2013 Zeex
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

#ifndef SAMPGDK_CORE_HPP
#define SAMPGDK_CORE_HPP

#include <cstdarg>

#include <sampgdk/compatibility.h>
#include <sampgdk/core.h>

#ifdef _MSC_VER
  #pragma warning(push)
  #pragma warning(disable:4355) // 'this' : used in base member initializer list
#endif

SAMPGDK_BEGIN_NAMESPACE

class Plugin {
 public:
  Plugin(void *handle): handle_(handle) {}

  void *GetHandle() { return handle_; }
  void SetHandle(void *handle) { handle_ = handle; }

  int Load(void **ppData) { return sampgdk_init_plugin(handle_, ppData); }
  int Unload() { return sampgdk_cleanup_plugin(handle_); }

  int Register() { return sampgdk_register_plugin(handle_); }
  int Unregister() { return sampgdk_unregister_plugin(handle_); }

  void *GetSymbol(const char *name) {
    return sampgdk_get_plugin_symbol(handle_, name);
  }

  void ProcessTimers() { sampgdk_process_plugin_timers(handle_); }

 private:
  void *handle_;
};

class ThisPlugin: public Plugin {
 public:
  ThisPlugin(): Plugin(sampgdk_get_plugin_handle(this)) {}
};

class ServerLog {
 public:
  static void Printf(const char *format, ...) {
    std::va_list args;
    va_start(args, format);
    VPrintf(format, args);
    va_end(args);
  }
  static void VPrintf(const char *format, std::va_list args) {
    sampgdk_vlogprintf(format, args);
  }
};

SAMPGDK_END_NAMESPACE

#ifdef _MSC_VER
  #pragma warning(pop)
#endif

#endif // !SAMPGDK_CORE_HPP
