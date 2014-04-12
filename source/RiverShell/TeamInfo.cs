using GameMode.World;
using RiverShell.World;

namespace RiverShell
{
    public class TeamInfo
    {
        public RPlayer PlayerWithVehicle { get; set; }

        public int TimesCaptured { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
