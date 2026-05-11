using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities.SAMP;
using Shouldly;
using Xunit;

namespace TestMode.UnitTests;

public class WorldServiceTests : TestBase
{
    private IWorldService Sut => Services.GetRequiredService<IWorldService>();

    [Fact]
    public void Gravity_set_should_succeed()
    {
        Sut.Gravity = 0.008f;
    }

    [Fact]
    public void Gravity_roundtrip_should_succeed()
    {
        Sut.Gravity = 0.008f;
        Sut.Gravity.ShouldBe(0.008f);
    }

    [Fact]
    public void Gravity_below_minus_50_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Sut.Gravity = -50.1f);
    }

    [Fact]
    public void Gravity_above_50_should_throw()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Sut.Gravity = 50.1f);
    }
}
