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
using System.IO;
using Grandlarc.Controllers;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace Grandlarc
{
    public class GameMode : BaseMode
    {
        public static TextDraw LosSantosTextDraw;
        public static TextDraw SanFierroTextDraw;
        public static TextDraw LasVenturasTextDraw;
        public static TextDraw HelpSpawnTextdraw;

        public override bool OnGameModeInit()
        {
            SetGameModeText("Grand Larceny");

            ShowPlayerMarkers(PlayerMarkersMode.Global);
            ShowNameTags(true);
            SetNameTagDrawDistance(40.0f);
            EnableStuntBonusForAll(false);
            DisableInteriorEnterExits();
            SetWeather(2);
            SetWorldTime(11);
            UsePlayerPedAnims();

            AddPlayerClass(1, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(2, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(3, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(4, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(5, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(6, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(8, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(42, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(65, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(86, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(47, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(48, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(49, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(50, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(51, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(52, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(53, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(54, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(55, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(56, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(57, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(58, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(68, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(69, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(70, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(71, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(72, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(73, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(75, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(76, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(78, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(79, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(80, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(81, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(82, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(83, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(84, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(85, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(87, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(88, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(89, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(91, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(92, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(93, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(95, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(96, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(97, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(98, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(99, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(119, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(149, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(208, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(273, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(289, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(281, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(282, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(283, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(284, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(285, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(286, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(287, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(288, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(289, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(265, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(266, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(267, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(268, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(269, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);
            AddPlayerClass(270, new Vector(1759.0189, -1898.1260, 13.5622), 266.4503f);

            CreateSpawnTextdraws();
            LoadVehicles();

            return base.OnGameModeInit();
        }

        private void LoadVehicles()
        {
            if (!Directory.Exists("scriptfiles/vehicles/"))
            {
                Console.WriteLine(
                    "Vehicles folder doesn't exists. Remember to copy this folder to insert vehicles in the gamemode!");
                return;
            }

            string[] files = Directory.GetFiles("scriptfiles/vehicles/");
            int totalVehicles = 0;

            foreach (string file in files)
            {
                foreach (string line in File.ReadLines(file))
                {
                    string[] tokens = line.Split(',');

                    // the sa-mp team thinks that was funny to put a ; at the end of the line *-*
                    int endOfLineDelimiter = tokens[6].IndexOf(';');

                    GtaVehicle.CreateStatic(
                        int.Parse(tokens[0]),
                        new Vector(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])),
                        float.Parse(tokens[4]),
                        int.Parse(tokens[5]),
                        int.Parse(tokens[6].Substring(0,
                            endOfLineDelimiter != -1 ? endOfLineDelimiter - 1 : tokens[6].Length)
                            )
                        );

                    ++totalVehicles;
                }
            }

            Console.WriteLine("Total vehicles from files: " + totalVehicles);
        }

        private void CreateSpawnTextdraws()
        {
            LosSantosTextDraw = new TextDraw(10.0f, 380.0f, "Los Santos");
            InitCitySelectTextDraw(LosSantosTextDraw);

            SanFierroTextDraw = new TextDraw(10.0f, 380.0f, "San Fierro");
            InitCitySelectTextDraw(SanFierroTextDraw);

            LasVenturasTextDraw = new TextDraw(10.0f, 380.0f, "Las Venturas");
            InitCitySelectTextDraw(LasVenturasTextDraw);

            HelpSpawnTextdraw = new TextDraw(10.0f, 415.0f,
                "Press ~b~~k~~GO_LEFT~ ~w~or ~b~~k~~GO_RIGHT~ ~w~to switch cities.~n~ Press ~r~~k~~PED_FIREWEAPON~ ~w~to select.")
            {
                UseBox = true,
                BoxColor = 0x222222BB,
                LetterHeight = 1.0f,
                LetterWidth = 0.3f,
                Font = TextDrawFont.Normal,
                Shadow = 0,
                Outline = 1,
                BackColor = 0x000000FF,
                ForeColor = 0xFFFFFFFF
            };
        }

        private void InitCitySelectTextDraw(TextDraw textDraw)
        {
            textDraw.UseBox = true;
            textDraw.LetterHeight = 3.0f;
            textDraw.LetterWidth = 1.25f;
            textDraw.Font = TextDrawFont.Diploma;
            textDraw.Shadow = 0;
            textDraw.Outline = 1;
            textDraw.ForeColor = 0xEEEEEEFF;
            textDraw.BackColor = 0x000000FF;
        }

        protected override void LoadControllers(ControllerCollection controllers)
        {
            base.LoadControllers(controllers);

            controllers.Remove<GtaPlayerController>();
            controllers.Add(new PlayerController());
        }

        [Command("kill")]
        public static void KillMe(GtaPlayer player)
        {
            player.Health = 0.0f;
        }

        [Command("changecity")]
        public static void ChangeCity(GtaPlayer player)
        {
            player.ForceClassSelection();
            (player as Player).HasCitySelected = false;
            player.SendClientMessage("You will change city the next time you spawn. Or use /kill.");
        }
    }
}