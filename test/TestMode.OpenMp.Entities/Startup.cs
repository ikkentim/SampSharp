using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;
using SampSharp.OpenMp.Core;

namespace TestMode.OpenMp.Entities;

public class Startup : IEcsStartup
{
    public void Initialize(IStartupContext context)
    {
        context.UseEntities()
            .UseCommands();
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TestSampSharpOptions>(configuration.GetSection("sampsharp"))
            .AddSingleton<HelpService>();

    }

    public void Configure(IEcsBuilder builder)
    {
        builder.Services.GetRequiredService<ILogger<Startup>>()
            .LogDebug("This is a debug log message!");
    }
}

public class HelpService(IPlayerCommandService commandService, ICommandTextFormatter commandTextFormatter)
{
    public void Send(Player player, CommandGroup group)
    {
        SendFor(player, group);

        foreach (var g in commandService.Registry.GetGroups(group))
        {
            SendFor(player, g);
        }
    }

    public void Send(Player player)
    {
        player.SendClientMessage(Color.DarkOliveGreen, "--- Available Commands ---");

        var playerCommands = commandService.Registry.GetAll()
            .OrderBy(c => c.FullName)
            .ToList();

        foreach (var cmd in playerCommands)
        {
            var aliases = cmd.Aliases.Count > 0 ? $" ({string.Join(", ", cmd.Aliases.Select(a => $"/{a.Name}"))})" : "";
            var frmt = commandTextFormatter.FormatCommandUsage(cmd.Name, cmd.Group.ToString(), cmd.ParsedParameters);

            player.SendClientMessage($"{frmt}{aliases}");
        }
    }

    private void SendFor(Player player, CommandGroup group)
    {
        var cmds = commandService.Registry.GetCommandsInGroup(group);

        player.SendClientMessage(Color.DarkOliveGreen, $"--- Commands in /{group.FullName} ---");
        foreach (var cmd in cmds)
        {
            var aliases = cmd.Aliases.Count > 0 ? $" ({string.Join(", ", cmd.Aliases.Select(a => $"/{a.Name}"))})" : "";
            var text = commandTextFormatter.FormatCommandUsage(cmd.Name, cmd.Group.ToString(), cmd.ParsedParameters);
            player.SendClientMessage($"{text}{aliases}");
        }
    }
}
