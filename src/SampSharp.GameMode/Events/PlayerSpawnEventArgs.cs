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
    ///     Provides data for the <see cref="BaseMode.PlayerSpawned" /> event.
    /// </summary>
    public class PlayerSpawnEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the PlayerSpawnEventArgs class.
        /// </summary>
        /// <param name="playerid">The id of the player.</param>
        public PlayerSpawnEventArgs(int playerid) : base(playerid)
        {
        }

        /// <summary>
        ///     Gets or sets whether the player will be forced back to the class selection when they next spawn.
        /// </summary>
        public bool ReturnToClassSelection { get; set; }
    }
}