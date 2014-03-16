namespace GameMode.Events
{
    public class PlayerInteriorChangedEventArgs : PlayerEventArgs
    {
        public PlayerInteriorChangedEventArgs(int playerid, int newinterior, int oldinterior) : base(playerid)
        {
            NewInterior = newinterior;
            OldInterior = oldinterior;
        }

        public int NewInterior { get; private set; }

        public int OldInterior { get; private set; }
    }
}
