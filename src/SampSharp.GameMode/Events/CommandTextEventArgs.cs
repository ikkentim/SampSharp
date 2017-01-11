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
    ///     Provides data for the <see cref="BaseMode.PlayerCommandText" /> or <see cref="BasePlayer.CommandText" /> event.
    /// </summary>
    public class CommandTextEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandTextEventArgs" /> class.
        /// </summary>
        /// <param name="text">The text sent by the player.</param>
        public CommandTextEventArgs(string text)
        {
            Text = text;
        }

        /// <summary>
        ///     Gets the text sent by the player.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        ///     Gets or sets whether this command has been handled successfully.
        /// </summary>
        public bool Success { get; set; }
    }
}