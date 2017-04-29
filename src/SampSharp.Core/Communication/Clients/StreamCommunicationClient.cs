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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SampSharp.Core.Logging;

namespace SampSharp.Core.Communication.Clients
{
    /// <summary>
    ///     Represents a base class for communication clients based on a <see cref="Stream" />. The stream must support
    ///     <see cref="Stream.ReadAsync(byte[], int, int, System.Threading.CancellationToken)" />,
    ///     <see cref="Stream.Write(byte[], int, int)" /> and <see cref="Stream.Flush" />.
    /// </summary>
    public abstract class StreamCommunicationClient : ICommunicationClient
    {
        private readonly MessageBuffer _buffer = new MessageBuffer();
        private readonly byte[] _readBuffer = new byte[1024 * 2];
        private readonly byte[] _singleByteBuffer = new byte[1];
        private bool _disposed;
        private Stream _stream;

        /// <summary>
        ///     Finalizes an instance of the <see cref="NamedPipeClient" /> class.
        /// </summary>
        ~StreamCommunicationClient()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected void Dispose(bool disposing)
        {
            _disposed = true;
            if (disposing)
                _stream.Dispose();
        }

        /// <summary>
        ///     Throws a <see cref="ObjectDisposedException" /> if this instance has been disposed of.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Thrown if this instance has been disposed of.</exception>
        protected void AssertNotDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(NamedPipeClient));
        }

        /// <summary>
        ///     Returns a newly created and connected stream for this client.
        /// </summary>
        /// <returns>A newly created and connected stream for this client.</returns>
        protected abstract Task<Stream> CreateStream();

        #region IDisposable

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Implementation of IPipeClient

        /// <summary>
        ///     Connects the client to the server.
        /// </summary>
        /// <returns></returns>
        public virtual async Task Connect()
        {
            AssertNotDisposed();
            _stream = await CreateStream();
        }

        /// <summary>
        ///     Disconnects this client from the server.
        /// </summary>
        public virtual void Disconnect()
        {
            AssertNotDisposed();

            _buffer.Clear();
            _stream.Dispose();
            _stream = null;
        }

        /// <summary>
        ///     Sends the specified command to the named pipe.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="data">The data.</param>
        public virtual void Send(ServerCommand command, IEnumerable<byte> data)
        {
            AssertNotDisposed();

            var dataBytes = data as byte[] ?? data?.ToArray();
            var length = dataBytes?.Length ?? 0;
            var lenbytes = ValueConverter.GetBytes(length);

            _singleByteBuffer[0] = (byte) command;
            _stream.Write(_singleByteBuffer, 0, 1);
            _stream.Write(lenbytes, 0, 4);

            if (dataBytes != null)
                _stream.Write(dataBytes, 0, dataBytes.Length);

            _stream.Flush(); // TODO should I flush?
        }

        /// <summary>
        ///     Waits for the next command sent by the server.
        /// </summary>
        /// <returns>The command sent by the server.</returns>
        public virtual async Task<ServerCommandData> ReceiveAsync()
        {
            AssertNotDisposed();

            while (true)
            {
                if (_buffer.TryPop(out var command))
                    return command;

                // TODO: This breaks on disconnect; _stream is set to null.
                var len = await _stream.ReadAsync(_readBuffer, 0, _readBuffer.Length);
                _buffer.Push(_readBuffer, 0, len);
            }
        }

        #endregion
    }
}