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
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class CharsetTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            Native.Print(": \u00D6");
            Native.Print(": Ä ä Ö ö Ü ü ß ...");
        }

        [Command("charset")]
        public static bool CharsetCommand(Player player)
        {
            Player.SendClientMessageToAll(Color.Teal, "this is a test: \u00D6");
            Player.SendClientMessageToAll(Color.Teal, "this is a test: Ä ä Ö ö Ü ü ß ...");

            Console.WriteLine("this is a test: \u00D6");
            Console.WriteLine("this is a test: Ä ä Ö ö Ü ü ß ...");

            return true;
        }
    }
}