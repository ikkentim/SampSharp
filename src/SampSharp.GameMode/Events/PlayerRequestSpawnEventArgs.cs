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

using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerRequestSpawn" /> event.
    /// </summary>
    public class PlayerRequestSpawnEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the PlayerRequestSpawnEventArgs class.
        /// </summary>
        /// <param name="playerid">The id of the player.</param>
        public PlayerRequestSpawnEventArgs(int playerid)
        {
            PlayerId = playerid;
        }

        /// <summary>
        ///     Gets the id of the player involved in the event.
        /// </summary>
        public int PlayerId { get; private set; }

        /// <summary>
        ///     Gets the player involved in the event.
        /// </summary>
        public GtaPlayer Player
        {
            get { return PlayerId == GtaPlayer.InvalidId ? null : GtaPlayer.Find(PlayerId); }
        }

        /// <summary>
        ///     Gets or sets whether the player is prevented from spawning.
        /// </summary>
        public bool PreventSpawning { get; set; }
    }
}