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

using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerTakeDamage" /> or
    ///     <see cref="BaseMode.PlayerGiveDamage" /> event.
    /// </summary>
    public class PlayerDamageEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the PlayerDamageEventArgs class.
        /// </summary>
        /// <param name="playerid">Id of the player.</param>
        /// <param name="otherplayerid">Id of the other player.</param>
        /// <param name="amount">Amount of damage done.</param>
        /// <param name="weapon">Weapon used to damage another.</param>
        /// <param name="bodypart">BodyPart shot at.</param>
        public PlayerDamageEventArgs(int playerid, int otherplayerid, float amount, Weapon weapon, BodyPart bodypart)
            : base(playerid)
        {
            OtherPlayerId = otherplayerid;
            Amount = amount;
            Weapon = weapon;
            BodyPart = bodypart;
        }

        /// <summary>
        ///     Gets the id of the other player.
        /// </summary>
        public int OtherPlayerId { get; private set; }

        /// <summary>
        ///     Gets the other player.
        /// </summary>
        public GtaPlayer OtherPlayer
        {
            get { return OtherPlayerId == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(OtherPlayerId); }
        }

        /// <summary>
        ///     Gets the amount of damage done.
        /// </summary>
        public float Amount { get; private set; }

        /// <summary>
        ///     Gets the Weapon used to damage another player.
        /// </summary>
        public Weapon Weapon { get; private set; }

        /// <summary>
        ///     Gets the BodyPart shot at.
        /// </summary>
        public BodyPart BodyPart { get; private set; }
    }
}