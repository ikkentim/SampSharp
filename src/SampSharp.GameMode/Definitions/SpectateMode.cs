﻿// SampSharp
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
    ///     Contains all spectating modes.
    /// </summary>
    public enum SpectateMode
    {
        /// <summary>
        ///     Normal spectating mode.
        /// </summary>
        Normal = 1,

        /// <summary>
        ///     Player is looking from a fixed point.
        /// </summary>
        Fixed = 2,

        /// <summary>
        ///     Attached to the side.
        /// </summary>
        Side = 3
    }
}