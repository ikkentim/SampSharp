using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of a per-player text draw.
/// </summary>
public class PlayerTextDraw : IdProvider
{
    private readonly IPlayerTextDrawData _playerTextDraws;
    private readonly IPlayerTextDraw _playerTextDraw;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerTextDraw" /> class.
    /// </summary>
    protected PlayerTextDraw(IPlayerTextDrawData playerTextDraws, IPlayerTextDraw playerTextDraw) : base((IIDProvider)playerTextDraw)
    {
        _playerTextDraws = playerTextDraws;
        _playerTextDraw = playerTextDraw;
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _playerTextDraw.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>
    /// Gets or sets the size of the letters in this text draw.
    /// </summary>
    public virtual Vector2 LetterSize
    {
        get => _playerTextDraw.GetLetterSize();
        set => _playerTextDraw.SetLetterSize(value);
    }

    /// <summary>
    /// Gets or sets the size of this text draw box and clickable area.
    /// </summary>
    public virtual Vector2 TextSize
    {
        get => _playerTextDraw.GetTextSize();
        set => _playerTextDraw.SetTextSize(value);
    }

    /// <summary>
    /// Gets or sets the alignment of this text draw.
    /// </summary>
    public virtual TextDrawAlignment Alignment
    {
        get => (TextDrawAlignment)_playerTextDraw.GetAlignment();
        set => _playerTextDraw.SetAlignment((TextDrawAlignmentTypes)value);
    }

    /// <summary>
    /// Gets or sets the foreground <see cref="Color" /> of this text draw.
    /// </summary>
    public virtual Color ForeColor
    {
        get => _playerTextDraw.GetLetterColour();
        set => _playerTextDraw.SetColour(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether a box is displayed for this text draw.
    /// </summary>
    public virtual bool UseBox
    {
        get => _playerTextDraw.HasBox();
        set => _playerTextDraw.UseBox(value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> of the box in this text draw.
    /// </summary>
    public virtual Color BoxColor
    {
        get
        {
            _playerTextDraw.GetBoxColour(out var colour);
            return colour;
        }
        set => _playerTextDraw.SetBoxColour(value);
    }

    /// <summary>
    /// Gets or sets the shadow size of this text draw.
    /// </summary>
    public virtual int Shadow
    {
        get => _playerTextDraw.GetShadow();
        set => _playerTextDraw.SetShadow(value);
    }

    /// <summary>
    /// Gets or sets the outline size of this text draw.
    /// </summary>
    public virtual int Outline
    {
        get => _playerTextDraw.GetOutline();
        set => _playerTextDraw.SetOutline(value);
    }

    /// <summary>
    /// Gets or sets the background <see cref="Color" /> of this text draw.
    /// </summary>
    public virtual Color BackColor
    {
        get => _playerTextDraw.GetBackgroundColour();
        set => _playerTextDraw.SetBackgroundColour(value);
    }

    /// <summary>
    /// Gets or sets the font of this text draw.
    /// </summary>
    public virtual TextDrawFont Font
    {
        get => (TextDrawFont)_playerTextDraw.GetStyle();
        set => _playerTextDraw.SetStyle((TextDrawStyle)value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the text of this text draw uses proportional spacing.
    /// </summary>
    public virtual bool Proportional
    {
        get => _playerTextDraw.IsProportional();
        set => _playerTextDraw.SetProportional(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this text draw is selectable by the player.
    /// </summary>
    public virtual bool Selectable
    {
        get => _playerTextDraw.IsSelectable();
        set => _playerTextDraw.SetSelectable(value);
    }

    /// <summary>
    /// Gets or sets the text displayed in this text draw.
    /// </summary>
    public virtual string Text
    {
        get => _playerTextDraw.GetText();
        set => _playerTextDraw.SetText(string.IsNullOrEmpty(value) ? "_" : value);
    }

    /// <summary>
    /// Gets or sets the preview model ID displayed in this text draw.
    /// </summary>
    public virtual int PreviewModel
    {
        get => _playerTextDraw.GetPreviewModel();
        set => _playerTextDraw.SetPreviewModel(value);
    }

    /// <summary>
    /// Gets or sets the position of this text draw.
    /// </summary>
    public virtual Vector2 Position
    {
        get => _playerTextDraw.GetPosition();
        set => _playerTextDraw.SetPosition(value);
    }

    /// <summary>
    /// Gets or sets the preview model rotation of this text draw.
    /// </summary>
    public virtual Vector3 PreviewRotation
    {
        get => _playerTextDraw.GetPreviewRotation();
        set => _playerTextDraw.SetPreviewRotation(value);
    }

    /// <summary>
    /// Gets the preview model zoom of this text draw.
    /// </summary>
    public virtual float PreviewZoom => _playerTextDraw.GetPreviewZoom();

    /// <summary>
    /// Forces this text draw to be re-sent to the player.
    /// </summary>
    public virtual void Restream()
    {
        _playerTextDraw.Restream();
    }

    /// <summary>
    /// Sets the preview model rotation and zoom of this text draw.
    /// </summary>
    /// <param name="rotation">The rotation of the preview model.</param>
    /// <param name="zoom">The zoom level of the preview model.</param>
    public virtual void SetPreviewRotation(Vector3 rotation, float zoom = 1.0f)
    {
        _playerTextDraw.SetPreviewRotation(rotation);
        _playerTextDraw.SetPreviewZoom(zoom);
    }

    /// <summary>
    /// Sets the preview vehicle colors of this text draw.
    /// </summary>
    /// <param name="color1">The primary color of the preview vehicle.</param>
    /// <param name="color2">The secondary color of the preview vehicle.</param>
    public virtual void SetPreviewVehicleColor(int color1, int color2)
    {
        _playerTextDraw.SetPreviewVehicleColour(color1, color2);
    }

    /// <summary>
    /// Shows this text draw for the player.
    /// </summary>
    public virtual void Show()
    {
        _playerTextDraw.Show();
    }

    /// <summary>
    /// Hides this text draw for the player.
    /// </summary>
    public virtual void Hide()
    {
        _playerTextDraw.Hide();
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            _playerTextDraws.AsPool().Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id}, Text: {Text})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="PlayerTextDraw" /> to <see cref="IPlayerTextDraw" />.
    /// </summary>
    public static implicit operator IPlayerTextDraw(PlayerTextDraw playerTextDraw)
    {
        return playerTextDraw._playerTextDraw;
    }
}