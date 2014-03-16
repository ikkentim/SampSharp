namespace GameMode.Events
{
    public class PlayerObjectEventArgs : PlayerEventArgs
    {
        public PlayerObjectEventArgs(int playerid, int objectid) : base(playerid)
        {
            ObjectId = objectid;
        }

        public int ObjectId { get; private set; }
    }
}
