using JetBrains.Annotations;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Marks an instance method on an <see cref="ISystem" /> as a console command.
/// The method is invoked when a command is sent via the console (from a player or server).
/// </summary>
/// <remarks>
/// The signature can be:
/// - <c>(ConsoleCommandSender sender, [args...])</c> - if the first parameter is ConsoleCommandSender, it's provided automatically
/// - <c>([args...])</c> - if no ConsoleCommandSender first parameter, treat all parameters as regular parsed parameters
/// - subsequent parameters are parsed from the console input via <see cref="ICommandParameterParser" />
/// - Return type may be <see langword="bool" />, <see langword="int" />, <see langword="void" />,
/// <see cref="Task" />, or <see cref="Task{T}" />.
/// Unlike player commands, console commands do NOT have permission checking.
/// </remarks>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
[MeansImplicitUse]
public class ConsoleCommandAttribute : Attribute, ICommandAttribute
{
    /// <summary>Initializes a new instance with the command name inferred from the method name (lowercased, trailing "Command" stripped).</summary>
    public ConsoleCommandAttribute()
    {
    }

    /// <summary>Initializes a new instance with the explicit command <paramref name="name" />.</summary>
    public ConsoleCommandAttribute(string name)
    {
        Name = name;
    }

    /// <summary>Optional localization key for the usage message. Used by ICommandTextFormatter to generate localized usage text.</summary>
    public string? UsageMessageKey { get; set; }

    /// <inheritdoc />
    public string? Name { get; set; }
}