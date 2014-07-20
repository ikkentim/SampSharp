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

            var cmd = Command.GetAll<DetectedCommand>().FirstOrDefault(c => c.Name == "console");
            Console.WriteLine("Command paths: {0}", string.Join(", ", cmd.CommandPaths));

            Console.WriteLine("[RESULT] {0}",
                gameMode.OnPlayerCommandText(Player.Create(0).Id, "/ 1 word 4 and more!")
                    ? "command executed successfully"
                    : "command failed to execute");

            Console.WriteLine("[RESULT] {0}",
                gameMode.OnPlayerCommandText(Player.Create(0).Id, "/ 1 word 4 and more!")
                    ? "command executed successfully"
                    : "command failed to execute");

        }

        [Command("console", Alias = "c", Shortcut = "1", PermissionCheckMethod = "TestCommandPermission")]
        [CommandGroup("tools")]
        [Word("word")]
        [Integer("num")]
        [Text("text")]
        public static bool TestCommand(Player player, string word, int num, string text)
        {
            Console.WriteLine("Player: {0}, Word: {1}, Rest: {2}, Num: {3}", player, word, text, num);
            Console.WriteLine("Text written to console...");
            player.SendClientMessage(Color.Green, "Text written to console!");

            player.SendClientMessage(Color.Green, "Formattest {0} -- {1} ,, {2}", 123, "xyz", "::DD");
            return true;
        }

        private static bool _perms = true;
        public static bool TestCommandPermission(Player player)
        {
            bool can = _perms;
            _perms = false;
            return can;
        }

        [Command("vehicle")]
        [Integer("modelid")]
        public static bool VehicleCommand(Player player, int modelid)
        {
            Vehicle.Create(modelid, player.Position + new Vector(0, 0, 1), player.Rotation.Z, -1, -1);
            return true;
        }

        [Command("put")]
        [Integer("vehicleid")]
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

        [Command("tp")]
        [Integer("x")]
        [Integer("y")]
        [Integer("z")]
        public static bool TpCommand(Player player, int x, int y, int z)
        {
            player.Position = new Vector(x, y, z);
            return true;
        }

        [Command("wor")]
        [Integer("world")]
        public static bool World(Player player, int world)
        {
            player.VirtualWorld = world;
            return true;
        }

        [Command("int")]
        [Integer("interior")]
        public static bool Interior(Player player, int interior)
        {
            player.Interior = interior;
            return true;
        }
    }
}