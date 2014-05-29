using System;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class DisposureTest  : ITest
    {
        public void Start(GameMode gameMode)
        {
            var playercount = Player.All.Count;
            var success = true;

            Player player = Player.Create(499);

            if (Player.All.Count - 1 != playercount)
            {
                Console.WriteLine("DisposureTest: Adding didn't add player to pool.");
                success = false;
            }
            player.Dispose();

            if (Player.All.Count != playercount)
            {
                Console.WriteLine("DisposureTest: Disposing didn't remove player from pool.");
                success = false;
            }
            try
            {
                player.SetChatBubble("Test!", Color.Yellow, 100, 10);

                Console.WriteLine("DisposureTest: Passed SetChatBubble.");
                success = false;
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("DisposureTest: Exception thrown.");
            }
            
            Console.WriteLine("DisposureTest successful: {0}", success);
        }
    }
}
