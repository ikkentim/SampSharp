// SampSharp
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

using System;

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a service for controlling the SA:MP server.</summary>
public class ServerService : IServerService
{
    private readonly ServerServiceNative _native;

    /// <summary>Initializes a new instance of the <see cref="ServerService" /> class.</summary>
    public ServerService(INativeProxy<ServerServiceNative> nativeProxy,
        INativeProxy<VariableCollection.ServerVariableCollectionNatives> serverVariablesNativeProxy)
    {
        _native = nativeProxy.Instance;
        Variables = new VariableCollection(serverVariablesNativeProxy.Instance);
    }

    /// <inheritdoc />
    public int TickCount => _native.GetTickCount();

    /// <inheritdoc />
    public int MaxPlayers => _native.GetMaxPlayers();

    /// <inheritdoc />
    public int PlayerPoolSize => _native.GetPlayerPoolSize();

    /// <inheritdoc />
    public int VehiclePoolSize => _native.GetVehiclePoolSize();

    /// <inheritdoc />
    public VariableCollection Variables { get; }

    /// <inheritdoc />
    public int ActorPoolSize => _native.GetActorPoolSize();

    /// <inheritdoc />
    public int TickRate => _native.GetServerTickRate();

    /// <inheritdoc />
    public string NetworkStats
    {
        get
        {
            _native.GetNetworkStats(out var buffer, 1024);
            return buffer;
        }
    }

    /// <inheritdoc />
    public void SetGameModeText(string text)
    {
        _native.SetGameModeText(text);
    }

    /// <inheritdoc />
    public void SetTeamCount(int count)
    {
        _native.SetTeamCount(count);
    }

    /// <inheritdoc />
    public void ShowNameTags(bool show)
    {
        _native.ShowNameTags(show);
    }

    /// <inheritdoc />
    public void ShowPlayerMarkers(PlayerMarkersMode mode)
    {
        _native.ShowPlayerMarkers((int)mode);
    }

    /// <inheritdoc />
    public void GameModeExit()
    {
        _native.GameModeExit();
    }

    /// <inheritdoc />
    public void SetWorldTime(int hour)
    {
        _native.SetWorldTime(hour);
    }

    /// <inheritdoc />
    public int AddPlayerClass(int modelId, Vector3 spawnPosition, float angle, Weapon weapon1 = Weapon.Unarmed, int weapon1Ammo = 0,
        Weapon weapon2 = Weapon.Unarmed, int weapon2Ammo = 0, Weapon weapon3 = Weapon.Unarmed, int weapon3Ammo = 0)
    {
        return _native.AddPlayerClass(modelId, spawnPosition.X, spawnPosition.Y, spawnPosition.Z, angle, (int)weapon1, weapon1Ammo, (int)weapon2, weapon2Ammo,
            (int)weapon3, weapon3Ammo);
    }

    /// <inheritdoc />
    public int AddPlayerClass(int teamId, int modelId, Vector3 spawnPosition, float angle, Weapon weapon1 = Weapon.Unarmed, int weapon1Ammo = 0,
        Weapon weapon2 = Weapon.Unarmed, int weapon2Ammo = 0, Weapon weapon3 = Weapon.Unarmed, int weapon3Ammo = 0)
    {
        return _native.AddPlayerClassEx(teamId, modelId, spawnPosition.X, spawnPosition.Y, spawnPosition.Z, angle, (int)weapon1, weapon1Ammo, (int)weapon2,
            weapon2Ammo, (int)weapon3, weapon3Ammo);
    }

    /// <inheritdoc />
    public void EnableVehicleFriendlyFire()
    {
        _native.EnableVehicleFriendlyFire();
    }

    /// <inheritdoc />
    public void UsePlayerPedAnims()
    {
        _native.UsePlayerPedAnims();
    }

    /// <inheritdoc />
    public void DisableInteriorEnterExits()
    {
        _native.DisableInteriorEnterExits();
    }

    /// <inheritdoc />
    public void SetNameTagDrawDistance(float distance = 70.0f)
    {
        _native.SetNameTagDrawDistance(distance);
    }

    /// <inheritdoc />
    public void LimitGlobalChatRadius(float chatRadius)
    {
        _native.LimitGlobalChatRadius(chatRadius);
    }

    /// <inheritdoc />
    public void LimitPlayerMarkerRadius(float markerRadius)
    {
        _native.LimitPlayerMarkerRadius(markerRadius);
    }

    /// <inheritdoc />
    public void ConnectNpc(string name, string script)
    {
        _native.ConnectNPC(name, script);
    }

    /// <inheritdoc />
    public void SendRconCommand(string command)
    {
        _native.SendRconCommand(command);
    }

    /// <inheritdoc />
    public void BlockIpAddress(string ipAddress, TimeSpan time = default)
    {
        _native.BlockIpAddress(ipAddress, (int)time.TotalMilliseconds);
    }

    /// <inheritdoc />
    public void UnBlockIpAddress(string ipAddress)
    {
        _native.UnBlockIpAddress(ipAddress);
    }

    /// <inheritdoc />
    public string GetConsoleVarAsString(string variableName)
    {
        _native.GetConsoleVarAsString(variableName, out var buffer, 1024);
        return buffer;
    }

    /// <inheritdoc />
    public int GetConsoleVarAsInt(string variableName)
    {
        return _native.GetConsoleVarAsInt(variableName);
    }

    /// <inheritdoc />
    public bool GetConsoleVarAsBool(string variableName)
    {
        return _native.GetConsoleVarAsBool(variableName);
    }

    /// <inheritdoc />
    public void ManualVehicleEngineAndLights()
    {
        _native.ManualVehicleEngineAndLights();
    }

    /// <inheritdoc />
    public void EnableStuntBonus(bool enable)
    {
        _native.EnableStuntBonusForAll(enable);
    }
}