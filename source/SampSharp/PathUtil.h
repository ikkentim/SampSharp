#pragma once

#ifdef _WIN32
#include <direct.h>
#define getcwd _getcwd // stupid MSFT "deprecation" warning
#else
#include <unistd.h>
#endif
#include <string>

using namespace std;

struct PathUtil
{
	static string GetBinDirectory()
	{
		#ifdef _WIN32
		string s_cwd(getcwd(NULL, 0));
		return s_cwd.append("/");
		#else
		return "./";
		#endif
	}

	static string GetPathInBin(string append)
	{
		return GetBinDirectory().append(append);
	}

	static string GetMonoDirectory()
	{
		#ifdef _WIN32
		return GetPathInBin("mono/win32/");
		#else
		return GetPathInBin("mono/linux/");
		#endif
	}

	static string GetLibDirectory()
	{
		return GetMonoDirectory().append("lib/");
	}

	static string GetConfigDirectory()
	{
		return GetMonoDirectory().append("etc/");
	}
};
