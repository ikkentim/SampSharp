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

internal class DialogService : IDialogService
{
    private readonly IEntityManager _entityManager;

    /// <summary>
    /// The dialog ID used by the dialog service.
    /// </summary>
    public const int DialogId = 10000;

    public DialogService(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public void Show<TResponse>(Player player, IDialog<TResponse> dialog, Action<TResponse> responseHandler) where TResponse : struct
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(dialog);
        ArgumentNullException.ThrowIfNull(responseHandler);

        _entityManager.Destroy<VisibleDialog>(player);

        IPlayer native = player;
        if (!native.TryQueryExtension<IPlayerDialogData>(out var dialogData))
        {
            throw new InvalidOperationException("Missing dialog data");
        }

        dialogData.Show(native, DialogId, (SampSharp.OpenMp.Core.Api.DialogStyle)dialog.Style, dialog.Caption ?? string.Empty, dialog.Content ?? string.Empty,
            dialog.Button1 ?? string.Empty, dialog.Button2 ?? string.Empty);

        _entityManager.AddComponent<VisibleDialog>(player, dialog, (Action<DialogResult>)Handler);
        return;

        void Handler(DialogResult result)
        {
            // Destroy the visible dialog component before the dialog handler might replace it with a new dialog.
            _entityManager.Destroy<VisibleDialog>(player);

            var translated = dialog.Translate(result);
            responseHandler?.Invoke(translated);
        }
    }
    
    public Task<TResponse> ShowAsync<TResponse>(Player player, IDialog<TResponse> dialog) where TResponse : struct
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(dialog);

        var taskCompletionSource = new TaskCompletionSource<TResponse>();

        Show(player, dialog, taskCompletionSource.SetResult);

        return taskCompletionSource.Task;
    }
}