using System.Collections.Concurrent;
using Microsoft.Extensions.ObjectPool;

namespace SampSharp.OpenMp.Core;

internal class SampSharpSynchronizationContext : SynchronizationContext
{
    private readonly ObjectPool<SendOrPostCallbackItem> _pool = new DefaultObjectPool<SendOrPostCallbackItem>(new SendOrPostCallbackItemPooledObjectPolicy());
    private readonly ConcurrentQueue<SendOrPostCallbackItem> _queue = new();

    public override void Send(SendOrPostCallback d, object? state)
    {
        var item = _pool.Get();
        item.Set(ExecutionType.Send, d, state, null);

        _queue.Enqueue(item);

        item.ExecutionCompleteWaitHandle.WaitOne();

        var ex = item.Exception;

        _pool.Return(item);

        if (ex != null)
        {
            throw ex;
        }
    }

    public override void Post(SendOrPostCallback d, object? state)
    {
        // Queue the item and don't wait for its execution to complete. Let the item return itself to the pool when it's
        // done.
        var item = _pool.Get();
        item.Set(ExecutionType.Post, d, state, _pool);
        _queue.Enqueue(item);
    }

    public override SynchronizationContext CreateCopy()
    {
        // Do not copy
        return this;
    }

    public SendOrPostCallbackItem? GetMessage()
    {
        _queue.TryDequeue(out var result);
        return result;
    }
}