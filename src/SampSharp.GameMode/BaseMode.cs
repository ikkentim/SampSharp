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
using SampSharp.GameMode.API;
using SampSharp.GameMode.Controllers;

namespace SampSharp.GameMode
{
    /// <summary>
    ///     Base class for a SA-MP game mode.
    /// </summary>
    public abstract partial class BaseMode : IDisposable
    {
        private readonly ControllerCollection _controllers = new ControllerCollection();
        private readonly List<IExtension> _extensions = new List<IExtension>(); 
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseMode" /> class.
        /// </summary>
        protected BaseMode()
        {
            Console.SetOut(new LogWriter());

            var type = Type.GetType("Mono.Runtime");
            if (type != null)
            {
                var displayName = type.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);

                if (displayName != null)
                    FrameworkLog.WriteLine(FrameworkMessageLevel.Debug, "Detected mono version: {0}",
                        displayName.Invoke(null, null));
            }

            Services = new GameModeServiceContainer();

            Instance = this;
        }

        #endregion

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        public static BaseMode Instance { get; private set; }

        internal void Initialize()
        {
            foreach (
                var assembly in
                    GetType()
                        .Assembly.GetReferencedAssemblies()
                        .Select(Assembly.Load)
                        .Where(a => a.GetCustomAttribute<SampSharpExtensionAttribute>() != null))
            {
                var attribute = assembly.GetCustomAttribute<SampSharpExtensionAttribute>();

                var extensionType = attribute.Type;

                if (extensionType == null)
                {
                    FrameworkLog.WriteLine(FrameworkMessageLevel.Warning,
                        "The extension from {0} could not be loaded. The specified extension type is null.",
                        assembly);
                    continue;
                }
                if (!typeof (IExtension).IsAssignableFrom(extensionType))
                {
                    FrameworkLog.WriteLine(FrameworkMessageLevel.Warning,
                        "The extension from {0} could not be loaded. The specified extension type does not inherit from IExtension.",
                        assembly);
                    continue;
                }

                var extension = (IExtension) Activator.CreateInstance(extensionType);
                Extension.Register(extension);
                _extensions.Add(extension);
            }
            
            Native.LoadDelegates<BaseMode>();
            Native.LoadDelegates(GetType()); 

            RegisterControllers();
        }

        #region Implementation of IDisposable

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _controllers.Dispose();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion

        #region Properties of BaseMode

        /// <summary>
        ///     Gets the collection of controllers loaded.
        /// </summary>
        protected virtual ControllerCollection Controllers
        {
            get { return _controllers; }
        }

        /// <summary>
        ///     Gets the <see cref="GameModeServiceContainer" /> holding all the service providers attached to the game mode.
        /// </summary>
        /// <value>
        ///     The services.
        /// </value>
        public virtual GameModeServiceContainer Services { get; private set; }

        #endregion

        #region Methods of BaseMode

        private void RegisterControllers()
        {
            foreach (var extension in _extensions)
                extension.PreLoad(this);

            LoadControllers(_controllers);

            foreach (var controller in _controllers)
            {
                var typeProvider = controller as ITypeProvider;
                var eventListener = controller as IEventListener;
                var serviceProvider = controller as IGameServiceProvider;

                if (serviceProvider != null)
                    serviceProvider.RegisterServices(this, Services);

                if (typeProvider != null)
                    typeProvider.RegisterTypes();

                if (eventListener != null)
                    eventListener.RegisterEvents(this);
            }

            foreach (var extension in _extensions)
                extension.PostLoad(this);
        }

        /// <summary>
        ///     Loads all controllers into the given ControllerCollection.
        /// </summary>
        /// <param name="controllers">The collection to load the default controllers into.</param>
        protected virtual void LoadControllers(ControllerCollection controllers)
        {
            controllers.Add(new CommandController());
            controllers.Add(new DialogController());
            controllers.Add(new GlobalObjectController());
            controllers.Add(new MenuController());
            controllers.Add(new BasePlayerController());
            controllers.Add(new PlayerObjectController());
            controllers.Add(new PlayerTextDrawController());
            controllers.Add(new TextDrawController());
            controllers.Add(new TimerController());
            controllers.Add(new DelayController());
            controllers.Add(new BaseVehicleController());
            controllers.Add(new SyncController());
            controllers.Add(new PickupController());
            controllers.Add(new ActorController());

            foreach (var extension in _extensions)
                extension.Load(this, controllers);
        }

        #endregion
    }
}