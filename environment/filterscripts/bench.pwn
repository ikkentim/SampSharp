#include <a_samp>

#define START_BENCH(%0); {new __a=%0, __b=0,__c,__d=GetTickCount(),__e=1;do{}\
	while(__d==GetTickCount());__c=GetTickCount();__d=__c;while(__c-__d<__a||\
    __e){if(__e){if(__c-__d>=__a){__e=0;__c=GetTickCount();do{}while(__c==\
	GetTickCount());__c=GetTickCount();__d=__c;__b=0;}}{
		
#define FINISH_BENCH(%0); }__b++;__c=GetTickCount();}printf(" Bench for "%0": executes, by average, %.2f times/ms.",floatdiv(__b,__a));}

#pragma tabsize 0

public OnFilterScriptInit()
{
	print("\n--------------------------------------");
	print(" SampSharp benchmark PAWN test");
	print("--------------------------------------\n");
	
	START_BENCH(1000);
	{
		IsPlayerConnected(0);
	}
	FINISH_BENCH("NativeIsPlayerConnected");
	return 1;
}
