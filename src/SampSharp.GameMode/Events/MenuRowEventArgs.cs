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
using SampSharp.GameMode.Display;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerSelectedMenuRow" />, <see cref="BasePlayer.SelectedMenuRow" /> or
    ///     <see cref="Menu.Response" /> event.
    /// </summary>
    public class MenuRowEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MenuRowEventArgs" /> class.
        /// </summary>
        /// <param name="row">The row.</param>
        public MenuRowEventArgs(int row)
        {
            Row = row;
        }

        /// <summary>
        ///     Gets the row.
        /// </summary>
        public int Row { get; private set; }
    }
}