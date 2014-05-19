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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Helpers;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all commands.
    /// </summary>
    public class CommandController : IEventListener
    {
        private readonly WordParameterAttribute _nameFilter = new WordParameterAttribute(string.Empty);
        private MethodInfo[] _commands;

        /// <summary>
        ///     Initalizes a new instance of the CommandController class.
        /// </summary>
        public CommandController()
        {
            UsageFormat =
                (name, parameters) =>
                    string.Format("Usage: /{0}{1}{2}", name, parameters.Any() ? ": " : string.Empty,
                        string.Join(" ", parameters.Select(
                            p => p.Optional
                                ? string.Format("({0})", p.DisplayName)
                                : string.Format("[{0}]", p.DisplayName)
                            ))
                        );
        }

        /// <summary>
        ///     Gets or sets the usage message send when a wrongly formatted command is being processed.
        /// </summary>
        public Func<string, ParameterAttribute[], string> UsageFormat { get; set; }

        /// <summary>
        ///     Registers the events this GlobalObjectController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            //Detect commands if assembly containing the gamemode
            _commands = gameMode.GetType().Assembly.GetTypes().SelectMany(t => t.GetMethods())
                .Where(m => m.IsStatic && m.GetCustomAttributes(typeof (CommandAttribute), false).Length > 0)
                .ToArray();

            gameMode.PlayerCommandText += gameMode_PlayerCommandText;
        }

        private void gameMode_PlayerCommandText(object sender, PlayerTextEventArgs e)
        {
            //Strip / and trim spaces.
            string commandText = e.Text.Substring(1).Trim();

            //Filter name of the command to be executed.
            object name;
            _nameFilter.Check(ref commandText, out name);
            var commandName = name as string;

            Player player = e.Player;

            foreach (MethodInfo command in _commands)
            {
                //Get CommandAttribute from method
                var attribute = command.GetCustomAttribute<CommandAttribute>();
                if (attribute == null)
                    continue;

                //If name doesn't match, continue
                if (attribute.Name != commandName)
                    continue;

                IEnumerable<string> methodParameters = command.GetParameters().Select(q => q.Name);
                var arguments = new Dictionary<string, object>();
                IOrderedEnumerable<ParameterAttribute> parameters =
                    command.GetCustomAttributes<ParameterAttribute>()
                        .OrderBy(p => command.GetParameters().Select(q => q.Name).IndexOf(p.Name));

                //Loop trough all parameters.
                bool pass = true;

                foreach (ParameterAttribute parameter in parameters)
                {
                    //Trim commandtext.
                    commandText = commandText.Trim();

                    //Check if parameter is correctly formatted.
                    object argument;

                    if (commandText.Length == 0 && parameter.Optional)
                    {
                        //Optional parameter not given; set null
                        arguments[parameter.Name] = null;
                        continue;
                    }

                    if (commandText.Length == 0 || !parameter.Check(ref commandText, out argument))
                    {
                        pass = false;
                        break;
                    }

                    //Add argument to list.
                    arguments[parameter.Name] = argument;
                }


                //If tests didn't pass.
                if (!pass)
                {
                    //If we want to show the usage.
                    if (UsageFormat != null)
                    {
                        player.SendClientMessage(Color.White, UsageFormat(commandName, parameters.ToArray()));

                        //Show usage and stop.
                        e.Success = true;
                        return;
                    }

                    //Continue to next command.
                    continue;
                }

                //Assign player to first parameter
                arguments[methodParameters.First()] = player;

                //Check parameter counts match.
                if (methodParameters.Count() != parameters.Count() + 1 ||
                    parameters.Any(p => !methodParameters.Contains(p.Name)))
                {
                    continue;
                }

                //Run the command.
                e.Success =
                    (bool)
                        command.Invoke(null,
                            arguments.OrderBy(pair => methodParameters.IndexOf(pair.Key))
                                .Select(pair => pair.Value)
                                .ToArray());
                return;
            }
        }
    }
}