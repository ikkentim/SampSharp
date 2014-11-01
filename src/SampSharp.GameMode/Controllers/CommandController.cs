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
using System.Linq;
using System.Reflection;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all commands.
    /// </summary>
    public class CommandController : IEventListener
    {
        /// <summary>
        ///     Registers the events this GlobalObjectController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            try
            {
                //Detect commands in assembly containing the gamemode
                foreach (MethodInfo method in gameMode.GetType().Assembly.GetTypes().SelectMany(t => t.GetMethods())
                    .Where(m => m.IsStatic && m.GetCustomAttributes(typeof (CommandAttribute), false).Length > 0))
                {
                    new DetectedCommand(method, method.GetCustomAttribute<CommandAttribute>().IgnoreCase);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("[SampSharp] No commands were loaded.");
                /*
                 * If no commands where found this statement throws an exception.
                 * We dismiss it and assume no commands were registered.
                 */
            }


            gameMode.PlayerCommandText += gameMode_PlayerCommandText;
        }

        private void gameMode_PlayerCommandText(object sender, PlayerTextEventArgs e)
        {
            string text = e.Text.Substring(1);
            Player player = e.Player;

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
    }
}