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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerInteriorChanged" /> or <see cref="BasePlayer.InteriorChanged" />
    ///     event.
    /// </summary>
    public class InteriorChangedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InteriorChangedEventArgs" /> class.
        /// </summary>
        /// <param name="newInterior">The new interior.</param>
        /// <param name="oldInterior">The old interior.</param>
        public InteriorChangedEventArgs(int newInterior, int oldInterior)
        {
            NewInterior = newInterior;
            OldInterior = oldInterior;
        }

        /// <summary>
        ///     Gets the new interior.
        /// </summary>
        public int NewInterior { get; private set; }

        /// <summary>
        ///     Gets the old interior.
        /// </summary>
        public int OldInterior { get; private set; }
    }
}