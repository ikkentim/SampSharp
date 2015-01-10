// SampSharp
// Copyright (C) 2015 Tim Potze
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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerDied" /> event.
    /// </summary>
    public class PlayerDeathEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the PlayerDeathEventArgs class.
        /// </summary>
        /// <param name="playerid">Id of the player.</param>
        /// <param name="killerid">Id of the killer.</param>
        /// <param name="reason">Reason of the death.</param>
        public PlayerDeathEventArgs(int playerid, int killerid, Weapon reason) : base(playerid)
        {
            KillerId = killerid;
            DeathReason = reason;
        }

        /// <summary>
        ///     Gets the id of the killer.
        /// </summary>
        public int KillerId { get; private set; }

        /// <summary>
        ///     Gets the killer.
        /// </summary>
        public GtaPlayer Killer
        {
            get { return KillerId == GtaPlayer.InvalidId ? null : GtaPlayer.FindOrCreate(KillerId); }
        }

        /// <summary>
        ///     Gets the reason of the death.
        /// </summary>
        public Weapon DeathReason { get; private set; }
    }
}