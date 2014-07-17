#include <iostream>
#include <time.h>
#include <sampgdk/core.h>
#include <sampgdk/a_samp.h>
#include <sampgdk/a_players.h>
#include <sampgdk/a_vehicles.h>

#pragma once

using sampgdk::logprintf;

#ifdef DO_BENCHMARK

typedef void (Test) ();

void NativeIsPlayerConnected()
{
    IsPlayerConnected(0);
}

void NativeCreateDestroyVehicle()
{
    int id = CreateVehicle(400, 0, 0, 0, 0, -1, -1, 0);
    DestroyVehicle(id);
}

void RunBench(const char *name, Test *test)
{
    clock_t clocks = clock();
    clock_t now = clock();
    int count = 0;
    float msdiff = 0;

    
    while(msdiff < 5000)
    {
        test();
        count++;
        now = clock();
        msdiff = (float)(now-clocks) / CLOCKS_PER_SEC * 1000;
    }
    
    logprintf(" Bench for %s: executes, by average, %.2f times/ms.", name, (float)count / msdiff);
}

void Benchmark() {

    logprintf("\n--------------------------------------");
    logprintf(" SampSharp benchmark SAMPGDK test");
    logprintf("--------------------------------------\n");

    RunBench("NativeIsPlayerConnected", NativeIsPlayerConnected);
    RunBench("NativeCreateDestroyVehicle", NativeCreateDestroyVehicle);

}

#endif