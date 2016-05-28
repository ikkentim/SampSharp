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
using System.Linq;
using System.Reflection;
using SampSharp.GameMode.API;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode
{
    /// <summary>
    ///     Base class for a SA-MP game mode.
    /// </summary>
    public abstract partial class BaseMode : IDisposable
    {
        private readonly ControllerCollection _controllers = new ControllerCollection();
        private readonly List<IExtension> _extensions = new List<IExtension>();

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        public static BaseMode Instance { get; private set; }

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseMode" /> class.
        /// </summary>
        protected BaseMode() : this(true)
        {
        }

        protected BaseMode(bool redirectConsole)
        {
            if (redirectConsole)
                Console.SetOut(new ServerLogWriter());

            if (FrameworkConfiguration.MessageLevel == FrameworkMessageLevel.Debug)
            {
                var type = Type.GetType("Mono.Runtime");
                var displayName = type?.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);

                if (displayName != null)
                    FrameworkLog.WriteLine(FrameworkMessageLevel.Debug, "Detected mono version: {0}",
                        displayName.Invoke(null, null));
            }

            Instance = this;
        }

        #endregion
        
        /// <summary>
        ///     Gets the collection of controllers loaded.
        /// </summary>
        protected virtual ControllerCollection Controllers => _controllers;

        /// <summary>
        ///     Gets the <see cref="GameModeServiceContainer" /> holding all the service providers attached to the game mode.
        /// </summary>
        public virtual GameModeServiceContainer Services { get; } = new GameModeServiceContainer();
        
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

        public void AutoloadControllersForAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetExportedTypes()
                .Where(t => t.IsClass &&
                            typeof (IController).IsAssignableFrom(t) &&
                            t.GetCustomAttribute<ControllerAttribute>() != null))
            {
                FrameworkLog.WriteLine(FrameworkMessageLevel.Debug, $"Autoloading type {type}...");
                _controllers.Override(Activator.CreateInstance(type) as IController);
            }
        }

        internal void Initialize()
        {
            LoadExtensions();
            
            AutoloadPoolTypes();
            LoadServicesAndControllers();
        }

        /// <summary>
        ///     Loads all controllers into the given ControllerCollection.
        /// </summary>
        /// <param name="controllers">The collection to load the default controllers into.</param>
        protected virtual void LoadControllers(ControllerCollection controllers)
        {
            AutoloadControllers();

            foreach (var extension in _extensions)
                extension.LoadControllers(this, controllers);
        }

        private void AutoloadControllers()
        {
            AutoloadControllersForAssembly(typeof(BaseMode).Assembly);
            AutoloadControllersForAssembly(GetType().Assembly);
        }

        private void AutoloadPoolTypes()
        {
            var types = new List<Type>();

            foreach (var poolType in new[] {typeof (BaseMode), GetType()}.Concat(_extensions.Select(e => e.GetType()))
                .Select(t => t.Assembly)
                .Distinct()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && t.GetCustomAttribute<PooledTypeAttribute>() != null)
                .Distinct())
            {
                // If poolType or subclass of poolType is already in types, continue.
                if (types.Any(t => t == poolType || poolType.IsAssignableFrom(t)))
                {
                    FrameworkLog.WriteLine(FrameworkMessageLevel.Debug,
                        $"Pool of type {poolType} is not autoloaded because a subclass of it will already be loaded.");
                    continue;
                }

                // Remove all types in types where type is supertype of poolType.
                foreach (var t in types.Where(t => t.IsAssignableFrom(poolType)).ToArray())
                {
                    FrameworkLog.WriteLine(FrameworkMessageLevel.Debug,
                        $"No longer autoloading type {poolType} because a subclass of it is going to be loaded.");
                    types.Remove(t);
                }

                FrameworkLog.WriteLine(FrameworkMessageLevel.Debug, $"Autoloading pool of type {poolType}.");
                types.Add(poolType);
            }

            var poolTypes = new[]
            {
                typeof (IdentifiedPool<>),
                typeof (IdentifiedOwnedPool<,>)
            };
            foreach (var type in types)
            {
                var pool = type;
                do
                {
                    pool = pool.BaseType;
                } while (pool != null && (!pool.IsGenericType || !poolTypes.Contains(pool.GetGenericTypeDefinition())));

                if (pool == null)
                {
                    FrameworkLog.WriteLine(FrameworkMessageLevel.Debug, $"Skipped autoloading pool of type {type} because it's not a subtype of a pool.");
                    continue;
                }

                pool.GetMethod("Register", new[] {typeof (Type)}).Invoke(null, new[] {type});
            }
        }

        private void LoadServicesAndControllers()
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

        private void AddExtensionToLoadList(Assembly assembly, List<Assembly> load, List<Assembly> loading)
        {
            // Ensure assembly is an extension.
            if (!assembly.GetCustomAttributes<SampSharpExtensionAttribute>().Any())
                throw new Exception($"Assembly {assembly} is not an extension");

            // Check if assembly is already in the load list.
            if (load.Contains(assembly))
                return;

            // Already in loading chain? (circular dependency detected).
            if (loading.Contains(assembly))
                throw new Exception($"Circular extension dependency detected: {assembly}");

            loading.Add(assembly);

            // Load extension's dependencies to the load list.
            foreach (
                var dependency in
                    assembly.GetCustomAttributes<SampSharpExtensionAttribute>()
                        .SelectMany(a => a.LoadBeforeAssemblies)
                        .Except(new[] {assembly}))
                AddExtensionToLoadList(dependency, load, loading);

            loading.Remove(assembly);
            load.Add(assembly);
        }

        private void LoadExtensions()
        {
            var load = new List<Assembly>();

            // Create a dependency-ordered list of extensions.
            var loading = new List<Assembly>();
            foreach (
                var assembly in
                    GetType()
                        .Assembly.GetReferencedAssemblies()
                        .Select(Assembly.Load)
                        .Concat(new[] {GetType().Assembly})
                        .Distinct()
                        .Where(a => a.GetCustomAttributes<SampSharpExtensionAttribute>().Any()))
                AddExtensionToLoadList(assembly, load, loading);

            // Load extensions according to dependency list.
            foreach (var assembly in load)
            {
                var attributes = assembly.GetCustomAttributes<SampSharpExtensionAttribute>();
                
                foreach (var extensionType in attributes.Select(attribute => attribute.Type))
                {
                    if (extensionType == null)
                        continue;
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
                            "The extension from {0} could not be loaded. The specified extension type is not part of the assembly.",
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

                    // Register the extension to the plugin.
                    var extension = (IExtension) Activator.CreateInstance(extensionType);
                    Extension.Register(extension);
                    _extensions.Add(extension);
                }
            }
        }

    }
}