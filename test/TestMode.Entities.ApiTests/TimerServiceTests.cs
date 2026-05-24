using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class TimerServiceTests : TestBase
{
    private ITimerService Sut => Services.GetRequiredService<ITimerService>();

    [Fact]
    public void Start_should_return_active_timer()
    {
        var timer = Sut.Start(_ => { }, TimeSpan.FromSeconds(1));

        try
        {
            timer.ShouldNotBeNull();
            timer.IsActive.ShouldBeTrue();
        }
        finally
        {
            Sut.Stop(timer);
        }
    }

    [Fact]
    public void Start_with_timer_reference_overload_should_return_active_timer()
    {
        var timer = Sut.Start((_, _) => { }, TimeSpan.FromSeconds(1));

        try
        {
            timer.ShouldNotBeNull();
            timer.IsActive.ShouldBeTrue();
        }
        finally
        {
            Sut.Stop(timer);
        }
    }

    [Fact]
    public void Delay_should_return_active_timer()
    {
        var timer = Sut.Delay(_ => { }, TimeSpan.FromSeconds(60));

        try
        {
            timer.ShouldNotBeNull();
            timer.IsActive.ShouldBeTrue();
        }
        finally
        {
            Sut.Stop(timer);
        }
    }

    [Fact]
    public void Stop_should_deactivate_timer()
    {
        var timer = Sut.Start(_ => { }, TimeSpan.FromSeconds(1));

        Sut.Stop(timer);

        timer.IsActive.ShouldBeFalse();
    }

    [Fact]
    public void NextTick_should_be_in_the_future()
    {
        var timer = Sut.Start(_ => { }, TimeSpan.FromSeconds(1));

        try
        {
            timer.NextTick.ShouldBeGreaterThan(TimeSpan.Zero);
        }
        finally
        {
            Sut.Stop(timer);
        }
    }

    [Fact]
    public void Start_with_zero_interval_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Sut.Start(_ => { }, TimeSpan.Zero));
    }

    [Fact]
    public void Start_with_negative_interval_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Sut.Start(_ => { }, TimeSpan.FromSeconds(-1)));
    }

    [Fact]
    public void Delay_with_zero_delay_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Sut.Delay(_ => { }, TimeSpan.Zero));
    }
}
