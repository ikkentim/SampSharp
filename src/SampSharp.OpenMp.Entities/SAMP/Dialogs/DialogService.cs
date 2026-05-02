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

        dialogData.Show(native, DialogId, (OpenMp.Core.Api.DialogStyle)dialog.Style, dialog.Caption ?? string.Empty, dialog.Content ?? string.Empty,
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