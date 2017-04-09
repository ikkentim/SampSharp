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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampSharp.Core.Communication
{
    /// <summary>
    ///     Contains the methods a named pipe SampSharp client.
    /// </summary>
    public interface IPipeClient : IDisposable
    {
        /// <summary>
        ///     Connects the named pipe with the specified pipe name.
        /// </summary>
        /// <param name="pipeName">Name of the pipe.</param>
        /// <returns></returns>
        Task Connect(string pipeName);

        /// <summary>
        ///     Sends the specified command to the named pipe.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        void Send(ServerCommand command, IEnumerable<byte> data);

        /// <summary>
        ///     Waits for the next command sent by the server.
        /// </summary>
        /// <returns>The command sent by the server.</returns>
        Task<ServerCommandData> ReceiveAsync();

        /// <summary>
        ///     Waits for the next command sent by the server.
        /// </summary>
        /// <returns>The command sent by the server.</returns>
        ServerCommandData Receive();
    }
}