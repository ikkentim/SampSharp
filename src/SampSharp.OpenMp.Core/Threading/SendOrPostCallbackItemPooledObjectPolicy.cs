using Microsoft.Extensions.ObjectPool;

namespace SampSharp.OpenMp.Core;

internal sealed class SendOrPostCallbackItemPooledObjectPolicy : PooledObjectPolicy<SendOrPostCallbackItem>
{
    public override SendOrPostCallbackItem Create()
    {
        return new SendOrPostCallbackItem();
    }

    public override bool Return(SendOrPostCallbackItem obj)
    {
        obj.Reset();
        return true;
    }
}