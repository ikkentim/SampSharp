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

using System;
using SampSharp.EntityComponentSystem.Components;
using SampSharp.EntityComponentSystem.Entities;
using SampSharp.EntityComponentSystem.SAMP.NativeComponents;

namespace SampSharp.EntityComponentSystem.SAMP.Components
{
    /// <summary>
    /// Represents a component which provides the data and functionality of a gang zone.
    /// </summary>
    public class GangZone : Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GangZone" /> class.
        /// </summary>
        /// <param name="minX">The minimum x.</param>
        /// <param name="minY">The minimum y.</param>
        /// <param name="maxX">The maximum x.</param>
        /// <param name="maxY">The maximum y.</param>
        public GangZone(float minX, float minY, float maxX, float maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

        /// <summary>
        /// Gets the minimum x value for this <see cref="GangZone" />.
        /// </summary>
        public virtual float MinX { get; }

        /// <summary>
        /// Gets the minimum y value for this <see cref="GangZone" />.
        /// </summary>
        public virtual float MinY { get; }

        /// <summary>
        /// Gets the maximum x value for this <see cref="GangZone" />.
        /// </summary>
        public virtual float MaxX { get; }

        /// <summary>
        /// Gets the maximum y value for this <see cref="GangZone" />.
        /// </summary>
        public virtual float MaxY { get; }

        /// <summary>
        /// Gets or sets the color of this <see cref="GangZone" />.
        /// </summary>
        public virtual Color Color { get; set; }

        /// <summary>
        /// Shows this <see cref="GangZone" />.
        /// </summary>
        public virtual void Show()
        {
            GetComponent<NativeGangZone>().GangZoneShowForAll(Color);
        }

        /// <summary>
        /// Shows this <see cref="GangZone" /> to the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void Show(Entity player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            GetComponent<NativeGangZone>().GangZoneShowForPlayer(player.Id, Color);
        }

        /// <summary>
        /// Hides this <see cref="GangZone" />.
        /// </summary>
        public virtual void Hide()
        {
            GetComponent<NativeGangZone>().GangZoneHideForAll();
        }

        /// <summary>
        /// Hides this <see cref="GangZone" /> for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void Hide(Entity player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            GetComponent<NativeGangZone>().GangZoneHideForPlayer(player.Id);
        }

        /// <summary>
        /// Flashes this <see cref="GangZone" />.
        /// </summary>
        /// <param name="color">The color.</param>
        public virtual void Flash(Color color)
        {
            GetComponent<NativeGangZone>().GangZoneFlashForAll(color);
        }

        /// <summary>
        /// Flashes this <see cref="GangZone" /> for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void Flash(Entity player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            Flash(player, new Color());
        }

        /// <summary>
        /// Flashes this <see cref="GangZone" /> for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="color">The color.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void Flash(Entity player, Color color)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            GetComponent<NativeGangZone>().GangZoneFlashForPlayer(player.Id, color);
        }

        /// <summary>
        /// Stops this <see cref="GangZone" /> from flash.
        /// </summary>
        public virtual void StopFlash()
        {
            GetComponent<NativeGangZone>().GangZoneStopFlashForAll();
        }

        /// <summary>
        /// Stops this <see cref="GangZone" /> from flash for the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void StopFlash(Entity player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            GetComponent<NativeGangZone>().GangZoneStopFlashForPlayer(player.Id);
        }

        /// <inheritdoc />
        protected override void OnDestroyComponent()
        {
            GetComponent<NativeGangZone>().GangZoneDestroy();
        }
    }
}