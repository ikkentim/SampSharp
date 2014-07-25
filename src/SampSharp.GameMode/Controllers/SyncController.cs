using System;
using System.Linq;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.Controllers
{
    public sealed class SyncController : IEventListener
    {
        private static BaseMode _gameMode;
        private static bool _waiting;
        public void RegisterEvents(BaseMode gameMode)
        {
            _gameMode = gameMode;
        }

        public static void Start()
        {
            if (!_waiting)
            {
                _waiting = true;
                _gameMode.Tick += _gameMode_Tick;
            }
        }

        static void _gameMode_Tick(object sender, System.EventArgs e)
        {
            foreach (var t in Sync.SyncTask.All)
            {
                t.Run();
            }
            _waiting = false;
            _gameMode.Tick -= _gameMode_Tick;
        }
    }
}
