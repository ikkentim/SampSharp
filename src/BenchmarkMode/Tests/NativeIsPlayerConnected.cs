using System;
using SampSharp.GameMode.Natives;

namespace BenchmarkMode.Tests
{
    public class NativeIsPlayerConnected : ITest
    {
        public void Run(GameMode gameMode)
        {
            bool v = Native.IsPlayerConnected(0);
        }
    }
}
