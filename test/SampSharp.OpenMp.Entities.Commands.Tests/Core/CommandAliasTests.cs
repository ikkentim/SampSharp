using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Core;

/// <summary>
/// Tests for the CommandAlias shorthand command name representation.
/// </summary>
public class CommandAliasTests
{
    [Fact]
    public void Constructor_WithValidName_InitializesCorrectly()
    {
        var alias = new CommandAlias("pm");
        alias.Name.ShouldBe("pm");
    }

    [Fact]
    public void Constructor_WithNullName_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandAlias(null!));
    }

    [Fact]
    public void Constructor_WithEmptyName_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandAlias(""));
    }

    [Fact]
    public void Constructor_WithWhitespaceName_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandAlias("   "));
    }

    [Fact]
    public void Equals_SameNames_ReturnsTrue()
    {
        var alias1 = new CommandAlias("pm");
        var alias2 = new CommandAlias("pm");
        alias1.Equals(alias2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_DifferentNames_ReturnsFalse()
    {
        var alias1 = new CommandAlias("pm");
        var alias2 = new CommandAlias("msg");
        alias1.Equals(alias2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WithObject_SameAlias_ReturnsTrue()
    {
        var alias1 = new CommandAlias("pm");
        object alias2 = new CommandAlias("pm");
        alias1.Equals(alias2).ShouldBeTrue();
    }

    [Fact]
    public void Equals_WithObject_DifferentAlias_ReturnsFalse()
    {
        var alias1 = new CommandAlias("pm");
        object alias2 = new CommandAlias("msg");
        alias1.Equals(alias2).ShouldBeFalse();
    }

    [Fact]
    public void Equals_WithNonAliasObject_ReturnsFalse()
    {
        var alias = new CommandAlias("pm");
        alias.Equals((object)"pm").ShouldBeFalse();
    }

    [Fact]
    public void GetHashCode_SameNames_ReturnsSameHashCode()
    {
        var alias1 = new CommandAlias("pm");
        var alias2 = new CommandAlias("pm");
        alias1.GetHashCode().ShouldBe(alias2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentNames_ReturnsDifferentHashCode()
    {
        var alias1 = new CommandAlias("pm");
        var alias2 = new CommandAlias("msg");
        alias1.GetHashCode().ShouldNotBe(alias2.GetHashCode());
    }

    [Fact]
    public void ToString_ReturnsName()
    {
        var alias = new CommandAlias("pm");
        alias.ToString().ShouldBe("pm");
    }

    [Fact]
    public void EqualityOperator_SameNames_ReturnsTrue()
    {
        var alias1 = new CommandAlias("pm");
        var alias2 = new CommandAlias("pm");
        (alias1 == alias2).ShouldBeTrue();
    }

    [Fact]
    public void EqualityOperator_DifferentNames_ReturnsFalse()
    {
        var alias1 = new CommandAlias("pm");
        var alias2 = new CommandAlias("msg");
        (alias1 == alias2).ShouldBeFalse();
    }

    [Fact]
    public void InequalityOperator_DifferentNames_ReturnsTrue()
    {
        var alias1 = new CommandAlias("pm");
        var alias2 = new CommandAlias("msg");
        (alias1 != alias2).ShouldBeTrue();
    }

    [Fact]
    public void InequalityOperator_SameNames_ReturnsFalse()
    {
        var alias1 = new CommandAlias("pm");
        var alias2 = new CommandAlias("pm");
        (alias1 != alias2).ShouldBeFalse();
    }

    [Fact]
    public void CanBeUsedAsHashTableKey()
    {
        var alias1 = new CommandAlias("pm");
        var alias2 = new CommandAlias("pm");

        var dict = new Dictionary<CommandAlias, string> { { alias1, "value1" } };
        dict[alias2].ShouldBe("value1");
    }

    [Fact]
    public void CanBeUsedInHashSet()
    {
        var alias1 = new CommandAlias("pm");
        var alias2 = new CommandAlias("pm");
        var alias3 = new CommandAlias("msg");

        var set = new HashSet<CommandAlias> { alias1, alias2, alias3 };
        set.Count.ShouldBe(2);
    }
}
