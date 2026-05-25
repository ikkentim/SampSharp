using System.Linq;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class PlayerWeaponSlotsTests
{
    [Fact]
    public void Default_ctor_creates_empty_slots()
    {
        var slots = new PlayerWeaponSlots();
        slots.Count().ShouldBe(0);
    }

    [Fact]
    public void Default_ctor_indexer_returns_default_slots()
    {
        var slots = new PlayerWeaponSlots();
        for (var i = 0; i < WeaponSlots.MAX_WEAPON_SLOTS; i++)
        {
            slots[i].Weapon.ShouldBe(Weapon.None);
            slots[i].Ammo.ShouldBe(0);
        }
    }

    [Fact]
    public void Ctor_with_data_succeeds_when_length_matches()
    {
        var data = new WeaponSlotData[WeaponSlots.MAX_WEAPON_SLOTS];
        Should.NotThrow(() => new PlayerWeaponSlots(data));
    }

    [Fact]
    public void Ctor_with_data_throws_on_wrong_length()
    {
        var data = new WeaponSlotData[WeaponSlots.MAX_WEAPON_SLOTS - 1];
        Should.Throw<ArgumentException>(() => new PlayerWeaponSlots(data));
    }

    [Fact]
    public void Ctor_with_null_data_throws()
    {
        Should.Throw<ArgumentNullException>(() => new PlayerWeaponSlots(null!));
    }

    [Fact]
    public void Add_places_weapon_in_correct_slot()
    {
        // Colt45 (id 22) -> slot 2
        var slots = new PlayerWeaponSlots();
        slots.Add(new PlayerWeaponSlot(Weapon.Colt45, 100));
        slots[2].Weapon.ShouldBe(Weapon.Colt45);
        slots[2].Ammo.ShouldBe(100);
    }

    [Fact]
    public void Add_throws_for_weapon_with_no_valid_slot()
    {
        var slots = new PlayerWeaponSlots();
        // Connect (id 200) is beyond the WeaponInfo table -> slot -1
        Should.Throw<ArgumentException>(() => slots.Add(new PlayerWeaponSlot(Weapon.Connect, 1)));
    }

    [Fact]
    public void Add_replaces_existing_weapon_in_same_slot()
    {
        // Both Colt45 (22) and Silenced (23) share slot 2
        var slots = new PlayerWeaponSlots();
        slots.Add(new PlayerWeaponSlot(Weapon.Colt45, 50));
        slots.Add(new PlayerWeaponSlot(Weapon.Silenced, 75));
        slots[2].Weapon.ShouldBe(Weapon.Silenced);
        slots[2].Ammo.ShouldBe(75);
    }

    [Fact]
    public void Reset_clears_specified_slot()
    {
        var slots = new PlayerWeaponSlots();
        slots.Add(new PlayerWeaponSlot(Weapon.Colt45, 100));
        slots.Reset((WeaponSlot)2);
        slots[2].Weapon.ShouldBe(Weapon.None);
        slots[2].Ammo.ShouldBe(0);
    }

    [Fact]
    public void Reset_throws_when_slot_negative()
    {
        var slots = new PlayerWeaponSlots();
        Should.Throw<ArgumentOutOfRangeException>(() => slots.Reset((WeaponSlot)(-1)));
    }

    [Fact]
    public void Reset_throws_when_slot_at_max()
    {
        var slots = new PlayerWeaponSlots();
        Should.Throw<ArgumentOutOfRangeException>(() => slots.Reset((WeaponSlot)WeaponSlots.MAX_WEAPON_SLOTS));
    }

    [Fact]
    public void Remove_by_weapon_returns_true_when_present()
    {
        var slots = new PlayerWeaponSlots();
        slots.Add(new PlayerWeaponSlot(Weapon.Colt45, 100));
        slots.Remove(Weapon.Colt45).ShouldBeTrue();
        slots[2].Ammo.ShouldBe(0);
    }

    [Fact]
    public void Remove_by_weapon_returns_false_when_slot_empty()
    {
        var slots = new PlayerWeaponSlots();
        slots.Remove(Weapon.Colt45).ShouldBeFalse();
    }

    [Fact]
    public void Remove_by_item_delegates_to_remove_by_weapon()
    {
        var slots = new PlayerWeaponSlots();
        slots.Add(new PlayerWeaponSlot(Weapon.Colt45, 50));
        slots.Remove(new PlayerWeaponSlot(Weapon.Colt45, 9999)).ShouldBeTrue();
        slots.Remove(new PlayerWeaponSlot(Weapon.Colt45, 0)).ShouldBeFalse();
    }

    [Fact]
    public void Remove_throws_for_weapon_with_no_valid_slot()
    {
        var slots = new PlayerWeaponSlots();
        Should.Throw<ArgumentException>(() => slots.Remove(Weapon.Connect));
    }

    [Fact]
    public void Indexer_throws_for_negative()
    {
        var slots = new PlayerWeaponSlots();
        Should.Throw<ArgumentOutOfRangeException>(() => slots[-1]);
    }

    [Fact]
    public void Indexer_throws_at_max()
    {
        var slots = new PlayerWeaponSlots();
        Should.Throw<ArgumentOutOfRangeException>(() => slots[WeaponSlots.MAX_WEAPON_SLOTS]);
    }

    [Fact]
    public void Enumeration_skips_empty_slots()
    {
        var slots = new PlayerWeaponSlots();
        slots.Add(new PlayerWeaponSlot(Weapon.Colt45, 100));
        slots.Add(new PlayerWeaponSlot(Weapon.Grenade, 5));
        var list = slots.ToList();
        list.Count.ShouldBe(2);
        list.ShouldContain(s => s.Weapon == Weapon.Colt45);
        list.ShouldContain(s => s.Weapon == Weapon.Grenade);
    }

    [Fact]
    public void Enumeration_via_non_generic_interface_works()
    {
        var slots = new PlayerWeaponSlots();
        slots.Add(new PlayerWeaponSlot(Weapon.Colt45, 100));
        var enumerator = ((System.Collections.IEnumerable)slots).GetEnumerator();
        enumerator.MoveNext().ShouldBeTrue();
        ((PlayerWeaponSlot)enumerator.Current!).Weapon.ShouldBe(Weapon.Colt45);
    }

    [Fact]
    public void ToOmpData_wraps_internal_array()
    {
        var slots = new PlayerWeaponSlots();
        slots.Add(new PlayerWeaponSlot(Weapon.Colt45, 100));
        var omp = slots.ToOmpData();
        omp.Data.Length.ShouldBe(WeaponSlots.MAX_WEAPON_SLOTS);
        omp.Data[2].Id.ShouldBe((byte)Weapon.Colt45);
        omp.Data[2].Ammo.ShouldBe(100);
    }
}
