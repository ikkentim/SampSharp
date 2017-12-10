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
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World
{
    /// <summary>
    ///     Represents a SA-MP actor.
    /// </summary>
    public partial class Actor : IdentifiedPool<Actor>, IWorldObject
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
        ///     Gets the facing angle of this <see cref="Actor" />.
        /// </summary>
        public float FacingAngle
        {
            get
            {
                float angle;

                AssertNotDisposed();

                ActorInternal.Instance.GetActorFacingAngle(Id, out angle);
                return angle;
            }
            set
            {
                AssertNotDisposed();
                ActorInternal.Instance.SetActorFacingAngle(Id, value);
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

                ActorInternal.Instance.GetActorHealth(Id, out health);
                return health;
            }
            set
            {
                AssertNotDisposed();
                ActorInternal.Instance.SetActorHealth(Id, value);
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
                return ActorInternal.Instance.IsActorInvulnerable(Id);
            }
            set
            {
                AssertNotDisposed();
                ActorInternal.Instance.SetActorInvulnerable(Id, value);
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
                return ActorInternal.Instance.IsValidActor(Id);
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
                return ActorInternal.Instance.GetActorVirtualWorld(Id);
            }
            set
            {
                AssertNotDisposed();
                ActorInternal.Instance.SetActorVirtualWorld(Id, value);
            }
        }

        /// <summary>
        ///     Gets the size of the actors pool.
        /// </summary>
        public static int PoolSize => ActorInternal.Instance.GetActorPoolSize();

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

                ActorInternal.Instance.GetActorPos(Id, out x, out y, out z);
                return new Vector3(x, y, z);
            }
            set
            {
                AssertNotDisposed();
                ActorInternal.Instance.SetActorPos(Id, value.X, value.Y, value.Z);
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

        /// <summary>
        ///     Creates a new <see cref="Actor" />.
        /// </summary>
        /// <param name="modelid">The modelid.</param>
        /// <param name="position">The position.</param>
        /// <param name="rotation">The rotation.</param>
        /// <returns>The instance of the actor.</returns>
        public static Actor Create(int modelid, Vector3 position, float rotation)
        {
            var id = ActorInternal.Instance.CreateActor(modelid, position.X, position.Y, position.Z, rotation);

            return id == InvalidId ? null : FindOrCreate(id);
        }

        /// <summary>
        ///     Determines whether this <see cref="Actor" /> is streamed in for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>True if streamed in; False otherwise.</returns>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public bool IsStreamedIn(BasePlayer player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            AssertNotDisposed();

            return ActorInternal.Instance.IsActorStreamedIn(Id, player.Id);
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
            if (library == null) throw new ArgumentNullException(nameof(library));
            if (name == null) throw new ArgumentNullException(nameof(name));

            AssertNotDisposed();

            ActorInternal.Instance.ApplyActorAnimation(Id, library, name, fDelta, loop, lockx, locky, freeze, time);
        }

        /// <summary>
        ///     Clear any animations applied to this Actor.
        /// </summary>
        public void ClearAnimations()
        {
            AssertNotDisposed();

            ActorInternal.Instance.ClearActorAnimations(Id);
        }
        
        public enum MoveAction
        {
            Walk = "WALK_civi",
            Run = "RUN_civi"
        }
        
        private Timer timer;
        
        public bool Moving
        {
            get
            {
                return timer.IsRunning;
            }
            set
            {
                if(value==false)
                {
                    timer.IsRunning = value;
                    Position.Z = Position.Z-0.1f;
                    ClearAnimations();
                }
            }
        }
        
        public void Move(Vector3 position, MoveAction action)
        {
            if(Moving==true) Moving=false;
            float animSpeed;
            if(action==MoveAction.Walk) animSpeed = 1.5357f;
            else animSpeed = 3.7f;
            ClearAnimations();
            FaceToPos(position);
            ApplyAnimation("ped", action, 4.1, 1, 1, 1, 0, 0);
            timer = new Timer(Vector3.Distance(Position, position)/animSpeed*918.0f, false);
            timer.Tick += Stop;
            
        }
        
        private void Stop(object sender, EventArgs e)
        {
                Position.Z = Position.Z-0.1f;
                ClearAnimations();
        }
        
        public void FaceToPos(Vector3 position)
        {
            private double angle = Math.Atan2(position.Y-Position.Y, position.X-Position.X) + 270.0;
            if(angle>360.0) angle-=360.0;
            FacingAngle = (float) angle;
        }
        
        /// <summary>
        ///     Raises the <see cref="E:StreamIn" /> event.
        /// </summary>
        /// <param name="args">The <see cref="PlayerEventArgs" /> instance containing the event data.</param>
        public void OnStreamIn(PlayerEventArgs args)
        {
            StreamIn?.Invoke(this, args);
        }

        /// <summary>
        ///     Raises the <see cref="E:StreamOut" /> event.
        /// </summary>
        /// <param name="args">The <see cref="PlayerEventArgs" /> instance containing the event data.</param>
        public void OnStreamOut(PlayerEventArgs args)
        {
            StreamOut?.Invoke(this, args);
        }

        /// <summary>
        ///     Raises the <see cref="E:PlayerGiveDamage" /> event.
        /// </summary>
        /// <param name="args">The <see cref="DamageEventArgs" /> instance containing the event data.</param>
        public void OnPlayerGiveDamage(DamageEventArgs args)
        {
            PlayerGiveDamage?.Invoke(this, args);
        }

        #region Overrides of Pool<GtaPlayer>

        /// <summary>
        ///     Removes this instance from the pool.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            ActorInternal.Instance.DestroyActor(Id);
            base.Dispose(disposing);
        }

        #endregion
    }
}
