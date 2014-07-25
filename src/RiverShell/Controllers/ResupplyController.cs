using RiverShell.World;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;

namespace RiverShell.Controllers
{
    public class ResupplyController : IController, IEventListener
    {
        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerUpdate += (sender, args) =>
            {
                RPlayer player = args.Player as RPlayer;
                if (player.IsInRangeOfPoint(2.5f, GameMode.BlueTeam.ResupplyPosition))
                {
                    Resupply(player);
                }
            };
        }

        private void Resupply(RPlayer player)
        {
            //Check if we haven't resupplied recently
            if (player.LastResupplyTime != 0 &&
                (Native.GetTickCount() - player.LastResupplyTime) <= Config.ResupplyTime * 1000) return;

            //Resupply
            player.LastResupplyTime = Native.GetTickCount();

            player.ResetWeapons();
            player.GiveWeapon(Weapon.M4, 100);
            player.GiveWeapon(Weapon.MP5, 200);
            player.GiveWeapon(Weapon.Sniper, 10);

            player.Health = 100.0f;
            player.Armour = 100.0f;

            player.GameText("Resupplied", 2000, 5);
            player.PlaySound(1150);
        }
    }
}
