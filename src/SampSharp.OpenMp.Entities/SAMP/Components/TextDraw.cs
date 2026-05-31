using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of a text draw.
/// </summary>
public class TextDraw : IdProvider
{
    private readonly ITextDrawsComponent _textDraws;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextDraw" /> class.
    /// </summary>
    protected TextDraw(ITextDrawsComponent textDraws, ITextDraw textDraw) : base(textDraw.HasValue ? (IIDProvider)textDraw : default)
    {
        _textDraws = textDraws;
        Resource = textDraw;
    }

    private ITextDraw Resource
    {
        get
        {
            ObjectDisposedException.ThrowIf(!IsComponentAlive, typeof(TextDraw));
            return field;
        }
    }

    /// <summary>
    /// Gets or sets the size of the letters in this text draw.
    /// </summary>
    public virtual Vector2 LetterSize
    {
        get => Resource.GetLetterSize();
        set => Resource.SetLetterSize(value);
    }

    /// <summary>
    /// Gets or sets the size of this text draw box and clickable area.
    /// </summary>
    public virtual Vector2 TextSize
    {
        get => Resource.GetTextSize();
        set => Resource.SetTextSize(value);
    }

    /// <summary>
    /// Gets or sets the alignment of this text draw.
    /// </summary>
    public virtual TextDrawAlignment Alignment
    {
        get => (TextDrawAlignment)Resource.GetAlignment();
        set => Resource.SetAlignment((TextDrawAlignmentTypes)value);
    }

    /// <summary>
    /// Gets or sets the foreground <see cref="Color" /> of this text draw.
    /// </summary>
    public virtual Color ForeColor
    {
        get => Resource.GetLetterColour();
        set => Resource.SetColour(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether a box is displayed for this text draw.
    /// </summary>
    public virtual bool UseBox
    {
        get => Resource.HasBox();
        set => Resource.UseBox(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> of the box in this text draw.
    /// </summary>
    public virtual Color BoxColor
    {
        get
        {
            Resource.GetBoxColour(out var colour);
            return colour;
        }
        set => Resource.SetBoxColour(value);
    }

    /// <summary>
    /// Gets or sets the shadow size of this text draw.
    /// </summary>
    public virtual int Shadow
    {
        get => Resource.GetShadow();
        set => Resource.SetShadow(value);
    }

    /// <summary>
    /// Gets or sets the outline size of this text draw.
    /// </summary>
    public virtual int Outline
    {
        get => Resource.GetOutline();
        set => Resource.SetOutline(value);
    }

    /// <summary>
    /// Gets or sets the background <see cref="Color" /> of this text draw.
    /// </summary>
    public virtual Color BackColor
    {
        get => Resource.GetBackgroundColour();
        set => Resource.SetBackgroundColour(value);
    }

    /// <summary>
    /// Gets or sets the font of this text draw.
    /// </summary>
    public virtual TextDrawFont Font
    {
        get => (TextDrawFont)Resource.GetStyle();
        set => Resource.SetStyle((TextDrawStyle)value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the text of this text draw uses proportional spacing.
    /// </summary>
    public virtual bool Proportional
    {
        get => Resource.IsProportional();
        set => Resource.SetProportional(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this text draw is selectable by players.
    /// </summary>
    public virtual bool Selectable
    {
        get => Resource.IsSelectable();
        set => Resource.SetSelectable(value);
    }

    /// <summary>
    /// Gets or sets the text displayed in this text draw.
    /// </summary>
    public virtual string Text
    {
        get => Resource.GetText();
        set => Resource.SetText(string.IsNullOrEmpty(value) ? "_" : value);
    }

    /// <summary>
    /// Gets or sets the preview model ID displayed in this text draw.
    /// </summary>
    public virtual int PreviewModel
    {
        get => Resource.GetPreviewModel();
        set => Resource.SetPreviewModel(value);
    }

    /// <summary>
    /// Gets or sets the position of this text draw.
    /// </summary>
    public virtual Vector2 Position
    {
        get => Resource.GetPosition();
        set => Resource.SetPosition(value);
    }

    /// <summary>
    /// Gets or sets the preview model rotation of this text draw.
    /// </summary>
    public virtual Vector3 PreviewRotation
    {
        get => Resource.GetPreviewRotation();
        set => Resource.SetPreviewRotation(value);
    }

    /// <summary>
    /// Gets the preview model zoom of this text draw.
    /// </summary>
    public virtual float PreviewZoom => Resource.GetPreviewZoom();

    /// <summary>
    /// Forces this text draw to be re-sent to all players who currently have it visible.
    /// </summary>
    public virtual void Restream()
    {
        Resource.Restream();
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
        Resource.SetTextForPlayer(player, string.IsNullOrEmpty(text) ? "_" : text);
    }

    /// <summary>
    /// Sets the preview model rotation and zoom of this text draw.
    /// </summary>
    /// <param name="rotation">The rotation of the preview model as a <see cref="Vector3" />.</param>
    /// <param name="zoom">The zoom level of the preview model.</param>
    public virtual void SetPreviewRotation(Vector3 rotation, float zoom = 1.0f)
    {
        Resource.SetPreviewRotation(rotation);
        Resource.SetPreviewZoom(zoom);
    }

    /// <summary>
    /// Sets the preview vehicle colors of this text draw.
    /// </summary>
    /// <param name="color1">The primary color of the preview vehicle.</param>
    /// <param name="color2">The secondary color of the preview vehicle.</param>
    public virtual void SetPreviewVehicleColor(int color1, int color2)
    {
        Resource.SetPreviewVehicleColour(color1, color2);
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
        
        Resource.ShowForPlayer(player);
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
        
        Resource.HideForPlayer(player);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!Resource.GetExtension<ComponentExtension>().IsOmpEntityDestroyed)
        {
            _textDraws.AsPool().Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (!IsComponentAlive)
        {
            return "(Destroyed)";
        }
        return $"(Id: {Id}, Text: {Text})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="TextDraw" /> to <see cref="ITextDraw" />.
    /// </summary>
    public static implicit operator ITextDraw(TextDraw? textDraw)
    {
        return textDraw?.Resource ?? default;
    }
}