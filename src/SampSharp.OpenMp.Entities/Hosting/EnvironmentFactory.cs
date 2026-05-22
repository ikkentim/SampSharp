using SampSharp.OpenMp.Core;

namespace SampSharp.Entities;

internal static class EnvironmentFactory
{
    public static SampSharpEnvironment Create(IStartupContext context)
    {
        var environment = new SampSharpEnvironment(
            context.Configurator.GetType().Assembly,
            context.Core,
            context.ComponentList,
            new SafeComponentHandleProvider(context),
            GetEnvironmentName(context));
        return environment;
    }

    private static string GetEnvironmentName(IStartupContext context)
    {
        var environment = context.Core.GetConfig().GetString("sampsharp.environment");

        if (!string.IsNullOrEmpty(environment))
        {
            return environment;
        }

        environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

        if (!string.IsNullOrEmpty(environment))
        {
            return environment;
        }

        environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (!string.IsNullOrEmpty(environment))
        {
            return environment;
        }


        return "Production";
    }
}