using System;
using SampSharp.Streamer;
using SampSharp.Streamer.Natives;

namespace TestMode.Tests
{
    public class StreamerTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            Console.WriteLine("StreamerTest started...");
            Console.WriteLine("Streamer tick rate: {0}", StreamerNative.GetTickRate());
        }
    }
}
