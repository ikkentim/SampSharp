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

namespace SampSharp.GameMode.API
{
    /// <summary>
    ///     Represents a native function.
    /// </summary>
    public static class Native
    {
        private static INativeLoader _nativeLoader = new DefaultNativeLoader();
        private static readonly List<string> LoadedAssemblies = new List<string>();

        /// <summary>
        ///     Gets or sets the native loader.
        /// </summary>
        public static INativeLoader NativeLoader
        {
            get { return _nativeLoader; }
            set { if (value != null) _nativeLoader = value; }
        }
        
        /// <summary>
        ///     Checks whether a native with the specified <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True if a native with the specified name exists; False otherwise.</returns>
        public static bool Exists(string name)
        {
            return _nativeLoader.Exists(name);
        }

        /// <summary>
        ///     Loads a native with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The loaded native.</returns>
        public static INative Load(string name, params Type[] parameterTypes)
        {
            return Load(name, null, parameterTypes);
        }

        /// <summary>
        ///     Loads a native with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sizes">The references to the parameter which contains the size of array parameters.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The loaded native.</returns>
        public static INative Load(string name, int[] sizes, params Type[] parameterTypes)
        {
            return _nativeLoader.Load(name, sizes, parameterTypes);
        }

        /// <summary>
        ///     Gets the native with the specified handle.
        /// </summary>
        /// <param name="handle">The handle of the native.</param>
        /// <returns>The native.</returns>
        public static INative Get(int handle)
        {
            return _nativeLoader.Get(handle);
        }
        /// <summary>
        ///     Loads the delegate fields annotated with a <see cref="NativeAttribute" /> attribute within the assembly of the
        ///     specified type <typeparamref name="T" /> with calls to the desired native function.
        /// </summary>
        /// <typeparam name="T">A type within the assembly of which to load the delegate fields.</typeparam>
        public static void LoadDelegates<T>()
        {
            LoadDelegates(typeof (T));
        }

        /// <summary>
        ///     Loads the delegate fields annotated with a <see cref="NativeAttribute" /> attribute within the assembly of the
        ///     specified <paramref name="type" /> with calls to the desired native function.
        /// </summary>
        /// <param name="type">A type within the assembly of which to load the delegate fields.</param>
        public static void LoadDelegates(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            LoadDelegates(type.Assembly);
        }

        /// <summary>
        ///     Loads the delegate fields annotated with a <see cref="NativeAttribute" /> attribute within the specified
        ///     <paramref name="assembly" /> with calls to the desired native function.
        /// </summary>
        /// <param name="assembly">The assembly of which to load the delegate fields.</param>
        public static void LoadDelegates(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            if (LoadedAssemblies.Contains(assembly.FullName))
            {
                FrameworkLog.WriteLine(FrameworkMessageLevel.Debug,
                    $"Native delegates in {assembly.FullName} have already been loaded. Skipping...");
                return;
            }

            FrameworkLog.WriteLine(FrameworkMessageLevel.Debug, $"Loading native delegates in {assembly.FullName}...");

            LoadedAssemblies.Add(assembly.FullName);

            // Loop trough every type in the assembly and load all native delegate fields it contains.
            foreach (var type in assembly.GetTypes())
            {
                // Do not process interfaces or types with generic parameters.
                if (type.IsInterface || type.ContainsGenericParameters)
                    continue;

                foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                    )
                {
                    // Skip non-static or non-delegate fields.
                    if (!field.IsStatic || !typeof (Delegate).IsAssignableFrom(field.FieldType))
                        continue;

                    var delegateType = field.FieldType;
                    var attribute = field.GetCustomAttribute<NativeAttribute>();

                    // Skip fields without Native attribute or with set values.
                    if (attribute == null || field.GetValue(null) != null)
                        continue;

                    // Load the native.
                    var parameterTypes = delegateType.GetMethod("Invoke")
                        .GetParameters()
                        .Select(p => p.ParameterType)
                        .ToArray();

                    var nativeFunction = Load(attribute.Name, attribute.Lengths, parameterTypes);
                    if (nativeFunction == null)
                    {
                        FrameworkLog.WriteLine(FrameworkMessageLevel.Warning, $"Could not load native '{attribute.Name}'");
                        continue;
                    }

                    field.SetValue(null, nativeFunction.GenerateInvoker(delegateType));
                }
            }
        }
    }
}
