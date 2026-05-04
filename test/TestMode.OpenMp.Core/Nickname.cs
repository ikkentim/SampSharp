using SampSharp.OpenMp.Core;

namespace TestMode.OpenMp.Core;

[Extension(0x57a6f80937089f8b)]
public class Nickname : Extension
{
    public Nickname(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public override string ToString()
    {
        return $"{{name: {Name}}}";
    }
}