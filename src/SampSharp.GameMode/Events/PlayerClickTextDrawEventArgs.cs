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

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerClickTextDraw" /> or
    ///     <see cref="BaseMode.PlayerClickPlayerTextDraw" /> event.
    /// </summary>
    public class PlayerClickTextDrawEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the PlayerClickTextDrawEventArgs class.
        /// </summary>
        /// <param name="playerid">Id of the player.</param>
        /// <param name="textdrawid">Id of the textdraw.</param>
        public PlayerClickTextDrawEventArgs(int playerid, int textdrawid) : base(playerid)
        {
            TextDrawId = textdrawid;
        }

        /// <summary>
        ///     Gets the id of the textdraw.
        /// </summary>
        public int TextDrawId { get; private set; }
    }
}