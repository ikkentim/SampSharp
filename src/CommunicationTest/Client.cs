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
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CommunicationTest
{
    public class Client : IDisposable
    {
        private readonly TcpClient _client;
        private readonly MessageQueue _queue = new MessageQueue();
        private readonly byte[] _readBuffer = new byte[1024];
        private NetworkStream _stream;

        public Client()
        {
            _client = new TcpClient();
        }

        #region IDisposable

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        ~Client()
        {
            Dispose(false);
        }

        public static IEnumerable<byte> StringToBytes(string str)
        {
            return str.Select(c => (byte) c).Concat(new[] { (byte) 0 });
        }

        public static byte[] IntToBytes(int val)
        {
            var dat = BitConverter.GetBytes(val);

            if (!BitConverter.IsLittleEndian)
                dat = dat.Reverse().ToArray();

            return dat;
        }

        public static byte[] IntToBytes(uint val)
        {
            var dat = BitConverter.GetBytes(val);

            if (!BitConverter.IsLittleEndian)
                dat = dat.Reverse().ToArray();

            return dat;
        }

        public static uint BytesToUInt(byte[] buffer, int index)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));

            return ((uint) buffer[index] << 0) |
                   ((uint) buffer[index+1] << 8) |
                   ((uint) buffer[index+2] << 16) |
                   ((uint) buffer[index+3] << 24);
//            return ((uint)buffer[index] << 24) |
//                   ((uint)buffer[index] << 16) |
//                   ((uint)buffer[index] << 8) |
//                   (uint)buffer[index];
        }

        public static int BytesToInt(byte[] buffer, int index)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));

            return (buffer[index] << 0) |
                   (buffer[index+1] << 8) |
                   (buffer[index+2] << 16) |
                   (buffer[index+3] << 24);
//            return (buffer[index] << 24) |
//                   (buffer[index] << 16) |
//                   (buffer[index] << 8) |
//                   buffer[index];
        }

        public async Task Connect(string host, int port)
        {
            if (host == null) throw new ArgumentNullException(nameof(host));

            await _client.ConnectAsync(host, port);

            _stream = _client.GetStream();
        }

        public async Task Send(ServerCommand command, IEnumerable<byte> data)
        {
            var dataBytes = data as byte[] ?? data?.ToArray();

            var lenbytes = IntToBytes(dataBytes?.Length ?? 0);

            var buffer = new byte[5 + (dataBytes?.Length ?? 0)];
            buffer[0] = (byte) command;
            lenbytes.CopyTo(buffer, 1);
            dataBytes?.CopyTo(buffer, 5);

            await _stream.WriteAsync(buffer, 0, buffer.Length);
            await _stream.FlushAsync(); // TODO should I flush?

            if (_stream.DataAvailable)
                await Read();
        }

        private async Task Read()
        {
            var len = await _stream.ReadAsync(_readBuffer, 0, _readBuffer.Length);
            
            for (var i = 0; i < len; i++)
            {
                _queue.Add(_readBuffer[i]);
            }
        }

        public async Task<ServerCommandData> Receive()
        {
            while (!_queue.CanGet())
            {
                await Read();
            }

            (var cmd, var data) = _queue.Get();

            return new ServerCommandData
            {
                Command = (ServerCommand) cmd,
                Data = data
            };
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
                _client.Dispose();
        }
    }
}