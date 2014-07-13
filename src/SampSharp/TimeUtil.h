#pragma once

#include <time.h>
#include <string.h>

struct TimeUtil
{
	static char *GetTimeStamp() {
	time_t now = time(0);
	char timestamp[32];
	
	strftime(timestamp, sizeof(timestamp), "[%d/%m/%Y %H:%M:%S]", localtime(&now));

	char *timestamp2 = new char[32];
	strcpy(timestamp2, timestamp);
	return  timestamp2;
    }
};
