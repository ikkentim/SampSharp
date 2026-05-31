using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of a vehicle.
/// </summary>
public class Vehicle : WorldEntity
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IVehiclesComponent _vehicles;

    /// <summary>
    /// Initializes a new instance of the <see cref="Vehicle" /> class.
    /// </summary>
    protected Vehicle(IOmpEntityProvider entityProvider, IVehiclesComponent vehicles, IVehicle vehicle) : base(vehicle.HasValue ? (IEntity)vehicle : default)
    {
        _entityProvider = entityProvider;
        _vehicles = vehicles;
        Resource = vehicle;
    }

    private IVehicle Resource
    {
        get
        {
            ObjectDisposedException.ThrowIf(!IsComponentAlive, typeof(Vehicle));
            return field;
        }
    }

    /// <summary>
    /// Gets or sets the Z angle of this vehicle.
    /// </summary>
    public virtual float Angle
    {
        get => Resource.GetZAngle();
        set => Resource.SetZAngle(value);
    }

    /// <summary>
    /// Gets the model ID of this vehicle.
    /// </summary>
    public virtual VehicleModelType Model => (VehicleModelType)Resource
        .GetModel();

    /// <summary>
    /// Gets a value indicating whether this vehicle has a trailer attached.
    /// </summary>
    public virtual bool HasTrailer => Resource.GetTrailer() != null;

    /// <summary>
    /// Gets or sets the <see cref="Vehicle" /> trailer attached to this vehicle.
    /// </summary>
    public virtual Vehicle? Trailer
    {
        get => Resource.GetTrailer().TryGetExtension<ComponentExtension>()?.Component as Vehicle;
        set
        {
            if (value)
            {
                Resource.AttachTrailer(value!);
            }
            else
            {
                Resource.DetachTrailer();
            }
        }
    }

    /// <summary>
    /// Gets or sets the velocity at which this vehicle is moving.
    /// </summary>
    public virtual Vector3 Velocity
    {
        get => Resource.GetVelocity();
        set => Resource.SetVelocity(value);
    }

    /// <summary>
    /// Gets or sets the parameters of this vehicle, including the engine, lights, alarm, doors, bonnet, boot, and objective status.
    /// </summary>
    public virtual VehicleParameters Parameters
    {
        get
        {
            var parameters = Resource.GetParams();
            return VehicleParameters.FromParams(ref parameters);
        }
        set
        {
            var p = value.ToParams();
            Resource.SetParams(ref p);
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the engine of this vehicle is running.
    /// </summary>
    public virtual bool Engine
    {
        get
        {
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
            var parameters = Resource.GetParams();
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
    public virtual bool IsSirenOn => Resource.GetSirenState() == 1;

    /// <summary>
    /// Gets or sets the health of this vehicle.
    /// </summary>
    public virtual float Health
    {
        get => Resource.GetHealth();
        set => Resource.SetHealth(value);
    }

    /// <summary>
    /// Gets the rotation of this vehicle on all axes as a quaternion.
    /// </summary>
    [Obsolete("Deprecated. Use Rotation instead.")]
    public virtual Quaternion RotationQuaternion => Rotation;

    /// <summary>
    /// Gets the primary color of this vehicle.
    /// </summary>
    public virtual int Color1 => Colors.Primary;

    /// <summary>
    /// Gets the secondary color of this vehicle.
    /// </summary>
    public virtual int Color2 => Colors.Secondary;

    /// <summary>
    /// Gets or sets the paintjob of this vehicle. For solid colors, use <see cref="ChangeColor" /> instead.
    /// </summary>
    /// <remarks>Use 3 to remove the paintjob.</remarks>
    public virtual int Paintjob
    {
        get => Resource.GetPaintJob();
        set => Resource.SetPaintJob(value);
    }

    /// <summary>
    /// Gets or sets the interior ID of this vehicle.
    /// </summary>
    public virtual int Interior
    {
        get => Resource.GetInterior();
        set => Resource.SetInterior(value);
    }

    /// <summary>
    /// Gets or sets the spawn data of this vehicle (model, position, rotation, colors, siren, interior, respawn delay).
    /// </summary>
    public virtual VehicleSpawnInfo SpawnData
    {
        get
        {
            var raw = Resource.GetSpawnData();
            return new VehicleSpawnInfo(
                ModelId: raw.modelID,
                Position: raw.position,
                ZRotation: raw.zRotation,
                PrimaryColor: raw.colour1,
                SecondaryColor: raw.colour2,
                HasSiren: raw.siren,
                Interior: raw.interior,
                RespawnDelay: raw.respawnDelay);
        }
        set
        {
            var raw = new VehicleSpawnData(
                respawnDelay: value.RespawnDelay,
                modelID: value.ModelId,
                position: value.Position,
                zRotation: value.ZRotation,
                colour1: value.PrimaryColor,
                colour2: value.SecondaryColor,
                siren: value.HasSiren,
                interior: value.Interior);
            Resource.SetSpawnData(ref raw);
        }
    }


    /// <summary>
    /// Gets or sets the colors of this vehicle as a tuple of <c>(primary, secondary)</c> colors IDs.
    /// </summary>
    public virtual (int Primary, int Secondary) Colors
    {
        get => Resource.GetColour();
        set => Resource.SetColour(value.Primary, value.Secondary);
    }

    /// <summary>
    /// Gets the current numberplate text of this vehicle.
    /// </summary>
    public virtual string NumberPlate => Resource.GetPlate();

    /// <summary>
    /// Gets a value indicating whether this vehicle is dead (destroyed).
    /// </summary>
    public virtual bool IsDead => Resource.IsDead();

    /// <summary>
    /// Gets a value indicating whether this vehicle is currently in the process of respawning.
    /// </summary>
    public virtual bool IsRespawning => Resource.IsRespawning();

    /// <summary>
    /// Gets or sets the respawn delay for this vehicle.
    /// </summary>
    public virtual TimeSpan RespawnDelay
    {
        get => Resource.GetRespawnDelay();
        set => Resource.SetRespawnDelay(value);
    }

    /// <summary>
    /// Gets a value indicating whether this vehicle has ever been occupied.
    /// </summary>
    public virtual bool HasBeenOccupied => Resource.HasBeenOccupied();

    /// <summary>
    /// Gets a value indicating whether this vehicle is currently occupied.
    /// </summary>
    public virtual bool IsOccupied => Resource.IsOccupied();

    /// <summary>
    /// Gets the timestamp at which this vehicle was last occupied.
    /// </summary>
    public virtual DateTimeOffset LastOccupiedTime => Resource.GetLastOccupiedTime();

    /// <summary>
    /// Gets the timestamp at which this vehicle was last spawned.
    /// </summary>
    public virtual DateTimeOffset LastSpawnTime => Resource.GetLastSpawnTime();

    /// <summary>
    /// Gets the player pool ID of the last driver of this vehicle.
    /// </summary>
    public virtual int LastDriverPoolID => Resource.GetLastDriverPoolID();

    /// <summary>
    /// Gets a value indicating whether this vehicle is a trailer (i.e. is being towed by another vehicle).
    /// </summary>
    public virtual bool IsTrailer => Resource.IsTrailer();

    /// <summary>
    /// Gets the cab (towing vehicle) currently towing this vehicle, or <see langword="null" /> if there is none.
    /// </summary>
    public virtual Vehicle? Cab => _entityProvider.GetComponent(Resource.GetCab());

    /// <summary>
    /// Gets the current driver of this vehicle, or <see langword="null" /> if there is none.
    /// </summary>
    public virtual Player? Driver => _entityProvider.GetComponent(Resource.GetDriver());

    /// <summary>
    /// Gets the current Hydra (jet) thrust angle of this vehicle.
    /// </summary>
    public virtual uint HydraThrustAngle => Resource.GetHydraThrustAngle();

    /// <summary>
    /// Gets the current train speed of this vehicle.
    /// </summary>
    public virtual float TrainSpeed => Resource.GetTrainSpeed();

    /// <summary>
    /// Gets the current state of this vehicle's landing gear.
    /// </summary>
    public virtual byte LandingGearState => Resource.GetLandingGearState();

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
        return Resource.IsStreamedInForPlayer(player);
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
        Resource.SetParamsForPlayer(player, ref p);
    }

    /// <summary>
    /// Respawns this vehicle to its original spawn location.
    /// </summary>
    public virtual void Respawn()
    {
        Resource.Respawn();
    }

    /// <summary>
    /// Links this vehicle to an interior, allowing it to be used in interior areas such as arenas or stadiums.
    /// </summary>
    /// <param name="interiorId">The ID of the interior to link this vehicle to.</param>
    [Obsolete("Use the Interior property instead.")]
    public virtual void LinkToInterior(int interiorId)
    {
        Resource.SetInterior(interiorId);
    }

    /// <summary>
    /// Adds a component (modification) to this vehicle.
    /// </summary>
    /// <param name="componentId">The ID of the component to add.</param>
    public virtual void AddComponent(int componentId)
    {
        Resource.AddComponent(componentId);
    }

    /// <summary>
    /// Removes a component from this vehicle.
    /// </summary>
    /// <param name="componentId">The ID of the component to remove.</param>
    public virtual void RemoveComponent(int componentId)
    {
        Resource.RemoveComponent(componentId);
    }

    /// <summary>
    /// Changes the primary and secondary colors of this vehicle.
    /// </summary>
    /// <param name="color1">The primary color ID to set.</param>
    /// <param name="color2">The secondary color ID to set.</param>
    [Obsolete("Use Colors property instead.")]
    public virtual void ChangeColor(int color1, int color2)
    {
        Colors = (color1, color2);
    }

    /// <summary>
    /// Changes the paintjob of this vehicle. For solid colors, use <see cref="ChangeColor" /> instead.
    /// </summary>
    /// <param name="paintjobId">The paintjob ID to apply. Use 3 to remove the paintjob.</param>
    [Obsolete("Use Paintjob property instead.")]
    public virtual void ChangePaintjob(int paintjobId)
    {
        Paintjob = paintjobId;
    }

    /// <summary>
    /// Sets the license plate text of this vehicle, which supports color embedding.
    /// </summary>
    /// <param name="numberplate">The license plate text to display. Color embedding is supported.</param>
    public virtual void SetNumberPlate(string numberplate)
    {
        Resource.SetPlate(numberplate);
    }

    /// <summary>
    /// Gets the component ID installed in the specified <paramref name="slot" /> of this vehicle.
    /// </summary>
    /// <param name="slot">The component slot to check.</param>
    /// <returns>The ID of the component installed in the specified <paramref name="slot" />.</returns>
    public virtual int GetComponentInSlot(CarModType slot)
    {
        return Resource.GetComponentInSlot((int)slot);
    }

    /// <summary>
    /// Fully repairs this vehicle, including all visual damage such as bumps, dents, scratches, and popped tires.
    /// </summary>
    public virtual void Repair()
    {
        Resource.Repair();
    }

    /// <summary>
    /// Sets the angular velocity of this vehicle.
    /// </summary>
    /// <param name="velocity">The angular velocity to set.</param>
    public virtual void SetAngularVelocity(Vector3 velocity)
    {
        Resource.SetAngularVelocity(velocity);
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
        Resource.GetDamageStatus(out panels, out doors, out lights, out tires);
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
        Resource.SetDamageStatus(panels, doors, (byte)lights, (byte)tires, updater ?? default(IPlayer));
    }

    /// <summary>
    /// Enumerates the current passengers of this vehicle.
    /// </summary>
    /// <returns>A lazy sequence of <see cref="Player" /> components.</returns>
    public virtual IEnumerable<Player> GetPassengers()
    {
        foreach (var raw in Resource.GetPassengers())
        {
            var component = _entityProvider.GetComponent(raw);
            if (component != null)
            {
                yield return component;
            }
        }
    }

    /// <summary>
    /// Enumerates the players for whom this vehicle is currently streamed in.
    /// </summary>
    /// <returns>A lazy sequence of <see cref="Player" /> components.</returns>
    public virtual IEnumerable<Player> StreamedForPlayers()
    {
        foreach (var raw in Resource.StreamedForPlayers())
        {
            var component = _entityProvider.GetComponent(raw);
            if (component != null)
            {
                yield return component;
            }
        }
    }

    /// <summary>
    /// Sets the siren state of this vehicle.
    /// </summary>
    /// <param name="enable"><see langword="true" /> to turn the siren on; <see langword="false" /> to turn it off.</param>
    public virtual void SetSiren(bool enable)
    {
        Resource.SetSiren(enable);
    }

    /// <summary>
    /// Adds a carriage to this train at the specified position in the carriage chain.
    /// </summary>
    /// <param name="carriage">The vehicle to add as a carriage.</param>
    /// <param name="pos">The position of the carriage in the chain.</param>
    public virtual void AddCarriage(Vehicle carriage, int pos)
    {
        ArgumentNullException.ThrowIfNull(carriage);
        Resource.AddCarriage(carriage, pos);
    }

    /// <summary>
    /// Updates the position and velocity of a carriage attached to this train.
    /// </summary>
    /// <param name="position">The new carriage position.</param>
    /// <param name="velocity">The new carriage velocity.</param>
    public virtual void UpdateCarriage(Vector3 position, Vector3 velocity)
    {
        Resource.UpdateCarriage(position, velocity);
    }

    /// <summary>
    /// Enumerates the carriages currently linked to this train, in order.
    /// </summary>
    /// <returns>A sequence of <see cref="Vehicle" /> components representing the carriages.</returns>
    public virtual IEnumerable<Vehicle> GetCarriages()
    {
        var array = Resource.GetCarriages();
        foreach (var raw in array.Values)
        {
            var component = _entityProvider.GetComponent(raw);
            if (component != null)
            {
                yield return component;
            }
        }
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!Resource.GetExtension<ComponentExtension>().IsOmpEntityDestroyed)
        {
            _vehicles.AsPool().Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (IsDestroying)
        {
            return "(Destroyed)";
        }

        return $"(Id: {Id}, Model: {Model})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Vehicle" /> to <see cref="IVehicle" />.
    /// </summary>
    public static implicit operator IVehicle(Vehicle? vehicle)
    {
        return vehicle?.Resource ?? default;
    }
}