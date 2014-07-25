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
        private class A
        {
        }

        private class B : A
        {
        }

        public void Start(GameMode gameMode)
        {
            CommandGroup.Register("tools", "t", CommandGroup.Register("test", "t"));

            var cmd = Command.GetAll<DetectedCommand>().FirstOrDefault(c => c.Name == "console");
            Console.WriteLine("Command paths: {0}", string.Join(", ", cmd.CommandPaths));
        }

        [Command("console", Alias = "c", Shortcut = "1", PermissionCheckMethod = "TestCommandPermission")]
        [CommandGroup("tools")]
        [Text("text")]
        public static bool TestCommand(Player player, string word, int num, string text)
        {
            Console.WriteLine("Player: {0}, Word: {1}, Rest: {2}, Num: {3}", player, word, text, num);
            Console.WriteLine("Text written to console...");
            player.SendClientMessage(Color.Green, "Text written to console!");

            player.SendClientMessage(Color.Green, "Formattest {0} -- {1} ,, {2}", 123, "xyz", "::DD");
            return true;
        }

        public static bool TestCommandPermission(Player player)
        {
            return player.IsAdmin;
        }

        [Command("vehicle", Alias = "v")]
        public static bool VehicleCommand(Player player, VehicleModelType model)
        {
            var vehicle = Vehicle.Create(model, player.Position + new Vector(0, 0, 0.5), player.Rotation.Z, -1, -1);
            player.PutInVehicle(vehicle);
            return true;
        }

        [Command("tell")]
        [Text("message")]
        public static bool TellCommand(Player player, Player to, string message)
        {
            to.SendClientMessage(Color.Green, "{0} tells you: {1}", player.Name, message);
            return true;
        }

        [Command("put")]
        [Integer("seat", Optional = true)]
        public static bool PutCommand(Player player, int vehicleid, int seat=0)
        {
            var v = Vehicle.Find(vehicleid);
            if (v == null)
            {
                player.SendClientMessage(Color.Red, "This vehicle does not exist!");
                return false;
            }
            player.PutInVehicle(v, seat);
            return true;
        }

        [Command("position")]
        public static bool PositionCommand(Player player)
        {
            player.SendClientMessage(Color.Green, "Position: {0}", player.Position);

            return true;
        }

        [Command("teleport", Alias = "tp")]
        [Integer("z",  Optional = true, DefaultValue = 4)]
        public static bool TpCommand(Player player, int x, int y, int z)
        {
            player.Position = new Vector(x, y, z);
            Console.WriteLine("Teleporting {0} to {1}, {2}, {3}", player, x, y, z);
            return true;
        }

        [Command("wor")]
        public static bool World(Player player, int world)
        {
            player.VirtualWorld = world;
            return true;
        }

        [Command("int")]
        public static bool Interior(Player player, int interior)
        {
            player.Interior = interior;
            return true;
        }
    }
}