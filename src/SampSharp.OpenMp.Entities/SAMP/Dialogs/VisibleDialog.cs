using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// A component which contains the data of the currently visible dialog.
/// </summary>
/// <seealso cref="Component" />
/// <remarks>Initializes a new instance of the <see cref="VisibleDialog" /> class.</remarks>
/// <param name="dialog">The open.mp dialog shown to the player.</param>
/// <param name="handler">The response handler.</param>
public class VisibleDialog(IDialog dialog, Action<DialogResult> handler) : Component
{

    /// <summary>
    /// Gets the visible dialog.
    /// </summary>
    public IDialog Dialog { get; } = dialog ?? throw new ArgumentNullException(nameof(dialog));

    /// <summary>
    /// Gets the response handler for the dialog.
    /// </summary>
    public Action<DialogResult> Handler { get; } = handler ?? throw new ArgumentNullException(nameof(handler));

    /// <summary>
    /// Gets or sets a value indicating whether a response has been received.
    /// </summary>
    public bool ResponseReceived { get; set; }
    
    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (ResponseReceived)
        {
            return;
        }

        var player = GetComponent<Player>();

        if (player == null)
        {
            return;
        }
        ResponseReceived = true;
        Handler(new DialogResult(DialogResponse.RightButtonOrCancel, 0, null));

        IPlayer native = player;
        if (native.TryQueryExtension<IPlayerDialogData>(out var dialogData))
        {
            dialogData.Hide(native);
        }
    }
}