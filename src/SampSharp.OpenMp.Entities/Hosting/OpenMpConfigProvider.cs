using Microsoft.Extensions.Configuration;
using SampSharp.OpenMp.Core.Api;
using System.Globalization;

namespace SampSharp.Entities;

internal class OpenMpConfigProvider(SampSharpEnvironment environment) : ConfigurationProvider, IConfigurationSource
{
    public override void Load()
    {
        Data.Clear();

        var config = environment.Core.GetConfig();

        foreach (var opt in config.GetOptions())
        {
            var key = TransformKey(opt.Key);
            switch (opt.Value)
            {
                case ConfigOptionType.Int:
                    Data[key] = config.GetInt(opt.Key).Value.ToString(CultureInfo.InvariantCulture);
                    break;
                case ConfigOptionType.String:
                    Data[key] = config.GetString(opt.Key);
                    break;
                case ConfigOptionType.Float:
                    Data[key] = config.GetFloat(opt.Key).Value.ToString(CultureInfo.InvariantCulture);
                    break;
                case ConfigOptionType.Strings:
                    var str = config.GetStrings(opt.Key);
                    for (var i = 0; i < str.Length; i++)
                    {
                        Data[$"{key}:{i}"] = str[i];
                    }
                    break;
                case ConfigOptionType.Bool:
                    Data[key] = config.GetBool(opt.Key).Value.ToString();
                    break;
            }
        }

    }

    private static string TransformKey(string input)
    {
        return input.Replace('.', ':').Replace("_", "");
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return this;
    }
}
