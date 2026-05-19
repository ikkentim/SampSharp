using SampSharp.Entities;

namespace TestMode.OpenMp.Entities.Components;

public class ClassNameComponent(string name) : Component
{
    public string Name { get; } = name;
}