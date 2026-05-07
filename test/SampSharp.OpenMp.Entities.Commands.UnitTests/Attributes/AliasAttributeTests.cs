using System;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Attributes;

/// <summary>
/// Tests for AliasAttribute.
/// </summary>
public class AliasAttributeTests
{
    [Fact]
    public void SingleAliasConstructor_SetsAliases()
    {
        var attr = new AliasAttribute("pm");

        attr.Aliases.ShouldBe(new[] { "pm" });
    }

    [Fact]
    public void MultipleAliasConstructor_SetsAliases()
    {
        var attr = new AliasAttribute("pm", "msg");

        attr.Aliases.ShouldBe(new[] { "pm", "msg" });
    }

    [Fact]
    public void SingleAliasConstructor_EmptyAlias_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new AliasAttribute(""));
    }

    [Fact]
    public void SingleAliasConstructor_WhitespaceAlias_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new AliasAttribute("  "));
    }

    [Fact]
    public void MultipleAliasConstructor_NoAliases_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new AliasAttribute(Array.Empty<string>()));
    }

    [Fact]
    public void MultipleAliasConstructor_EmptyAliasAmongValid_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new AliasAttribute("pm", ""));
    }

    [Fact]
    public void AllowMultiple_IsTrue()
    {
        var usageAttr = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
            typeof(AliasAttribute), typeof(AttributeUsageAttribute))!;

        usageAttr.AllowMultiple.ShouldBeTrue();
    }
}
