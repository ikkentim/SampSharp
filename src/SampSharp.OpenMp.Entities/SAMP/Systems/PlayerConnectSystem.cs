using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class PlayerConnectSystem : DisposableSystem, IPlayerConnectEventHandler
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IOmpEntityProvider _entityProvider;

    public PlayerConnectSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler(x => x.GetPlayers().GetPlayerConnectDispatcher(), this));
    }

    public void OnIncomingConnection(IPlayer player, string ipAddress, ushort port)
    {
        _eventDispatcher.Invoke("OnIncomingConnection", _entityProvider.GetEntity(player), ipAddress, port);
    }

    public void OnPlayerConnect(IPlayer player)
    {
        _eventDispatcher.Invoke("OnPlayerConnect", _entityProvider.GetEntity(player));
    }

    public void OnPlayerDisconnect(IPlayer player, PeerDisconnectReason reason)
    {
        _eventDispatcher.Invoke("OnPlayerDisconnect", _entityProvider.GetEntity(player), reason);
    }

    public void OnPlayerClientInit(IPlayer player)
    {
        _eventDispatcher.Invoke("OnPlayerClientInit", _entityProvider.GetEntity(player));
    }
}