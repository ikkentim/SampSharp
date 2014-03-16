using GameMode.Definitions;

namespace GameMode.Events
{
    public class PlayerDamageEventArgs : PlayerEventArgs
    {
        public PlayerDamageEventArgs(int playerid, int otherplayerid, float amount, Weapon weapon, BodyPart bodypart)
            : base(playerid)
        {
            OtherPlayerId = otherplayerid;
            Amount = amount;
            Weapon = weapon;
            BodyPart = bodypart;
        }

        public int OtherPlayerId { get; private set; }
        public float Amount { get; private set; }
        public Weapon Weapon { get; private set; }
        public BodyPart BodyPart { get; private set; }
    }
}
