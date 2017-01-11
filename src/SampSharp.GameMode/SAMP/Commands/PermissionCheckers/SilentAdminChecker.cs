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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP.Commands.PermissionCheckers
{
    /// <summary>
    ///     Represents a permission checker for admins without a permission denied message.
    /// </summary>
    public class SilentAdminChecker : IPermissionChecker
    {
        #region Implementation of IPermissionChecker

        /// <summary>
        ///     Gets the message displayed when the player is denied permission.
        /// </summary>
        public string Message => null;

        /// <summary>
        ///     Checks the permission for the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>true if allowed; false if denied.</returns>
        public bool Check(BasePlayer player) => player.IsAdmin;

        #endregion
    }
}