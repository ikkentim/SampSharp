using Microsoft.Extensions.Configuration;

namespace SampSharp.Entities;

internal class ConfigurationFactory
{
    public static IConfiguration Create(SampSharpEnvironment environment, Action<IConfigurationBuilder> configure)
    {
        var basePath = Path.GetDirectoryName(environment.EntryAssembly.Location) ?? ".";

        var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .Add(new OpenMpConfigProvider(environment))
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        configure(builder);

        return builder.Build();
    }

}
