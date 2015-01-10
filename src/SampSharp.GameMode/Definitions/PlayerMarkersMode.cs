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
    ///     Contains all PlayerMarker modes.
    /// </summary>
    public enum PlayerMarkersMode
    {
        /// <summary>
        ///     No makers.
        /// </summary>
        Off = 0,

        /// <summary>
        ///     All markers.
        /// </summary>
        Global = 1,

        /// <summary>
        ///     All markers within the streamed area.
        /// </summary>
        Streamed = 2
    }
}