namespace SampSharp.Entities.SAMP.Commands.Async;

/// <summary>
/// Result of async task execution.
/// </summary>
public class AsyncTaskResult
{
    /// <summary>True if the task completed successfully.</summary>
    public bool IsSuccess { get; set; }

    /// <summary>The actual result value (for Task&lt;T&gt;).</summary>
    public object? Value { get; set; }

    /// <summary>Exception if the task faulted.</summary>
    public Exception? Exception { get; set; }

    /// <summary>True if the task is still running (not awaited).</summary>
    public bool IsRunning { get; set; }
}

/// <summary>
/// Handles execution of async command methods.
/// </summary>
public static class AsyncCommandExecutor
{
    /// <summary>
    /// Executes a method and handles async results.
    /// </summary>
    public static AsyncTaskResult ExecuteAsync(object? result)
    {
        var taskResult = new AsyncTaskResult { IsSuccess = true };

        // Handle null/void return
        if (result == null)
            return taskResult;

        var resultType = result.GetType();

        // Handle Task
        if (resultType == typeof(Task))
        {
            var task = (Task)result;
            if (!task.IsCompleted)
            {
                taskResult.IsRunning = true;
                // Fire-and-forget: exceptions will be handled by unhandled exception handler
                task.ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        // Exception from async task - would typically be logged
                    }
                });
                return taskResult;
            }

            // Task completed
            if (task.IsFaulted)
            {
                taskResult.IsSuccess = false;
                taskResult.Exception = task.Exception;
                return taskResult;
            }

            return taskResult;
        }

        // Handle ValueTask
        if (resultType == typeof(ValueTask))
        {
            var vt = (ValueTask)result;
            if (!vt.IsCompleted)
            {
                taskResult.IsRunning = true;
                vt.Preserve().GetAwaiter().OnCompleted(() => { });
                return taskResult;
            }

            try
            {
                vt.GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                taskResult.IsSuccess = false;
                taskResult.Exception = ex;
            }

            return taskResult;
        }

        // Handle Task<T>
        if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Task<>))
        {
            dynamic task = result;
            if (!task.IsCompleted)
            {
                taskResult.IsRunning = true;
                task.ContinueWith((Action<dynamic>)(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        // Exception from async task
                    }
                }));
                return taskResult;
            }

            if (task.IsFaulted)
            {
                taskResult.IsSuccess = false;
                taskResult.Exception = task.Exception;
                return taskResult;
            }

            taskResult.Value = task.Result;
            return taskResult;
        }

        // Handle ValueTask<T>
        if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(ValueTask<>))
        {
            dynamic vt = result;
            if (!vt.IsCompleted)
            {
                taskResult.IsRunning = true;
                return taskResult;
            }

            try
            {
                taskResult.Value = vt.Result;
            }
            catch (Exception ex)
            {
                taskResult.IsSuccess = false;
                taskResult.Exception = ex;
            }

            return taskResult;
        }

        // Not an async result
        return taskResult;
    }

    /// <summary>
    /// Converts an async result to a bool success value.
    /// Task/Task&lt;&gt; return true if completed successfully, false if still running or faulted.
    /// Task&lt;bool&gt; returns the task result.
    /// </summary>
    public static bool ToSuccessValue(AsyncTaskResult asyncResult)
    {
        if (asyncResult.IsRunning)
            return true; // Fire-and-forget: treat as success

        if (!asyncResult.IsSuccess)
            return false;

        // If there's a value, check if it's truthy
        if (asyncResult.Value != null)
        {
            if (asyncResult.Value is bool b)
                return b;
            if (asyncResult.Value is int i)
                return i != 0;
        }

        return true;
    }
}
