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
    ///     Provides data for the <see cref="BaseMode.PlayerText" /> event.
    /// </summary>
    public class PlayerTextEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the PlayerTextEventArgs class.
        /// </summary>
        /// <param name="playerid">The id of the player.</param>
        /// <param name="text">The text sent by the player.</param>
        public PlayerTextEventArgs(int playerid, string text) : base(playerid)
        {
            Text = text;
        }

        /// <summary>
        ///     Gets the text sent by the player.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        ///     Gets or sets whether this message should be sent to all players.
        ///     Returning 0 in this callback will stop the text from being sent to all players
        /// </summary>
        public bool SendToPlayers { get; set; }
    }
}