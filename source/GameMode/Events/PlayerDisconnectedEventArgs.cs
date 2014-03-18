using GameMode.Definitions;

namespace GameMode.Events
{
    public class PlayerDisconnectedEventArgs : PlayerEventArgs
    {
        public PlayerDisconnectedEventArgs(int playerid, DisconnectReason reason)
            : base(playerid)
        {
            Reason = reason;
        }

        public DisconnectReason Reason { get; private set; }
    }
}
