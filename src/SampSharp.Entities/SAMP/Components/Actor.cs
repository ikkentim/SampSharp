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

using System;
using SampSharp.Entities.SAMP.NativeComponents;

namespace SampSharp.Entities.SAMP.Components
{
    /// <summary>
    /// Represents a component which provides the data and functionality of an actor.
    /// </summary>
    public class Actor : Component
    {
        /// <summary>
        /// Gets the facing angle of this actor.
        /// </summary>
        public float FacingAngle
        {
            get
            {
                GetComponent<NativeActor>().GetActorFacingAngle(out var angle);
                return angle;
            }
            set => GetComponent<NativeActor>().SetActorFacingAngle(value);
        }

        /// <summary>
        /// Gets the health of this actor.
        /// </summary>
        public float Health
        {
            get
            {
                GetComponent<NativeActor>().GetActorHealth(out var health);
                return health;
            }
            set => GetComponent<NativeActor>().SetActorHealth(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this actor is invulnerable.
        /// </summary>
        public bool IsInvulnerable
        {
            get => GetComponent<NativeActor>().IsActorInvulnerable();
            set => GetComponent<NativeActor>().SetActorInvulnerable(value);
        }

        /// <summary>
        /// Gets or sets the virtual world of this actor.
        /// </summary>
        public int VirtualWorld
        {
            get => GetComponent<NativeActor>().GetActorVirtualWorld();
            set => GetComponent<NativeActor>().SetActorVirtualWorld(value);
        }

        /// <summary>
        /// Gets the position of this actor.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                GetComponent<NativeActor>().GetActorPos(out var x, out var y, out var z);
                return new Vector3(x, y, z);
            }
            set => GetComponent<NativeActor>().SetActorPos(value.X, value.Y, value.Z);
        }

        /// <summary>
        /// Determines whether this actor is streamed in for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>True if streamed in; False otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public bool IsStreamedIn(Entity player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            return GetComponent<NativeActor>().IsActorStreamedIn(player.Id);
        }

        /// <summary>
        /// Applies the specified animation to this actor.
        /// </summary>
        /// <param name="library">The animation library from which to apply an animation.</param>
        /// <param name="name">The name of the animation to apply, within the specified library.</param>
        /// <param name="fDelta">The speed to play the animation.</param>
        /// <param name="loop">if set to <c>true</c> the animation will loop.</param>
        /// <param name="lockX">if set to <c>true</c> allow this Actor to move it's x-coordinate.</param>
        /// <param name="lockY">if set to <c>true</c> allow this Actor to move it's y-coordinate.</param>
        /// <param name="freeze">if set to <c>true</c> freeze this Actor at the end of the animation.</param>
        /// <param name="time">The amount of time (in milliseconds) to play the animation.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if <paramref name="library" /> or <paramref name="name" /> is
        /// null.
        /// </exception>
        public void ApplyAnimation(string library, string name, float fDelta, bool loop, bool lockX, bool lockY,
            bool freeze, int time)
        {
            if (library == null) throw new ArgumentNullException(nameof(library));
            if (name == null) throw new ArgumentNullException(nameof(name));
            GetComponent<NativeActor>().ApplyActorAnimation(library, name, fDelta, loop, lockX, lockY, freeze, time);
        }

        /// <summary>
        /// Clear any animations applied to this actor.
        /// </summary>
        public void ClearAnimations()
        {
            GetComponent<NativeActor>().ClearActorAnimations();
        }

        /// <inheritdoc />
        protected override void OnDestroyComponent()
        {
            GetComponent<NativeActor>().DestroyActor();
        }
    }
}