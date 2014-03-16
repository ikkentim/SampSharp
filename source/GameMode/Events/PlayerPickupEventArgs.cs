namespace GameMode.Events
{
    public class PlayerPickupEventArgs : PlayerEventArgs
    {
        public PlayerPickupEventArgs(int playerid, int pickupid) : base(playerid)
        {
            PickupId = pickupid;
        }

        public int PickupId { get; private set; }
    }
}
