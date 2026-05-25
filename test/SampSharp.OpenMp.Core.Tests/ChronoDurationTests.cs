using SampSharp.OpenMp.Core.Std.Chrono;
using Shouldly;
using Xunit;

namespace SampSharp.OpenMp.Core.Tests;

public class ChronoDurationTests
{
    [Fact]
    public void Seconds_AsTimeSpan_returns_seconds()
    {
        var s = new Seconds(42);
        s.AsTimeSpan().ShouldBe(TimeSpan.FromSeconds(42));
    }

    [Fact]
    public void Seconds_implicit_to_TimeSpan()
    {
        TimeSpan ts = new Seconds(10);
        ts.ShouldBe(TimeSpan.FromSeconds(10));
    }

    [Fact]
    public void Seconds_implicit_from_TimeSpan()
    {
        Seconds s = TimeSpan.FromSeconds(7);
        ((TimeSpan)s).ShouldBe(TimeSpan.FromSeconds(7));
    }

    [Fact]
    public void Seconds_implicit_from_TimeSpan_truncates_sub_second()
    {
        Seconds s = TimeSpan.FromMilliseconds(1500);
        ((TimeSpan)s).ShouldBe(TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Seconds_ToString_uses_invariant_culture()
    {
        new Seconds(123).ToString().ShouldBe("123");
    }

    [Fact]
    public void Milliseconds_AsTimeSpan_returns_milliseconds()
    {
        new Milliseconds(500).AsTimeSpan().ShouldBe(TimeSpan.FromMilliseconds(500));
    }

    [Fact]
    public void Milliseconds_roundtrip_via_TimeSpan()
    {
        var original = new Milliseconds(750);
        TimeSpan ts = original;
        Milliseconds back = ts;
        ((TimeSpan)back).ShouldBe((TimeSpan)original);
    }

    [Fact]
    public void Milliseconds_implicit_from_TimeSpan_truncates_sub_ms()
    {
        Milliseconds ms = TimeSpan.FromTicks(TimeSpan.TicksPerMillisecond + 5);
        ((TimeSpan)ms).ShouldBe(TimeSpan.FromMilliseconds(1));
    }

    [Fact]
    public void Milliseconds_ToString_uses_invariant_culture()
    {
        new Milliseconds(987).ToString().ShouldBe("987");
    }

    [Fact]
    public void Microseconds_AsTimeSpan_returns_microseconds()
    {
        new Microseconds(2000).AsTimeSpan().ShouldBe(TimeSpan.FromMicroseconds(2000));
    }

    [Fact]
    public void Microseconds_implicit_to_TimeSpan()
    {
        TimeSpan ts = new Microseconds(1000);
        ts.ShouldBe(TimeSpan.FromMicroseconds(1000));
    }

    [Fact]
    public void Microseconds_implicit_from_TimeSpan()
    {
        Microseconds us = TimeSpan.FromMicroseconds(500);
        ((TimeSpan)us).ShouldBe(TimeSpan.FromMicroseconds(500));
    }

    [Fact]
    public void Microseconds_ToString_uses_invariant_culture()
    {
        new Microseconds(54321).ToString().ShouldBe("54321");
    }

    [Fact]
    public void Minutes_AsTimeSpan_returns_minutes()
    {
        new Minutes(5).AsTimeSpan().ShouldBe(TimeSpan.FromMinutes(5));
    }

    [Fact]
    public void Minutes_implicit_to_TimeSpan()
    {
        TimeSpan ts = new Minutes(15);
        ts.ShouldBe(TimeSpan.FromMinutes(15));
    }

    [Fact]
    public void Minutes_implicit_from_TimeSpan_truncates_sub_minute()
    {
        Minutes m = TimeSpan.FromSeconds(125);
        ((TimeSpan)m).ShouldBe(TimeSpan.FromMinutes(2));
    }

    [Fact]
    public void Minutes_ToString_uses_invariant_culture()
    {
        new Minutes(42).ToString().ShouldBe("42");
    }

    [Fact]
    public void Hours_AsTimeSpan_returns_hours()
    {
        new Hours(3).AsTimeSpan().ShouldBe(TimeSpan.FromHours(3));
    }

    [Fact]
    public void Hours_implicit_to_TimeSpan()
    {
        TimeSpan ts = new Hours(24);
        ts.ShouldBe(TimeSpan.FromHours(24));
    }

    [Fact]
    public void Hours_implicit_from_TimeSpan_truncates_sub_hour()
    {
        Hours h = TimeSpan.FromMinutes(125);
        ((TimeSpan)h).ShouldBe(TimeSpan.FromHours(2));
    }

    [Fact]
    public void Hours_ToString_uses_invariant_culture()
    {
        new Hours(99).ToString().ShouldBe("99");
    }
}
