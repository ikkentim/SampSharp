using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a component which provides the data and functionality of a player 3D text label.</summary>
public class PlayerTextLabel : WorldEntity
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IPlayerTextLabelData _playerTextLabels;
    private readonly IPlayerTextLabel _playerTextLabel;

    /// <summary>Constructs an instance of PlayerTextLabel, should be used internally.</summary>
    protected PlayerTextLabel(IOmpEntityProvider entityProvider, IPlayerTextLabelData playerTextLabels, IPlayerTextLabel playerTextLabel) : base((IEntity)playerTextLabel)
    {
        _entityProvider = entityProvider;
        _playerTextLabels = playerTextLabels;
        _playerTextLabel = playerTextLabel;
    }
    
    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _playerTextLabel.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>Gets the color of this player text label.</summary>
    public virtual Color Color
    {
        get
        {
            _playerTextLabel.GetColour(out var colour);
            return colour;
        }
    }

    /// <summary>Gets the text of this player text label.</summary>
    public virtual string Text => _playerTextLabel.GetText();

    /// <summary>Gets the draw distance.</summary>
    public virtual float DrawDistance => _playerTextLabel.GetDrawDistance();

    /// <summary>Gets a value indicating whether to test the line of sight.</summary>
    public virtual bool TestLos => _playerTextLabel.GetTestLOS();

    /// <summary>Gets the attached entity.</summary>
    public virtual Component? AttachedEntity
    {
        get
        {
            var attachmentData = _playerTextLabel.GetAttachmentData();

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
    /// Attaches this player text label to the specified player.
    /// </summary>
    /// <param name="player">The player to attach this player text label to.</param>
    /// <param name="offset">The offset from the player's position to attach this player text label to.</param>
    public virtual void Attach(Player player, Vector3 offset = default)
    {
        _playerTextLabel.AttachToPlayer(player, offset);
    }
    
    /// <summary>
    /// Attaches this player text label to the specified vehicle.
    /// </summary>
    /// <param name="vehicle">The vehicle to attach this text label to.</param>
    /// <param name="offset">The offset from the vehicle's position to attach this text label to.</param>
    public virtual void Attach(Vehicle vehicle, Vector3 offset = default)
    {
        _playerTextLabel.AttachToVehicle(vehicle, offset);
    }
    
    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            _playerTextLabels.Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id}, Text: {Text})";
    }
    
    /// <summary>Performs an implicit conversion from <see cref="PlayerTextLabel" /> to <see cref="IPlayerTextLabel" />.</summary>
    public static implicit operator IPlayerTextLabel(PlayerTextLabel playerTextLabel)
    {
        return playerTextLabel._playerTextLabel;
    }
}