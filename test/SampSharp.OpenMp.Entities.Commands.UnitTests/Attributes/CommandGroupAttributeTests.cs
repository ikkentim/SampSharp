using System;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Attributes;

/// <summary>
/// Tests for CommandGroupAttribute.
/// </summary>
public class CommandGroupAttributeTests
{
    [Fact]
    public void SinglePartConstructor_SetsParts()
    {
        var attr = new CommandGroupAttribute("admin");

        attr.Parts.ShouldBe(new[] { "admin" });
    }

    [Fact]
    public void MultiplePartsConstructor_SetsParts()
    {
        var attr = new CommandGroupAttribute("admin", "money");

        attr.Parts.ShouldBe(new[] { "admin", "money" });
    }

    [Fact]
    public void SinglePartConstructor_EmptyPart_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandGroupAttribute(""));
    }

    [Fact]
    public void SinglePartConstructor_WhitespacePart_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandGroupAttribute("  "));
    }

    [Fact]
    public void MultiplePartsConstructor_EmptyParts_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandGroupAttribute(Array.Empty<string>()));
    }

    [Fact]
    public void MultiplePartsConstructor_WhitespacePartAmongValid_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandGroupAttribute("admin", ""));
    }

    [Fact]
    public void AllowMultiple_IsTrue()
    {
        var usageAttr = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
            typeof(CommandGroupAttribute), typeof(AttributeUsageAttribute))!;

        usageAttr.AllowMultiple.ShouldBeTrue();
    }

    [Fact]
    public void TargetsClassAndMethod()
    {
        var usageAttr = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
            typeof(CommandGroupAttribute), typeof(AttributeUsageAttribute))!;

        (usageAttr.ValidOn & AttributeTargets.Class).ShouldBe(AttributeTargets.Class);
        (usageAttr.ValidOn & AttributeTargets.Method).ShouldBe(AttributeTargets.Method);
    }
}
