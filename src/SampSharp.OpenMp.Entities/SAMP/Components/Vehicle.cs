using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a component which provides the data and functionality of a vehicle.</summary>
public class Vehicle : WorldEntity
{
    private readonly IVehiclesComponent _vehicles;
    private readonly IVehicle _vehicle;

    /// <summary>Constructs an instance of Vehicle, should be used internally.</summary>
    protected Vehicle(IVehiclesComponent vehicles, IVehicle vehicle) : base((IEntity)vehicle)
    {
        _vehicles = vehicles;
        _vehicle = vehicle;
    }
    
    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _vehicle.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>Gets or sets the Z angle of this vehicle.</summary>
    public virtual float Angle
    {
        get => _vehicle.GetZAngle();
        set => _vehicle.SetZAngle(value);
    }

    /// <summary>Gets the model ID of this vehicle.</summary>
    public virtual VehicleModelType Model => (VehicleModelType)_vehicle
        .GetModel();

    /// <summary>Gets whether this vehicle has a trailer attached to it.</summary>
    public virtual bool HasTrailer => _vehicle.GetTrailer() != null;

    /// <summary>Gets or sets the the trailer attached to this vehicle.</summary>
    /// <returns>The trailer attached.</returns>
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

    /// <summary>Gets or sets the velocity at which this vehicle is moving.</summary>
    public virtual Vector3 Velocity
    {
        get => _vehicle.GetVelocity();
        set => _vehicle.SetVelocity(value);
    }

    /// <summary>
    /// Gets or sets the parameters of this vehicle. This includes the engine, lights, alarm, doors, bonnet, boot and
    /// objective status.
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

    /// <summary>Gets or sets this vehicle's engine status. If True, the engine is running.</summary>
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

    /// <summary>Gets or sets this vehicle's lights' state. If True the lights are on.</summary>
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

    /// <summary>Gets or sets this vehicle's alarm state. If True the alarm is (or was) sounding.</summary>
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

    /// <summary>Gets or sets the lock status of the doors of this vehicle. If True the doors are locked.</summary>
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

    /// <summary>Gets or sets the bonnet/hood status of this vehicle. If True, it's open.</summary>
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

    /// <summary>Gets or sets the boot/trunk status of this vehicle. True means it is open.</summary>
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

    /// <summary>Gets or sets the objective status of this vehicle. True means the objective is on.</summary>
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

    /// <summary>Gets or sets a value indicating whether the driver door is open.</summary>
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

    /// <summary>Gets or sets a value indicating whether the passenger door is open.</summary>
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

    /// <summary>Gets or sets a value indicating whether the driver door is open.</summary>
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

    /// <summary>Gets or sets a value indicating whether the driver door is open.</summary>
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

    /// <summary>Gets or sets a value indicating whether the driver window is closed.</summary>
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

    /// <summary>Gets or sets a value indicating whether the passenger window is closed.</summary>
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

    /// <summary>Gets or sets a value indicating whether the driver window is closed.</summary>
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

    /// <summary>Gets or sets a value indicating whether the driver window is closed.</summary>
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

    /// <summary>Gets a value indicating whether this Vehicle's siren is on.</summary>
    public virtual bool IsSirenOn => _vehicle.GetSirenState() == 1;

    /// <summary>Gets or sets the health of this vehicle.</summary>
    public virtual float Health
    {
        get => _vehicle.GetHealth();
        set => _vehicle.SetHealth(value);
    }

    /// <summary>Gets this vehicle's rotation on all axis as a quaternion.</summary>
    [Obsolete("Deprecated. Use Rotation instead.")]
    public virtual Quaternion RotationQuaternion => Rotation;

    /// <summary>Gets the first color of this vehicle.</summary>
    public virtual int Color1
    {
        get
        {
            _vehicle.GetColour(out var pair);
            return pair.First;
        }
    }

    /// <summary>Gets the second color of this vehicle.</summary>
    public virtual int Color2
    {
        get
        {
            _vehicle.GetColour(out var pair);
            return pair.Second;
        }
    }

    /// <summary>
    /// This function can be used to calculate the distance (as a float) between this vehicle and another map
    /// coordinate. This can be useful to detect how far a vehicle away is from a location.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <returns>A float containing the distance from the point specified in the coordinates.</returns>
    public virtual float GetDistanceFromPoint(Vector3 point)
    {
        var offset = point - Position;
        return offset.Length();
    }

    /// <summary>Checks if this vehicle is streamed in for the specified <paramref name="player" />.</summary>
    /// <param name="player">The player to check.</param>
    /// <returns><see langword="true" /> if this vehicle is streamed in for the specified vehicle; <see langword="false" /> otherwise.</returns>
    public virtual bool IsStreamedIn(Player player)
    {
        return _vehicle.IsStreamedInForPlayer(player);
    }

