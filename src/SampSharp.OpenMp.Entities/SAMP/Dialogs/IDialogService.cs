namespace SampSharp.Entities.SAMP;

/// <summary>
/// Provides the functionality for showing dialogs to players.
/// </summary>
public interface IDialogService
{
    /// <summary>
    /// Shows the specified <paramref name="dialog" /> to the <paramref name="player" />.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response returned by the dialog.</typeparam>
    /// <param name="player">The player to show the dialog to.</param>
    /// <param name="dialog">The dialog to show to the player.</param>
    /// <param name="responseHandler">A handler for the dialog response.</param>
    void Show<TResponse>(Player player, IDialog<TResponse> dialog, Action<TResponse> responseHandler) where TResponse : struct;

    /// <summary>
    /// Shows the specified <paramref name="dialog" /> to the <paramref name="player" />.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="player">The player to show the dialog to.</param>
    /// <param name="dialog">The dialog to show to the player.</param>
    /// <returns>The dialog response.</returns>
    Task<TResponse> ShowAsync<TResponse>(Player player, IDialog<TResponse> dialog) where TResponse : struct;
}