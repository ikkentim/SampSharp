using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class ConfigService(SampSharpEnvironment environment) : IConfigService
{
    private readonly IConfig _config = environment.Core.GetConfig();

    public string? GetString(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        return _config.GetValueType(key) == ConfigOptionType.String ? _config.GetString(key) : null;
    }

    public int? GetInt(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        if (_config.GetValueType(key) != ConfigOptionType.Int)
        {
            return null;
        }
        var value = _config.GetInt(key);
        return value.HasValue ? value.Value : null;
    }

    public float? GetFloat(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        if (_config.GetValueType(key) != ConfigOptionType.Float)
        {
            return null;
        }
        var value = _config.GetFloat(key);
        return value.HasValue ? value.Value : null;
    }

    public bool? GetBool(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        if (_config.GetValueType(key) != ConfigOptionType.Bool)
        {
            return null;
        }
        var value = _config.GetBool(key);
        return value.HasValue ? value.Value : null;
    }

    public string?[] GetStrings(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        return _config.GetStrings(key);
    }

    public ConfigOptionType GetValueType(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        return _config.GetValueType(key);
    }
}
