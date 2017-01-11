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
    ///     Provides data for the <see cref="BaseMode.PlayerClickMap" /> or <see cref="BasePlayer.ClickMap" /> event.
    /// </summary>
    public class PositionEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PositionEventArgs" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public PositionEventArgs(Vector3 position)
        {
            Position = position;
        }

        /// <summary>
        ///     Gets the position.
        /// </summary>
        public Vector3 Position { get; private set; }
    }
}