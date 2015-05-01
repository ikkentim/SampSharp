// SampSharp
// Copyright 2015 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Runtime.CompilerServices;

namespace SampSharp.GameMode.Natives
{
    /// <summary>
    ///     Contains all native methods.
    /// </summary>
    public static partial class Native
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendClientMessage(int playerid, int color, string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendClientMessageToAll(int color, string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendPlayerMessageToPlayer(int playerid, int senderid, string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendPlayerMessageToAll(int senderid, string message);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendDeathMessage(int killer, int killee, int weapon);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GameTextForAll(string text, int time, int style);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GameTextForPlayer(int playerid, string text, int time, int style);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetTickCount();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetMaxPlayers();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerPoolSize();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetVehiclePoolSize();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetActorPoolSize();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetGameModeText(string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetTeamCount(int count);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddPlayerClass(int modelid, float spawnX, float spawnY, float spawnZ, float zAngle,
            int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddPlayerClassEx(int teamid, int modelid, float spawnX, float spawnY, float spawnZ,
            float zAngle, int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddStaticVehicle(int modelid, float spawnX, float spawnY, float spawnZ,
            float zAngle, int color1, int color2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddStaticVehicleEx(int modelid, float spawnX, float spawnY, float spawnZ,
            float zAngle, int color1, int color2, int respawnDelay, bool addsiren = false);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddStaticPickup(int model, int type, float x, float y, float z, int virtualworld);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePickup(int model, int type, float x, float y, float z, int virtualworld);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyPickup(int pickupid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowNameTags(bool show);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowPlayerMarkers(int mode);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GameModeExit();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetWorldTime(int hour);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetWeaponName(int weaponid, out string name, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableTirePopping(bool enable);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool EnableVehicleFriendlyFire();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool AllowInteriorWeapons(bool allow);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetWeather(int weatherid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetGravity(float gravity);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern float GetGravity();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CreateExplosion(float x, float y, float z, int type, float radius);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool UsePlayerPedAnims();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableInteriorEnterExits();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetNameTagDrawDistance(float distance);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableNameTagLOS();

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool LimitGlobalChatRadius(float chatRadius);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool LimitPlayerMarkerRadius(float markerRadius);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ConnectNPC(string name, string script);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerNPC(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsPlayerAdmin(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Kick(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Ban(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool BanEx(int playerid, string reason);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendRconCommand(string command);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetServerVarAsString(string varname, out string value, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetServerVarAsInt(string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetServerVarAsBool(string varname);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerNetworkStats(int playerid, out string retstr, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetNetworkStats(out string retstr, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GetPlayerVersion(int playerid, out string version, int len);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreateMenu(string title, int columns, float x, float y, float col1Width,
            float col2Width);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DestroyMenu(int menuid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int AddMenuItem(int menuid, int column, string menutext);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SetMenuColumnHeader(int menuid, int column, string columnheader);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowMenuForPlayer(int menuid, int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool HideMenuForPlayer(int menuid, int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool IsValidMenu(int menuid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableMenu(int menuid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DisableMenuRow(int menuid, int row);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GetPlayerMenu(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int TextDrawCreate(float x, float y, string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawDestroy(int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawLetterSize(int text, float x, float y);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawTextSize(int text, float x, float y);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawAlignment(int text, int alignment);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawColor(int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawUseBox(int text, bool use);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawBoxColor(int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetShadow(int text, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetOutline(int text, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawBackgroundColor(int text, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawFont(int text, int font);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetProportional(int text, bool set);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetSelectable(int text, bool set);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawShowForPlayer(int playerid, int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawHideForPlayer(int playerid, int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawShowForAll(int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawHideForAll(int text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetString(int text, string str);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetPreviewModel(int text, int modelindex);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetPreviewRot(int text, float rotX, float rotY, float rotZ, float zoom);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool TextDrawSetPreviewVehCol(int text, int color1, int color2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SelectTextDraw(int playerid, int hovercolor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool CancelSelectTextDraw(int playerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int GangZoneCreate(float minx, float miny, float maxx, float maxy);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneDestroy(int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneShowForPlayer(int playerid, int zone, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneShowForAll(int zone, int color);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneHideForPlayer(int playerid, int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneHideForAll(int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneFlashForPlayer(int playerid, int zone, int flashcolor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneFlashForAll(int zone, int flashcolor);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneStopFlashForPlayer(int playerid, int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool GangZoneStopFlashForAll(int zone);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int Create3DTextLabel(string text, int color, float x, float y, float z, float drawDistance,
            int virtualWorld, bool testLOS);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Delete3DTextLabel(int id);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Attach3DTextLabelToPlayer(int id, int playerid, float offsetX, float offsetY,
            float offsetZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Attach3DTextLabelToVehicle(int id, int vehicleid, float offsetX, float offsetY,
            float offsetZ);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool Update3DTextLabelText(int id, int color, string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int CreatePlayer3DTextLabel(int playerid, string text, int color, float x, float y, float z,
            float drawDistance, int attachedplayer, int attachedvehicle, bool testLOS);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool DeletePlayer3DTextLabel(int playerid, int id);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool UpdatePlayer3DTextLabelText(int playerid, int id, int color, string text);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool ShowPlayerDialog(int playerid, int dialogid, int style, string caption, string info,
            string button1, string button2);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int SetTimer(int interval, bool repeat, object args);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool KillTimer(int timerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        // ReSharper disable once InconsistentNaming
        public static extern bool gpci(int playerid, out string buffer, int size);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool SendDeathMessageToPlayer(int playerid, int killer, int killee, int weapon);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool BlockIpAddress(string ipAddress, int timems);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool UnBlockIpAddress(string ipAddress);
    }
}