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
    ///     Contains all limit definitions.
    /// </summary>
    public static class Limits
    {
        /// <summary>
        ///     This is the number of attached indexes available ie 10 = 0-9
        /// </summary>
        public const int MaxPlayerAttachedObjects = 10;

        /// <summary>
        ///     The maximum length of chatbubble text.
        /// </summary>
        public const int MaxChatbubbleLength = 144;

        /// <summary>
        ///     The maximum length of a playername.
        /// </summary>
        public const int MaxPlayerName = 24;

        /// <summary>
        ///     The maximum number of players.
        /// </summary>
        public const int MaxPlayers = 500;

        /// <summary>
        ///     The maximum number of vehicles.
        /// </summary>
        public const int MaxVehicles = 2000;

        /// <summary>
        ///     The maximum number of global objects.
        /// </summary>
        public const int MaxObjects = 1000;

        /// <summary>
        ///     The maximum number of gangzones.
        /// </summary>
        public const int MaxGangZones = 1024;

        /// <summary>
        ///     The maximum number of textdraws.
        /// </summary>
        public const int MaxTextDraws = 2048;

        /// <summary>
        ///     The maximum number of player-textdraws.
        /// </summary>
        public const int MaxPlayerTextDraws = 256;

        /// <summary>
        ///     The maximum number of menus.
        /// </summary>
        public const int MaxMenus = 128;

        /// <summary>
        ///     The maximum number of global 3D textlabels.
        /// </summary>
        public const int Max_3DTextGlobal = 1024;

        /// <summary>
        ///     The maximum number of player 3D textlabels.
        /// </summary>
        public const int Max_3DTextPlayer = 1024;

        /// <summary>
        ///     The maximum number of pickups.
        /// </summary>
        public const int MaxPickups = 4096;
    }
}