namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// System that bridges the new Commands system to the open.mp console command event system.
/// Registers console commands and handles dispatch.
/// </summary>
internal class ConsoleBridgeSystem : ISystem
{
    private readonly ConsoleCommandService _commandService;

    public ConsoleBridgeSystem(ConsoleCommandService commandService)
    {
        _commandService = commandService;
    }

    [Event]
    public void OnConsoleCommandListRequest(ConsoleCommandCollection commands)
    {
        var registry = _commandService.Commands.Registry;
        foreach (var command in registry.GetAll())
        {
            commands.Add(command.Name);

            // Also register group-qualified names if they have a group
            if (command.Group.HasValue)
            {
                commands.Add(command.Group.Value.FullName + " " + command.Name);
            }
        }
    }

    [Event]
    public bool OnConsoleText(string command, string args, ConsoleCommandSender sender, IServiceProvider serviceProvider)
    {
        // Build input text from command and args
        var inputText = string.IsNullOrEmpty(args) ? command : $"{command} {args}";

        // Create a dispatch context with message handler to send responses back
        var context = new ConsoleCommandDispatchContext(sender.Player, msg => Console.WriteLine(msg)); // TODO: ConsoleCommandSender is missing a messageSender

        // Dispatch through command service
        var success = _commandService.Invoke(serviceProvider, context, inputText);

        return success;
    }
}