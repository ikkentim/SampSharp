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
using SampSharp.GameMode;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode
{
    public class GameMode : BaseMode
    {
        #region Hex
        public class HexParameter : WordParameter
        {
            public HexParameter()
            {

            }

            public HexParameter(string name) : base(name)
            {

            }

            public HexParameter(string name, bool optional)
                : base(name, optional)
            {

            }

            public override bool Check(ref string command, out object output)
            {
                if (!base.Check(ref command, out output))
                    return false;

                string word = (string)output;

                int number;


                try
                {
                    number = Convert.ToInt32(word, 16);
                }
                catch (FormatException)
                {
                    output = null;
                    return false;
                }

                output = number;

                return true;
            }
        }
        #endregion

        public override bool OnGameModeInit()
        {
            new Command("console", new Parameter[] { new WordParameter("word"), new IntegerParameter("number", true), new TextParameter("rest", true) }, (Func<Player, string, int, string, bool>)TestCommand);
            
            new Command("color", new Parameter[] { new HexParameter("color"), new TextParameter("text") },
                (Func<Player, int,string,bool>)((player, color, text) =>
                {
                    Color c = color;
                    Player.SendClientMessageToAll(c, player.Name + ": " + text);
                    return true;
                }));


            AddPlayerClass(65, new Vector(), 0);
            return true;
        }

        static bool TestCommand(Player player, string word, int number, string rest)
        {
            Console.WriteLine("Player: {0}, Word: {1}, Rest: {2}, Num: {3}", player, word, rest, number);

            return true;
        }
    }
}