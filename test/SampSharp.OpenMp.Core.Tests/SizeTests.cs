using SampSharp.OpenMp.Core.Std;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Core.Tests;

public class SizeTests
{
    [Fact]
    public void Ctor_sets_value()
    {
        var s = new Size(42);
        s.Value.ShouldBe((nint)42);
    }

    [Fact]
    public void ToInt32_converts_value()
    {
        var s = new Size(123);
        s.ToInt32().ShouldBe(123);
    }

    [Fact]
    public void Explicit_to_int()
    {
        var s = new Size(500);
        ((int)s).ShouldBe(500);
    }

    [Fact]
    public void Implicit_from_int()
    {
        Size s = 99;
        s.Value.ShouldBe((nint)99);
    }

    [Fact]
    public void Length_is_64_bit()
    {
        Size.Length.ShouldBe(8);
    }
}
