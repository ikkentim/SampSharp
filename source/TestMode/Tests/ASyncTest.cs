using System;
using System.Threading;
using System.Threading.Tasks;
using Timer = SampSharp.GameMode.SAMP.Timer;

namespace TestMode.Tests
{
    public class ASyncTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            Console.WriteLine("Starting async gm-init.");
            ASyncTestMethod();

            Console.WriteLine("Starting timer gm-init.");
            var timer = new Timer(1000, true);
            timer.Tick += (sender, args) => Console.WriteLine("Timer!");
        }

        public void ASyncTestMethod()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 60; i++)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("ASYNC");
                }
            });

        }
    }
}
