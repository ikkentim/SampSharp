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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BenchmarkMode.Tests;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;

namespace BenchmarkMode
{
    public class GameMode : BaseMode
    {
        private readonly List<ITest> _tests = new List<ITest>
        {
            new NativeIsPlayerConnected(),
            new NativeCreateDestroyVehicle(),
            new CreateDestroyVehicle(),
        };

        public override bool OnGameModeInit()
        {
            SetGameModeText("sa-mp# benchmarkmode");

            Console.WriteLine("\n--------------------------------------");
            Console.WriteLine(" SampSharp benchmark MONO test");
            Console.WriteLine("--------------------------------------\n");
            var sw = new Stopwatch();
            foreach (ITest test in _tests)
            {
                int count = 0;
                sw.Start();
                while (sw.ElapsedMilliseconds < 5000)
                {
                    count ++;
                    test.Run(this);
                }
                sw.Stop();
                Console.WriteLine(" Bench for {0}: executes, by average, {1} times/ms.", test.GetType().Name,
                    Math.Round(((float) count/sw.ElapsedMilliseconds), 2));
                sw.Reset();
            }

            SendRconCommand("loadfs bench");

            return true;
        }

        protected override void LoadControllers(ControllerCollection controllers)
        {
            base.LoadControllers(controllers);

            foreach (IControllerTest test in _tests.OfType<IControllerTest>())
                test.LoadControllers(controllers);
        }
    }
}