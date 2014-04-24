using SampSharp.GameMode.World;

namespace RiverShell.World
{
    public class Team : InstanceKeeper<Team>, IIdentifyable
    {
        public Team(int id, Color color)
        {
            Id = id;
            Color = color;
        }

        public int Id { get; set; }

        public Color Color { get; set; }

        public RPlayer PlayerWithVehicle { get; set; }

        public int TimesCaptured { get; set; }

        public Vehicle Vehicle { get; set; }

    }
}
