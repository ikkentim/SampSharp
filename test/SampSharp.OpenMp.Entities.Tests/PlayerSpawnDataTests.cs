using System.Numerics;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class PlayerSpawnDataTests
{
    [Fact]
    public void Default_ctor_initializes_weapons()
    {
        var data = new PlayerSpawnData();
        data.Weapons.ShouldNotBeNull();
    }

    [Fact]
    public void Default_ctor_has_default_team_skin_angle()
    {
        var data = new PlayerSpawnData();
        data.Team.ShouldBe(0);
        data.Skin.ShouldBe(0);
        data.Angle.ShouldBe(0f);
        data.Location.ShouldBe(Vector3.Zero);
    }

    [Fact]
    public void Parameterized_ctor_sets_all_fields()
    {
        var weapons = new PlayerWeaponSlots();
        weapons.Add(new PlayerWeaponSlot(Weapon.Colt45, 50));
        var data = new PlayerSpawnData(team: 3, skin: 7, location: new Vector3(1, 2, 3), angle: 90f, weapons: weapons);
        data.Team.ShouldBe(3);
        data.Skin.ShouldBe(7);
        data.Location.ShouldBe(new Vector3(1, 2, 3));
        data.Angle.ShouldBe(90f);
        data.Weapons.ShouldBe(weapons);
    }

    [Fact]
    public void Properties_are_settable()
    {
        var data = new PlayerSpawnData
        {
            Team = 5,
            Skin = 99,
            Location = new Vector3(10, 20, 30),
            Angle = 45f
        };
        data.Team.ShouldBe(5);
        data.Skin.ShouldBe(99);
        data.Location.ShouldBe(new Vector3(10, 20, 30));
        data.Angle.ShouldBe(45f);
    }

    [Fact]
    public void ToOmpData_serializes_fields()
    {
        var weapons = new PlayerWeaponSlots();
        weapons.Add(new PlayerWeaponSlot(Weapon.Colt45, 50));
        var data = new PlayerSpawnData(team: 3, skin: 7, location: new Vector3(1, 2, 3), angle: 90f, weapons: weapons);
        var omp = data.ToOmpData();
        omp.Team.ShouldBe(3);
        omp.Skin.ShouldBe(7);
        omp.Spawn.ShouldBe(new Vector3(1, 2, 3));
        omp.Angle.ShouldBe(90f);
        omp.Weapons.Data[2].Id.ShouldBe((byte)Weapon.Colt45);
        omp.Weapons.Data[2].Ammo.ShouldBe(50);
    }

    [Fact]
    public void FromOmpData_deserializes_fields()
    {
        var weaponSlotData = new WeaponSlotData[WeaponSlots.MAX_WEAPON_SLOTS];
        weaponSlotData[2] = new WeaponSlotData((byte)Weapon.Colt45, 75);
        var ompClass = new PlayerClass(team: 2, skin: 8, spawn: new Vector3(4, 5, 6), angle: 180f, weapons: new WeaponSlots(weaponSlotData));
        var data = PlayerSpawnData.FromOmpData(ref ompClass);
        data.Team.ShouldBe(2);
        data.Skin.ShouldBe(8);
        data.Location.ShouldBe(new Vector3(4, 5, 6));
        data.Angle.ShouldBe(180f);
        data.Weapons[2].Weapon.ShouldBe(Weapon.Colt45);
        data.Weapons[2].Ammo.ShouldBe(75);
    }

    [Fact]
    public void OmpData_roundtrip_preserves_fields()
    {
        var weapons = new PlayerWeaponSlots();
        weapons.Add(new PlayerWeaponSlot(Weapon.Grenade, 3));
        var original = new PlayerSpawnData(1, 50, new Vector3(7, 8, 9), 270f, weapons);
        var omp = original.ToOmpData();
        var roundtrip = PlayerSpawnData.FromOmpData(ref omp);
        roundtrip.Team.ShouldBe(original.Team);
        roundtrip.Skin.ShouldBe(original.Skin);
        roundtrip.Location.ShouldBe(original.Location);
        roundtrip.Angle.ShouldBe(original.Angle);
        roundtrip.Weapons[8].Weapon.ShouldBe(Weapon.Grenade);
        roundtrip.Weapons[8].Ammo.ShouldBe(3);
    }
}
