using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace RiverShell.World
{
    public class Team : IdentifiedPool<Team>, IIdentifyable
    {
        public int Id { get; set; }

        public Color Color { get; set; }

        public string GameTextTeamName { get; set; }

        public Checkpoint Target { get; set; }

        public int TimesCaptured { get; set; }

        public Vehicle TargetVehicle { get; set; }

        public Vector FixedSpectatePosition { get; set; }

        public Vector FixedSpectateLookAtPosition { get; set; }

        public Vector ResupplyPosition { get; set; }
    }
}
