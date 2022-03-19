// SampSharp
// Copyright 2018 Tim Potze
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
using System.Collections.Concurrent;
using System.Threading;

namespace SampSharp.Core.Threading
{
    internal class SampSharpSynchronizationContext : SynchronizationContext
    {
        private readonly ConcurrentQueue<SendOrPostCallbackItem> _queue = new();
        
        public override void Send(SendOrPostCallback d, object state)
        {
            var item = new SendOrPostCallbackItem(d, state, ExecutionType.Send);
            _queue.Enqueue(item);

            item.ExecutionCompleteWaitHandle.WaitOne();

            if (item.ExecutedWithException)
                throw item.Exception;
        }

        public void HandleQueue(Action<SendOrPostCallbackItem> handle)
        {
            while (_queue.TryDequeue(out var item))
            {
                handle(item);
            }
        }
        
        public override void Post(SendOrPostCallback d, object state)
        {
            // Queue the item and don't wait for its execution. 
            var item = new SendOrPostCallbackItem(d, state, ExecutionType.Post);
            _queue.Enqueue(item);
        }

        public override SynchronizationContext CreateCopy() => this; // no copy support
    }
}