// SampSharp
// Copyright 2017 Tim Potze
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
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.Core.Natives.NativeObjects.NativeHandles;

namespace SampSharp.Core.Natives
{
    /// <summary>
    ///     Represents a native function loader
    /// </summary>
    public class NativeLoader : INativeLoader
    {
        private readonly IGameModeClient _gameModeClient;
        private readonly Dictionary<string, int> _handles = new Dictionary<string, int>();
        private readonly Dictionary<int, INative> _natives = new Dictionary<int, INative>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeLoader" /> class.
        /// </summary>
        /// <param name="gameModeClient">The game mode client.</param>
        public NativeLoader(IGameModeClient gameModeClient)
        {
            _gameModeClient = gameModeClient ?? throw new ArgumentNullException(nameof(gameModeClient));
            ProxyFactory = new NativeHandleBasedNativeObjectProxyFactory(gameModeClient, this);
        }

        #region Implementation of INativeLoader

        /// <inheritdoc />
        public INativeObjectProxyFactory ProxyFactory { get; }

        /// <summary>
        ///     Loads a native with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sizes">The references to the parameter which contains the size of array parameters.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>The loaded native.</returns>
        public INative Load(string name, uint[] sizes, Type[] parameterTypes)
        {
            if (parameterTypes == null) throw new ArgumentNullException(nameof(parameterTypes));
            var parameters = NativeParameterInfo.ForTypes(parameterTypes, sizes);

            return Load(name, parameters);
        }

        /// <summary>
        ///     Loads a native with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The loaded native.</returns>
        public INative Load(string name, params NativeParameterInfo[] parameters)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            if (!_handles.TryGetValue(name, out var handle))
                handle = _handles[name] = _gameModeClient.GetNativeHandle(name);

            if (handle < 0)
                return null;

            return _natives[handle] = new Native(_gameModeClient, name, handle, parameters);
        }

        /// <summary>
        ///     Gets the native with the specified handle.
        /// </summary>
        /// <param name="handle">The handle of the native.</param>
        /// <returns>The native.</returns>
        public INative Get(int handle)
        {
            _natives.TryGetValue(handle, out var native);
            return native;
        }

        /// <summary>
        ///     Checks whether a native with the specified <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True if a native with the specified name exists; False otherwise.</returns>
        public bool Exists(string name)
        {
            if (!_handles.TryGetValue(name, out var handle))
                handle = _handles[name] = _gameModeClient.GetNativeHandle(name);

            return handle != -1;
        }

        #endregion
    }
}