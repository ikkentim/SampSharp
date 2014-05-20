// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

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
                    if (Points[i].X + (point.Y - Points[i].Y)/(Points[j].Y - Points[i].Y)*(Points[j].X - Points[i].X) <
                        point.X)
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