// SampSharp
// Copyright 2022 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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

    public void OnDialogResponse(IPlayer player, int dialogId, SampSharp.OpenMp.Core.Api.DialogResponse response, int listItem, string inputText)
    {
        _eventDispatcher.Invoke("OnDialogResponse", _entityProvider.GetEntity(player), dialogId, response, listItem, inputText);
    }
}