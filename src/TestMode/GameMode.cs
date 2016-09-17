// SampSharp
// Copyright 2016 Tim Potze
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SampSharp.GameMode;
using SampSharp.GameMode.API;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using TestMode.Tests;

namespace TestMode
{
    public class GameMode : BaseMode
    {
        #region Overrides of BaseMode

        protected override void OnInitialized(EventArgs args)
        {
            Console.WriteLine($"TestMode for SampSharp v{GetType().Assembly.GetName().Version}");
            Console.WriteLine("----------------------");

            var exists = Native.Exists("RNPC_StartPlayback");
          
            Console.Write("RNPC LOADED? " + exists);

            return;
            Server.ToggleDebugOutput(true);

            SetGameModeText("sa-mp# testmode");

            UsePlayerPedAnimations();

            AddPlayerClass(65, new Vector3(5), 0);
            
            foreach (
                var test in
                    GetType()
                        .Assembly.GetTypes()
                        .Where(t => t.IsClass)
                        .Where(typeof (ITest).IsAssignableFrom)
                        .Select(t => Activator.CreateInstance(t) as ITest))
            {
                Console.WriteLine();
                Console.WriteLine("=========");
                Console.WriteLine(test);
                Console.WriteLine("=========");
                test.Start(this);
                Console.WriteLine($"Test {test} completed.");
                Console.WriteLine();
            }

            base.OnInitialized(args);
        }

        #endregion
    }
}