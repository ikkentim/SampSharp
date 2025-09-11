﻿// SampSharp
// Copyright 2022 Tim Potze
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

#pragma warning disable 1591

namespace SampSharp.Entities.SAMP;

public class ServerServiceNative
{
    [NativeMethod]
    public virtual int GetTickCount()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetMaxPlayers()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetPlayerPoolSize()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetVehiclePoolSize()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetActorPoolSize()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetGameModeText(string text)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetTeamCount(int count)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool ShowNameTags(bool show)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool ShowPlayerMarkers(int mode)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GameModeExit()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SetWorldTime(int hour)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int AddPlayerClass(int modelId, float spawnX, float spawnY, float spawnZ, float zAngle,
        int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int AddPlayerClassEx(int teamId, int modelId, float spawnX, float spawnY, float spawnZ,
        float zAngle, int weapon1, int weapon1Ammo, int weapon2, int weapon2Ammo, int weapon3, int weapon3Ammo)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool EnableVehicleFriendlyFire()
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool UsePlayerPedAnims()
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
    public virtual bool ConnectNPC(string name, string script)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool SendRconCommand(string command)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetNetworkStats(out string retStr, int size)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool BlockIpAddress(string ipAddress, int timeMs)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool UnBlockIpAddress(string ipAddress)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetConsoleVarAsString(string varName, out string buffer, int len)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetConsoleVarAsInt(string varName)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual bool GetConsoleVarAsBool(string varName)
    {
        throw new NativeNotImplementedException();
    }

    [NativeMethod]
    public virtual int GetServerTickRate()
    {
        throw new NativeNotImplementedException();
    }

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
}