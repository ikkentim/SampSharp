// SampSharp
// Copyright 2022 Tim Potze
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

using System.Reflection;
using SampSharp.GameMode.SAMP.Commands.PermissionCheckers;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands;

/// <summary>
/// Represents the default help command for a command group.
/// </summary>
public class DefaultHelpCommand : DefaultCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultHelpCommand" /> class.
    /// </summary>
    /// <param name="names">The names.</param>
    /// <param name="displayName">The display name.</param>
    /// <param name="ignoreCase">if set to <c>true</c> ignore the case of the command.</param>
    /// <param name="permissionCheckers">The permission checkers.</param>
    /// <param name="method">The method.</param>
    /// <param name="usageMessage">The usage message.</param>
    public DefaultHelpCommand(CommandPath[] names, string displayName, bool ignoreCase,
        IPermissionChecker[] permissionCheckers, MethodInfo method, string usageMessage) : base(names, displayName,
        ignoreCase, permissionCheckers, method, usageMessage)
    {
    }

    /// <inheritdoc />
    public override CommandCallableResponse CanInvoke(BasePlayer player, string commandText,
        out int matchedNameLength)
    {
        var result = base.CanInvoke(player, commandText, out matchedNameLength);

        return result == CommandCallableResponse.False
            ? result
            : CommandCallableResponse.Optional;
    }
}