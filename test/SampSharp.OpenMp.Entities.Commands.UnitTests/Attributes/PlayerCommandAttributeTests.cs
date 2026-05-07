using System;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Attributes;

/// <summary>
/// Tests for PlayerCommandAttribute.
/// </summary>
public class PlayerCommandAttributeTests
{
    [Fact]
    public void DefaultConstructor_NameIsNull()
    {
        var attr = new PlayerCommandAttribute();

        attr.Name.ShouldBeNull();
    }

    [Fact]
    public void NameConstructor_SetsName()
    {
        var attr = new PlayerCommandAttribute("kick");

        attr.Name.ShouldBe("kick");
    }

    [Fact]
    public void NameProperty_CanBeSetAfterConstruction()
    {
        var attr = new PlayerCommandAttribute();
        attr.Name = "ban";

        attr.Name.ShouldBe("ban");
    }

    [Fact]
    public void UsageMessageKey_IsNullByDefault()
    {
        var attr = new PlayerCommandAttribute();

        attr.UsageMessageKey.ShouldBeNull();
    }

    [Fact]
    public void UsageMessageKey_CanBeSet()
    {
        var attr = new PlayerCommandAttribute();
        attr.UsageMessageKey = "cmd.kick.usage";

        attr.UsageMessageKey.ShouldBe("cmd.kick.usage");
    }

    [Fact]
    public void ImplementsICommandAttribute()
    {
        var attr = new PlayerCommandAttribute();

        attr.ShouldBeAssignableTo<ICommandAttribute>();
    }

    [Fact]
    public void AllowMultiple_IsTrue()
    {
        var usageAttr = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
            typeof(PlayerCommandAttribute), typeof(AttributeUsageAttribute))!;

        usageAttr.AllowMultiple.ShouldBeTrue();
    }
}
