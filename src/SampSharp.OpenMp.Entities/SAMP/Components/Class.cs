using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of a player spawn class.
/// </summary>
public class Class : IdProvider
{
    private readonly IClassesComponent _classes;

    /// <summary>
    /// Initializes a new instance of the <see cref="Class" /> class.
    /// </summary>
    protected Class(IClassesComponent classes, IClass playerClass) : base(playerClass.HasValue ? (IIDProvider)playerClass : default)
    {
        _classes = classes;
        Resource = playerClass;
    }

    private IClass Resource
    {
        get
        {
            ObjectDisposedException.ThrowIf(!IsComponentAlive, typeof(Class));
            return field;
        }
    }

    /// <summary>
    /// Gets or sets the team ID for this player class.
    /// </summary>
    public virtual int Team => GetSpawnData().Team;

    /// <summary>
    /// Gets or sets the skin model ID for this player class.
    /// </summary>
    public virtual int Skin => GetSpawnData().Skin;

    /// <summary>
    /// Gets or sets the spawn position for this player class.
    /// </summary>
    public virtual Vector3 Location => GetSpawnData().Location;

    /// <summary>
    /// Gets or sets the spawn angle (in degrees) for this player class.
    /// </summary>
    public virtual float Angle => GetSpawnData().Angle;

    /// <summary>
    /// Gets or sets the weapon slots assigned to this player class.
    /// </summary>
    public virtual PlayerWeaponSlots Weapons => GetSpawnData().Weapons;

    /// <summary>
    /// Sets the spawn data for the player using the specified spawn configuration.
    /// </summary>
    /// <param name="data">The spawn configuration data to apply.</param>
    public virtual void SetSpawnData(PlayerSpawnData data)
    {
        ArgumentNullException.ThrowIfNull(data);

        var omp = data.ToOmpData();
        Resource.SetClass(ref omp);
    }

    /// <summary>
    /// Retrieves the current spawn data for the player.
    /// </summary>
    /// <returns>A <see cref="PlayerSpawnData"/> instance containing the player's spawn information.</returns>
    public virtual PlayerSpawnData GetSpawnData()
    {
        ref var dat = ref Resource.GetClass();
        return PlayerSpawnData.FromOmpData(ref dat);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!Resource.GetExtension<ComponentExtension>().IsOmpEntityDestroyed)
        {
            _classes.AsPool().Release(Resource.GetID());
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (!IsComponentAlive)
        {
            return "(Destroyed)";
        }
        return $"(Id: {Id})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Class" /> to <see cref="IClass" />.
    /// </summary>
    public static implicit operator IClass(Class? playerClass)
    {
        return playerClass?.Resource ?? default;
    }
}