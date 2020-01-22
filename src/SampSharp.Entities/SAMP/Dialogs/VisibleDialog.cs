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

namespace SampSharp.Entities.SAMP
{
    /// <summary>
    /// A component which contains the data of the currently visible dialog.
    /// </summary>
    /// <seealso cref="SampSharp.Entities.Component" />
    public class VisibleDialog : Component
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VisibleDialog" /> class.
        /// </summary>
        public VisibleDialog(IDialog dialog, Action<DialogResult> handler)
        {
            Dialog = dialog ?? throw new ArgumentNullException(nameof(dialog));
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        /// <summary>
        /// Gets the visible dialog.
        /// </summary>
        public IDialog Dialog { get; }

        /// <summary>
        /// Gets the response handler for the dialog.
        /// </summary>
        public Action<DialogResult> Handler { get; }

        /// <summary>
        /// Gets or sets a value indicating whether a response has been received.
        /// </summary>
        public bool ResponseReceived { get; set; }

        /// <inheritdoc />
        protected override void OnDestroyComponent()
        {
            var component = GetComponent<NativePlayer>();

            if (ResponseReceived)
                return;

            ResponseReceived = true;
            Handler(new DialogResult(DialogResponse.RightButtonOrCancel, 0, null));

            component?.ShowPlayerDialog(DialogService.DialogHideId, (int) DialogStyle.MessageBox, " ", " ", " ", " ");
        }
    }
}