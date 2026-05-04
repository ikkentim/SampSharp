using System.Numerics;
using SampSharp.OpenMp.Core.Api;
using OmpPlayerClass = SampSharp.OpenMp.Core.Api.PlayerClass;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of a player spawn class.
/// </summary>
public class PlayerClass : IdProvider
{
    private readonly IClass _class;
    private readonly IClassesComponent _classes;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerClass" /> class.
    /// </summary>
    protected PlayerClass(IClassesComponent classes, IClass playerClass) : base((IIDProvider)playerClass)
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
    public virtual int Team
    {
        get => _class.GetClass().Team;
        set
        {
            var data = _class.GetClass();
            var newData = new OmpPlayerClass(value, data.Skin, data.Spawn, data.Angle, data.Weapons);
            _class.SetClass(ref newData);
        }
    }

    /// <summary>
    /// Gets or sets the skin model ID for this player class.
    /// </summary>
    public virtual int Skin
    {
        get => _class.GetClass().Skin;
        set
        {
            var data = _class.GetClass();
            var newData = new OmpPlayerClass(data.Team, value, data.Spawn, data.Angle, data.Weapons);
            _class.SetClass(ref newData);
        }
    }

    /// <summary>
    /// Gets or sets the spawn position for this player class.
    /// </summary>
    public virtual Vector3 SpawnPosition
    {
        get => _class.GetClass().Spawn;
        set
        {
            var data = _class.GetClass();
            var newData = new OmpPlayerClass(data.Team, data.Skin, value, data.Angle, data.Weapons);
            _class.SetClass(ref newData);
        }
    }

    /// <summary>
    /// Gets or sets the spawn angle (in degrees) for this player class.
    /// </summary>
    public virtual float Angle
    {
        get => _class.GetClass().Angle;
        set
        {
            var data = _class.GetClass();
            var newData = new OmpPlayerClass(data.Team, data.Skin, data.Spawn, value, data.Weapons);
            _class.SetClass(ref newData);
        }
    }

    /// <summary>
    /// Gets or sets the weapon slots assigned to this player class.
    /// </summary>
    public virtual WeaponSlots Weapons
    {
        get => _class.GetClass().Weapons;
        set
        {
            var data = _class.GetClass();
            var newData = new OmpPlayerClass(data.Team, data.Skin, data.Spawn, data.Angle, value);
            _class.SetClass(ref newData);
        }
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
    /// Performs an implicit conversion from <see cref="PlayerClass" /> to <see cref="IClass" />.
    /// </summary>
    public static implicit operator IClass(PlayerClass playerClass)
    {
        return playerClass._class;
    }
}
