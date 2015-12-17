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
using System.Reflection;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.SAMP.Commands.Parameters;
using SampSharp.GameMode.World;

namespace TestMode.Commands
{
    public class MyCommand : DefaultCommand
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefaultCommand" /> class.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="ignoreCase">if set to <c>true</c> ignore the case of the command.</param>
        /// <param name="permissionCheckers">The permission checkers.</param>
        /// <param name="method">The method.</param>
        /// <param name="usageMessage">The usage message.</param>
        public MyCommand(CommandPath[] names, string displayName, bool ignoreCase,
            IPermissionChecker[] permissionCheckers, MethodInfo method, string usageMessage)
            : base(names, displayName, ignoreCase, permissionCheckers, method, usageMessage)
        {
        }

        #region Overrides of DefaultCommand

        /// <summary>
        ///     Gets the type of the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        /// <returns>The type of the parameter.</returns>
        protected override ICommandParameterType GetParameterType(ParameterInfo parameter, int index, int count)
        {
            // use default parameter type detection.
            var type = base.GetParameterType(parameter, index, count);

            if (type != null)
                return type;

            // if no parameter type was found check if it's of any type we recognize.
            if (parameter.ParameterType == typeof (bool))
            {
                // TODO: detected this type to be of type `bool`. 
                // TODO: Return an implementation of ICommandParameterType which processes booleans.
            }

            // Unrecognized type. Return null.
            return null;
        }

        /// <summary>
        ///     Sends the permission denied message for the specified permission checker.
        /// </summary>
        /// <param name="permissionChecker">The permission checker.</param>
        /// <param name="player">The player.</param>
        /// <returns>true on success; false otherwise.</returns>
        protected override bool SendPermissionDeniedMessage(IPermissionChecker permissionChecker, BasePlayer player)
        {
            if (permissionChecker == null) throw new ArgumentNullException(nameof(permissionChecker));
            if (player == null) throw new ArgumentNullException(nameof(player));

            if (permissionChecker.Message == null)
                return false;

            // Send permission denied message in red instead of white.
            player.SendClientMessage(Color.Red, permissionChecker.Message);
            return true;
        }

        #endregion
    }
}