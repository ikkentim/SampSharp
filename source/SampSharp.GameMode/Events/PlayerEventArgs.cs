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

using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.PlayerConnected" />,
    ///     <see cref="BaseMode.PlayerSpawned" />, <see cref="BaseMode.PlayerEnterCheckpoint" />,
    ///     <see cref="BaseMode.PlayerLeaveCheckpoint" />, <see cref="BaseMode.PlayerEnterRaceCheckpoint" />,
    ///     <see cref="BaseMode.PlayerLeaveRaceCheckpoint" />, <see cref="BaseMode.PlayerRequestSpawn" />,
    ///     <see cref="BaseMode.VehicleDamageStatusUpdated" />, <see cref="BaseMode.PlayerExitedMenu" /> or
    ///     <see cref="BaseMode.PlayerUpdate" /> event.
    /// </summary>
    public class PlayerEventArgs : GameModeEventArgs
    {
        public PlayerEventArgs(int playerid)
        {
            PlayerId = playerid;
        }

        public int PlayerId { get; private set; }

        public Player Player
        {
            get { return PlayerId == Player.InvalidId ? null : Player.Find(PlayerId); }
        }
    }
}