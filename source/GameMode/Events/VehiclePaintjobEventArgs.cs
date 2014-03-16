namespace GameMode.Events
{
    public class VehiclePaintjobEventArgs : PlayerVehicleEventArgs
    {
        public VehiclePaintjobEventArgs(int playerid, int vehicleid, int paintjobid)
            : base(playerid, vehicleid)
        {
            PaintjobId = paintjobid;
        }

        public int PaintjobId { get; private set; }
    }
}
