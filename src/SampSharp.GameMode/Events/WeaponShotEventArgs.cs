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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerWeaponShot" /> or <see cref="BasePlayer.WeaponShot" /> event.
    /// </summary>
    public class WeaponShotEventArgs : PositionEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WeaponShotEventArgs" /> class.
        /// </summary>
        /// <param name="weapon">The weapon.</param>
        /// <param name="hittype">The hit type.</param>
        /// <param name="hitid">The hit ID.</param>
        /// <param name="position">The position.</param>
        public WeaponShotEventArgs(Weapon weapon, BulletHitType hittype, int hitid, Vector3 position)
            : base(position)
        {
            Weapon = weapon;
            BulletHitType = hittype;
            HitId = hitid;
        }

        /// <summary>
        ///     Gets the weapon.
        /// </summary>
        public Weapon Weapon { get; }

        /// <summary>
        ///     Gets the type of the bullet hit.
        /// </summary>
        public BulletHitType BulletHitType { get; }

        /// <summary>
        ///     Gets the hit identifier.
        /// </summary>
        public int HitId { get; }

        /// <summary>
        ///     Gets or sets whether the bullets should be prevented from causing damage.
        /// </summary>
        public bool PreventDamage { get; set; }
    }
}