#ifdef _WIN32
#include <direct.h>
#define getcwd _getcwd // stupid MSFT "deprecation" warning
#else
#include <unistd.h>
#endif
#include <string>

#pragma once

struct PathUtil
{
	static std::string GetBinDirectory()
	{
		#ifdef _WIN32
		std::string s_cwd(getcwd(NULL, 0));
		return s_cwd.append("/");
		#else
		return "./";
		#endif
	}

	static std::string GetPathInBin(std::string append)
	{
		return GetBinDirectory().append(append);
	}

	static std::string GetMonoDirectory()
	{
		return GetPathInBin("mono/");
	}

	static std::string GetLibDirectory()
	{
		return GetMonoDirectory().append("lib/");
	}

	static std::string GetConfigDirectory()
	{
		return GetMonoDirectory().append("etc/");
    }

    static std::string GetGameModeDirectory()
    {
        return GetPathInBin("gamemode/");
    }
};
