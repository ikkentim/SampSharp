namespace SampSharp.GameMode.World.Shapes
{
    /// <summary>
    /// Represents a 2D circle.
    /// </summary>
    public class Circle : IShape
    {
        public Circle(Vector position, float radius)
        {
            Position = position;
            Radius = radius;
        }

        public Vector Position { get; set; }

        public float Radius { get; set; }

        public bool Contains(Vector point)
        {
            return new Vector(Position.X, Position.Y).DistanceTo(new Vector(point.X, point.Y)) < Radius;
        }
    }
}
