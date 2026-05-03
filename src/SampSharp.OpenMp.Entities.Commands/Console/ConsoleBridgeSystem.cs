using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands.Core;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP.Commands.Console;

/// <summary>
/// System that bridges the new Commands system to the open.mp console command event system.
/// Registers console commands and handles dispatch.
/// </summary>
internal class ConsoleBridgeSystem : ISystem
{
    private readonly ConsoleCommandService _commandService;

    public ConsoleBridgeSystem(IEntityManager entityManager, ISystemRegistry systemRegistry)
    {
        _commandService = new ConsoleCommandService(entityManager, systemRegistry);
    }

    [Event]
    public void OnConsoleCommandListRequest(ConsoleCommandCollection commands)
    {
        var registry = _commandService.GetRegistry();
        foreach (var command in registry.GetConsoleCommands())
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

        // Dispatch through command service
        var response = _commandService.Invoke(serviceProvider, sender, inputText);

        // If response is null, command succeeded silently
        if (response != null)
        {
            System.Console.WriteLine(response);
        }

        // Return true if we handled it (found the command), false otherwise
        // We determine this by whether we got a "not found" message
        var registry = _commandService.GetRegistry();
        return registry.TryFind(command) != null || (inputText.Contains(' ') && registry.TryFind(inputText.Split()[0]) != null);
    }
}
