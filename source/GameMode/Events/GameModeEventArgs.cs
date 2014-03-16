using System;

namespace GameMode.Events
{
    public class GameModeEventArgs : EventArgs
    {

        public GameModeEventArgs() : this(true)
        {

        }

        public GameModeEventArgs(bool success)
        {
            Success = success;
        }

        public bool Success { get; set; }
    }
}
