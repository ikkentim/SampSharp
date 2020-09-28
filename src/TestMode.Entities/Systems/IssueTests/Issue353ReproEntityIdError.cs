using System;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.IssueTests
{
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
}