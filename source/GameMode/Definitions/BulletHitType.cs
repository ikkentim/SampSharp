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

namespace GameMode.Definitions
{
    /// <summary>
    /// Contains all things bullets can hit.
    /// </summary>
    public enum BulletHitType
    {
        /// <summary>
        /// Hit nothing.
        /// </summary>
        None = 0,
        /// <summary>
        /// Hit a player.
        /// </summary>
        Player = 1,
        /// <summary>
        /// Hit a vehicle.
        /// </summary>
        Vehicle = 2,
        /// <summary>
        /// Hit an GlobalObject.
        /// </summary>
        Object = 3,
        /// <summary>
        /// Hit a PlayerObject.
        /// </summary>
        PlayerObject = 4
    }
}