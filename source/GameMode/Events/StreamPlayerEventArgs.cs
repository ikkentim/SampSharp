namespace GameMode.Events
{
    public class StreamPlayerEventArgs : PlayerEventArgs
    {
        public StreamPlayerEventArgs(int playerid, int forplayerid) : base(playerid)
        {
            ForPlayerId = forplayerid;
        }

        public int ForPlayerId { get; private set; }
    }
}
