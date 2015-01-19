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

#include <string>
#include <assert.h>
#include <mono/jit/jit.h>
#include "PathUtil.h"

#pragma once


struct MonoUtil
{
    #ifdef _WIN32
    static void GenerateSymbols(const char * path) {
	    MonoAssembly *mdbconverter = mono_domain_assembly_open(mono_domain_get(), 
            PathUtil::GetLibDirectory().append("mono/4.5/pdb2mdb.exe").c_str());

        assert(mdbconverter);

		char *argv[2];

		argv[0] = (char *)path;
		argv[1] = (char *)path;

		mono_jit_exec(mono_domain_get(), mdbconverter, 2, argv);
    }
    #endif
};
