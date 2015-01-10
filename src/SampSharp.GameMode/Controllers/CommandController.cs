// SampSharp
// Copyright (C) 2015 Tim Potze
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
using System.Linq;
using System.Reflection;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all commands.
    /// </summary>
    public class CommandController : Disposable, IEventListener
    {
        /// <summary>
        ///     Registers the events this GlobalObjectController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            Console.WriteLine("[SampSharp] Loaded {0} commands.",
                RegisterCommands(Assembly.GetAssembly(gameMode.GetType())));

            gameMode.PlayerCommandText += gameMode_PlayerCommandText;
        }

        /// <summary>
        ///     Loads all commands from the given assembly.
        /// </summary>
        /// <param name="assembly">The assembly of who to load the commands from.</param>
        /// <returns>The number of commands loaded.</returns>
        public int RegisterCommands(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");

            try
            {
                int commandsCount = 0;
                //Detect commands in assembly containing the gamemode
                foreach (MethodInfo method in assembly.GetTypes().SelectMany(t => t.GetMethods())
                    .Where(m => m.IsStatic && m.GetCustomAttributes(typeof (CommandAttribute), false).Length > 0))
                {
                    new DetectedCommand(method, method.GetCustomAttribute<CommandAttribute>().IgnoreCase);
                    commandsCount++;
                }

                return commandsCount;
            }
            catch (Exception)
            {
                /*
                 * If there are no non-static types in the given assembly,
                 * in some cases this statement throws an exception.
                 * We dismiss it and assume no commands were registered.
                 */

                return 0;
            }
        }

        /// <summary>
        ///     Loads all commands from the assembly of the given type.
        /// </summary>
        /// <typeparam name="T">A type of whose assembly to load.</typeparam>
        /// <returns>The number of commands loaded.</returns>
        public int RegisterCommands<T>() where T : class
        {
            return RegisterCommands(Assembly.GetAssembly(typeof (T)));
        }

        private void gameMode_PlayerCommandText(object sender, PlayerTextEventArgs e)
        {
            string text = e.Text.Substring(1);
            GtaPlayer player = e.Player;

            foreach (Command cmd in Command.All.Where(c => c.HasPlayerPermissionForCommand(player)))
            {
                string args = text;
                if (cmd.CommandTextMatchesCommand(ref args))
                {
                    if (cmd.RunCommand(player, args))
                    {
                        e.Success = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (Command cmd in Command.All)
                {
                    cmd.Dispose();
                }
            }
        }
    }
}