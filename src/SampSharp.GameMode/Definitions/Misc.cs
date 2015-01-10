// SampSharp
// Copyright (C) 2015 Tim Potze
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

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Misc defined values.
    /// </summary>
    public static class Misc
    {
        /// <summary>
        ///     Invalid player id.
        /// </summary>
        public const int InvalidPlayerId = 0xFFFF;

        /// <summary>
        ///     Invalid vehicle id.
        /// </summary>
        public const int InvalidVehicleId = 0xFFFF;

        /// <summary>
        ///     No team.
        /// </summary>
        public const int NoTeam = 255;

        /// <summary>
        ///     Invalid object id.
        /// </summary>
        public const int InvalidObjectId = 0xFFFF;

        /// <summary>
        ///     Invalid menu id.
        /// </summary>
        public const int InvalidMenu = 0xFF;

        /// <summary>
        ///     Invalid textdraw id.
        /// </summary>
        public const int InvalidTextDraw = 0xFFFF;

        /// <summary>
        ///     Invalid gangzone id.
        /// </summary>
        public const int InvalidGangZone = -1;

        /// <summary>
        ///     Invalid 3D textlabel id.
        /// </summary>
        public const int Invalid_3DTextId = 0xFFFF;

        /// <summary>
        ///     Max number of attachedobjects attached to a player.
        /// </summary>
        public const int MaxPlayerAttachedObjects = 10;
    }
}