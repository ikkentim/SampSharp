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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SampSharp.Core.Communication;

namespace SampSharp.Core.Threading
{
    /// <remarks>
    ///     This implementation ONLY works for 2-thread-communication. When more threads are involved, the wait handles list will not rewind in the right order.
    /// </remarks>
    internal class CommandWaitQueue
    {
        private readonly ConcurrentQueue<ServerCommandData> _asyncQueue = new ConcurrentQueue<ServerCommandData>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0, int.MaxValue);
        private readonly ConcurrentStack<WaitEntry> _waitEntryPool = new ConcurrentStack<WaitEntry>();
        private readonly LinkedList<WaitEntry> _waitHandles = new LinkedList<WaitEntry>();
        
        private void Recycle(WaitEntry entry)
        {
            if (_waitEntryPool.Count < 20)
                _waitEntryPool.Push(entry);
        }

        public WaitEntry CreateWaitHandle(ServerCommand condition)
        {
            if (!_waitEntryPool.TryPop(out var entry))
                entry = new WaitEntry(this);

            entry.Prepare(condition);

            lock (_waitHandles)
            {
                _waitHandles.AddLast(entry);
            }

            return entry;
        }

        public async Task<ServerCommandData> WaitAsync()
        {
            for (;;)
            {
                await _semaphore.WaitAsync();
                if (_asyncQueue.TryDequeue(out var result))
                    return result;
            }
        }
        
        public void Release(ServerCommandData command)
        {
            // Find wait handle waiting for this command.
            lock (_waitHandles)
            {
                // Process the command in REVERSE order (stack) because of natives calling callbacks
                // calling natives calling callbacks... The deepest native should first receive a response.
                // When forcing an unhandled command down some wait handle (next while loop), the order does
                // not matter.
                var current = _waitHandles.Last;
                while (current != null)
                {
                    if (current.Value.Process(command, false))
                    {
                        _waitHandles.Remove(current);
                        return;
                    }

                    current = current.Previous;
                }

                // Force it down some wait handle... Can't let asyncQueue handle it because at this point
                // the main thread might be waiting for the response of a native, while the server might be
                // waiting for the response to a callback invoked by the native. Deadlocks, yay! If no wait
                // handles are available, the async queue should be able to handle this because the main thread
                // is likely idle.
                current = _waitHandles.First;
                while (current != null)
                {
                    if (current.Value.Process(command, true))
                    {
                        return;
                    }

                    current = current.Next;
                }
            }

            // If no wait handles are able to process this command, put it on the queue for when the main thread
            // is idle.
            _asyncQueue.Enqueue(command);
            _semaphore.Release();
        }

        public class WaitEntry
        {
            private readonly CommandWaitQueue _owner;
            private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0, 1);
            private ServerCommand _condition;
            private ServerCommandData _result;
            
            public WaitEntry(CommandWaitQueue owner)
            {
                _owner = owner;
            }

            public bool Process(ServerCommandData command, bool force)
            {
                if (_condition != command.Command && !force)
                    return false;

                _result = command;
                
                _semaphore.Release();

                return true;
            }

            public void Prepare(ServerCommand condition)
            {
                _condition = condition;
            }

            public ServerCommandData Wait()
            {
                _semaphore.Wait();

                var result = _result;
                _result = default(ServerCommandData);

                // If we're not waiting for this command, don't recycle this handle.
                if (result.Command == _condition)
                    _owner.Recycle(this);

                return result;
            }
        }
    }
}