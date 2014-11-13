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
