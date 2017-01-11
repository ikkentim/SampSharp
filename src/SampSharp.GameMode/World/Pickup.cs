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
using System;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.World
{
    /// <summary>
    ///     Represents a SA-MP pickup.
    /// </summary>
    public partial class Pickup : IdentifiedPool<Pickup>, IWorldObject
    {
        /// <summary>
        ///     Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = -1;

        /// <summary>
        ///     Maximum number of pickups which can exist.
        /// </summary>
        public const int Max = 4096;

        /// <summary>
        ///     Occurs when the <see cref="OnPickUp" /> is being called.
        ///     Called when a player picks up a pickup created with <see cref="Create" />
        /// </summary>
        public event EventHandler<PlayerEventArgs> PickUp;

        /// <summary>
        ///     Creates a <see cref="Pickup" />.
        /// </summary>
        /// <param name="model">The model of the pickup.</param>
        /// <param name="type">The pickup spawn type.</param>
        /// <param name="position">The position where the pickup should be spawned.</param>
        /// <param name="virtualWorld">The virtual world ID of the pickup. Use -1 for all worlds.</param>
        /// <returns>The created pickup or null if it cannot be created.</returns>
        public static Pickup Create(int model, int type, Vector3 position, int virtualWorld = -1)
        {
            var id = PickupInternal.Instance.CreatePickup(model, type, position.X, position.Y, position.Z, virtualWorld);

            if (id == InvalidId) return null;

            var pickup = FindOrCreate(id);

            pickup.Position = position;
            pickup.VirtualWorld = virtualWorld;
            pickup.Model = model;
            pickup.SpawnType = type;
            return pickup;
        }

        /// <summary>
        ///     Creates a static <see cref="Pickup" /> in the game.
        /// </summary>
        /// <param name="model">The model of the pickup.</param>
        /// <param name="type">The pickup spawn type.</param>
        /// <param name="position">The position where the pickup should be spawned.</param>
        /// <param name="virtualWorld">The virtual world ID of the pickup. Use -1 for all worlds.</param>
        /// <returns>True if the pickup has been created, otherwise False.</returns>
        public static bool CreateStatic(int model, int type, Vector3 position, int virtualWorld = -1)
        {
            return PickupInternal.Instance.AddStaticPickup(model, type, position.X, position.Y, position.Z, virtualWorld) == 1;
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            PickupInternal.Instance.DestroyPickup(Id);
        }

        #region Events

        /// <summary>
        ///     Raises the <see cref="PickUp" /> event.
        /// </summary>
        /// <param name="e">An <see cref="PlayerEventArgs" /> that contains the event data. </param>
        public virtual void OnPickUp(PlayerEventArgs e)
        {
            PickUp?.Invoke(this, e);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Pickup(Id: {Id}, Model: {Model})";
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the virtual world assigned to this <see cref="Pickup" />.
        /// </summary>
        public int VirtualWorld { get; private set; }

        /// <summary>
        ///     Gets the model of this <see cref="Pickup" />.
        /// </summary>
        public int Model { get; private set; }

        /// <summary>
        ///     Gets the type of this <see cref="Pickup" />.
        /// </summary>
        public int SpawnType { get; private set; }

        /// <summary>
        ///     Gets the position of this <see cref="Pickup" />.
        /// </summary>
        public Vector3 Position { get; private set; }

        #endregion
    }
}