namespace GameMode.Events
{
    public class VehicleModEventArgs : PlayerVehicleEventArgs
    {
        public VehicleModEventArgs(int playerid, int vehicleid, int componentid)
            : base(playerid, vehicleid)
        {
            ComponentId = componentid;
        }

        public int ComponentId { get; set; }
    }
}
