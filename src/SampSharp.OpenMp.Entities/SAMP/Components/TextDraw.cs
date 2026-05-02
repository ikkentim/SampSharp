using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of a text draw.
/// </summary>
public class TextDraw : IdProvider
{
    private readonly ITextDrawsComponent _textDraws;
    private readonly ITextDraw _textDraw;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextDraw" /> class.
    /// </summary>
    protected TextDraw(ITextDrawsComponent textDraws, ITextDraw textDraw) : base((IIDProvider)textDraw)
    {
        _textDraws = textDraws;
        _textDraw = textDraw;
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _textDraw.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>
    /// Gets or sets the size of the letters in this text draw.
    /// </summary>
    public virtual Vector2 LetterSize
    {
        get => _textDraw.GetLetterSize();
        set => _textDraw.SetLetterSize(value);
    }

    /// <summary>
    /// Gets or sets the size of this text draw box and clickable area.
    /// </summary>
    public virtual Vector2 TextSize
    {
        get => _textDraw.GetTextSize();
        set => _textDraw.SetTextSize(value);
    }

    /// <summary>
    /// Gets or sets the alignment of this text draw.
    /// </summary>
    public virtual TextDrawAlignment Alignment
    {
        get => (TextDrawAlignment)_textDraw.GetAlignment();
        set => _textDraw.SetAlignment((TextDrawAlignmentTypes)value);
    }

    /// <summary>
    /// Gets or sets the foreground <see cref="Color" /> of this text draw.
    /// </summary>
    public virtual Color ForeColor
    {
        get => _textDraw.GetLetterColour();
        set => _textDraw.SetColour(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether a box is displayed for this text draw.
    /// </summary>
    public virtual bool UseBox
    {
        get => _textDraw.HasBox();
        set => _textDraw.UseBox(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> of the box in this text draw.
    /// </summary>
    public virtual Color BoxColor
    {
        get
        {
            _textDraw.GetBoxColour(out var colour);
            return colour;
        }
        set => _textDraw.SetBoxColour(value);
    }

    /// <summary>
    /// Gets or sets the shadow size of this text draw.
    /// </summary>
    public virtual int Shadow
    {
        get => _textDraw.GetShadow();
        set => _textDraw.SetShadow(value);
    }

    /// <summary>
    /// Gets or sets the outline size of this text draw.
    /// </summary>
    public virtual int Outline
    {
        get => _textDraw.GetOutline();
        set => _textDraw.SetOutline(value);
    }

    /// <summary>
    /// Gets or sets the background <see cref="Color" /> of this text draw.
    /// </summary>
    public virtual Color BackColor
    {
        get => _textDraw.GetBackgroundColour();
        set => _textDraw.SetBackgroundColour(value);
    }

    /// <summary>
    /// Gets or sets the font of this text draw.
    /// </summary>
    public virtual TextDrawFont Font
    {
        get => (TextDrawFont)_textDraw.GetStyle();
        set => _textDraw.SetStyle((TextDrawStyle)value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the text of this text draw uses proportional spacing.
    /// </summary>
    public virtual bool Proportional
    {
        get => _textDraw.IsProportional();
        set => _textDraw.SetProportional(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this text draw is selectable by players.
    /// </summary>
    public virtual bool Selectable
    {
        get => _textDraw.IsSelectable();
        set => _textDraw.SetSelectable(value);
    }

    /// <summary>
    /// Gets or sets the text displayed in this text draw.
    /// </summary>
    public virtual string Text
    {
        get => _textDraw.GetText();
        set => _textDraw.SetText(string.IsNullOrEmpty(value) ? "_" : value);
    }

    /// <summary>
    /// Gets or sets the preview model ID displayed in this text draw.
    /// </summary>
    public virtual int PreviewModel
    {
        get => _textDraw.GetPreviewModel();
        set => _textDraw.SetPreviewModel(value);
    }

    /// <summary>
    /// Gets or sets the position of this text draw.
    /// </summary>
    public virtual Vector2 Position
    {
        get => _textDraw.GetPosition();
        set => _textDraw.SetPosition(value);
    }

    /// <summary>
    /// Gets or sets the preview model rotation of this text draw.
    /// </summary>
    public virtual Vector3 PreviewRotation
    {
        get => _textDraw.GetPreviewRotation();
        set => _textDraw.SetPreviewRotation(value);
    }

    /// <summary>
    /// Gets the preview model zoom of this text draw.
    /// </summary>
    public virtual float PreviewZoom => _textDraw.GetPreviewZoom();

    /// <summary>
    /// Forces this text draw to be re-sent to all players who currently have it visible.
    /// </summary>
    public virtual void Restream()
    {
        _textDraw.Restream();
    }

    /// <summary>
    /// Updates the displayed text of this text draw for a single <paramref name="player" /> without hiding/showing it.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="text">The new text.</param>
    public virtual void SetTextForPlayer(Player player, string text)
    {
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(text);
        _textDraw.SetTextForPlayer(player, string.IsNullOrEmpty(text) ? "_" : text);
    }

    /// <summary>
    /// Sets the preview model rotation and zoom of this text draw.
    /// </summary>
    /// <param name="rotation">The rotation of the preview model as a <see cref="Vector3" />.</param>
    /// <param name="zoom">The zoom level of the preview model.</param>
    public virtual void SetPreviewRotation(Vector3 rotation, float zoom = 1.0f)
    {
        _textDraw.SetPreviewRotation(rotation);
        _textDraw.SetPreviewZoom(zoom);
    }

    /// <summary>
    /// Sets the preview vehicle colors of this text draw.
    /// </summary>
    /// <param name="color1">The primary color of the preview vehicle.</param>
    /// <param name="color2">The secondary color of the preview vehicle.</param>
    public virtual void SetPreviewVehicleColor(int color1, int color2)
    {
        _textDraw.SetPreviewVehicleColour(color1, color2);
    }

    /// <summary>
    /// Shows this text draw to all players.
    /// </summary>
    public virtual void Show()
    {
        foreach (var player in Manager.GetComponents<Player>())
        {
            Show(player);
        }
    }

    /// <summary>
    /// Shows this text draw to the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The <see cref="Player" /> to show this text draw to.</param>
    public virtual void Show(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        
        _textDraw.ShowForPlayer(player);
    }

    /// <summary>
    /// Hides this text draw for all players.
    /// </summary>
    public virtual void Hide()
    {
        foreach (var player in Manager.GetComponents<Player>())
        {
            Hide(player);
        }
    }

    /// <summary>
    /// Hides this text draw for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player to hide this text draw from.</param>
    public virtual void Hide(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        
        _textDraw.HideForPlayer(player);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            _textDraws.AsPool().Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id}, Text: {Text})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="TextDraw" /> to <see cref="ITextDraw" />.
    /// </summary>
    public static implicit operator ITextDraw(TextDraw textDraw)
    {
        return textDraw._textDraw;
    }
}