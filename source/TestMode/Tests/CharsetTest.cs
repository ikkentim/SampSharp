using System;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    class CharsetTest : ITest
    {
        public void Start(GameMode gameMode)
        {

        }

        [Command("charset")]
        public static void CharsetCommand(Player player)
        {
            player.SendClientMessage(Color.Teal, "this is a test: \u00D6");
            player.SendClientMessage(Color.Teal, "this is a test: Ä ä Ö ö Ü ü ß ...");

            Console.WriteLine("this is a test: \u00D6");   
            Console.WriteLine("this is a test: Ä ä Ö ö Ü ü ß ...");   
        }
    }
}
