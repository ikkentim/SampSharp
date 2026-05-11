using System.Numerics;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.UnitTests;

public class PlayerWeaponSlotsTests
{
    [Fact]
    public void Add_should_place_weapon_in_associated_slot()
    {
        var sut = new PlayerWeaponSlots();

        sut.Add(new PlayerWeaponSlot(Weapon.Golfclub, 1));

        sut[(int)WeaponSlot.Melee].ShouldBe(new PlayerWeaponSlot(Weapon.Golfclub, 1));
    }

    [Fact]
    public void Add_should_override_existing_weapon_in_same_slot()
    {
        var sut = new PlayerWeaponSlots();

        sut.Add(new PlayerWeaponSlot(Weapon.Colt45, 12));
        sut.Add(new PlayerWeaponSlot(Weapon.Deagle, 24));

        sut[(int)WeaponSlot.Pistol].ShouldBe(new PlayerWeaponSlot(Weapon.Deagle, 24));
        sut.ShouldHaveSingleItem().ShouldBe(new PlayerWeaponSlot(Weapon.Deagle, 24));
    }

    [Fact]
    public void GetEnumerator_should_return_non_empty_slots_in_slot_order()
    {
        var sut = new PlayerWeaponSlots();
        sut.Add(new PlayerWeaponSlot(Weapon.Deagle, 14));
        sut.Add(new PlayerWeaponSlot(Weapon.Golfclub, 1));

        sut.ShouldBe(
        [
            new PlayerWeaponSlot(Weapon.Golfclub, 1),
            new PlayerWeaponSlot(Weapon.Deagle, 14)
        ]);
    }

    [Fact]
    public void Reset_should_clear_specified_slot()
    {
        var sut = new PlayerWeaponSlots();
        sut.Add(new PlayerWeaponSlot(Weapon.Deagle, 14));

        sut.Reset(WeaponSlot.Pistol);

        sut[(int)WeaponSlot.Pistol].ShouldBe(default);
        sut.ShouldBeEmpty();
    }

    [Fact]
    public void Remove_should_clear_associated_slot()
    {
        var sut = new PlayerWeaponSlots();
        sut.Add(new PlayerWeaponSlot(Weapon.Deagle, 14));

        sut.Remove(Weapon.Deagle).ShouldBeTrue();
        sut.Remove(new PlayerWeaponSlot(Weapon.Deagle, 14)).ShouldBeFalse();
        sut[(int)WeaponSlot.Pistol].ShouldBe(default);
    }

    [Fact]
    public void Collection_expression_should_create_player_weapon_slots()
    {
        var spawnData = new PlayerSpawnData(1, 2, new Vector3(1, 2, 3), 4,
            [new PlayerWeaponSlot(Weapon.Golfclub, 1)]);

        spawnData.Weapons[(int)WeaponSlot.Melee].ShouldBe(new PlayerWeaponSlot(Weapon.Golfclub, 1));
        spawnData.Weapons.ShouldHaveSingleItem().ShouldBe(new PlayerWeaponSlot(Weapon.Golfclub, 1));
    }
}
