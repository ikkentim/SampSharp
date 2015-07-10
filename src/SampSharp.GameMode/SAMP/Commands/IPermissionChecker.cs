// SampSharp
// Copyright 2015 Tim Potze
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

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     A Permission checker is used to check if a player
    ///     is allowed to use a specific command.
    ///     Every class that implement this interface
    ///     should have a parameter-less constructor or let
    ///     the compiler create one
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        ///     The message that the user should see when
        ///     he doesn't have the permission to use the command.
        ///     If null the default SA-MP message will be used.
        /// </summary>
        string Message { get; }

        /// <summary>
        ///     Called when a user tries to use a command
        ///     that require permission.
        /// </summary>
        /// <param name="player">The player that has tried to execute the command</param>
        /// <returns>Return true if the player passed as argument is allowed to use the command, False otherwise.</returns>
        bool Check(GtaPlayer player);
    }
}