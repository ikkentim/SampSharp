// SampSharp
// Copyright 2018 Tim Potze
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
using System.Diagnostics;
using System.Threading.Channels;
using SampSharp.Core.Callbacks;
using SampSharp.Core.Hosting;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace TestMode.GameMode
{
    public class BenchmarkNatives
    {
        [NativeMethod] 
        public virtual void CallRemoteFunction(string func, string format, params object[] args)
        {
            throw new NativeNotImplementedException();
        }
    }

    public class GameMode : BaseMode
    {
        private (string, int, float)? _benchStore;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Client.UnhandledException += (sender, args) => Console.WriteLine($"ERROR! {args.Exception}");

            Console.WriteLine("The game mode has loaded.");
            
            AddPlayerClass(0, new Vector3(1482.9055, 1504.2122, 10.5474), 0);

            BaseVehicle.Create(VehicleModelType.BF400, new Vector3(1489.9055, 1520.2122, 11), 0, 3, 3);
            BaseVehicle.Create(VehicleModelType.Banshee, new Vector3(1449.9055, 1520.2122, 11), 0, 3, 3);
            BaseVehicle.Create(VehicleModelType.Cabbie, new Vector3(1449.9055, 1550.2122, 11), 0, 3, 3);
            
            return;

            Console.WriteLine("Bench start");
            RunCallbackBenchmark();
        }

        private void RunCallbackBenchmark()
        {
            var natives = NativeObjectProxyFactory.CreateInstance<BenchmarkNatives>();
            var args = new object[] { "stringValue", 4321, 23.665f };
            var sw = new Stopwatch();
            
            const int maxRuns = 10;
            const int runCallCount = 1_000_000;

            var totalElapsed = TimeSpan.Zero;
            for (var j = 0; j <= maxRuns; j++)
            {
                Console.WriteLine($"Bench run {j}/{maxRuns}");
                if (j == 0) Console.WriteLine("(warmup)");

                sw.Restart();

                for (var i = 0; i < runCallCount; i++)
                {
                    natives.CallRemoteFunction("BenchmarkCallback", "sdf", args);

                    // validate
                    
                    if (!_benchStore.HasValue || _benchStore.Value.Item1 != "stringValue" ||
                        _benchStore.Value.Item2 != 4321 || Math.Abs(_benchStore.Value.Item3 - 23.665f) > 0.001f)
                    {
                        throw new InvalidOperationException("native call failed!");
                    }

                    _benchStore = null;
                }

                sw.Stop();

                if (j > 0)
                    totalElapsed += sw.Elapsed;
                Console.WriteLine($"{runCallCount} calls took {sw.Elapsed}");
            }

            Console.WriteLine($"AVG of {totalElapsed / maxRuns}");
        }

        [Callback]
        public void BenchmarkCallback(string stringValue, int intValue, float floatValue)
        {
            _benchStore = (stringValue, intValue, floatValue);
        }
    }
}