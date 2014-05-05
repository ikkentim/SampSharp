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
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all commands.
    /// </summary>
    public class CommandController : IEventListener
    {
        private static List<ICommand> _commands = new List<ICommand>();
        private readonly WordParameter _nameFilter = new WordParameter();

        /// <summary>
        ///     Initalizes a new instance of the CommandController class.
        /// </summary>
        public CommandController()
        {
            UsageFormat =
                c =>
                    string.Format("Usage: /{0}{1}{2}", c.Name, c.Parameters.Any() ? ": " : string.Empty,
                        string.Join(" ", c.Parameters.Select(
                            p => p.Optional
                                ? string.Format("({0})", p.Name)
                                : string.Format("[{0}]", p.Name)
                            ))
                        );
        }

        /// <summary>
        ///     Gets or sets a collection of commands to be processed by this controller.
        /// </summary>
        public static List<ICommand> Commands
        {
            get { return _commands; }
            set { _commands = value; }
        }

        /// <summary>
        ///     Gets or sets the usage message send when a wrongly formatted command is being processed.
        /// </summary>
        public virtual Func<ICommand, string> UsageFormat { get; set; }

        /// <summary>
        ///     Registers the events this GlobalObjectController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerCommandText += gameMode_PlayerCommandText;
        }

        private void gameMode_PlayerCommandText(object sender, Events.PlayerTextEventArgs e)
        {
            //Strip / and trim spaces.
            string commandText = e.Text.Substring(1).Trim();

            //Filter name of the command to be executed.
            object name;
            _nameFilter.Check(ref commandText, out name);
            string commandName = name as string;

            var player = e.Player;

            //Loop trough all command with the given name.
            foreach (var command in Commands.Where(c => c.Name == commandName))
            {
                //Construct a list of parameters.
                var arguments = new List<object>();

                if (!command.CanExecute(player))
                    continue;

                //Loop trough all parameters.
                bool pass = true;
                foreach (var parameter in command.Parameters)
                {
                    //Trim commandtext.
                    commandText = commandText.Trim();

                    //Check if parameter is correctly formatted.
                    object argument;

                    if (commandText.Length == 0 && parameter.Optional)
                    {
                        //Optional parameter not given; add null
                        arguments.Add(null);
                        continue;
                    }
                    if (commandText.Length == 0 || !parameter.Check(ref commandText, out argument))
                    {
                        pass = false;
                        break;
                    }

                    //Add argument to list.
                    arguments.Add(argument);
                }

                //If tests didn't pass.
                if (!pass)
                {
                    //If we want to show the usage.
                    if (UsageFormat != null)
                    {
                        player.SendClientMessage(Color.White, UsageFormat(command));

                        //Show usage and stop.
                        e.Success = true;
                        return;
                    }

                    //Continue to next command.
                    continue;
                }

                //Run the command.
                if (command.Execute(e.Player, arguments))
                {
                    e.Success = true;
                    return;
                }

                //Success; Don't continue to next commands.
                return;
            }
        }
    }
}