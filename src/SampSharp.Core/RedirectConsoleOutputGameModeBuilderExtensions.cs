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

namespace SampSharp.Core;

/// <summary>
/// Provides extended functionality for <see cref="GameModeBuilder" /> for configuring console output redirection.
/// </summary>
public static class RedirectConsoleOutputGameModeBuilderExtensions
{
    /// <summary>
    ///     Redirect the console output to the server.
    /// </summary>
    /// <param name="builder">The game mode builder.</param>
    /// <returns>The updated game mode configuration builder.</returns>
    public static GameModeBuilder RedirectConsoleOutput(this GameModeBuilder builder) =>
        builder.AddBuildAction(next =>
        {
            var runner = next();
            Console.SetOut(new ServerLogWriter(runner.Client));
            return runner;
        });
}