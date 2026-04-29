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

namespace TestMode.Entities.Systems.IssueTests;

public class Issue353ReproEntityIdError : ISystem
{
    [PlayerCommand]
    public void Die(Player player)
    {
        player.Health = 0;
        player.SendClientMessage("You're dead!");
    }

    [PlayerCommand]
    public void SetVehicleHealth(Player sender, Vehicle vehicle, int health)
    {
        vehicle.Health = health;
        sender.SendClientMessage("Vehicle health set!");
    }

    [Event]
    public void OnVehicleDeath(Vehicle vehicle, Player killerid)
    {
        Console.WriteLine("Work."); // nothing happens
        Console.WriteLine($"Destroy {vehicle.Entity.Handle}"); // nothing happens
    }

    [Event]
    public void OnPlayerDeath(Player player, Player killer, Weapon reason)
    {
        if (killer == null)
        {
            // If the player killed itself, remove their money.
            player.ResetMoney();
        }
        else
        {
            // If the player was killed, give their money to the killer.
            var money = player.Money;
            if (money > 0)
            {
                killer.GiveMoney(money);
                player.ResetMoney();
            }
        }
    }
}