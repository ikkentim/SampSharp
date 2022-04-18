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

using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands;

/// <summary>
///     Represents a player command.
/// </summary>
public interface ICommand
{
    /// <summary>
    ///     Determines whether this instance can be invoked by the specified player.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="commandText">The command text.</param>
    /// <param name="matchedNameLength">This value is set to the length of the name of this command which the command text was matched with.</param>
    /// <returns>A value indicating whether this instance can be invoked.</returns>
    CommandCallableResponse CanInvoke(BasePlayer player, string commandText, out int matchedNameLength);

    /// <summary>
    ///     Invokes this command.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="commandText">The command text.</param>
    /// <returns>true on success; false otherwise.</returns>
    bool Invoke(BasePlayer player, string commandText);
}