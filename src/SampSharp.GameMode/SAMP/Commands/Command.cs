// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

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
        public abstract bool RunCommand(Player player, string args);

        /// <summary>
        ///     Checks whether the given player has the permission to run this command.
        /// </summary>
        /// <param name="player">The player that attempts to run this command.</param>
        /// <returns>True when allowed, False otherwise.</returns>
        public virtual bool HasPlayerPermissionForCommand(Player player)
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
        /// <returns>True when successful, False otherwise.</returns>
        public virtual bool CommandTextMatchesCommand(ref string commandText)
        {
            if (commandText.StartsWith(Name))
            {
                commandText = Name == commandText ? string.Empty : commandText.Substring(Name.Length);
                return true;
            }
            return false;
        }
    }
}