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

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all racecheckpoint types.
    /// </summary>
    public enum CheckpointType
    {
        /// <summary>
        ///     Normal racecheckpoint. (Normal red circle)
        /// </summary>
        Normal = 0,

        /// <summary>
        ///     Finish racecheckpoint. (Finish flag in red circle)
        /// </summary>
        Finish = 1,

        /// <summary>
        ///     No checkpoint.
        /// </summary>
        Nothing = 2,

        /// <summary>
        ///     Air racecheckpoint. (normal red circle in the air)
        /// </summary>
        Air = 3,

        /// <summary>
        ///     Finish air racecheckpoint. (Finish flag in red circle in the air)
        /// </summary>
        AirFinish = 4
    }
}