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

using System;
using System.Collections.Generic;
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
        ///     Registers the events this <see cref="GlobalObjectController" /> wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            FrameworkLog.WriteLine(FrameworkMessageLevel.Debug, "Loaded {0} commands.",
                RegisterCommands(Assembly.GetAssembly(gameMode.GetType())));

            gameMode.PlayerCommandText += gameMode_PlayerCommandText;
        }

        /// <summary>
        ///     Loads all commands from the given assembly.
        /// </summary>
        /// <param name="typeInAssembly">A type inside the assembly of who to load the commands from.</param>
        /// <returns>The number of commands loaded.</returns>
        public int RegisterCommands(Type typeInAssembly)
        {
            return RegisterCommands(Assembly.GetAssembly(typeInAssembly));
        }

        /// <summary>
        ///     Loads all commands from the given <paramref name="assembly" />.
        /// </summary>
        /// <param name="assembly">The assembly of who to load the commands from.</param>
        /// <returns>The number of commands loaded.</returns>
        public int RegisterCommands(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            try
            {
                var commandsCount = 0;
                //Detect commands in assembly containing the gamemode
                foreach (var method in assembly.GetTypes().SelectMany(t => t.GetMethods())
                    .Where(m => m.GetCustomAttributes(typeof (CommandAttribute), false).Length > 0))
                {
                    if (!method.IsStatic && !typeof (BasePlayer).IsAssignableFrom(method.DeclaringType))
                        continue;

                    new DetectedCommand(method);
                    commandsCount++;
                }

                return commandsCount;
            }
            catch (Exception)
            {
                /*
                 * If there are no non-static types in the given assembly,
                 * this statement might throw an exception.
                 * We dismiss it and assume no commands were registered.
                 */

                return 0;
            }
        }

        /// <summary>
        ///     Loads all commands from the assembly of the given type.
        /// </summary>
        /// <typeparam name="T">A type inside the assembly of who to load the commands from.</typeparam>
        /// <returns>The number of commands loaded.</returns>
        public int RegisterCommands<T>() where T : class
        {
            return RegisterCommands(typeof (T));
        }

        private void gameMode_PlayerCommandText(object sender, CommandTextEventArgs e)
        {
            var player = sender as BasePlayer;
            if (player == null) return;

            var text = e.Text.Substring(1);

            var candidates = new List<Tuple<Tuple<int, string>, Command>>();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var cmd in Command.All)
            {
                var args = text;
                var count = cmd.CommandTextMatchesCommand(ref args);
                if (count > 0)
                {
                    candidates.Add(new Tuple<Tuple<int, string>, Command>(new Tuple<int, string>(count, args), cmd));
                }
            }

            var orderedCandidates = candidates.OrderByDescending(c => c.Item1.Item1);

            /*
             * 1) Find every command for which the arguments are valid.
             * 2) Try to run each found command until one has run successfully.
             */
            var firstSuitable =
                orderedCandidates.FirstOrDefault(
                    candidate => candidate.Item2.AreArgumentsValid(candidate.Item1.Item2) &&
                                 candidate.Item2.RunCommand(player, candidate.Item1.Item2));

            if (firstSuitable != null)
            {
                e.Success = true;
                return;
            }

            /*
             * If no command ran successfully, run the first command anyways.
             */
            var command = orderedCandidates.FirstOrDefault();

            if (command != null &&
                command.Item2.RunCommand(player, command.Item1.Item2))
                e.Success = true;
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var cmd in Command.All)
                {
                    cmd.Dispose();
                }
            }
        }
    }
}