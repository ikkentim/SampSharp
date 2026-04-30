namespace SampSharp.OpenMp.Core;

/// <summary>Represents a task which, when awaited, will switch the continuation to the main thread.</summary>
public readonly struct SyncToMainThreadTask
{
    /// <summary>Gets the awaiter for this task.</summary>
    /// <returns>The await for this task.</returns>
    public MainThreadTaskAwaiter GetAwaiter()
    {
        return new MainThreadTaskAwaiter();
    }
}