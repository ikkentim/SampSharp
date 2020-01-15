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

using SampSharp.Entities.SAMP.NativeComponents;

namespace SampSharp.Entities.SAMP.Components
{
    /// <summary>
    /// Represents a component which provides the data and functionality of a pickup.
    /// </summary>
    public sealed class Pickup : Component
    {
        
        private Pickup(int virtualWorld, int model, int spawnType, Vector3 position)
        {
            VirtualWorld = virtualWorld;
            Model = model;
            SpawnType = spawnType;
            Position = position;
        }

        /// <inheritdoc />
        protected override void OnDestroyComponent()
        {
            GetComponent<NativePickup>().DestroyPickup();
        }

        #region Properties

        /// <summary>
        /// Gets the virtual world assigned to this <see cref="Pickup" />.
        /// </summary>
        public int VirtualWorld { get; }

        /// <summary>
        /// Gets the model of this <see cref="Pickup" />.
        /// </summary>
        public int Model { get; }

        /// <summary>
        /// Gets the type of this <see cref="Pickup" />.
        /// </summary>
        public int SpawnType { get; }

        /// <summary>
        /// Gets the position of this <see cref="Pickup" />.
        /// </summary>
        public Vector3 Position { get; }

        #endregion
    }
}