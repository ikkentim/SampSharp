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
            timer.Tick += (sender, args) => { };

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