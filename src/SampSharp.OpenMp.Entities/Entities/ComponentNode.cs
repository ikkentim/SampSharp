namespace SampSharp.Entities;

internal sealed class ComponentNode
{
    public Component? Component;
    public ComponentNode? Next;
    public ComponentNode? Previous;

    public void Reset()
    {
        Component = null;
        Previous = null;
        Next = null;
    }
}