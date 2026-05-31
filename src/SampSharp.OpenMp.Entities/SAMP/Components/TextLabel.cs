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

    /// <summary>
    /// Initializes a new instance of the <see cref="TextLabel" /> class.
    /// </summary>
    protected TextLabel(IOmpEntityProvider entityProvider, ITextLabelsComponent textLabels, ITextLabel textLabel) : base(textLabel.HasValue ? (IEntity)textLabel : default)
    {
        _entityProvider = entityProvider;
        _textLabels = textLabels;
        Resource = textLabel;
    }

    private ITextLabel Resource
    {
        get
        {
            ObjectDisposedException.ThrowIf(!IsComponentAlive, typeof(TextLabel));
            return field;
        }
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> of this text label.
    /// </summary>
    public virtual Color Color
    {
        get
        {
            Resource.GetColour(out var colour);
            return colour;
        }
        set => Resource.SetColour(value);
    }

    /// <summary>
    /// Gets or sets the text displayed in this text label.
    /// </summary>
    public virtual string Text
    {
        get => Resource.GetText();
        set => Resource.SetText(value);
    }

    /// <summary>
    /// Gets the draw distance of this text label.
    /// </summary>
    public virtual float DrawDistance => Resource.GetDrawDistance();

    /// <summary>
    /// Gets a value indicating whether line-of-sight testing is enabled for this text label.
    /// </summary>
    public virtual bool TestLos => Resource.GetTestLOS();

    /// <summary>
    /// Gets the entity this text label is attached to, if any.
    /// </summary>
    public virtual Component? AttachedEntity
    {
        get
        {
            var attachmentData = Resource.GetAttachmentData();

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
    /// Gets the <see cref="Player" /> this text label is attached to, or <see langword="null" /> if it is not attached to a player.
    /// </summary>
    public virtual Player? AttachedPlayer => _entityProvider.GetPlayer(Resource.GetAttachmentData().PlayerId);

    /// <summary>
    /// Gets the <see cref="Vehicle" /> this text label is attached to, or <see langword="null" /> if it is not attached to a vehicle.
    /// </summary>
    public virtual Vehicle? AttachedVehicle => _entityProvider.GetVehicle(Resource.GetAttachmentData().VehicleId);

    /// <summary>
    /// Attaches this text label to the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The <see cref="Player" /> to attach this text label to.</param>
    /// <param name="offset">The offset position relative to the player as a <see cref="Vector3" />.</param>
    public virtual void Attach(Player player, Vector3 offset = default)
    {
        ArgumentNullException.ThrowIfNull(player);
        
        Resource.AttachToPlayer(player, offset);
    }

    /// <summary>
    /// Attaches this text label to the specified <paramref name="vehicle" />.
    /// </summary>
    /// <param name="vehicle">The <see cref="Vehicle" /> to attach this text label to.</param>
    /// <param name="offset">The offset position relative to the vehicle as a <see cref="Vector3" />.</param>
    public virtual void Attach(Vehicle vehicle, Vector3 offset = default)
    {
        ArgumentNullException.ThrowIfNull(vehicle);
        
        Resource.AttachToVehicle(vehicle, offset);
    }

    /// <summary>
    /// Detaches this text label from the player it was attached to and places it at <paramref name="position" />.
    /// </summary>
    /// <param name="position">The new world position.</param>
    public virtual void DetachFromPlayer(Vector3 position)
    {
        Resource.DetachFromPlayer(position);
    }

    /// <summary>
    /// Detaches this text label from the vehicle it was attached to and places it at <paramref name="position" />.
    /// </summary>
    /// <param name="position">The new world position.</param>
    public virtual void DetachFromVehicle(Vector3 position)
    {
        Resource.DetachFromVehicle(position);
    }

    /// <summary>
    /// Updates the color and text of this text label in a single operation.
    /// </summary>
    /// <param name="color">The new color.</param>
    /// <param name="text">The new text.</param>
    public virtual void SetColorAndText(Color color, string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        Resource.SetColourAndText(color, text);
    }

    /// <summary>
    /// Checks whether this text label is streamed in for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns><see langword="true" /> if streamed in; otherwise <see langword="false" />.</returns>
    public virtual bool IsStreamedInForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return Resource.IsStreamedInForPlayer(player);
    }

    /// <summary>
    /// Streams this text label in for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player.</param>
    public virtual void StreamInForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        Resource.StreamInForPlayer(player);
    }

    /// <summary>
    /// Streams this text label out for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The player.</param>
    public virtual void StreamOutForPlayer(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        Resource.StreamOutForPlayer(player);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!Resource.GetExtension<ComponentExtension>().IsOmpEntityDestroyed)
        {
            _textLabels.AsPool().Release(Id);
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
    /// Performs an implicit conversion from <see cref="TextLabel" /> to <see cref="ITextLabel" />.
    /// </summary>
    public static implicit operator ITextLabel(TextLabel? textLabel)
    {
        return textLabel?.Resource ?? default;
    }
}