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

using System.Collections.Generic;
using System.Threading;

namespace SampSharp.Core.Threading
{
    /// <summary>
    ///     Represents a message queue for messages sent to a <see cref="SampSharpSyncronizationContext" />.
    /// </summary>
    public class MessageQueue
    {
        private readonly Queue<SendOrPostCallbackItem> _queue = new Queue<SendOrPostCallbackItem>();
        private readonly Semaphore _semaphore = new Semaphore(0, int.MaxValue);// TODO: Is the semaphore reset properly?
        private readonly ManualResetEvent _stopSignal = new ManualResetEvent(false);
        private readonly WaitHandle[] _waitHandles;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageQueue" /> class.
        /// </summary>
        public MessageQueue()
        {
            _waitHandles = new WaitHandle[] { _semaphore, _stopSignal };
        }

        /// <summary>
        ///     Pushes the specified item onto this instance.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public void PushMessage(SendOrPostCallbackItem item)
        {
            lock (_queue)
            {
                _queue.Enqueue(item);
            }
            _semaphore.Release();
        }

        /// <summary>
        ///     Pops an item from this instance. If none is avaiable, this call will wait for an item to be pushed.
        /// </summary>
        /// <returns>The next message, or null if canceled.</returns>
        public SendOrPostCallbackItem WaitForMessage()
        {
            // Wait for a signal (send by Enqueue or ReleaseReader)
            WaitHandle.WaitAny(_waitHandles);

            // Dequeue from the internal queue.
            lock (_queue)
            {
                if (_queue.Count > 0)
                    return _queue.Dequeue();
            }

            // If no item was dequeued, a stop signal must have been sent, reset the signal back for the next read and return a default value.
            _stopSignal.Reset();

            return null;
        }

        /// <summary>
        ///     Releases the reader waiting in a <see cref="WaitForMessage" />.
        /// </summary>
        public void ReleaseReader()
        {
            _stopSignal.Set();
        }
    }
}