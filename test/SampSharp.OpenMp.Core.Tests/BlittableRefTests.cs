using SampSharp.OpenMp.Core;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Core.Tests;

public class BlittableRefTests
{
    [Fact]
    public void HasValue_NullPointer_ReturnsFalse()
    {
        var b = new BlittableRef<int>(0);
        b.HasValue.ShouldBeFalse();
    }

    [Fact]
    public void HasValue_NonNullPointer_ReturnsTrue()
    {
        unsafe
        {
            int value = 42;
            var b = new BlittableRef<int>((nint)(&value));
            b.HasValue.ShouldBeTrue();
        }
    }

    [Fact]
    public void Value_NullPointer_ThrowsInvalidOperationException()
    {
        var b = new BlittableRef<int>(0);
        Should.Throw<InvalidOperationException>(() => _ = b.Value);
    }

    [Fact]
    public void Value_NonNullPointer_ReturnsValue()
    {
        unsafe
        {
            int value = 42;
            var b = new BlittableRef<int>((nint)(&value));
            b.Value.ShouldBe(42);
        }
    }

    [Fact]
    public void GetValueOrDefault_NullPointer_ReturnsDefault()
    {
        var b = new BlittableRef<int>(0);
        b.GetValueOrDefault().ShouldBe(0);
    }

    [Fact]
    public void GetValueOrDefault_NonNullPointer_ReturnsValue()
    {
        unsafe
        {
            int value = 99;
            var b = new BlittableRef<int>((nint)(&value));
            b.GetValueOrDefault().ShouldBe(99);
        }
    }

    [Fact]
    public void GetValueOrDefault_WithDefault_NullPointer_ReturnsSpecifiedDefault()
    {
        var b = new BlittableRef<int>(0);
        b.GetValueOrDefault(77).ShouldBe(77);
    }

    [Fact]
    public void GetValueOrDefault_WithDefault_NonNullPointer_ReturnsValue()
    {
        unsafe
        {
            int value = 55;
            var b = new BlittableRef<int>((nint)(&value));
            b.GetValueOrDefault(77).ShouldBe(55);
        }
    }
}
