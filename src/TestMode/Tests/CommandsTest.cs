// SampSharp
// Copyright 2015 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Linq;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class CommandsTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            CommandGroup.Register("tools", "t", CommandGroup.Register("test", "t"));
            CommandGroup.Register("vehicle", "v");

            DetectedCommand cmd = Command.GetAll<DetectedCommand>().FirstOrDefault(c => c.Name == "console");
            Console.WriteLine("Command paths: {0}", string.Join(", ", cmd.CommandPaths));
        }

        [Command("console", Alias = "c", Shortcut = "1", PermissionCheckMethod = "TestCommandPermission")]
        [CommandGroup("tools")]
        [Text("text")]
        public static void TestCommand(GtaPlayer player, string word, int num, string text)
        {
            Console.WriteLine("Player: {0}, Word: {1}, Rest: {2}, Num: {3}", player, word, text, num);
            Console.WriteLine("Text written to console...");
            player.SendClientMessage(Color.Green, "Text written to console!");

            player.SendClientMessage(Color.Green, "Formattest {0} -- {1} ,, {2}", 123, "xyz", "::DD");
        }

        public static bool TestCommandPermission(GtaPlayer player)
        {
            return player.IsAdmin;
        }

        [Command("list", Alias = "l")]
        [CommandGroup("vehicle")]
        public static void VehicleListCommand(GtaPlayer player)
        {
            player.SendClientMessage(Color.Green, "Available vehicles:");
            player.SendClientMessage(Color.GreenYellow, string.Join(", ", typeof (VehicleModelType).GetEnumNames()));
        }

        [Command("commands")]
        public static void CommandsCommand(GtaPlayer player)
        {
            player.SendClientMessage(Color.Green, "Commands:");
            foreach (
                DetectedCommand cmd in
                    Command.GetAll<DetectedCommand>()
                        .Where(c => c.HasPlayerPermissionForCommand(player))
                        .OrderBy( /* category??? */c => c.CommandPath))
            {
                player.SendClientMessage(Color.White,
                    "/{0}: I could add an Attribute in my gamemode with an help message and/or color", cmd.CommandPath);
            }
        }

        [Command("spawn", Alias = "s", Shortcut = "v")]
        [CommandGroup("vehicle")]
        public static void VehicleCommand(GtaPlayer player, VehicleModelType model)
        {
            player.SendClientMessage(Color.GreenYellow, "You have spawned a {0}", model);
            Console.WriteLine("Spawning a {0} {2} for {1}", model, player, (int) model);
            GtaVehicle vehicle = GtaVehicle.Create(model, player.Position + new Vector(0, 0, 0.5), player.Rotation.Z, -1,
                -1);
            player.PutInVehicle(vehicle);
        }

        [Command("tell")]
        [Text("message")]
        public static void TellCommand(GtaPlayer player, GtaPlayer to, string message)
        {
            to.SendClientMessage(Color.Green, "{0} tells you: {1}", player.Name, message);
        }

        [Command("put")]
        [Integer("seat")]
        public static void PutCommand(GtaPlayer player, int vehicleid, int seat = 0)
        {
            GtaVehicle v = GtaVehicle.Find(vehicleid);
            if (v == null)
            {
                player.SendClientMessage(Color.Red, "This vehicle does not exist!");
                return;
            }
            player.PutInVehicle(v, seat);
        }

        [Command("position")]
        public static void PositionCommand(GtaPlayer player)
        {
            player.SendClientMessage(Color.Green, "Position: {0}", player.Position);
        }

        [Command("teleport", Alias = "tp")]
        public static void TpCommand(GtaPlayer player, int x, int y, int z = 4)
        {
            player.Position = new Vector(x, y, z);
            Console.WriteLine("Teleporting {0} to {1}, {2}, {3}", player, x, y, z);
        }

        [Command("wor")]
        public static void World(GtaPlayer player, int world)
        {
            player.VirtualWorld = world;
        }

        [Command("int")]
        public static void Interior(GtaPlayer player, int interior)
        {
            player.Interior = interior;
        }
    }
}