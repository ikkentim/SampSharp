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

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.IncomingConnection" /> event.
    /// </summary>
    public class ConnectionEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the ConnectionEventArgs class.
        /// </summary>
        /// <param name="playerid">Id of the player trying to connect.</param>
        /// <param name="ipAddress">Ip of the connection.</param>
        /// <param name="port">Port of the connection.</param>
        public ConnectionEventArgs(int playerid, string ipAddress, int port)
        {
            PlayerId = playerid;
            IpAddress = ipAddress;
            Port = port;
        }

        // Not using Player here as we don't delete the object when
        // the player fails to connect to the server.

        /// <summary>
        ///     Gets the id of the player trying to connect.
        /// </summary>
        public int PlayerId { get; private set; }

        /// <summary>
        ///     Gets the ip of this connection.
        /// </summary>
        public string IpAddress { get; private set; }

        /// <summary>
        ///     Gets the port of this connection.
        /// </summary>
        public int Port { get; private set; }
    }
}