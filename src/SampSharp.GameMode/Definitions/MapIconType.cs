// SampSharp
// Copyright 2017 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all map icon styles.
    /// </summary>
    /// <remarks>
    ///     See <see href="http://wiki.sa-mp.com/wiki/MapIconStyle">http://wiki.sa-mp.com/wiki/MapIconStyle</see>.
    /// </remarks>
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