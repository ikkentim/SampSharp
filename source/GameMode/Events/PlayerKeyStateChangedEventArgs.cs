using GameMode.Definitions;

namespace GameMode.Events
{
    public class PlayerKeyStateChangedEventArgs : PlayerEventArgs
    {
        public PlayerKeyStateChangedEventArgs(int playerid, Keys newkeys, Keys oldkeys) : base(playerid)
        {
            NewKeys = newkeys;
            OldKeys = oldkeys;
        }

        public Keys NewKeys { get; private set; }

        public Keys OldKeys { get; private set; }
    }
}
