namespace GameMode.Events
{
    public class VehicleResprayedEventArgs : PlayerVehicleEventArgs
    {
        public VehicleResprayedEventArgs(int playerid, int vehicleid, int color1, int color2) : base(playerid, vehicleid)
        {
            Color1 = color1;
            Color2 = color2;
        }

        public int Color1 { get; private set; }

        public int Color2 { get; private set; }
    }
}
