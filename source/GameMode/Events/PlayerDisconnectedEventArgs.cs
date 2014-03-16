using GameMode.Definitions;

namespace GameMode.Events
{
    public class PlayerDisconnectedEventArgs : PlayerEventArgs
    {
        public PlayerDisconnectedEventArgs(int playerid, PlayerDisconnectReason reason)
            : base(playerid)
        {
            Reason = reason;
        }

        public PlayerDisconnectReason Reason { get; private set; }
    }
}
