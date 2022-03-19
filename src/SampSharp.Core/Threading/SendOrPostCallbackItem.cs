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
using System.Threading;

namespace SampSharp.Core.Threading
{
    internal class SendOrPostCallbackItem
    {
        private readonly ManualResetEvent _asyncWaitHandle = new(false);
        private readonly ExecutionType _executionType;
        private readonly SendOrPostCallback _method;
        private readonly object _state;
        
        internal SendOrPostCallbackItem(SendOrPostCallback callback,
            object state, ExecutionType type)
        {
            _method = callback;
            _state = state;
            _executionType = type;
        }
        
        public Exception Exception { get; private set; }
        
        public bool ExecutedWithException => Exception != null;
        
        public WaitHandle ExecutionCompleteWaitHandle => _asyncWaitHandle;
        
        public void Execute()
        {
            if (_executionType == ExecutionType.Send)
            {
                try
                {
                    _method(_state);
                }
                catch (Exception e)
                {
                    Exception = e;
                }
                finally
                {
                    _asyncWaitHandle.Set();
                }
            }
            else
            {
                _method(_state);
            }
        }
    }
}