using GameMode.World;

namespace GameMode.Events
{
    public class PlayerClickMapEventArgs : PlayerEventArgs
    {
        public PlayerClickMapEventArgs(int playerid, Vector position) : base(playerid)
        {
            Position = position;
        }

        public Vector Position { get; private set; }
    }
}
