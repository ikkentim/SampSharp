// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using RiverShell.World;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace RiverShell.Controllers
{
    public class ObjectiveController : IEventListener, IController
    {
        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerEnterCheckpoint += checkpoint_Enter;
            gameMode.PlayerStateChanged += gameMode_PlayerStateChanged;
        }

        private void gameMode_PlayerStateChanged(object sender, PlayerStateEventArgs e)
        {
            var player = e.Player as Player;

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

        private void checkpoint_Enter(object sender, PlayerEventArgs e)
        {
            var player = e.Player as Player;

            GtaVehicle vehicle = player.Vehicle;

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
                Player.GameTextForAll(string.Format("{0} wins!", player.Team.GameTextTeamName), 3000, 5);
                GameMode.ObjectiveReached = true;
                foreach (GtaPlayer p in Player.All)
                    p.PlaySound(1185);

                var exitTimer = new Timer(6000, false);
                exitTimer.Tick += (tsender, args) =>
                {
                    foreach (GtaPlayer p in Player.All)
                        p.PlaySound(1186);
                    Native.GameModeExit();
                };
            }
            else
            {
                Player.GameTextForAll(string.Format("{0} captured the ~y~boat!", player.Team.GameTextTeamName), 3000, 5);
                vehicle.Respawn();
            }
        }
    }
}