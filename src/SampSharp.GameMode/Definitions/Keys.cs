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
using System;

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all detectable keys.
    /// </summary>
    /// <remarks>
    ///     See <see href="https://www.open.mp/docs/scripting/resources/keys">https://www.open.mp/docs/scripting/resources/keys</see>.
    /// </remarks>
    [Flags]
    public enum Keys
    {
        /// <summary>
        ///     The action key. (Default: Tab, on-foot. ALT GR / L-CTRL / NUM 0, in-vehicle)
        /// </summary>
        Action = 1,

        /// <summary>
        ///     The crouch key. (Default: C, on-foot, H / CAPS-LOCK, in-vehicle)
        /// </summary>
        Crouch = 2,

        /// <summary>
        ///     The fire key. (Default: L-CTRL / LMB, on-foot. L-ALT, in-vehicle)
        /// </summary>
        Fire = 4,

        /// <summary>
        ///     The sprint key. (Default: SPACE, on-foot. W, in-vehicle)
        /// </summary>
        Sprint = 8,

        /// <summary>
        ///     Secondary attack key. (Default: ENTER, on-foot. ENTER, in-vehicle)
        /// </summary>
        SecondaryAttack = 16,

        /// <summary>
        ///     Jump key. (Default: L-SHIFT, on-foot)
        /// </summary>
        Jump = 32,

        /// <summary>
        ///     Look right key. (Default: E, in-vehicle)
        /// </summary>
        LookRight = 64,

        /// <summary>
        ///     Handbrake key. (Default: RMB, on-foot. SPACE, in-vehicle)
        /// </summary>
        Handbrake = 128,

        /// <summary>
        ///     Aim key. (Default: RMB, on-foot. SPACE, in-vehicle)
        /// </summary>
        Aim = 128,

        /// <summary>
        ///     Look left key. (Default: Q, in-vehicle)
        /// </summary>
        LookLeft = 256,

        /// <summary>
        ///     Submission key. (Default: NUM 1 / MMB, on-foot. 2 / NUM +, in-vehicle)
        /// </summary>
        Submission = 512,

        /// <summary>
        ///     Look behind key, look left + look right combined. (Default: NUM 1 / MMB, on-foot. 2, in-vehicle)
        /// </summary>
        LookBehind = 512,

        /// <summary>
        ///     Walk key. (Default: L-ALT, on-foot)
        /// </summary>
        Walk = 1024,

        /// <summary>
        ///     Analog up key. (Default: NUM 8)
        /// </summary>
        AnalogUp = 2048,

        /// <summary>
        ///     Analog down key. (Default: NUM 2)
        /// </summary>
        AnalogDown = 4096,

        /// <summary>
        ///     Analog left key. (Default: NUM 4)
        /// </summary>
        AnalogLeft = 8192,

        /// <summary>
        ///     Analog right key. (Default: NUM 6)
        /// </summary>
        AnalogRight = 16384,

        /// <summary>
        ///     Yes key. (Default: Y)
        /// </summary>
        Yes = 65536,

        /// <summary>
        ///     No key. (Default: N)
        /// </summary>
        No = 131072,

        /// <summary>
        ///     Control back key. (Default: H)
        /// </summary>
        CtrlBack = 262144
    }
}