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
            //Proof timers and C# async can run at the same time
            //while the game is still functioning properly

            ASyncTestMethod();

            var timer = new Timer(1000, true);
            timer.Tick += (sender, args) =>
            {
                
            };

            Console.WriteLine("Started async method");
        }

        public void ASyncTestMethod()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 60; i++)
                {
                    Thread.Sleep(2000);
                }
            });

        }
    }
}
