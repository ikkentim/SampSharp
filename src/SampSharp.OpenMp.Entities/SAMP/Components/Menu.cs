using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a component which provides the data and functionality of a menu.</summary>
public class Menu : IdProvider
{
    private readonly IMenusComponent _menus;
    private readonly IMenu _menu;
    
    /// <summary>Constructs an instance of Menu, should be used internally.</summary>
    protected Menu(IMenusComponent menus, IMenu menu, string title) : base((IIDProvider)menu)
    {
        _menus = menus;
        _menu = menu;
        Title = title; // no getter available in IMenu
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _menu.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>Gets the title of this menu.</summary>
    public virtual string Title { get; }

    /// <summary>Gets the number of columns in this menu.</summary>
    public virtual int Columns => _menu.GetColumnCount();

    /// <summary>Gets the position of this menu.</summary>
    public virtual Vector2 Position => _menu.GetPosition();

    /// <summary>Gets the width of the left column in this menu.</summary>
    public virtual float Col0Width => _menu.GetColumnWidths().X;

    /// <summary>Gets the width of the right column in this menu.</summary>
    public virtual float Col1Width => _menu.GetColumnWidths().Y;

    /// <summary>Gets or sets the caption of the left column in this menu.</summary>
    public virtual string Col0Header
    {
        get => _menu.GetColumnHeader(0) ?? string.Empty;
        set => _menu.SetColumnHeader(value, 0);
    }

    /// <summary>Gets or sets the caption of the right column in this menu.</summary>
    public virtual string Col1Header
    {
        get => _menu.GetColumnHeader(1) ?? string.Empty;
        set => _menu.SetColumnHeader(value, 1);
    }

    /// <summary>Adds an item to this menu.</summary>
    /// <param name="col0Text">The text for the left column.</param>
    /// <param name="col1Text">The text for the right column. If this menu only has one column, this value is ignored.</param>
    /// <returns>The index of the row this item was added to.</returns>
    /// <remarks>
    /// You can only have 12 items per menu (13th goes to the right side of the header of column name (colored), 14th and higher not display at all). Maximum
    /// length of menu item is 31 symbols.
    /// </remarks>
    public virtual int AddItem(string col0Text, string? col1Text = null)
    {
        ArgumentNullException.ThrowIfNull(col0Text);

        if (col1Text == null && Columns == 2)
        {
            throw new ArgumentNullException(nameof(col1Text), "The text for the right column may not be null because this menu has 2 columns.");
        }

        var result = _menu.AddCell(col0Text, 0);

        if (Columns == 2)
        {
            _menu.AddCell(col1Text!, 1);
        }

        return result;
    }

    /// <summary>Shows this menu for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    public virtual void Show(Player player)
    {
        _menu.ShowForPlayer(player);
    }

    /// <summary>Hides this menu for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player.</param>
    public virtual void Hide(Player player)
    {
        _menu.HideForPlayer(player);
    }

    /// <summary>Disable input for this menu. Any item will lose the ability to be selected.</summary>
    public virtual void Disable()
    {
        _menu.Disable();
    }

    /// <summary>Disable a specific row in this menu for all players. It will be grayed-out and can't be selected by players.</summary>
    /// <param name="row">The index of the row to disable.</param>
    public virtual void DisableRow(int row)
    {
        _menu.DisableRow((byte)row);
    }
    
    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            _menus.AsPool().Release(Id);
        }
    }
    
    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id}, Title: {Title})";
    }

    /// <summary>Performs an implicit conversion from <see cref="Menu" /> to <see cref="IMenu" />.</summary>
    public static implicit operator IMenu(Menu menu)
    {
        return menu._menu;
    }
}