// SampSharp
// Copyright 2017 Tim Potze
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
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;
using Timer = SampSharp.GameMode.SAMP.Timer;

namespace TestMode.Tests
{
    public class ASyncTest : ITest
    {
        private Thread _main;
        private DateTime _timerStart;


        public void Start(GameMode gameMode)
        {
            //Proof timers and C# async can run at the same time
            //while the game is still functioning properly
            //also proof calls to native from other threads work all right. (due to being synced)
            _main = Thread.CurrentThread;

            ASyncTestMethod();
            ASyncTestMethod2();
            ASyncTestMethod3();
            ASyncTestMethod4();

            _timerStart = DateTime.Now;
            var timer = new Timer(new TimeSpan(0, 0, 0, 2, 500), false);
            timer.Tick += TimerOnTick;

            gameMode.PlayerConnected += gameMode_PlayerConnected;
        }

        private void Print(string env, string msg)
        {
            Console.WriteLine($"[{env}:{_main == Thread.CurrentThread}] {msg}");
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            Print("timer", $"took " + (DateTime.Now - _timerStart));
            Print("timer", $"ticks " + Server.GetTickCount());
        }

        private void gameMode_PlayerConnected(object sender, EventArgs e)
        {
            ASyncPlayerConnectedDelayed(sender as BasePlayer);
        }

        public async void ASyncPlayerConnectedDelayed(BasePlayer player)
        {
            Print("asyncp", "delay(2000)");
            await Task.Delay(2000);
            Print("asyncp", "past delay");
            player.SendClientMessage("ASync message! (on main thread?: {0})", Thread.CurrentThread == _main);

            Sync.Run(
                () => player.SendClientMessage("Sync message! (on main thread?: {0})", Thread.CurrentThread == _main));
        }

        public async void ASyncTestMethod2()
        {
            Print("async2", "delay(2000)");
            await Task.Delay(2000);

            Print("async2", "past delay");
            Sync.Run(() => Print("async2", "in Sync.Run"));
        }

        public void ASyncTestMethod()
        {
            Print("async1", "calling Task.Run");
            Task.Run(async () =>
            {
                Print("async1", "Task => delay(1000)");
                await Task.Delay(1000);

                Print("async1", "Task => past delay");
                Sync.Run(() => Print("async1", "Task => in Sync.Run"));
            });
        }

        public async void ASyncTestMethod3()
        {
            Print("async3", "delay(2500)");
            await Task.Delay(2500);

            Print("async3", "Call Sync.RunAsync(GetTickCount)");
            var ticks = await Sync.RunAsync(() => Server.GetTickCount());

            Print("async3", $"Ticks: {ticks}");
            Print("async3", $"Immediate ticks call: {Server.GetTickCount()}");
        }

        public async void ASyncTestMethod4()
        {
            Print("async4", "delay(2500)");
            await Task.Delay(2500);
            Print("async4", "done.");
        }
    }
}