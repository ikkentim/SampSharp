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

using SampSharp.GameMode.API;

namespace SampSharp.GameMode
{
    public abstract partial class BaseMode
    {
        private static class Internal
        {
            public delegate int AddPlayerClassExImpl(
                int teamid, int modelid, float spawnX, float spawnY, float spawnZ, float zAngle, int weapon1,
                int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

            public delegate int AddPlayerClassImpl(
                int modelid, float spawnX, float spawnY, float spawnZ, float zAngle, int weapon1, int weapon1Ammo,
                int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo);

            public delegate bool AllowInteriorWeaponsImpl(bool allow);

            public delegate bool DisableInteriorEnterExitsImpl();

            public delegate bool DisableNameTagLOSImpl();

            public delegate bool EnableStuntBonusForAllImpl(bool enable);

            public delegate bool EnableTirePoppingImpl(bool enable);

            public delegate bool EnableVehicleFriendlyFireImpl();

            public delegate bool GameModeExitImpl();

            public delegate float GetGravityImpl();

            public delegate bool LimitGlobalChatRadiusImpl(float chatRadius);

            public delegate bool LimitPlayerMarkerRadiusImpl(float markerRadius);

            public delegate bool ManualVehicleEngineAndLightsImpl();

            public delegate bool SetGameModeTextImpl(string text);

            public delegate bool SetGravityImpl(float gravity);

            public delegate bool SetNameTagDrawDistanceImpl(float distance);

            public delegate bool SetTeamCountImpl(int count);

            public delegate bool ShowNameTagsImpl(bool show);

            public delegate bool ShowPlayerMarkersImpl(int mode);

            public delegate bool UsePlayerPedAnimsImpl();

            [Native("ManualVehicleEngineAndLights")] public static readonly ManualVehicleEngineAndLightsImpl
                NativeManualVehicleEngineAndLights = null;

            [Native("EnableStuntBonusForAll")] public static readonly EnableStuntBonusForAllImpl
                NativeEnableStuntBonusForAll =
                    null;

            [Native("UsePlayerPedAnims")] public static readonly UsePlayerPedAnimsImpl UsePlayerPedAnims = null;

            [Native("ShowPlayerMarkers")] public static readonly ShowPlayerMarkersImpl NativeShowPlayerMarkers = null;

            [Native("AddPlayerClass")] public static readonly AddPlayerClassImpl NativeAddPlayerClass = null;
            [Native("AddPlayerClassEx")] public static readonly AddPlayerClassExImpl NativeAddPlayerClassEx = null;

            [Native("LimitGlobalChatRadius")] public static readonly LimitGlobalChatRadiusImpl
                NativeLimitGlobalChatRadius = null;

            [Native("LimitPlayerMarkerRadius")] public static readonly LimitPlayerMarkerRadiusImpl
                NativeLimitPlayerMarkerRadius =
                    null;

            [Native("GameModeExit")] public static readonly GameModeExitImpl GameModeExit = null;

            [Native("ShowNameTags")] public static readonly ShowNameTagsImpl NativeShowNameTags = null;

            [Native("SetTeamCount")] public static readonly SetTeamCountImpl NativeSetTeamCount = null;

            [Native("SetGameModeText")] public static readonly SetGameModeTextImpl NativeSetGameModeText = null;

            [Native("EnableTirePopping")] public static readonly EnableTirePoppingImpl NativeEnableTirePopping = null;

            [Native("EnableVehicleFriendlyFire")] public static readonly EnableVehicleFriendlyFireImpl
                NativeEnableVehicleFriendlyFire = null;

            [Native("AllowInteriorWeapons")] public static readonly AllowInteriorWeaponsImpl NativeAllowInteriorWeapons
                = null;

            [Native("SetGravity")] public static readonly SetGravityImpl NativeSetGravity = null;
            [Native("GetGravity")] public static readonly GetGravityImpl NativeGetGravity = null;

            [Native("DisableInteriorEnterExits")] public static readonly DisableInteriorEnterExitsImpl
                NativeDisableInteriorEnterExits = null;

            [Native("SetNameTagDrawDistance")] public static readonly SetNameTagDrawDistanceImpl
                NativeSetNameTagDrawDistance =
                    null;

            [Native("DisableNameTagLOS")] public static readonly DisableNameTagLOSImpl NativeDisableNameTagLOS = null;
        }
    }
}