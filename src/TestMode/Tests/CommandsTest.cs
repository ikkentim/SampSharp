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

            gameMode.OnPlayerCommandText(new Player(999).Id, "/vehicle list");
        }

        [Command("console", Alias = "c", Shortcut = "1", PermissionCheckMethod = "TestCommandPermission")]
        [CommandGroup("tools")]
        [Text("text")]
        public static void TestCommand(Player player, string word, int num, string text)
        {
            Console.WriteLine("Player: {0}, Word: {1}, Rest: {2}, Num: {3}", player, word, text, num);
            Console.WriteLine("Text written to console...");
            player.SendClientMessage(Color.Green, "Text written to console!");

            player.SendClientMessage(Color.Green, "Formattest {0} -- {1} ,, {2}", 123, "xyz", "::DD");
        }

        public static bool TestCommandPermission(Player player)
        {
            return player.IsAdmin;
        }

        [Command("list", Alias = "l")]
        [CommandGroup("vehicle")]
        public static void VehicleListCommand(Player player)
        {
            player.SendClientMessage(Color.Green, "Available vehicles:");
            player.SendClientMessage(Color.GreenYellow, string.Join(", ", typeof (VehicleModelType).GetEnumNames()));
        }

        [Command("commands")]
        public static void CommandsCommand(Player player)
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
        public static void VehicleCommand(Player player, VehicleModelType model)
        {
            player.SendClientMessage(Color.GreenYellow, "You have spawned a {0}", model);
            Console.WriteLine("Spawning a {0} {2} for {1}", model, player, (int) model);
            Vehicle vehicle = Vehicle.Create(model, player.Position + new Vector(0, 0, 0.5), player.Rotation.Z, -1, -1);
            player.PutInVehicle(vehicle);
        }

        [Command("tell")]
        [Text("message")]
        public static void TellCommand(Player player, Player to, string message)
        {
            to.SendClientMessage(Color.Green, "{0} tells you: {1}", player.Name, message);
        }

        [Command("put")]
        [Integer("seat")]
        public static void PutCommand(Player player, int vehicleid, int seat = 0)
        {
            Vehicle v = Vehicle.Find(vehicleid);
            if (v == null)
            {
                player.SendClientMessage(Color.Red, "This vehicle does not exist!");
                return;
            }
            player.PutInVehicle(v, seat);
        }

        [Command("position")]
        public static void PositionCommand(Player player)
        {
            player.SendClientMessage(Color.Green, "Position: {0}", player.Position);
        }

        [Command("teleport", Alias = "tp")]
        public static void TpCommand(Player player, int x, int y, int z = 4)
        {
            player.Position = new Vector(x, y, z);
            Console.WriteLine("Teleporting {0} to {1}, {2}, {3}", player, x, y, z);
        }

        [Command("wor")]
        public static void World(Player player, int world)
        {
            player.VirtualWorld = world;
        }

        [Command("int")]
        public static void Interior(Player player, int interior)
        {
            player.Interior = interior;
        }
    }
}