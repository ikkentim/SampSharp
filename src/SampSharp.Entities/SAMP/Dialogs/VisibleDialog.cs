using System;
using SampSharp.Entities.SAMP.Definitions;
using SampSharp.Entities.SAMP.NativeComponents;

namespace SampSharp.Entities.SAMP.Dialogs
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
            var component = Entity.GetComponent<NativePlayer>();

            if (!ResponseReceived)
            {
                ResponseReceived = true;
                Handler(new DialogResult(DialogResponse.RightButtonOrCancel, 0, null));

                component?.ShowPlayerDialog(DialogService.DialogHideId, (int) DialogStyle.MessageBox, " ", " ", " ",
                    " ");
            }
        }
    }
}