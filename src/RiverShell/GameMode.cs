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
using RiverShell.Controllers;
using RiverShell.World;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace RiverShell
{
    public class GameMode : BaseMode
    {
        public static Team BlueTeam = new Team
        {
            Id = 1,
            Color = 0x7777DDFF,
            GameTextTeamName = "~b~BLUE ~w~team",
            Target = new Vector(2329.4226f, 532.7426f, 0.5862f),
            FixedSpectatePosition = new Vector(2221.5820, -273.9985, 61.7806),
            FixedSpectateLookAtPosition = new Vector(2220.9978, -273.1861, 61.4606),
            ResupplyPosition = new Vector(2140.83f, -235.13f, 7.13f)
        };

        public static Team GreenTeam = new Team
        {
            Id = 2,
            Color = 0x77CC77FF,
            GameTextTeamName = "~g~GREEN ~w~team",
            Target = new Vector(2135.7368f, -179.8811f, -0.5323f),
            FixedSpectatePosition = new Vector(2274.8467, 591.3257, 30.1311),
            FixedSpectateLookAtPosition = new Vector(2275.0503, 590.3463, 29.9460),
            ResupplyPosition = new Vector(2318.73f, 590.96f, 6.75f)
        };

        public static bool ObjectiveReached;

        public override bool OnGameModeInit()
        {
            SetGameModeText("Rivershell");
            ShowPlayerMarkers(0);
            ShowNameTags(true);
            SetWorldTime(17);
            SetWeather(11);
            UsePlayerPedAnims();
            EnableVehicleFriendlyFire();
            SetNameTagDrawDistance(110.0f);
            DisableInteriorEnterExits();

            // Green classes
            AddPlayerClass(162, new Vector(2117.0129, -224.4389, 8.15), 0, Weapon.M4, 100, Weapon.MP5, 200,
                Weapon.Sniper, 10);
            AddPlayerClass(157, new Vector(2148.6606, -224.3336, 8.15), 347.1396f, Weapon.M4, 100, Weapon.MP5, 200,
                Weapon.Sniper, 10);

            // Blue classes
            AddPlayerClass(154, new Vector(2352.9873, 580.3051, 7.7813), 178.1424f, Weapon.M4, 100, Weapon.MP5, 200,
                Weapon.Sniper, 10);
            AddPlayerClass(138, new Vector(2281.1504, 567.6248, 7.7813), 163.7289f, Weapon.M4, 100, Weapon.MP5, 200,
                Weapon.Sniper, 10);

            // Objective vehicles
            BlueTeam.TargetVehicle = GtaVehicle.Create(453, new Vector(2184.7156, -188.5401, -0.0239), 0.0000f, 114, 1,
                100);
            // gr reefer
            GreenTeam.TargetVehicle = GtaVehicle.Create(453, new Vector(2380.0542, 535.2582, -0.0272), 178.4999f, 79, 7,
                100); // bl reefer

            // Green Dhingys
            GtaVehicle.Create(473, new Vector(2096.0833, -168.7771, 0.3528), 4.5000f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2103.2510, -168.7598, 0.3528), 3.1800f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2099.4966, -168.8216, 0.3528), 2.8200f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2107.1143, -168.7798, 0.3528), 3.1800f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2111.0674, -168.7609, 0.3528), 3.1800f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2114.8933, -168.7898, 0.3528), 3.1800f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2167.2217, -169.0570, 0.3528), 3.1800f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2170.4294, -168.9724, 0.3528), 3.1800f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2173.7952, -168.9217, 0.3528), 3.1800f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2177.0386, -168.9767, 0.3528), 3.1800f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2161.5786, -191.9538, 0.3528), 89.1000f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2161.6394, -187.2925, 0.3528), 89.1000f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2161.7610, -183.0225, 0.3528), 89.1000f, 114, 1, 100);
            GtaVehicle.Create(473, new Vector(2162.0283, -178.5106, 0.3528), 89.1000f, 114, 1, 100);

            // Green Mavericks
            GtaVehicle.Create(487, new Vector(2088.7905, -227.9593, 8.3662), 0.0000f, 114, 1, 100);
            GtaVehicle.Create(487, new Vector(2204.5991, -225.3703, 8.2400), 0.0000f, 114, 1, 100);

            // Blue Dhingys
            GtaVehicle.Create(473, new Vector(2370.3198, 518.3151, 0.1240), 180.3600f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2362.6484, 518.3978, 0.0598), 180.3600f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2358.6550, 518.2167, 0.2730), 180.3600f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2366.5544, 518.2680, 0.1080), 180.3600f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2354.6321, 518.1960, 0.3597), 180.3600f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2350.7449, 518.1929, 0.3597), 180.3600f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2298.8977, 518.4470, 0.3597), 180.3600f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2295.6118, 518.3963, 0.3597), 180.3600f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2292.3237, 518.4249, 0.3597), 180.3600f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2289.0901, 518.4363, 0.3597), 180.3600f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2304.8232, 539.7859, 0.3597), 270.5998f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2304.6936, 535.0454, 0.3597), 270.5998f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2304.8245, 530.3308, 0.3597), 270.5998f, 79, 7, 100);
            GtaVehicle.Create(473, new Vector(2304.8142, 525.7471, 0.3597), 270.5998f, 79, 7, 100);

            // Blue Mavericks
            GtaVehicle.Create(487, new Vector(2260.2637, 578.5220, 8.1223), 182.3401f, 79, 7, 100);
            GtaVehicle.Create(487, new Vector(2379.9792, 580.0323, 8.0178), 177.9601f, 79, 7, 100);

            // Green Base Section
            new GlobalObject(9090, new Vector(2148.64, -222.88, -20.60), new Vector(0.00, 0.00, 179.70));
            // Green resupply hut
            new GlobalObject(12991, new Vector(2140.83, -235.13, 7.13), new Vector(0.00, 0.00, -89.94));

            // Blue Base Section
            new GlobalObject(9090, new Vector(2317.09, 572.27, -20.97), new Vector(0.00, 0.00, 0.00));
            // Blue resupply hut
            new GlobalObject(12991, new Vector(2318.73, 590.96, 6.75), new Vector(0.00, 0.00, 89.88));

            // General mapping
            new GlobalObject(12991, new Vector(2140.83, -235.13, 7.13), new Vector(0.00, 0.00, -89.94));
            new GlobalObject(19300, new Vector(2137.33, -237.17, 46.61), new Vector(0.00, 0.00, 180.00));
            new GlobalObject(12991, new Vector(2318.73, 590.96, 6.75), new Vector(0.00, 0.00, 89.88));
            new GlobalObject(19300, new Vector(2325.41, 587.93, 47.37), new Vector(0.00, 0.00, 180.00));
            new GlobalObject(12991, new Vector(2140.83, -235.13, 7.13), new Vector(0.00, 0.00, -89.94));
            new GlobalObject(12991, new Vector(2318.73, 590.96, 6.75), new Vector(0.00, 0.00, 89.88));
            new GlobalObject(12991, new Vector(2140.83, -235.13, 7.13), new Vector(0.00, 0.00, -89.94));
            new GlobalObject(12991, new Vector(2318.73, 590.96, 6.75), new Vector(0.00, 0.00, 89.88));
            new GlobalObject(18228, new Vector(1887.93, -59.78, -2.14), new Vector(0.00, 0.00, 20.34));
            new GlobalObject(17031, new Vector(1990.19, 541.37, -22.32), new Vector(0.00, 0.00, 0.00));
            new GlobalObject(18227, new Vector(2000.82, 494.15, -7.53), new Vector(11.70, -25.74, 154.38));
            new GlobalObject(17031, new Vector(1992.35, 539.80, -2.97), new Vector(9.12, 30.66, 0.00));
            new GlobalObject(17031, new Vector(1991.88, 483.77, -0.66), new Vector(-2.94, -5.22, 12.78));
            new GlobalObject(17029, new Vector(2070.57, -235.87, -6.05), new Vector(-7.20, 4.08, 114.30));
            new GlobalObject(17029, new Vector(2056.50, -228.77, -19.67), new Vector(14.16, 19.68, 106.56));
            new GlobalObject(17029, new Vector(2074.00, -205.33, -18.60), new Vector(16.02, 60.60, 118.86));
            new GlobalObject(17029, new Vector(2230.39, -242.59, -11.41), new Vector(5.94, 7.56, 471.24));
            new GlobalObject(17029, new Vector(2252.53, -213.17, -20.81), new Vector(18.90, -6.30, -202.38));
            new GlobalObject(17029, new Vector(2233.04, -234.08, -19.00), new Vector(21.84, -8.88, -252.06));
            new GlobalObject(17027, new Vector(2235.05, -201.49, -11.90), new Vector(-11.94, -4.08, 136.32));
            new GlobalObject(17029, new Vector(2226.11, -237.07, -2.45), new Vector(8.46, 2.10, 471.24));
            new GlobalObject(4368, new Vector(2433.79, 446.26, 4.67), new Vector(-8.04, -9.30, 61.02));
            new GlobalObject(4368, new Vector(2031.23, 489.92, -13.20), new Vector(-8.04, -9.30, -108.18));
            new GlobalObject(17031, new Vector(2458.36, 551.10, -6.95), new Vector(0.00, 0.00, 0.00));
            new GlobalObject(17031, new Vector(2465.37, 511.35, -7.70), new Vector(0.00, 0.00, 0.00));
            new GlobalObject(17031, new Vector(2474.80, 457.71, -5.17), new Vector(0.00, 0.00, 172.74));
            new GlobalObject(17031, new Vector(2466.03, 426.28, -5.17), new Vector(0.00, 0.00, 0.00));
            new GlobalObject(791, new Vector(2310.45, -229.38, 7.41), new Vector(0.00, 0.00, 0.00));
            new GlobalObject(791, new Vector(2294.00, -180.15, 7.41), new Vector(0.00, 0.00, 60.90));
            new GlobalObject(791, new Vector(2017.50, -305.30, 7.29), new Vector(0.00, 0.00, 60.90));
            new GlobalObject(791, new Vector(2106.45, -279.86, 20.05), new Vector(0.00, 0.00, 60.90));
            new GlobalObject(706, new Vector(2159.13, -263.71, 19.22), new Vector(356.86, 0.00, -17.18));
            new GlobalObject(706, new Vector(2055.75, -291.53, 13.98), new Vector(356.86, 0.00, -66.50));
            new GlobalObject(791, new Vector(1932.65, -315.88, 6.77), new Vector(0.00, 0.00, -35.76));
            new GlobalObject(790, new Vector(2429.40, 575.79, 10.42), new Vector(0.00, 0.00, 3.14));
            new GlobalObject(790, new Vector(2403.40, 581.56, 10.42), new Vector(0.00, 0.00, 29.48));
            new GlobalObject(791, new Vector(2083.44, 365.48, 13.19), new Vector(356.86, 0.00, -1.95));
            new GlobalObject(791, new Vector(2040.15, 406.02, 13.33), new Vector(356.86, 0.00, -1.95));
            new GlobalObject(791, new Vector(1995.36, 588.10, 7.50), new Vector(356.86, 0.00, -1.95));
            new GlobalObject(791, new Vector(2126.11, 595.15, 5.99), new Vector(0.00, 0.00, -35.82));
            new GlobalObject(791, new Vector(2188.35, 588.90, 6.04), new Vector(0.00, 0.00, 0.00));
            new GlobalObject(791, new Vector(2068.56, 595.58, 5.99), new Vector(0.00, 0.00, 52.62));
            new GlobalObject(698, new Vector(2385.32, 606.16, 9.79), new Vector(0.00, 0.00, 34.62));
            new GlobalObject(698, new Vector(2309.29, 606.92, 9.79), new Vector(0.00, 0.00, -54.54));
            new GlobalObject(790, new Vector(2347.14, 619.77, 9.94), new Vector(0.00, 0.00, 3.14));
            new GlobalObject(698, new Vector(2255.28, 606.94, 9.79), new Vector(0.00, 0.00, -92.76));
            new GlobalObject(4298, new Vector(2121.37, 544.12, -5.74), new Vector(-10.86, 6.66, 3.90));
            new GlobalObject(4368, new Vector(2273.18, 475.02, -15.30), new Vector(4.80, 8.10, 266.34));
            new GlobalObject(18227, new Vector(2232.38, 451.61, -30.71), new Vector(-18.54, -6.06, 154.38));
            new GlobalObject(17031, new Vector(2228.15, 518.87, -16.51), new Vector(13.14, -1.32, -20.10));
            new GlobalObject(17031, new Vector(2230.42, 558.52, -18.38), new Vector(-2.94, -5.22, 12.78));
            new GlobalObject(17031, new Vector(2228.97, 573.62, 5.17), new Vector(17.94, -15.60, -4.08));
            new GlobalObject(17029, new Vector(2116.67, -87.71, -2.31), new Vector(5.94, 7.56, 215.22));
            new GlobalObject(17029, new Vector(2078.66, -83.87, -27.30), new Vector(13.02, -53.94, -0.30));
            new GlobalObject(17029, new Vector(2044.80, -36.91, -9.26), new Vector(-13.74, 27.90, 293.76));
            new GlobalObject(17029, new Vector(2242.41, 426.16, -15.43), new Vector(-21.54, 22.26, 154.80));
            new GlobalObject(17029, new Vector(2220.06, 450.07, -34.78), new Vector(-1.32, 10.20, -45.84));
            new GlobalObject(17029, new Vector(2252.49, 439.08, -19.47), new Vector(-41.40, 20.16, 331.86));
            new GlobalObject(17031, new Vector(2241.41, 431.93, -5.62), new Vector(-2.22, -4.80, 53.64));
            new GlobalObject(17029, new Vector(2141.10, -81.30, -2.41), new Vector(5.94, 7.56, 39.54));
            new GlobalObject(17031, new Vector(2277.07, 399.31, -1.65), new Vector(-2.22, -4.80, -121.74));
            new GlobalObject(17026, new Vector(2072.75, -224.40, -5.25), new Vector(0.00, 0.00, -41.22));

            // Ramps
            new GlobalObject(1632, new Vector(2131.97, 110.24, 0.00), new Vector(0.00, 0.00, 153.72));
            new GlobalObject(1632, new Vector(2124.59, 113.69, 0.00), new Vector(0.00, 0.00, 157.56));
            new GlobalObject(1632, new Vector(2116.31, 116.44, 0.00), new Vector(0.00, 0.00, 160.08));
            new GlobalObject(1632, new Vector(2113.22, 108.48, 0.00), new Vector(0.00, 0.00, 340.20));
            new GlobalObject(1632, new Vector(2121.21, 105.21, 0.00), new Vector(0.00, 0.00, 340.20));
            new GlobalObject(1632, new Vector(2127.84, 102.06, 0.00), new Vector(0.00, 0.00, 334.68));
            new GlobalObject(1632, new Vector(2090.09, 40.90, 0.00), new Vector(0.00, 0.00, 348.36));
            new GlobalObject(1632, new Vector(2098.73, 39.12, 0.00), new Vector(0.00, 0.00, 348.36));
            new GlobalObject(1632, new Vector(2107.17, 37.94, 0.00), new Vector(0.00, 0.00, 348.36));
            new GlobalObject(1632, new Vector(2115.88, 36.47, 0.00), new Vector(0.00, 0.00, 348.36));
            new GlobalObject(1632, new Vector(2117.46, 45.86, 0.00), new Vector(0.00, 0.00, 529.20));
            new GlobalObject(1632, new Vector(2108.98, 46.95, 0.00), new Vector(0.00, 0.00, 529.20));
            new GlobalObject(1632, new Vector(2100.42, 48.11, 0.00), new Vector(0.00, 0.00, 526.68));
            new GlobalObject(1632, new Vector(2091.63, 50.02, 0.00), new Vector(0.00, 0.00, 526.80));

            Console.WriteLine("----------------------------------");
            Console.WriteLine("  Rivershell by Kye 2006-2013");
            Console.WriteLine("  SAMPSHARP PORT");
            Console.WriteLine("----------------------------------");

            return base.OnGameModeInit();
        }

        protected override void LoadControllers(ControllerCollection controllers)
        {
            base.LoadControllers(controllers);

            controllers.Remove<GtaPlayerController>();
            controllers.Add(new PlayerController());

            controllers.Remove<GtaVehicleController>();
            controllers.Add(new VehicleController());

            controllers.Add(new TeamController());
            controllers.Add(new ResupplyController());
            controllers.Add(new ObjectiveController());
        }
    }
}