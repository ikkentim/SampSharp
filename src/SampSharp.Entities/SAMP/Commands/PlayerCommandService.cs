// SampSharp
// Copyright 2020 Tim Potze
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
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities.SAMP.Commands
{
    /// <summary>
    /// Provides player commands functionality.
    /// </summary>
    public class PlayerCommandService : CommandServiceBase, IPlayerCommandService
    {
        private readonly IEntityManager _entityManager;

        /// <inheritdoc />
        public PlayerCommandService(IEntityManager entityManager) : base(entityManager, 1)
        {
            _entityManager = entityManager;
        }

        /// <inheritdoc />
        protected override bool TryCollectParameters(ParameterInfo[] parameters, int prefixParameters, out CommandParameterInfo[] result)
        {
            if (!base.TryCollectParameters(parameters, prefixParameters, out result))
                return false;

            // Ensure player is first parameter
            var type = parameters[0].ParameterType;
            return type == typeof(EntityId) || typeof(Component).IsAssignableFrom(type);
        }

        /// <inheritdoc />
        public bool Invoke(IServiceProvider services, EntityId player, string inputText)
        {
            if (!player.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            var result = Invoke(services, new object[] {player}, inputText);

            if (result.Response != InvokeResponse.InvalidArguments)
                return result.Response == InvokeResponse.Success;

            _entityManager.GetComponent<Player>(player)
                ?.SendClientMessage(result.UsageMessage);
            return true;
        }

        /// <inheritdoc />
        protected override bool ValidateInputText(ref string input)
        {
            if (!base.ValidateInputText(ref input))
                return false;

            // Player commands must start with a slash.
            if (!input.StartsWith("/") || input.Length <= 1)
                return false;

            input = input.Substring(1);

            return true;
        }

        /// <inheritdoc />
        protected override IEnumerable<(MethodInfo method, ICommandMethodInfo commandInfo)> ScanMethods(
            AssemblyScanner scanner)
        {
            return scanner.ScanMethods<PlayerCommandAttribute>()
                .Select(r => (r.method, r.attribute as ICommandMethodInfo));
        }

        private static string CommandText(CommandInfo command)
        {
            return command.Parameters.Length == 0
                ? $"Usage: /{command.Name}"
                : $"Usage: /{command.Name} " + string.Join(" ",
                      command.Parameters.Select(arg => arg.IsRequired ? $"[{arg.Name}]" : $"<{arg.Name}>"));
        }

        /// <inheritdoc />
        protected override string GetUsageMessage(CommandInfo[] commands)
        {
            return commands.Length == 1
                ? CommandText(commands[0])
                : $"Usage: {string.Join(" -or- ", commands.Select(CommandText))}";
        }
    }
}