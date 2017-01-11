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
    ///     Provides data for the <see cref="BaseMode.PlayerDisconnected" />, <see cref="BaseMode.PlayerCleanup" />,
    ///     <see cref="BasePlayer.Disconnected" /> or <see cref="BasePlayer.Cleanup" /> event.
    /// </summary>
    public class DisconnectEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DisconnectEventArgs" /> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        public DisconnectEventArgs(DisconnectReason reason)
        {
            Reason = reason;
        }

        /// <summary>
        ///     Gets the reason of the disconnection.
        /// </summary>
        public DisconnectReason Reason { get; private set; }
    }
}