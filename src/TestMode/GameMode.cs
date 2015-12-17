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
using System.Reflection;
using SampSharp.GameMode;
using SampSharp.GameMode.API;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.SAMP.Commands.Parameters;
using SampSharp.GameMode.World;
using TestMode;
using TestMode.Tests;

[assembly: SampSharpExtension(typeof(ExA), typeof(ExB))]
//[assembly: SampSharpExtension(typeof(ExB), typeof(ExA))]

namespace TestMode
{
    #region Misc classes

    public class ExA : Extension
    {
        #region Overrides of Extension

        public override void PostLoad(BaseMode gameMode)
        {
            Console.WriteLine("ExA extension was loaded!");
        }

        #endregion
    }

    public class ExB : Extension
    {
        #region Overrides of Extension

        public override void PostLoad(BaseMode gameMode)
        {
            Console.WriteLine("ExB extension was loaded!");
        }

        #endregion
    }


    public class Vehicle : BaseVehicle
    {
        public override void OnPlayerEnter(EnterVehicleEventArgs e)
        {
            e.Player.SendClientMessage("You entered {0} ID {1}", this, Id);
        }
    }

    public class VehicleController : BaseVehicleController
    {
        public override void RegisterTypes()
        {
            Vehicle.Register<Vehicle>();
        }
    }

    [CommandGroup("playertest")]
    public class Player : BasePlayer
    {
        [Command("spawnbmx")]
        public void SpawnVehicle()
        {
            var v = BaseVehicle.Create(VehicleModelType.BMX, Position + new Vector3(0, 0.5f, 0), 0, -1, -1);
            PutInVehicle(v);
        }

        [Command("spawn")]
        public void SpawnVehicle(VehicleModelType model)
        {
            var v = BaseVehicle.Create(model, Position + new Vector3(0, 0.5f, 0), 0, -1, -1);
            PutInVehicle(v);
        }
    }

    public class PlayerController : BasePlayerController
    {
        public override void RegisterTypes()
        {
            Player.Register<Player>();
        }
    }

    #endregion

    public class MyCommandManager : CommandsManager
    {
        public MyCommandManager(BaseMode gameMode) : base(gameMode)
        {
        }

        #region Overrides of CommandsManager

        /// <summary>
        ///     Creates a command.
        /// </summary>
        /// <param name="commandPaths">The command paths.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="ignoreCase">if set to <c>true</c> ignore the case the command.</param>
        /// <param name="permissionCheckers">The permission checkers.</param>
        /// <param name="method">The method.</param>
        /// <param name="usageMessage">The usage message.</param>
        /// <returns>The created command</returns>
        protected override ICommand CreateCommand(CommandPath[] commandPaths, string displayName, bool ignoreCase,
            IPermissionChecker[] permissionCheckers, MethodInfo method, string usageMessage)
        {
            return new MyCommand(commandPaths, displayName, ignoreCase, permissionCheckers, method, usageMessage);
        }

        #endregion
    }

    public class MyCommand : DefaultCommand
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultCommand" /> class.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="ignoreCase">if set to <c>true</c> ignore the case of the command.</param>
        /// <param name="permissionCheckers">The permission checkers.</param>
        /// <param name="method">The method.</param>
        /// <param name="usageMessage">The usage message.</param>
        public MyCommand(CommandPath[] names, string displayName, bool ignoreCase,
            IPermissionChecker[] permissionCheckers, MethodInfo method, string usageMessage)
            : base(names, displayName, ignoreCase, permissionCheckers, method, usageMessage)
        {
        }

        #region Overrides of DefaultCommand

        /// <summary>
        ///     Gets the type of the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>The type of the parameter.</returns>
        protected override ICommandParameterType GetParameterType(ParameterInfo parameter, int index, int count)
        {
            // use default parameter type detection.
            var type = base.GetParameterType(parameter, index, count);
            
            if (type != null)
                return type;

            // if no parameter type was found check if it's of any type we recognize.
            if (parameter.ParameterType == typeof (bool))
            {
                // TODO: detected this type to be of type `bool`. 
                // TODO: Return an implementation of ICommandParameterType which processes booleans.
            }

            // Unrecognized type. Return null.
            return null;
        }
        
        /// <summary>
        ///     Sends the permission denied message for the specified permission checker.
        /// </summary>
        /// <param name="permissionChecker">The permission checker.</param>
        /// <param name="player">The player.</param>
        /// <returns>true on success; false otherwise.</returns>
        protected override bool SendPermissionDeniedMessage(IPermissionChecker permissionChecker, BasePlayer player)
        {
            if (permissionChecker == null) throw new ArgumentNullException(nameof(permissionChecker));
            if (player == null) throw new ArgumentNullException(nameof(player));

            if (permissionChecker.Message == null)
                return false;

            // Send permission denied message in red instead of white.
            player.SendClientMessage(Color.Red, permissionChecker.Message);
            return true;
        }
        
        #endregion
    }

    public class MyCommandController : CommandController
    {
        #region Overrides of CommandController

        /// <summary>
        ///     Registers the services this controller provides.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        /// <param name="serviceContainer">The service container.</param>
        public override void RegisterServices(BaseMode gameMode, GameModeServiceContainer serviceContainer)
        {
            CommandsManager = new CommandsManager(gameMode);
            serviceContainer.AddService(CommandsManager);

            // Register commands in game mode.
            CommandsManager.RegisterCommands(gameMode.GetType());
        }

        #endregion
    }

    public class GameMode : BaseMode
    {
        #region Tests

        private readonly List<ITest> _tests = new List<ITest>
        {
            new CommandsTest(),
//            new ASyncTest(),
//            new DelayTest(),
//            new MenuTest(),
//            new DisposureTest(),
//            new DialogTest(),
//            new CharsetTest(),
//            new VehicleInfoTest(),
//            new NativesTest(),
//            new MapAndreasTest(),
//            new KeyHandlerTest(),
//            new ExtensionTest(),
//            new ActorTest(),
//            new ServicesTest()
        };

        #endregion
        
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
        
        protected override void LoadControllers(ControllerCollection controllers)
        {
            base.LoadControllers(controllers);

            controllers.Remove<BasePlayerController>();
            controllers.Add(new PlayerController());
            controllers.Remove<BaseVehicleController>();
            controllers.Add(new VehicleController());
            controllers.Remove<CommandController>();
            controllers.Add(new MyCommandController());

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