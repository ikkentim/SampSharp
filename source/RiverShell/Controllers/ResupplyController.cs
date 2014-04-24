using RiverShell.World;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;

namespace RiverShell.Controllers
{
    public class ResupplyController : IEventListener, IController
    {
        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerUpdate += gameMode_PlayerUpdate;
        }

        private void gameMode_PlayerUpdate(object sender, PlayerEventArgs e)
        {
            var player = e.Player as RPlayer;

            //Check the player is on foot
            if (player.PlayerState != PlayerState.OnFoot) return;

            //Check if in range of resupply position
            if (!player.IsInRangeOfPoint(2.5f, GameMode.BlueTeam.ResupplyPosition) &&
                !player.IsInRangeOfPoint(2.5f, GameMode.GreenTeam.ResupplyPosition)) return;

            //Check if we haven't resupplied recently
            if (player.LastResupplyTime != 0 &&
                (Native.GetTickCount() - player.LastResupplyTime) <= Config.ResupplyTime*1000) return;

            //Resupply
            player.LastResupplyTime = Native.GetTickCount();

            player.ResetWeapons();
            player.GiveWeapon((Weapon)31, 100);
            player.GiveWeapon((Weapon)29, 200);
            player.GiveWeapon((Weapon)34, 10);

            player.Health = 100.0f;
            player.Armour = 100.0f;

            player.GameText("Resupplied", 2000, 5);
            player.PlaySound(1150);
        }
    }
}
