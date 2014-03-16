namespace GameMode.Events
{
    public class PlayerEventArgs : GameModeEventArgs
    {
        public PlayerEventArgs(int playerid)
        {
            PlayerId = playerid;
        }

        public int PlayerId { get; private set; }
    }
}
