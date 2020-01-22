/* * * * * * * * * * * * * * * * * * * * * * *
 *  __                __                      
 * (_   _. ._ _  ._  (_  |_   _. ._ ._        
 * __) (_| | | | |_) __) | | (_| |  |_)       
 *               |                  |         
 *                                            
 * Name: SampSharp Intermission script        
 * Copyright: SampSharp                       
 * Description:                               
 *   Wipes the created resources in game (vehicles, objects, pvars, etc.) and
 *   shows a "restarting.." message to the players while this filter script is
 *   loaded.
 *                                            
 * Thanks to:                                 
 *   - newbprogramming                        
 * 
 */
 
#include <a_samp>

/* Undefined limits */
#define MAX_PLAYER_OBJECTS      255

/* Colors */
#define COLOR_ANNOUNCE          0x88AA62FF
#define COLOR_CONNECTED         0xA9C4E4FF
#define COLOR_CONNECTED_HOST    "{B9C9BF}"

/* Localization */
#define STR_RESTARTING          "The server is restarting.."
#define STR_CONNECTED           "Connected to " COLOR_CONNECTED_HOST "%s"

/* Postions */
// TODO: What is the default?
#define CAMERA_POSITION         1133.0504, -2038.4034, 69.1000

/* Callbacks made available by SampSharp. */
forward OnSampSharpStart();
forward OnSampSharpError();
forward OnSampSharpDisconnect();

/* Undeclared natives. */
native IsValidVehicle(vehicleid);

static stock gscPlayerColors[100] =
    { /* Extracted from fixes.inc */
        0xFF8C13FF, 0xC715FFFF, 0x20B2AAFF, 0xDC143CFF, 0x6495EDFF,
        0xF0E68CFF, 0x778899FF, 0xFF1493FF, 0xF4A460FF, 0xEE82EEFF,
        0xFFD720FF, 0x8B4513FF, 0x4949A0FF, 0x148B8BFF, 0x14FF7FFF,
        0x556B2FFF, 0x0FD9FAFF, 0x10DC29FF, 0x534081FF, 0x0495CDFF,
        0xEF6CE8FF, 0xBD34DAFF, 0x247C1BFF, 0x0C8E5DFF, 0x635B03FF,
        0xCB7ED3FF, 0x65ADEBFF, 0x5C1ACCFF, 0xF2F853FF, 0x11F891FF,
        0x7B39AAFF, 0x53EB10FF, 0x54137DFF, 0x275222FF, 0xF09F5BFF,
        0x3D0A4FFF, 0x22F767FF, 0xD63034FF, 0x9A6980FF, 0xDFB935FF,
        0x3793FAFF, 0x90239DFF, 0xE9AB2FFF, 0xAF2FF3FF, 0x057F94FF,
        0xB98519FF, 0x388EEAFF, 0x028151FF, 0xA55043FF, 0x0DE018FF,
        0x93AB1CFF, 0x95BAF0FF, 0x369976FF, 0x18F71FFF, 0x4B8987FF,
        0x491B9EFF, 0x829DC7FF, 0xBCE635FF, 0xCEA6DFFF, 0x20D4ADFF,
        0x2D74FDFF, 0x3C1C0DFF, 0x12D6D4FF, 0x48C000FF, 0x2A51E2FF,
        0xE3AC12FF, 0xFC42A8FF, 0x2FC827FF, 0x1A30BFFF, 0xB740C2FF,
        0x42ACF5FF, 0x2FD9DEFF, 0xFAFB71FF, 0x05D1CDFF, 0xC471BDFF,
        0x94436EFF, 0xC1F7ECFF, 0xCE79EEFF, 0xBD1EF2FF, 0x93B7E4FF,
        0x3214AAFF, 0x184D3BFF, 0xAE4B99FF, 0x7E49D7FF, 0x4C436EFF,
        0xFA24CCFF, 0xCE76BEFF, 0xA04E0AFF, 0x9F945CFF, 0xDCDE3DFF,
        0x10C9C5FF, 0x70524DFF, 0x0BE472FF, 0x8A2CD7FF, 0x6152C2FF,
        0xCF72A9FF, 0xE59338FF, 0xEEDC2DFF, 0xD8C762FF, 0xD8C762FF
    };
        
