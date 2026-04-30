using System.Runtime.CompilerServices;

namespace SampSharp.OpenMp.Core;

/// <summary>Represents an awaiter for the <see cref="SyncToMainThreadTask" />.</summary>
/// <seealso cref="INotifyCompletion" />
public readonly struct MainThreadTaskAwaiter : INotifyCompletion
{
    /// <summary>Gets a value indicating whether the task is completed.</summary>
    public bool IsCompleted => SynchronizationContextExtension.Active.IsMainThread();

    /// <summary>Gets the result of the task.</summary>
    public void GetResult()
    {
        // task never returns a result and never throws.
    }

    /// <inheritdoc />
    public void OnCompleted(Action continuation)
    {
        ArgumentNullException.ThrowIfNull(continuation);

        if (SynchronizationContextExtension.Active.IsMainThread())
        {
            continuation();
        }

        SynchronizationContextExtension.Active.Invoke(continuation);
    }
}