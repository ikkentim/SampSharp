// SampSharp
// Copyright 2020 Tim Potze
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

namespace SampSharp.Entities.SAMP.Definitions
{
    /// <summary>
    /// Contains the reasons a player could disconnect.
    /// </summary>
    public enum PlayerDisconnectReason
    {
        /// <summary>
        /// The player timed out.
        /// </summary>
        TimedOut = 0,

        /// <summary>
        /// The player left.
        /// </summary>
        Left = 1,

        /// <summary>
        /// The player was kicked.
        /// </summary>
        Kicked = 2
    }
}