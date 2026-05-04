using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace TestMode.UnitTests;

public class ConfigTests : TestBase
{
    private IConfig _config;

    public ConfigTests()
    {
        _config = Services.GetRequiredService<SampSharpEnvironment>().Core.GetConfig();
    }

    [Fact]
    public void GetOptions_should_succeed()
    {
        var options = _config.GetOptions();

        options.Count.ShouldBeGreaterThan(10);
        options["sampsharp.directory"].ShouldBe(ConfigOptionType.String);
    }
}