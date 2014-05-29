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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerClickPlayer" /> event.
    /// </summary>
    public class PlayerClickPlayerEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Intializes a new instance of the PlayerClickPlayerEventArgs class.
        /// </summary>
        /// <param name="playerid">Id of the player.</param>
        /// <param name="clickedplayerid">Id of the clicked player.</param>
        /// <param name="source">PlayerClickSource of the event.</param>
        public PlayerClickPlayerEventArgs(int playerid, int clickedplayerid, PlayerClickSource source) : base(playerid)
        {
            ClickedPlayerId = clickedplayerid;
            PlayerClickSource = source;
        }

        /// <summary>
        ///     Gets the id of the clicked player.
        /// </summary>
        public int ClickedPlayerId { get; private set; }

        /// <summary>
        /// Gets the clicked player.
        /// </summary>
        public Player ClickedPlayer
        {
            get { return ClickedPlayerId == Player.InvalidId ? null : Player.FindOrCreate(ClickedPlayerId); }
        }

        /// <summary>
        ///     Gets the PlayerClickSource of this event.
        /// </summary>
        public PlayerClickSource PlayerClickSource { get; private set; }
    }
}