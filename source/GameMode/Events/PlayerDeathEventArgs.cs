using GameMode.Definitions;

namespace GameMode.Events
{
    public class PlayerDeathEventArgs: PlayerEventArgs
    {
        public PlayerDeathEventArgs(int playerid, int killerid, Weapon reason) : base(playerid)
        {
            KillerId = killerid;
            DeathReason = reason;
        }

        public int KillerId { get; private set; }
        public Weapon DeathReason { get; private set; }
    }
}
