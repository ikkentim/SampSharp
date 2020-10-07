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
using SampSharp.Core.Hosting;
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

        private ReadOnlySpan<char> Testing(string inp, out Encoding enc, out int len)
        {
            enc = Client.Encoding ?? Encoding.ASCII;
            var result = inp.AsSpan();
            len = enc.GetByteCount(result) + 1;
            return result;
        }

        private unsafe void RunPerformanceBenchmark()
        {
            var v = BaseVehicle.Create(VehicleModelType.BMX, Vector3.One, 0, 0, 0);

            var id = v.Id;
            var native = Interop.FastNativeFind("GetVehicleParamsEx");
            

            Timer x = new Timer(2000, true, true);

            
            var nativeSetGameModeText = Interop.FastNativeFind("SetGameModeText");
            
            Span<int> dataSetGameModeText = stackalloc int[16];
            var test = "TestValue";

            var text = Testing(test, out var enc, out var len);
            Span<byte> zzz = stackalloc byte[len];
            zzz[len - 1] = 0;
            enc.GetBytes(text, zzz);

            fixed (int* ptXData = &dataSetGameModeText.GetPinnableReference())
            fixed (byte* xp = &zzz.GetPinnableReference())
            {
                dataSetGameModeText[0] = (int) (IntPtr) xp;
                Interop.FastNativeInvoke(nativeSetGameModeText, "s", ptXData);
            }

            void CallThat()
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

            void PerfTest()
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < 400000; i++)
                {
                    CallThat();

                    //v.GetParameters(out VehicleParameterValue e, out var l, out var a, out var d, out var b, out var z, out var o);
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