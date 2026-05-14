namespace SampSharp.Entities;

internal sealed class EntityNode(ComponentStore components)
{
    public readonly ComponentStore Components = components;

    public int ChildCount;
    public EntityNode? FirstChild;
    public EntityId Id;
    public EntityNode? Next;
    public EntityNode? Parent;

    public EntityNode? Previous;

    public bool IsEmpty => ChildCount == 0 && Components.IsEmpty;

    public void AppendChild(EntityNode child)
    {
        ChildCount++;
        if (FirstChild == null)
        {
            FirstChild = child;
            return;
        }

        // insert as first
        child.Next = FirstChild;
        FirstChild.Previous = child;
        FirstChild = child;
    }

    public void Reset()
    {
        Id = default;
        Next = null;
        Previous = null;
        Parent = null;
        FirstChild = null;
        ChildCount = 0;
        Components.ReturnComponents();
    }
}