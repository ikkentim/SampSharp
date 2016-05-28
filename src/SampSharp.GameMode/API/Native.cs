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
    }
}
