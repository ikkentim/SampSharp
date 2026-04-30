using Microsoft.Extensions.ObjectPool;

namespace SampSharp.Entities;

internal sealed class EntityNodeObjectPolicy(ObjectPool<ComponentNode> componentPool) : PooledObjectPolicy<EntityNode>
{
    public override EntityNode Create()
    {
        return new EntityNode(new ComponentStore(componentPool));
    }

    public override bool Return(EntityNode obj)
    {
        obj.Reset();
        return true;
    }
}