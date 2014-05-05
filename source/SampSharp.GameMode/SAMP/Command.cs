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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Represents an executable command
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        ///     Initializes a new instance of the Command class.
        /// </summary>
        /// <param name="name">The name of this command.</param>
        /// <param name="parameters">The parameters of this command.</param>
        /// <param name="run">The delegate to run when the  command is being executed.</param>
        public Command(string name, IEnumerable<Parameter> parameters, Delegate run)
        {
            //Check parameters valid
            var lastIndex = parameters.FindLastIndex(p => !p.Optional);
            var firstIndex = parameters.FindIndex(p => p.Optional);

            if (firstIndex != -1 && lastIndex != -1 && lastIndex > firstIndex)
                throw new ArgumentException("Optional parameters must be listed after required parameters.");

            //Store properties.
            Name = name;
            Parameters = parameters.ToArray();
            Run = run;

            //Register
            CommandController.Commands.Add(this);
        }

        /// <summary>
        ///     Gets the delegate runned when this command is being executed.
        /// </summary>
        public virtual Delegate Run { get; private set; }

        /// <summary>
        ///     Gets the name of this command.
        /// </summary>
        public virtual string Name { get; private set; }

        /// <summary>
        ///     Gets the parameters of this command.
        /// </summary>
        public virtual Parameter[] Parameters { get; private set; }

        /// <summary>
        ///     Execute this command.
        /// </summary>
        /// <param name="player">The player executing the command.</param>
        /// <param name="arguments">The arguments send with the command.</param>
        /// <returns>True when successful, False otherwise.</returns>
        public virtual bool Execute(Player player, IEnumerable<object> arguments)
        {
            //If there is no delegate to run.
            if (Run == null)
                return false;

            //If the delegate does not return a bool
            if (Run.GetMethodInfo().ReturnParameter.ParameterType != typeof (bool))
                return false;

            //If the delegate does not accept the same number of parameters as this command
            if (Run.GetMethodInfo().GetParameters().Count() != arguments.Count() + 1)
                return false;

            //Call the delegate
            return (bool) Run.DynamicInvoke(new[] {player}.Concat(arguments).ToArray());
        }

        /// <summary>
        ///     Chechs whether the <paramref name="player" /> can execute this command.
        /// </summary>
        /// <param name="player">The player who wants to execute this command.</param>
        /// <returns>True when allowed, False otherwise.</returns>
        public virtual bool CanExecute(Player player)
        {
            return true;
        }
    }
}