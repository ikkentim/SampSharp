// SampSharp
// Copyright 2017 Tim Potze
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

using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.GameMode
{
    public abstract partial class BaseMode
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public class BaseModeInternal : NativeObjectSingleton<BaseModeInternal>
        {
            [NativeMethod]
            public virtual bool ManualVehicleEngineAndLights()
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool EnableStuntBonusForAll(bool enable)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool UsePlayerPedAnims()
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ShowPlayerMarkers(int mode)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int AddPlayerClass(int modelid, float spawnX, float spawnY, float spawnZ, float zAngle,
                int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual int AddPlayerClassEx(int teamid, int modelid, float spawnX, float spawnY, float spawnZ,
                float zAngle, int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool LimitGlobalChatRadius(float chatRadius)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool LimitPlayerMarkerRadius(float markerRadius)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool GameModeExit()
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool ShowNameTags(bool show)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetTeamCount(int count)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetGameModeText(string text)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool EnableTirePopping(bool enable)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool EnableVehicleFriendlyFire()
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool AllowInteriorWeapons(bool allow)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetGravity(float gravity)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual float GetGravity()
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DisableInteriorEnterExits()
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool SetNameTagDrawDistance(float distance)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod]
            public virtual bool DisableNameTagLOS()
            {
                throw new NativeNotImplementedException();
            }
        }
#pragma warning restore CS1591
    }
}