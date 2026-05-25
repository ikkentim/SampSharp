using SampSharp.OpenMp.Core.Std.Chrono;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Core.Tests;

public class ChronoDurationTests
{
    [Fact]
    public void Seconds_AsTimeSpan_should_return_equivalent_TimeSpan()
    {
        var s = new Seconds(42);
        s.AsTimeSpan().ShouldBe(TimeSpan.FromSeconds(42));
    }

    [Fact]
    public void Seconds_should_implicitly_convert_to_TimeSpan()
    {
        TimeSpan ts = new Seconds(10);
        ts.ShouldBe(TimeSpan.FromSeconds(10));
    }

    [Fact]
    public void Seconds_should_implicitly_convert_from_TimeSpan()
    {
        Seconds s = TimeSpan.FromSeconds(7);
        ((TimeSpan)s).ShouldBe(TimeSpan.FromSeconds(7));
    }

    [Fact]
    public void Seconds_implicit_from_TimeSpan_should_truncate_sub_second_remainder()
    {
        Seconds s = TimeSpan.FromMilliseconds(1500);
        ((TimeSpan)s).ShouldBe(TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Seconds_ToString_should_use_invariant_culture()
    {
        new Seconds(123).ToString().ShouldBe("123");
    }

    [Fact]
    public void Milliseconds_AsTimeSpan_should_return_equivalent_TimeSpan()
    {
        new Milliseconds(500).AsTimeSpan().ShouldBe(TimeSpan.FromMilliseconds(500));
    }

    [Fact]
    public void Milliseconds_should_roundtrip_via_TimeSpan()
    {
        var original = new Milliseconds(750);
        TimeSpan ts = original;
        Milliseconds back = ts;
        ((TimeSpan)back).ShouldBe((TimeSpan)original);
    }

    [Fact]
    public void Milliseconds_implicit_from_TimeSpan_should_truncate_sub_millisecond_remainder()
    {
        Milliseconds ms = TimeSpan.FromTicks(TimeSpan.TicksPerMillisecond + 5);
        ((TimeSpan)ms).ShouldBe(TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    public void Milliseconds_ToString_should_use_invariant_culture()
    {
        new Milliseconds(987).ToString().ShouldBe("987");
    }

    [Fact]
    public void Microseconds_AsTimeSpan_should_return_equivalent_TimeSpan()
    {
        new Microseconds(2000).AsTimeSpan().ShouldBe(TimeSpan.FromMicroseconds(2000));
    }

    [Fact]
    public void Microseconds_should_implicitly_convert_to_TimeSpan()
    {
        TimeSpan ts = new Microseconds(1000);
        ts.ShouldBe(TimeSpan.FromMicroseconds(1000));
    }

    [Fact]
    public void Microseconds_should_implicitly_convert_from_TimeSpan()
    {
        Microseconds us = TimeSpan.FromMicroseconds(500);
        ((TimeSpan)us).ShouldBe(TimeSpan.FromMicroseconds(500));
    }

    [Fact]
    public void Microseconds_ToString_should_use_invariant_culture()
    {
        new Microseconds(54321).ToString().ShouldBe("54321");
    }

    [Fact]
    public void Minutes_AsTimeSpan_should_return_equivalent_TimeSpan()
    {
        new Minutes(5).AsTimeSpan().ShouldBe(TimeSpan.FromMinutes(5));
    }

    [Fact]
    public void Minutes_should_implicitly_convert_to_TimeSpan()
    {
        TimeSpan ts = new Minutes(15);
        ts.ShouldBe(TimeSpan.FromMinutes(15));
    }

    [Fact]
    public void Minutes_implicit_from_TimeSpan_should_truncate_sub_minute_remainder()
    {
        Minutes m = TimeSpan.FromSeconds(125);
        ((TimeSpan)m).ShouldBe(TimeSpan.FromMinutes(2));
    }

    [Fact]
    public void Minutes_ToString_should_use_invariant_culture()
    {
        new Minutes(42).ToString().ShouldBe("42");
    }

    [Fact]
    public void Hours_AsTimeSpan_should_return_equivalent_TimeSpan()
    {
        new Hours(3).AsTimeSpan().ShouldBe(TimeSpan.FromHours(3));
    }

    [Fact]
    public void Hours_should_implicitly_convert_to_TimeSpan()
    {
        TimeSpan ts = new Hours(24);
        ts.ShouldBe(TimeSpan.FromHours(24));
    }

    [Fact]
    public void Hours_implicit_from_TimeSpan_should_truncate_sub_hour_remainder()
    {
        Hours h = TimeSpan.FromMinutes(125);
        ((TimeSpan)h).ShouldBe(TimeSpan.FromHours(2));
    }

    [Fact]
    public void Hours_ToString_should_use_invariant_culture()
    {
        new Hours(99).ToString().ShouldBe("99");
    }
}
