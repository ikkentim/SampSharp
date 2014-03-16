namespace GameMode.Events
{
    public class PlayerEnterVehicleEventArgs : PlayerVehicleEventArgs
    {
        public PlayerEnterVehicleEventArgs(int playerid, int vehicleid, bool ispassenger) : base(playerid, vehicleid)
        {
            IsPassenger = ispassenger;
        }

        public bool IsPassenger { get; private set; }
    }
}
