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
            var displayName = type?.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);

            if (displayName != null)
                FrameworkLog.WriteLine(FrameworkMessageLevel.Debug, "Detected mono version: {0}",
                    displayName.Invoke(null, null));

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
            var loadAssemblies = new List<Assembly>();
            var loadingAssemblies = new List<Assembly>();


            Action<Assembly> loadAssemblyOrder = null;
            loadAssemblyOrder = assembly =>
            {
                // Already in load order.
                if (loadAssemblies.Contains(assembly))
                    return;

                // Make sure the assembly is an extension.
                if (!assembly.GetCustomAttributes<SampSharpExtensionAttribute>().Any())
                    return;

                // Already in loading chain (circular dependency detected).
                if (loadingAssemblies.Contains(assembly))
                    throw new Exception($"Circular extension dependency detected. ({assembly})");

                loadingAssemblies.Add(assembly);

                foreach (
                    var dependency in
                        assembly.GetCustomAttributes<SampSharpExtensionAttribute>()
                            .SelectMany(a => a.LoadBeforeAssemblies)
                            .Except(new[] {assembly}))
                    loadAssemblyOrder(dependency);

                loadingAssemblies.Remove(assembly);
                loadAssemblies.Add(assembly);
            };

            foreach (
                var assembly in
                    GetType()
                        .Assembly.GetReferencedAssemblies()
                        .Select(Assembly.Load)
                        .Concat(new []{GetType().Assembly})
                        .Distinct()
                        .Where(a => a.GetCustomAttributes<SampSharpExtensionAttribute>().Any()))
                loadAssemblyOrder(assembly);

            foreach (var assembly in loadAssemblies)
            {
                var attributes = assembly.GetCustomAttributes<SampSharpExtensionAttribute>();

                foreach (var extensionType in attributes.Select(attribute => attribute.Type))
                {
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
                    if (extensionType.Assembly != assembly)
                    {
                        FrameworkLog.WriteLine(FrameworkMessageLevel.Warning,
                            "The extension from {0} could not be loaded. The specified extension type is not part of the loading assembly.",
                            assembly);
                        continue;
                    }
                    if (_extensions.Any(e => e.GetType() == extensionType))
                    {
                        FrameworkLog.WriteLine(FrameworkMessageLevel.Warning,
                            "The extension from {0} could not be loaded. The specified extension type was already loaded.",
                            assembly);
                        continue;
                    }

                    var extension = (IExtension) Activator.CreateInstance(extensionType);
                    Extension.Register(extension);
                    _extensions.Add(extension);
                }
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
        protected virtual ControllerCollection Controllers => _controllers;

        /// <summary>
        ///     Gets the <see cref="GameModeServiceContainer" /> holding all the service providers attached to the game mode.
        /// </summary>
        /// <value>
        ///     The services.
        /// </value>
        public virtual GameModeServiceContainer Services { get; }

        #endregion

        #region Methods of BaseMode

        private void RegisterControllers()
        {
            foreach (var extension in _extensions)
                extension.LoadServices(this);

            LoadControllers(_controllers);

            foreach (var controller in _controllers.OfType<IGameServiceProvider>())
                controller.RegisterServices(this, Services);

            foreach (var controller in _controllers.OfType<ITypeProvider>())
                controller.RegisterTypes();

            foreach (var controller in _controllers.OfType<IEventListener>())
                controller.RegisterEvents(this);

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
                extension.LoadControllers(this, controllers);
        }

        #endregion
    }
}