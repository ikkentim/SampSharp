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
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using SampSharp.Core.Communication;
using SampSharp.Core.Hosting;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.Core.Natives.NativeObjects.FastNatives;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace TestMode
{
    public class GameMode : BaseMode
    {
        #region Overrides of BaseMode
        
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Console.WriteLine("The game mode has loaded.");
            AddPlayerClass(0, Vector3.Zero, 0);

            // RunPerformanceBenchmark();
            // .
        }

        #endregion

        private unsafe void CallThat(IntPtr native, int id)
        {
            Span<int> data = stackalloc int[16];
            // 0-7: pointers to cells   [8]
            // 8: id cell               [1]
            // 9-15: out cells          [7]

            fixed (int* ptData = &data.GetPinnableReference())
            {
                for (var j = 0; j < 8; j++)// set points for all 8 args
                {
                    data[j] = (int) (IntPtr) (ptData + 8 + j);
                }

                data[8] = id;

                Interop.FastNativeInvoke(native, "dRRRRRRR", ptData);
            }
        }
        
        private void RunPerformanceBenchmark()
        {
            var vehicle = BaseVehicle.Create(VehicleModelType.BMX, Vector3.One, 0, 0, 0);
            
            var native = Interop.FastNativeFind("GetVehicleParamsEx");

            var timer = new Timer(2000, true, true);

            var id = vehicle.Id;
  
            void PerfTest()
            {
                var sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < 400000; i++)
                {
                    CallThat(native, id);
                }
                sw.Stop();
                Console.WriteLine("BestPossible={0}", sw.Elapsed.TotalMilliseconds);
                sw.Reset();
                sw.Start();
                for (var i = 0; i < 400000; i++)
                {
                    vehicle.GetParameters(out VehicleParameterValue a, out _, out _, out _, out _, out _, out _);
                }

                sw.Stop();
                Console.WriteLine("ViaOOWrapper={0}", sw.Elapsed.TotalMilliseconds);
                sw.Reset();

                var proxy = BaseVehicle.VehicleInternal.Instance;
                sw.Start();
                for (var i = 0; i < 400000; i++)
                {
                    proxy.GetVehicleParamsEx(id, out _, out _, out _, out _, out _, out _, out _);
                }

                sw.Stop();
                Console.WriteLine("ViaDirectProxy={0}", sw.Elapsed.TotalMilliseconds);
                sw.Reset();
            }

            PerfTest();

            var benchRuns = 50;
            timer.Tick += (sender, args) =>
            {
                PerfTest();

                if (--benchRuns == 0)
                    timer.IsRunning = false;
            };
        }
    }
}