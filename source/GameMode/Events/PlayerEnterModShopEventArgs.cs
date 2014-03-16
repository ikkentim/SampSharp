using GameMode.Definitions;

namespace GameMode.Events
{
    public class PlayerEnterModShopEventArgs : PlayerEventArgs
    {
        public PlayerEnterModShopEventArgs(int playerid, EnterExit enterExit, int interiorid) : base(playerid)
        {
            EnterExit = enterExit;
            InteriorId = interiorid;
        }

        public EnterExit EnterExit { get; private set; }

        public int InteriorId { get; private set; }
    }
}
