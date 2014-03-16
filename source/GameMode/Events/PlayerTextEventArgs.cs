namespace GameMode.Events
{
    public class PlayerTextEventArgs : PlayerEventArgs
    {
        public PlayerTextEventArgs(int playerid, string text) : base(playerid)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }
}
