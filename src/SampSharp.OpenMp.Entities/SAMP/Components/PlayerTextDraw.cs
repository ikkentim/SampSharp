using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a component which provides the data and functionality of a per-player text draw.</summary>
public class PlayerTextDraw : IdProvider
{
    private readonly IPlayerTextDrawData _playerTextDraws;
    private readonly IPlayerTextDraw _playerTextDraw;

    /// <summary>Constructs an instance of <see cref="PlayerTextDraw" />, should be used internally.</summary>
    protected PlayerTextDraw(IPlayerTextDrawData playerTextDraws, IPlayerTextDraw playerTextDraw) : base((IIDProvider)playerTextDraw)
    {
        _playerTextDraws = playerTextDraws;
        _playerTextDraw = playerTextDraw;
    }
    
    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _playerTextDraw.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>Gets or sets the size of the letters of this text draw.</summary>
    public virtual Vector2 LetterSize
    {
        get => _playerTextDraw.GetLetterSize();
        set => _playerTextDraw.SetLetterSize(value);
    }

    /// <summary>Gets or sets the size of this text draw box and click-able area.</summary>
    public virtual Vector2 TextSize
    {
        get => _playerTextDraw.GetTextSize();
        set => _playerTextDraw.SetTextSize(value);
    }

    /// <summary>Gets or sets the alignment of this text draw.</summary>
    public virtual TextDrawAlignment Alignment
    {
        get => (TextDrawAlignment)_playerTextDraw.GetAlignment();
        set => _playerTextDraw.SetAlignment((TextDrawAlignmentTypes)value);
    }

    /// <summary>Gets or sets the color of the text of this text draw.</summary>
    public virtual Color ForeColor
    {
        get => _playerTextDraw.GetLetterColour();
        set => _playerTextDraw.SetColour(value);
    }

    /// <summary>Gets or sets a value indicating whether a box is used for this text draw.</summary>
    public virtual bool UseBox
    {
        get => _playerTextDraw.HasBox();
        set => _playerTextDraw.UseBox(value);
    }

    /// <summary>Gets or sets the color of the box of this text draw.</summary>
    public virtual Color BoxColor
    {
        get
        {
            _playerTextDraw.GetBoxColour(out var colour);
            return colour;
        }
        set => _playerTextDraw.SetBoxColour(value);
    }

    /// <summary>Gets or sets the shadow size of this text draw.</summary>
    public virtual int Shadow
    {
        get => _playerTextDraw.GetShadow();
        set => _playerTextDraw.SetShadow(value);
    }

    /// <summary>Gets or sets the outline size of this text draw.</summary>
    public virtual int Outline
    {
        get => _playerTextDraw.GetOutline();
        set => _playerTextDraw.SetOutline(value);
    }

    /// <summary>Gets or sets the background color of this text draw.</summary>
    public virtual Color BackColor
    {
        get => _playerTextDraw.GetBackgroundColour();
        set => _playerTextDraw.SetBackgroundColour(value);
    }

    /// <summary>Gets or sets the font of this text draw.</summary>
    public virtual TextDrawFont Font
    {
        get => (TextDrawFont)_playerTextDraw.GetStyle();
        set => _playerTextDraw.SetStyle((TextDrawStyle)value);
    }

    /// <summary>Gets or sets a value indicating whether the font of this text draw is rendered as a monospaced font.</summary>
    public virtual bool Proportional
    {
        get => _playerTextDraw.IsProportional();
        set => _playerTextDraw.SetProportional(value);
    }

    /// <summary>Gets or sets a value indicating whether this text draw is selectable by the player.</summary>
    public virtual bool Selectable
    {
        get => _playerTextDraw.IsSelectable();
        set => _playerTextDraw.SetSelectable(value);
    }

    /// <summary>Gets or sets the text of this text draw.</summary>
    public virtual string Text
    {
        get => _playerTextDraw.GetText();
        set => _playerTextDraw.SetText(string.IsNullOrEmpty(value) ? "_" : value);
    }

    /// <summary>Gets or sets the preview model of this text draw.</summary>
    public virtual int PreviewModel
    {
        get => _playerTextDraw.GetPreviewModel();
        set => _playerTextDraw.SetPreviewModel(value);
    }

    /// <summary>Gets the position of this text draw.</summary>
    public virtual Vector2 Position => _playerTextDraw.GetPosition();


    /// <summary>Sets the preview object rotation and zoom of this text draw.</summary>
    /// <param name="rotation">The rotation of the preview object.</param>
    /// <param name="zoom">The zoom of the preview object.</param>
    public virtual void SetPreviewRotation(Vector3 rotation, float zoom = 1.0f)
    {
        _playerTextDraw.SetPreviewRotation(rotation);
        _playerTextDraw.SetPreviewZoom(zoom);
    }

    /// <summary>Sets the color of the preview vehicle of this text draw.</summary>
    /// <param name="color1">The primary color of the vehicle.</param>
    /// <param name="color2">The secondary color of the vehicle.</param>
    public virtual void SetPreviewVehicleColor(int color1, int color2)
    {
        _playerTextDraw.SetPreviewVehicleColour(color1, color2);
    }

    /// <summary>Shows this text draw for the player.</summary>
    public virtual void Show()
    {
        _playerTextDraw.Show();
    }

    /// <summary>Hides this text draw for the player.</summary>
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
    
    /// <summary>Performs an implicit conversion from <see cref="PlayerTextDraw" /> to <see cref="IPlayerTextDraw" />.</summary>
    public static implicit operator IPlayerTextDraw(PlayerTextDraw playerTextDraw)
    {
        return playerTextDraw._playerTextDraw;
    }
}