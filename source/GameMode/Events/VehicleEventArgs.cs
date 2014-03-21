using GameMode.World;

namespace GameMode.Events
{
    public class VehicleEventArgs : GameModeEventArgs
    {
        public VehicleEventArgs(int vehicleid)
        {
            VehicleId = vehicleid;
        }

        public int VehicleId { get; private set; }

        public Vehicle Vehicle
        {
            get { return Vehicle.Find(VehicleId); }
        }
    }
}
