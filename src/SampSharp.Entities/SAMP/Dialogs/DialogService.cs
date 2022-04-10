// SampSharp
// Copyright 2020 Tim Potze
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

using System;
using System.Threading.Tasks;

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// Provides dialog functionality.
    /// </summary>
    /// <seealso cref="IDialogService" />
    public class DialogService : IDialogService
    {
        private readonly IEntityManager _entityManager;

        /// <summary>
        /// The dialog ID used by the dialog service.
        /// </summary>
        public const int DialogId = 10000;

        /// <summary>
        /// The dialog ID used by the dialog service to hide the visible dialog.
        /// </summary>
        public const int DialogHideId = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogService" /> class.
        /// </summary>
        public DialogService(IEntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        /// <inheritdoc />
        public void Show<TResponse>(EntityId player, IDialog<TResponse> dialog, Action<TResponse> responseHandler)
            where TResponse : struct
        {
            if (!player.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            if (dialog == null)
                throw new ArgumentNullException(nameof(dialog));

            _entityManager.Destroy<VisibleDialog>(player);

            var native = _entityManager.GetComponent<NativePlayer>(player);

            native.ShowPlayerDialog(DialogId, (int) dialog.Style, dialog.Caption ?? string.Empty,
                dialog.Content ?? string.Empty, dialog.Button1 ?? string.Empty, dialog.Button2 ?? string.Empty);
            

            void Handler(DialogResult result)
            {
                // Destroy the visible dialog component before the dialog handler might replace it with a new dialog.
                _entityManager.Destroy<VisibleDialog>(player);

                var translated = dialog.Translate(result);
                responseHandler?.Invoke(translated);
            }
            
            _entityManager.AddComponent<VisibleDialog>(player, dialog, (Action<DialogResult>) Handler);
        }


        /// <inheritdoc />
        public Task<TResponse> Show<TResponse>(EntityId player, IDialog<TResponse> dialog) where TResponse : struct
        {
            if (!player.IsOfType(SampEntities.PlayerType))
                throw new InvalidEntityArgumentException(nameof(player), SampEntities.PlayerType);

            if (dialog == null) throw new ArgumentNullException(nameof(dialog));

            var taskCompletionSource = new TaskCompletionSource<TResponse>();

            Show(player, dialog, taskCompletionSource.SetResult);

            return taskCompletionSource.Task;
        }
    }
}