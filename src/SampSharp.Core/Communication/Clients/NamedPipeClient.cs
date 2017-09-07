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
using System.IO.Pipes;
using System.Threading.Tasks;

namespace SampSharp.Core.Communication.Clients
{
    /// <summary>
    ///     Represents a named pipe communication client.
    /// </summary>
    public class NamedPipeClient : StreamCommunicationClient
    {
        private readonly string _pipeName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="NamedPipeClient" /> class.
        /// </summary>
        /// <param name="pipeName">Name of the pipe.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="pipeName" /> is null.</exception>
        public NamedPipeClient(string pipeName)
        {
            _pipeName = pipeName ?? throw new ArgumentNullException(nameof(pipeName));
        }

        #region Overrides of StreamCommunicationClient

        /// <summary>
        ///     Returns a newly created and connected stream for this client.
        /// </summary>
        /// <returns>A newly created and connected stream for this client.</returns>
        protected override async Task<Stream> CreateStream()
        {
            var stream = new NamedPipeClientStream(".", _pipeName, PipeDirection.InOut, PipeOptions.WriteThrough | PipeOptions.Asynchronous);

            await stream.ConnectAsync();
            stream.ReadMode = PipeTransmissionMode.Byte;

            return stream;
        }

        #endregion

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"named pipe on {_pipeName}";
        }

        #endregion
    }
}