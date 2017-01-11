// SampSharp
// Copyright 2017 Tim Potze
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
using SampSharp.GameMode;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.SAMP.Commands.PermissionCheckers;

namespace TestMode.Commands
{
    public class MyCommandManager : CommandsManager
    {
        public MyCommandManager(BaseMode gameMode) : base(gameMode)
        {
        }

        #region Overrides of CommandsManager

        /// <summary>
        ///     Creates a command.
        /// </summary>
        /// <param name="commandPaths">The command paths.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="ignoreCase">if set to <c>true</c> ignore the case the command.</param>
        /// <param name="permissionCheckers">The permission checkers.</param>
        /// <param name="method">The method.</param>
        /// <param name="usageMessage">The usage message.</param>
        /// <returns>The created command</returns>
        protected override ICommand CreateCommand(CommandPath[] commandPaths, string displayName, bool ignoreCase,
            IPermissionChecker[] permissionCheckers, MethodInfo method, string usageMessage)
        {
            return new MyCommand(commandPaths, displayName, ignoreCase, permissionCheckers, method, usageMessage);
        }

        #endregion
    }
}