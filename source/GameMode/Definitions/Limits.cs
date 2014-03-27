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
    public static class Limits
    {
        /// <summary>
        ///     This is the number of attached indexes available ie 10 = 0-9
        /// </summary>
        public const int MaxPlayerAttachedObjects = 10;

        public const int MaxChatbubbleLength = 144;
        public const int MaxPlayerName = 24;
        public const int MaxPlayers = 500;
        public const int MaxVehicles = 2000;
        public const int MaxObjects = 1000;
        public const int MaxGangZones = 1024;
        public const int MaxTextDraws = 2048;
        public const int MaxPlayerTextDraws = 256;
        public const int MaxMenus = 128;
        public const int Max_3DTextGlobal = 1024;
        public const int Max_3DTextPlayer = 1024;
        public const int MaxPickups = 4096;
    }
}