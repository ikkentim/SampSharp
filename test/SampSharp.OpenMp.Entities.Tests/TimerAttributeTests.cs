using SampSharp.Entities;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Entities.Tests;

public class TimerAttributeTests
{
    [Fact]
    public void Ctor_sets_interval_property()
    {
        var attr = new TimerAttribute(123.5);
        attr.Interval.ShouldBe(123.5);
    }

    [Fact]
    public void Interval_is_settable()
    {
        var attr = new TimerAttribute(10);
        attr.Interval = 500;
        attr.Interval.ShouldBe(500);
    }
}
