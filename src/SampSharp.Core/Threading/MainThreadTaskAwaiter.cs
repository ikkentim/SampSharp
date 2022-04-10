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