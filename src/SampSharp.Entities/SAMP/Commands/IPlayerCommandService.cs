// SampSharp
// Copyright 2022 Tim Potze
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

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Provides the functionality for invoking player commands.
/// </summary>
public interface IPlayerCommandService
{
    /// <summary>
    /// Invokes a player command using the specified <paramref name="inputText" /> for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="services">A service provider.</param>
    /// <param name="player">The player for which the command is invoked.</param>
    /// <param name="inputText">The input text to be parsed.</param>
    /// <returns><c>true</c> if the command was handled; otherwise <c>false</c>.</returns>
    bool Invoke(IServiceProvider services, EntityId player, string inputText);
}