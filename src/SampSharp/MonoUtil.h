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

    static char * MonoStringToString(MonoString *str)
    {
	    //TODO: seems a little sloppy, should research better solutions.
	    mono_unichar2 *uni_buffer = mono_string_chars(str);
        int len = mono_string_length(str);
 
        char *buffer = new char[len + 1];
	    for (int i = 0; i < len; i++) {
		    buffer[i] = (char)uni_buffer[i];
	    }
        buffer[len] = '\0';
    
        return buffer;
    }
};
