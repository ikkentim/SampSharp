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
using SampSharp.Entities.SAMP.NativeComponents;

namespace SampSharp.Entities.SAMP.Dialogs
{
    /// <summary>
    /// Provides dialog functionality.
    /// </summary>
    /// <seealso cref="IDialogService" />
    public class DialogService : IDialogService
    {
        /// <summary>
        /// The dialog ID used by the dialog service.
        /// </summary>
        public const int DialogId = 10000;

        /// <summary>
        /// The dialog ID used by the dialog service to hide the visible dialog.
        /// </summary>
        public const int DialogHideId = -1;

        /// <inheritdoc />
        public void Show<TResponse>(Entity player, IDialog<TResponse> dialog, Action<TResponse> responseHandler)
            where TResponse : struct
        {
            if (dialog == null)
                throw new ArgumentNullException(nameof(dialog));

            // TODO: general todo, Check everywhere(all components) where Entity is used for type of entity.

            player.Destroy<VisibleDialog>();

            var native = player.GetComponent<NativePlayer>();

            native.ShowPlayerDialog(DialogId, (int) dialog.Style, dialog.Caption ?? string.Empty,
                dialog.Content ?? string.Empty, dialog.Button1 ?? string.Empty, dialog.Button2);

            void Handler(DialogResult result)
            {
                var translated = dialog.Translate(result);
                responseHandler?.Invoke(translated);

                player.Destroy<VisibleDialog>();
            }

            player.AddComponent<VisibleDialog>(dialog, (Action<DialogResult>) Handler);
        }


        /// <inheritdoc />
        public Task<TResponse> Show<TResponse>(Entity player, IDialog<TResponse> dialog) where TResponse : struct
        {
            if (dialog == null) throw new ArgumentNullException(nameof(dialog));

            var taskCompletionSource = new TaskCompletionSource<TResponse>();

            Show(player, dialog, taskCompletionSource.SetResult);

            return taskCompletionSource.Task;
        }
    }
}