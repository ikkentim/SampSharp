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

using System.Collections.Generic;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Provides the functionality for a <see cref="Command" /> to be runnable.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        ///     Gets the name of this command.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the parameters of this command.
        /// </summary>
        Parameter[] Parameters { get; }

        /// <summary>
        ///     Execute this command.
        /// </summary>
        /// <param name="player">The player executing the command.</param>
        /// <param name="arguments">The arguments send with the command.</param>
        /// <returns>True when successful, False otherwise.</returns>
        bool Execute(Player player, IEnumerable<object> arguments);

        /// <summary>
        ///     Chechs whether the <paramref name="player" /> can execute this command.
        /// </summary>
        /// <param name="player">The player who wants to execute this command.</param>
        /// <returns>True when allowed, False otherwise.</returns>
        bool CanExecute(Player player);
    }
}