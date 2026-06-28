using Microsoft.Extensions.ObjectPool;

namespace SampSharp.OpenMp.Core;

internal sealed class SendOrPostCallbackItem : IDisposable
{
    private readonly ManualResetEvent _asyncWaitHandle = new(false);
    private ExecutionType _executionType;
    private SendOrPostCallback? _method;
    private ObjectPool<SendOrPostCallbackItem>? _pool;
    private object? _state;

    public Exception? Exception { get; private set; }

    public WaitHandle ExecutionCompleteWaitHandle => _asyncWaitHandle;

    public void Dispose()
    {
        _asyncWaitHandle?.Dispose();
        GC.SuppressFinalize(this);
    }

    public void Set(ExecutionType executionType, SendOrPostCallback method, object? state, ObjectPool<SendOrPostCallbackItem>? returnToPool)
    {
        _executionType = executionType;
        _method = method;
        _state = state;
        _pool = returnToPool;
    }

    public void Reset()
    {
        _asyncWaitHandle.Reset();
        Exception = null;
        _method = null;
        _state = null;
        _pool = null;
    }

    public void Execute()
    {
        try
        {
            var method = _method;

            // A pooled item must never reach Execute() without a delegate. If it does, it was
            // Reset() (which nulls _method) between Set() and Execute() — a pool/lifecycle race
            // that stays rare under light load but surfaces under heavy main-thread marshalling.
            // Previously this threw a bare NullReferenceException whose only stack frame was this
            // method, which is impossible to attribute. Surface it explicitly instead, and don't
            // leave a blocked Send() caller waiting forever or tear down the tick drain.
            if (method == null)
            {
                SampSharpExceptionHandler.HandleException("async",
                    new InvalidOperationException(
                        $"{nameof(SendOrPostCallbackItem)}.{nameof(Execute)}: callback was null " +
                        $"(executionType={_executionType}). Pooled item was reset before execution."));
                _asyncWaitHandle.Set(); // unblock any waiting Send() caller; no-op for Post
                return;
            }

            if (_executionType == ExecutionType.Send)
            {
                try
                {
                    method(_state);
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
                // Fire-and-forget: nobody observes Exception on a Post item, so a throwing handler
                // would otherwise escape to the tick-drain catch as an "unhandled" exception with
                // no context. Capture it here instead.
                try
                {
                    method(_state);
                }
                catch (Exception e)
                {
                    SampSharpExceptionHandler.HandleException("async post callback", e);
                }
            }
        }
        finally
        {
            _pool?.Return(this);
        }
    }
}