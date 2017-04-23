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
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;

namespace SampSharp.Core.Communication
{
    /// <summary>
    ///     Represents a named pipe SampSharp client.
    /// </summary>
    public class PipeClient : IPipeClient
    {
        private bool _disposed;
        private readonly MessageBuffer _buffer = new MessageBuffer();
        private readonly byte[] _readBuffer = new byte[1024 * 2];
        private readonly byte[] _singleByteBuffer = new byte[1];
        private NamedPipeClientStream _stream;

        /// <summary>
        ///     Finalizes an instance of the <see cref="PipeClient" /> class.
        /// </summary>
        ~PipeClient()
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

        private void AssertNotDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(PipeClient));
        }

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
        ///     Connects the named pipe with the specified pipe name.
        /// </summary>
        /// <param name="pipeName">Name of the pipe.</param>
        /// <returns></returns>
        public async Task Connect(string pipeName)
        {
            AssertNotDisposed();

            _stream = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.WriteThrough | PipeOptions.Asynchronous);

            await _stream.ConnectAsync();
            _stream.ReadMode = PipeTransmissionMode.Byte;
        }

        /// <summary>
        ///     Sends the specified command to the named pipe.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="data">The data.</param>
        public void Send(ServerCommand command, IEnumerable<byte> data)
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
        public async Task<ServerCommandData> ReceiveAsync()
        {
            AssertNotDisposed();

            while (true)
            {
                if (_buffer.TryPop(out var command))
                    return command;

                var len = await _stream.ReadAsync(_readBuffer, 0, _readBuffer.Length);
                _buffer.Push(_readBuffer, 0, len);
            }
        }

        /// <summary>
        ///     Waits for the next command sent by the server.
        /// </summary>
        /// <returns>The command sent by the server.</returns>
        public ServerCommandData Receive()
        {
            AssertNotDisposed();

            while (true)
            {
                if (_buffer.TryPop(out var command))
                    return command;

                var len = _stream.Read(_readBuffer, 0, _readBuffer.Length);
                _buffer.Push(_readBuffer, 0, len);
            }
        }

        #endregion
    }
}