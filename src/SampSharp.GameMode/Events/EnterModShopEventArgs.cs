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
    ///     Provides data for the <see cref="BaseMode.PlayerEnterExitModShop" /> or <see cref="BasePlayer.EnterExitModShop" />
    ///     event.
    /// </summary>
    public class EnterModShopEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EnterModShopEventArgs" /> class.
        /// </summary>
        /// <param name="enterExit">The enter exit value.</param>
        /// <param name="interiorid">The interiorid.</param>
        public EnterModShopEventArgs(EnterExit enterExit, int interiorid)
        {
            EnterExit = enterExit;
            InteriorId = interiorid;
        }

        /// <summary>
        ///     Gets a value indicating whether the player is entering or exiting.
        /// </summary>
        public EnterExit EnterExit { get; private set; }

        /// <summary>
        ///     Gets the interior identifier of the mod shop.
        /// </summary>
        public int InteriorId { get; private set; }
    }
}