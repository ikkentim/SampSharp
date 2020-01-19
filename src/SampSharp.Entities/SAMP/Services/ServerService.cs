// SampSharp
// Copyright 2020 Tim Potze
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
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Represents a service for controlling the SA:MP server.
    /// </summary>
    public class ServerService : IServerService
    {
        /// <summary>
        /// The type of a server entity.
        /// </summary>
        public static readonly Guid ServerType = new Guid("AB9B7255-75A1-4377-B3DC-E55C2F552449");

        /// <summary>
        /// The server entity.
        /// </summary>
        public static readonly EntityId Server = new EntityId(ServerType, 0);

        private readonly IEntityManager _entityManager;

        /// <inheritdoc />
        public ServerService(IEntityManager entityManager, INativeProxy<VariableCollection.ServerVariableCollectionNatives> nativeProxy)
        {
            _entityManager = entityManager;
            Variables = new VariableCollection(nativeProxy.Instance);
        }

        private NativeServer Native => _entityManager.GetComponent<NativeServer>(Server);

        /// <inheritdoc />
        public int TickCount => Native.GetTickCount();

        /// <inheritdoc />
        public int MaxPlayers => Native.GetMaxPlayers();

        /// <inheritdoc />
        public int PlayerPoolSize => Native.GetPlayerPoolSize();

        /// <inheritdoc />
        public int VehiclePoolSize => Native.GetVehiclePoolSize();

        /// <inheritdoc />
        public VariableCollection Variables { get; }

        /// <inheritdoc />
        public int ActorPoolSize => Native.GetActorPoolSize();

        /// <inheritdoc />
        public int TickRate => Native.GetServerTickRate();

        /// <inheritdoc />
        public string NetworkStats
        {
            get
            {
                Native.GetNetworkStats(out var buffer, 1024);
                return buffer;
            }
        }

        /// <inheritdoc />
        public void SetGameModeText(string text)
        {
            Native.SetGameModeText(text);
        }

        /// <inheritdoc />
        public void SetTeamCount(int count)
        {
            Native.SetTeamCount(count);
        }

        /// <inheritdoc />
        public void ShowNameTags(bool show)
        {
            Native.ShowNameTags(show);
        }

        /// <inheritdoc />
        public void ShowPlayerMarkers(PlayerMarkersMode mode)
        {
            Native.ShowPlayerMarkers((int) mode);
        }

        /// <inheritdoc />
        public void GameModeExit()
        {
            Native.GameModeExit();
        }

        /// <inheritdoc />
        public void SetWorldTime(int hour)
        {
            Native.SetWorldTime(hour);
        }

        /// <inheritdoc />
        public int AddPlayerClass(int modelId, Vector3 spawnPosition, float angle, Weapon weapon1 = Weapon.Unarmed,
            int weapon1Ammo = 0, Weapon weapon2 = Weapon.Unarmed, int weapon2Ammo = 0, Weapon weapon3 = Weapon.Unarmed,
            int weapon3Ammo = 0)
        {
            return Native.AddPlayerClass(modelId, spawnPosition.X, spawnPosition.Y, spawnPosition.Z, angle,
                (int) weapon1, weapon1Ammo, (int) weapon2, weapon2Ammo, (int) weapon3, weapon3Ammo);
        }

        /// <inheritdoc />
        public int AddPlayerClass(int teamId, int modelId, Vector3 spawnPosition, float angle,
            Weapon weapon1 = Weapon.Unarmed, int weapon1Ammo = 0, Weapon weapon2 = Weapon.Unarmed, int weapon2Ammo = 0,
            Weapon weapon3 = Weapon.Unarmed, int weapon3Ammo = 0)
        {
            return Native.AddPlayerClassEx(teamId, modelId, spawnPosition.X, spawnPosition.Y, spawnPosition.Z, angle,
                (int) weapon1, weapon1Ammo, (int) weapon2, weapon2Ammo, (int) weapon3, weapon3Ammo);
        }

        /// <inheritdoc />
        public void EnableVehicleFriendlyFire()
        {
            Native.EnableVehicleFriendlyFire();
        }

        /// <inheritdoc />
        public void UsePlayerPedAnims()
        {
            Native.UsePlayerPedAnims();
        }

        /// <inheritdoc />
        public void DisableInteriorEnterExits()
        {
            Native.DisableInteriorEnterExits();
        }

        /// <inheritdoc />
        public void SetNameTagDrawDistance(float distance = 70.0f)
        {
            Native.SetNameTagDrawDistance(distance);
        }

        /// <inheritdoc />
        public void LimitGlobalChatRadius(float chatRadius)
        {
            Native.LimitGlobalChatRadius(chatRadius);
        }

        /// <inheritdoc />
        public void LimitPlayerMarkerRadius(float markerRadius)
        {
            Native.LimitPlayerMarkerRadius(markerRadius);
        }

        /// <inheritdoc />
        public void ConnectNpc(string name, string script)
        {
            Native.ConnectNPC(name, script);
        }

        /// <inheritdoc />
        public void SendRconCommand(string command)
        {
            Native.SendRconCommand(command);
        }

        /// <inheritdoc />
        public void BlockIpAddress(string ipAddress, TimeSpan time = default)
        {
            Native.BlockIpAddress(ipAddress, (int) time.TotalMilliseconds);
        }

        /// <inheritdoc />
        public void UnBlockIpAddress(string ipAddress)
        {
            Native.UnBlockIpAddress(ipAddress);
        }

        /// <inheritdoc />
        public string GetConsoleVarAsString(string variableName)
        {
            Native.GetConsoleVarAsString(variableName, out var buffer, 1024);
            return buffer;
        }

        /// <inheritdoc />
        public int GetConsoleVarAsInt(string variableName)
        {
            return Native.GetConsoleVarAsInt(variableName);
        }

        /// <inheritdoc />
        public bool GetConsoleVarAsBool(string variableName)
        {
            return Native.GetConsoleVarAsBool(variableName);
        }

        /// <inheritdoc />
        public void ManualVehicleEngineAndLights()
        {
            Native.ManualVehicleEngineAndLights();
        }

        /// <inheritdoc />
        public void EnableStuntBonus(bool enable)
        {
            Native.EnableStuntBonusForAll(enable);
        }
    }
}