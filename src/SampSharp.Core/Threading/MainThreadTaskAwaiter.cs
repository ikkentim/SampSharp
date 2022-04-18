// SampSharp
// Copyright 2022 Tim Potze
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
using System.Runtime.CompilerServices;

namespace SampSharp.Core;

/// <summary>
/// Represents an awaiter for the <see cref="SyncToMainThreadTask" />.
/// </summary>
/// <seealso cref="INotifyCompletion" />
public struct MainThreadTaskAwaiter : INotifyCompletion
{
    /// <summary>
    /// Gets a value indicating whether the task is completed.
    /// </summary>
    public bool IsCompleted => !InternalStorage.RunningClient.SynchronizationProvider.InvokeRequired;

    /// <summary>
    /// Gets the result of the task.
    /// </summary>
    public void GetResult()
    {
        // task never returns a result and never throws.
    }

    /// <inheritdoc />
    public void OnCompleted(Action continuation)
    {
        InternalStorage.RunningClient.SynchronizationProvider.Invoke(continuation);
    }
}