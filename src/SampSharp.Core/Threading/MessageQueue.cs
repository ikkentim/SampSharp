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
    public class MessageQueue
    {
        private readonly ManualResetEvent _killThread = new ManualResetEvent(false);
        private readonly Queue<SendOrPostCallbackItem> _queue = new Queue<SendOrPostCallbackItem>();
        private readonly Semaphore _semaphore = new Semaphore(0, int.MaxValue);
        private readonly WaitHandle[] _waitHandles;

        public MessageQueue()
        {
            _waitHandles = new WaitHandle[] { _semaphore, _killThread };
        }

        public void Enqueue(SendOrPostCallbackItem data)
        {
            lock (_queue)
            {
                _queue.Enqueue(data);
            }
            _semaphore.Release();
        }

        public SendOrPostCallbackItem Dequeue()
        {
            WaitHandle.WaitAny(_waitHandles);
            lock (_queue)
            {
                if (_queue.Count > 0)
                    return _queue.Dequeue();
            }
            return default(SendOrPostCallbackItem);
        }

        public void ReleaseReader()
        {
            _killThread.Set();
        }
    }
}