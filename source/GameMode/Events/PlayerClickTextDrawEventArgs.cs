namespace GameMode.Events
{
    public class PlayerClickTextDrawEventArgs : PlayerEventArgs
    {
        public PlayerClickTextDrawEventArgs(int playerid, int textdrawid) : base(playerid)
        {
            TextDrawId = textdrawid;
        }

        public int TextDrawId { get; private set; }
    }
}