/* Sets the specified player to the default camera position. If spectate is
 * not 0, the player will be put into spectate mode. 
 */
SetDefaultCameraPosition(playerid, spectate)
{
    /* De-spawn them from the world. When they respawn we want them in class
     * selection.
     */
    ForceClassSelection(playerid);

    /* Hide the controls for now. */
    TogglePlayerSpectating(playerid, spectate);
    
    if(!spectate)
        SetPlayerPos(playerid, 0, 0, 5);

    /* Set their camera position. */
    SetPlayerCameraPos(playerid, CAMERA_POSITION);
}

/* Show the "restarting" message to the specified player. */
forward ShowRestartingMessage(playerid);
public ShowRestartingMessage(playerid)
{
    SendClientMessage(playerid, COLOR_ANNOUNCE, STR_RESTARTING);
}

/* Destruction macros */
/* call, limit, [prefix], [tag] */
#define DESTROY_POOL_TAG(%1,%2,%3);         for(new di=%2;di>=0;di--){%1(%3 di);}
#define DESTROY_POOL_PFX_TAG(%1,%2,%3,%4);  for(new di=%2;di>=0;di--){%1(%3,%4 di);}
#define DESTROY_POOL_CHK_TAG(%1,%2,%3,%4);  for(new di=%3;di>=0;di--){if(%2(%4 di)){%1(%4 di);}}
#define DESTROY_POOL(%1,%2);                DESTROY_POOL_TAG(%1,%2,_:);
#define DESTROY_POOL_PFX(%1,%2,%3);         DESTROY_POOL_PFX_TAG(%1,%2,%3,_:);
#define DESTROY_POOL_CHK(%1,%2,%3);         DESTROY_POOL_CHK_TAG(%1,%2,%3,_:);

DestroyGlobalResources()
{
    new varName[128];
    
    /* Properties */
    SetWorldTime(12);
    SetWeather(10);
    SetGravity(0.008);
    //LimitGlobalChatRadius(200); // TODO: What is the default?
    //LimitPlayerMarkerRadius(100); // TODO: What is the default?
    
    /* Pools */
    DESTROY_POOL_CHK(DestroyActor, IsValidActor, GetActorPoolSize());
    DESTROY_POOL_CHK(DestroyVehicle, IsValidVehicle, GetVehiclePoolSize());
    DESTROY_POOL_TAG(TextDrawDestroy, MAX_TEXT_DRAWS-1,Text:);
    DESTROY_POOL_TAG(Delete3DTextLabel, MAX_3DTEXT_GLOBAL-1,Text3D:);
    DESTROY_POOL_CHK_TAG(DestroyMenu, IsValidMenu, MAX_MENUS-1,Menu:);
    DESTROY_POOL(DestroyObject, MAX_OBJECTS-1);
    DESTROY_POOL(DestroyPickup, MAX_PICKUPS-1);
    DESTROY_POOL(GangZoneDestroy, MAX_GANG_ZONES-1);
    
    /* SVar */
    for(new i = GetSVarsUpperIndex(); i >= 0; i--)
    {
        GetSVarNameAtIndex(i, varName, sizeof(varName));

        if(GetSVarType(varName) != SERVER_VARTYPE_NONE)
            DeleteSVar(varName);
    }
}

