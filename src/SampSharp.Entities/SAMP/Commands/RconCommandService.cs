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
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Core;
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities.SAMP.Commands
{
    /// <summary>
    /// Provides rcon commands functionality.
    /// </summary>
    public class RconCommandService : CommandServiceBase, IRconCommandService
    {
        /// <inheritdoc />
        public RconCommandService() : base(0)
        {
        }

        /// <inheritdoc />
        public bool Invoke(IServiceProvider services, string inputText)
        {
            var result = Invoke(services, null, inputText);

            if (result.Response == InvokeResponse.InvalidArguments)
            {
                services.GetRequiredService<IGameModeClient>().Print(result.UsageMessage);
                return true;
            }

            return result.Response == InvokeResponse.Success;
        }

        /// <inheritdoc />
        protected override IEnumerable<(MethodInfo method, ICommandMethodInfo commandInfo)> ScanMethods(
            AttributeScanner scanner)
        {
            return scanner.ScanMethods<RconCommandAttribute>()
                .Select(r => (r.method, r.attribute as ICommandMethodInfo));
        }
    }
}