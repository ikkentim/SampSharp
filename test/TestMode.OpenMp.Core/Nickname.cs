using SampSharp.OpenMp.Core;

namespace TestMode.OpenMp.Core;

[Extension(0x57a6f80937089f8b)]
public class Nickname(string name) : Extension
{
    public string Name { get; } = name;

    public override string ToString()
    {
        return $"{{name: {Name}}}";
    }
}