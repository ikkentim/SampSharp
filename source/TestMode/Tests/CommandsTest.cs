using System;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    #region Hex
    public class HexParameter : WordParameter
    {
        public HexParameter()
        {

        }

        public HexParameter(string name)
            : base(name)
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

    public class CommandsTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            new Command("console",
                new Parameter[]
                {new WordParameter("word"), new IntegerParameter("number", true), new TextParameter("rest", true)},
                (Func<Player, string, int, string, bool>) TestCommand);

            new Command("color", new Parameter[] {new HexParameter("color"), new TextParameter("text")},
                (Func<Player, int, string, bool>) ((player, color, text) =>
                {
                    Color c = color;
                    Player.SendClientMessageToAll(c, player.Name + ": " + text);
                    return true;
                }));

        }

        private bool TestCommand(Player player, string word, int number, string rest)
        {
            Console.WriteLine("Player: {0}, Word: {1}, Rest: {2}, Num: {3}", player, word, rest, number);

            return true;
        }
    }
}
