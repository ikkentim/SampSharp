// SampSharp
// Copyright 2015 Tim Potze
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Threading;
using System.Threading.Tasks;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;
using Timer = SampSharp.GameMode.SAMP.Timer;

namespace TestMode.Tests
{
    public class ASyncTest : ITest
    {
        private Thread _main;

        public void Start(GameMode gameMode)
        {
            //Proof timers and C# async can run at the same time
            //while the game is still functioning properly

            _main = Thread.CurrentThread;

            ASyncTestMethod();
            ASyncTestMethod2();
            ASyncTestMethod3();
            ASyncTestMethod4();

            DateTime tstart = DateTime.Now;
            var timer = new Timer(new TimeSpan(0, 0, 0, 2, 500), false);
            timer.Tick +=
                (sender, args) =>
                    Console.WriteLine("Timer: Mainthread: {0} !{2}!; took {1}", _main == Thread.CurrentThread,
                        DateTime.Now - tstart, Native.IsMainThread());

            Console.WriteLine("Started async methods !{0}!", Native.IsMainThread());

            gameMode.PlayerConnected += gameMode_PlayerConnected;
        }

        private void gameMode_PlayerConnected(object sender, EventArgs e)
        {
            ASyncPlayerConnectedDelayed(sender as GtaPlayer);
        }

        public async void ASyncPlayerConnectedDelayed(GtaPlayer player)
        {
            await Task.Delay(2000);
            Console.WriteLine("in ASyncPlayerConnectedDelayed");
            player.SendClientMessage("ASync message! !{0}!", Native.IsMainThread());

            Sync.Run(() => player.SendClientMessage("Sync message! !{0}!", Native.IsMainThread()));
        }

        public async void ASyncTestMethod2()
        {
            await Task.Delay(2000);
            Console.WriteLine("ASync2: Mainthread: {0} !{1}!", Thread.CurrentThread == _main, Native.IsMainThread());

            Sync.Run(() => Console.WriteLine("Sync2: Mainthread: {0} !{1}!", Thread.CurrentThread == _main, Native.IsMainThread()));
        }

        public void ASyncTestMethod()
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);

                Console.WriteLine("ASync: Mainthread: {0} !{1}!", Thread.CurrentThread == _main, Native.IsMainThread());

                Sync.Run(() => Console.WriteLine("Sync: Mainthread: {0} !{1}!", Thread.CurrentThread == _main, Native.IsMainThread()));
            });
        }

        public async void ASyncTestMethod3()
        {
            await Task.Delay(2500);
            Console.WriteLine("ASync is fetching tick count from main thread !{0}!", Native.IsMainThread());
            int ticks = await Sync.RunAsync(() => Server.GetTickCount());

            Console.WriteLine("Tick count is {0}", ticks);
        }

        public async void ASyncTestMethod4()
        {
            await Task.Delay(2500);
            Console.WriteLine("Logging synced to main thread.");
        }
    }
}