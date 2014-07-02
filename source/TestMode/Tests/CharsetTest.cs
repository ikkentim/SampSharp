using System;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    class CharsetTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            Native.Print(": \u00D6");
            Native.Print(": Ä ä Ö ö Ü ü ß ...");
            gameMode.PlayerConnected += (sender, args) => CharsetCommand(args.Player);
        }

        [Command("charset")]
        public static void CharsetCommand(Player player)
        {
            Player.SendClientMessageToAll(Color.Teal, "this is a test: \u00D6");
            Player.SendClientMessageToAll(Color.Teal, "this is a test: Ä ä Ö ö Ü ü ß ...");

            Console.WriteLine("this is a test: \u00D6");   
            Console.WriteLine("this is a test: Ä ä Ö ö Ü ü ß ...");   
        }
    }
}
