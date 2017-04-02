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

using System.Collections.Generic;

namespace SampSharp.Core
{
    /// <summary>
    ///     A buffer of data which can be translated into server messages.
    /// </summary>
    internal class MessageQueue
    {
        private readonly Queue<byte> _queue = new Queue<byte>(1000);

        private byte _command;
        private uint _commandLength;
        private bool _localFill;

        /// <summary>
        ///     Tries to pop a server command from the buffer.
        /// </summary>
        /// <param name="command">The popped command.</param>
        /// <returns>true if a command has been popped of the buffer; false otherwise.</returns>
        public bool TryPop(out ServerCommandData command)
        {
            if (!CanGet())
            {
                command = default(ServerCommandData);
                return false;
            }

            var data = new byte[_commandLength];
            for (var i = 0; i < _commandLength; i++)
            {
                data[i] = _queue.Dequeue();
            }

            _localFill = false;

            command = new ServerCommandData((ServerCommand) _command, data);
            return true;
        }

        /// <summary>
        ///     Pushes the specified value onto the buffer.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Push(byte value)
        {
            _queue.Enqueue(value);
        }

        private bool TryFillLocal()
        {
            if (_localFill)
                return true;
            if (_queue.Count < 5)
                return false;

            _command = _queue.Dequeue();

            _commandLength = (uint) (
                (_queue.Dequeue() << 0) |
                (_queue.Dequeue() << 8) |
                (_queue.Dequeue() << 16) |
                (_queue.Dequeue() << 24));

            _localFill = true;

            return true;
        }

        private bool CanGet()
        {
            if (!TryFillLocal())
                return false;

            return _commandLength <= _queue.Count;
        }
    }
}