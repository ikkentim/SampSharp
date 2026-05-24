using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.OpenMp.Core.Api;
using Shouldly;
using Xunit;

namespace TestMode.Entities.ApiTests;

public class ConfigTests : TestBase
{
    private IConfig _config;
    private IConfigService _configService;

    public ConfigTests()
    {
        _config = Services.GetRequiredService<SampSharpEnvironment>().Core.GetConfig();
        _configService = Services.GetRequiredService<IConfigService>();
    }

    [Fact]
    public void GetOptions_should_succeed()
    {
        var options = _config.GetOptions();

        options.Count.ShouldBeGreaterThan(10);
        options["sampsharp.directory"].ShouldBe(ConfigOptionType.String);
    }

    [Fact]
    public void ConfigService_GetOptions_should_return_non_empty_dictionary()
    {
        var options = _configService.GetOptions();
        options.ShouldNotBeEmpty();
        options.ContainsKey("sampsharp.directory").ShouldBeTrue();
    }

    [Fact]
    public void ConfigService_GetString_should_return_value_for_known_key()
    {
        var value = _configService.GetString("sampsharp.directory");
        value.ShouldNotBeNull();
    }

    [Fact]
    public void ConfigService_GetString_should_return_null_for_missing_key()
    {
        var value = _configService.GetString("this_key_does_not_exist");
        value.ShouldBeNull();
    }

    [Fact]
    public void ConfigService_GetInt_should_return_null_for_missing_key()
    {
        var value = _configService.GetInt("this_key_does_not_exist");
        value.ShouldBeNull();
    }

    [Fact]
    public void ConfigService_GetFloat_should_return_null_for_missing_key()
    {
        var value = _configService.GetFloat("this_key_does_not_exist");
        value.ShouldBeNull();
    }

    [Fact]
    public void ConfigService_GetBool_should_return_null_for_missing_key()
    {
        var value = _configService.GetBool("this_key_does_not_exist");
        value.ShouldBeNull();
    }

    [Fact]
    public void ConfigService_GetStrings_should_return_empty_for_missing_key()
    {
        var values = _configService.GetStrings("this_key_does_not_exist");
        values.ShouldBeEmpty();
    }

    [Fact]
    public void ConfigService_GetValueType_should_return_string_for_known_key()
    {
        var type = _configService.GetValueType("sampsharp.directory");
        type.ShouldBe(ConfigOptionType.String);
    }

    [Fact]
    public void ConfigService_GetInt_should_return_value_for_int_key()
    {
        var options = _configService.GetOptions();
        var intKey = options.FirstOrDefault(kv => kv.Value == ConfigOptionType.Int).Key;

        if (intKey == null)
            return; // no int keys in this config — skip

        var value = _configService.GetInt(intKey);
        value.ShouldNotBeNull();
    }

    [Fact]
    public void ConfigService_GetBool_should_return_value_for_bool_key()
    {
        var options = _configService.GetOptions();
        var boolKey = options.FirstOrDefault(kv => kv.Value == ConfigOptionType.Bool).Key;

        if (boolKey == null)
            return; // no bool keys in this config — skip

        var value = _configService.GetBool(boolKey);
        value.ShouldNotBeNull();
    }
}