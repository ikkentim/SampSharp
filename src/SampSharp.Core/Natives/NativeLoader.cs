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
using SampSharp.Core.Natives.NativeObjects.FastNatives;

namespace SampSharp.Core.Natives
{
    /// <summary>
    ///     Represents a native function loader
    /// </summary>
    internal class NativeLoader : INativeLoader
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NativeLoader" /> class.
        /// </summary>
        /// <param name="gameModeClient">The game mode client.</param>
        public NativeLoader(IGameModeClient gameModeClient)
        {
            if (gameModeClient == null)
                throw new ArgumentNullException(nameof(gameModeClient));
            
            ProxyFactory = new FastNativeBasedNativeObjectProxyFactory(gameModeClient);
        }

        #region Implementation of INativeLoader

        /// <inheritdoc />
        public INativeObjectProxyFactory ProxyFactory { get; }
        
        /// <summary>
        ///     Checks whether a native with the specified <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True if a native with the specified name exists; False otherwise.</returns>
        public bool Exists(string name)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}