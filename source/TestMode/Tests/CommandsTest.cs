using System;
using System.Xml;
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
    }
}
