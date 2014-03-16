using GameMode.Definitions;

namespace GameMode.Events
{
    public class PlayerClickPlayerEventArgs : PlayerEventArgs
    {
        public PlayerClickPlayerEventArgs(int playerid, int clickedplayerid, PlayerClickSource source) : base(playerid)
        {
            ClickPlayerId = clickedplayerid;
            PlayerClickSource = source;
        }

        public int ClickPlayerId { get; private set; }

        public PlayerClickSource PlayerClickSource { get; private set; }
    }
}
