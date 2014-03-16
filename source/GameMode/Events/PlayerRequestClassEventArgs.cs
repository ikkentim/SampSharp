namespace GameMode.Events
{
    public class PlayerRequestClassEventArgs : PlayerEventArgs
    {
        public PlayerRequestClassEventArgs(int playerid, int classid) : base(playerid)
        {
            ClassId = classid;
        }

        public int ClassId { get; private set; }
    }
}
