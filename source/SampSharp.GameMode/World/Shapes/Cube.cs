namespace SampSharp.GameMode.World.Shapes
{
    public class Cube : IShape
    {
        public Cube(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
            MinZ = minZ;
            MaxZ = maxZ;

        }

        public float MinX { get; set; }
        public float MaxX { get; set; }
        public float MinY { get; set; }
        public float MaxY { get; set; }
        public float MinZ { get; set; }
        public float MaxZ { get; set; }

        public bool Contains(Vector point)
        {
            return MinX < point.X &&
                   point.X < MaxX &&
                   MinY < point.Y &&
                   point.Y < MaxY &&
                   MinZ < point.Z &&
                   point.Z < MaxZ;
        }
    }
}
