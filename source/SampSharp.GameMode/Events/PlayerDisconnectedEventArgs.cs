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
    ///     Provides data for the <see cref="BaseMode.PlayerDisconnected" /> event.
    /// </summary>
    public class PlayerDisconnectedEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the PlayerDisconnectedEventArgs class
        /// </summary>
        /// <param name="playerid">Id of the player disconnected.</param>
        /// <param name="reason">DisconnectReason.</param>
        public PlayerDisconnectedEventArgs(int playerid, DisconnectReason reason)
            : base(playerid)
        {
            Reason = reason;
        }

        /// <summary>
        ///     Gets the reason of the disconnection.
        /// </summary>
        public DisconnectReason Reason { get; private set; }
    }
}