﻿// SampSharp
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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerTakeDamage" />, <see cref="BaseMode.PlayerGiveDamage" />,
    ///     <see cref="BasePlayer.TakeDamage" /> or <see cref="BasePlayer.GiveDamage" /> event.
    /// </summary>
    public class ActorDamageEventArgs : DamageEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the DamageEventArgs class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="actor">The actor.</param>
        /// <param name="amount">Amount of damage done.</param>
        /// <param name="weapon">Weapon used to damage another.</param>
        /// <param name="bodypart">BodyPart shot at.</param>
        public ActorDamageEventArgs(BasePlayer player, Actor actor, float amount, Weapon weapon, BodyPart bodypart) : base(player, null, amount, weapon, bodypart)
        {
            Actor = actor;
        }

        /// <summary>
        ///     Gets the Actor
        /// </summary>
        public Actor Actor { get; private set; }
        
    }
}