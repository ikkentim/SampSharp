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

using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;

namespace RiverShell.World
{
    public class Player : GtaPlayer
    {
        private Player _lastKiller;
        private SpectateState _spectateState;
        private SpectatingMode _spectatingMode;

        public Player(int id) : base(id)
        {
        }

        public new Team Team
        {
            get { return Team.Find(base.Team); }
            set { base.Team = value.Id; }
        }

        public int LastDeathTick { get; set; }

        public int LastResupplyTime { get; set; }

        public override void OnRequestClass(PlayerRequestClassEventArgs e)
        {
            Position = new Vector(1984.4445f, 157.9501f, 55.9384f);
            CameraPosition = new Vector(1984.4445f, 160.9501f, 55.9384f);
            SetCameraLookAt(new Vector(1984.4445f, 157.9501f, 55.9384f));
            Angle = 0;

            switch (e.ClassId)
            {
                case 0:
                case 1:
                    Team = GameMode.GreenTeam;
                    break;
                case 2:
                case 3:
                    Team = GameMode.BlueTeam;
                    break;
            }

            GameText(Team.GameTextTeamName, 1000, 5);
            base.OnRequestClass(e);
        }

        public override void OnConnected(PlayerEventArgs e)
        {
            GameText("~r~SA-MP: ~w~Rivershell", 2000, 5);

            Color = 0x888888FF;
            SetWorldBounds(2500.0f, 1850.0f, 631.2963f, -454.9898f);

            if (PVars.Get<int>("BuildingsRemoved") == 0)
            {
                GlobalObject.Remove(this, 9090, new Vector(2317.0859f, 572.2656f, -20.9688f), 10.0f);
                GlobalObject.Remove(this, 9091, new Vector(2317.0859f, 572.2656f, -20.9688f), 10.0f);
                GlobalObject.Remove(this, 13483, new Vector(2113.5781f, -96.7344f, 0.9844f), 0.25f);
                GlobalObject.Remove(this, 12990, new Vector(2113.5781f, -96.7344f, 0.9844f), 0.25f);
                GlobalObject.Remove(this, 935, new Vector(2119.8203f, -84.4063f, -0.0703f), 0.25f);
                GlobalObject.Remove(this, 1369, new Vector(2104.0156f, -105.2656f, 1.7031f), 0.25f);
                GlobalObject.Remove(this, 935, new Vector(2122.3750f, -83.3828f, 0.4609f), 0.25f);
                GlobalObject.Remove(this, 935, new Vector(2119.5313f, -82.8906f, -0.1641f), 0.25f);
                GlobalObject.Remove(this, 935, new Vector(2120.5156f, -79.0859f, 0.2188f), 0.25f);
                GlobalObject.Remove(this, 935, new Vector(2119.4688f, -69.7344f, 0.2266f), 0.25f);
                GlobalObject.Remove(this, 935, new Vector(2119.4922f, -73.6172f, 0.1250f), 0.25f);
                GlobalObject.Remove(this, 935, new Vector(2117.8438f, -67.8359f, 0.1328f), 0.25f);

                PVars["BuildingsRemoved"] = 1;
            }

            base.OnConnected(e);
        }

        public override void OnSpawned(PlayerEventArgs e)
        {
            if (LastDeathTick != 0 && Native.GetTickCount() - LastDeathTick < (Config.RespawnTime*1000))
            {
                SendClientMessage(0xFFAAEEEE, "Waiting to respawn....");
                ToggleSpectating(true);

                // If the last killer id is valid, we should try setting it now to avoid any camera lag switching to spectate.
                if (_lastKiller == null || !_lastKiller.IsAlive) return;

                GoSpectatePlayer(_lastKiller);
                _spectateState = SpectateState.Player;

                return;
            }

            GameText(
                Team == GameMode.GreenTeam
                    ? "Defend the ~g~GREEN ~w~team's ~y~Reefer~n~~w~Capture the ~b~BLUE ~w~team's ~y~Reefer"
                    : "Defend the ~b~BLUE ~w~team's ~y~Reefer~n~~w~Capture the ~g~GREEN ~w~team's ~y~Reefer",
                6000, 5);

            Color = Team.Color;
            Health = 100;
            Armour = 100;

            _spectateState = SpectateState.None;
            _spectatingMode = SpectatingMode.None;

            base.OnSpawned(e);
        }

        private void GoSpectatePlayer(GtaPlayer player)
        {
            switch (State)
            {
                case PlayerState.OnFoot:
                    if (_spectatingMode == SpectatingMode.Player) return;
                    SpectatePlayer(player);
                    _spectatingMode = SpectatingMode.Player;
                    break;
                case PlayerState.Passenger:
                case PlayerState.Driving:
                    if (_spectatingMode == SpectatingMode.Vehicle) return;
                    SpectateVehicle(player.Vehicle);
                    _spectatingMode = SpectatingMode.Vehicle;
                    break;
            }
        }

        public override void OnDeath(PlayerDeathEventArgs e)
        {
            var killer = e.Killer as Player;
            SendDeathMessageToAll(killer, this, e.DeathReason);

            if (killer != null && Team != killer.Team)
                killer.Score++;

            LastDeathTick = Native.GetTickCount();
            _lastKiller = killer;

            base.OnDeath(e);
        }

        public override void OnUpdate(PlayerEventArgs e)
        {
            if (IsNPC) return;

            if (State == PlayerState.Spectating)
            {
                // Allow respawn after an arbitrary time has passed
                if (LastDeathTick == 0 || Native.GetTickCount() - LastDeathTick > Config.RespawnTime*1000)
                {
                    ToggleSpectating(false);
                    base.OnUpdate(e);
                    return;
                }

                // Make sure the killer player is still active in the world
                if (_lastKiller.IsConnected && _lastKiller.IsAlive)
                {
                    GoSpectatePlayer(_lastKiller);
                    _spectatingMode = SpectatingMode.Player;
                }
                else if (_spectateState != SpectateState.Fixed)
                {
                    // Else switch to the fixed position camera
                    CameraPosition = Team.FixedSpectatePosition;
                    SetCameraLookAt(Team.FixedSpectateLookAtPosition);

                    _spectateState = SpectateState.Fixed;
                }
                base.OnUpdate(e);
                return;
            }

            base.OnUpdate(e);
        }
    }
}