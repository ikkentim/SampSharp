using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IMenu" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(IIDProvider))]
public readonly partial struct IMenu
{
    /// <summary>
    /// Sets the header text for a menu column.
    /// </summary>
    /// <param name="header">The header text to display.</param>
    /// <param name="column">The column index (0 or 1).</param>
    public partial void SetColumnHeader(string header, byte column);

    /// <summary>
    /// Adds a cell with text to a menu column.
    /// </summary>
    /// <param name="itemText">The text for the cell.</param>
    /// <param name="column">The column index (0 or 1).</param>
    /// <returns>The row index of the added cell.</returns>
    public partial int AddCell(string itemText, byte column);

    /// <summary>
    /// Disables a menu row, making it unselectable.
    /// </summary>
    /// <param name="row">The row index to disable.</param>
    public partial void DisableRow(byte row);

    /// <summary>
    /// Checks if a menu row is enabled.
    /// </summary>
    /// <param name="row">The row index to check.</param>
    /// <returns><c>true</c> if the row is enabled; otherwise, <c>false</c>.</returns>
    public partial bool IsRowEnabled(byte row);

    /// <summary>
    /// Disables the entire menu.
    /// </summary>
    public partial void Disable();

    /// <summary>
    /// Checks if the menu is enabled.
    /// </summary>
    /// <returns><c>true</c> if the menu is enabled; otherwise, <c>false</c>.</returns>
    public partial bool IsEnabled();

    /// <summary>
    /// Gets the position of the menu.
    /// </summary>
    /// <returns>A reference to the menu's position as a <see cref="System.Numerics.Vector2" />.</returns>
    public partial ref Vector2 GetPosition();

    /// <summary>
    /// Gets the number of rows in a menu column.
    /// </summary>
    /// <param name="column">The column index.</param>
    /// <returns>The number of rows in the column.</returns>
    public partial int GetRowCount(byte column);

    /// <summary>
    /// Gets the number of columns in the menu.
    /// </summary>
    /// <returns>The number of columns (typically 1 or 2).</returns>
    public partial int GetColumnCount();

    /// <summary>
    /// Gets the widths of menu columns.
    /// </summary>
    /// <param name="widths">When the method returns, contains the column widths.</param>
    private partial void GetColumnWidths(out Vector2 widths);

    /// <summary>
    /// Gets the widths of the menu columns.
    /// </summary>
    /// <returns>A <see cref="System.Numerics.Vector2" /> containing the widths of the columns.</returns>
    public Vector2 GetColumnWidths()
    {
        GetColumnWidths(out var result);
        return result;
    }

    /// <summary>
    /// Gets the header text of a menu column.
    /// </summary>
    /// <param name="column">The column index.</param>
    /// <returns>The header text, or <c>null</c> if not set.</returns>
    public partial string? GetColumnHeader(byte column);

    /// <summary>
    /// Gets the text of a menu cell.
    /// </summary>
    /// <param name="column">The column index.</param>
    /// <param name="row">The row index.</param>
    /// <returns>The cell text, or <c>null</c> if empty.</returns>
    public partial string? GetCell(byte column, byte row);

    /// <summary>
    /// Initializes the menu for a player (called before showing).
    /// </summary>
    /// <param name="player">The player to initialize the menu for.</param>
    public partial void InitForPlayer(IPlayer player);

    /// <summary>
    /// Shows the menu to a player.
    /// </summary>
    /// <param name="player">The player to show the menu to.</param>
    public partial void ShowForPlayer(IPlayer player);

    /// <summary>
    /// Hides the menu from a player.
    /// </summary>
    /// <param name="player">The player to hide the menu from.</param>
    public partial void HideForPlayer(IPlayer player);
}