namespace SampSharp.GameMode.World.Shapes
{
    public class Polygon : IShape
    {
        public Polygon(Vector[] points)
        {
            Points = points;
        }
        public Vector[] Points { get; set; }

        public bool Contains(Vector point)
        {
            if (Points == null)
                return false;

            bool result = false;
            int j = Points.Length - 1;
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i].Y < point.Y && Points[j].Y >= point.Y || Points[j].Y < point.Y && Points[i].Y >= point.Y)
                {
                    if (Points[i].X + (point.Y - Points[i].Y) / (Points[j].Y - Points[i].Y) * (Points[j].X - Points[i].X) < point.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
    }
}
