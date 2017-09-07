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

namespace SampSharp.Core
{
    /// <summary>
    ///     Contains the methods of a provider of game mode routines.
    /// </summary>
    public interface IGameModeProvider : IDisposable
    {
        /// <summary>
        ///     Initializes the game mode with the specified game mode client.
        /// </summary>
        /// <param name="client">The game mode client which is loading this game mode.</param>
        void Initialize(IGameModeClient client);

        /// <summary>
        ///     A method called once every server tick.
        /// </summary>
        void Tick();
    }
}