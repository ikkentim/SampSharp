namespace GameMode.Events
{
    public class PlayerSelectedMenuRowEventArgs : PlayerEventArgs
    {
        public PlayerSelectedMenuRowEventArgs(int playerid, int row) : base(playerid)
        {
            Row = row;
        }

        public int Row { get; private set; }
    }
}
