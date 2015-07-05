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
using SampSharp.GameMode.API;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.World
{
    /// <summary>
    ///     Represents a SA-MP actor.
    /// </summary>
    public class Actor : IdentifiedPool<Actor>, IIdentifiable, IWorldObject
    {
        /// <summary>
        ///     Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = 0xFFFF;

        /// <summary>
        ///     Maximum number of actors which can exist.
        /// </summary>
        public const int Max = 1000;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Actor" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Actor(int id)
        {
            Id = id;
        }

        /// <summary>
        ///     Gets the facing angle of this <see cref="Actor" />.
        /// </summary>
        public float FacingAngle
        {
            get
            {
                float angle;

                AssertNotDisposed();

                GetActorFacingAngle(Id, out angle);
                return angle;
            }
            set
            {
                AssertNotDisposed();
                SetActorFacingAngle(Id, value);
            }
        }

        /// <summary>
        ///     Gets the health of this <see cref="Actor" />.
        /// </summary>
        public float Health
        {
            get
            {
                float health;

                AssertNotDisposed();

                GetActorHealth(Id, out health);
                return health;
            }
            set
            {
                AssertNotDisposed();
                SetActorHealth(Id, value);
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="Actor" /> is invulnerable.
        /// </summary>
        public bool IsInvulnerable
        {
            get
            {
                AssertNotDisposed();
                return IsActorInvulnerable(Id);
            }
            set
            {
                AssertNotDisposed();
                SetActorInvulnerable(Id, value);
            }
        }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="Actor" /> is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                AssertNotDisposed();
                return IsValidActor(Id);
            }
        }

        /// <summary>
        ///     Gets or sets the virtual world of this <see cref="Actor" />.
        /// </summary>
        public int VirtualWorld
        {
            get
            {
                AssertNotDisposed();
                return GetActorVirtualWorld(Id);
            }
            set
            {
                AssertNotDisposed();
                SetActorVirtualWorld(Id, value);
            }
        }

        /// <summary>
        ///     Gets the size of the actors pool.
        /// </summary>
        public static int PoolSize
        {
            get { return Native.GetActorPoolSize(); }
        }

        #region Implementation of IIdentifiable

        /// <summary>
        ///     Gets the Identity of this <see cref="Actor" />.
        /// </summary>
        public int Id { get; private set; }

        #endregion

        #region Implementation of IWorldObject

        /// <summary>
        ///     Gets the position of this <see cref="Actor" />.
        /// </summary>
        public Vector3 Position
        {
            get
            {
                float x, y, z;

                AssertNotDisposed();

                GetActorPos(Id, out x, out y, out z);
                return new Vector3(x, y, z);
            }
            set
            {
                AssertNotDisposed();
                SetActorPos(Id, value.X, value.Y, value.Z);
            }
        }

        #endregion

        /// <summary>
        ///     Occurs when this <see cref="Actor" /> is being streamed in for a player.
        /// </summary>
        public event EventHandler<PlayerEventArgs> StreamIn;

        /// <summary>
        ///     Occurs when this <see cref="Actor" /> is being streamed out for a player.
        /// </summary>
        public event EventHandler<PlayerEventArgs> StreamOut;

        /// <summary>
        ///     Occurs when a player harms this <see cref="Actor" />.
        /// </summary>
        public event EventHandler<DamageEventArgs> PlayerGiveDamage;

        #region Native functions

        private delegate bool ApplyActorAnimationImpl(int actorid, string animlib, string animname, float fDelta,
    bool loop, bool lockx, bool locky, bool freeze, int time);

        private delegate bool ClearActorAnimationsImpl(int actorid);

        private delegate int CreateActorImpl(int modelid, float x, float y, float z, float rotattion);

        private delegate bool DestroyActorImpl(int actorid);

        private delegate bool GetActorFacingAngleImpl(int actorid, out float angle);

        private delegate bool GetActorHealthImpl(int actorid, out float health);

        private delegate bool GetActorPosImpl(int actorid, out float x, out float y, out float z);

        private delegate int GetActorVirtualWorldImpl(int actorid);

        private delegate bool IsActorInvulnerableImpl(int actorid);

        private delegate bool IsActorStreamedInImpl(int actorid, int forplayerid);

        private delegate bool IsValidActorImpl(int actorid);

        private delegate bool SetActorFacingAngleImpl(int actorid, float angle);

        private delegate bool SetActorHealthImpl(int actorid, float health);

        private delegate bool SetActorInvulnerableImpl(int actorid, bool invulnerable = true);

        private delegate bool SetActorPosImpl(int actorid, float x, float y, float z);

        private delegate bool SetActorVirtualWorldImpl(int actorid, int vworld);

        [Native("CreateActor")]
        private static readonly CreateActorImpl CreateActor = null;
        [Native("DestroyActor")]
        private static readonly DestroyActorImpl DestroyActor = null;
        [Native("IsActorStreamedIn")]
        private static readonly IsActorStreamedInImpl IsActorStreamedIn = null;
        [Native("SetActorVirtualWorld")]
        private static readonly SetActorVirtualWorldImpl SetActorVirtualWorld = null;
        [Native("GetActorVirtualWorld")]
        private static readonly GetActorVirtualWorldImpl GetActorVirtualWorld = null;
        [Native("ClearActorAnimations")]
        private static readonly ClearActorAnimationsImpl ClearActorAnimations = null;
        [Native("SetActorPos")]
        private static readonly SetActorPosImpl SetActorPos = null;
        [Native("GetActorPos")]
        private static readonly GetActorPosImpl GetActorPos = null;
        [Native("SetActorFacingAngle")]
        private static readonly SetActorFacingAngleImpl SetActorFacingAngle = null;
        [Native("GetActorFacingAngle")]
        private static readonly GetActorFacingAngleImpl GetActorFacingAngle = null;
        [Native("SetActorHealth")]
        private static readonly SetActorHealthImpl SetActorHealth = null;
        [Native("GetActorHealth")]
        private static readonly GetActorHealthImpl GetActorHealth = null;
        [Native("SetActorInvulnerable")]
        private static readonly SetActorInvulnerableImpl SetActorInvulnerable = null;
        [Native("IsActorInvulnerable")]
        private static readonly IsActorInvulnerableImpl IsActorInvulnerable = null;
        [Native("IsValidActor")]
        private static readonly IsValidActorImpl IsValidActor = null;
        [Native("ApplyActorAnimation")]
        private static readonly ApplyActorAnimationImpl ApplyActorAnimation = null;

        #endregion

        /// <summary>
        ///     Creates a new <see cref="Actor" />.
        /// </summary>
        /// <param name="modelid">The modelid.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns>The instance of the actor.</returns>
        public static Actor Create(int modelid, Vector3 position, float rotation)
        {
            var id = CreateActor(modelid, position.X, position.Y, position.Z, rotation);

            return id == InvalidId ? null : new Actor(id);
        }

        /// <summary>
        ///     Determines whether this <see cref="Actor" /> is streamed in for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>True if streamed in; False otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public bool IsStreamedIn(GtaPlayer player)
        {
            if (player == null) throw new ArgumentNullException("player");

            AssertNotDisposed();

            return IsActorStreamedIn(Id, player.Id);
        }

        /// <summary>
        ///     Applies the specified animation to this <see cref="Actor" />.
        /// </summary>
        /// <param name="library">The animation library from which to apply an animation.</param>
        /// <param name="name">The name of the animation to apply, within the specified library.</param>
        /// <param name="fDelta">The speed to play the animation.</param>
        /// <param name="loop">if set to <c>true</c> the animation will loop.</param>
        /// <param name="lockx">if set to <c>true</c> allow this Actor to move it's x-coordinate.</param>
        /// <param name="locky">if set to <c>true</c> allow this Actor to move it's y-coordinate.</param>
        /// <param name="freeze">if set to <c>true</c> freeze this Actor at the end of the animation.</param>
        /// <param name="time">The amount of time (in milliseconds) to play the animation.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     animlib
        ///     or
        ///     animname
        /// </exception>
        public void ApplyAnimation(string library, string name, float fDelta, bool loop, bool lockx, bool locky,
            bool freeze, int time)
        {
            if (library == null) throw new ArgumentNullException("animlib");
            if (name == null) throw new ArgumentNullException("animname");

            AssertNotDisposed();

            ApplyActorAnimation(Id, library, name, fDelta, loop, lockx, locky, freeze, time);
        }

        /// <summary>
        ///     Clear any animations applied to this Actor.
        /// </summary>
        public void ClearAnimations()
        {
            AssertNotDisposed();

            ClearActorAnimations(Id);
        }

        #region Overrides of Pool<GtaPlayer>

        /// <summary>
        ///     Removes this instance from the pool.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            DestroyActor(Id);
            base.Dispose(disposing);
        }

        #endregion

        /// <summary>
        ///     Raises the <see cref="E:StreamIn" /> event.
        /// </summary>
        /// <param name="args">The <see cref="PlayerEventArgs" /> instance containing the event data.</param>
        public void OnStreamIn(PlayerEventArgs args)
        {
            if (StreamIn != null)
                StreamIn(this, args);
        }

        /// <summary>
        ///     Raises the <see cref="E:StreamOut" /> event.
        /// </summary>
        /// <param name="args">The <see cref="PlayerEventArgs" /> instance containing the event data.</param>
        public void OnStreamOut(PlayerEventArgs args)
        {
            if (StreamOut != null)
                StreamOut(this, args);
        }

        /// <summary>
        ///     Raises the <see cref="E:PlayerGiveDamage" /> event.
        /// </summary>
        /// <param name="args">The <see cref="DamageEventArgs" /> instance containing the event data.</param>
        public void OnPlayerGiveDamage(DamageEventArgs args)
        {
            if (PlayerGiveDamage != null)
                PlayerGiveDamage(this, args);
        }
    }
}