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

namespace SampSharp.GameMode.Natives
{
    /// <summary>
    ///     Contains all native methods.
    /// </summary>
    public static partial class Native
    {
        public delegate int AddMenuItemImpl(int menuid, int column, string menutext);

        public delegate int AddPlayerClassExImpl(
            int teamid, int modelid, float spawnX, float spawnY, float spawnZ, float zAngle, int weapon1,
            int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        public delegate int AddPlayerClassImpl(
            int modelid, float spawnX, float spawnY, float spawnZ, float zAngle, int weapon1, int weapon1Ammo,
            int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

        public delegate int AddStaticPickupImpl(int model, int type, float x, float y, float z, int virtualworld);

        public delegate int AddStaticVehicleExImpl(
            int modelid, float spawnX, float spawnY, float spawnZ, float zAngle, int color1, int color2,
            int respawnDelay, bool addsiren = false);

        public delegate int AddStaticVehicleImpl(
            int modelid, float spawnX, float spawnY, float spawnZ, float zAngle, int color1, int color2);

        public delegate bool AllowInteriorWeaponsImpl(bool allow);

        public delegate bool Attach3DTextLabelToPlayerImpl(
            int id, int playerid, float offsetX, float offsetY, float offsetZ);

        public delegate bool Attach3DTextLabelToVehicleImpl(
            int id, int vehicleid, float offsetX, float offsetY, float offsetZ);

        public delegate bool BanExImpl(int playerid, string reason);

        public delegate bool BanImpl(int playerid);

        public delegate bool BlockIpAddressImpl(string ipAddress, int timems);

        public delegate bool CancelSelectTextDrawImpl(int playerid);

        public delegate bool ConnectNPCImpl(string name, string script);

        public delegate int Create3DTextLabelImpl(
            string text, int color, float x, float y, float z, float drawDistance, int virtualWorld, bool testLOS);

        public delegate bool CreateExplosionImpl(float x, float y, float z, int type, float radius);

        public delegate int CreateMenuImpl(string title, int columns, float x, float y, float col1Width, float col2Width
            );

        public delegate int CreatePickupImpl(int model, int type, float x, float y, float z, int virtualworld);

        public delegate int CreatePlayer3DTextLabelImpl(
            int playerid, string text, int color, float x, float y, float z, float drawDistance, int attachedplayer,
            int attachedvehicle, bool testLOS);

        public delegate bool Delete3DTextLabelImpl(int id);

        public delegate bool DeletePlayer3DTextLabelImpl(int playerid, int id);

        public delegate bool DestroyMenuImpl(int menuid);

        public delegate bool DestroyPickupImpl(int pickupid);

        public delegate bool DisableInteriorEnterExitsImpl();

        public delegate bool DisableMenuImpl(int menuid);

        public delegate bool DisableMenuRowImpl(int menuid, int row);

        public delegate bool DisableNameTagLOSImpl();

        public delegate bool EnableTirePoppingImpl(bool enable);

        public delegate bool EnableVehicleFriendlyFireImpl();

        public delegate bool GameModeExitImpl();

        public delegate bool GameTextForAllImpl(string text, int time, int style);

        public delegate bool GameTextForPlayerImpl(int playerid, string text, int time, int style);

        public delegate int GangZoneCreateImpl(float minx, float miny, float maxx, float maxy);

        public delegate bool GangZoneDestroyImpl(int zone);

        public delegate bool GangZoneFlashForAllImpl(int zone, int flashcolor);

        public delegate bool GangZoneFlashForPlayerImpl(int playerid, int zone, int flashcolor);

        public delegate bool GangZoneHideForAllImpl(int zone);

        public delegate bool GangZoneHideForPlayerImpl(int playerid, int zone);

        public delegate bool GangZoneShowForAllImpl(int zone, int color);

        public delegate bool GangZoneShowForPlayerImpl(int playerid, int zone, int color);

        public delegate bool GangZoneStopFlashForAllImpl(int zone);

        public delegate bool GangZoneStopFlashForPlayerImpl(int playerid, int zone);

        public delegate int GetActorPoolSizeImpl();

        public delegate float GetGravityImpl();

        public delegate int GetMaxPlayersImpl();

        public delegate bool GetNetworkStatsImpl(out string retstr, int size);

        public delegate int GetPlayerMenuImpl(int playerid);

        public delegate bool GetPlayerNetworkStatsImpl(int playerid, out string retstr, int size);

        public delegate int GetPlayerPoolSizeImpl();

        public delegate bool GetPlayerVersionImpl(int playerid, out string version, int len);

        public delegate bool GetServerVarAsBoolImpl(string varname);

        public delegate int GetServerVarAsIntImpl(string varname);

        public delegate bool GetServerVarAsStringImpl(string varname, out string value, int size);

        public delegate int GetTickCountImpl();

        public delegate int GetVehiclePoolSizeImpl();

        public delegate bool GetWeaponNameImpl(int weaponid, out string name, int size);

        public delegate bool gpciImpl(int playerid, out string buffer, int size);

        public delegate bool HideMenuForPlayerImpl(int menuid, int playerid);

        public delegate bool IsPlayerAdminImpl(int playerid);

        public delegate bool IsPlayerNPCImpl(int playerid);

        public delegate bool IsValidMenuImpl(int menuid);

        public delegate bool KickImpl(int playerid);

        public delegate bool LimitGlobalChatRadiusImpl(float chatRadius);

        public delegate bool LimitPlayerMarkerRadiusImpl(float markerRadius);

        public delegate bool SelectTextDrawImpl(int playerid, int hovercolor);

        public delegate bool SendClientMessageImpl(int playerid, int color, string message);

        public delegate bool SendClientMessageToAllImpl(int color, string message);

        public delegate bool SendDeathMessageImpl(int killer, int killee, int weapon);

        public delegate bool SendDeathMessageToPlayerImpl(int playerid, int killer, int killee, int weapon);

        public delegate bool SendPlayerMessageToAllImpl(int senderid, string message);

        public delegate bool SendPlayerMessageToPlayerImpl(int playerid, int senderid, string message);

        public delegate bool SendRconCommandImpl(string command);

        public delegate bool SetGameModeTextImpl(string text);

        public delegate bool SetGravityImpl(float gravity);

        public delegate bool SetMenuColumnHeaderImpl(int menuid, int column, string columnheader);

        public delegate bool SetNameTagDrawDistanceImpl(float distance);

        public delegate bool SetTeamCountImpl(int count);

        public delegate bool SetWeatherImpl(int weatherid);

        public delegate bool SetWorldTimeImpl(int hour);

        public delegate bool ShowMenuForPlayerImpl(int menuid, int playerid);

        public delegate bool ShowNameTagsImpl(bool show);

        public delegate bool ShowPlayerDialogImpl(
            int playerid, int dialogid, int style, string caption, string info, string button1, string button2);

        public delegate bool ShowPlayerMarkersImpl(int mode);

        public delegate bool TextDrawAlignmentImpl(int text, int alignment);

        public delegate bool TextDrawBackgroundColorImpl(int text, int color);

        public delegate bool TextDrawBoxColorImpl(int text, int color);

        public delegate bool TextDrawColorImpl(int text, int color);

        public delegate int TextDrawCreateImpl(float x, float y, string text);

        public delegate bool TextDrawDestroyImpl(int text);

        public delegate bool TextDrawFontImpl(int text, int font);

        public delegate bool TextDrawHideForAllImpl(int text);

        public delegate bool TextDrawHideForPlayerImpl(int playerid, int text);

        public delegate bool TextDrawLetterSizeImpl(int text, float x, float y);

        public delegate bool TextDrawSetOutlineImpl(int text, int size);

        public delegate bool TextDrawSetPreviewModelImpl(int text, int modelindex);

        public delegate bool TextDrawSetPreviewRotImpl(int text, float rotX, float rotY, float rotZ, float zoom);

        public delegate bool TextDrawSetPreviewVehColImpl(int text, int color1, int color2);

        public delegate bool TextDrawSetProportionalImpl(int text, bool set);

        public delegate bool TextDrawSetSelectableImpl(int text, bool set);

        public delegate bool TextDrawSetShadowImpl(int text, int size);

        public delegate bool TextDrawSetStringImpl(int text, string str);

        public delegate bool TextDrawShowForAllImpl(int text);

        public delegate bool TextDrawShowForPlayerImpl(int playerid, int text);

        public delegate bool TextDrawTextSizeImpl(int text, float x, float y);

        public delegate bool TextDrawUseBoxImpl(int text, bool use);

        public delegate bool UnBlockIpAddressImpl(string ipAddress);

        public delegate bool Update3DTextLabelTextImpl(int id, int color, string text);

        public delegate bool UpdatePlayer3DTextLabelTextImpl(int playerid, int id, int color, string text);

        public delegate bool UsePlayerPedAnimsImpl();

        [Native("SendClientMessage")] public static readonly SendClientMessageImpl SendClientMessage = null;

        [Native("SendClientMessageToAll")] public static readonly SendClientMessageToAllImpl SendClientMessageToAll =
            null;

        [Native("SendPlayerMessageToPlayer")] public static readonly SendPlayerMessageToPlayerImpl
            SendPlayerMessageToPlayer = null;

        [Native("SendPlayerMessageToAll")] public static readonly SendPlayerMessageToAllImpl SendPlayerMessageToAll =
            null;

        [Native("SendDeathMessage")] public static readonly SendDeathMessageImpl SendDeathMessage = null;
        [Native("GameTextForAll")] public static readonly GameTextForAllImpl GameTextForAll = null;
        [Native("GameTextForPlayer")] public static readonly GameTextForPlayerImpl GameTextForPlayer = null;
        [Native("GetTickCount")] public static readonly GetTickCountImpl GetTickCount = null;
        [Native("GetMaxPlayers")] public static readonly GetMaxPlayersImpl GetMaxPlayers = null;
        [Native("GetPlayerPoolSize")] public static readonly GetPlayerPoolSizeImpl GetPlayerPoolSize = null;
        [Native("GetVehiclePoolSize")] public static readonly GetVehiclePoolSizeImpl GetVehiclePoolSize = null;
        [Native("GetActorPoolSize")] public static readonly GetActorPoolSizeImpl GetActorPoolSize = null;
        [Native("SetGameModeText")] public static readonly SetGameModeTextImpl SetGameModeText = null;
        [Native("SetTeamCount")] public static readonly SetTeamCountImpl SetTeamCount = null;
        [Native("AddPlayerClass")] public static readonly AddPlayerClassImpl AddPlayerClass = null;
        [Native("AddPlayerClassEx")] public static readonly AddPlayerClassExImpl AddPlayerClassEx = null;
        [Native("AddStaticVehicle")] public static readonly AddStaticVehicleImpl AddStaticVehicle = null;
        [Native("AddStaticVehicleEx")] public static readonly AddStaticVehicleExImpl AddStaticVehicleEx = null;
        [Native("AddStaticPickup")] public static readonly AddStaticPickupImpl AddStaticPickup = null;
        [Native("CreatePickup")] public static readonly CreatePickupImpl CreatePickup = null;
        [Native("DestroyPickup")] public static readonly DestroyPickupImpl DestroyPickup = null;
        [Native("ShowNameTags")] public static readonly ShowNameTagsImpl ShowNameTags = null;
        [Native("ShowPlayerMarkers")] public static readonly ShowPlayerMarkersImpl ShowPlayerMarkers = null;
        [Native("GameModeExit")] public static readonly GameModeExitImpl GameModeExit = null;
        [Native("SetWorldTime")] public static readonly SetWorldTimeImpl SetWorldTime = null;
        [Native("GetWeaponName")] public static readonly GetWeaponNameImpl GetWeaponName = null;
        [Native("EnableTirePopping")] public static readonly EnableTirePoppingImpl EnableTirePopping = null;

        [Native("EnableVehicleFriendlyFire")] public static readonly EnableVehicleFriendlyFireImpl
            EnableVehicleFriendlyFire = null;

        [Native("AllowInteriorWeapons")] public static readonly AllowInteriorWeaponsImpl AllowInteriorWeapons = null;
        [Native("SetWeather")] public static readonly SetWeatherImpl SetWeather = null;
        [Native("SetGravity")] public static readonly SetGravityImpl SetGravity = null;
        [Native("GetGravity")] public static readonly GetGravityImpl GetGravity = null;
        [Native("CreateExplosion")] public static readonly CreateExplosionImpl CreateExplosion = null;
        [Native("UsePlayerPedAnims")] public static readonly UsePlayerPedAnimsImpl UsePlayerPedAnims = null;

        [Native("DisableInteriorEnterExits")] public static readonly DisableInteriorEnterExitsImpl
            DisableInteriorEnterExits = null;

        [Native("SetNameTagDrawDistance")] public static readonly SetNameTagDrawDistanceImpl SetNameTagDrawDistance =
            null;

        [Native("DisableNameTagLOS")] public static readonly DisableNameTagLOSImpl DisableNameTagLOS = null;
        [Native("LimitGlobalChatRadius")] public static readonly LimitGlobalChatRadiusImpl LimitGlobalChatRadius = null;

        [Native("LimitPlayerMarkerRadius")] public static readonly LimitPlayerMarkerRadiusImpl LimitPlayerMarkerRadius =
            null;

        [Native("ConnectNPC")] public static readonly ConnectNPCImpl ConnectNPC = null;
        [Native("IsPlayerNPC")] public static readonly IsPlayerNPCImpl IsPlayerNPC = null;
        [Native("IsPlayerAdmin")] public static readonly IsPlayerAdminImpl IsPlayerAdmin = null;
        [Native("Kick")] public static readonly KickImpl Kick = null;
        [Native("Ban")] public static readonly BanImpl Ban = null;
        [Native("BanEx")] public static readonly BanExImpl BanEx = null;
        [Native("SendRconCommand")] public static readonly SendRconCommandImpl SendRconCommand = null;
        [Native("GetServerVarAsString")] public static readonly GetServerVarAsStringImpl GetServerVarAsString = null;
        [Native("GetServerVarAsInt")] public static readonly GetServerVarAsIntImpl GetServerVarAsInt = null;
        [Native("GetServerVarAsBool")] public static readonly GetServerVarAsBoolImpl GetServerVarAsBool = null;
        [Native("GetPlayerNetworkStats")] public static readonly GetPlayerNetworkStatsImpl GetPlayerNetworkStats = null;
        [Native("GetNetworkStats")] public static readonly GetNetworkStatsImpl GetNetworkStats = null;
        [Native("GetPlayerVersion")] public static readonly GetPlayerVersionImpl GetPlayerVersion = null;
        [Native("CreateMenu")] public static readonly CreateMenuImpl CreateMenu = null;
        [Native("DestroyMenu")] public static readonly DestroyMenuImpl DestroyMenu = null;
        [Native("AddMenuItem")] public static readonly AddMenuItemImpl AddMenuItem = null;
        [Native("SetMenuColumnHeader")] public static readonly SetMenuColumnHeaderImpl SetMenuColumnHeader = null;
        [Native("ShowMenuForPlayer")] public static readonly ShowMenuForPlayerImpl ShowMenuForPlayer = null;
        [Native("HideMenuForPlayer")] public static readonly HideMenuForPlayerImpl HideMenuForPlayer = null;
        [Native("IsValidMenu")] public static readonly IsValidMenuImpl IsValidMenu = null;
        [Native("DisableMenu")] public static readonly DisableMenuImpl DisableMenu = null;
        [Native("DisableMenuRow")] public static readonly DisableMenuRowImpl DisableMenuRow = null;
        [Native("GetPlayerMenu")] public static readonly GetPlayerMenuImpl GetPlayerMenu = null;
        [Native("TextDrawCreate")] public static readonly TextDrawCreateImpl TextDrawCreate = null;
        [Native("TextDrawDestroy")] public static readonly TextDrawDestroyImpl TextDrawDestroy = null;
        [Native("TextDrawLetterSize")] public static readonly TextDrawLetterSizeImpl TextDrawLetterSize = null;
        [Native("TextDrawTextSize")] public static readonly TextDrawTextSizeImpl TextDrawTextSize = null;
        [Native("TextDrawAlignment")] public static readonly TextDrawAlignmentImpl TextDrawAlignment = null;
        [Native("TextDrawColor")] public static readonly TextDrawColorImpl TextDrawColor = null;
        [Native("TextDrawUseBox")] public static readonly TextDrawUseBoxImpl TextDrawUseBox = null;
        [Native("TextDrawBoxColor")] public static readonly TextDrawBoxColorImpl TextDrawBoxColor = null;
        [Native("TextDrawSetShadow")] public static readonly TextDrawSetShadowImpl TextDrawSetShadow = null;
        [Native("TextDrawSetOutline")] public static readonly TextDrawSetOutlineImpl TextDrawSetOutline = null;

        [Native("TextDrawBackgroundColor")] public static readonly TextDrawBackgroundColorImpl TextDrawBackgroundColor =
            null;

        [Native("TextDrawFont")] public static readonly TextDrawFontImpl TextDrawFont = null;

        [Native("TextDrawSetProportional")] public static readonly TextDrawSetProportionalImpl TextDrawSetProportional =
            null;

        [Native("TextDrawSetSelectable")] public static readonly TextDrawSetSelectableImpl TextDrawSetSelectable = null;
        [Native("TextDrawShowForPlayer")] public static readonly TextDrawShowForPlayerImpl TextDrawShowForPlayer = null;
        [Native("TextDrawHideForPlayer")] public static readonly TextDrawHideForPlayerImpl TextDrawHideForPlayer = null;
        [Native("TextDrawShowForAll")] public static readonly TextDrawShowForAllImpl TextDrawShowForAll = null;
        [Native("TextDrawHideForAll")] public static readonly TextDrawHideForAllImpl TextDrawHideForAll = null;
        [Native("TextDrawSetString")] public static readonly TextDrawSetStringImpl TextDrawSetString = null;

        [Native("TextDrawSetPreviewModel")] public static readonly TextDrawSetPreviewModelImpl TextDrawSetPreviewModel =
            null;

        [Native("TextDrawSetPreviewRot")] public static readonly TextDrawSetPreviewRotImpl TextDrawSetPreviewRot = null;

        [Native("TextDrawSetPreviewVehCol")] public static readonly TextDrawSetPreviewVehColImpl
            TextDrawSetPreviewVehCol = null;

        [Native("SelectTextDraw")] public static readonly SelectTextDrawImpl SelectTextDraw = null;
        [Native("CancelSelectTextDraw")] public static readonly CancelSelectTextDrawImpl CancelSelectTextDraw = null;
        [Native("GangZoneCreate")] public static readonly GangZoneCreateImpl GangZoneCreate = null;
        [Native("GangZoneDestroy")] public static readonly GangZoneDestroyImpl GangZoneDestroy = null;
        [Native("GangZoneShowForPlayer")] public static readonly GangZoneShowForPlayerImpl GangZoneShowForPlayer = null;
        [Native("GangZoneShowForAll")] public static readonly GangZoneShowForAllImpl GangZoneShowForAll = null;
        [Native("GangZoneHideForPlayer")] public static readonly GangZoneHideForPlayerImpl GangZoneHideForPlayer = null;
        [Native("GangZoneHideForAll")] public static readonly GangZoneHideForAllImpl GangZoneHideForAll = null;

        [Native("GangZoneFlashForPlayer")] public static readonly GangZoneFlashForPlayerImpl GangZoneFlashForPlayer =
            null;

        [Native("GangZoneFlashForAll")] public static readonly GangZoneFlashForAllImpl GangZoneFlashForAll = null;

        [Native("GangZoneStopFlashForPlayer")] public static readonly GangZoneStopFlashForPlayerImpl
            GangZoneStopFlashForPlayer = null;

        [Native("GangZoneStopFlashForAll")] public static readonly GangZoneStopFlashForAllImpl GangZoneStopFlashForAll =
            null;

        [Native("Create3DTextLabel")] public static readonly Create3DTextLabelImpl Create3DTextLabel = null;
        [Native("Delete3DTextLabel")] public static readonly Delete3DTextLabelImpl Delete3DTextLabel = null;

        [Native("Attach3DTextLabelToPlayer")] public static readonly Attach3DTextLabelToPlayerImpl
            Attach3DTextLabelToPlayer = null;

        [Native("Attach3DTextLabelToVehicle")] public static readonly Attach3DTextLabelToVehicleImpl
            Attach3DTextLabelToVehicle = null;

        [Native("Update3DTextLabelText")] public static readonly Update3DTextLabelTextImpl Update3DTextLabelText = null;

        [Native("CreatePlayer3DTextLabel")] public static readonly CreatePlayer3DTextLabelImpl CreatePlayer3DTextLabel =
            null;

        [Native("DeletePlayer3DTextLabel")] public static readonly DeletePlayer3DTextLabelImpl DeletePlayer3DTextLabel =
            null;

        [Native("UpdatePlayer3DTextLabelText")] public static readonly UpdatePlayer3DTextLabelTextImpl
            UpdatePlayer3DTextLabelText = null;

        [Native("ShowPlayerDialog")] public static readonly ShowPlayerDialogImpl ShowPlayerDialog = null;
        [Native("gpci")] public static readonly gpciImpl gpci = null;

        [Native("SendDeathMessageToPlayer")] public static readonly SendDeathMessageToPlayerImpl
            SendDeathMessageToPlayer = null;

        [Native("BlockIpAddress")] public static readonly BlockIpAddressImpl BlockIpAddress = null;
        [Native("UnBlockIpAddress")] public static readonly UnBlockIpAddressImpl UnBlockIpAddress = null;
    }
}