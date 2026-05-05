using SampSharp.OpenMp.Core;

namespace SampSharp.Entities.SAMP.Commands;

internal static class AsyncCommandExecutor
{
    public static bool ExecuteAsync(object? result)
    {
        // Handle null/void return
        if (result == null)
        {
            return true;
        }

        var resultType = result.GetType();

        // Handle Task
        if (resultType == typeof(Task))
        {
            return HandleTask((Task)result);
        }

        // Handle ValueTask
        if (resultType == typeof(ValueTask))
        {
            var vt = (ValueTask)result;
            if (!vt.IsCompleted)
            {
                return HandleTask(vt.AsTask());
            }

            vt.GetAwaiter().GetResult();
            return true;
        }

        // Handle Task<T>
        if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Task<>))
        {
            dynamic task = result;
            if (!task.IsCompleted)
            {
                task.ContinueWith((Action<dynamic>)(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        // Exception from async task
                    }
                }));

                return true;
            }

            if (task.IsFaulted)
            {
                task.GetAwaiter().GetResult();
            }

            return task.Result is not bool b || b;
        }

        // Handle ValueTask<T>
        if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(ValueTask<>))
        {
            dynamic vt = result;
            if (!vt.IsCompleted)
            {
                Task t = vt.AsTask();
                return HandleTask(t);
            }

            return vt.Result is not bool b || b;
        }

        // Not an async result
        return true;
    }

    private static bool HandleTask(Task task)
    {
        if (!task.IsCompleted)
        {
            // Fire-and-forget: exceptions will be handled by unhandled exception handler
            task.ContinueWith(t =>
            {
                if (t is { IsFaulted: true, Exception: not null })
                {
                    // Exception from async task - would typically be logged
                    // TODO: IUnhandledExceptionHandler
                    SampSharpExceptionHandler.HandleException("async-command", t.Exception);
                }
            });

            return true;
        }

        task.GetAwaiter().GetResult();
        return true;
    }
}