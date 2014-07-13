using RiverShell.World;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;
using SampSharp.GameMode.World.Shapes;

namespace RiverShell.Controllers
{
    public class ResupplyController : IController
    {
        public ResupplyController()
        {
            Region blueResupply = new Region(new Sphere(GameMode.BlueTeam.ResupplyPosition, 2.5f));
            Region greenResupply = new Region(new Sphere(GameMode.GreenTeam.ResupplyPosition, 2.5f));

            blueResupply.Enter += Enter;
            greenResupply.Enter += Enter;
        }

        private void Enter(object sender, PlayerEventArgs e)
        {
            var player = e.Player as RPlayer;

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
