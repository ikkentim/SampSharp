﻿// SampSharp
// Copyright (C) 04 Tim Potze
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

using System;

namespace SampSharp.GameMode.Definitions
{
    /// <summary>
    ///     Contains all actionkeys.
    /// </summary>
    [Flags]
    public enum Keys
    {
        /// <summary>
        ///     The action key. (Default: Tab, onfoot. ALT GR / LCTRL / NUM 0, invehicle)
        /// </summary>
        Action = 1,

        /// <summary>
        ///     The crouch key. (Default: C, onfoot, H / CAPSLOCK, invehicle)
        /// </summary>
        Crouch = 2,

        /// <summary>
        ///     The fire key. (Default: LCTRL / LMB, onfoot. LALT, invehicle)
        /// </summary>
        Fire = 4,

        /// <summary>
        ///     The sprint key. (Default: SPACE, onfoot. W, invehicle)
        /// </summary>
        Sprint = 8,

        /// <summary>
        ///     Secondary attack key. (Default: ENTER, onfoot. ENTER, invehicle)
        /// </summary>
        SecondaryAttack = 16,

        /// <summary>
        ///     Jump key. (Default: LSHIFT, onfoot)
        /// </summary>
        Jump = 32,

        /// <summary>
        ///     Look right key. (Default: E, invehicle)
        /// </summary>
        LookRight = 64,

        /// <summary>
        ///     Handbrake key. (Default: RMB, onfoot. SPACE, invehicle)
        /// </summary>
        Handbrake = 128,

        /// <summary>
        ///     Aim key. (Default: RMB, onfoot. SPACE, invehicle)
        /// </summary>
        Aim = 128,

        /// <summary>
        ///     Look left key. (Default: Q, invehicle)
        /// </summary>
        LookLeft = 256,

        /// <summary>
        ///     Submission key. (Default: NUM 1 / MMB, onfoot. 2 / NUM +, invehicle)
        /// </summary>
        Submission = 512,

        /// <summary>
        ///     Look behind key, look left + look right combined. (Default: NUM 1 / MMB, onfoot. 2, invehicle)
        /// </summary>
        LookBehind = 512,

        /// <summary>
        ///     Walk key. (Default: LALT, onfoot)
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
        ///     Controll back key. (Default: H)
        /// </summary>
        CtrlBack = 262144,
    }
}