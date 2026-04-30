using Microsoft.Extensions.ObjectPool;

namespace SampSharp.Entities;

internal sealed class ComponentNodeObjectPolicy : PooledObjectPolicy<ComponentNode>
{
    public override ComponentNode Create()
    {
        return new ComponentNode();
    }

    public override bool Return(ComponentNode obj)
    {
        obj.Reset();
        return true;
    }
}