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
    /// <summary>
    ///     Represents a 2D circle.
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