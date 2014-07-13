#pragma once

#include "PathUtil.h"
#include <string>
#include <mono/jit/jit.h>
#include <sampgdk/a_samp.h>
struct MonoUtil
{
    #ifdef _WIN32
    static void GenerateSymbols(const char * path) {	
	    char * p = (char *)PathUtil::GetLibDirectory()
            .append("mono/4.5/pdb2mdb.exe").c_str();

	
	    MonoAssembly *mdbconverter = mono_domain_assembly_open(
            mono_domain_get(), p);

	    if (mdbconverter) {
		    char *argv[2];
		    argv[0] = p;
		    argv[1] = (char *)path;

		    mono_jit_exec(mono_domain_get(), mdbconverter, 2, argv);
	    }

    }
    #endif
};
