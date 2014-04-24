using System.Linq;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;

namespace RiverShell.World
{
    public class RPlayer : Player
    {
        private int _lastDeathTick;
        private int _lastResupplyTime;
        private RPlayer _lastKiller;
        private SpectatingMode _spectatingMode;
        private SpectateState _spectateState;
        public RPlayer(int id) : base(id)
        {

        }

        public new Team Team
        {
            get { return Team.Find(base.Team); }
            set { base.Team = value.Id; }
        }

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
                    GameText("~g~GREEN ~w~TEAM", 1000, 5);
                    break;
                case 2:
                case 3:
                    Team = GameMode.BlueTeam;
                    GameText("~b~BLUE ~w~TEAM", 1000, 5);
                    break;
            }

            base.OnRequestClass(e);
        }

        public void SetToColor(bool isObjective)
        {
            if (isObjective)
            {
                Color = 0xE2C063FF;
                return;
            }

            Color = Team.Color;
        }

        public override void OnStateChanged(PlayerStateEventArgs e)
        {
            switch (e.NewState)
            {
                case PlayerState.Driving:
                    var vehicle = Vehicle;
                    var team = Team;
                    if (team == GameMode.GreenTeam && vehicle == GameMode.GreenTeam.Vehicle)
                    {
                        // It's the objective vehicle
                        SetToColor(true);
                        GameText("~w~Take the ~y~boat ~w~back to the ~r~spawn!", 3000, 5);
                        SetCheckpoint(new Vector(2135.7368f, -179.8811f, -0.5323f), 10.0f);
                        GameMode.GreenTeam.PlayerWithVehicle = this;
                    }
                    if (team == GameMode.BlueTeam && vehicle == GameMode.BlueTeam.Vehicle)
                    {
                        // It's the objective vehicle
                        SetToColor(true);
                        GameText("~w~Take the ~y~boat ~w~back to the ~r~spawn!", 3000, 5);
                        SetCheckpoint(new Vector(2329.4226f, 532.7426f, 0.5862f), 10.0f);
                        GameMode.BlueTeam.PlayerWithVehicle = this;
                    }
                    break;
                case PlayerState.OnFoot:
                    if (this == GameMode.GreenTeam.PlayerWithVehicle)
                    {
                        GameMode.GreenTeam.PlayerWithVehicle = null;
                        SetToColor(false);
                        DisableCheckpoint();
                    }
                    if (this == GameMode.BlueTeam.PlayerWithVehicle)
                    {
                        GameMode.BlueTeam.PlayerWithVehicle = null;
                        SetToColor(false);
                        DisableCheckpoint();
                    }
                    break;
            }

            base.OnStateChanged(e);
        }

        public override void OnConnected(PlayerEventArgs e)
        {
            Color = 0x888888FF;

            GameText("~r~SA-MP: ~w~Rivershell", 2000, 5);

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
            if (_lastDeathTick != 0 && Native.GetTickCount() - _lastDeathTick < (Config.RespawnTime*1000))
            {
                SendClientMessage(0xFFAAEEEE, "Waiting to respawn....");
                ToggleSpectating(true);

                // If the last killer id is valid, we should try setting it now to avoid any camera lag switching to spectate.
                if (_lastKiller == null || !_lastKiller.IsConnected ||
                    !new[] {PlayerState.OnFoot, PlayerState.Driving, PlayerState.Passenger}.Contains(
                        _lastKiller.PlayerState)) return;

                GoSpectatePlayer(_lastKiller);
                _spectateState = SpectateState.Player;

                return;
            }

            SetToColor(false);

            GameText(
                Team == GameMode.GreenTeam
                    ? "Defend the ~g~GREEN ~w~team's ~y~Reefer~n~~w~Capture the ~b~BLUE ~w~team's ~y~Reefer"
                    : "Defend the ~b~BLUE ~w~team's ~y~Reefer~n~~w~Capture the ~g~GREEN ~w~team's ~y~Reefer",
                6000, 5);


            Health = 100;
            Armour = 100;
            SetWorldBounds(2500.0f, 1850.0f, 631.2963f, -454.9898f);

            _spectateState = SpectateState.None;
            _spectatingMode = SpectatingMode.None;

            base.OnSpawned(e);
        }

        private void GoSpectatePlayer(Player player)
        {
            var state = PlayerState;

            switch (state)
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

        public override void OnEnterCheckpoint(PlayerEventArgs e)
        {
            var vehicle = Vehicle;

            if (GameMode.ObjectiveReached)
                return;

            if (vehicle == GameMode.GreenTeam.Vehicle && Team == GameMode.GreenTeam)
            {
                // Green OBJECTIVE REACHED.
                GameMode.GreenTeam.TimesCaptured++;
                Score += 5;

                if (GameMode.GreenTeam.TimesCaptured == Config.CapturesToWin)
                {
                    GameTextForAll("~g~GREEN ~w~team wins!", 3000, 5);
                    GameMode.ObjectiveReached = true;
                    foreach (var p in All)
                        p.PlaySound(1185);

                    var exitTimer = new Timer(6000, false);
                    exitTimer.Tick += (sender, args) =>
                    {
                        foreach (var p in All)
                            p.PlaySound(1186);
                        Native.GameModeExit();
                    };
                }
                else
                {
                    GameTextForAll("~g~GREEN ~w~team captured the ~y~boat!", 3000, 5);
                    vehicle.Respawn();
                }
            }
            else if (vehicle == GameMode.BlueTeam.Vehicle && Team == GameMode.BlueTeam)
            {
                // Blue OBJECTIVE REACHED.
                GameMode.BlueTeam.TimesCaptured++;
                Score += 5;

                if (GameMode.BlueTeam.TimesCaptured == Config.CapturesToWin)
                {
                    GameTextForAll("~b~BLUE ~w~team wins!", 3000, 5);
                    GameMode.ObjectiveReached = true;
                    foreach (var p in All)
                        p.PlaySound(1185);

                    var exitTimer = new Timer(6000, false);
                    exitTimer.Tick += (sender, args) =>
                    {
                        foreach (var p in All)
                            p.PlaySound(1186);
                        Native.GameModeExit();
                    };
                }
                else
                {
                    GameTextForAll("~b~BLUE ~w~team captured the ~y~boat!", 3000, 5);
                    vehicle.Respawn();
                }
            }

            base.OnEnterCheckpoint(e);
        }

        public override void OnDeath(PlayerDeathEventArgs e)
        {
            var killer = e.Killer as RPlayer;
            SendDeathMessage(killer, e.DeathReason);

            if (killer != null && Team != killer.Team)
                killer.Score++;

            _lastDeathTick = Native.GetTickCount();
            _lastKiller = killer;

            base.OnDeath(e);
        }

        public override void OnUpdate(PlayerEventArgs e)
        {
            if (IsNPC) return;

            if (PlayerState == PlayerState.Spectating)
            {
                if (_lastDeathTick == 0)
                {
                    ToggleSpectating(false);
                    base.OnUpdate(e);
                    return;
                }
                // Allow respawn after an arbitrary time has passed
                if (Native.GetTickCount() - _lastDeathTick > Config.RespawnTime*1000)
                {
                    ToggleSpectating(false);
                    base.OnUpdate(e);
                    return;
                }

                // Make sure the killer player is still active in the world
                var state = _lastKiller.PlayerState;
                if (_lastKiller.IsConnected && new[]{PlayerState.OnFoot,PlayerState.Driving, PlayerState.Passenger}.Contains(state))
                {
                    GoSpectatePlayer(_lastKiller);
                    _spectatingMode=SpectatingMode.Player;
                }
                else
                {
                    // Else switch to the fixed position camera
                    if (_spectateState != SpectateState.Fixed)
                    {
                        if (Team == GameMode.GreenTeam)
                        {
                            CameraPosition = new Vector(2221.5820, -273.9985, 61.7806);
                            SetCameraLookAt(new Vector(2220.9978, -273.1861, 61.4606));
                        }
                        else
                        {
                            CameraPosition = new Vector(2274.8467, 591.3257, 30.1311);
                            SetCameraLookAt(new Vector(2275.0503, 590.3463, 29.9460));
                        }
                        _spectateState = SpectateState.Fixed;
                    }
                }
                base.OnUpdate(e);
                return;
            }

            if (PlayerState == PlayerState.OnFoot)
            {
                if (IsInRangeOfPoint(2.5f, new Vector(2140.83f, -235.13f, 7.13f)) ||
                    IsInRangeOfPoint(2.5f, new Vector(2318.73f, 590.96f, 6.75f)))
                {
                    if (_lastResupplyTime == 0 || (Native.GetTickCount() - _lastResupplyTime) > 30000)
                    {
                        _lastResupplyTime= Native.GetTickCount();
                        ResetWeapons();
                        GiveWeapon((Weapon)31, 100);
                        GiveWeapon((Weapon)29, 200);
                        GiveWeapon((Weapon)34, 10);

                        Health = 100.0f;
                        Armour = 100.0f;

                        GameText("Resupplied", 2000, 5);
                        PlaySound(1150);
                    }
                }
            }
            base.OnUpdate(e);
        }
    }
}
