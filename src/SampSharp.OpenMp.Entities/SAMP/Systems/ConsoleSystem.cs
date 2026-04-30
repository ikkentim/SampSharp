using SampSharp.OpenMp.Core.Api;
using SampSharp.OpenMp.Core.RobinHood;

namespace SampSharp.Entities.SAMP;

internal class ConsoleSystem : DisposableSystem, IConsoleEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public ConsoleSystem(IOmpEntityProvider entityProvider, IEventDispatcher eventDispatcher, SampSharpEnvironment environment)
    {
        _entityProvider = entityProvider;
        _eventDispatcher = eventDispatcher;
        AddDisposable(environment.AddEventHandler<IConsoleComponent, IConsoleEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public bool OnConsoleText(string command, string parameters, ref ConsoleCommandSenderData sender)
    {
        var player = sender.Player.HasValue 
            ? _entityProvider.GetComponent(sender.Player.Value) 
            : null;
        var isCustom = sender.Sender == SampSharp.OpenMp.Core.Api.ConsoleCommandSender.Custom;
        var isConsole = sender.Sender == SampSharp.OpenMp.Core.Api.ConsoleCommandSender.Console;

        return _eventDispatcher.InvokeAs("OnConsoleText", false, command, parameters, new ConsoleCommandSender(player, isConsole, isCustom));
    }

    public void OnRconLoginAttempt(IPlayer player, string password, bool success)
    {
        _eventDispatcher.Invoke("OnRconLoginAttempt", _entityProvider.GetEntity(player), password, success);
    }

    public void OnConsoleCommandListRequest(FlatHashSetStringView commands)
    {
        var collection = new ConsoleCommandCollection(commands);

        _eventDispatcher.Invoke("OnConsoleCommandListRequest", collection);
    }
}