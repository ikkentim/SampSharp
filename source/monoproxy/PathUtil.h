#pragma once

#ifdef _WIN32
#include <direct.h>
#define getcwd _getcwd // stupid MSFT "deprecation" warning
#elif
#include <unistd.h>
#endif

#include <string>

struct PathUtil
{
	static std::string GetBinDirectory()
	{
		std::string s_cwd(getcwd(NULL, 0));
		return s_cwd.append("\\");
	}

	static std::string GetMonoDirectory()
	{
		return GetBinDirectory().append("Mono\\");
	}

	static std::string GetLibDirectory()
	{
		return GetMonoDirectory().append("lib\\");
	}

	static std::string GetConfigDirectory()
	{
		return GetMonoDirectory().append("etc\\");
	}
};
