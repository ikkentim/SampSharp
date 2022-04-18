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
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems;

public class BasicCommandsSystem : ISystem
{
    [Event]
    public void OnGameModeInit(IServerService serverService)
    {
        serverService.SetGameModeText("SampSharp.Entities");
    }

    [Event]
    public void OnPlayerWeaponShot(Player player, Weapon weapon, EntityId hit, Vector3 position)
    {
        player.SendClientMessage($"You shot {hit} at {position} with {weapon}");
    }

    [Event]
    public void OnPlayerConnect(Player player)
    {
        Console.WriteLine("I connected! " + player.Entity);
    }

    [Event]
    public void OnPlayerText(Player player, string text)
    {
        Console.WriteLine(player + ": " + text);
    }

    [PlayerCommand]
    public void SpawnCommand(Player sender, VehicleModelType model, IWorldService worldService)
    {
        var vehicle = worldService.CreateVehicle(model, sender.Position + Vector3.Up, 0, -1, -1);
        sender.PutInVehicle(vehicle.Entity);
        sender.SendClientMessage($"{model} spawned!");
    }

    [PlayerCommand]
    public void WeaponCommand(Player player)
    {
        player.GiveWeapon(Weapon.AK47, 100);
        player.SetArmedWeapon(Weapon.AK47);
        player.PlaySound(1083);
    }

    [PlayerCommand("pos")]
    public void PositionCommand(Player player)
    {
        player.SendClientMessage(-1, $"You are at {player.Position}");
    }

}