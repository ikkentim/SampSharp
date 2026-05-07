using System;
using Shouldly;
using Xunit;
using SampSharp.Entities.SAMP.Commands;

namespace SampSharp.OpenMp.Entities.Commands.UnitTests.Attributes;

/// <summary>
/// Tests for CommandTagAttribute.
/// </summary>
public class CommandTagAttributeTests
{
    [Fact]
    public void Constructor_SetsKeyAndValue()
    {
        var attr = new CommandTagAttribute("category", "admin");

        attr.Key.ShouldBe("category");
        attr.Value.ShouldBe("admin");
    }

    [Fact]
    public void Constructor_EmptyKey_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandTagAttribute("", "value"));
    }

    [Fact]
    public void Constructor_NullKey_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new CommandTagAttribute(null!, "value"));
    }

    [Fact]
    public void Constructor_NullValue_ThrowsArgumentNullException()
    {
        Should.Throw<ArgumentNullException>(() => new CommandTagAttribute("key", null!));
    }

    [Fact]
    public void AllowMultiple_IsTrue()
    {
        var usageAttr = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
            typeof(CommandTagAttribute), typeof(AttributeUsageAttribute))!;

        usageAttr.AllowMultiple.ShouldBeTrue();
    }
}

/// <summary>
/// Tests for RequiresPermissionAttribute.
/// </summary>
public class RequiresPermissionAttributeTests
{
    [Fact]
    public void Constructor_SetsPermissionTag()
    {
        var attr = new RequiresPermissionAttribute("admin");

        attr.Key.ShouldBe("permission");
        attr.Value.ShouldBe("admin");
    }

    [Fact]
    public void InheritsFromCommandTagAttribute()
    {
        var attr = new RequiresPermissionAttribute("admin");

        attr.ShouldBeAssignableTo<CommandTagAttribute>();
    }
}
