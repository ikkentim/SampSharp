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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerKeyStateChanged" /> or <see cref="BasePlayer.KeyStateChanged" />
    ///     event.
    /// </summary>
    public class KeyStateChangedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="KeyStateChangedEventArgs" /> class.
        /// </summary>
        /// <param name="newKeys">The new keys.</param>
        /// <param name="oldKeys">The old keys.</param>
        public KeyStateChangedEventArgs(Keys newKeys, Keys oldKeys)
        {
            NewKeys = newKeys;
            OldKeys = oldKeys;
        }

        /// <summary>
        ///     Gets the new keys.
        /// </summary>
        /// <value>
        ///     The new keys.
        /// </value>
        public Keys NewKeys { get; private set; }

        /// <summary>
        ///     Gets the old keys.
        /// </summary>
        /// <value>
        ///     The old keys.
        /// </value>
        public Keys OldKeys { get; private set; }
    }
}