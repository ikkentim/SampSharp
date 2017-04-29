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
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SampSharp.Core.Communication.Clients
{
    /// <summary>
    ///     Represents a Unix Domain Socket communictaion client.
    /// </summary>
    public class UnixDomainSocketCommunicationClient : StreamCommunicationClient
    {
        private readonly string _path;
        private Socket _socket;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TcpCommunicationClient" /> class.
        /// </summary>
        /// <param name="path">The path to the domain socket.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="path" /> is null.</exception>
        public UnixDomainSocketCommunicationClient(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        #region Overrides of StreamCommunicationClient

        /// <summary>
        ///     Returns a newly created and connected stream for this client.
        /// </summary>
        /// <returns>A newly created and connected stream for this client.</returns>
        protected override async Task<Stream> CreateStream()
        {
            var retry = false;

            for (;;)
            {
                try
                {
                    if (retry)
                    {
                        await Task.Delay(1000);
                    }

                    _socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.IP);
                    var unixEp = new UnixEndPoint(_path);
                    await _socket.ConnectAsync(unixEp);

                    return new NetworkStream(_socket, true);
                }
                catch (SocketException)
                {
                    retry = true;
                }
            }
        }

        #endregion
    }
}