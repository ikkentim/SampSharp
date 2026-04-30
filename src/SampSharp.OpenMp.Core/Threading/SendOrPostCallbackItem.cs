using Microsoft.Extensions.ObjectPool;

namespace SampSharp.OpenMp.Core;

internal class SendOrPostCallbackItem : IDisposable
{
    private readonly ManualResetEvent _asyncWaitHandle = new(false);
    private ExecutionType _executionType;
    private SendOrPostCallback? _method;
    private object? _state;
    private ObjectPool<SendOrPostCallbackItem>? _pool;
    
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

    public Exception? Exception { get; private set; }
        
    public WaitHandle ExecutionCompleteWaitHandle => _asyncWaitHandle;

    public void Execute()
    {
        try
        {
            if (_executionType == ExecutionType.Send)
            {
                try
                {
                    _method!(_state);
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
                _method!(_state);
            }
        }
        finally
        {
            _pool?.Return(this);
        }
    }

    public void Dispose()
    {
        _asyncWaitHandle?.Dispose();
        GC.SuppressFinalize(this);
    }
}