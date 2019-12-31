// SampSharp
// Copyright 2019 Tim Potze
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

using SampSharp.Entities.SAMP.Components;

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Provides functionality for adding entities to the SA:MP world.
    /// </summary>
    public interface IWorldService
    {
        /// <summary>
        ///     Creates a new <see cref="Actor" />.
        /// </summary>
        /// <param name="modelId">The model identifier.</param>
        /// <param name="position">The position of the actor.</param>
        /// <param name="rotation">The rotation of the actor.</param>
        /// <returns>The actor component of the newly created entity.</returns>
        Actor CreateActor(int modelId, Vector3 position, float rotation);
    }
}