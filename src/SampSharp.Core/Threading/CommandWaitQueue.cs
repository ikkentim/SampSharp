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

                // Force it down some wait handle.
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

            // Else provide to asyncs

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

                if (result.Command == _condition)
                    _owner.Recycle(this);

                return result;
            }
        }
    }
}