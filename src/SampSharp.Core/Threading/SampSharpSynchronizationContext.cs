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
using System.Threading;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents a synchronization context for the SampSharp main thread.
    /// </summary>
    /// <seealso cref="SynchronizationContext" />
    internal class SampSharpSynchronizationContext : SynchronizationContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SampSharpSynchronizationContext" /> class.
        /// </summary>
        public SampSharpSynchronizationContext(IMessageQueue messageQueue)
        {
            MessageQueue = messageQueue ?? throw new ArgumentNullException(nameof(messageQueue));
        }

        /// <summary>
        /// Gets the message queue to which calls are en-queued.
        /// </summary>
        public IMessageQueue MessageQueue { get; }
        
        /// <summary>
        ///     When overridden in a derived class, dispatches a synchronous message to a synchronization context.
        /// </summary>
        /// <param name="d">The <see cref="SendOrPostCallback" /> delegate to call.</param>
        /// <param name="state">The object passed to the delegate. </param>
        public override void Send(SendOrPostCallback d, object state)
        {
            var item = new SendOrPostCallbackItem(d, state, ExecutionType.Send);
            MessageQueue.PushMessage(item);

            item.ExecutionCompleteWaitHandle.WaitOne();

            if (item.ExecutedWithException)
                throw item.Exception;
        }
        
        /// <summary>
        ///     When overridden in a derived class, dispatches an asynchronous message to a synchronization context.
        /// </summary>
        /// <param name="d">The <see cref="SendOrPostCallback" /> delegate to call.</param>
        /// <param name="state">The object passed to the delegate.</param>
        public override void Post(SendOrPostCallback d, object state)
        {
            // Queue the item and don't wait for its execution. 
            var item = new SendOrPostCallbackItem(d, state, ExecutionType.Post);
            MessageQueue.PushMessage(item);
        }

        /// <summary>
        ///     When overridden in a derived class, creates a copy of the synchronization context.
        /// </summary>
        /// <returns>A new <see cref="SynchronizationContext" /> object.</returns>
        public override SynchronizationContext CreateCopy()
        {
            // Do not copy
            return this;
        }
    }
}