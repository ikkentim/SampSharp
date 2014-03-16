using GameMode.World;

namespace GameMode.Events
{
    public class PlayerClickMapEventArgs : PlayerEventArgs
    {
        public PlayerClickMapEventArgs(int playerid, Position position) : base(playerid)
        {
            Position = position;
        }

        public Position Position { get; private set; }
    }
}
