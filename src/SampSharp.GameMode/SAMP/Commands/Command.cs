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

using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Represents a player-command.
    /// </summary>
    public abstract class Command : Pool<Command>
    {
        /// <summary>
        ///     Gets the name of the command (/name).
        /// </summary>
        public virtual string Name { get; protected set; }

        /// <summary>
        ///     Gets whether this command is case-sensitive.
        /// </summary>
        public virtual bool IgnoreCase { get; protected set; }

        /// <summary>
        ///     Runs the command.
        /// </summary>
        /// <param name="player">The player running the command.</param>
        /// <param name="args">The arguments the player entered.</param>
        /// <returns>True when the command has been executed, False otherwise.</returns>
        public abstract bool RunCommand(GtaPlayer player, string args);

        /// <summary>
        ///     Checks whether the given player has the permission to run this command.
        /// </summary>
        /// <param name="player">The player that attempts to run this command.</param>
        /// <returns>True when allowed, False otherwise.</returns>
        public virtual bool HasPlayerPermissionForCommand(GtaPlayer player)
        {
            return true;
        }

        /// <summary>
        ///     Checks whether the provided <paramref name="commandText" /> starts with the right characters to run this command.
        /// </summary>
        /// <param name="commandText">
        ///     The command the player entered. When the command returns True, the referenced string will
        ///     only contain the command arguments.
        /// </param>
        /// <returns>0 if the command text does not match, otherwise the number of matching words.</returns>
        public virtual int CommandTextMatchesCommand(ref string commandText)
        {
            if (commandText == Name || commandText.StartsWith(Name + " "))
            {
                commandText = Name == commandText ? string.Empty : commandText.Substring(Name.Length);
                return 1;
            }
            return 0;
        }

        /// <summary>
        ///     Checks whether the <paramref name="commandText" /> contains all required arguments.
        /// </summary>
        /// <param name="commandText">The text to check.</param>
        /// <returns>True if all required arguments are present; False otherwise.</returns>
        public abstract bool AreArgumentsValid(string commandText);
    }
}