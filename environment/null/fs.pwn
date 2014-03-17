//Rcon fix
//Returns 0 in OnRconCommand to allow gamemodes to process the commands
#include <a_samp>
public OnRconCommand(cmd[])
{
	return 0;
}
