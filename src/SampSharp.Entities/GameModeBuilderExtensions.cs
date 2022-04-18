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

using SampSharp.Core;

namespace SampSharp.Entities;

/// <summary>
/// Provides extended functionality for configuring a <see cref="GameModeBuilder" /> instance.
/// </summary>
public static class GameModeBuilderExtensions
{
    /// <summary>
    /// Enables the EntityComponentSystem with the specified <typeparamref name="TStartup" /> type as the startup
    /// configuration.
    /// </summary>
    /// <typeparam name="TStartup">The type of the startup configuration.</typeparam>
    /// <param name="gameModeBuilder">The game mode builder.</param>
    /// <returns>The game mode builder.</returns>
    public static GameModeBuilder UseEcs<TStartup>(this GameModeBuilder gameModeBuilder)
        where TStartup : class, IStartup, new()
    {
        return gameModeBuilder.Use(new EcsManager(new TStartup()));
    }
}