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
using System.IO;
using System.Linq;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace TestMode
{
    internal class GameMode : BaseMode
    {
        #region Overrides of BaseMode
        
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Console.WriteLine("The game mode has loaded.");
            AddPlayerClass(0, Vector3.Zero, 0);

 
            RunPerformanceBenchmark();
        }

        #endregion

        private void RunPerformanceBenchmark()
        {
            var v = BaseVehicle.Create(VehicleModelType.BMX, Vector3.One, 0, 0, 0);

            Timer x = new Timer(2000, true, true);

            void PerfTest()
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < 400000; i++)
                {
                    v.GetParameters(out VehicleParameterValue e, out var l, out var a, out var d, out var b, out var z, out var o);
                }
                sw.Stop();
                Console.WriteLine("TestMultiple={0}", sw.Elapsed.TotalMilliseconds);
            }

            PerfTest();

            x.Tick += (sender, args) =>
            {
                PerfTest();
            };
        }
    }
}