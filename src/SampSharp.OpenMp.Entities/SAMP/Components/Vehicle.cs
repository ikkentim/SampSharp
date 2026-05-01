using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of a vehicle.
/// </summary>
public class Vehicle : WorldEntity
{
    private readonly IVehiclesComponent _vehicles;
    private readonly IVehicle _vehicle;

    /// <summary>
    /// Initializes a new instance of the <see cref="Vehicle" /> class.
    /// </summary>
    protected Vehicle(IVehiclesComponent vehicles, IVehicle vehicle) : base((IEntity)vehicle)
    {
        _vehicles = vehicles;
        _vehicle = vehicle;
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _vehicle.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>
    /// Gets or sets the Z angle of this vehicle.
    /// </summary>
    public virtual float Angle
    {
        get => _vehicle.GetZAngle();
        set => _vehicle.SetZAngle(value);
    }

    /// <summary>
    /// Gets the model ID of this vehicle.
    /// </summary>
    public virtual VehicleModelType Model => (VehicleModelType)_vehicle
        .GetModel();

    /// <summary>
    /// Gets a value indicating whether this vehicle has a trailer attached.
    /// </summary>
    public virtual bool HasTrailer => _vehicle.GetTrailer() != null;

    /// <summary>
    /// Gets or sets the <see cref="Vehicle" /> trailer attached to this vehicle.
    /// </summary>
    public virtual Vehicle? Trailer
    {
        get => _vehicle.GetTrailer().TryGetExtension<ComponentExtension>()?.Component as Vehicle;
        set
        {
            if (value)
            {
                _vehicle.AttachTrailer(value!);
            }
            else
            {
                _vehicle.DetachTrailer();
            }
        }
    }

    /// <summary>
    /// Gets or sets the velocity at which this vehicle is moving.
    /// </summary>
    public virtual Vector3 Velocity
    {
        get => _vehicle.GetVelocity();
        set => _vehicle.SetVelocity(value);
    }

    /// <summary>
    /// Gets or sets the parameters of this vehicle, including the engine, lights, alarm, doors, bonnet, boot, and objective status.
    /// </summary>
    public virtual VehicleParameters Parameters
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return VehicleParameters.FromParams(ref parameters);
        }
        set
        {
            var p = value.ToParams();
            _vehicle.SetParams(ref p);
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the engine of this vehicle is running.
    /// </summary>
    public virtual bool Engine
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.engine == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                Engine = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the lights of this vehicle are on.
    /// </summary>
    public virtual bool Lights
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.lights == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                Lights = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the alarm of this vehicle is on.
    /// </summary>
    public virtual bool Alarm
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.alarm == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                Alarm = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the doors of this vehicle are locked.
    /// </summary>
    public virtual bool Doors
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.doors == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                Doors = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the bonnet (hood) of this vehicle is open.
    /// </summary>
    public virtual bool Bonnet
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.bonnet == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                Bonnet = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the boot (trunk) of this vehicle is open.
    /// </summary>
    public virtual bool Boot
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.boot == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                Boot = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the objective marker of this vehicle is on.
    /// </summary>
    public virtual bool Objective
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.objective == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                Objective = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the driver door of this vehicle is open.
    /// </summary>
    public virtual bool IsDriverDoorOpen
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.doorDriver == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                DoorDriver = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the passenger door of this vehicle is open.
    /// </summary>
    public virtual bool IsPassengerDoorOpen
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.doorPassenger == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                DoorPassenger = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the back left door of this vehicle is open.
    /// </summary>
    public virtual bool IsBackLeftDoorOpen
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.doorBackLeft == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                DoorBackLeft = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the back right door of this vehicle is open.
    /// </summary>
    public virtual bool IsBackRightDoorOpen
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.doorBackRight == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                DoorBackRight = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the driver window of this vehicle is closed.
    /// </summary>
    public virtual bool IsDriverWindowClosed
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.windowDriver == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                WindowDriver = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the passenger window of this vehicle is closed.
    /// </summary>
    public virtual bool IsPassengerWindowClosed
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.windowPassenger == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                WindowPassenger = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the back left window of this vehicle is closed.
    /// </summary>
    public virtual bool IsBackLeftWindowClosed
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.windowBackLeft == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                WindowBackLeft = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets or sets a value indicating whether the back right window of this vehicle is closed.
    /// </summary>
    public virtual bool IsBackRightWindowClosed
    {
        get
        {
            var parameters = _vehicle.GetParams();
            return (VehicleParameterValue)parameters.windowBackRight == VehicleParameterValue.On;
        }
        set =>
            Parameters = Parameters with
            {
                WindowBackRight = value ? VehicleParameterValue.On : VehicleParameterValue.Off
            };
    }

    /// <summary>
    /// Gets a value indicating whether the siren of this vehicle is on.
    /// </summary>
    public virtual bool IsSirenOn => _vehicle.GetSirenState() == 1;

    /// <summary>
    /// Gets or sets the health of this vehicle.
    /// </summary>
    public virtual float Health
    {
        get => _vehicle.GetHealth();
        set => _vehicle.SetHealth(value);
    }

    /// <summary>
    /// Gets the rotation of this vehicle on all axes as a quaternion.
    /// </summary>
    [Obsolete("Deprecated. Use Rotation instead.")]
    public virtual Quaternion RotationQuaternion => Rotation;

    /// <summary>
    /// Gets the primary color of this vehicle.
    /// </summary>
    public virtual int Color1
    {
        get
        {
            _vehicle.GetColour(out var pair);
            return pair.First;
        }
    }

    /// <summary>
    /// Gets the secondary color of this vehicle.
    /// </summary>
    public virtual int Color2
    {
        get
        {
            _vehicle.GetColour(out var pair);
            return pair.Second;
        }
    }

    /// <summary>
    /// Calculates the distance between this vehicle and the specified <paramref name="point" />.
    /// </summary>
    /// <param name="point">The point to calculate the distance to as a <see cref="Vector3" />.</param>
    /// <returns>The distance from this vehicle to the specified <paramref name="point" />.</returns>
    public virtual float GetDistanceFromPoint(Vector3 point)
    {
        var offset = point - Position;
        return offset.Length();
    }

    /// <summary>
    /// Determines whether this vehicle is streamed in for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The <see cref="Player" /> to check.</param>
    /// <returns><see langword="true" /> if this vehicle is streamed in for the specified <paramref name="player" />; <see langword="false" /> otherwise.</returns>
    public virtual bool IsStreamedIn(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return _vehicle.IsStreamedInForPlayer(player);
    }

    /// <summary>
    /// Sets the parameters of this vehicle for the specified <paramref name="player" />.
    /// </summary>
    /// <param name="player">The <see cref="Player" /> to set this vehicle's parameters for.</param>
    /// <param name="parameters">The <see cref="VehicleParameters" /> to set.</param>
    public virtual void SetParametersForPlayer(Player player, in VehicleParameters parameters)
    {
        ArgumentNullException.ThrowIfNull(player);
        var p = parameters.ToParams();
        _vehicle.SetParamsForPlayer(player, ref p);
    }

    /// <summary>
    /// Respawns this vehicle to its original spawn location.
    /// </summary>
    public virtual void Respawn()
    {
        _vehicle.Respawn();
    }

    /// <summary>
    /// Links this vehicle to an interior, allowing it to be used in interior areas such as arenas or stadiums.
    /// </summary>
    /// <param name="interiorId">The ID of the interior to link this vehicle to.</param>
    public virtual void LinkToInterior(int interiorId)
    {
        _vehicle.SetInterior(interiorId);
    }

    /// <summary>
    /// Adds a component (modification) to this vehicle.
    /// </summary>
    /// <param name="componentId">The ID of the component to add.</param>
    public virtual void AddComponent(int componentId)
    {
        _vehicle.AddComponent(componentId);
    }

    /// <summary>
    /// Removes a component from this vehicle.
    /// </summary>
    /// <param name="componentId">The ID of the component to remove.</param>
    public virtual void RemoveComponent(int componentId)
    {
        _vehicle.RemoveComponent(componentId);
    }

    /// <summary>
    /// Changes the primary and secondary colors of this vehicle.
    /// </summary>
    /// <param name="color1">The primary color ID to set.</param>
    /// <param name="color2">The secondary color ID to set.</param>
    public virtual void ChangeColor(int color1, int color2)
    {
        _vehicle.SetColour(color1, color2);
    }

    /// <summary>
    /// Changes the paintjob of this vehicle. For solid colors, use <see cref="ChangeColor" /> instead.
    /// </summary>
    /// <param name="paintjobId">The paintjob ID to apply. Use 3 to remove the paintjob.</param>
    public virtual void ChangePaintjob(int paintjobId)
    {
        _vehicle.SetPaintJob(paintjobId);
    }

    /// <summary>
    /// Sets the license plate text of this vehicle, which supports color embedding.
    /// </summary>
    /// <param name="numberplate">The license plate text to display. Color embedding is supported.</param>
    public virtual void SetNumberPlate(string numberplate)
    {
        _vehicle.SetPlate(numberplate);
    }

    /// <summary>
    /// Gets the component ID installed in the specified <paramref name="slot" /> of this vehicle.
    /// </summary>
    /// <param name="slot">The component slot to check.</param>
    /// <returns>The ID of the component installed in the specified <paramref name="slot" />.</returns>
    public virtual int GetComponentInSlot(CarModType slot)
    {
        return _vehicle.GetComponentInSlot((int)slot);
    }

    /// <summary>
    /// Fully repairs this vehicle, including all visual damage such as bumps, dents, scratches, and popped tires.
    /// </summary>
    public virtual void Repair()
    {
        _vehicle.Repair();
    }

    /// <summary>
    /// Sets the angular velocity of this vehicle.
    /// </summary>
    /// <param name="velocity">The angular velocity to set.</param>
    public virtual void SetAngularVelocity(Vector3 velocity)
    {
        _vehicle.SetAngularVelocity(velocity);
    }

    /// <summary>
    /// Retrieves the damage status of this vehicle's panels, doors, lights, and tires.
    /// </summary>
    /// <param name="panels">The panel damage status.</param>
    /// <param name="doors">The door damage status.</param>
    /// <param name="lights">The light damage status.</param>
    /// <param name="tires">The tire damage status.</param>
    public virtual void GetDamageStatus(out int panels, out int doors, out int lights, out int tires)
    {
        _vehicle.GetDamageStatus(out panels, out doors, out lights, out tires);
    }

    /// <summary>
    /// Sets the damage status of this vehicle's panels, doors, lights, and tires.
    /// </summary>
    /// <param name="panels">The panel damage status to set.</param>
    /// <param name="doors">The door damage status to set.</param>
    /// <param name="lights">The light damage status to set.</param>
    /// <param name="tires">The tire damage status to set.</param>
    /// <param name="updater">The player updating the vehicle damage, or <see langword="null" /> to update for all players.</param>
    public virtual void UpdateDamageStatus(int panels, int doors, int lights, int tires, Player? updater = null)
    {
        _vehicle.SetDamageStatus(panels, doors, (byte)lights, (byte)tires, updater ?? default(IPlayer));
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            _vehicles.AsPool().Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id}, Model: {Model})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Vehicle" /> to <see cref="IVehicle" />.
    /// </summary>
    public static implicit operator IVehicle(Vehicle vehicle)
    {
        return vehicle._vehicle;
    }
}