using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for the CommandGroup hierarchical path representation.
/// </summary>
public class CommandGroupTests
{
    [Fact]
    public void Constructor_WithParams_InitializesCorrectly()
    {
        var group = new CommandGroup("admin", "money");
        group.Parts.Count.ShouldBe(2);
        group.Parts.ElementAt(0).ShouldBe("admin");
        group.Parts.ElementAt(1).ShouldBe("money");
    }

    [Fact]
    public void Constructor_WithEnumerable_InitializesCorrectly()
    {
        var parts = new[] { "admin", "money", "give" };
        var group = new CommandGroup(parts as IEnumerable<string>);
        group.Parts.Count.ShouldBe(3);
        group.Parts.SequenceEqual(parts).ShouldBeTrue();
    }

    [Fact]
    public void Constructor_WithSinglePart_Succeeds()
    {
        var group = new CommandGroup("admin");
        group.Parts.Count.ShouldBe(1);
        group.Parts.ElementAt(0).ShouldBe("admin");
    }

    [Fact]
    public void Constructor_WithNullParams_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandGroup(null!));
    }

    [Fact]
    public void Constructor_WithEmptyParams_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandGroup(Array.Empty<string>()));
    }

    [Fact]
    public void Constructor_WithNullEnumerable_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandGroup((IEnumerable<string>?)null!));
    }

    [Fact]
    public void Constructor_WithEmptyEnumerable_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandGroup(Array.Empty<string>() as IEnumerable<string>));
    }

    [Fact]
    public void FullName_WithMultipleParts_ReturnsSpaceSeparatedString()
    {
        var group = new CommandGroup("admin", "money");
        group.FullName.ShouldBe("admin money");
    }

    [Fact]
    public void FullName_WithSinglePart_ReturnsPart()
    {
        var group = new CommandGroup("admin");
        group.FullName.ShouldBe("admin");
    }

    [Fact]
    public void Depth_ReturnsNumberOfParts()
    {
        var group = new CommandGroup("admin", "money", "give");
        group.Depth.ShouldBe(3);
    }

    [Fact]
    public void Depth_SinglePart_ReturnsOne()
    {
        var group = new CommandGroup("admin");
        group.Depth.ShouldBe(1);
    }

    [Fact]
    public void GetParent_WithValidDepth_ReturnsSubgroup()
    {
        var group = new CommandGroup("admin", "money", "give");
        var parent = group.GetParent(2);
        parent.Parts.Count.ShouldBe(2);
        parent.Parts.ElementAt(0).ShouldBe("admin");
        parent.Parts.ElementAt(1).ShouldBe("money");
    }

    [Fact]
    public void GetParent_Depth1_ReturnsFirstPart()
    {
        var group = new CommandGroup("admin", "money", "give");
        var parent = group.GetParent(1);
        parent.Parts.Count.ShouldBe(1);
        parent.Parts[0].ShouldBe("admin");
    }

    [Fact]
    public void GetParent_FullDepth_ReturnsCopy()
    {
        var group = new CommandGroup("admin", "money");
        var parent = group.GetParent(2);
        parent.Parts.ShouldBe(group.Parts);
    }

    [Fact]
    public void GetParent_DepthTooLarge_ThrowsArgumentOutOfRangeException()
    {
        var group = new CommandGroup("admin", "money");
        Should.Throw<ArgumentOutOfRangeException>(() => group.GetParent(3));
    }

    [Fact]
    public void GetParent_DepthZero_ThrowsArgumentOutOfRangeException()
    {
        var group = new CommandGroup("admin", "money");
        Should.Throw<ArgumentOutOfRangeException>(() => group.GetParent(0));
    }

    [Fact]
    public void GetParent_NegativeDepth_ThrowsArgumentOutOfRangeException()
    {
        var group = new CommandGroup("admin", "money");
        Should.Throw<ArgumentOutOfRangeException>(() => group.GetParent(-1));
    }

    [Fact]
    public void Stack_WithCommandGroup_CombinesParts()
    {
        var group1 = new CommandGroup("admin", "money");
        var group2 = new CommandGroup("give");
        var stacked = group1.Stack(group2);

        stacked.Parts.Count.ShouldBe(3);
        stacked.Parts.ElementAt(0).ShouldBe("admin");
        stacked.Parts.ElementAt(1).ShouldBe("money");
        stacked.Parts.ElementAt(2).ShouldBe("give");
    }

    [Fact]
    public void Stack_WithMultiplePartsGroup_CombinesAll()
    {
        var group1 = new CommandGroup("admin");
        var group2 = new CommandGroup("money", "give");
        var stacked = group1.Stack(group2);

        stacked.Parts.ShouldHaveCount(3);
        stacked.FullName.ShouldBe("admin money give");
    }

    [Fact]
    public void Stack_WithString_AddsPartToEnd()
    {
        var group = new CommandGroup("admin", "money");
        var stacked = group.Stack("give");

        stacked.Parts.Count.ShouldBe(3);
        stacked.Parts.ElementAt(2).ShouldBe("give");
    }

    [Fact]
    public void Stack_WithNullString_ThrowsArgumentException()
    {
        var group = new CommandGroup("admin");
        Should.Throw<ArgumentException>(() => group.Stack(null!));
    }

    [Fact]
    public void Stack_WithEmptyString_ThrowsArgumentException()
    {
        var group = new CommandGroup("admin");
        Should.Throw<ArgumentException>(() => group.Stack(""));
    }

    [Fact]
    public void Stack_WithWhitespaceString_ThrowsArgumentException()
    {
        var group = new CommandGroup("admin");
        Should.Throw<ArgumentException>(() => group.Stack("   "));
    }

    [Fact]
    public void Equals_SameValues_ReturnsTrue()
    {
        var group1 = new CommandGroup("admin", "money");
        var group2 = new CommandGroup("admin", "money");
        group1.Equals(group2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_DifferentValues_ReturnsFalse()
    {
        var group1 = new CommandGroup("admin", "money");
        var group2 = new CommandGroup("admin", "power");
        group1.Equals(group2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_DifferentLength_ReturnsFalse()
    {
        var group1 = new CommandGroup("admin", "money");
        var group2 = new CommandGroup("admin");
        group1.Equals(group2).ShouldBeFalse();
    }

    [Fact]
    public void GetHashCode_SameValues_AreEqual()
    {
        var group1 = new CommandGroup("admin", "money");
        var group2 = new CommandGroup("admin", "money");
        // Test that equal instances are actually equal
        group1.Equals(group2).ShouldBeTrue();
        group1.Equals((object)group2).ShouldBeTrue();
    }

    [Fact]
    public void ToString_ReturnsSameAsFullName()
    {
        var group = new CommandGroup("admin", "money");
        group.ToString().ShouldBe(group.FullName);
    }

    [Fact]
    public void EqualityOperator_EqualGroups_ReturnsTrue()
    {
        var group1 = new CommandGroup("admin", "money");
        var group2 = new CommandGroup("admin", "money");
        (group1 == group2).ShouldBeTrue();
    }

    [Fact]
    public void InequalityOperator_DifferentGroups_ReturnsTrue()
    {
        var group1 = new CommandGroup("admin", "money");
        var group2 = new CommandGroup("admin", "power");
        (group1 != group2).ShouldBeTrue();
    }
}
