using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of a 3D text label.
/// </summary>
public class TextLabel : WorldEntity
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly ITextLabelsComponent _textLabels;
    private readonly ITextLabel _textLabel;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextLabel" /> class.
    /// </summary>
    protected TextLabel(IOmpEntityProvider entityProvider, ITextLabelsComponent textLabels, ITextLabel textLabel) : base((IEntity)textLabel)
    {
        _entityProvider = entityProvider;
        _textLabels = textLabels;
        _textLabel = textLabel;
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _textLabel.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>
    /// Gets or sets the <see cref="Color" /> of this text label.
    /// </summary>
    public virtual Color Color
    {
        get
        {
            _textLabel.GetColour(out var colour);
            return colour;
        }
        set => _textLabel.SetColour(value);
    }

    /// <summary>
    /// Gets or sets the text displayed in this text label.
    /// </summary>
    public virtual string Text
    {
        get => _textLabel.GetText();
        set => _textLabel.SetText(value);
    }

    /// <summary>
    /// Gets the draw distance of this text label.
    /// </summary>
    public virtual float DrawDistance => _textLabel.GetDrawDistance();

    /// <summary>
    /// Gets a value indicating whether line-of-sight testing is enabled for this text label.
    /// </summary>
    public virtual bool TestLos => _textLabel.GetTestLOS();

    /// <summary>
    /// Gets the entity this text label is attached to, if any.
    /// </summary>
    public virtual Component? AttachedEntity
    {
        get
        {
            var attachmentData = _textLabel.GetAttachmentData();

            if (attachmentData.PlayerId != OpenMpConstants.INVALID_PLAYER_ID)
            {
                return _entityProvider.GetPlayer(attachmentData.PlayerId);
            }

            if (attachmentData.VehicleId != OpenMpConstants.INVALID_VEHICLE_ID)
            {
                return _entityProvider.GetVehicle(attachmentData.VehicleId);
            }

            return null;
        }
    }

    /// <summary>
    /// Attaches this text label to the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The <see cref="Player" /> to attach this text label to.</param>
    /// <param name="offset">The offset position relative to the player as a <see cref="Vector3" />.</param>
    public virtual void Attach(Player player, Vector3 offset = default)
    {
        ArgumentNullException.ThrowIfNull(player);
        
        _textLabel.AttachToPlayer(player, offset);
    }

    /// <summary>
    /// Attaches this text label to the specified <paramref name="vehicle" />.
    /// </summary>
    /// <param name="vehicle">The <see cref="Vehicle" /> to attach this text label to.</param>
    /// <param name="offset">The offset position relative to the vehicle as a <see cref="Vector3" />.</param>
    public virtual void Attach(Vehicle vehicle, Vector3 offset = default)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        
        _textLabel.AttachToVehicle(vehicle, offset);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            _textLabels.AsPool().Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id}, Text: {Text})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="TextLabel" /> to <see cref="ITextLabel" />.
    /// </summary>
    public static implicit operator ITextLabel(TextLabel textLabel)
    {
        return textLabel._textLabel;
    }
}