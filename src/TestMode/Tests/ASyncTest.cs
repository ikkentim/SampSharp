// SampSharp
// Copyright (C) 2014 Tim Potze
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
using SampSharp.GameMode.Tools;
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

            Thread main = Thread.CurrentThread;

            var timer = new Timer(1000, false);
            timer.Tick += (sender, args) => Console.WriteLine("Timer: Mainthread: {0}", main == Thread.CurrentThread);

            Console.WriteLine("Started async method");
        }

        public void ASyncTestMethod()
        {
            Thread main = Thread.CurrentThread;
            
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("ASync: Mainthread: {0}", Thread.CurrentThread == main);

                Sync.Run(() => Console.WriteLine("Sync: Mainthread: {0}", Thread.CurrentThread == main));
            });
        }
    }
}