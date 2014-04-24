using RiverShell.World;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;
using SampSharp.GameMode.Events;

namespace RiverShell.Controllers
{
    public class ObjectiveController : IEventListener, IController
    {
        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerEnterCheckpoint += gameMode_PlayerEnterCheckpoint;
            gameMode.PlayerStateChanged += gameMode_PlayerStateChanged;
        }

        private void gameMode_PlayerStateChanged(object sender, PlayerStateEventArgs e)
        {
            var player = e.Player as RPlayer;

            switch (e.NewState)
            {
                case PlayerState.Driving:
                    if (player.Vehicle == player.Team.TargetVehicle)
                    {
                        // It's the objective vehicle
                        player.Color = 0xE2C063FF;
                        player.GameText("~w~Take the ~y~boat ~w~back to the ~r~spawn!", 3000, 5);
                        player.SetCheckpoint(player.Team.Target, 10.0f);
                    }
                    break;
                case PlayerState.OnFoot:
                    player.Color = player.Team.Color;
                    player.DisableCheckpoint();
                    break;
            }
        }

        private void gameMode_PlayerEnterCheckpoint(object sender, PlayerEventArgs e)
        {
            var player = e.Player as RPlayer;

            var vehicle = player.Vehicle;

            //Check if game's already over
            if (GameMode.ObjectiveReached)
                return;

            //Check if we're in target vehicle
            if (vehicle != player.Team.TargetVehicle) return;

            // objective reached.
            player.Team.TimesCaptured++;
            player.Score += 5;

            if (player.Team.TimesCaptured == Config.CapturesToWin)
            {
                RPlayer.GameTextForAll(string.Format("{0} wins!", player.Team.GameTextTeamName), 3000, 5);
                GameMode.ObjectiveReached = true;
                foreach (var p in RPlayer.All)
                    p.PlaySound(1185);

                var exitTimer = new Timer(6000, false);
                exitTimer.Tick += (tsender, args) =>
                {
                    foreach (var p in RPlayer.All)
                        p.PlaySound(1186);
                    Native.GameModeExit();
                };
            }
            else
            {
                RPlayer.GameTextForAll(string.Format("{0} captured the ~y~boat!", player.Team.GameTextTeamName), 3000, 5);
                vehicle.Respawn();
            }
        }
    }
}