    /// <summary>Set the parameters of this vehicle for a player.</summary>
    /// <param name="player">The player to set this vehicle's parameters for.</param>
    /// <param name="parameters">The vehicle parameters</param>
    public virtual void SetParametersForPlayer(Player player, in VehicleParameters parameters)
    {
        var p = parameters.ToParams();
        _vehicle.SetParamsForPlayer(player, ref p);
    }

    /// <summary>Sets this vehicle back to the position at where it was created.</summary>
    public virtual void Respawn()
    {
        _vehicle.Respawn();
    }

    /// <summary>Links this vehicle to the interior. This can be used for example for an arena/stadium.</summary>
    /// <param name="interiorId">Interior ID.</param>
    public virtual void LinkToInterior(int interiorId)
    {
        _vehicle.SetInterior(interiorId);
    }

    /// <summary>Adds a 'component' (often referred to as a 'mod' (modification)) to this Vehicle.</summary>
    /// <param name="componentId">The ID of the component to add to the vehicle.</param>
    public virtual void AddComponent(int componentId)
    {
        _vehicle.AddComponent(componentId);
    }

    /// <summary>Remove a component from the vehicle.</summary>
    /// <param name="componentId">ID of the component to remove.</param>
    public virtual void RemoveComponent(int componentId)
    {
        _vehicle.RemoveComponent(componentId);
    }

    /// <summary>Change this vehicle's primary and secondary colors.</summary>
    /// <param name="color1">The new vehicle's primary Color ID.</param>
    /// <param name="color2">The new vehicle's secondary Color ID.</param>
    public virtual void ChangeColor(int color1, int color2)
    {
        _vehicle.SetColour(color1, color2);
    }

    /// <summary>Change this vehicle's paintjob (for plain colors see <see cref="ChangeColor" />).</summary>
    /// <param name="paintjobId">The ID of the paintjob to apply. Use 3 to remove a paintjob.</param>
    public virtual void ChangePaintjob(int paintjobId)
    {
        _vehicle.SetPaintJob(paintjobId);
    }

    /// <summary>Set this vehicle's numberplate, which supports color embedding.</summary>
    /// <param name="numberplate">The text that should be displayed on the numberplate. Color Embedding> is
    /// supported.</param>
    public virtual void SetNumberPlate(string numberplate)
    {
        _vehicle.SetPlate(numberplate);
    }

    /// <summary>Retrieves the installed component ID from this vehicle in a specific slot.</summary>
    /// <param name="slot">The component slot to check for components.</param>
    /// <returns>The ID of the component installed in the specified slot.</returns>
    public virtual int GetComponentInSlot(CarModType slot)
    {
        return _vehicle.GetComponentInSlot((int)slot);
    }

    /// <summary>Fully repairs this vehicle, including visual damage (bumps, dents, scratches, popped tires
    /// etc.).</summary>
    public virtual void Repair()
    {
        _vehicle.Repair();
    }

    /// <summary>Sets the angular velocity of this vehicle.</summary>
    /// <param name="velocity">The amount of velocity in the angular directions.</param>
    public virtual void SetAngularVelocity(Vector3 velocity)
    {
        _vehicle.SetAngularVelocity(velocity);
    }

    /// <summary>Retrieve the damage statuses of this vehicle.</summary>
    /// <param name="panels">A variable to store the panel damage data in, passed by reference.</param>
    /// <param name="doors">A variable to store the door damage data in, passed by reference.</param>
    /// <param name="lights">A variable to store the light damage data in, passed by reference.</param>
    /// <param name="tires">A variable to store the tire damage data in, passed by reference.</param>
    public virtual void GetDamageStatus(out int panels, out int doors, out int lights, out int tires)
    {
        _vehicle.GetDamageStatus(out panels, out doors, out lights, out tires);
    }

    /// <summary>Sets the various visual damage statuses of this vehicle, such as popped tires, broken lights and
    /// damaged panels.</summary>
    /// <param name="panels">A set of bits containing the panel damage status.</param>
    /// <param name="doors">A set of bits containing the door damage status.</param>
    /// <param name="lights">A set of bits containing the light damage status.</param>
    /// <param name="tires">A set of bits containing the tire damage status.</param>
    /// <param name="updater">TODO: document parameter</param>
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
    
    /// <summary>Performs an implicit conversion from <see cref="Vehicle" /> to <see cref="IVehicle" />.</summary>
    public static implicit operator IVehicle(Vehicle vehicle)
    {
        return vehicle._vehicle;
    }
}