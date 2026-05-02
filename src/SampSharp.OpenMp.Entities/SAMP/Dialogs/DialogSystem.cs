using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class DialogSystem : DisposableSystem, IPlayerDialogEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public DialogSystem(IOmpEntityProvider entityProvider, IEventDispatcher eventDispatcher, SampSharpEnvironment environment)
    {
        _entityProvider = entityProvider;
        _eventDispatcher = eventDispatcher;
        AddDisposable(environment.AddEventHandler<IDialogsComponent, IPlayerDialogEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public void OnDialogResponse(IPlayer player, int dialogId, OpenMp.Core.Api.DialogResponse response, int listItem, string inputText)
    {
        _eventDispatcher.Invoke("OnDialogResponse", _entityProvider.GetEntity(player), dialogId, response, listItem, inputText);
    }

    [Event]
    public void OnPlayerDisconnect(VisibleDialog player, DisconnectReason _)
    {
        player.ResponseReceived = true;
        player.Handler(new DialogResult(DialogResponse.Disconnected, 0, null));
        player.Destroy();
    }

    [Event]
    public void OnDialogResponse(VisibleDialog player, int dialogId, DialogResponse response, int listItem, string inputText)
    {
        if (dialogId != DialogService.DialogId)
        {
            return; // Prevent dialog hacks
        }

        player.ResponseReceived = true;
        player.Handler(new DialogResult(response, listItem, inputText));

        if (!player.IsComponentAlive)
        {
            return;
        }

        player.Destroy();
    }
}