using JetBrains.Annotations;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Marks an instance method on an <see cref="ISystem" /> as a player command.
/// The method is invoked when a connected player sends matching chat input
/// (a slash followed by the command name and optional arguments).
/// </summary>
/// <remarks>
/// The signature can be:
/// - <c>(Player player, [args...])</c> - if the first parameter is Player, it's provided automatically
/// - <c>([args...])</c> - if no Player first parameter, treat all parameters as regular parsed parameters
/// - subsequent parameters are parsed from the player input via <see cref="ICommandParameterParser" />
/// - Return type may be <see langword="void" />, <see langword="Task" />, or <see cref="Task" />.
/// </remarks>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
[MeansImplicitUse]
public class PlayerCommandAttribute : Attribute, ICommandAttribute
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

    /// <summary>Optional localization key for the usage message. Used by ICommandNameProvider to generate localized usage text.</summary>
    public string? UsageMessageKey { get; set; }
}
