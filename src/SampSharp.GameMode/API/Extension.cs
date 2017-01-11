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
using SampSharp.GameMode.Controllers;

namespace SampSharp.GameMode.API
{
    /// <summary>
    ///     Contains methods for registering SampSharp extensions and represents a simple base class for extensions.
    /// </summary>
    public abstract class Extension : IExtension
    {
        /// <summary>
        ///     Registers an extension to the plugin and loads its natives.
        /// </summary>
        /// <param name="extension">The extension to register.</param>
        /// <returns>
        ///     True on success, False otherwise.
        /// </returns>
        public static bool Register<T>(T extension) where T : IExtension
        {
            return InteropProvider.RegisterExtension(extension);
        }

        #region Implementation of IExtension

        /// <summary>
        ///     Loads services provided by this extensions.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        public virtual void LoadServices(BaseMode gameMode)
        {
        }

        /// <summary>
        ///     Loads controllers provided by this extensions.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        /// <param name="controllerCollection">The controller collection.</param>
        public virtual void LoadControllers(BaseMode gameMode, ControllerCollection controllerCollection)
        {
            gameMode.AutoloadControllersForAssembly(GetType().Assembly);
        }

        /// <summary>
        ///     Performs post-load actions.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        public virtual void PostLoad(BaseMode gameMode)
        {
        }

        #endregion
    }
}