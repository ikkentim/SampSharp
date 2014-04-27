namespace SampSharp.GameMode.Events
{
    public class ConnectionEventArgs : GameModeEventArgs
    {
        public ConnectionEventArgs(int playerid, string ipAddress, int port)
            : base(true)
        {
            PlayerId = playerid;
            IpAddress = ipAddress;
            Port = port;
        }

        // Not using Player here as we don't delete the object when
        // the player fails to connect to the server.

        public int PlayerId { get; private set; }

        public string IpAddress { get; private set; }
        
        public int Port { get; private set; }
    }
}
