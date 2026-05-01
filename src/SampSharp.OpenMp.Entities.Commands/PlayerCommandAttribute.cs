using JetBrains.Annotations;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Marks an instance method on an <see cref="ISystem" /> as a player command.
/// The method is invoked when a connected player sends matching chat input
/// (a slash followed by the command name and optional arguments).
/// </summary>
/// <remarks>
/// The signature is <c>(Player player, [args...])</c>. The first parameter
/// must be the invoking <see cref="Player" /> (or <see cref="EntityId" />);
/// subsequent parameters are parsed from the chat input via
/// <see cref="Parsers.ICommandParameterParser" />. Return type may be
/// <see langword="bool" />, <see langword="int" /> or <see langword="void" />.
/// </remarks>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
[MeansImplicitUse]
public class PlayerCommandAttribute : Attribute, ICommandMethodInfo
{
    /// <summary>Initializes a new instance with the command name inferred from the method name (lowercased, trailing "Command" stripped).</summary>
    public PlayerCommandAttribute()
    {
    }

    /// <summary>Initializes a new instance with the explicit command <paramref name="name" />.</summary>
    public PlayerCommandAttribute(string name)
    {
        Name = name;
    }

    /// <inheritdoc />
    public string? Name { get; set; }
}
