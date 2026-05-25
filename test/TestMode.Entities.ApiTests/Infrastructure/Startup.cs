using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities;
using SampSharp.OpenMp.Core;
using Shouldly;

namespace TestMode.Entities.ApiTests;

public class Startup : IStartup
{
    public void Initialize(IStartupContext context)
    {
        ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0005f;
        context.UseEntities()
            .ConfigureServices(services =>
            {
                services.AddSingleton(new TestContext(Directory.GetCurrentDirectory()));
            });
    }
}

public record TestContext(string ServerDirectory);