DestroyPlayerResources(playerid)
{
    new varName[128];
    
    if(IsPlayerNPC(playerid))
    {
        Kick(playerid);
        return;
    }
    
    /* Properties */
    SetPlayerHealth(playerid, 100);
    SetPlayerArmour(playerid, 0);
    ResetPlayerWeapons(playerid);
    SetPlayerDrunkLevel(playerid, 0);
    SetSpawnInfo(playerid, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
    SetPlayerScore(playerid, 0);
    SetPlayerColor(playerid, gscPlayerColors[playerid % 100]);
    SetPlayerTime(playerid, 12, 0);
    
    /* Pools */
    // Disabled: Can cause crashes
    //DESTROY_POOL_PFX(DestroyPlayerObject,MAX_PLAYER_OBJECTS-1,playerid);
    //DESTROY_POOL_PFX_TAG(PlayerTextDrawDestroy, MAX_PLAYER_TEXT_DRAWS-1,playerid,PlayerText:);
    //DESTROY_POOL_PFX_TAG(DeletePlayer3DTextLabel, MAX_3DTEXT_PLAYER-1,playerid,PlayerText3D:);
    
    /* PVar */
    for(new i = GetPVarsUpperIndex(playerid); i >= 0; i--)
    {
        GetPVarNameAtIndex(playerid, i, varName, sizeof(varName));

        if(GetPVarType(playerid, varName) != PLAYER_VARTYPE_NONE)
            DeletePVar(playerid, varName);
    } 
    
}

public OnFilterScriptInit()
{
    /* Clear all the resources (objects, vehicles, textdraws, etc) and show the
     * "restarting.." message to all connected players.
     */
     
    DestroyGlobalResources();
    
    for(new playerid = GetPlayerPoolSize(); playerid >= 0; playerid--)
    {
        if(!IsPlayerConnected(playerid)) continue;
 
        DestroyPlayerResources(playerid);
        ShowRestartingMessage(playerid);
        SetDefaultCameraPosition(playerid, 1);
    }
   
    return 1;
}

public OnFilterScriptExit()
{
    new hostname[64];
    new connectedMessage[144];
    
    /* Show a "connected" message to every player and disable the spectate
     * mode.
     */
    GetConsoleVarAsString("hostname", hostname, 64);
    format(connectedMessage, 144, STR_CONNECTED, hostname);
   
    for(new playerid = GetPlayerPoolSize(); playerid >= 0; playerid--)
    {
        if(IsPlayerConnected(playerid) == 0) continue;
 
        SendClientMessage(playerid, COLOR_CONNECTED, connectedMessage);
        SetDefaultCameraPosition(playerid, 0);
    }
    return 1;
}

public OnPlayerConnect(playerid)
{
    /* Show a "restarting.." message to the player, but delay it a bit, so it
     * appears after the default "connected" message.
     */
    SetTimerEx("ShowRestartingMessage", 100, 0, "d", playerid);
    SetDefaultCameraPosition(playerid, 1);
    return 1;
}

public OnPlayerRequestClass(playerid, classid)
{
    /* If the player somehow manages to request a class, send them back to the
     * default screen with spectate mode enabled.
     */
    SetDefaultCameraPosition(playerid, 1);
    return 0;
}

public OnPlayerRequestSpawn(playerid)
{
    /* If the player somehow manages to request to spawn, send them back to the
     * default screen with spectate mode enabled.
     */
    SetDefaultCameraPosition(playerid, 1);
    return 0;
}

public OnPlayerText(playerid, text[])
{
    /* Disable chat during intermission by returning 0 here.
     */
    return 0;
}

public OnSampSharpStart()
{
    /* This is called by SampSharp to indicate the server has started the
     * intermission mode because the server has just been started, and the
     * game mode has not yet connected to the server.
     */
}

public OnSampSharpDisconnect()
{
    /* This is called by SampSharp to indicate the server has started the
     * intermission mode because the game mode has intentionally been detached
     * from the server(the game mode shut down on purpose).
     */
}

public OnSampSharpError()
{
    /* This is called by SampSharp to indicate the server has started the
     * intermission mode because the game mode has unintentionally been detached
     * from the server(the game mode shut down by error, it might have crashed).
     */
}

public OnPlayerUpdate(playerid)
{
    /* Prevent propagation of player updates during the intermission. */
    return 0;
}

