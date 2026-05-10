using SampSharp.Entities;

namespace TestMode.OpenMp.Entities;

public class ClassNameComponent(string name) : Component
{
    public string Name { get; } = name;
}