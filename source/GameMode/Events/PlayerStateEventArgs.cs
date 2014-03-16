using GameMode.Definitions;

namespace GameMode.Events
{
    public class PlayerStateEventArgs : PlayerEventArgs
    {
        public PlayerStateEventArgs(int playerid, PlayerState newstate, PlayerState oldstate)
            : base(playerid)
        {
            NewState = newstate;
            OldState = oldstate;
        }

        public PlayerState NewState { get; private set; }

        public PlayerState OldState { get; private set; }

    }
}
