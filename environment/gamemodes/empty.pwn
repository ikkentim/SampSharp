// Main pawn gamemode.
//
// Satisfies the samp-server, loads the empty filterscript
// and lets SampSharp do it's work.
#include <a_samp>

main() return;
public OnGameModeInit()	return SendRconCommand("loadfs empty"), 1;
