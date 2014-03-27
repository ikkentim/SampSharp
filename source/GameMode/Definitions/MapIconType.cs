﻿// SampSharp
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
    public enum MapIconType
    {
        /// <summary>
        ///     Displays in the player's local are.
        /// </summary>
        Local = 0,

        /// <summary>
        ///     Displays always.
        /// </summary>
        Global = 1,

        /// <summary>
        ///     Displays in the player's local area and has a checkpoint marker.
        /// </summary>
        LocalCheckPoint = 2,

        /// <summary>
        ///     Displays always and has a checkpoint marker.
        /// </summary>
        GlobalCheckPoint = 3
    }
}