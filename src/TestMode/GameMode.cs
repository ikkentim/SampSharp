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
using System.Linq;
using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using TestMode.Tests;

namespace TestMode
{
    public class Vehicle : BaseVehicle
    {
        public override void OnPlayerEnter(EnterVehicleEventArgs e)
        {
            e.Player.SendClientMessage("You entered {0} ID {1}", this, Id);
        }
    }

    public class VehicleController : GtaVehicleController
    {
        public override void RegisterTypes()
        {
            Vehicle.Register<Vehicle>();
        }
    }

    public class Player : BasePlayer
    {
        [Command("spawn")]
        public void SpawnVehicle()
        {
            var v = BaseVehicle.Create(VehicleModelType.BMX, Position + new Vector3(0, 0.5f, 0), 0, -1, -1);
            PutInVehicle(v);
        }
    }

    public class PlayerController : GtaPlayerController
    {
        public override void RegisterTypes()
        {
            Player.Register<Player>();
        }
    }

    public class GameMode : BaseMode
    {
        #region Tests

        private readonly List<ITest> _tests = new List<ITest>
        {
            new CommandsTest(),
            new ASyncTest(),
            new DelayTest(),
            new MenuTest(),
            new DisposureTest(),
            new DialogTest(),
            new CharsetTest(),
            new VehicleInfoTest(),
            new NativesTest(),
            new MapAndreasTest(),
            new KeyHandlerTest(),
            new ExtensionTest(),
            new ActorTest(),
            new ServicesTest(),
            new PoolTest()
        };

        #endregion

        private void StackFiller(int c)
        {
            if (c <= 0)
                throw new Exception();
            StackFiller(c - 1);
        }

        #region Overrides of BaseMode

        protected override void OnInitialized(EventArgs args)
        {
            Console.WriteLine("TestMode for SampSharp");
            Console.WriteLine("----------------------");

            Server.ToggleDebugOutput(true);
            SetGameModeText("sa-mp# testmode");
            UsePlayerPedAnimations();

            AddPlayerClass(65, new Vector3(5), 0);

            foreach (var test in _tests)
            {
                Console.WriteLine("=========");
                Console.WriteLine("Starting test: {0}", test);
                test.Start(this);
                Console.WriteLine();
            }

            base.OnInitialized(args);
        }

        protected override void OnRconCommand(RconEventArgs e)
        {
            Console.WriteLine("[RCON] {0}", e.Command);
            BasePlayer.SendClientMessageToAll("Rcon message: {0}", e.Command);

            Console.WriteLine("Throwing exception after a stack filter...");
            StackFiller(20);

            e.Success = false;
            base.OnRconCommand(e);
        }

        protected override void LoadControllers(ControllerCollection controllers)
        {
            base.LoadControllers(controllers);

            controllers.Remove<GtaPlayerController>();
            controllers.Add(new PlayerController());
            controllers.Remove<GtaVehicleController>();
            controllers.Add(new VehicleController());

            foreach (var test in _tests.OfType<IControllerTest>())
                test.LoadControllers(controllers);
        }

        /// <summary>
        ///     Raises the <see cref="E:CallbackException" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ExceptionEventArgs" /> instance containing the event data.</param>
        protected override void OnCallbackException(ExceptionEventArgs e)
        {
            Console.WriteLine("[SampSharp] Exception thrown during execution of {0}:", e.Exception.TargetSite.Name);

            Console.WriteLine("{0}: {1}", e.Exception.GetType().FullName, e.Exception.Message);
            Console.WriteLine(e.Exception.StackTrace);
            e.Handled = true;
            base.OnCallbackException(e);
        }

        #endregion
    }
}