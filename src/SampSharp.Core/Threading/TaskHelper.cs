namespace SampSharp.Core;

/// <summary>
/// Provides helper methods for dealing with tasks.
/// </summary>
public static class TaskHelper
{
    /// <summary>
    /// Returns a task which, when awaited, will switch the continuation to the main thread.
    /// </summary>
    /// <returns>A task to </returns>
    public static SyncToMainThreadTask SwitchToMainThread()
    {
        return new SyncToMainThreadTask();
    }
}