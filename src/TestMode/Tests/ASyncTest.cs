// SampSharp
// Copyright (C) 2015 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;
using System.Threading;
using System.Threading.Tasks;
using SampSharp.GameMode.Events;
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

            var timer = new Timer(1000, false);
            timer.Tick += (sender, args) => Console.WriteLine("Timer: Mainthread: {0}", _main == Thread.CurrentThread);

            Console.WriteLine("Started async method");

            gameMode.PlayerConnected += gameMode_PlayerConnected;
        }

        private void gameMode_PlayerConnected(object sender, PlayerEventArgs e)
        {
            ASyncPlayerConnectedDelayed(e.Player);
        }

        public async void ASyncPlayerConnectedDelayed(GtaPlayer player)
        {
            await Task.Delay(2000);
            Console.WriteLine("in ASyncPlayerConnectedDelayed");
            player.SendClientMessage("ASync message!");

            Sync.Run(() => player.SendClientMessage("Sync message!"));
        }

        public async void ASyncTestMethod2()
        {
            await Task.Delay(2000);
            Console.WriteLine("ASync2: Mainthread: {0}", Thread.CurrentThread == _main);

            Sync.Run(() => Console.WriteLine("Sync2: Mainthread: {0}", Thread.CurrentThread == _main));
        }

        public void ASyncTestMethod()
        {
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("ASync: Mainthread: {0}", Thread.CurrentThread == _main);

                Sync.Run(() => Console.WriteLine("Sync: Mainthread: {0}", Thread.CurrentThread == _main));
            });
        }

        public async void ASyncTestMethod3()
        {
            await Task.Delay(2500);
            Console.WriteLine("ASync is fetching tick count from main thread");
            var ticks = await Sync.Run(() => Server.GetTickCount());

            Console.WriteLine("Tick count is {0}", ticks);
        }
        public async void ASyncTestMethod4()
        {
            await Task.Delay(2500);
            Console.WriteLine("Logging synced to main thread.");
        }
    }
}