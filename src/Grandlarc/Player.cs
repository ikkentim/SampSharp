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

using System;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace Grandlarc
{
    public class Player : GtaPlayer
    {
        private static readonly Random random = new Random();

        public bool HasCitySelected;
        private DateTime lastSelectionTime;

        public Player(int id) : base(id)
        {
            SelectedCity = City.LosSantos;
        }

        public City SelectedCity { get; private set; }

        public override void OnConnected(PlayerEventArgs e)
        {
            base.OnConnected(e);

            Console.WriteLine(e.Player.Name + " is connected.");

            SendClientMessage("Welcome to {88AA88}G{FFFFFF}rand {88AA88}L{FFFFFF}arceny");
            GameText("~w~Grand Larceny", 4000, 4);
        }

        public override void OnSpawned(PlayerEventArgs e)
        {
            base.OnSpawned(e);

            Interior = 0;

            int randomPosition = random.Next(0, SpawnPositions.Positions[SelectedCity].Count);

            Position = SpawnPositions.Positions[SelectedCity][randomPosition].Position;
            Rotation = new Vector(SpawnPositions.Positions[SelectedCity][randomPosition].Rotation);

            SetSkillLevel(WeaponSkill.Pistol, 200);
            SetSkillLevel(WeaponSkill.PistolSilenced, 200);
            SetSkillLevel(WeaponSkill.DesertEagle, 200);
            SetSkillLevel(WeaponSkill.Shotgun, 200);
            SetSkillLevel(WeaponSkill.SawnoffShotgun, 200);
            SetSkillLevel(WeaponSkill.Spas12Shotgun, 200);
            SetSkillLevel(WeaponSkill.MicroUzi, 200);
            SetSkillLevel(WeaponSkill.MP5, 200);
            SetSkillLevel(WeaponSkill.AK47, 200);
            SetSkillLevel(WeaponSkill.M4, 200);
            SetSkillLevel(WeaponSkill.SniperRifle, 200);

            GiveWeapon(Weapon.Colt45, 100);
            ToggleClock(false);
        }

        public override void OnDeath(PlayerDeathEventArgs e)
        {
            base.OnDeath(e);

            if (e.KillerId != InvalidId && Money > 0)
            {
                e.Killer.Money += Money;
            }

            ResetMoney();
        }

        public override void OnRequestClass(PlayerRequestClassEventArgs e)
        {
            base.OnRequestClass(e);

            if (!HasCitySelected)
            {
                lastSelectionTime = DateTime.Now;
                ToggleSpectating(true);

                GameMode.HelpSpawnTextdraw.Show(this);
                PrepareSelectedCity();
                e.Success = false;
                return;
            }

            ShowCharacterSelection();
            e.Success = true;
        }

        public override void OnUpdate(PlayerEventArgs e)
        {
            base.OnUpdate(e);

            if (State == PlayerState.Spectating && !HasCitySelected)
            {
                HandleCitySelection();
            }
        }

        private void HandleCitySelection()
        {
            Keys keys;
            int upDown;
            int leftright;

            GetKeys(out keys, out upDown, out leftright);

            if (keys == Keys.Fire)
            {
                ToggleSpectating(false);
                HasCitySelected = true;

                GameMode.HelpSpawnTextdraw.Hide(this);
                GameMode.LasVenturasTextDraw.Hide(this);
                GameMode.LosSantosTextDraw.Hide(this);
                GameMode.SanFierroTextDraw.Hide(this);
                return;
            }

            DateTime now = DateTime.Now;

            if ((now - lastSelectionTime).Milliseconds < 150)
            {
                return;
            }

            lastSelectionTime = now;

            if (leftright < 0)
            {
                SelectedCity = SelectedCity.Next();
            }
            else if (leftright > 0)
            {
                SelectedCity = SelectedCity.Prev();
            }

            PrepareSelectedCity();
        }

        private void PrepareSelectedCity()
        {
            switch (SelectedCity)
            {
                case City.LosSantos:
                    Interior = 0;
                    CameraPosition = new Vector(1630.6136, -2286.0298, 110.0);
                    SetCameraLookAt(new Vector(1887.6034, -1682.1442, 47.6167));

                    GameMode.LasVenturasTextDraw.Hide(this);
                    GameMode.LosSantosTextDraw.Show(this);
                    GameMode.SanFierroTextDraw.Hide(this);
                    break;
                case City.SanFierro:
                    Interior = 0;
                    CameraPosition = new Vector(-1300.8754, 68.0546, 129.4823);
                    SetCameraLookAt(new Vector(-1817.9412, 769.3878, 132.6589));

                    GameMode.LasVenturasTextDraw.Hide(this);
                    GameMode.LosSantosTextDraw.Hide(this);
                    GameMode.SanFierroTextDraw.Show(this);
                    break;
                case City.LasVenturas:
                    Interior = 0;
                    CameraPosition = new Vector(1310.6155, 1675.9182, 110.7390);
                    SetCameraLookAt(new Vector(2285.2944, 1919.3756, 68.2275));

                    GameMode.LasVenturasTextDraw.Show(this);
                    GameMode.LosSantosTextDraw.Hide(this);
                    GameMode.SanFierroTextDraw.Hide(this);
                    break;
            }
        }

        private void ShowCharacterSelection()
        {
            switch (SelectedCity)
            {
                case City.LosSantos:
                    Interior = 11;
                    Position = new Vector(508.7362, -87.4335, 998.9609);
                    Angle = 0.0f;
                    CameraPosition = new Vector(508.7362, -83.4335, 998.9609);
                    SetCameraLookAt(new Vector(508.7362f, -87.4335f, 998.9609f));
                    break;

                case City.SanFierro:
                    Interior = 3;
                    Position = new Vector(-2673.8381, 1399.7424, 918.3516);
                    Angle = 181.0f;
                    CameraPosition = new Vector(-2673.2776, 1394.3859, 918.3516);
                    SetCameraLookAt(new Vector(-2673.8381, 1399.7424, 918.3516));
                    break;

                case City.LasVenturas:
                    Interior = 3;
                    Position = new Vector(349.0453, 193.2271, 1014.1797);
                    Angle = 286.25f;
                    CameraPosition = new Vector(352.9164, 194.5702, 1014.1875);
                    SetCameraLookAt(new Vector(349.0453, 193.2271, 1014.1797));
                    break;
            }
        }
    }
}