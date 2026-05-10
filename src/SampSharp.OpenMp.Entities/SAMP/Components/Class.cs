using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of a player spawn class.
/// </summary>
public class Class : IdProvider
{
    private readonly IClass _class;
    private readonly IClassesComponent _classes;

    /// <summary>
    /// Initializes a new instance of the <see cref="Class" /> class.
    /// </summary>
    protected Class(IClassesComponent classes, IClass playerClass) : base((IIDProvider)playerClass)
    {
        _classes = classes;
        _class = playerClass;
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _class.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

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
        _class.SetClass(ref omp);
    }

    /// <summary>
    /// Retrieves the current spawn data for the player.
    /// </summary>
    /// <returns>A <see cref="PlayerSpawnData"/> instance containing the player's spawn information.</returns>
    public virtual PlayerSpawnData GetSpawnData()
    {
        ref var dat = ref _class.GetClass();
        return PlayerSpawnData.FromOmpData(ref dat);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            _classes.AsPool().Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="Class" /> to <see cref="IClass" />.
    /// </summary>
    public static implicit operator IClass(Class playerClass)
    {
        return playerClass._class;
    }
}