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

namespace SampSharp.Core;

/// <summary>Contains the methods of a runnable game mode.</summary>
public interface IGameModeRunner
{
    /// <summary>Runs the game mode of this runner.</summary>
    /// <returns><c>true</c> on success; <c>false</c> otherwise.</returns>
    bool Run();

    /// <summary>Gets the client of this game mode runner.</summary>
    IGameModeClient Client { get; }
}