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
    ///     Contains all types of things bullets can hit.
    /// </summary>
    /// <remarks>
    ///     See <see href="http://wiki.sa-mp.com/wiki/BulletHitTypes">http://wiki.sa-mp.com/wiki/BulletHitTypes</see>.
    /// </remarks>
    public enum BulletHitType
    {
        /// <summary>
        ///     Hit nothing.
        /// </summary>
        None = 0,

        /// <summary>
        ///     Hit a player.
        /// </summary>
        Player = 1,

        /// <summary>
        ///     Hit a vehicle.
        /// </summary>
        Vehicle = 2,

        /// <summary>
        ///     Hit an GlobalObject.
        /// </summary>
        Object = 3,

        /// <summary>
        ///     Hit a PlayerObject.
        /// </summary>
        PlayerObject = 4
    }
}