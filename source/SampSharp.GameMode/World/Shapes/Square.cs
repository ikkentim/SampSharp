namespace SampSharp.GameMode.World.Shapes
{
    public class Square : IShape
    {
        public Square(float minX, float maxX, float minY, float maxY)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;

        }

        public float MinX { get; set; }
        public float MaxX { get; set; }
        public float MinY { get; set; }
        public float MaxY { get; set; }

        public bool Contains(Vector point)
        {
            return MinX < point.X &&
                   point.X < MaxX &&
                   MinY < point.Y &&
                   point.Y < MaxY;
        }
    }
}
