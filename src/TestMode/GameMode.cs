// SampSharp
// Copyright 2015 Tim Potze
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
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using TestMode.Tests;

namespace TestMode
{
    public class GameMode : BaseMode
    {
        private readonly List<ITest> _tests = new List<ITest>
        {
            //new CommandsTest(),
            //new ASyncTest(),
            //new DelayTest(),
            //new MenuTest(),
            //new DisposureTest(),
            //new DialogTest(),
            //new CharsetTest(),
            //new VehicleInfoTest(),
            new NativesTest(),
            //new KeyHandlerTest(),
        };

        protected override void OnInitialized(EventArgs args)
        {
            Console.WriteLine("Booting version 2");
            Server.ToggleDebugOutput(true);
            SetGameModeText("sa-mp# testmode");
            UsePlayerPedAnimations();

            Debug.WriteLine("Loading player classes...");
            AddPlayerClass(65, new Vector(5), 0);

            foreach (ITest test in _tests)
            {
                Console.WriteLine("=========");
                Console.WriteLine("Starting test: {0}", test);
                test.Start(this);
                Console.WriteLine();
            }

            base.OnInitialized(args);
        }

        #region Overrides of BaseMode

        /// <summary>
        ///     Raises the <see cref="BaseMode.RconCommand" /> event.
        /// </summary>
        /// <param name="e">An <see cref="RconEventArgs" /> that contains the event data. </param>
        protected override void OnRconCommand(RconEventArgs e)
        {
            Console.WriteLine("[RCON] {0}", e.Command);
            base.OnRconCommand(e);
        }

        #endregion

        protected override void LoadControllers(ControllerCollection controllers)
        {
            base.LoadControllers(controllers);

            foreach (IControllerTest test in _tests.OfType<IControllerTest>())
                test.LoadControllers(controllers);
        }
    }
}