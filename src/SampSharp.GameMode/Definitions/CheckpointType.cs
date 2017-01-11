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
    ///     Contains all race checkpoint types.
    /// </summary>
    /// <remarks>
    ///     See
    ///     <see href="http://wiki.sa-mp.com/wiki/SetPlayerRaceCheckpoint">http://wiki.sa-mp.com/wiki/SetPlayerRaceCheckpoint</see>
    ///     .
    /// </remarks>
    public enum CheckpointType
    {
        /// <summary>
        ///     Normal racecheckpoint. (Normal red cilinder)
        /// </summary>
        Normal = 0,

        /// <summary>
        ///     Finish racecheckpoint. (Finish flag in red cilinder)
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