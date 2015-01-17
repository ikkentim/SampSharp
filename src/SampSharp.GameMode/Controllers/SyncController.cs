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

using System;
using System.Threading;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing sync requests.
    /// </summary>
    public sealed class SyncController : IEventListener
    {
        private static BaseMode _gameMode;
        private static bool _waiting;

        /// <summary>
        ///     Gets the main thread.
        /// </summary>
        public static Thread MainThread { get; private set; }

        /// <summary>
        ///     Registers the events this SyncController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public void RegisterEvents(BaseMode gameMode)
        {
            _gameMode = gameMode;
            MainThread = Thread.CurrentThread;
        }

        /// <summary>
        ///     Start waiting for a gamemode tick to sync all resync requests.
        /// </summary>
        public static void Start()
        {
            if (!_waiting)
            {
                _waiting = true;
                _gameMode.Tick += _gameMode_Tick;
            }
        }

        private static void _gameMode_Tick(object sender, EventArgs e)
        {
            foreach (Sync.SyncTask t in Sync.SyncTask.All)
            {
                t.Run();
                t.Dispose();
            }

            _waiting = false;
            _gameMode.Tick -= _gameMode_Tick;
        }
    }
}