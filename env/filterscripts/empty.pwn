//Rcon fix
//Returns 0 in OnRconCommand to allow gamemodes to process the commands
forward OnRconCommand(cmd[]);
public OnRconCommand(cmd[])
{
	return 0;
}
