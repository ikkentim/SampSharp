using System;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Attributes;

/// <summary>
/// Tests for ConsoleCommandAttribute.
/// </summary>
public class ConsoleCommandAttributeTests
{
    [Fact]
    public void DefaultConstructor_NameIsNull()
    {
        var attr = new ConsoleCommandAttribute();

        attr.Name.ShouldBeNull();
    }

    [Fact]
    public void NameConstructor_SetsName()
    {
        var attr = new ConsoleCommandAttribute("status");

        attr.Name.ShouldBe("status");
    }

    [Fact]
    public void NameProperty_CanBeSetAfterConstruction()
    {
        var attr = new ConsoleCommandAttribute();
        attr.Name = "reload";

        attr.Name.ShouldBe("reload");
    }

    [Fact]
    public void UsageMessageKey_IsNullByDefault()
    {
        var attr = new ConsoleCommandAttribute();

        attr.UsageMessageKey.ShouldBeNull();
    }

    [Fact]
    public void UsageMessageKey_CanBeSet()
    {
        var attr = new ConsoleCommandAttribute();
        attr.UsageMessageKey = "cmd.status.usage";

        attr.UsageMessageKey.ShouldBe("cmd.status.usage");
    }

    [Fact]
    public void ImplementsICommandAttribute()
    {
        var attr = new ConsoleCommandAttribute();

        attr.ShouldBeAssignableTo<ICommandAttribute>();
    }

    [Fact]
    public void AllowMultiple_IsTrue()
    {
        var usageAttr = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
            typeof(ConsoleCommandAttribute), typeof(AttributeUsageAttribute))!;

        usageAttr.AllowMultiple.ShouldBeTrue();
    }
}
