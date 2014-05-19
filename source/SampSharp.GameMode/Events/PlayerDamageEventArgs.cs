// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using SampSharp.GameMode.Definitions;

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