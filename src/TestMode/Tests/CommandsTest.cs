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
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class CommandsTest : ITest
    {
        public void Start(GameMode gameMode)
        {
        }

        [Command("console")]
        [WordParameter("word")]
        [IntegerParameter("num")]
        [TextParameter("text")]
        public static bool TestCommand(Player player, string word, int num, string text)
        {
            Console.WriteLine("Player: {0}, Word: {1}, Rest: {2}, Num: {3}", player, word, text, num);
            Console.WriteLine("Text written to console...");
            player.SendClientMessage(Color.Green, "Text written to console!");
            return true;
        }

        [Command("vehicle")]
        [IntegerParameter("modelid")]
        public static bool VehicleCommand(Player player, int modelid)
        {
            Vehicle.Create(modelid, player.Position + new Vector(0, 0, 1), player.Rotation.Z, -1, -1, -1);
            return true;
        }
    }
}