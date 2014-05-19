namespace SampSharp.GameMode.World.Shapes
{
    public class Sphere : IShape
    {
        public Sphere(Vector position, float radius)
        {
            Position = position;
            Radius = radius;
        }

        public Vector Position { get; set; }

        public float Radius { get; set; }

        public bool Contains(Vector point)
        {
            return Position.DistanceTo(point) < Radius;
        }
    }
}
