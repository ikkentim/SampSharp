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

using System;

namespace SampSharp.GameMode.SAMP.Commands
{
    /// <summary>
    ///     Indicates that a method is a player-comammand and specifies information about the command.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommandAttribute" /> class.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        public CommandAttribute(string name)
        {
            Name = name;
            IgnoreCase = true;
        }

        /// <summary>
        ///     Gets or sets the name of this Command.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets whether this Command is case-sensitive.
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        ///     Gets or sets an alias of this Command.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///     Gets or sets a shortcut of this Command.
        /// </summary>
        /// <remarks>
        ///     A shortcut is the same as an alias, but without the commandgroup in front of it.
        /// </remarks>
        public string Shortcut { get; set; }

        /// <summary>
        ///     Gets or sets the method run to check whether a player has the permissions the run the command.
        /// </summary>
        public string PermissionCheckMethod { get; set; }
    }
}