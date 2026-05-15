namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a console command sender.
/// </summary>
/// <param name="Player">The player that sent the command.</param>
/// <param name="IsConsole">A value indicating whether the command was sent by the console or a script.</param>
/// <param name="IsCustom">A value indicating whether the command was sent by as custom component.</param>
/// <param name="HandleConsoleMessage">A function which can be used to return a console message to the sender.</param>
[EventParameter]
public record ConsoleCommandSender(Player? Player, bool IsConsole, bool IsCustom, Action<string>? HandleConsoleMessage);