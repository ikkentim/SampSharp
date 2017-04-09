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

namespace SampSharp.Core.Communication
{
    /// <summary>
    ///     Contains the server commands which can be sent to and received from the SampSharp server.
    /// </summary>
    public enum ServerCommand : byte
    {
        /// <summary>
        ///     An instruction which can be sent to the server to request a <see cref="Pong" />
        /// </summary>
        Ping = 0x01,

        /// <summary>
        ///     An instruction which can be sent to the server to print a message to the console.
        /// </summary>
        Print = 0x02,

        /// <summary>
        ///     A message which can be sent to the server to deliver the response of a <see cref="PublicCall" />.
        /// </summary>
        Response = 0x03,

        /// <summary>
        ///     An instruction which can be sent to the server to indicate the client will disconnect and connect again.
        /// </summary>
        Reconnect = 0x04,

        /// <summary>
        ///     An instruction which can be sent to the server to register a callback.
        /// </summary>
        RegisterCall = 0x05,

        /// <summary>
        ///     An instruction which can be sent to the server to look a native up.
        /// </summary>
        FindNative = 0x06,

        /// <summary>
        ///     An instruction which can be sent to the server to invoke a native.
        /// </summary>
        InvokeNative = 0x07,

        /// <summary>
        ///     Once sent to the server, <see cref="Tick" /> and <see cref="PublicCall" /> calls will start being sent.
        /// </summary>
        Start = 0x08,

        /// <summary>
        ///     A call sent by the server every server tick.
        /// </summary>
        Tick = 0x11,

        /// <summary>
        ///     A reply sent by the server after a <see cref="Ping" />.
        /// </summary>
        Pong = 0x12,

        /// <summary>
        ///     A call sent by the server when a callback has been called.
        /// </summary>
        PublicCall = 0x13,

        /// <summary>
        ///     A reply sent by the server after <see cref="FindNative" /> or <see cref="InvokeNative" />.
        /// </summary>
        Reply = 0x14,

        /// <summary>
        ///     An announcement sent by the server after connecting to the server.
        /// </summary>
        Announce = 0x15
    }
}