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

namespace GameMode.World
{
    /// <summary>
    ///     Defines an object that is placed in the world.
    /// </summary>
    public interface IWorldObject
    {
        /// <summary>
        ///     Gets or sets the position of this IWorldObject.
        /// </summary>
        Vector Position { get; set; }

        /// <summary>
        ///     Gets or sets the rotation of this IWorldObject.
        /// </summary>
        Vector Rotation { get; set; }
    }
}