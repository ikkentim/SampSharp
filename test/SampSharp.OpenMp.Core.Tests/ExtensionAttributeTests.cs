using SampSharp.OpenMp.Core;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Core.Tests;

public class ExtensionAttributeTests
{
    [Fact]
    public void Uid_ReturnsCorrectValue()
    {
        var attr = new ExtensionAttribute(0x1234567890abcdef);
        attr.Uid.ToString().ShouldBe("1234567890abcdef");
    }

    [Fact]
    public void Uid_ZeroValue_ReturnsZeroUid()
    {
        var attr = new ExtensionAttribute(0);
        attr.Uid.ToString().ShouldBe("0000000000000000");
    }

    [Fact]
    public void AttributeUsage_IsClass_NotInherited()
    {
        var usage = typeof(ExtensionAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false)
            .Cast<AttributeUsageAttribute>()
            .Single();

        usage.ValidOn.ShouldBe(AttributeTargets.Class);
        usage.Inherited.ShouldBeFalse();
    }

    [Fact]
    public void Attribute_CanBeAppliedToClass()
    {
        var attr = typeof(TestExtensionClass).GetCustomAttributes(typeof(ExtensionAttribute), false)
            .Cast<ExtensionAttribute>()
            .SingleOrDefault();

        attr.ShouldNotBeNull();
        attr.Uid.ToString().ShouldBe("00000000deadbeef");
    }

    [Extension(0xdeadbeef)]
    private class TestExtensionClass;
}
