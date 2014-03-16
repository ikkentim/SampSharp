namespace GameMode.Events
{
    public class RconEventArgs : GameModeEventArgs
    {
        public RconEventArgs(string command)
        {
            Command = command;
        }

        public string Command { get; private set; }
    }
}
