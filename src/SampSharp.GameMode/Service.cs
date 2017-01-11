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
namespace SampSharp.GameMode
{
    /// <summary>
    ///     Defines the base implementation for the <see cref="IService" />.
    /// </summary>
    public abstract class Service : IService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Service" /> class.
        /// </summary>
        /// <param name="gameMode">The game mode.</param>
        protected Service(BaseMode gameMode)
        {
            GameMode = gameMode;
        }

        /// <summary>
        ///     Gets the game mode.
        /// </summary>
        public virtual BaseMode GameMode { get; }
    }
}