#pragma once

#ifdef _WIN32
#include <direct.h>
#define getcwd _getcwd // stupid MSFT "deprecation" warning
#elif
#include <unistd.h>
#endif
#include <string>

using namespace std;

struct PathUtil
{
	static string GetBinDirectory()
	{
		string s_cwd(getcwd(NULL, 0));
		return s_cwd.append("\\");
	}

	static string GetPathInBin(string append)
	{
		return GetBinDirectory().append(append);
	}

	static string GetMonoDirectory()
	{
		return GetBinDirectory().append("Mono\\");
	}

	static string GetLibDirectory()
	{
		return GetMonoDirectory().append("lib\\");
	}

	static string GetConfigDirectory()
	{
		return GetMonoDirectory().append("etc\\");
	}
};
