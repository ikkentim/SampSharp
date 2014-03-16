namespace GameMode.Events
{
    public class UnoccupiedVehicleEventArgs : PlayerVehicleEventArgs
    {
        public UnoccupiedVehicleEventArgs(int playerid, int vehicleid, int passengerSeat) : base(playerid, vehicleid)
        {
            PassengerSeat = passengerSeat;
        }

        public int PassengerSeat { get; private set; }
    }
}
