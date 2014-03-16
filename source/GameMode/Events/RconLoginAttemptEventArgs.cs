namespace GameMode.Events
{
    public class RconLoginAttemptEventArgs : GameModeEventArgs
    {
        public RconLoginAttemptEventArgs(string ip, string password, bool success)
        {
            IP = ip;
            Password = password;
            SuccessfulLogin = success;
        }

        public string IP { get; private set; }

        public string Password { get; private set; }

        public bool SuccessfulLogin { get; private set; }
    }
}
